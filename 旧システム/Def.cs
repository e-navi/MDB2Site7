using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Site7 {
    public static class Def {
        static public string iniFileName0;
        static public string iniFileName;
        static public bool IsGaigyo = true;
        static public string appName = "SITE";
        static public string title0 = "遺跡調査システム";
        static public string title;

        static public string genbaPathNew = "C:/SITE7/GENBA/NEW/";
        static public string genbaPath0 = "C:/SITE7/GENBA/DATA/";
        static public string genbaName = "外業現場3";
        static public string genbaPath = genbaPath0 + "外業現場3";

        [DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            uint nSize,
            string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern bool WritePrivateProfileString(
            string lpAppName,
           string lpKeyName,
            string lpString,
            string lpFileName);

        public static string GetIniStr(string fname, string app, string key) {
            var sb = new StringBuilder(1024);
            GetPrivateProfileString(app, key, "", sb, Convert.ToUInt32(sb.Capacity), fname);
            return sb.ToString();
        }
        public static string GetIniStr(string app, string key) {
            string str = GetIniStr(iniFileName, app, key);
            if (str == null) str = "";
            return str;
        }

        //public static string GetIniStr(string key) {
            //return GetIniStr(iniFileName, appName, key);
        //}
        public static int GetIniInt(string app, string key, int defval) {
            string str = GetIniStr(app, key);
            if (str == null) return defval;
            if (int.TryParse(str, out int val)) return val;
            return defval;
        }
        public static double GetIniDouble(string app, string key, double defval) {
            string str = GetIniStr(app, key);
            if (str == null) return 0.0;
            if (double.TryParse(str, out double val)) return val;
            return 0.0;
        }
        public static void SetIniStr(string fname, string app, string key, string str) {
            WritePrivateProfileString(app, key, str, fname);
        }
        public static void SetIniStr(string app, string key, string str) {
            SetIniStr(iniFileName, app, key, str);
        }
        public static void SetIniInt(string app, string key, int val) {
            SetIniStr(iniFileName, app, key, val.ToString());
        }
        public static void SetIniDouble(string app, string key, double val) {
            SetIniStr(iniFileName, app, key, val.ToString());
        }

        static public string GetDbPath() { return Path.Combine(genbaPath, "SITE7.db3"); }
        static public string GetThumbnailPath() { return Path.Combine(genbaPath, "SITE7.png"); }
        static public string GetDefPath() { return Path.Combine(genbaPath, "Def"); }
        static public string GetColorDefPath() { return Path.Combine(GetDefPath(), "Color.txt"); }
        static public string GetIkouDefPath() { return Path.Combine(GetDefPath(), "遺構.txt"); }
        static public string GetIkouLineDefPath() { return Path.Combine(GetDefPath(), "遺構線.txt"); }
        static public string GetIbutuKindDefPath() { return Path.Combine(GetDefPath(), "遺物_種別.txt"); }
        static public string GetIbutuSouiDefPath() { return Path.Combine(GetDefPath(), "遺物_層位.txt"); }
        static public string GetIbutuTikuDefPath() { return Path.Combine(GetDefPath(), "遺物_地区.txt"); }


        public static void LoadDef(string fname) {
            iniFileName0 = fname;

            title0 = GetIniStr(iniFileName0, appName, "title0");
            if (title0 == "") title0 = "遺跡調査システム";

            genbaPathNew = GetIniStr(iniFileName0, appName, "genbaPathNew");
            if (genbaPathNew == "") {
                genbaPathNew = @"C:\SITE7\GENBA\NEW\";
                SetIniStr(iniFileName0, appName, "genbaPathNew", genbaPathNew);
            }
            genbaPath0 = GetIniStr(iniFileName0, appName, "genbaPath0");
            if (genbaPath0 == "") {
                genbaPath0 = @"C:\SITE7\GENBA\DATA\";
                SetIniStr(iniFileName0, appName, "genbaPath0", genbaPath0);
            }
            string name = GetIniStr(iniFileName0, appName, "genbaName");
            SetGenbaName(name);
        }
        static public void SetGenbaPath0(string path) {
            genbaPath0 = path;
        }
        static public string GetGenbaPath() {
            return genbaPath;
        }
        static public void SetGenbaName(string name) {
            genbaName = name;
            SetIniStr(iniFileName0, appName, "genbaName", genbaName);
            genbaPath = Path.Combine(genbaPath0, name);
            iniFileName = Path.Combine(genbaPath, "SITE7.ini");
        }
        static public string GetGenbaName() {
            return genbaName;
        }
        /*
        static public string GetComPort(string name) {
            string comPort = GetIniStr(iniFileName, "TS", "comPort");
            return comPort;
        }
        static public void SetComPort(string name, string comPort) {
            SetIniStr(iniFileName, "TS", "comPort", comPort);
        }
        */

    }
}
