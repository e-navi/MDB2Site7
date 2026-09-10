using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Site7DbEditor.Services
{
    public class IkouNameColorItem
    {
        public string NamePattern { get; set; } = "";
        public int ColorIndex { get; set; } = 4; // 1..16 (デフォルト青)

        public Color Color => GetColor(false);

        public Color GetColor(bool isDarkBackground = false)
        {
            if (ColorIndex == 1)
            {
                return isDarkBackground ? Color.FromArgb(240, 240, 240) : Color.FromArgb(0, 0, 0);
            }
            if (ColorIndex == 8)
            {
                return isDarkBackground ? Color.FromArgb(255, 255, 255) : Color.FromArgb(100, 100, 100);
            }

            return (ColorIndex >= 1 && ColorIndex < EditorLayerService.LayerTableColors.Length)
                ? EditorLayerService.LayerTableColors[ColorIndex]
                : Color.FromArgb(0, 0, 0);
        }
    }

    public class IkouNameColorService
    {
        private static readonly Lazy<IkouNameColorService> _instance = new(() => new IkouNameColorService());
        public static IkouNameColorService Instance => _instance.Value;

        public const string FileName = "遺構名色.txt";
        public const string DefaultSystemDefDir = @"C:\SITE7\GENBA\NEW\Def";
        public const string FallbackSystemDefDir = @"C:\SITE7\DEF";

        public static readonly string[] ColorNames = new[]
        {
            "黒", "赤", "緑", "青", "黄", "マゼンタ", "シアン", "白",
            "牡丹", "茶", "橙", "薄緑", "明青", "青紫", "明灰", "暗灰"
        };

        public List<IkouNameColorItem> Items { get; } = new();

        public void SaveToFile(string filePath)
        {
            try
            {
                string? dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var sb = new StringBuilder();
                sb.AppendLine("# 遺構名(プレフィックス)\t色番号(1-16)");
                foreach (var item in Items)
                {
                    if (string.IsNullOrWhiteSpace(item.NamePattern)) continue;
                    sb.AppendLine($"{item.NamePattern}\t{item.ColorIndex}");
                }

                try
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    File.WriteAllText(filePath, sb.ToString(), Encoding.GetEncoding(932));
                }
                catch
                {
                    File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                }
            }
            catch { }
        }

        public void SaveToGenbaOrSystem(string? genbaDbPath)
        {
            string? genbaDir = !string.IsNullOrEmpty(genbaDbPath) ? Path.GetDirectoryName(genbaDbPath) : null;
            if (!string.IsNullOrEmpty(genbaDir))
            {
                string genbaDefDir = Path.Combine(genbaDir, "Def");
                if (Directory.Exists(genbaDefDir))
                {
                    SaveToFile(Path.Combine(genbaDefDir, FileName));
                    return;
                }
                SaveToFile(Path.Combine(genbaDir, FileName));
                return;
            }

            string sysDef = Directory.Exists(DefaultSystemDefDir) ? DefaultSystemDefDir : FallbackSystemDefDir;
            SaveToFile(Path.Combine(sysDef, FileName));
        }

        public void Load(string? genbaDbPath)
        {
            Items.Clear();
            string? filePath = ResolveFilePath(genbaDbPath);

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                ReadFromFile(filePath);
            }

            // 未定義時のフォールバック
            if (Items.Count == 0)
            {
                RegisterDefaultPatterns();
            }
        }

        private void RegisterDefaultPatterns()
        {
            Items.Add(new IkouNameColorItem { NamePattern = "Pit", ColorIndex = 8 }); // 白/灰
            Items.Add(new IkouNameColorItem { NamePattern = "SA", ColorIndex = 2 });  // 赤
            Items.Add(new IkouNameColorItem { NamePattern = "SB", ColorIndex = 3 });  // 緑
            Items.Add(new IkouNameColorItem { NamePattern = "SD", ColorIndex = 8 });  // 白
            Items.Add(new IkouNameColorItem { NamePattern = "SE", ColorIndex = 2 });  // 赤
            Items.Add(new IkouNameColorItem { NamePattern = "SF", ColorIndex = 3 });  // 緑
            Items.Add(new IkouNameColorItem { NamePattern = "SH", ColorIndex = 7 });  // シアン
            Items.Add(new IkouNameColorItem { NamePattern = "SK", ColorIndex = 8 });  // 白
            Items.Add(new IkouNameColorItem { NamePattern = "SR", ColorIndex = 9 });  // 牡丹
            Items.Add(new IkouNameColorItem { NamePattern = "SX", ColorIndex = 10 }); // 茶
            Items.Add(new IkouNameColorItem { NamePattern = "その他", ColorIndex = 4 }); // 最終行: その他(青)
        }

        private string? ResolveFilePath(string? genbaDbPath)
        {
            if (!string.IsNullOrEmpty(genbaDbPath))
            {
                string? genbaDir = Path.GetDirectoryName(genbaDbPath);
                if (!string.IsNullOrEmpty(genbaDir))
                {
                    string f1 = Path.Combine(genbaDir, "Def", FileName);
                    if (File.Exists(f1)) return f1;
                    string f2 = Path.Combine(genbaDir, FileName);
                    if (File.Exists(f2)) return f2;
                }
            }

            string s1 = Path.Combine(DefaultSystemDefDir, FileName);
            if (File.Exists(s1)) return s1;

            string s2 = Path.Combine(FallbackSystemDefDir, FileName);
            if (File.Exists(s2)) return s2;

            return null;
        }

        private void ReadFromFile(string path)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string[] lines;
                try
                {
                    lines = File.ReadAllLines(path, Encoding.GetEncoding(932));
                }
                catch
                {
                    lines = File.ReadAllLines(path, Encoding.UTF8);
                }

                bool isFirstLine = true;
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var trimmed = line.Trim();

                    // 1行目は説明行としてスキップ (または # // で始まる行)
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        if (trimmed.StartsWith("#") || trimmed.StartsWith("//") || trimmed.Contains("遺構名") || trimmed.Contains("色"))
                            continue;
                    }

                    if (trimmed.StartsWith("#") || trimmed.StartsWith("//")) continue;

                    string[] parts = trimmed.Contains('\t') ? trimmed.Split('\t') : trimmed.Split(',');
                    if (parts.Length < 2) continue;

                    string pattern = parts[0].Trim();
                    if (string.IsNullOrEmpty(pattern)) continue;

                    string colorStr = parts[1].Trim();
                    int colorIdx = 1;

                    if (int.TryParse(colorStr, out int parsedIdx))
                    {
                        colorIdx = Math.Clamp(parsedIdx, 1, 16);
                    }
                    else
                    {
                        int nameIdx = Array.IndexOf(ColorNames, colorStr);
                        if (nameIdx >= 0)
                        {
                            colorIdx = nameIdx + 1;
                        }
                    }

                    Items.Add(new IkouNameColorItem { NamePattern = pattern, ColorIndex = colorIdx });
                }
            }
            catch { }
        }

        /// <summary>
        /// 遺構名に対する基本色 (Color, ColorIndex) を取得。
        /// 一致しない場合は「最終行の色」を使用。
        /// </summary>
        public (Color Color, int ColorIndex) GetIkouBaseColor(string ikouName, bool isDarkBackground = false)
        {
            if (Items.Count == 0)
                RegisterDefaultPatterns();

            var lastItem = Items.LastOrDefault() ?? new IkouNameColorItem { NamePattern = "その他", ColorIndex = 4 };

            if (string.IsNullOrWhiteSpace(ikouName))
                return (lastItem.GetColor(isDarkBackground), lastItem.ColorIndex);

            string name = ikouName.Trim();

            // 1. 完全一致
            var exact = Items.FirstOrDefault(x => x.NamePattern.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (exact != null) return (exact.GetColor(isDarkBackground), exact.ColorIndex);

            // 2. 最長前方一致
            var match = Items
                .Where(x => name.StartsWith(x.NamePattern, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.NamePattern.Length)
                .FirstOrDefault();

            if (match != null) return (match.GetColor(isDarkBackground), match.ColorIndex);

            // 3. 対象とならない遺構名は「最終行の色」を使用
            return (lastItem.GetColor(isDarkBackground), lastItem.ColorIndex);
        }

        /// <summary>
        /// 遺構名と濃淡レベル (1:濃い/上端, 2:中間/中, 3:薄い/下端) から描画色と線幅を計算
        /// </summary>
        public (Color Color, float PenWidth) GetIkouRenderStyle(string ikouName, int toneLevel, float basePenWidth = 1.6f, bool isDarkBackground = false)
        {
            var (baseColor, _) = GetIkouBaseColor(ikouName, isDarkBackground);

            int alpha;
            float penWidth = basePenWidth;

            switch (toneLevel)
            {
                case 1: // 濃い (上端)
                    alpha = 255;
                    penWidth = Math.Max(1.6f, basePenWidth * 1.15f);
                    break;
                case 2: // 中間 (中)
                    alpha = 160;
                    penWidth = Math.Max(1.3f, basePenWidth * 0.85f);
                    break;
                case 3: // 薄い (下端)
                    alpha = 85;
                    penWidth = Math.Max(1.0f, basePenWidth * 0.65f);
                    break;
                default:
                    alpha = 255;
                    break;
            }

            Color renderColor = Color.FromArgb(alpha, baseColor.R, baseColor.G, baseColor.B);
            return (renderColor, penWidth);
        }
    }
}
