using System;
using System.Collections.Generic;

namespace Site7DbEditor
{
    public class IkouModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string Date { get; set; } = "";
    }

    public class IkouLModel
    {
        public long Id { get; set; }
        public long Lid { get; set; }
        public string Name { get; set; } = "";
        public int Mode { get; set; } // 0: 開放, 1: 閉
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Layer { get; set; } = 1; // 1-16
        public string Date { get; set; } = "";
        public string Precs { get; set; } = "";
    }

    public class IkouPointRecord
    {
        public int Pid { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string Date { get; set; } = "";
        public double S { get; set; }
        public double V { get; set; }
        public double H { get; set; }
        public string KPName { get; set; } = "";
        public string BPName { get; set; } = "";
        public double KPH { get; set; }
        public double MRH { get; set; }
    }

    public class IbutuModel
    {
        public long Id { get; set; }
        public string Chiku { get; set; } = "";
        public string Soui { get; set; } = "";
        public string Syubetu { get; set; } = "";
        public int No { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Layer { get; set; } = 1; // 1-16
        public string Date { get; set; } = "";
        public double S { get; set; }
        public double V { get; set; }
        public double H { get; set; }
        public string KPName { get; set; } = "";
        public string BPName { get; set; } = "";
        public double KPH { get; set; }
        public double MRH { get; set; }
    }

    public class KikaiModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Layer { get; set; } = 1; // 1-16
        public string Date { get; set; } = "";
        public double S { get; set; }
        public double V { get; set; }
        public double H { get; set; }
        public string KPName { get; set; } = "";
        public string BPName { get; set; } = "";
        public double KPH { get; set; }
        public double MRH { get; set; }
    }

    public class LayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Color { get; set; } = 1; // 1-16
        public int Mark { get; set; } = 1;  // 1:〇 2:□ 3:△ 4:⦿
        public double Size { get; set; } = 5.0; // 1-20
        public int Width { get; set; } = 1; // 1-5
        public int LType { get; set; } = 1; // 1:折れ線 2:曲線
    }
}
