using System;

namespace Site7DbEditor
{
    public class BLXY
    {
        public int cSKei, cKei;
        public static int SKEI_JPN = 0;
        public static int SKEI_WLD = 1;
        BL BL0 = new BL();
        double a;
        double f;
        double e;
        double e1;
        double e2;
        double b1, b2, b3, b4, b5, b6, b7, b8, b9;
        double s0;
        double m0 = 0.9999;

        public BLXY(int skei, int kei)
        {
            SetKei(skei, kei);
        }

        public class BL
        {
            public double Lat;
            public double Lng;
            public BL(double _lat, double _lng)
            {
                Lat = _lat;
                Lng = _lng;
            }
            public BL()
            {
                Lat = 0.0;
                Lng = 0.0;
            }
            public string ToStr()
            {
                return Lat.ToString("0.000000000") + ", " + Lng.ToString("0.0000000000");
            }
        }

        public class P2
        {
            public double X;
            public double Y;
            public P2(double _x, double _y)
            {
                X = _x;
                Y = _y;
            }
            public P2()
            {
                X = 0.0;
                Y = 0.0;
            }
            public double CalcLen(P2 p)
            {
                return (Math.Sqrt((X - p.X) * (X - p.X) + (Y - p.Y) * (Y - p.Y)));
            }
        }

        double MeridS(double Phi)
        {
            double s = b1 * Phi + b2 * Math.Sin(2.0 * Phi) +
                    b3 * Math.Sin(4.0 * Phi) + b4 * Math.Sin(6.0 * Phi) +
                    b5 * Math.Sin(8.0 * Phi) + b6 * Math.Sin(10.0 * Phi) +
                    b7 * Math.Sin(12.0 * Phi) + b8 * Math.Sin(14.0 * Phi) +
                    b9 * Math.Sin(16.0 * Phi);
            return s;
        }

        public P2 BLtoWP2(int sc, BL bl0)
        {
            P2 p = new P2();
            double t1, t2, t3;

            t1 = 128 / Math.PI;
            t2 = (bl0.Lat * Math.PI / 180);
            t2 = Math.Sin(t2);
            t3 = Math.Pow(2, sc);

            p.Y = t1 / 2 * Math.Log((1 + t2) / (1 - t2)) + 128;
            p.Y = p.Y / 256 * t3;
            p.Y = t3 - p.Y;

            p.X = (bl0.Lng * Math.PI / 180);
            p.X = t1 * (p.X + Math.PI);
            p.X = p.X / 256 * t3;

            return p;
        }

        public BL WP2toBL(int sc, P2 p)
        {
            BL bl = new BL();
            double t1, t2, t3;

            t1 = 128 / Math.PI;
            t3 = Math.Pow(2, sc);
            t2 = (t3 - p.Y) * 256 / t3;
            t2 = Math.Atan(Math.Sinh((t2 - 128) / t1));
            bl.Lat = t2 / Math.PI * 180;

            t2 = p.X * 256 / t3;
            t2 = t2 / t1 - Math.PI;

            bl.Lng = t2 / Math.PI * 180;

            return bl;
        }

        public BL XYR2BL(P2 XY)
        {
            P2 p = new P2(XY.Y, XY.X);
            return XY2BL(p);
        }

        public P2 BL2XYR(BL bl)
        {
            P2 p = BL2XY(bl);
            return new P2(p.Y, p.X);
        }

