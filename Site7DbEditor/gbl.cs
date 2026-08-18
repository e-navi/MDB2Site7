using System;

namespace Site7DbEditor
{
    public static class gbl
    {
        public static KikaiMan KikaiMan { get; set; } = new KikaiMan();
        public static KikaiMan KikaiMan0 { get; set; } = new KikaiMan();
        public static TStation TStation { get; set; } = new TStation();
        public static Gps Gps { get; set; } = new Gps();
        public static MField MField { get; set; } = new MField();
        public static LN100 LN100 { get; set; } = new LN100();
        public static FormEditor? FormMain { get; set; }
        public static FormKikaiDef? FormKikaiDef { get; set; }
        public static FormKikai? FormKikai { get; set; }
        public static UCCtrl? UCCtrl { get; set; }

        public static class st7Data
        {
            public static class KijunP
            {
                public static System.Collections.Generic.List<KijunPRecEx> KPList { get; set; } = new System.Collections.Generic.List<KijunPRecEx>();
            }
        }
    }
}
