using System;

namespace Site7DbEditor
{
    public static class Env
    {
        public const int TSGPS_TS = 0;
        public const int TSGPS_GPS = 1;

        public const int TS_LN100 = 0;
        public const int TS_PS = 1;
        public const int TS_DS = 2;
        public const int TS_OS = 3;
        public static string[] TSStrs = { "LN100/150", "PS-A GT100 MS(自動追尾)", "DS-AC PS-AC GT-500(自動視準)", "OS ES GM(手動視準)" };

        public const int Prism_0 = 0;
        public const int Prism_ATP2 = 1;
        public const int Prism_A7P = 2;
        public const int Prism_NonPri = 3;
        public static string[] PrismStrs = { "プリズム(0mm)", "360°プリズム ATP2(-7mm)", "360°プリズム A7P(-2mm)", "ノンプリズム(0mm)" };
        public static int[] PrismVals = { 0, -7, -2, 0 };

        public const int SokkyoMode_Seimitu = 0;
        public const int SokkyoMode_Kosoku = 1;
        public static string[] SokkyoModeStrs = { "精密", "高速" };

        public const int Tilt_HV = 0;
        public const int Tilt_None = 1;
        public const int Tilt_V = 2;
        public static string[] TiltStrs = { "Tilt補正(H,V)", "Tilt補正なし", "Tilt補正(Vのみ)" };

        public const int LightPat_LED = 0;
        public const int LightPat_Laser = 1;
        public static string[] LightPatStrs = { "ガイドライト・ＬＥＤ", "レーザー照準" };

        public const int LightVal_Dark = 0;
        public const int LightVal_Normal = 1;
        public const int LightVal_Bright = 2;
        public static string[] LightValStrs = { "暗い", "普通", "明るい" };

        public const int UseRC_No = 0;
        public const int UseRC_Yes = 1;
        public static string[] UseRCStrs = { "使用しない", "使用する" };

        public const int GuideLightPat_1 = 0;
        public const int GuideLightPat_2 = 1;
        public static string[] GuideLightPatStrs = { "パターン１", "パターン２" };

        public const int GPSHeight_2024 = 0;
        public const int GPSHeight_2011 = 1;
        public const int GPSHeight_WGS84 = 2;
        public static string[] GPSHeightStrs = { "平均海水面からの標高(ジオイド2024)", "平均海水面からの標高(ジオイド2011)", "WGS84楕円体からの高さ" };

        public const int i93IMU_No = 0;
        public const int i93IMU_Yes = 1;
        public static string[] i93IMUStrs = { "補正なし", "補正あり" };

        public const int PAPER_SIZE_A0 = 0;
        public const int PAPER_SIZE_A1 = 1;
        public const int PAPER_SIZE_A2 = 2;
        public const int PAPER_SIZE_A3 = 3;
        public const int PAPER_SIZE_A4 = 4;
        public const int PAPER_SIZE_A5 = 5;
        public static string[] PaperSizeStrs = {
            "A0横(1189mm x 841mm)",
            "A1横(841mm x 594mm)",
            "A2横(594mm x 420mm)",
            "A3横(420mm x 297mm)",
            "A4横(297mm x 210mm)",
            "A5横(210mm x 148mm)"
        };

        public const int PAPER_Scale_20 = 0;
        public const int PAPER_Scale_50 = 1;
        public const int PAPER_Scale_100 = 2;
        public const int PAPER_Scale_200 = 3;
        public const int PAPER_Scale_500 = 4;
        public const int PAPER_Scale_1000 = 5;
        public static string[] PaperScaleStrs = {
            "1 / 20",
            "1 / 50",
            "1 / 100",
            "1 / 200",
            "1 / 500",
            "1 / 1000"
        };

        public static int TSGPS = Def.GetIniInt("TS", "TSGPS", 0);
        public static int TS = Def.GetIniInt("TS", "TS", TS_PS);
        public static string ComPortTS = Def.GetIniStr("TS", "ComPortTS");
        public static string ComPortGPS = Def.GetIniStr("TS", "ComPortGPS");

        public static int Prism = Def.GetIniInt("TS", "Prism", Prism_ATP2);
        public static int PrismVal = Def.GetIniInt("TS", "PrismVal", PrismVals[Prism_ATP2]);
        public static int SokkyoMode = Def.GetIniInt("TS", "SokkyoMode", SokkyoMode_Seimitu);
        public static int Tilt = Def.GetIniInt("TS", "Tilt", Tilt_HV);
        public static int LightPat = Def.GetIniInt("TS", "LightPat", LightPat_LED);
        public static int LightVal = Def.GetIniInt("TS", "LightVal", LightVal_Normal);
        public static int SearchH = Def.GetIniInt("TS", "SearchH", 15);
        public static int SearchV = Def.GetIniInt("TS", "SearchV", 15);
        public static int UseRC = Def.GetIniInt("TS", "UseRC", UseRC_No);
        public static int GuideLightPat = Def.GetIniInt("TS", "GuideLightPat", GuideLightPat_1);
        public static int GuideLightVal = Def.GetIniInt("TS", "GuideLightVal", LightVal_Normal);
        public static int GPSHeight = Def.GetIniInt("TS", "GPSHeight", GPSHeight_2011);
        public static int GPSCount = Def.GetIniInt("TS", "GPSCount", 10);
        public static int i93IMU = Def.GetIniInt("TS", "i93IMU", i93IMU_Yes);