        public BL XY2BL(P2 XY)
        {
            BL BL1 = new BL();
            double s0 = MeridS(BL0.Lat);

            double M = s0 + XY.X / m0;

            int icount = 0;
            double phi1 = BL0.Lat;
            double oldphi1;
            double s1;
            double Bunsi;
            double Bunbo;
            do
            {
                icount = icount + 1;
                oldphi1 = phi1;
                s1 = MeridS(phi1);
                Bunsi = 2.0 * (s1 - M) * Math.Pow((1.0 - e1 * Math.Sin(phi1) * Math.Sin(phi1)), 1.5);
                Bunbo = 3.0 * e1 * (s1 - M) * Math.Sin(phi1) * Math.Cos(phi1) * Math.Sqrt(1.0 - e1 * Math.Sin(phi1) * Math.Sin(phi1)) - 2.0 * a * (1.0 - e1);
                phi1 = phi1 + Bunsi / Bunbo;
            } while ((Math.Abs(phi1 - oldphi1) > 0.0000000000001) && (icount < 100));

            double YM0;
            double T, T2, T4, T6;
            double Eta2;
            double M1, N1, N1CosPhi1;
            double B, L;
            double CEE;
            double Ep2;

            CEE = a / Math.Sqrt(1.0 - e);
            Ep2 = e / (1.0 - e);

            YM0 = XY.Y / m0;
            T = Math.Tan(phi1);
            T2 = T * T; T4 = T2 * T2; T6 = T4 * T2;
            Eta2 = Ep2 * Math.Cos(phi1) * Math.Cos(phi1);
            M1 = CEE / Math.Sqrt(Math.Pow((1.0 + Eta2), 3.0));
            N1 = CEE / Math.Sqrt(1.0 + Eta2);
            N1CosPhi1 = N1 * Math.Cos(phi1);

            B = ((1385.0D + 3633.0D * T2 + 4095.0D * T4 + 1575.0D * T6) / (40320.0D * Math.Pow(N1, 8.0))) * Math.Pow(YM0, 8.0);
            B -= ((61.0D + 90.0D * T2 + 45.0D * T4 + 107.0D * Eta2 - 162.0D * T2 * Eta2 - 45.0D * T4 * Eta2) / (720.0D * Math.Pow(N1, 6.0))) * Math.Pow(YM0, 6.0);
            B += ((5.0D + 3.0D * T2 + 6.0D * Eta2 - 6.0D * T2 * Eta2 - 3.0D * Math.Pow(Eta2, 2.0) - 9.0D * T2 * Math.Pow(Eta2, 2.0)) / (24.0D * Math.Pow(N1, 4.0))) * Math.Pow(YM0, 4.0);
            B -= ((1.0D + Eta2) / (2.0D * Math.Pow(N1, 2.0))) * Math.Pow(YM0, 2.0);
            B *= T;
            B += phi1;

            L = -((61.0 + 662.0 * T2 + 1320.0 * T4 + 720.0 * T6) / (5040.0 * Math.Pow(N1, 6.0) * N1CosPhi1)) * Math.Pow(YM0, 7.0);
            L = L + ((5.0 + 28.0 * T2 + 24.0 * T4 + 6.0 * Eta2 + 8.0 * T2 * Eta2) / (120.0 * Math.Pow(N1, 4.0) * N1CosPhi1)) * Math.Pow(YM0, 5.0);
            L = L - ((1.0 + 2.0 * T2 + Eta2) / (6.0 * Math.Pow(N1, 2.0) * N1CosPhi1)) * Math.Pow(YM0, 3.0);
            L = L + (1.0 / N1CosPhi1) * YM0;
            L = L + BL0.Lng;

            BL1.Lat = Rad2Deg(B);
            BL1.Lng = Rad2Deg(L);

            return BL1;
        }

        public P2 BL2XY(BL BL1)
        {
            P2 XY = new P2();

            BL1.Lat = Deg2Rad(BL1.Lat);
            BL1.Lng = Deg2Rad(BL1.Lng);

            double s = b1 * BL1.Lat + b2 * Math.Sin(2.0 * BL1.Lat) +
                                b3 * Math.Sin(4.0 * BL1.Lat) + b4 * Math.Sin(6.0 * BL1.Lat) +
                                b5 * Math.Sin(8.0 * BL1.Lat) + b6 * Math.Sin(10.0 * BL1.Lat) +
                                b7 * Math.Sin(12.0 * BL1.Lat) + b8 * Math.Sin(14.0 * BL1.Lat) +
                                b9 * Math.Sin(16.0 * BL1.Lat);

            double SinLat1 = Math.Sin(BL1.Lat);
            double CosLat1 = Math.Cos(BL1.Lat);
            double TanLat1 = Math.Tan(BL1.Lat);

            double deltaLambda = BL1.Lng - BL0.Lng;
            double eta2 = Math.Pow(e2, 2.0) * Math.Pow(CosLat1, 2.0);
            double t = TanLat1;
            double m0 = 0.9999;

            double w = Math.Sqrt(1.0 - Math.Pow(e1, 2.0) * Math.Pow(SinLat1, 2.0));
            double n = a / w;

            XY.X = ((s - s0) + 1.0 / 2.0 * n * Math.Pow(CosLat1, 2.0) * t * Math.Pow(deltaLambda, 2.0) +
                                1.0 / 24.0 * n * Math.Pow(CosLat1, 4.0) * t *
                                (5.0 - Math.Pow(t, 2.0) + 9.0 * eta2 + 4.0 * Math.Pow(eta2, 2.0)) * Math.Pow(deltaLambda, 4.0) -
                                1.0 / 720.0 * n * Math.Pow(CosLat1, 6.0) * t *
                                (-61.0 + 58.0 * Math.Pow(t, 2.0) - Math.Pow(t, 4.0) - 270.0 * eta2 + 330.0 * Math.Pow(t, 2.0) * eta2) *
                                Math.Pow(deltaLambda, 6.0) -
                                1.0 / 40320.0 * n * Math.Pow(CosLat1, 8.0) * t *
                                (-1385.0 + 3111.0 * Math.Pow(t, 2.0) - 543.0 * Math.Pow(t, 4.0) + Math.Pow(t, 6.0)) * Math.Pow(deltaLambda, 8.0)) * m0;

            XY.Y = (n * CosLat1 * deltaLambda -
                                1.0 / 6.0 * n * Math.Pow(CosLat1, 3.0) * (-1.0 + Math.Pow(t, 2.0) - eta2) *
                                        Math.Pow(deltaLambda, 3.0) -
                                1.0 / 120.0 * n * Math.Pow(CosLat1, 5.0) *
                                (-5.0 + 18.0 * Math.Pow(t, 2.0) - Math.Pow(t, 4.0) - 14.0 * eta2 + 58.0 * Math.Pow(t, 2.0) * eta2) *
                                        Math.Pow(deltaLambda, 5.0) -
                                1.0 / 5040.0 * n * Math.Pow(CosLat1, 7.0) *
                                (-61.0 + 479.0 * Math.Pow(t, 2.0) - 179.0 * Math.Pow(t, 4.0) + Math.Pow(t, 6.0)) * Math.Pow(deltaLambda, 7.0)) * m0;
            return XY;
        }

