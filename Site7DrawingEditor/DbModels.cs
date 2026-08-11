using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace Site7DrawingEditor
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
    }

    public class Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D() : this(0, 0, 0) { }

        public Point3D(double x, double y, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(Point3D other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }
    }

    /// <summary>
    /// 用紙サイズ構造体 (ミリメートル単位)
    /// </summary>
    public class PaperSizeInfo
    {
        public int SizeId { get; set; }
        public string Name { get; set; } = "";
        public float WidthMm { get; set; }
        public float HeightMm { get; set; }

        public static readonly PaperSizeInfo[] PaperSizes = new PaperSizeInfo[]
        {
            new PaperSizeInfo { SizeId = 0, Name = "A0", WidthMm = 1189f, HeightMm = 841f },
            new PaperSizeInfo { SizeId = 1, Name = "A1", WidthMm = 841f, HeightMm = 594f },
            new PaperSizeInfo { SizeId = 2, Name = "A2", WidthMm = 594f, HeightMm = 420f },
            new PaperSizeInfo { SizeId = 3, Name = "A3", WidthMm = 420f, HeightMm = 297f },
            new PaperSizeInfo { SizeId = 4, Name = "A4", WidthMm = 297f, HeightMm = 210f },
            new PaperSizeInfo { SizeId = 5, Name = "A5", WidthMm = 210f, HeightMm = 148f }
        };
    }

    /// <summary>
    /// 図面 (Drawing) テーブルモデル
    /// </summary>
    public class DrawingModel
    {
        public int ZID { get; set; }
        public int Type { get; set; }       // 0:全図 1:部分図
        public string Name { get; set; } = "";
        public int PaperSize { get; set; }  // 0:A0 1:A1 2:A2 3:A3 4:A4 5:A5
        public int Scale { get; set; }      // 1 / Scale (e.g. 20 for 1/20)

        public PaperSizeInfo PaperInfo => (PaperSize >= 0 && PaperSize < PaperSizeInfo.PaperSizes.Length)
            ? PaperSizeInfo.PaperSizes[PaperSize]
            : PaperSizeInfo.PaperSizes[3];
    }

    /// <summary>
    /// 図面遺構 (Drawing Feature) テーブルモデル
    /// </summary>
    public class DrawingIkouModel
    {
        public int ZID { get; set; }
        public int IID { get; set; }
        public string Name { get; set; } = "";
        public XYZ P1 { get; set; } = new XYZ(); // 左下
        public XYZ P2 { get; set; } = new XYZ(); // 右下
        public XYZ P3 { get; set; } = new XYZ(); // 右上 (右下からの垂線上)
        public Point3D PP { get; set; } = new Point3D(); // 配置点 (用紙座標 mm)

        public string LListStr { get; set; } = "";
        public string DmListStr { get; set; } = "";

        public List<ZIkouLRec> LList { get; set; } = new List<ZIkouLRec>();

        public int IsShowDirection { get; set; } = 1; // 方位を表示するか 0:しない 1:する
        public Point3D PDirection { get; set; } = new Point3D(20, 20, 0); // 方位の表示位置 (用紙座標 mm)
        public List<DanmenRec> DmList { get; set; } = new List<DanmenRec>();

        public void Str2LList(string strs)
        {
            LList.Clear();
            if (string.IsNullOrEmpty(strs)) return;
            string[] lines = strs.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                try
                {
                    LList.Add(new ZIkouLRec(line));
                }
                catch { }
            }
        }

        public string LList2Str()
        {
            var strList = new List<string>();
            foreach (var item in LList)
            {
                strList.Add(item.ToStrs());
            }
            return string.Join("\n", strList);
        }

        public void Str2DmList(string strs)
        {
            IsShowDirection = 1;
            PDirection = new Point3D(20, 20, 0);
            DmList.Clear();
            if (string.IsNullOrEmpty(strs)) return;

            string[] lines = strs.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0) return;

            try
            {
                string header = lines[0];
                string[] items = header.Split('\t');
                if (items.Length >= 3)
                {
                    IsShowDirection = int.Parse(items[0]);
                    PDirection.X = double.Parse(items[1], CultureInfo.InvariantCulture);
                    PDirection.Y = double.Parse(items[2], CultureInfo.InvariantCulture);
                }
            }
            catch { }

            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    DmList.Add(new DanmenRec(lines[i]));
                }
                catch { }
            }
        }

        public string DmList2Str()
        {
            string header = $"{IsShowDirection}\t{PDirection.X:0.000}\t{PDirection.Y:0.000}";
            var strList = new List<string> { header };
            foreach (var dm in DmList)
            {
                strList.Add(dm.ToStrs());
            }
            return string.Join("\n", strList);
        }
    }

    public class ZIkouLRec
    {
        public int LID { get; set; }
        public int Layer { get; set; }
        public int Flag { get; set; } // 0:直線 1:曲線 2:点
        public List<Point3D> Pnts { get; set; } = new List<Point3D>();

        public ZIkouLRec(int lid, int layer, int flag, List<Point3D> pnts)
        {
            LID = lid;
            Layer = layer;
            Flag = flag;
            Pnts = pnts;
        }

        public ZIkouLRec(string strs)
        {
            string[] items = strs.Split('\t');
            LID = int.Parse(items[0]);
            Layer = int.Parse(items[1]);
            Flag = int.Parse(items[2]);
            Pnts = new List<Point3D>();

            int count = (items.Length - 3) / 3;
            for (int i = 0; i < count; i++)
            {
                double x = double.Parse(items[3 + i * 3 + 0], CultureInfo.InvariantCulture);
                double y = double.Parse(items[3 + i * 3 + 1], CultureInfo.InvariantCulture);
                double z = double.Parse(items[3 + i * 3 + 2], CultureInfo.InvariantCulture);
                Pnts.Add(new Point3D(x, y, z));
            }
        }

        public string ToStrs()
        {
            string str = $"{LID}\t{Layer}\t{Flag}";
            foreach (var p in Pnts)
            {
                str += $"\t{p.X:0.000}\t{p.Y:0.000}\t{p.Z:0.000}";
            }
            return str;
        }
    }

    public class MasterLayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int LType { get; set; } = 1; // 1: 折れ線, 2: 曲線
    }

    public class DanmenRec
    {
        public int DID { get; set; }
        public string Name { get; set; } = "";
        public XYZ Sp { get; set; } = new XYZ(); // 始点
        public XYZ Ep { get; set; } = new XYZ(); // 終点
        public XYZ Dp { get; set; } = new XYZ(); // 断面表示位置
        public List<DanmenPRec> DmpList { get; set; } = new List<DanmenPRec>();

        public DanmenRec() : this(0, "", new XYZ(), new XYZ(), new XYZ()) { }

        public DanmenRec(int did, string name, XYZ sp, XYZ ep, XYZ dp)
        {
            DID = did;
            Name = name;
            Sp = new XYZ(sp);
            Ep = new XYZ(ep);
            Dp = new XYZ(dp);
            DmpList = new List<DanmenPRec>();
        }

        public DanmenRec(string strs)
        {
            string[] items = strs.Split('\t');
            int ii = 0;
            DID = int.Parse(items[ii++]);
            Name = items[ii++];
            Sp = new XYZ(double.Parse(items[ii++], CultureInfo.InvariantCulture), double.Parse(items[ii++], CultureInfo.InvariantCulture));
            Ep = new XYZ(double.Parse(items[ii++], CultureInfo.InvariantCulture), double.Parse(items[ii++], CultureInfo.InvariantCulture));
            Dp = new XYZ(double.Parse(items[ii++], CultureInfo.InvariantCulture), double.Parse(items[ii++], CultureInfo.InvariantCulture));
            DmpList = new List<DanmenPRec>();

            int count = (items.Length - 8) / 2;
            for (int i = 0; i < count; i++)
            {
                double len = double.Parse(items[8 + i * 2 + 0], CultureInfo.InvariantCulture);
                double h = double.Parse(items[8 + i * 2 + 1], CultureInfo.InvariantCulture);
                DmpList.Add(new DanmenPRec(len, h));
            }
        }

        public double GetBaseH()
        {
            double h = -1000;
            foreach (var dp in DmpList)
            {
                if (h < dp.H) h = dp.H;
            }
            h *= 10;
            h = Math.Ceiling(h);
            h /= 10;
            return h;
        }

        public string ToStrs()
        {
            string str = $"{DID}\t{Name}";
            str += $"\t{Sp.X:0.000}\t{Sp.Y:0.000}";
            str += $"\t{Ep.X:0.000}\t{Ep.Y:0.000}";
            str += $"\t{Dp.X:0.000}\t{Dp.Y:0.000}";
            foreach (var dp in DmpList)
            {
                str += $"\t{dp.Len:0.000}\t{dp.H:0.000}";
            }
            return str;
        }
    }

    public class DanmenPRec : IEquatable<DanmenPRec>, IComparable<DanmenPRec>
    {
        public double Len { get; set; }
        public double H { get; set; }

        public DanmenPRec(double len, double h)
        {
            Len = len;
            H = h;
        }

        public int CompareTo(DanmenPRec? other)
        {
            if (other == null) return 1;
            return other.Len < Len ? 1 : -1;
        }

        public bool Equals(DanmenPRec? other)
        {
            if (other == null) return false;
            return Math.Abs(Len - other.Len) < 0.0001;
        }
    }

    public class MasterIkouModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class MasterIkouLModel
    {
        public long Id { get; set; }
        public long Lid { get; set; }
        public string Name { get; set; } = "";
        public int Mode { get; set; }
        public int Layer { get; set; }
        public string Precs { get; set; } = "";
    }

    public class MasterIbutuModel
    {
        public long Id { get; set; }
        public string Chiku { get; set; } = "";
        public string Soui { get; set; } = "";
        public string Syubetu { get; set; } = "";
        public long No { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Layer { get; set; }
    }

    public class MasterKikaiModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Syubetu { get; set; }
    }
}