        public static int TSMode = getTSMode(); //getTSMode()で初期化する。TSの設定によって変わるため、Defから直接読み込まない。
        public static int curTSMode0 = TSMode;  //実行中ユーザによって変更した値(自動追尾/自動視準/視準のみ/測定)
        public static int curTSMode = TSMode;   //実行中状態によりて変わる値(自動追尾->測定、視準のみ→測定など)

        public static int PaperSize = Def.GetIniInt("PAPER", "SIZE", PAPER_SIZE_A3);
        public static int PaperScale = Def.GetIniInt("PAPER", "SCALE", PAPER_Scale_200);
        public static int PaperAng = Def.GetIniInt("PAPER", "ANG", 0);

        public static void SaveEnvVal()
        {
            Def.SetIniInt("TS", "Prism", Prism);
            Def.SetIniInt("TS", "PrismVal", PrismVal);
            Def.SetIniInt("TS", "SokkyoMode", SokkyoMode);
            Def.SetIniInt("TS", "Tilt", Tilt);
            Def.SetIniInt("TS", "LightPat", LightPat);
            Def.SetIniInt("TS", "LightVal", LightVal);
            Def.SetIniInt("TS", "SearchH", SearchH);
            Def.SetIniInt("TS", "SearchV", SearchV);
            Def.SetIniInt("TS", "UseRC", UseRC);
            Def.SetIniInt("TS", "GuideLightPat", GuideLightPat);
            Def.SetIniInt("TS", "GuideLightVal", GuideLightVal);
            Def.SetIniInt("TS", "GPSHeight", GPSHeight);
            Def.SetIniInt("TS", "GPSCount", GPSCount);
            Def.SetIniInt("TS", "i93IMU", i93IMU);

            Def.SetIniInt("PAPER", "SIZE", PaperSize);
            Def.SetIniInt("PAPER", "SCALE", PaperScale);
            Def.SetIniInt("PAPER", "ANG", PaperAng);
        }

        public const int GPS_POCHISTA = 0;
        public const int GPS_i93 = 1;
        public static string[] GPSStrs = { "ポチスタ", "i93" };

        public static int GPS = Def.GetIniInt("TS", "GPS", GPS_i93);
        public static int KeiNum = Def.GetIniInt("TS", "KeiNum", 5);
        public static int GPSStatus = Def.GetIniInt("TS", "GPSStatus", 0);

        public const int TS_MODE_TUIBI = 3;
        public const int TS_MODE_SHIJUNSOKUTEI = 2;
        public const int TS_MODE_SHIJUN = 1;
        public const int TS_MODE_SOKUTEI = 0;

        public static string[] TS_STR = { "測定", "視準のみ", "視準測定", "自動追尾" };

        public static bool isUseLN100() => (TS == TS_LN100);

        public static int getTSMode()
        {
            if (TS == TS_LN100 || TS == TS_PS) return TS_MODE_TUIBI;
            if (TS == TS_DS) return TS_MODE_SHIJUNSOKUTEI;
            return TS_MODE_SOKUTEI;
        }

        public static string getCurTSModeStr()
        {
            int mode = getTSMode();
            if (mode >= 0 && mode < TS_STR.Length) return TS_STR[mode];
            return "自動追尾";
        }

        public static string getTSModeStr(int i)
        {
            if (i >= 0 && i < TS_STR.Length) return TS_STR[i];
            return "自動追尾";
        }

        public static void SaveEnvTS()
        {
            Def.SetIniInt("TS", "TSGPS", TSGPS);
            Def.SetIniInt("TS", "TS", TS);
            Def.SetIniStr("TS", "ComPortTS", ComPortTS ?? "");
        }

        public static void SaveEnvGPS()
        {
            Def.SetIniInt("TS", "TSGPS", TSGPS);
            Def.SetIniInt("TS", "KeiNum", KeiNum);
            Def.SetIniInt("TS", "GPS", GPS);
            Def.SetIniStr("TS", "ComPortGPS", ComPortGPS ?? "");
            Def.SetIniInt("TS", "GPSStatus", GPSStatus);
        }
    }
}
