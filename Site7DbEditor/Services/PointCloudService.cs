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

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class PointCloudService
    {
        public static PointCloudService Instance { get; } = new PointCloudService();

        public string CurrentFilePath { get; private set; } = "";
        public List<Point3D> Points { get; } = new List<Point3D>();

        public double MinX { get; private set; }
        public double MaxX { get; private set; }
        public double MinY { get; private set; }
        public double MaxY { get; private set; }
        public double MinZ { get; private set; }
        public double MaxZ { get; private set; }

        public bool HasPoints => Points.Count > 0;

        // 空間グリッドインデックス (セルサイズ 1.0m)
        private double _cellSize = 1.0;
        private readonly Dictionary<long, List<int>> _grid = new Dictionary<long, List<int>>();

        private PointCloudService() { }

        public void Clear()
        {
            CurrentFilePath = "";
            Points.Clear();
            _grid.Clear();
            MinX = MaxX = MinY = MaxY = MinZ = MaxZ = 0;
        }

        public bool LoadFile(string path, int maxPoints = 5000000)
        {
            if (!File.Exists(path)) return false;

            string ext = Path.GetExtension(path).ToLowerInvariant();
            bool success = false;

            if (ext == ".las")
            {
                success = LoadLasFile(path, maxPoints);
            }
            else
            {
                success = LoadTextFile(path, maxPoints);
            }

            if (success && Points.Count > 0)
            {
                CurrentFilePath = path;
                BuildSpatialIndex();
                return true;
            }
            return false;
        }

        private bool LoadTextFile(string path, int maxPoints)
        {
            Clear();
            try
            {
                using (var sr = new StreamReader(path, Encoding.UTF8))
                {
                    string? line;
                    char[] sep = new char[] { ' ', '\t', ',', ';' };

                    double minX = double.MaxValue, maxX = double.MinValue;
                    double minY = double.MaxValue, maxY = double.MinValue;
                    double minZ = double.MaxValue, maxZ = double.MinValue;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        if (line.StartsWith("#") || line.StartsWith("//")) continue;

                        var parts = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 3) continue;

                        // 3連続の数値を探す (点名, X, Y, Z や X Y Z に対応)
                        int startIdx = -1;
                        double x = 0, y = 0, z = 0;
                        for (int i = 0; i <= parts.Length - 3; i++)
                        {
                            if (double.TryParse(parts[i], NumberStyles.Float, CultureInfo.InvariantCulture, out double px) &&
                                double.TryParse(parts[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out double py) &&
                                double.TryParse(parts[i + 2], NumberStyles.Float, CultureInfo.InvariantCulture, out double pz))
                            {
                                x = px;
                                y = py;
                                z = pz;
                                startIdx = i;
                                break;
                            }
                        }

                        if (startIdx >= 0)
                        {
                            Points.Add(new Point3D(x, y, z));

                            if (x < minX) minX = x;
                            if (x > maxX) maxX = x;
                            if (y < minY) minY = y;
                            if (y > maxY) maxY = y;
                            if (z < minZ) minZ = z;
                            if (z > maxZ) maxZ = z;

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

        private bool LoadLasFile(string path, int maxPoints)
        {
            Clear();
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

                    fs.Seek(offsetToPoints, SeekOrigin.Begin);
                    long pointsToRead = (long)Math.Min((ulong)maxPoints, totalPoints);

                    for (long i = 0; i < pointsToRead; i++)
                    {
                        int rawX = br.ReadInt32();
                        int rawY = br.ReadInt32();
                        int rawZ = br.ReadInt32();

                        double x = rawX * xScale + xOffset;
                        double y = rawY * yScale + yOffset;
                        double z = rawZ * zScale + zOffset;

                        Points.Add(new Point3D(x, y, z));

                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                        if (z < minZ) minZ = z;
                        if (z > maxZ) maxZ = z;

                        // Skip remaining bytes in point record
                        int remainingBytes = pointRecordLen - 12;
                        if (remainingBytes > 0)
                        {
                            fs.Seek(remainingBytes, SeekOrigin.Current);
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

        private void BuildSpatialIndex()
        {
            _grid.Clear();
            if (Points.Count == 0) return;

            // 点群の広がりから適切なセルサイズを自動計算 (0.5m 〜 2.0m)
            double range = Math.Max(MaxX - MinX, MaxY - MinY);
            _cellSize = Math.Clamp(range / 100.0, 0.5, 5.0);

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
        /// 指定測量座標 (X, Y) における最近傍点・逆距離加重補間によるZ標高値を高速取得
        /// </summary>
        public double? GetInterpolatedZ(double surveyX, double surveyY, double maxSearchRadius = 3.0)
        {
            if (!HasPoints) return null;

            long centerGx = (long)Math.Floor(surveyX / _cellSize);
            long centerGy = (long)Math.Floor(surveyY / _cellSize);
            int searchCells = (int)Math.Ceiling(maxSearchRadius / _cellSize);

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
                            double distSq = (pt.X - surveyX) * (pt.X - surveyX) + (pt.Y - surveyY) * (pt.Y - surveyY);
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

            if (closestDistSq < maxRadiusSq)
            {
                return closestZ;
            }

            return null;
        }
    }
}
