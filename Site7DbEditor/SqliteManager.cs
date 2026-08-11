using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Site7DbEditor
{
    public static class SqliteManager
    {
        public static List<IkouPointRecord> ParsePrecsText(string precsText)
        {
            var list = new List<IkouPointRecord>();
            if (string.IsNullOrWhiteSpace(precsText)) return list;

            string[] lines = precsText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('\t');
                if (parts.Length < 4) continue;

                int.TryParse(parts[0].Trim(), out int pid);
                double.TryParse(parts[1].Trim(), out double x);
                double.TryParse(parts[2].Trim(), out double y);
                double.TryParse(parts[3].Trim(), out double z);
                string date = parts.Length > 4 ? parts[4].Trim() : "";
                double.TryParse(parts.Length > 5 ? parts[5].Trim() : "", out double s);
                double.TryParse(parts.Length > 6 ? parts[6].Trim() : "", out double v);
                double.TryParse(parts.Length > 7 ? parts[7].Trim() : "", out double h);
                string kp = parts.Length > 8 ? parts[8].Trim() : "";
                string bp = parts.Length > 9 ? parts[9].Trim() : "";
                double.TryParse(parts.Length > 10 ? parts[10].Trim() : "", out double kph);
                double.TryParse(parts.Length > 11 ? parts[11].Trim() : "", out double mrh);

                list.Add(new IkouPointRecord
                {
                    Pid = pid,
                    X = x,
                    Y = y,
                    Z = z,
                    Date = date,
                    S = s,
                    V = v,
                    H = h,
                    KPName = kp,
                    BPName = bp,
                    KPH = kph,
                    MRH = mrh
                });
            }
            return list;
        }

        public static string FormatPrecsText(List<IkouPointRecord> points)
        {
            var lines = new List<string>();
            int pid = 1;
            foreach (var pt in points)
            {
                string line = $"{pid}\t{pt.X:0.000}\t{pt.Y:0.000}\t{pt.Z:0.000}\t{pt.Date}\t{pt.S:0.000}\t{pt.V:0.000}\t{pt.H:0.000}\t{pt.KPName}\t{pt.BPName}\t{pt.KPH:0.000}\t{pt.MRH:0.000}";
                lines.Add(line);
                pid++;
            }
            return string.Join("\n", lines);
        }
    }
}
