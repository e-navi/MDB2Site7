using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Site7DrawingEditor.Services
{
    public class IkouNameColorItem
    {
        public string NamePattern { get; set; } = "";
        public Color Color { get; set; } = Color.FromArgb(255, 30, 115, 210);
    }

    public class IkouNameColorService
    {
        private static readonly Lazy<IkouNameColorService> _instance = new(() => new IkouNameColorService());
        public static IkouNameColorService Instance => _instance.Value;

        public const string FileName = "遺構名色.txt";
        public const string DefaultSystemDefDir = @"C:\SITE7\GENBA\NEW\Def";
        public const string FallbackSystemDefDir = @"C:\SITE7\DEF";

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
                sb.AppendLine("# 遺構名(プレフィックス)\tR,G,B,A");
                foreach (var item in Items)
                {
                    if (string.IsNullOrWhiteSpace(item.NamePattern)) continue;
                    sb.AppendLine($"{item.NamePattern}\t{item.Color.R},{item.Color.G},{item.Color.B},{item.Color.A}");
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

            // デフォルトの基本パターン登録（未定義時のフォールバック）
            if (Items.Count == 0)
            {
                RegisterDefaultPatterns();
            }
        }

        private void RegisterDefaultPatterns()
        {
            Items.Add(new IkouNameColorItem { NamePattern = "SB", Color = Color.FromArgb(255, 220, 38, 38) });    // 竪穴建物 (赤)
            Items.Add(new IkouNameColorItem { NamePattern = "SD", Color = Color.FromArgb(255, 37, 99, 235) });    // 溝 (青)
            Items.Add(new IkouNameColorItem { NamePattern = "SK", Color = Color.FromArgb(255, 22, 163, 74) });    // 土坑 (緑)
            Items.Add(new IkouNameColorItem { NamePattern = "Pit", Color = Color.FromArgb(255, 217, 119, 6) });   // ピット (橙/黄)
            Items.Add(new IkouNameColorItem { NamePattern = "P", Color = Color.FromArgb(255, 217, 119, 6) });     // P (橙/黄)
            Items.Add(new IkouNameColorItem { NamePattern = "SX", Color = Color.FromArgb(255, 147, 51, 234) });   // その他遺構 (紫)
            Items.Add(new IkouNameColorItem { NamePattern = "SI", Color = Color.FromArgb(255, 13, 148, 136) });   // 掘立柱 (青緑)
            Items.Add(new IkouNameColorItem { NamePattern = "ST", Color = Color.FromArgb(255, 180, 83, 9) });     // 包含層/集石 (茶)
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

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var trimmed = line.Trim();
                    if (trimmed.StartsWith("#") || trimmed.StartsWith("//")) continue;

                    string[] parts = trimmed.Contains('\t') ? trimmed.Split('\t') : trimmed.Split(',');
                    if (parts.Length < 2) continue;

                    string pattern = parts[0].Trim();
                    if (string.IsNullOrEmpty(pattern)) continue;

                    string colorStr = string.Join(",", parts.Skip(1)).Trim();
                    Color color = ParseColor(colorStr);

                    Items.Add(new IkouNameColorItem { NamePattern = pattern, Color = color });
                }
            }
            catch { }
        }

        public static Color ParseColor(string colorStr)
        {
            if (string.IsNullOrWhiteSpace(colorStr)) return Color.FromArgb(255, 40, 110, 210);

            var parts = colorStr.Split(new[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 4 &&
                int.TryParse(parts[0], out int r) &&
                int.TryParse(parts[1], out int g) &&
                int.TryParse(parts[2], out int b) &&
                int.TryParse(parts[3], out int a))
            {
                return Color.FromArgb(Math.Clamp(a, 0, 255), Math.Clamp(r, 0, 255), Math.Clamp(g, 0, 255), Math.Clamp(b, 0, 255));
            }
            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int r3) &&
                int.TryParse(parts[1], out int g3) &&
                int.TryParse(parts[2], out int b3))
            {
                return Color.FromArgb(255, Math.Clamp(r3, 0, 255), Math.Clamp(g3, 0, 255), Math.Clamp(b3, 0, 255));
            }

            if (colorStr.StartsWith("#"))
            {
                string hex = colorStr.Substring(1).Trim();
                if (hex.Length == 8 && uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint argb))
                {
                    return Color.FromArgb((int)argb);
                }
                if (hex.Length == 6 && int.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int rgb))
                {
                    return Color.FromArgb(255, (rgb >> 16) & 0xFF, (rgb >> 8) & 0xFF, rgb & 0xFF);
                }
            }

            if (int.TryParse(colorStr, out int paletteIdx))
            {
                return LayerManager.GetLayerColor(paletteIdx);
            }

            return Color.FromArgb(255, 40, 110, 210);
        }

        public Color GetIkouBaseColor(string ikouName)
        {
            if (string.IsNullOrWhiteSpace(ikouName))
                return Color.FromArgb(255, 30, 115, 210);

            string name = ikouName.Trim();

            // 1. 完全一致
            var exact = Items.FirstOrDefault(x => x.NamePattern.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (exact != null) return exact.Color;

            // 2. 最長前方一致 (例: "Pit17" -> "Pit", "SB01" -> "SB")
            var match = Items
                .Where(x => name.StartsWith(x.NamePattern, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.NamePattern.Length)
                .FirstOrDefault();

            if (match != null) return match.Color;

            // 3. デフォルト
            return Color.FromArgb(255, 40, 110, 210);
        }

        /// <summary>
        /// 遺構名と濃淡レベル (1:濃い/上端, 2:中間/中, 3:薄い/下端) から描画色と線幅を計算
        /// </summary>
        public (Color Color, float PenWidth) GetIkouRenderStyle(string ikouName, int toneLevel, float basePenWidth = 1.6f)
        {
            Color baseColor = GetIkouBaseColor(ikouName);

            int alpha = baseColor.A;
            float penWidth = basePenWidth;

            switch (toneLevel)
            {
                case 1: // 濃い (上端)
                    alpha = baseColor.A; // 100%
                    penWidth = Math.Max(1.6f, basePenWidth * 1.15f);
                    break;
                case 2: // 中間 (中)
                    alpha = (int)(baseColor.A * 0.60f); // 60%
                    penWidth = Math.Max(1.2f, basePenWidth * 0.85f);
                    break;
                case 3: // 薄い (下端)
                    alpha = (int)(baseColor.A * 0.35f); // 35%
                    penWidth = Math.Max(1.0f, basePenWidth * 0.65f);
                    break;
                default:
                    alpha = baseColor.A;
                    break;
            }

            Color renderColor = Color.FromArgb(Math.Clamp(alpha, 25, 255), baseColor.R, baseColor.G, baseColor.B);
            return (renderColor, penWidth);
        }
    }
}