        public void SetKei(int skei, int kei)
        {
            cSKei = skei;
            cKei = kei;

            if (kei == 1) BL0 = new BL(330000.0, 1293000.0);
            if (kei == 2) BL0 = new BL(330000.0, 1310000.0);
            if (kei == 3) BL0 = new BL(360000.0, 1321000.0);
            if (kei == 4) BL0 = new BL(330000.0, 1333000.0);
            if (kei == 5) BL0 = new BL(360000.0, 1342000.0);
            if (kei == 6) BL0 = new BL(360000.0, 1360000.0);
            if (kei == 7) BL0 = new BL(360000.0, 1371000.0);
            if (kei == 8) BL0 = new BL(360000.0, 1383000.0);
            if (kei == 9) BL0 = new BL(360000.0, 1395000.0);
            if (kei == 10) BL0 = new BL(400000.0, 1405000.0);
            if (kei == 11) BL0 = new BL(440000.0, 1401500.0);
            if (kei == 12) BL0 = new BL(440000.0, 1421500.0);
            if (kei == 13) BL0 = new BL(440000.0, 1441500.0);
            if (kei == 14) BL0 = new BL(260000.0, 1420000.0);
            if (kei == 15) BL0 = new BL(260000.0, 1273000.0);
            if (kei == 16) BL0 = new BL(260000.0, 1240000.0);
            if (kei == 17) BL0 = new BL(260000.0, 1310000.0);
            if (kei == 18) BL0 = new BL(200000.0, 1360000.0);
            if (kei == 19) BL0 = new BL(260000.0, 1540000.0);
            BL0.Lat = Deg2Rad0(BL0.Lat);
            BL0.Lng = Deg2Rad0(BL0.Lng);

            if (skei == SKEI_JPN)
            {
                a = 6377397.155;
                f = 1 / 299.152813;
            }
            else
            {
                a = 6378137.0;
                f = 1 / 298.257222101;
            }
            e = (2.0 / f - 1.0) * f * f;
            e1 = Math.Sqrt(2.0 * f - Math.Pow(f, 2.0));
            e2 = Math.Sqrt(2.0 * 1.0 / f - 1.0) / (1.0 / f - 1.0);

            double pA = 1.0 + 3.0 / 4.0 * Math.Pow(e1, 2.0) + 45.0 / 64.0 * Math.Pow(e1, 4.0) + 175.0 / 256.0 * Math.Pow(e1, 6.0) +
                                11025.0 / 16384.0 * Math.Pow(e1, 8.0) + 43659.0 / 65536.0 * Math.Pow(e1, 10.0) +
                                693693.0 / 1048576.0 * Math.Pow(e1, 12.0) + 19324305.0 / 29360128.0 * Math.Pow(e1, 14.0) +
                                4927697775.0 / 7516192768.0 * Math.Pow(e1, 16.0);
            double pB = 3.0 / 4.0 * Math.Pow(e1, 2.0) + 15.0 / 16.0 * Math.Pow(e1, 4.0) + 525.0 / 512.0 * Math.Pow(e1, 6.0) +
                                2205.0 / 2048.0 * Math.Pow(e1, 8.0) + 72765.0 / 65536.0 * Math.Pow(e1, 10.0) +
                                297297.0 / 262144.0 * Math.Pow(e1, 12.0) + 135270135.0 / 117440512.0 * Math.Pow(e1, 14.0) +
                                547521975.0 / 469762048.0 * Math.Pow(e1, 16.0);
            double pC = 15.0 / 64.0 * Math.Pow(e1, 4.0) + 105.0 / 256.0 * Math.Pow(e1, 6.0) + 2205.0 / 4096.0 * Math.Pow(e1, 8.0) +
                                10395.0 / 16384.0 * Math.Pow(e1, 10.0) + 1486485.0 / 2097152.0 * Math.Pow(e1, 12.0) +
                                45090045.0 / 58720256.0 * Math.Pow(e1, 14.0) + 766530765.0 / 939524096.0 * Math.Pow(e1, 16.0);
            double pD = 35.0 / 512.0 * Math.Pow(e1, 6.0) + 315.0 / 2048.0 * Math.Pow(e1, 8.0) +
                                31185.0 / 131072.0 * Math.Pow(e1, 10.0) + 165165.0 / 524288.0 * Math.Pow(e1, 12.0) +
                                45090045.0 / 117440512.0 * Math.Pow(e1, 14.0) + 209053845.0 / 469762048.0 * Math.Pow(e1, 16.0);
            double pE = 315.0 / 16384.0 * Math.Pow(e1, 8.0) + 3465.0 / 65536.0 * Math.Pow(e1, 10.0) +
                                99099.0 / 1048576.0 * Math.Pow(e1, 12.0) + 4099095.0 / 29360128.0 * Math.Pow(e1, 14.0) +
                                348423075.0 / 1879048192.0 * Math.Pow(e1, 16.0);
            double pF = 693.0 / 131072.0 * Math.Pow(e1, 10.0) + 9009.0 / 524288.0 * Math.Pow(e1, 12.0) +
                                4099095.0 / 117440512.0 * Math.Pow(e1, 14.0) + 26801775.0 / 469762048.0 * Math.Pow(e1, 16.0);
            double pG = 3003.0 / 2097152.0 * Math.Pow(e1, 12.0) + 315315.0 / 58720256.0 * Math.Pow(e1, 14.0) +
                                11486475.0 / 939524096.0 * Math.Pow(e1, 16.0);
            double pH = 45045.0 / 117440512.0 * Math.Pow(e1, 14.0) + 765765.0 / 469762048.0 * Math.Pow(e1, 16.0);
            double pI = 765765.0 / 7516192768.0 * Math.Pow(e1, 16.0);
            double bCoef = a * (1 - Math.Pow(e1, 2.0));

            b1 = bCoef * pA;
            b2 = bCoef * -pB / 2.0;
            b3 = bCoef * pC / 4.0;
            b4 = bCoef * -pD / 6.0;
            b5 = bCoef * pE / 8.0;
            b6 = bCoef * -pF / 10.0;
            b7 = bCoef * pG / 12.0;
            b8 = bCoef * -pH / 14.0;
            b9 = bCoef * pI / 16.0;

            s0 = b1 * BL0.Lat + b2 * Math.Sin(2.0 * BL0.Lat) +
                                b3 * Math.Sin(4.0 * BL0.Lat) + b4 * Math.Sin(6.0 * BL0.Lat) +
                                b5 * Math.Sin(8.0 * BL0.Lat) + b6 * Math.Sin(10.0 * BL0.Lat) +
                                b7 * Math.Sin(12.0 * BL0.Lat) + b8 * Math.Sin(14.0 * BL0.Lat) +
                                b9 * Math.Sin(16.0 * BL0.Lat);
        }

        double Deg2Rad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        double Rad2Deg(double rad)
        {
            return rad / Math.PI * 180.0;
        }

        double Deg2Rad0(double deg)
        {
            double fugou = deg < 0 ? -1 : 1;
            deg = Math.Abs(deg);
            double ang = Math.Floor(deg / 10000.0);
            double min = Math.Floor(deg / 100.0) - (ang * 100.0);
            double sec = deg - (ang * 10000.0) - (min * 100.0);
            double rad = fugou * (ang + (min + (sec / 60.0)) / 60.0);
            return rad * Math.PI / 180.0;
        }

        double Rad2Deg0(double rad)
        {
            double fugou = rad < 0 ? -1 : 1;
            rad = Math.Abs(rad) * 180.0 / Math.PI;
            double ang = Math.Floor(rad);
            rad = (rad - ang) * 60.0;
            double min = Math.Floor(rad);
            double sec = (rad - min) * 60.0;
            return fugou * (ang * 10000.0 + min * 100.0 + sec);
        }
    }
}
