using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Site7DbEditor
{
    public static class SqliteManager
    {
        public static bool IsSamePoint(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return Math.Round(x1, 3) == Math.Round(x2, 3) &&
                   Math.Round(y1, 3) == Math.Round(y2, 3) &&
                   Math.Round(z1, 3) == Math.Round(z2, 3);
        }

        public static List<IkouPointRecord> ParsePrecsText(string precsText)
        {
            var list = new List<IkouPointRecord>();
            if (string.IsNullOrWhiteSpace(precsText)) return list;

            string[] lines = precsText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int currentPid = 1;
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

                // 直前の点と同一座標（連続重複）の場合はスキップ
                if (list.Count > 0)
                {
                    var prev = list[list.Count - 1];
                    if (IsSamePoint(x, y, z, prev.X, prev.Y, prev.Z))
                    {
                        continue;
                    }
                }

                list.Add(new IkouPointRecord
                {
                    Pid = currentPid++,
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
            if (points == null || points.Count == 0) return "";

            int pid = 1;
            IkouPointRecord? prevPt = null;
            foreach (var pt in points)
            {
                if (prevPt != null && IsSamePoint(pt.X, pt.Y, pt.Z, prevPt.X, prevPt.Y, prevPt.Z))
                {
                    // 連続する重複点はスキップ
                    continue;
                }

                string line = $"{pid}\t{pt.X:0.000}\t{pt.Y:0.000}\t{pt.Z:0.000}\t{pt.Date}\t{pt.S:0.000}\t{pt.V:0.000}\t{pt.H:0.000}\t{pt.KPName}\t{pt.BPName}\t{pt.KPH:0.000}\t{pt.MRH:0.000}";
                lines.Add(line);
                pid++;
                prevPt = pt;
            }
            return string.Join("\n", lines);
        }
    }
}
