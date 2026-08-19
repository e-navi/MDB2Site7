using System;

namespace Site7DbEditor
{

    public class TINP3 : XYZ
    {
        public string Name { get; set; } = "";
        public TINP3() : base() { }
        public TINP3(double x, double y, double z = 0.0, string name = "") : base(x, y, z)
        {
            Name = name;
        }
        public TINP3(string name, double x, double y, double z = 0.0) : base(x, y, z)
        {
            Name = name;
        }

        public void set(TINP3 p)
        {
            base.set(p);
            Name = p.Name;
        }

        public double CalcLen(TINP3 p)
        {
            double dx = p.X - X;
            double dy = p.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double CalcLen(XYZ p)
        {
            double dx = p.X - X;
            double dy = p.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    public class KijunPRec : TINP3
    {
        public int Layer { get; set; } = 1;
        public KijunPRec() : base() { }
        public KijunPRec(string name, double x, double y, double z = 0.0, int layer = 1) : base(x, y, z, name)
        {
            Layer = layer;
        }
    }

    public class KijunPRecEx : KijunPRec
    {
        public KijunPRecEx() : base() { }
        public KijunPRecEx(string name, double x, double y, double z = 0.0, int layer = 1) : base(name, x, y, z, layer) { }
    }

    public static class St7Lib
    {
        public static double CheckDouble(string? val, double defVal = 0.0)
        {
            if (string.IsNullOrEmpty(val)) return defVal;
            if (double.TryParse(val, out double v)) return v;
            return defVal;
        }

        public static double CheckDouble2(string? val, double defVal = 0.0, int exp = 0)
        {
            if (string.IsNullOrEmpty(val)) return defVal;
            if (double.TryParse(val, out double v))
            {
                return v * Math.Pow(10, exp);
            }
            return defVal;
        }

        public static int CheckInt(string? val, int defVal = 0)
        {
            if (string.IsNullOrEmpty(val)) return defVal;
            if (int.TryParse(val, out int v)) return v;
            return defVal;
        }

        public static double CheckAng(string? val, double defVal = 0.0)
        {
            if (string.IsNullOrEmpty(val)) return defVal;
            if (double.TryParse(val, out double deg))
            {
                double d = Math.Floor(deg / 100.0);
                double m = deg - d * 100.0;
                return d + m / 60.0;
            }
            return defVal;
        }

        public static double ToRadian(double val)
        {
            return val * Math.PI / 180.0;
        }

        public static double ToDegree(double val)
        {
            return val * 180.0 / Math.PI;
        }

        public static int kotenCC(XYZ kp1, XYZ kp2, XYZ p1, double len1, XYZ p2, double len2)
        {
            try
            {
                double d = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
                if (d > len1 + len2 || d < Math.Abs(len1 - len2) || d == 0) return 0;
                double a = (Math.Pow(len1, 2) - Math.Pow(len2, 2) + Math.Pow(d, 2)) / (2 * d);
                double h = Math.Sqrt(Math.Max(0, Math.Pow(len1, 2) - Math.Pow(a, 2)));
                double x2 = p1.X + a * (p2.X - p1.X) / d;
                double y2 = p1.Y + a * (p2.Y - p1.Y) / d;

                kp1.X = x2 + h * (p2.Y - p1.Y) / d;
                kp1.Y = y2 - h * (p2.X - p1.X) / d;

                kp2.X = x2 - h * (p2.Y - p1.Y) / d;
                kp2.Y = y2 + h * (p2.X - p1.X) / d;
                return 2;
            }
            catch
            {
                return 0;
            }
        }

        public static XYZ CalcKoten2(XYZ p1, XYZ tp1, XYZ p2, XYZ tp2)
        {
            double a1 = tp1.Y - p1.Y;
            double b1 = p1.X - tp1.X;
            double c1 = a1 * p1.X + b1 * p1.Y;

            double a2 = tp2.Y - p2.Y;
            double b2 = p2.X - tp2.X;
            double c2 = a2 * p2.X + b2 * p2.Y;

            double det = a1 * b2 - a2 * b1;
            if (Math.Abs(det) < 1e-9) return new XYZ(p1.X, p1.Y, 0);

            double x = (b2 * c1 - b1 * c2) / det;
            double y = (a1 * c2 - a2 * c1) / det;
            return new XYZ(x, y, 0);
        }

        public static void CenterOnMainForm(System.Windows.Forms.Form form)
        {
            if (gbl.FormMain != null && !gbl.FormMain.IsDisposed)
            {
                form.Owner = gbl.FormMain;
                form.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                int x = gbl.FormMain.Location.X + (gbl.FormMain.Width - form.Width) / 2;
                int y = gbl.FormMain.Location.Y + (gbl.FormMain.Height - form.Height) / 2;
                form.Location = new System.Drawing.Point(x, y);
            }
            else
            {
                form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }
        }
    }
}
