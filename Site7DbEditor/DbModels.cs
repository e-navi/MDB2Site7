using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Site7DbEditor
{
    public class IkouModel
    {
        [DisplayName("ID")]
        public long Id { get; set; }
        [DisplayName("遺構名")]
        public string Name { get; set; } = "";
        [DisplayName("X")]
        public double X { get; set; }
        [DisplayName("Y")]
        public double Y { get; set; }
        [DisplayName("Z")]
        public double Z { get; set; }
        [DisplayName("日付")]
        public string Date { get; set; } = "";
    }

    public class IkouLModel
    {
        [DisplayName("ID")]
        public long Id { get; set; }
        [DisplayName("LID")]
        public long Lid { get; set; }
        [DisplayName("遺構線名")]
        public string Name { get; set; } = "";
        [DisplayName("種類")]
        public int Mode { get; set; } // 0: 開放, 1: 閉
        [DisplayName("X")]
        public double X { get; set; }
        [DisplayName("Y")]
        public double Y { get; set; }
        [DisplayName("Z")]
        public double Z { get; set; }
        [DisplayName("レイヤ")]
        public int Layer { get; set; } = 1; // 1-16
        [DisplayName("日付")]
        public string Date { get; set; } = "";
        [Browsable(false)]
        public string Precs { get; set; } = "";
    }

    public class IkouPointRecord
    {
        [DisplayName("PID")]
        public int Pid { get; set; }
        [DisplayName("X")]
        public double X { get; set; }
        [DisplayName("Y")]
        public double Y { get; set; }
        [DisplayName("Z")]
        public double Z { get; set; }
        [DisplayName("日付")]
        public string Date { get; set; } = "";
        [Browsable(false)]
        public double S { get; set; }
        [Browsable(false)]
        public double V { get; set; }
        [Browsable(false)]
        public double H { get; set; }
        [Browsable(false)]
        public string KPName { get; set; } = "";
        [Browsable(false)]
        public string BPName { get; set; } = "";
        [Browsable(false)]
        public double KPH { get; set; }
        [Browsable(false)]
        public double MRH { get; set; }
    }

    public class IbutuModel
    {
        [DisplayName("ID")]
        public long Id { get; set; }
        [DisplayName("出土地点")]
        public string Chiku { get; set; } = "";
        [DisplayName("出土層位")]
        public string Soui { get; set; } = "";
        [DisplayName("種別")]
        public string Syubetu { get; set; } = "";
        [DisplayName("No")]
        public int No { get; set; }
        [DisplayName("X")]
        public double X { get; set; }
        [DisplayName("Y")]
        public double Y { get; set; }
        [DisplayName("Z")]
        public double Z { get; set; }
        [DisplayName("レイヤ")]
        public int Layer { get; set; } = 1; // 1-16
        [DisplayName("日付")]
        public string Date { get; set; } = "";
        [Browsable(false)]
        public double S { get; set; }
        [Browsable(false)]
        public double V { get; set; }
        [Browsable(false)]
        public double H { get; set; }
        [Browsable(false)]
        public string KPName { get; set; } = "";
        [Browsable(false)]
        public string BPName { get; set; } = "";
        [Browsable(false)]
        public double KPH { get; set; }
        [Browsable(false)]
        public double MRH { get; set; }
    }

    public class KikaiModel
    {
        [DisplayName("ID")]
        public long Id { get; set; }
        [DisplayName("基準点名")]
        public string Name { get; set; } = "";
        [DisplayName("X")]
        public double X { get; set; }
        [DisplayName("Y")]
        public double Y { get; set; }
        [DisplayName("Z")]
        public double Z { get; set; }
        [DisplayName("レイヤ")]
        public int Layer { get; set; } = 1; // 1-16
        [DisplayName("日付")]
        public string Date { get; set; } = "";
        [Browsable(false)]
        public double S { get; set; }
        [Browsable(false)]
        public double V { get; set; }
        [Browsable(false)]
        public double H { get; set; }
        [Browsable(false)]
        public string KPName { get; set; } = "";
        [Browsable(false)]
        public string BPName { get; set; } = "";
        [Browsable(false)]
        public double KPH { get; set; }
        [Browsable(false)]
        public double MRH { get; set; }
    }

    public class LayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Color { get; set; } = 1; // 1-16
        public int Mark { get; set; } = 1;  // 1:〇 2:□ 3:△ 4:⦿
        public double Size { get; set; } = 1.0; // 1-20
        public int Width { get; set; } = 1; // 1-5
        public int LType { get; set; } = 1; // 1:折線 2:曲線
    }
}
