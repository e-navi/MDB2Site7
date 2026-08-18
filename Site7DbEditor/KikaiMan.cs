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
        public int KMODE_BI2 = 0;  // 後方交会２点 Backward Intersection
        public int KMODE_BI3 = 1;  // 後方交会３点
        public int KMODE_KB = 2;   // 後視点（既知２点（器械点、後視点)）

        public int kmode = 2;

        public TINP3 kp = new TINP3();
        public TINP3 bp = new TINP3();
        public TINP3 kpA = new TINP3();
        public double angK; // 後視点角度
        public double ang0; // 初期値
        public double kh;   // 器械点高
        public double mh;   // ミラー高
        public double kh3;
        public XYZ kp1 = new XYZ(), kp2 = new XYZ();    // 後方交会 交点
        public TINP3[] tkp = new TINP3[] { new TINP3(), new TINP3(), new TINP3() };
        public TINP3[] tkpA = new TINP3[] { new TINP3(), new TINP3(), new TINP3() }; // 後方交会３点用
        public bool isCalced = false;
        public string errMsg = "";

        public double bmh;      // BM点の高さ
        public KikaiRec[] kr = new KikaiRec[3]; // 視準点(器械高を求める時に使用)

        public KikaiMan()
        {
            kmode = KMODE_KB;
            kp = new TINP3();
            bp = new TINP3();
            kp.Name = Def.GetIniStr("TS", "器械点");
            bp.Name = Def.GetIniStr("TS", "後視点");
            kpA = new TINP3();
            kr = new KikaiRec[3];
            kr[0] = new KikaiRec();
            kr[1] = new KikaiRec();
            kr[2] = new KikaiRec();
            kp1 = new XYZ();
            kp2 = new XYZ();

            tkp = new TINP3[3];
            tkpA = new TINP3[3];

            for (int i = 0; i < 3; i++)
            {
                tkp[i] = new TINP3();
                tkpA[i] = new TINP3();
            }
            kh = Def.GetIniDouble("TS", "器械高", 1.5);
            mh = Def.GetIniDouble("TS", "ミラー高", 1.2);
        }

        public void showForm()
        {
            if (gbl.FormKikai is Form formKikai && !formKikai.IsDisposed)
            {
                formKikai.BringToFront();
            }
        }

        public XYZ cnvP(double lng, double angh, double angv)
        {
            double lh = lng * Math.Sin(St7Lib.ToRadian(angv * 360.0));
            double lv = lng * Math.Cos(St7Lib.ToRadian(angv * 360.0));
            double angH = angh - ang0 + angK;
            double x = kp.X + lh * Math.Cos(St7Lib.ToRadian(angH * 360.0));
            double y = kp.Y + lh * Math.Sin(St7Lib.ToRadian(angH * 360.0));
            lv = kp.Z + lv + kh - mh;
            x = Math.Round(x, 3);
            y = Math.Round(y, 3);
            lv = Math.Round(lv, 3);
            return new XYZ(x, y, lv);
        }

        public void set(KikaiMan km)
        {
            kmode = km.kmode;
            kp.set(km.kp);
            bp.set(km.bp);
            kpA.set(km.kpA);
            for (int i = 0; i < 3; i++)
            {
                kr[i].set(km.kr[i]);
            }
            ang0 = km.ang0;
            angK = km.angK;
            kh = km.kh;
            isCalced = km.isCalced;
        }

        public void reset()
        {
            for (int i = 0; i < 3; i++)
            {
                kr[i].p.Name = "";
                kr[i].isSet = false;
            }
            isCalced = false;
        }

        public bool isSokutenSelect()
        {
            if (kmode == KMODE_KB)
            {
                if (string.IsNullOrEmpty(kr[0].p.Name)) return false;
                if (string.IsNullOrEmpty(kr[1].p.Name)) return false;
            }
            if (kmode == KMODE_BI2)
            {
                if (string.IsNullOrEmpty(kr[0].p.Name)) return false;
                if (string.IsNullOrEmpty(kr[1].p.Name)) return false;
            }
            if (kmode == KMODE_BI3)
            {
                if (string.IsNullOrEmpty(kr[0].p.Name)) return false;
                if (string.IsNullOrEmpty(kr[1].p.Name)) return false;
                if (string.IsNullOrEmpty(kr[2].p.Name)) return false;
            }
            return true;
        }

        public bool isSokuteiSet()
        {
            if (kmode == KMODE_KB)
            {
                if (!kr[1].isSet) return false;
            }
            if (kmode == KMODE_BI2)
            {
                if (!kr[0].isSet) return false;
                if (!kr[1].isSet) return false;
            }
            if (kmode == KMODE_BI3)
            {
                if (!kr[0].isSet) return false;
                if (!kr[1].isSet) return false;
                if (!kr[2].isSet) return false;
            }
            return true;
        }

        public bool calc()
        {
            isCalced = false;
            if (kmode == KMODE_KB)
            {
                kp.set(kr[0].p);
                ang0 = kr[1].angH;
                angK = calc2PAng(kr[0].p, kr[1].p);
                bp.set(kr[1].p);
                isCalced = true;
                return true;
            }
            if (kmode == KMODE_BI2)
            {
                kp.Name = "K1";
                kp.X = 0;
                kp.Y = 0;
                kp.Z = 0;

                if (calcBI2(kr[0], kr[1], kp, kpA) == false)
                {
                    errMsg = "後方交会が求まりません。";
                    return false;
                }
                ang0 = kr[0].angH;
                angK = calc2PAng(kp, kr[0].p);
                bp.set(kr[0].p);
                isCalced = true;
                return true;
            }
            if (kmode == KMODE_BI3)
            {
                kp.Name = "K1";
                kp.X = 0;
                kp.Y = 0;
                kp.Z = 0;

                if (calcBI2(kr[0], kr[1], tkp[0], tkpA[0]) == false)
                {
                    errMsg = "後方交会が求まりません。(1)";
                    return false;
                }
                if (calcBI2(kr[1], kr[2], tkp[1], tkpA[1]) == false)
                {
                    errMsg = "後方交会が求まりません。(2)";
                    return false;
                }
                if (calcBI2(kr[2], kr[0], tkp[2], tkpA[2]) == false)
                {
                    errMsg = "後方交会が求まりません。(3)";
                    return false;
                }
                calcJushin(tkp[0], tkp[1], tkp[2], kp);
                calcJushin(tkpA[0], tkpA[1], tkpA[2], kpA);
                ang0 = kr[0].angH;
                angK = calc2PAng(kp, kr[0].p);
                bp.set(kr[0].p);
                isCalced = true;
                return true;
            }
            return false;
        }

        public void calcJushin(TINP3 p1, TINP3 p2, TINP3 p3, TINP3 p)
        {
            p.X = (p1.X + p2.X) / 2;
            p.Y = (p1.Y + p2.Y) / 2;
            p.X = p.X + (p3.X - p.X) / 3;
            p.Y = p.Y + (p3.Y - p.Y) / 3;
        }

        public bool calcBI2(KikaiRec kr1, KikaiRec kr2, XYZ p1, XYZ p2, TINP3 kp, TINP3 kpA)
        {
            double angd = kr2.angH - kr1.angH;
            if (angd < -0.5) angd += 1.0;
            if (0.5 < angd) angd -= 1.0;

            double len1 = kr1.getLngH();
            double len2 = kr2.getLngH();

            int ret = St7Lib.kotenCC(kp1, kp2, p1, len1, p2, len2);
            if (ret != 2)
            {
                return false;
            }
            double ang11 = calc2PAng(kp1, p1);
            double ang12 = calc2PAng(kp1, p2);
            double angd1 = ang12 - ang11;
            if (angd1 < -0.5) angd1 += 1.0;
            if (0.5 < angd1) angd1 -= 1.0;
            double angda1 = Math.Abs(angd1 - angd);

            double ang21 = calc2PAng(kp2, p1);
            double ang22 = calc2PAng(kp2, p2);
            double angd2 = ang22 - ang21;
            if (angd2 < -0.5) angd2 += 1.0;
            if (0.5 < angd2) angd2 -= 1.0;

            double angda2 = Math.Abs(angd2 - angd);

            double ang1;
            double ang2;
            if (angda1 < angda2)
            {
                kp.X = kp1.X;
                kp.Y = kp1.Y;
                ang1 = ang11 + (angd1 - angd) / 2;
                ang2 = ang12 - (angd1 - angd) / 2;
            }
            else
            {
                kp.X = kp2.X;
                kp.Y = kp2.Y;
                ang1 = ang21 + (angd2 - angd) / 2;
                ang2 = ang22 - (angd2 - angd) / 2;
            }
            XYZ tp1 = new XYZ(p1.X + 100 * Math.Cos(St7Lib.ToRadian(ang1 * 360.0)),
                    p1.Y + 100 * Math.Sin(St7Lib.ToRadian(ang1 * 360.0)));
            XYZ tp2 = new XYZ(p2.X + 100 * Math.Cos(St7Lib.ToRadian(ang2 * 360.0)),
                    p2.Y + 100 * Math.Sin(St7Lib.ToRadian(ang2 * 360.0)));

            XYZ koten = St7Lib.CalcKoten2(p1, tp1, p2, tp2);
            if (koten == null)
                return false;

            kpA.set(koten);
            return true;
        }

        public double calc2PAng(XYZ p0, XYZ p1)
        {
            double ang = Math.Atan2(p1.Y - p0.Y, p1.X - p0.X);
            ang /= (Math.PI * 2.0);
            if (ang < 0) ang += 1.0;
            return ang;
        }

        public bool calcBI2(KikaiRec kr1, KikaiRec kr2, TINP3 kp, TINP3 kpA)
        {
            return calcBI2(kr1, kr2, kr1.p, kr2.p, kp, kpA);
        }
    }
}
