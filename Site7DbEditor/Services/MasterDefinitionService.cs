using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Site7DbEditor.Services
{
    public class MasterItem
    {
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";

        public string DisplayText
        {
            get
            {
                if (string.IsNullOrEmpty(Description) || Description == Code)
                    return Code;
                return $"{Code} : {Description}";
            }
        }

        public override string ToString() => DisplayText;
    }

    public enum MasterType
    {
        Ikou,        // 遺構.txt
        IkouLine,    // 遺構線.txt
        IbutuSyubetu,// 遺物_種別.txt
        IbutuSoui,   // 遺物_層位.txt
        IbutuChiku   // 遺物_地区.txt
    }

    public class MasterDefinitionService
    {
        private static MasterDefinitionService? _instance;
        public static MasterDefinitionService Instance => _instance ??= new MasterDefinitionService();

        public const string DefaultSystemDefDir = @"C:\SITE7\GENBA\NEW\Def";
        public const string FallbackSystemDefDir = @"C:\SITE7\DEF";

        public Dictionary<MasterType, List<MasterItem>> Masters { get; } = new();

        public static readonly Dictionary<MasterType, string> FileNames = new()
        {
            { MasterType.Ikou, "遺構.txt" },
            { MasterType.IkouLine, "遺構線.txt" },
            { MasterType.IbutuSyubetu, "遺物_種別.txt" },
            { MasterType.IbutuSoui, "遺物_層位.txt" },
            { MasterType.IbutuChiku, "遺物_地区.txt" }
        };

        public static readonly Dictionary<MasterType, string> DisplayNames = new()
        {
            { MasterType.Ikou, "遺構" },
            { MasterType.IkouLine, "遺構線" },
            { MasterType.IbutuSyubetu, "遺物種別" },
            { MasterType.IbutuSoui, "遺物層位" },
            { MasterType.IbutuChiku, "遺物地区" }
        };

        static MasterDefinitionService()
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            }
            catch { }
        }

        public MasterDefinitionService()
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            }
            catch { }
            foreach (MasterType type in Enum.GetValues(typeof(MasterType)))
            {
                Masters[type] = new List<MasterItem>();
            }
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

        public string GetGenbaDefDirectory(string genbaDbPath)
        {
            string? genbaDir = Path.GetDirectoryName(genbaDbPath);
            if (string.IsNullOrEmpty(genbaDir)) return "";
            return Path.Combine(genbaDir, "Def");
        }

        public void LoadAll(string? genbaDbPath)
        {
            foreach (MasterType type in Enum.GetValues(typeof(MasterType)))
            {
                LoadMaster(type, genbaDbPath);
            }
        }

        public List<MasterItem> LoadMaster(MasterType type, string? genbaDbPath)
        {
            string fileName = FileNames[type];
            string filePath = ResolveFilePath(fileName, genbaDbPath);

            var list = new List<MasterItem>();
            if (File.Exists(filePath))
            {
                list = ReadMasterFile(filePath);
            }
            else
            {
                // Fallback default list if no file exists
                list = GetDefaultMasterItems(type);
            }

            Masters[type] = list;
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

        public List<MasterItem> ReadMasterFile(string filePath)
        {
            var list = new List<MasterItem>();
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
                    Encoding enc = Encoding.GetEncoding(932); // Shift-JIS
                    lines = File.ReadAllLines(filePath, enc);
                }
                catch
                {
                    lines = File.ReadAllLines(filePath, Encoding.UTF8);
                }

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var trimmed = line.Trim();
                    if (trimmed.StartsWith("#") || trimmed.StartsWith("//")) continue;

                    string[] parts;
                    if (trimmed.Contains('\t'))
                    {
                        parts = trimmed.Split('\t');
                    }
                    else if (trimmed.Contains(','))
                    {
                        parts = trimmed.Split(',');
                    }
                    else
                    {
                        // 1項目形式
                        parts = new[] { trimmed, trimmed };
                    }

                    string code = parts[0].Trim();
                    string desc = parts.Length > 1 ? parts[1].Trim() : code;

                    if (!string.IsNullOrEmpty(code))
                    {
                        list.Add(new MasterItem { Code = code, Description = desc });
                    }
                }
            }
            catch { }

            return list;
        }

        public void SaveMasterFile(string targetDir, MasterType type, List<MasterItem> items)
        {
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            string fileName = FileNames[type];
            string filePath = Path.Combine(targetDir, fileName);

            var sb = new StringBuilder();
            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item.Code)) continue;
                string desc = string.IsNullOrWhiteSpace(item.Description) ? item.Code : item.Description;
                sb.AppendLine($"{item.Code}\t{desc}");
            }

            Encoding enc = Encoding.GetEncoding(932);
            File.WriteAllText(filePath, sb.ToString(), enc);
            Masters[type] = new List<MasterItem>(items);
        }

        public static List<MasterItem> GetDefaultMasterItems(MasterType type)
        {
            return type switch
            {
                MasterType.Ikou => new List<MasterItem>
                {
                    new() { Code = "Pit", Description = "ピット(柱穴・小穴)" },
                    new() { Code = "SA", Description = "塀・柵跡" },
                    new() { Code = "SB", Description = "掘立柱建物跡" },
                    new() { Code = "SD", Description = "溝跡" },
                    new() { Code = "SE", Description = "井戸跡" },
                    new() { Code = "SF", Description = "焼成土坑跡など" },
                    new() { Code = "SH", Description = "竪穴建物跡" },
                    new() { Code = "SK", Description = "土坑跡" },
                    new() { Code = "SR", Description = "旧流路" },
                    new() { Code = "SX", Description = "古墳・墳墓など" },
                    new() { Code = "SZ", Description = "性格不明遺構" }
                },
                MasterType.IkouLine => new List<MasterItem>
                {
                    new() { Code = "上", Description = "上端(法肩)" },
                    new() { Code = "中", Description = "中段" },
                    new() { Code = "下", Description = "下端(法尻)" },
                    new() { Code = "上端", Description = "上端" },
                    new() { Code = "中端", Description = "中端" },
                    new() { Code = "下端", Description = "下端" },
                    new() { Code = "石", Description = "石列" }
                },
                MasterType.IbutuSyubetu => new List<MasterItem>
                {
                    new() { Code = "土師器", Description = "土師器(煮炊具・食器)" },
                    new() { Code = "須恵器", Description = "須恵器(貯蔵具・硬質)" },
                    new() { Code = "古墳土師器", Description = "古墳土師器" },
                    new() { Code = "瓦", Description = "古代・中世瓦" },
                    new() { Code = "羽口", Description = "羽口(製鉄関連)" },
                    new() { Code = "鉄製品", Description = "鉄製品(刀子・農具等)" },
                    new() { Code = "銅製品", Description = "銅製品(銅鏡・装身具等)" },
                    new() { Code = "古銭", Description = "古銭(皇朝十二銭等)" },
                    new() { Code = "石器", Description = "石器(打製・磨製)" },
                    new() { Code = "木製品", Description = "木製品" },
                    new() { Code = "陶磁器", Description = "陶磁器(近世・近代)" },
                    new() { Code = "不明", Description = "性格不明遺物" }
                },
                MasterType.IbutuSoui => new List<MasterItem>
                {
                    new() { Code = "黒色土層面", Description = "黒色土層面" },
                    new() { Code = "黄色土層面", Description = "黄色土層面" },
                    new() { Code = "黄色土層2面", Description = "黄色土層2面" },
                    new() { Code = "黄色土層中", Description = "黄色土層中" },
                    new() { Code = "SK2検出面", Description = "SK2検出面" },
                    new() { Code = "西壁サブトレ", Description = "西壁サブトレ" },
                    new() { Code = "SD01底", Description = "SD01底" }
                },
                MasterType.IbutuChiku => new List<MasterItem>
                {
                    new() { Code = "T74", Description = "74区トレンチ" },
                    new() { Code = "T75", Description = "75区トレンチ" },
                    new() { Code = "T76", Description = "76区トレンチ" },
                    new() { Code = "T77", Description = "77区トレンチ" },
                    new() { Code = "3Dマーカー", Description = "3Dマーカー地区" },
                    new() { Code = "P", Description = "ピット地区" }
                },
                _ => new List<MasterItem>()
            };
        }

        public void BindToComboBox(ComboBox cmb, MasterType type, string? currentCode = null)
        {
            var list = Masters.TryGetValue(type, out var items) ? items : GetDefaultMasterItems(type);

            cmb.BeginUpdate();
            cmb.Items.Clear();
            foreach (var item in list)
            {
                cmb.Items.Add(item);
            }
            cmb.EndUpdate();

            if (!string.IsNullOrEmpty(currentCode))
            {
                SelectCode(cmb, currentCode);
            }
        }

        public static void SelectCode(ComboBox cmb, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                cmb.SelectedIndex = -1;
                return;
            }

            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (cmb.Items[i] is MasterItem mi && string.Equals(mi.Code, code, StringComparison.OrdinalIgnoreCase))
                {
                    cmb.SelectedIndex = i;
                    return;
                }
                else if (string.Equals(cmb.Items[i].ToString(), code, StringComparison.OrdinalIgnoreCase))
                {
                    cmb.SelectedIndex = i;
                    return;
                }
            }

            cmb.Text = code;
        }

        public static string GetSelectedCode(ComboBox cmb)
        {
            if (cmb.SelectedItem is MasterItem mi)
            {
                return mi.Code;
            }
            string text = cmb.Text?.Trim() ?? "";
            if (text.Contains(" : "))
            {
                return text.Split(new[] { " : " }, StringSplitOptions.None)[0].Trim();
            }
            return text;
        }
    }
}
