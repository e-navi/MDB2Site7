using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Site7DbEditor.Services
{
    public struct Point3D
    {
        public double X;
        public double Y;
        public double Z;
        public byte R;
        public byte G;
        public byte B;
        public bool HasColor;

        public Point3D(double x, double y, double z, byte r = 0, byte g = 0, byte b = 0, bool hasColor = false)
        {
            X = x;
            Y = y;
            Z = z;
            R = r;
            G = g;
            B = b;
            HasColor = hasColor;
        }
    }

    public class PointCloudService
    {
        public static PointCloudService Instance { get; } = new PointCloudService();

        public string CurrentFilePath { get; private set; } = "";
        public bool SwapXY { get; set; } = false;
        public List<Point3D> Points { get; } = new List<Point3D>();

        public double MinX { get; private set; }
        public double MaxX { get; private set; }
        public double MinY { get; private set; }
        public double MaxY { get; private set; }
        public double MinZ { get; private set; }
        public double MaxZ { get; private set; }

        public bool HasPoints => Points.Count > 0;

        // 空間グリッドインデックス (セルサイズ 0.1m)
        private double _cellSize = 0.1;
        private readonly Dictionary<long, List<int>> _grid = new Dictionary<long, List<int>>();

        private PointCloudService() { }

        public void Clear()
        {
            CurrentFilePath = "";
            Points.Clear();
            _grid.Clear();
            MinX = MaxX = MinY = MaxY = MinZ = MaxZ = 0;
        }

        public void ToggleSwapXY()
        {
            if (string.IsNullOrEmpty(CurrentFilePath) || !File.Exists(CurrentFilePath))
            {
                if (Points.Count > 0)
                {
                    SwapXY = !SwapXY;
                    for (int i = 0; i < Points.Count; i++)
                    {
                        var pt = Points[i];
                        Points[i] = new Point3D(pt.Y, pt.X, pt.Z, pt.R, pt.G, pt.B, pt.HasColor);
                    }
                    double tmpMin = MinX; MinX = MinY; MinY = tmpMin;
                    double tmpMax = MaxX; MaxX = MaxY; MaxY = tmpMax;
                    BuildSpatialIndex();
                }
                return;
            }
            SwapXY = !SwapXY;
            LoadFile(CurrentFilePath, SwapXY);
        }

        public bool AutoDetectAndSwapXY(double siteX, double siteY)
        {
            if (Points.Count == 0) return false;
            double midX = (MinX + MaxX) / 2.0;
            double midY = (MinY + MaxY) / 2.0;

            double distNormal = (midX - siteX) * (midX - siteX) + (midY - siteY) * (midY - siteY);
            double distSwapped = (midY - siteX) * (midY - siteX) + (midX - siteY) * (midX - siteY);

            // 反転した方が現場基準点に明らかに近ければ自動反転
            if (distSwapped < distNormal * 0.5)
            {
                ToggleSwapXY();
                return true;
            }
            return false;
        }

        public bool LoadFile(string path, bool swapXY = false, int maxPoints = 5000000)
        {
            if (!File.Exists(path)) return false;
            SwapXY = swapXY;

            string ext = Path.GetExtension(path).ToLowerInvariant();
            bool success = false;

            if (ext == ".las")
            {
                success = LoadLasFile(path, swapXY, maxPoints);
            }
            else
            {
                success = LoadTextFile(path, swapXY, maxPoints);
            }

            if (success && Points.Count > 0)
            {
                CurrentFilePath = path;
                BuildSpatialIndex();
                return true;
            }
            return false;
        }

        private bool LoadTextFile(string path, bool swapXY, int maxPoints)
        {
            Clear();
            SwapXY = swapXY;
            try
            {
                using (var sr = new StreamReader(path, Encoding.UTF8))
                {
                    string? line;
                    double minX = double.MaxValue, maxX = double.MinValue;
                    double minY = double.MaxValue, maxY = double.MinValue;
                    double minZ = double.MaxValue, maxZ = double.MinValue;

                    char[] seps = new char[] { ' ', '\t', ',', ';' };

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        if (line.StartsWith("#") || line.StartsWith("//")) continue;

                        var parts = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 3) continue;

                        if (double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double px) &&
                            double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double py) &&
                            double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double pz))
                        {
                            byte r = 0, g = 0, b = 0;
                            bool hasColor = false;

                            // RGBのパース (X Y Z R G B または X Y Z I R G B)
                            if (parts.Length >= 6)
                            {
                                int rIdx = 3, gIdx = 4, bIdx = 5;
                                if (parts.Length >= 7 && !double.TryParse(parts[3], out double testVal) || (parts.Length >= 7 && double.TryParse(parts[6], out _)))
                                {
                                    // 4列目がIntensity等の場合
                                    if (double.TryParse(parts[3], out _) && double.TryParse(parts[4], out _) && double.TryParse(parts[5], out _) && double.TryParse(parts[6], out _))
                                    {
                                        rIdx = 4; gIdx = 5; bIdx = 6;
                                    }
                                }

                                if (double.TryParse(parts[rIdx], NumberStyles.Float, CultureInfo.InvariantCulture, out double cr) &&
                                    double.TryParse(parts[gIdx], NumberStyles.Float, CultureInfo.InvariantCulture, out double cg) &&
                                    double.TryParse(parts[bIdx], NumberStyles.Float, CultureInfo.InvariantCulture, out double cb))
                                {
                                    r = (byte)Math.Clamp(cr > 255 ? (cr / 256.0) : cr, 0, 255);
                                    g = (byte)Math.Clamp(cg > 255 ? (cg / 256.0) : cg, 0, 255);
                                    b = (byte)Math.Clamp(cb > 255 ? (cb / 256.0) : cb, 0, 255);
                                    hasColor = true;
                                }
                            }

                            double finalX = swapXY ? py : px;
                            double finalY = swapXY ? px : py;
                            double finalZ = pz;

                            Points.Add(new Point3D(finalX, finalY, finalZ, r, g, b, hasColor));

                            if (finalX < minX) minX = finalX;
                            if (finalX > maxX) maxX = finalX;
                            if (finalY < minY) minY = finalY;
                            if (finalY > maxY) maxY = finalY;
                            if (finalZ < minZ) minZ = finalZ;
                            if (finalZ > maxZ) maxZ = finalZ;

                            if (Points.Count >= maxPoints) break;
                        }
                    }

                    if (Points.Count > 0)
                    {
                        MinX = minX; MaxX = maxX;
                        MinY = minY; MaxY = maxY;
                        MinZ = minZ; MaxZ = maxZ;
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        private bool LoadLasFile(string path, bool swapXY, int maxPoints)
        {
            Clear();
            SwapXY = swapXY;
            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var br = new BinaryReader(fs))
                {
                    // ASPRS LAS Header (minimum 227 bytes)
                    byte[] sig = br.ReadBytes(4);
                    string signature = Encoding.ASCII.GetString(sig);
                    if (signature != "LASF") return false;

                    fs.Seek(94, SeekOrigin.Begin);
                    ushort headerSize = br.ReadUInt16();
                    uint offsetToPoints = br.ReadUInt32();
                    uint numVarLenRecords = br.ReadUInt32();
                    byte pointFormat = br.ReadByte();
                    ushort pointRecordLen = br.ReadUInt16();
                    uint legacyNumPoints = br.ReadUInt32();

                    fs.Seek(131, SeekOrigin.Begin);
                    double xScale = br.ReadDouble();
                    double yScale = br.ReadDouble();
                    double zScale = br.ReadDouble();
                    double xOffset = br.ReadDouble();
                    double yOffset = br.ReadDouble();
                    double zOffset = br.ReadDouble();

                    double minX = double.MaxValue, maxX = double.MinValue;
                    double minY = double.MaxValue, maxY = double.MinValue;
                    double minZ = double.MaxValue, maxZ = double.MinValue;

                    ulong totalPoints = legacyNumPoints;

                    // LAS 1.4 64bit point count support
                    if (headerSize >= 375 && legacyNumPoints == 0)
                    {
                        fs.Seek(247, SeekOrigin.Begin);
                        totalPoints = br.ReadUInt64();
                    }

                    // RGB オフセットの決定 (X, Y, Z 12 bytes からの相対スキップ)
                    bool hasLasColor = false;
                    int bytesBeforeColor = 0;
                    if (pointFormat == 2)
                    {
                        hasLasColor = true;
                        bytesBeforeColor = 8; // 20 - 12
                    }
                    else if (pointFormat == 3)
                    {
                        hasLasColor = true;
                        bytesBeforeColor = 16; // 28 - 12
                    }
                    else if (pointFormat == 7 || pointFormat == 8)
                    {
                        hasLasColor = true;
                        bytesBeforeColor = 18; // 30 - 12
                    }

                    fs.Seek(offsetToPoints, SeekOrigin.Begin);
                    long pointsToRead = (long)Math.Min((ulong)maxPoints, totalPoints);

                    for (long i = 0; i < pointsToRead; i++)
                    {
                        int rawX = br.ReadInt32();
                        int rawY = br.ReadInt32();
                        int rawZ = br.ReadInt32();

                        double px = rawX * xScale + xOffset;
                        double py = rawY * yScale + yOffset;
                        double pz = rawZ * zScale + zOffset;

                        double finalX = swapXY ? py : px;
                        double finalY = swapXY ? px : py;
                        double finalZ = pz;

                        byte r = 0, g = 0, b = 0;
                        if (hasLasColor)
                        {
                            if (bytesBeforeColor > 0) fs.Seek(bytesBeforeColor, SeekOrigin.Current);
                            ushort rawR = br.ReadUInt16();
                            ushort rawG = br.ReadUInt16();
                            ushort rawB = br.ReadUInt16();
                            r = (byte)(rawR > 255 ? (rawR >> 8) : rawR);
                            g = (byte)(rawG > 255 ? (rawG >> 8) : rawG);
                            b = (byte)(rawB > 255 ? (rawB >> 8) : rawB);

                            int remainingBytes = pointRecordLen - 12 - bytesBeforeColor - 6;
                            if (remainingBytes > 0) fs.Seek(remainingBytes, SeekOrigin.Current);
                        }
                        else
                        {
                            int remainingBytes = pointRecordLen - 12;
                            if (remainingBytes > 0) fs.Seek(remainingBytes, SeekOrigin.Current);
                        }

                        Points.Add(new Point3D(finalX, finalY, finalZ, r, g, b, hasLasColor));

                        if (finalX < minX) minX = finalX;
                        if (finalX > maxX) maxX = finalX;
                        if (finalY < minY) minY = finalY;
                        if (finalY > maxY) maxY = finalY;
                        if (finalZ < minZ) minZ = finalZ;
                        if (finalZ > maxZ) maxZ = finalZ;
                    }

                    if (Points.Count > 0)
                    {
                        MinX = minX; MaxX = maxX;
                        MinY = minY; MaxY = maxY;
                        MinZ = minZ; MaxZ = maxZ;
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        private void BuildSpatialIndex()
        {
            _grid.Clear();
            if (Points.Count == 0) return;

            // 0.1mメッシュの微細な地形変化を捉えるためセルサイズを0.1m(10cm)に設定
            _cellSize = 0.1;

            for (int i = 0; i < Points.Count; i++)
            {
                long key = GetGridKey(Points[i].X, Points[i].Y);
                if (!_grid.TryGetValue(key, out var list))
                {
                    list = new List<int>();
                    _grid[key] = list;
                }
                list.Add(i);
            }
        }

        private long GetGridKey(double x, double y)
        {
            long gx = (long)Math.Floor(x / _cellSize);
            long gy = (long)Math.Floor(y / _cellSize);
            return (gx << 32) ^ (gy & 0xFFFFFFFFL);
        }

        /// <summary>
        /// 3Dメッシュ生成用の超高速・精密最近傍標高取得 (指定許容距離内の点群のみ採用)
        /// </summary>
        public double? GetFastZ(double surveyX, double surveyY, double maxDist = 0.5)
        {
            if (!HasPoints) return null;
            if (surveyX < MinX - maxDist || surveyX > MaxX + maxDist || surveyY < MinY - maxDist || surveyY > MaxY + maxDist) return null;

            long centerGx = (long)Math.Floor(surveyX / _cellSize);
            long centerGy = (long)Math.Floor(surveyY / _cellSize);

            double maxDistSq = maxDist * maxDist;
            int cellRadius = Math.Clamp((int)Math.Ceiling(maxDist / _cellSize), 1, 3);

            double closestDistSq = double.MaxValue;
            double closestZ = 0;
            bool found = false;

            for (long dx = -cellRadius; dx <= cellRadius; dx++)
            {
                for (long dy = -cellRadius; dy <= cellRadius; dy++)
                {
                    long key = ((centerGx + dx) << 32) ^ ((centerGy + dy) & 0xFFFFFFFFL);
                    if (_grid.TryGetValue(key, out var list))
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            var pt = Points[list[i]];
                            double dSq = (pt.X - surveyX) * (pt.X - surveyX) + (pt.Y - surveyY) * (pt.Y - surveyY);
                            if (dSq < closestDistSq)
                            {
                                closestDistSq = dSq;
                                closestZ = pt.Z;
                                found = true;
                            }
                        }
                    }
                }
            }

            if (found && closestDistSq <= maxDistSq)
            {
                return closestZ;
            }

            return null;
        }

        /// <summary>
        /// 指定測量座標 (X, Y) における最近傍点・逆距離加重補間によるZ標高値を高速取得
        /// </summary>
        public double? GetInterpolatedZ(double surveyX, double surveyY, double maxSearchRadius = 15.0)
        {
            if (!HasPoints) return null;

            var z = QueryZInternal(surveyX, surveyY, maxSearchRadius);
            if (z.HasValue) return z;

            // XとYが逆順の点群データ（Easting/Northing）に対するフォールバック
            if (!SwapXY)
            {
                return QueryZInternal(surveyY, surveyX, maxSearchRadius);
            }
            return null;
        }

        private double? QueryZInternal(double qX, double qY, double maxSearchRadius)
        {
            // 範囲外の即時スキップ (0ミリ秒でリターン)
            if (qX < MinX - maxSearchRadius || qX > MaxX + maxSearchRadius ||
                qY < MinY - maxSearchRadius || qY > MaxY + maxSearchRadius)
            {
                return null;
            }

            long centerGx = (long)Math.Floor(qX / _cellSize);
            long centerGy = (long)Math.Floor(qY / _cellSize);
            int searchCells = Math.Clamp((int)Math.Ceiling(maxSearchRadius / _cellSize), 1, 3);

            double weightedSum = 0;
            double weightSum = 0;
            double closestDistSq = double.MaxValue;
            double closestZ = 0;

            double maxRadiusSq = maxSearchRadius * maxSearchRadius;

            for (long dx = -searchCells; dx <= searchCells; dx++)
            {
                for (long dy = -searchCells; dy <= searchCells; dy++)
                {
                    long key = ((centerGx + dx) << 32) ^ ((centerGy + dy) & 0xFFFFFFFFL);
                    if (_grid.TryGetValue(key, out var list))
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            var pt = Points[list[i]];
                            double distSq = (pt.X - qX) * (pt.X - qX) + (pt.Y - qY) * (pt.Y - qY);
                            if (distSq <= maxRadiusSq)
                            {
                                if (distSq < closestDistSq)
                                {
                                    closestDistSq = distSq;
                                    closestZ = pt.Z;
                                }

                                if (distSq < 1e-4) // ほぼ同一地点
                                {
                                    return pt.Z;
                                }

                                double w = 1.0 / distSq;
                                weightedSum += pt.Z * w;
                                weightSum += w;
                            }
                        }
                    }
                }
            }

            if (weightSum > 0)
            {
                return weightedSum / weightSum;
            }

            if (closestDistSq <= maxRadiusSq)
            {
                return closestZ;
            }

            return null;
        }
    }
}
