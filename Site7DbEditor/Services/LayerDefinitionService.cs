using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Site7DbEditor.Services
{
    public enum LayerGroup
    {
        Ikou = 0,    // 遺構 (Layer遺構.txt)
        Ibutu = 1,   // 遺物 (Layer遺物.txt)
        Kikai = 2,   // 基準点 (Layer基準点.txt)
        Sakuzu = 3   // 作図 (Layer作図.txt)
    }

    public class LayerItem
    {
        public int Index { get; set; } = 1;         // 1..16
        public string Code { get; set; } = "L01";  // L01..L16, K01..K16, D01..D16
        public string Name { get; set; } = "";     // レイヤ名
        public int Color { get; set; } = 1;        // 表示色番号 (1..16)
        public int Mark { get; set; } = 1;         // マーク (1..8)
        public double Size { get; set; } = 1.0;    // サイズ (デフォルト1.0)
        public int Width { get; set; } = 1;        // 線幅 (1..5)
        public int LType { get; set; } = 1;        // 線種 (1:折線, 2:曲線)

        public string DisplayText => string.IsNullOrWhiteSpace(Name) || Name == Code 
            ? Code 
            : $"{Code} {Name}";
    }

    public class LayerDefinitionService
    {
        private static readonly Lazy<LayerDefinitionService> _instance = new(() => new LayerDefinitionService());
        public static LayerDefinitionService Instance => _instance.Value;

        public const string DefaultSystemDefDir = @"C:\SITE7\GENBA\NEW\Def";
        public const string FallbackSystemDefDir = @"C:\SITE7\DEF";

        public static readonly Dictionary<LayerGroup, string> FileNames = new()
        {
            { LayerGroup.Ikou, "Layer遺構.txt" },
            { LayerGroup.Ibutu, "Layer遺物.txt" },
            { LayerGroup.Kikai, "Layer基準点.txt" },
            { LayerGroup.Sakuzu, "Layer作図.txt" }
        };

        public static readonly Dictionary<LayerGroup, string> DisplayNames = new()
        {
            { LayerGroup.Ikou, "遺構レイヤ" },
            { LayerGroup.Ibutu, "遺物レイヤ" },
            { LayerGroup.Kikai, "基準点レイヤ" },
            { LayerGroup.Sakuzu, "作図レイヤ" }
        };

        public Dictionary<LayerGroup, List<LayerItem>> Groups { get; } = new();

        static LayerDefinitionService()
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            }
            catch { }
        }

        public LayerDefinitionService()
        {
            foreach (LayerGroup g in Enum.GetValues(typeof(LayerGroup)))
            {
                Groups[g] = CreateDefaultLayers(g);
            }
        }

        public static List<LayerItem> CreateDefaultLayers(LayerGroup group)
        {
            var list = new List<LayerItem>();
            string prefix = group switch
            {
                LayerGroup.Kikai => "K",
                LayerGroup.Sakuzu => "D",
                _ => "L"
            };

            for (int i = 1; i <= 16; i++)
            {
                string code = $"{prefix}{i:D2}";
                string name;
                int defaultColor = ((i - 1) % 16) + 1;
                int defaultWidth = 1;
                int defaultMark = 1;
                double defaultSize = 1.0;
                int defaultLType = (group == LayerGroup.Ikou) ? 2 : 1;

                if (group == LayerGroup.Sakuzu)
                {
                    switch (i)
                    {
                        case 1:
                            name = "外枠";
                            defaultColor = 1; // 黒
                            defaultWidth = 2;
                            defaultSize = 1.0;
                            break;
                        case 2:
                            name = "内枠";
                            defaultColor = 4; // 青
                            defaultWidth = 1;
                            defaultSize = 1.0;
                            break;
                        case 3:
                            name = "座標グリッド";
                            defaultColor = 16; // 暗灰
                            defaultWidth = 1;
                            defaultSize = 1.0;
                            break;
                        case 4:
                            name = "座標値";
                            defaultColor = 1; // 黒
                            defaultWidth = 1;
                            defaultSize = 2.5;
                            break;
                        case 5:
                            name = "方位記号";
                            defaultColor = 1; // 黒
                            defaultWidth = 1;
                            defaultSize = 15.0;
                            break;
                        case 6:
                            name = "スケールバー";
                            defaultColor = 1; // 黒
                            defaultWidth = 1;
                            defaultSize = 2.5;
                            break;
                        case 7:
                            name = "表題欄";
                            defaultColor = 1; // 黒
                            defaultWidth = 1;
                            defaultSize = 3.0;
                            break;
                        case 8:
                            name = "凡例";
                            defaultColor = 1; // 黒
                            defaultWidth = 1;
                            defaultSize = 2.5;
                            break;
                        case 9:
                            name = "断面線";
                            defaultColor = 2; // 赤
                            defaultWidth = 1;
                            defaultSize = 1.0;
                            break;
                        case 10:
                            name = "断面名・標高";
                            defaultColor = 2; // 赤
                            defaultWidth = 1;
                            defaultSize = 3.0;
                            break;
                        default:
                            name = $"リザーブ{i:D2}";
                            defaultColor = i;
                            defaultWidth = 1;
                            defaultSize = 1.0;
                            break;
                    }
                }
                else
                {
                    name = group switch
                    {
                        LayerGroup.Ibutu => (i == 1) ? "遺物L01" : $"遺物L{i:D2}",
                        LayerGroup.Kikai => (i == 1) ? "基準点" : $"基準点L{i:D2}",
                        _ => code
                    };
                }

                list.Add(new LayerItem
                {
                    Index = i,
                    Code = code,
                    Name = name,
                    Color = defaultColor,
                    Mark = defaultMark,
                    Size = defaultSize,
                    Width = defaultWidth,
                    LType = defaultLType
                });
            }

            return list;
        }

        public string GetSystemDefDirectory()
        {
            if (Directory.Exists(DefaultSystemDefDir))
                return DefaultSystemDefDir;
            if (Directory.Exists(FallbackSystemDefDir))
                return FallbackSystemDefDir;

            try
            {
                Directory.CreateDirectory(DefaultSystemDefDir);
            }
            catch { }
            return DefaultSystemDefDir;
        }

        public string GetEffectiveDefDirectory(string? genbaDbPath)
        {
            if (!string.IsNullOrEmpty(genbaDbPath))
            {
                string? genbaDir = Path.GetDirectoryName(genbaDbPath);
                if (!string.IsNullOrEmpty(genbaDir))
                {
                    string genbaDefDir = Path.Combine(genbaDir, "Def");
                    if (Directory.Exists(genbaDefDir))
                    {
                        return genbaDefDir;
                    }
                }
            }
            return GetSystemDefDirectory();
        }

        public void LoadAll(string? genbaDbPath)
        {
            foreach (LayerGroup group in Enum.GetValues(typeof(LayerGroup)))
            {
                LoadGroup(group, genbaDbPath);
            }
        }

        public List<LayerItem> LoadGroup(LayerGroup group, string? genbaDbPath)
        {
            string fileName = FileNames[group];
            string filePath = ResolveFilePath(fileName, genbaDbPath);

            var list = new List<LayerItem>();
            if (File.Exists(filePath))
            {
                list = ReadLayerFile(filePath, group);
            }

            // 16件に満たない場合はデフォルトで補完
            if (list.Count < 16)
            {
                var defs = CreateDefaultLayers(group);
                while (list.Count < 16)
                {
                    int nextIdx = list.Count + 1;
                    var d = defs.FirstOrDefault(x => x.Index == nextIdx) ?? defs[list.Count];
                    list.Add(d);
                }
            }

            Groups[group] = list;
            return list;
        }

        private string ResolveFilePath(string fileName, string? genbaDbPath)
        {
            if (!string.IsNullOrEmpty(genbaDbPath))
            {
                string? genbaDir = Path.GetDirectoryName(genbaDbPath);
                if (!string.IsNullOrEmpty(genbaDir))
                {
                    string genbaFile = Path.Combine(genbaDir, "Def", fileName);
                    if (File.Exists(genbaFile))
                        return genbaFile;
                }
            }

            string sysFile1 = Path.Combine(DefaultSystemDefDir, fileName);
            if (File.Exists(sysFile1))
                return sysFile1;

            string sysFile2 = Path.Combine(FallbackSystemDefDir, fileName);
            if (File.Exists(sysFile2))
                return sysFile2;

            return sysFile1;
        }

        public List<LayerItem> ReadLayerFile(string filePath, LayerGroup group)
        {
            var list = new List<LayerItem>();
            if (!File.Exists(filePath)) return list;

            try
            {
                try
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                }
                catch { }

                string[] lines;
                try
                {
                    Encoding enc = Encoding.GetEncoding(932);
                    lines = File.ReadAllLines(filePath, enc);
                }
                catch
                {
                    lines = File.ReadAllLines(filePath, Encoding.UTF8);
                }

                int autoIdx = 1;
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var trimmed = line.Trim();
                    if (trimmed.StartsWith("#") || trimmed.StartsWith("//")) continue;

                    string[] parts = trimmed.Contains('\t') ? trimmed.Split('\t') : trimmed.Split(',');
                    if (parts.Length == 0) continue;

                    string codeOrName = parts[0].Trim();
                    string name = parts.Length > 1 ? parts[1].Trim() : codeOrName;

                    int color = 1;
                    if (parts.Length > 2 && int.TryParse(parts[2].Trim(), out int cVal))
                        color = Math.Clamp(cVal, 1, 16);
                    else
                        color = ((autoIdx - 1) % 16) + 1;

                    int width = 1;
                    if (parts.Length > 3 && int.TryParse(parts[3].Trim(), out int wVal))
                        width = Math.Clamp(wVal, 1, 10);

                    int ltype = (group == LayerGroup.Ikou) ? 2 : 1;
                    if (parts.Length > 4 && int.TryParse(parts[4].Trim(), out int ltVal))
                        ltype = Math.Clamp(ltVal, 1, 10);

                    int mark = 1;
                    if (parts.Length > 5 && int.TryParse(parts[5].Trim(), out int mVal))
                        mark = Math.Clamp(mVal, 1, 10);

                    double size = 1.0;
                    if (parts.Length > 6 && double.TryParse(parts[6].Trim(), out double sVal))
                        size = sVal;

                    string prefix = group switch
                    {
                        LayerGroup.Kikai => "K",
                        LayerGroup.Sakuzu => "D",
                        _ => "L"
                    };

                    string code = codeOrName.StartsWith("L", StringComparison.OrdinalIgnoreCase) ||
                                  codeOrName.StartsWith("K", StringComparison.OrdinalIgnoreCase) ||
                                  codeOrName.StartsWith("D", StringComparison.OrdinalIgnoreCase)
                                  ? codeOrName
                                  : $"{prefix}{autoIdx:D2}";

                    list.Add(new LayerItem
                    {
                        Index = autoIdx,
                        Code = code,
                        Name = name,
                        Color = color,
                        Width = width,
                        LType = ltype,
                        Mark = mark,
                        Size = size
                    });

                    autoIdx++;
                    if (autoIdx > 16) break;
                }
            }
            catch { }

            return list;
        }

        public void SaveAll(string targetDefDir)
        {
            foreach (LayerGroup group in Enum.GetValues(typeof(LayerGroup)))
            {
                SaveGroup(targetDefDir, group);
            }
        }

        public void SaveGroup(string targetDefDir, LayerGroup group)
        {
            if (!Directory.Exists(targetDefDir))
            {
                Directory.CreateDirectory(targetDefDir);
            }

            string fileName = FileNames[group];
            string filePath = Path.Combine(targetDefDir, fileName);

            if (!Groups.TryGetValue(group, out var list) || list == null)
            {
                list = CreateDefaultLayers(group);
            }

            var sb = new StringBuilder();
            sb.AppendLine("# レイヤ番号\tレイヤ名\t色番号(1-16)\t線幅(1-5)\t線種(1:折線, 2:曲線)\tマーク(1-8)\tサイズ");

            foreach (var item in list)
            {
                sb.AppendLine($"{item.Code}\t{item.Name}\t{item.Color}\t{item.Width}\t{item.LType}\t{item.Mark}\t{item.Size:F1}");
            }

            try
            {
                Encoding enc = Encoding.GetEncoding(932);
                File.WriteAllText(filePath, sb.ToString(), enc);
            }
            catch
            {
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            }
        }

        public List<LayerItem> GetGroup(LayerGroup group)
        {
            if (!Groups.TryGetValue(group, out var list) || list == null)
            {
                list = CreateDefaultLayers(group);
                Groups[group] = list;
            }
            return list;
        }

        public LayerItem GetLayer(LayerGroup group, int index)
        {
            int normIdx = index;
            if (group == LayerGroup.Kikai && normIdx >= 17 && normIdx <= 32)
            {
                normIdx -= 16;
            }
            else if (group == LayerGroup.Sakuzu && normIdx >= 33 && normIdx <= 48)
            {
                normIdx -= 32;
            }
            else if (group == LayerGroup.Ikou && normIdx >= 49 && normIdx <= 64)
            {
                normIdx -= 48;
            }

            if (normIdx < 1) normIdx = 1;
            if (normIdx > 16) normIdx = ((normIdx - 1) % 16) + 1;

            if (Groups.TryGetValue(group, out var list) && list != null)
            {
                var item = list.FirstOrDefault(x => x.Index == normIdx);
                if (item != null) return item;
                if (list.Count > 0) return list[0];
            }

            string prefix = group switch
            {
                LayerGroup.Kikai => "K",
                LayerGroup.Sakuzu => "D",
                _ => "L"
            };

            int defaultLType = (group == LayerGroup.Ikou) ? 2 : 1;
            return new LayerItem { Index = normIdx, Code = $"{prefix}{normIdx:D2}", Name = $"{prefix}{normIdx:D2}", Color = ((normIdx - 1) % 16) + 1, Mark = 1, Size = 1.0, LType = defaultLType };
        }

        public Color GetColor(LayerGroup group, int index, bool isDarkBackground = true)
        {
            var item = GetLayer(group, index);
            int colorIdx = Math.Clamp(item.Color, 1, 16);

            if (colorIdx == 1)
            {
                return isDarkBackground ? Color.FromArgb(240, 240, 240) : Color.FromArgb(0, 0, 0);
            }
            if (colorIdx == 8)
            {
                return isDarkBackground ? Color.FromArgb(255, 255, 255) : Color.FromArgb(100, 100, 100);
            }

            if (colorIdx < EditorLayerService.LayerTableColors.Length)
            {
                return EditorLayerService.LayerTableColors[colorIdx];
            }

            return Color.White;
        }
    }
}
