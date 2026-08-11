using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using FirebirdSql.Data.FirebirdClient;

namespace MdbFdbExporter
{
    public enum SplitRule
    {
        JapaneseSuffix,
        FeatureNumberEnd,
        LastHyphen,
        LastUnderscore,
        CustomRegex,
        DelimiterList,
        NoSplit
    }

    public class GroupPointData
    {
        public string GroupName { get; set; } = "";
        public string PointNo { get; set; } = "";
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

    public static class DbHelper
    {
        private const string JetProvider = "Microsoft.Jet.OLEDB.4.0";

        public static string GetMdbConnectionString(string mdbPath)
        {
            return $"Provider={JetProvider};Data Source={mdbPath};";
        }

        public static string GetFdbConnectionString(string fdbPath)
        {
            var builder = new FbConnectionStringBuilder();
            builder.Database = fdbPath;
            builder.UserID = "SYSDBA";
            builder.Password = "masterkey";
            builder.ServerType = FbServerType.Embedded;
            builder.ClientLibrary = "fbembed.dll";
            builder.Dialect = 3;
            builder.Charset = "NONE"; // NONE or SJIS for Windows Japanese environments
            return builder.ConnectionString;
        }

        // Test connection to Access MDB and get basic info
        public static (bool success, string message, int rowCount) GetMdbInfo(string mdbPath, string tableName)
        {
            try
            {
                using (var conn = new OleDbConnection(GetMdbConnectionString(mdbPath)))
                {
                    conn.Open();
                    
                    var schemaTable = conn.GetSchema("Tables");
                    if (schemaTable == null)
                    {
                        return (false, "Could not retrieve tables schema.", 0);
                    }
                    bool tableExists = false;
                    foreach (DataRow row in schemaTable.Rows)
                    {
                        var tName = row["TABLE_NAME"]?.ToString();
                        if (tName != null && tName.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                        {
                            tableExists = true;
                            break;
                        }
                    }

                    if (!tableExists)
                    {
                        return (false, $"Table '{tableName}' not found in database.", 0);
                    }

                    int totalCount = 0;
                    int activeCount = 0;
                    using (var cmd = new OleDbCommand($"SELECT COUNT(*) FROM [{tableName}]", conn))
                    {
                        totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    try
                    {
                        using (var cmd = new OleDbCommand($"SELECT COUNT(*) FROM [{tableName}] WHERE [DELETEFLG] = False AND [INVISIBLE] = False", conn))
                        {
                            activeCount = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    catch
                    {
                        activeCount = totalCount;
                    }

                    return (true, $"Success: Total rows = {totalCount}, Active rows = {activeCount}", activeCount);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, 0);
            }
        }

        // Test connection to Firebird FDB and get basic info
        public static (bool success, string message, int rowCount) GetFdbInfo(string fdbPath, string tableName)
        {
            try
            {
                using (var conn = new FbConnection(GetFdbConnectionString(fdbPath)))
                {
                    conn.Open();

                    var schemaTable = conn.GetSchema("Tables");
                    if (schemaTable == null)
                    {
                        return (false, "Could not retrieve tables schema.", 0);
                    }
                    bool tableExists = false;
                    foreach (DataRow row in schemaTable.Rows)
                    {
                        var tName = row["TABLE_NAME"]?.ToString();
                        if (tName != null && tName.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                        {
                            tableExists = true;
                            break;
                        }
                    }

                    if (!tableExists)
                    {
                        return (false, $"Table '{tableName}' not found in database.", 0);
                    }

                    int totalCount = 0;
                    int activeCount = 0;
                    using (var cmd = new FbCommand($"SELECT COUNT(*) FROM \"{tableName}\"", conn))
                    {
                        totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    try
                    {
                        string query;
                        if (tableName.Equals("IBUTU_HAND_V", StringComparison.OrdinalIgnoreCase))
                        {
                            query = "SELECT COUNT(*) FROM \"IBUTU_HAND_V\" WHERE \"DELETEFLG\" = 0 AND \"INVISIBLE\" = 0";
                        }
                        else if (tableName.Equals("IKOU_HAND_V", StringComparison.OrdinalIgnoreCase))
                        {
                            query = "SELECT COUNT(*) FROM \"IKOU_HAND_V\" v INNER JOIN \"IKOU_HAND_G\" g ON v.\"IKOU_G_ID\" = g.\"ID\" WHERE g.\"GDELETEFLG\" = 0 AND g.\"GINVISIBLE\" = 0";
                        }
                        else
                        {
                            query = $"SELECT COUNT(*) FROM \"{tableName}\"";
                        }

                        using (var cmd = new FbCommand(query, conn))
                        {
                            activeCount = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    catch
                    {
                        activeCount = totalCount;
                    }

                    return (true, $"Success: Total rows = {totalCount}, Active rows = {activeCount}", activeCount);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, 0);
            }
        }

        // Extract unique group names from both Access MDB and Firebird FDB
        public static List<string> ExtractUniqueGroupNames(string mdbIkouPath, string fdbPath)
        {
            var groups = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (!string.IsNullOrEmpty(mdbIkouPath) && File.Exists(mdbIkouPath))
            {
                try
                {
                    using (var conn = new OleDbConnection(GetMdbConnectionString(mdbIkouPath)))
                    {
                        conn.Open();
                        using (var cmd = new OleDbCommand("SELECT DISTINCT [Group] FROM [IKOU] WHERE [DELETEFLG] = False AND [INVISIBLE] = False AND [Group] IS NOT NULL", conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string val = reader[0]?.ToString()?.Trim() ?? "";
                                if (!string.IsNullOrEmpty(val))
                                    groups.Add(val);
                            }
                        }
                    }
                }
                catch {}
            }

            if (!string.IsNullOrEmpty(fdbPath) && File.Exists(fdbPath))
            {
                try
                {
                    using (var conn = new FbConnection(GetFdbConnectionString(fdbPath)))
                    {
                        conn.Open();
                        using (var cmd = new FbCommand("SELECT DISTINCT \"IKOUNAME\" FROM \"IKOU_HAND_G\" WHERE \"GDELETEFLG\" = 0 AND \"GINVISIBLE\" = 0 AND \"IKOUNAME\" IS NOT NULL", conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string val = reader[0]?.ToString()?.Trim() ?? "";
                                if (!string.IsNullOrEmpty(val))
                                    groups.Add(val);
                            }
                        }
                    }
                }
                catch {}
            }

            var list = new List<string>(groups);
            list.Sort();
            return list;
        }

        // Extract point coordinate data from Access MDB and Firebird FDB
        public static List<GroupPointData> ExtractGroupPointData(string mdbIkouPath, string fdbPath)
        {
            var list = new List<GroupPointData>();

            if (!string.IsNullOrEmpty(mdbIkouPath) && File.Exists(mdbIkouPath))
            {
                try
                {
                    using (var conn = new OleDbConnection(GetMdbConnectionString(mdbIkouPath)))
                    {
                        conn.Open();
                        using (var cmd = new OleDbCommand("SELECT [Group], [No], [X], [Y], [Z] FROM [IKOU] WHERE [DELETEFLG] = False AND [INVISIBLE] = False AND [Group] IS NOT NULL ORDER BY [ID]", conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string grp = reader[0]?.ToString()?.Trim() ?? "";
                                string no = reader[1]?.ToString()?.Trim() ?? "";
                                double.TryParse(reader[2]?.ToString(), out double x);
                                double.TryParse(reader[3]?.ToString(), out double y);
                                double.TryParse(reader[4]?.ToString(), out double z);

                                if (!string.IsNullOrEmpty(grp))
                                {
                                    list.Add(new GroupPointData { GroupName = grp, PointNo = no, X = x, Y = y, Z = z });
                                }
                            }
                        }
                    }
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(fdbPath) && File.Exists(fdbPath))
            {
                try
                {
                    using (var conn = new FbConnection(GetFdbConnectionString(fdbPath)))
                    {
                        conn.Open();
                        string query = @"
                            SELECT g.""IKOUNAME"", v.""ORGPNO"", v.""X"", v.""Y"", v.""Z""
                            FROM ""IKOU_HAND_V"" v
                            INNER JOIN ""IKOU_HAND_G"" g ON v.""IKOU_G_ID"" = g.""ID""
                            WHERE g.""GDELETEFLG"" = 0 AND g.""GINVISIBLE"" = 0
                            ORDER BY v.""IKOU_G_ID"", v.""SUBID"", v.""SUBSUBID""";

                        using (var cmd = new FbCommand(query, conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string grp = reader[0]?.ToString()?.Trim() ?? "";
                                string no = reader[1]?.ToString()?.Trim() ?? "";
                                double.TryParse(reader[2]?.ToString(), out double x);
                                double.TryParse(reader[3]?.ToString(), out double y);
                                double.TryParse(reader[4]?.ToString(), out double z);

                                if (!string.IsNullOrEmpty(grp))
                                {
                                    list.Add(new GroupPointData { GroupName = grp, PointNo = no, X = x, Y = y, Z = z });
                                }
                            }
                        }
                    }
                }
                catch { }
            }

            return list;
        }

        public static string ToHalfWidth(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            char[] chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (c >= '０' && c <= '９')
                {
                    chars[i] = (char)(c - '０' + '0');
                }
                else if (c >= 'Ａ' && c <= 'Ｚ')
                {
                    chars[i] = (char)(c - 'Ａ' + 'A');
                }
                else if (c >= 'ａ' && c <= 'ｚ')
                {
                    chars[i] = (char)(c - 'ａ' + 'a');
                }
                else if (c == '　')
                {
                    chars[i] = ' ';
                }
                else if (c == '－')
                {
                    chars[i] = '-';
                }
            }
            return new string(chars);
        }

        // Splitting Logic
        public static (string ikou, string ikouLine) SplitGroupName(string groupName, SplitRule rule, string customRegexPattern = "")
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                return ("", "");
            }

            groupName = ToHalfWidth(groupName).Trim();

            switch (rule)
            {
                case SplitRule.JapaneseSuffix:
                    string[] lineKeywords = { "上端", "下端", "中端", "底面", "断面", "法面", "肩", "底" };
                    foreach (var keyword in lineKeywords)
                    {
                        if (groupName.EndsWith(keyword))
                        {
                            string prefix = groupName.Substring(0, groupName.Length - keyword.Length).TrimEnd('_', '-').Trim();
                            return (prefix, keyword);
                        }
                    }
                    return (groupName, "");

                case SplitRule.FeatureNumberEnd:
                    var firstNumMatch = Regex.Match(groupName, @"^(?<ikou>\D*\d+)[-_]?(?<ikouline>.*)$");
                    if (firstNumMatch.Success)
                    {
                        string ikou = firstNumMatch.Groups["ikou"].Value.Trim();
                        string line = firstNumMatch.Groups["ikouline"].Value.Trim();
                        return (ikou, line);
                    }
                    return (groupName, "");

                case SplitRule.LastHyphen:
                    int lastHyphen = groupName.LastIndexOf('-');
                    if (lastHyphen > 0 && lastHyphen < groupName.Length - 1)
                    {
                        string prefix = groupName.Substring(0, lastHyphen).Trim();
                        string suffix = groupName.Substring(lastHyphen + 1).Trim();
                        return (prefix, suffix);
                    }
                    return (groupName, "");

                case SplitRule.LastUnderscore:
                    int lastUnderscore = groupName.LastIndexOf('_');
                    if (lastUnderscore > 0 && lastUnderscore < groupName.Length - 1)
                    {
                        string prefix = groupName.Substring(0, lastUnderscore).Trim();
                        string suffix = groupName.Substring(lastUnderscore + 1).Trim();
                        return (prefix, suffix);
                    }
                    return (groupName, "");

                case SplitRule.CustomRegex:
                    if (!string.IsNullOrEmpty(customRegexPattern))
                    {
                        try
                        {
                            var match = Regex.Match(groupName, customRegexPattern);
                            if (match.Success)
                            {
                                string ikou = match.Groups["ikou"].Success ? match.Groups["ikou"].Value : "";
                                string line = match.Groups["ikouline"].Success ? match.Groups["ikouline"].Value : "";

                                if (string.IsNullOrEmpty(ikou) && string.IsNullOrEmpty(line))
                                {
                                    if (match.Groups.Count > 1) ikou = match.Groups[1].Value;
                                    if (match.Groups.Count > 2) line = match.Groups[2].Value;
                                }

                                return (ikou.Trim(), line.Trim());
                            }
                        }
                        catch
                        {
                            // regex compilation or match error
                        }
                    }
                    return (groupName, "");

                case SplitRule.DelimiterList:
                    if (!string.IsNullOrEmpty(customRegexPattern))
                    {
                        var delimiters = customRegexPattern
                            .Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(d => d.Trim())
                            .Where(d => !string.IsNullOrEmpty(d))
                            .ToList();

                        var normalizedDelimiters = delimiters.Select(d => ToHalfWidth(d)).ToList();

                        // 1. Find all matches of all delimiters
                        var matches = new List<(int startIndex, int length, string value)>();
                        foreach (var delim in normalizedDelimiters)
                        {
                            int idx = groupName.IndexOf(delim, StringComparison.OrdinalIgnoreCase);
                            while (idx != -1)
                            {
                                matches.Add((idx, delim.Length, delim));
                                idx = groupName.IndexOf(delim, idx + 1, StringComparison.OrdinalIgnoreCase);
                            }
                        }

                        // 2. Filter out matches that are sub-segments of longer matches
                        var sortedMatches = matches.OrderByDescending(m => m.length).ToList();
                        var covered = new bool[groupName.Length];
                        var validMatches = new List<(int startIndex, int length, string value)>();

                        foreach (var m in sortedMatches)
                        {
                            bool hasOverlap = false;
                            for (int i = 0; i < m.length; i++)
                            {
                                if (m.startIndex + i < covered.Length && covered[m.startIndex + i])
                                {
                                    hasOverlap = true;
                                    break;
                                }
                            }

                            if (!hasOverlap)
                            {
                                for (int i = 0; i < m.length; i++)
                                {
                                    if (m.startIndex + i < covered.Length)
                                    {
                                        covered[m.startIndex + i] = true;
                                    }
                                }
                                validMatches.Add(m);
                            }
                        }

                        // 3. Find the rightmost valid match that is not at the start of the string (index > 0)
                        var rightmostMatch = validMatches
                            .Where(m => m.startIndex > 0)
                            .OrderByDescending(m => m.startIndex)
                            .FirstOrDefault();

                        if (rightmostMatch.length > 0)
                        {
                            string prefix = groupName.Substring(0, rightmostMatch.startIndex).TrimEnd('-', '_', ' ').Trim();
                            string suffix = groupName.Substring(rightmostMatch.startIndex).Trim();
                            return (prefix, suffix);
                        }
                    }
                    return (groupName, "");

                case SplitRule.NoSplit:
                default:
                    return (groupName, "");
            }
        }

        public static (string ikou, string ikouLine) SplitGroupNameChain(
            string groupName,
            SplitRule rule1, string pattern1,
            SplitRule rule2, string pattern2,
            SplitRule rule3, string pattern3)
        {
            var res1 = SplitGroupName(groupName, rule1, pattern1);
            if (!string.IsNullOrEmpty(res1.ikouLine))
            {
                return res1;
            }

            if (rule2 != SplitRule.NoSplit)
            {
                var res2 = SplitGroupName(groupName, rule2, pattern2);
                if (!string.IsNullOrEmpty(res2.ikouLine))
                {
                    return res2;
                }
            }

            if (rule3 != SplitRule.NoSplit)
            {
                var res3 = SplitGroupName(groupName, rule3, pattern3);
                if (!string.IsNullOrEmpty(res3.ikouLine))
                {
                    return res3;
                }
            }

            return res1;
        }

        // Export data from Access MDB
        public static DataTable ExportMdb(
            string mdbPath, string tableName,
            SplitRule rule1, string pattern1,
            SplitRule rule2, string pattern2,
            SplitRule rule3, string pattern3,
            Action<int>? progressCallback)
        {
            var dt = new DataTable();
            string connStr = GetMdbConnectionString(mdbPath);
            string query = $"SELECT * FROM [{tableName}] WHERE [DELETEFLG] = False AND [INVISIBLE] = False ORDER BY [ID]";

            using (var conn = new OleDbConnection(connStr))
            {
                conn.Open();
                using (var cmd = new OleDbCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            // If table is IKOU, split the Group column into IKOU and IKOULINE
            if (tableName.Equals("IKOU", StringComparison.OrdinalIgnoreCase))
            {
                dt.Columns.Add("IKOU", typeof(string));
                dt.Columns.Add("IKOULINE", typeof(string));

                // Position new columns next to "Group" for easier reading
                int groupIndex = dt.Columns.IndexOf("Group");
                if (groupIndex >= 0)
                {
                    dt.Columns["IKOU"]!.SetOrdinal(groupIndex + 1);
                    dt.Columns["IKOULINE"]!.SetOrdinal(groupIndex + 2);
                }

                foreach (DataRow row in dt.Rows)
                {
                    string groupVal = row["Group"]?.ToString() ?? "";
                    var splitResult = SplitGroupNameChain(groupVal, rule1, pattern1, rule2, pattern2, rule3, pattern3);
                    row["IKOU"] = splitResult.ikou;
                    row["IKOULINE"] = splitResult.ikouLine;
                }
            }

            progressCallback?.Invoke(dt.Rows.Count);
            return dt;
        }

        // Export data from Firebird FDB
        public static DataTable ExportFdb(
            string fdbPath, string tableName,
            SplitRule rule1, string pattern1,
            SplitRule rule2, string pattern2,
            SplitRule rule3, string pattern3,
            Action<int>? progressCallback)
        {
            var dt = new DataTable();
            string connStr = GetFdbConnectionString(fdbPath);
            string query;

            if (tableName.Equals("IBUTU_HAND_V", StringComparison.OrdinalIgnoreCase))
            {
                query = "SELECT * FROM \"IBUTU_HAND_V\" WHERE \"DELETEFLG\" = 0 AND \"INVISIBLE\" = 0 ORDER BY \"ID\"";
            }
            else if (tableName.Equals("IKOU_HAND_V", StringComparison.OrdinalIgnoreCase))
            {
                query = @"
                    SELECT 
                        v.*, 
                        g.""IKOUNAME"" AS ""GROUP_NAME"", 
                        g.""GROUPNO"" AS ""GROUP_NO"", 
                        g.""CHIKU"" AS ""GROUP_CHIKU"", 
                        g.""SOUI"" AS ""GROUP_SOUI"", 
                        g.""EX1"" AS ""GROUP_EX1"", 
                        g.""EX2"" AS ""GROUP_EX2""
                    FROM ""IKOU_HAND_V"" v
                    INNER JOIN ""IKOU_HAND_G"" g ON v.""IKOU_G_ID"" = g.""ID""
                    WHERE g.""GDELETEFLG"" = 0 AND g.""GINVISIBLE"" = 0
                    ORDER BY v.""IKOU_G_ID"", v.""SUBID"", v.""SUBSUBID""";
            }
            else
            {
                query = $"SELECT * FROM \"{tableName}\"";
            }

            using (var conn = new FbConnection(connStr))
            {
                conn.Open();
                using (var cmd = new FbCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            // If table is IKOU_HAND_V, split the GROUP_NAME column into IKOU and IKOULINE
            if (tableName.Equals("IKOU_HAND_V", StringComparison.OrdinalIgnoreCase))
            {
                dt.Columns.Add("IKOU", typeof(string));
                dt.Columns.Add("IKOULINE", typeof(string));

                int groupIndex = dt.Columns.IndexOf("GROUP_NAME");
                if (groupIndex >= 0)
                {
                    dt.Columns["IKOU"]!.SetOrdinal(groupIndex + 1);
                    dt.Columns["IKOULINE"]!.SetOrdinal(groupIndex + 2);
                }

                foreach (DataRow row in dt.Rows)
                {
                    string groupVal = row["GROUP_NAME"]?.ToString() ?? "";
                    var splitResult = SplitGroupNameChain(groupVal, rule1, pattern1, rule2, pattern2, rule3, pattern3);
                    row["IKOU"] = splitResult.ikou;
                    row["IKOULINE"] = splitResult.ikouLine;
                }
            }

            progressCallback?.Invoke(dt.Rows.Count);
            return dt;
        }
    }
}
