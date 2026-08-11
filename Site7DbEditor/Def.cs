using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Site7DbEditor
{
    public static class Def
    {
        public static string iniFileName0 = @"C:\SITE7\GENBA\DATA\SITE7.ini";
        public static string iniFileName = @"C:\SITE7\GENBA\DATA\SITE7.ini";
        public static string appName = "SITE";
        public static string title0 = "遺跡調査システム";

        public static string genbaPath0 = @"C:\SITE7\GENBA\DATA\";

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
            return GetIniStr(iniFileName, app, key);
        }

        public static int GetIniInt(string app, string key, int defval)
        {
            string str = GetIniStr(app, key);
            if (string.IsNullOrEmpty(str)) return defval;
            if (int.TryParse(str, out int val)) return val;
            return defval;
        }

        public static double GetIniDouble(string app, string key, double defval)
        {
            string str = GetIniStr(app, key);
            if (string.IsNullOrEmpty(str)) return defval;
            if (double.TryParse(str, out double val)) return val;
            return defval;
        }

        public static void SetIniStr(string fname, string app, string key, string str)
        {
            try
            {
                if (string.IsNullOrEmpty(fname)) fname = iniFileName;
                string? dir = Path.GetDirectoryName(fname);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                WritePrivateProfileString(app, key, str ?? "", fname);
            }
            catch { }
        }

        public static void SetIniStr(string app, string key, string str)
        {
            SetIniStr(iniFileName, app, key, str);
        }

        public static void SetIniInt(string app, string key, int val)
        {
            SetIniStr(iniFileName, app, key, val.ToString());
        }

        public static void SetIniDouble(string app, string key, double val)
        {
            SetIniStr(iniFileName, app, key, val.ToString());
        }
    }
}
