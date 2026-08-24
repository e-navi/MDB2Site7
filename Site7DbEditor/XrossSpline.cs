using System;
using System.Collections.Generic;
using System.Linq;

namespace Site7DbEditor
{
    public class XYZ
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public XYZ() : this(0, 0, 0) { }

        public XYZ(double x, double y, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public XYZ(XYZ other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }

        public void set(XYZ p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public bool equal(XYZ p)
        {
            return Math.Abs(X - p.X) < 1e-4 && Math.Abs(Y - p.Y) < 1e-4 && Math.Abs(Z - p.Z) < 1e-4;
        }

        public double CalcLen(XYZ p)
        {
            double dx = p.X - X;
            double dy = p.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double CalcAng(XYZ p)
        {
            double dx = p.X - X;
            double dy = p.Y - Y;
            double ang = Math.Atan2(dy, dx);
            if (ang < 0) ang += 2 * Math.PI;
            return ang;
        }

        public void min(XYZ p)
        {
            if (p.X < X) X = p.X;
            if (p.Y < Y) Y = p.Y;
            if (p.Z < Z) Z = p.Z;
        }

        public void max(XYZ p)
        {
            if (p.X > X) X = p.X;
            if (p.Y > Y) Y = p.Y;
            if (p.Z > Z) Z = p.Z;
        }
    }

    /// <summary>
    /// 3次元スプライン曲線を計算するクラス
    /// </summary>
    public class Xross_Spline
    {
        private double[] _parameterP = Array.Empty<double>();

        private void CurveMakeTable(double[] x, double[] y, ref double[] z)
        {
            int n = x.Length;
            if (n < 2) return;

            double[] h = new double[n];
            double[] d = new double[n];
            double t;

            z[0] = 0;
            z[n - 1] = 0;

            for (int i = 0; i < n - 1; i++)
            {
                double diff = x[i + 1] - x[i];
                if (Math.Abs(diff) < 1E-8)
                {
                    diff = 1E-8;
                    x[i + 1] = x[i] + diff;
                }
                h[i] = diff;
                d[i + 1] = (y[i + 1] - y[i]) / h[i];
            }

            if (n == 2) return;

            z[1] = d[2] - d[1] - h[0] * z[0];
            d[1] = 2 * (x[2] - x[0]);

            for (int i = 1; i < n - 2; i++)
            {
                t = h[i] / d[i];
                z[i + 1] = d[i + 2] - d[i + 1] - z[i] * t;
                d[i + 1] = 2 * (x[i + 2] - x[i]) - h[i] * t;
            }

            z[n - 2] -= h[n - 2] * z[n - 1];

            for (int i = n - 2; i > 0; i--)
            {
                double div = Math.Abs(d[i]) < 1E-12 ? (d[i] < 0 ? -1E-12 : 1E-12) : d[i];
                z[i] = (z[i] - h[i] * z[i + 1]) / div;
            }
        }

        private void CloseCurveMakeTable(double[] x, double[] y, ref double[] z)
        {
            int n = x.Length - 1;
            if (n < 2) return;

            double[] h = new double[n + 1];
            double[] d = new double[n + 1];
            double[] w = new double[n + 1];
            double t;

            for (int i = 0; i < n; i++)
            {
                double diff = x[i + 1] - x[i];
                if (Math.Abs(diff) < 1E-8)
                {
                    diff = 1E-8;
                    x[i + 1] = x[i] + diff;
                }
                h[i] = diff;
                w[i] = (y[i + 1] - y[i]) / h[i];
            }

            w[n] = w[0];

            for (int i = 1; i < n; i++)
            {
                d[i] = 2 * (x[i + 1] - x[i - 1]);
            }
            d[n] = 2 * (h[n - 1] + h[0]);

            for (int i = 1; i <= n; i++)
            {
                z[i] = w[i] - w[i - 1];
            }

            w[1] = h[0];
            w[n - 1] = h[n - 1];
            w[n] = d[n];

            for (int i = 2; i < n - 1; i++)
                w[i] = 0;

            for (int i = 1; i < n; i++)
            {
                double div = Math.Abs(d[i]) < 1E-12 ? (d[i] < 0 ? -1E-12 : 1E-12) : d[i];
                t = h[i] / div;
                z[i + 1] -= z[i] * t;
                d[i + 1] -= h[i] * t;
                w[i + 1] -= w[i] * t;
            }

            w[0] = w[n];
            z[0] = z[n];

            for (int i = n - 2; i >= 0; i--)
            {
                double div = Math.Abs(d[i + 1]) < 1E-12 ? (d[i + 1] < 0 ? -1E-12 : 1E-12) : d[i + 1];
                t = h[i] / div;
                z[i] -= z[i + 1] * t;
                w[i] -= w[i + 1] * t;
            }

            t = Math.Abs(w[0]) < 1E-12 ? 0 : z[0] / w[0];
            z[0] = t;
            z[n] = t;

            for (int i = 1; i < n; i++)
            {
                double div = Math.Abs(d[i]) < 1E-12 ? (d[i] < 0 ? -1E-12 : 1E-12) : d[i];
                z[i] = (z[i] - w[i] * t) / div;
            }
        }

        public double GetSplineValue(double t, double[] x, double[] y, double[] z)
        {
            int n = x.Length;
            if (n < 2) return y.Length > 0 ? y[0] : 0;

            int i = 0;
            int j = n - 1;

            while (i < j)
            {
                int k = (i + j) / 2;
                if (x[k] < t)
                    i = k + 1;
                else
                    j = k;
            }

            if (i > 0)
                i--;

            double h = x[i + 1] - x[i];
            if (Math.Abs(h) < 1E-12) h = 1E-08;
            double d = t - x[i];

            return (((z[i + 1] - z[i]) * d / h + z[i] * 3) * d + ((y[i + 1] - y[i]) / h - (z[i] * 2 + z[i + 1]) * h)) * d + y[i];
        }

        public double GetCloseSplineValue(double t, double[] x, double[] y, double[] z)
        {
            int n = x.Length - 1;
            if (n < 2) return y.Length > 0 ? y[0] : 0;

            double period = x[n] - x[0];
            if (period > 1E-12)
            {
                while (t > x[n]) t -= period;
                while (t < x[0]) t += period;
            }

            int i = 0;
            int j = n;

            while (i < j)
            {
                int k = (i + j) / 2;
                if (x[k] < t)
                    i = k + 1;
                else
                    j = k;
            }

            if (i > 0)
                i--;

            double h = x[i + 1] - x[i];
            if (Math.Abs(h) < 1E-12) h = 1E-08;
            double d = t - x[i];

            return (((z[i + 1] - z[i]) * d / h + z[i] * 3) * d + ((y[i + 1] - y[i]) / h - (z[i] * 2 + z[i + 1]) * h)) * d + y[i];
        }

        private List<IkouPointRecord> SanitizeInputPoints(List<IkouPointRecord> points)
        {
            var clean = new List<IkouPointRecord>();
            if (points == null) return clean;

            foreach (var p in points)
            {
                if (double.IsNaN(p.X) || double.IsNaN(p.Y) || double.IsNaN(p.Z)) continue;

                if (clean.Count == 0)
                {
                    clean.Add(p);
                }
                else
                {
                    var last = clean[^1];
                    double dist = Math.Sqrt((p.X - last.X) * (p.X - last.X) + (p.Y - last.Y) * (p.Y - last.Y) + (p.Z - last.Z) * (p.Z - last.Z));
                    if (dist > 0.0001)
                    {
                        clean.Add(p);
                    }
                }
            }
            return clean;
        }

        public List<XYZ> Calc3DCurvePoints(List<IkouPointRecord> points, int divideStep)
        {
            var clean = SanitizeInputPoints(points);
            if (clean.Count < 2)
                return points?.Select(p => new XYZ(p.X, p.Y, p.Z)).ToList() ?? new List<XYZ>();

            int n = clean.Count;
            double[] x = new double[n];
            double[] y = new double[n];
            double[] z = new double[n];
            _parameterP = new double[n];

            for (int i = 0; i < n; i++)
            {
                x[i] = clean[i].X;
                y[i] = clean[i].Y;
                z[i] = clean[i].Z;
            }

            _parameterP[0] = 0;
            for (int i = 1; i < n; i++)
            {
                double dx = x[i] - x[i - 1];
                double dy = y[i] - y[i - 1];
                double dz = z[i] - z[i - 1];
                double d = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                if (d < 1E-8) d = 1E-8;
                _parameterP[i] = _parameterP[i - 1] + d;
            }

            double totalLength = _parameterP[n - 1];
            if (totalLength > 0)
            {
                for (int i = 0; i < n; i++)
                    _parameterP[i] /= totalLength;
            }

            double[] tx = new double[n];
            double[] ty = new double[n];
            double[] tz = new double[n];

            CurveMakeTable(_parameterP, x, ref tx);
            CurveMakeTable(_parameterP, y, ref ty);
            CurveMakeTable(_parameterP, z, ref tz);

            double minX = x.Min(), maxX = x.Max();
            double minY = y.Min(), maxY = y.Max();
            double spanX = Math.Max(0.5, (maxX - minX) * 0.5);
            double spanY = Math.Max(0.5, (maxY - minY) * 0.5);

            List<XYZ> result = new List<XYZ>();
            int createCount = (n - 1) * Math.Max(1, divideStep);
            for (int i = 0; i <= createCount; i++)
            {
                double t = (double)i / createCount;
                double px = GetSplineValue(t, _parameterP, x, tx);
                double py = GetSplineValue(t, _parameterP, y, ty);
                double pz = GetSplineValue(t, _parameterP, z, tz);

                if (double.IsNaN(px) || px < minX - spanX || px > maxX + spanX) px = Math.Clamp(px, minX - spanX, maxX + spanX);
                if (double.IsNaN(py) || py < minY - spanY || py > maxY + spanY) py = Math.Clamp(py, minY - spanY, maxY + spanY);

                result.Add(new XYZ(px, py, pz));
            }

            return result;
        }

        public List<XYZ> Calc3DCloseCurvePoints(List<IkouPointRecord> points, int divideStep)
        {
            var clean = SanitizeInputPoints(points);
            if (clean.Count < 3)
                return points?.Select(p => new XYZ(p.X, p.Y, p.Z)).ToList() ?? new List<XYZ>();

            var first = clean[0];
            var last = clean[^1];
            double endDist = Math.Sqrt((first.X - last.X) * (first.X - last.X) + (first.Y - last.Y) * (first.Y - last.Y) + (first.Z - last.Z) * (first.Z - last.Z));
            if (endDist > 0.0001)
            {
                clean.Add(new IkouPointRecord { X = first.X, Y = first.Y, Z = first.Z });
            }

            int n = clean.Count - 1;
            double[] x = new double[clean.Count];
            double[] y = new double[clean.Count];
            double[] z = new double[clean.Count];
            _parameterP = new double[clean.Count];

            for (int i = 0; i < clean.Count; i++)
            {
                x[i] = clean[i].X;
                y[i] = clean[i].Y;
                z[i] = clean[i].Z;
            }

            _parameterP[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                double dx = x[i] - x[i - 1];
                double dy = y[i] - y[i - 1];
                double dz = z[i] - z[i - 1];
                double d = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                if (d < 1E-8) d = 1E-8;
                _parameterP[i] = _parameterP[i - 1] + d;
            }

            double totalLength = _parameterP[n];
            if (totalLength > 0)
            {
                for (int i = 0; i <= n; i++)
                    _parameterP[i] /= totalLength;
            }

            double[] tx = new double[n + 1];
            double[] ty = new double[n + 1];
            double[] tz = new double[n + 1];

            CloseCurveMakeTable(_parameterP, x, ref tx);
            CloseCurveMakeTable(_parameterP, y, ref ty);
            CloseCurveMakeTable(_parameterP, z, ref tz);

            double minX = x.Min(), maxX = x.Max();
            double minY = y.Min(), maxY = y.Max();
            double spanX = Math.Max(0.5, (maxX - minX) * 0.5);
            double spanY = Math.Max(0.5, (maxY - minY) * 0.5);

            List<XYZ> result = new List<XYZ>();
            int createCount = n * Math.Max(1, divideStep);
            for (int i = 0; i <= createCount; i++)
            {
                double t = (double)i / createCount;
                double px = GetCloseSplineValue(t, _parameterP, x, tx);
                double py = GetCloseSplineValue(t, _parameterP, y, ty);
                double pz = GetCloseSplineValue(t, _parameterP, z, tz);

                if (double.IsNaN(px) || px < minX - spanX || px > maxX + spanX) px = Math.Clamp(px, minX - spanX, maxX + spanX);
                if (double.IsNaN(py) || py < minY - spanY || py > maxY + spanY) py = Math.Clamp(py, minY - spanY, maxY + spanY);

                result.Add(new XYZ(px, py, pz));
            }

            return result;
        }
    }
}
