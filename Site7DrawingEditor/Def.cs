using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Site7DrawingEditor
{
    public static class Def
    {
        public static string iniFileName0 = @"C:\SITE7\GENBA\DATA\SITE7.ini";
        public static string iniFileName = @"C:\SITE7\GENBA\DATA\SITE7.ini";

        public static string GetSystemIniFileName()
        {
            string p1 = @"C:\SITE7\GENBA\NEW\SITE7.ini";
            if (File.Exists(p1)) return p1;
            string p2 = @"C:\SITE7\SITE7.ini";
            if (File.Exists(p2)) return p2;
            string p3 = @"C:\SITE7\GENBA\DATA\SITE7.ini";
            if (File.Exists(p3)) return p3;
            return p1;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            uint nSize,
            string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        public static string GetIniStr(string fname, string app, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(fname)) fname = iniFileName;
                var sb = new StringBuilder(1024);
                GetPrivateProfileString(app, key, "", sb, (uint)sb.Capacity, fname);
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string GetIniStr(string app, string key)
        {
            string sysIni = GetSystemIniFileName();
            string v = GetIniStr(sysIni, app, key);
            if (!string.IsNullOrEmpty(v)) return v;
            return GetIniStr(iniFileName, app, key);
        }

        public static void SetIniStr(string fname, string app, string key, string val)
        {
            try
            {
                if (string.IsNullOrEmpty(fname)) fname = iniFileName;
                WritePrivateProfileString(app, key, val, fname);
            }
            catch { }
        }

        public static void SetIniStr(string app, string key, string val)
        {
            string sysIni = GetSystemIniFileName();
            SetIniStr(sysIni, app, key, val);
            SetIniStr(iniFileName, app, key, val);
        }
    }
}
