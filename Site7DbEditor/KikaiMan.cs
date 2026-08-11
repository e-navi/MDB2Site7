using System;

namespace Site7DbEditor
{
    public class KikaiRec
    {
        public TINP3 p;
        public double lng;
        public double angV;
        public double angH;
        public bool isSet;

        public KikaiRec()
        {
            isSet = false;
            p = new TINP3();
        }

        public void set(KikaiRec kr)
        {
            p.set(kr.p);
            p.Name = kr.p.Name;
            lng = kr.lng;
            angV = kr.angV;
            angH = kr.angH;
            isSet = kr.isSet;
        }

        public double getLngH()
        {
            double lh = lng * Math.Sin(St7Lib.ToRadian(angV * 360.0));
            return Math.Abs(lh);
        }

        public double getLngV()
        {
            return lng * Math.Cos(St7Lib.ToRadian(angV * 360.0));
        }
    }

    public class KikaiMan
    {
        public int KMODE_BI2 = 0;
        public int KMODE_BI3 = 1;
        public int KMODE_KB = 2;

        public int kmode = 2;

        public TINP3 kp = new TINP3();
        public TINP3 bp = new TINP3();
        public TINP3 kpA = new TINP3();
        public double angK;
        public double ang0;
        public double kh;
        public double mh;
        public double kh3;
        public XYZ kp1 = new XYZ(), kp2 = new XYZ();
        public TINP3[] tkp = new TINP3[] { new TINP3(), new TINP3(), new TINP3() };
        public TINP3[] tkpA = new TINP3[] { new TINP3(), new TINP3(), new TINP3() };
        public bool isCalced = false;
        public string errMsg = "";

        public double bmh;

        public XYZ cnvP(double _lng, double _angH, double _angV)
        {
            double lh = _lng * Math.Sin(St7Lib.ToRadian(_angV * 360.0));
            double lv = _lng * Math.Cos(St7Lib.ToRadian(_angV * 360.0));

            double x = kp.X + lh * Math.Cos(St7Lib.ToRadian(_angH * 360.0));
            double y = kp.Y + lh * Math.Sin(St7Lib.ToRadian(_angH * 360.0));
            double z = kp.Z + kh + lv - mh;

            return new XYZ(x, y, z);
        }

        public void showForm()
        {
            // Placeholder for FormKikai
        }
    }
}
