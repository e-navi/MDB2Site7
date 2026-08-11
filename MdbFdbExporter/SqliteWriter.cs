using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;
using FirebirdSql.Data.FirebirdClient;

namespace MdbFdbExporter
{
    public static class SqliteWriter
    {
        public static string FindEmptyDbTemplate(string outFolder, string rootDbFolder)
        {
            // 1. Check C:\SITE7\GENBA\DATA
            string defaultFolder = @"C:\SITE7\GENBA\DATA";
            string path0 = Path.Combine(defaultFolder, "Site7.db3");
            if (File.Exists(path0)) return path0;

            // 2. Check directly in outFolder
            string path1 = Path.Combine(outFolder, "Site7.db3");
            if (File.Exists(path1)) return path1;

            // 3. Check in rootDbFolder / ExportedSite7
            string path2 = Path.Combine(rootDbFolder, "ExportedSite7", "Site7.db3");
            if (File.Exists(path2)) return path2;

            // 4. Default fallback path
            if (!Directory.Exists(defaultFolder))
            {
                try { Directory.CreateDirectory(defaultFolder); } catch { }
            }
            return Path.Combine(defaultFolder, "Site7.db3");
        }

        public static (bool success, string message, string outputDbPath) ExportToSite7Sqlite(
            string activeDbFolder,
            string outFolder,
            string rootDbFolder,
            bool isSite5,
            List<GroupPointData> pointData,
            SplitRule rule1, string pattern1,
            SplitRule rule2, string pattern2,
            SplitRule rule3, string pattern3,
            Action<string> log)
        {
            try
            {
                string activeFolderName = Path.GetFileName(activeDbFolder.TrimEnd('\\', '/'));
                if (string.IsNullOrEmpty(activeFolderName) || activeFolderName == ".")
                {
                    activeFolderName = "Site7_Export";
                }

                string targetSubFolder = Path.Combine(outFolder, activeFolderName);
                if (!Directory.Exists(targetSubFolder))
                {
                    Directory.CreateDirectory(targetSubFolder);
                    log($"Created target export directory: {targetSubFolder}");
                }

                string templatePath = FindEmptyDbTemplate(outFolder, rootDbFolder);
                string destDbPath = Path.Combine(targetSubFolder, "Site7.db3");

                if (File.Exists(templatePath))
                {
                    log($"Copying empty SQLite template '{Path.GetFileName(templatePath)}' to '{destDbPath}'...");
                    File.Copy(templatePath, destDbPath, overwrite: true);
                }
                else
                {
                    log($"[WARNING] Template DB not found at '{templatePath}'. Creating fresh SQLite DB at '{destDbPath}'...");
                }

                string connStr = $"Data Source={destDbPath};";

                using (var conn = new SqliteConnection(connStr))
                {
                    conn.Open();

                    // Ensure Site7 Schema Tables exist
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            CREATE TABLE IF NOT EXISTS '遺構' (
                                ID INTEGER PRIMARY KEY,
                                NAME TEXT,
                                X REAL,
                                Y REAL,
                                Z REAL,
                                DATE TEXT
                            );
                            CREATE TABLE IF NOT EXISTS '遺構L' (
                                ID INTEGER,
                                LID INTEGER,
                                NAME TEXT,
                                MODE INTEGER,
                                X REAL,
                                Y REAL,
                                Z REAL,
                                LAYER INTEGER,
                                DATE TEXT,
                                PRECS TEXT,
                                PRIMARY KEY(ID, LID)
                            );
                            CREATE TABLE IF NOT EXISTS '遺物' (
                                ID INTEGER PRIMARY KEY,
                                CHIKU TEXT,
                                SOUI TEXT,
                                SYUBETU TEXT,
                                No INTEGER,
                                X REAL,
                                Y REAL,
                                Z REAL,
                                LAYER INTEGER,
                                DATE TEXT,
                                S REAL,
                                V REAL,
                                H REAL,
                                KPNAME TEXT,
                                BPNAME TEXT,
                                KPH REAL,
                                MRH REAL
                            );
                            CREATE TABLE IF NOT EXISTS '基準点' (
                                ID INTEGER PRIMARY KEY,
                                NAME TEXT,
                                X REAL,
                                Y REAL,
                                Z REAL,
                                LAYER INTEGER,
                                DATE TEXT,
                                S REAL,
                                V REAL,
                                H REAL,
                                KPNAME TEXT,
                                BPNAME TEXT,
                                KPH REAL,
                                MRH REAL
                            );
                            CREATE TABLE IF NOT EXISTS 'LAYER' (
                                ID INTEGER PRIMARY KEY,
                                NAME TEXT,
                                COLOR INTEGER,
                                MARK INTEGER,
                                SIZE REAL,
                                WIDTH INTEGER,
                                LTYPE INTEGER
                            );";
                        cmd.ExecuteNonQuery();
                    }

                    using (var tx = conn.BeginTransaction())
                    {
                        // Clear existing data in target SQLite database
                        using (var clearCmd = conn.CreateCommand())
                        {
                            clearCmd.Transaction = tx;
                            clearCmd.CommandText = "DELETE FROM '遺構'; DELETE FROM '遺構L'; DELETE FROM '遺物'; DELETE FROM '基準点';";
                            clearCmd.ExecuteNonQuery();
                        }

                        // Prepare commands for 遺構 and 遺構L
                        var cmdIkou = conn.CreateCommand();
                        cmdIkou.Transaction = tx;
                        cmdIkou.CommandText = "INSERT INTO '遺構' (ID, NAME, X, Y, Z, DATE) VALUES (@id, @name, @x, @y, @z, @date);";
                        var pIkouId = cmdIkou.Parameters.Add("@id", SqliteType.Integer);
                        var pIkouName = cmdIkou.Parameters.Add("@name", SqliteType.Text);
                        var pIkouX = cmdIkou.Parameters.Add("@x", SqliteType.Real);
                        var pIkouY = cmdIkou.Parameters.Add("@y", SqliteType.Real);
                        var pIkouZ = cmdIkou.Parameters.Add("@z", SqliteType.Real);
                        var pIkouDate = cmdIkou.Parameters.Add("@date", SqliteType.Text);

                        var cmdIkouL = conn.CreateCommand();
                        cmdIkouL.Transaction = tx;
                        cmdIkouL.CommandText = "INSERT INTO '遺構L' (ID, LID, NAME, MODE, X, Y, Z, LAYER, DATE, PRECS) VALUES (@id, @lid, @name, @mode, @x, @y, @z, @layer, @date, @precs);";
                        var pLId = cmdIkouL.Parameters.Add("@id", SqliteType.Integer);
                        var pLLid = cmdIkouL.Parameters.Add("@lid", SqliteType.Integer);
                        var pLName = cmdIkouL.Parameters.Add("@name", SqliteType.Text);
                        var pLMode = cmdIkouL.Parameters.Add("@mode", SqliteType.Integer);
                        var pLX = cmdIkouL.Parameters.Add("@x", SqliteType.Real);
                        var pLY = cmdIkouL.Parameters.Add("@y", SqliteType.Real);
                        var pLZ = cmdIkouL.Parameters.Add("@z", SqliteType.Real);
                        var pLLayer = cmdIkouL.Parameters.Add("@layer", SqliteType.Integer);
                        var pLDate = cmdIkouL.Parameters.Add("@date", SqliteType.Text);
                        var pLPrecs = cmdIkouL.Parameters.Add("@precs", SqliteType.Text);

                        // Group point data by IKOU (Master Feature)
                        var ikouGroups = pointData
                            .GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3).ikou)
                            .ToList();

                        int ikouIdCounter = 1;
                        int ikouCount = 0;
                        int ikouLCount = 0;

                        foreach (var ikouGroup in ikouGroups)
                        {
                            string ikouName = string.IsNullOrEmpty(ikouGroup.Key) ? "(未分類)" : ikouGroup.Key;
                            var allPts = ikouGroup.ToList();
                            if (allPts.Count == 0) continue;

                            double avgX = allPts.Average(p => p.X);
                            double avgY = allPts.Average(p => p.Y);
                            double avgZ = allPts.Average(p => p.Z);
                            string firstDate = allPts.FirstOrDefault(p => !string.IsNullOrEmpty(p.Date))?.Date ?? DateTime.Now.ToString("yyyy/MM/dd");

                            int currentIkouId = ikouIdCounter++;

                            // Insert into '遺構'
                            pIkouId.Value = currentIkouId;
                            pIkouName.Value = ikouName;
                            pIkouX.Value = Math.Round(avgX, 3);
                            pIkouY.Value = Math.Round(avgY, 3);
                            pIkouZ.Value = Math.Round(avgZ, 3);
                            pIkouDate.Value = firstDate;
                            cmdIkou.ExecuteNonQuery();
                            ikouCount++;

                            // Group by IKOULINE (Line Suffix)
                            var lineGroups = ikouGroup
                                .GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3).ikouLine)
                                .ToList();

                            int lidCounter = 1;
                            foreach (var lineGrp in lineGroups)
                            {
                                string lineName = string.IsNullOrEmpty(lineGrp.Key) ? "" : lineGrp.Key;
                                var linePts = lineGrp.ToList();
                                if (linePts.Count == 0) continue;

                                double lineAvgX = linePts.Average(p => p.X);
                                double lineAvgY = linePts.Average(p => p.Y);
                                double lineAvgZ = linePts.Average(p => p.Z);
                                string lineDate = linePts.FirstOrDefault(p => !string.IsNullOrEmpty(p.Date))?.Date ?? firstDate;

                                // Build PRECS string (Tab-delimited point records separated by newline)
                                var precLines = new List<string>();
                                int pid = 1;
                                foreach (var pt in linePts)
                                {
                                    string dateStr = string.IsNullOrEmpty(pt.Date) ? lineDate : pt.Date;
                                    string precLine = $"{pid}\t{pt.X:0.000}\t{pt.Y:0.000}\t{pt.Z:0.000}\t{dateStr}\t{pt.S:0.000}\t{pt.V:0.000}\t{pt.H:0.000}\t{pt.KPName}\t{pt.BPName}\t{pt.KPH:0.000}\t{pt.MRH:0.000}";
                                    precLines.Add(precLine);
                                    pid++;
                                }
                                string precsText = string.Join("\n", precLines);

                                // Determine Mode: 1 = Closed loop (視点と終点が同一), 0 = Open line (開放)
                                int modeValue = 0;
                                if (linePts.Count > 1)
                                {
                                    var firstPt = linePts[0];
                                    var lastPt = linePts[linePts.Count - 1];

                                    bool samePointNo = !string.IsNullOrEmpty(firstPt.PointNo) &&
                                                       !string.IsNullOrEmpty(lastPt.PointNo) &&
                                                       string.Equals(firstPt.PointNo, lastPt.PointNo, StringComparison.OrdinalIgnoreCase);

                                    double dx = Math.Abs(firstPt.X - lastPt.X);
                                    double dy = Math.Abs(firstPt.Y - lastPt.Y);
                                    double dz = Math.Abs(firstPt.Z - lastPt.Z);

                                    bool sameCoords = (dx < 0.0015 && dy < 0.0015 && dz < 0.010);

                                    if (samePointNo || sameCoords)
                                    {
                                        modeValue = 1; // 1 (閉)
                                    }
                                }

                                pLId.Value = currentIkouId;
                                pLLid.Value = lidCounter++;
                                pLName.Value = lineName;
                                pLMode.Value = modeValue;
                                pLX.Value = Math.Round(lineAvgX, 3);
                                pLY.Value = Math.Round(lineAvgY, 3);
                                pLZ.Value = Math.Round(lineAvgZ, 3);
                                pLLayer.Value = 1; // Layer 1-16 (default 1)
                                pLDate.Value = lineDate;
                                pLPrecs.Value = precsText;
                                cmdIkouL.ExecuteNonQuery();
                                ikouLCount++;
                            }
                        }

                        log($"Inserted {ikouCount:N0} records into '遺構' and {ikouLCount:N0} records into '遺構L'.");

                        // Populate '遺物'
                        int ibutuCount = PopulateIbutuTable(activeDbFolder, isSite5, conn, tx, log);

                        // Populate '基準点' from KIKAI.txt
                        int kikaiCount = PopulateKikaiTable(activeDbFolder, conn, tx, log);

                        // Initialize 'LAYER' table defaults
                        PopulateDefaultLayers(conn, tx, log);

                        tx.Commit();
                        log($"[SUCCESS] SQLite Export Completed successfully: {destDbPath}");
                        return (true, $"Exported {ikouCount} 遺構, {ikouLCount} 遺構L, {ibutuCount} 遺物, {kikaiCount} 基準点 to Site7 SQLite DB.", destDbPath);
                    }
                }
            }
            catch (Exception ex)
            {
                log($"[ERROR] SQLite export failed: {ex.Message}");
                return (false, ex.Message, "");
            }
        }

        private static int PopulateKikaiTable(string folder, SqliteConnection conn, SqliteTransaction tx, Action<string> log)
        {
            string kikaiFile = Path.Combine(folder, "KIKAI.txt");
            if (!File.Exists(kikaiFile)) kikaiFile = Path.Combine(folder, "Kikai.txt");
            if (!File.Exists(kikaiFile)) kikaiFile = Path.Combine(folder, "kikai.txt");

            if (!File.Exists(kikaiFile))
            {
                log("[INFO] KIKAI.txt not found in active DB folder.");
                return 0;
            }

            log($"Parsing KIKAI.txt file '{Path.GetFileName(kikaiFile)}'...");
            int count = 0;

            var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO '基準点' (ID, NAME, X, Y, Z, LAYER, DATE, S, V, H, KPNAME, BPNAME, KPH, MRH)
                VALUES (@id, @name, @x, @y, @z, @layer, @date, @s, @v, @h, @kpname, @bpname, @kph, @mrh);";

            var pId = cmd.Parameters.Add("@id", SqliteType.Integer);
            var pName = cmd.Parameters.Add("@name", SqliteType.Text);
            var pX = cmd.Parameters.Add("@x", SqliteType.Real);
            var pY = cmd.Parameters.Add("@y", SqliteType.Real);
            var pZ = cmd.Parameters.Add("@z", SqliteType.Real);
            var pLayer = cmd.Parameters.Add("@layer", SqliteType.Integer);
            var pDate = cmd.Parameters.Add("@date", SqliteType.Text);
            var pS = cmd.Parameters.Add("@s", SqliteType.Real);
            var pV = cmd.Parameters.Add("@v", SqliteType.Real);
            var pH = cmd.Parameters.Add("@h", SqliteType.Real);
            var pKp = cmd.Parameters.Add("@kpname", SqliteType.Text);
            var pBp = cmd.Parameters.Add("@bpname", SqliteType.Text);
            var pKph = cmd.Parameters.Add("@kph", SqliteType.Real);
            var pMrh = cmd.Parameters.Add("@mrh", SqliteType.Real);

            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var sjisEncoding = System.Text.Encoding.GetEncoding("shift_jis");
                string[] lines = File.ReadAllLines(kikaiFile, sjisEncoding);
                int autoId = 1;
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split(',');
                    if (parts.Length < 7) continue;

                    string name = parts[0].Trim();
                    if (string.IsNullOrEmpty(name)) continue;

                    double.TryParse(parts.Length > 1 ? parts[1].Trim() : "", out double s);
                    double.TryParse(parts.Length > 2 ? parts[2].Trim() : "", out double v);
                    double.TryParse(parts.Length > 3 ? parts[3].Trim() : "", out double h);
                    double.TryParse(parts.Length > 4 ? parts[4].Trim() : "", out double x);
                    double.TryParse(parts.Length > 5 ? parts[5].Trim() : "", out double y);
                    double.TryParse(parts.Length > 6 ? parts[6].Trim() : "", out double z);
                    string date = parts.Length > 7 ? parts[7].Trim() : DateTime.Now.ToString("yyyy/MM/dd");
                    string kp = parts.Length > 8 ? parts[8].Trim() : "";
                    string bp = parts.Length > 9 ? parts[9].Trim() : "";
                    double.TryParse(parts.Length > 10 ? parts[10].Trim() : "", out double kph);
                    double.TryParse(parts.Length > 11 ? parts[11].Trim() : "", out double mrh);

                    pId.Value = autoId++;
                    pName.Value = name;
                    pX.Value = Math.Round(x, 3);
                    pY.Value = Math.Round(y, 3);
                    pZ.Value = Math.Round(z, 3);
                    pLayer.Value = 1; // Layer 1-16 (default 1)
                    pDate.Value = date;
                    pS.Value = s;
                    pV.Value = v;
                    pH.Value = h;
                    pKp.Value = kp;
                    pBp.Value = bp;
                    pKph.Value = kph;
                    pMrh.Value = mrh;

                    cmd.ExecuteNonQuery();
                    count++;
                }
                log($"Inserted {count:N0} records into '基準点'.");
            }
            catch (Exception ex)
            {
                log($"[ERROR] KIKAI.txt parsing failed: {ex.Message}");
            }

            return count;
        }

        private static void PopulateDefaultLayers(SqliteConnection conn, SqliteTransaction tx, Action<string> log)
        {
            var checkCmd = conn.CreateCommand();
            checkCmd.Transaction = tx;
            checkCmd.CommandText = "SELECT COUNT(*) FROM 'LAYER';";
            long count = Convert.ToInt64(checkCmd.ExecuteScalar());
            if (count > 0) return; // Already populated

            log("Initializing default Site7 LAYER definitions...");

            var insertCmd = conn.CreateCommand();
            insertCmd.Transaction = tx;
            insertCmd.CommandText = "INSERT INTO 'LAYER' (ID, NAME, COLOR, MARK, SIZE, WIDTH, LTYPE) VALUES (@id, @name, @color, @mark, @size, @width, @ltype);";
            var pId = insertCmd.Parameters.Add("@id", SqliteType.Integer);
            var pName = insertCmd.Parameters.Add("@name", SqliteType.Text);
            var pColor = insertCmd.Parameters.Add("@color", SqliteType.Integer);
            var pMark = insertCmd.Parameters.Add("@mark", SqliteType.Integer);
            var pSize = insertCmd.Parameters.Add("@size", SqliteType.Real);
            var pWidth = insertCmd.Parameters.Add("@width", SqliteType.Integer);
            var pLtype = insertCmd.Parameters.Add("@ltype", SqliteType.Integer);

            // ID 0: Dummy
            pId.Value = 0; pName.Value = "Dummy"; pColor.Value = 0; pMark.Value = 0; pSize.Value = 0.0; pWidth.Value = 0; pLtype.Value = 0;
            insertCmd.ExecuteNonQuery();

            // 1-16: 遺物
            for (int i = 1; i <= 16; i++)
            {
                pId.Value = i;
                pName.Value = $"遺物_{i}";
                pColor.Value = ((i - 1) % 16) + 1;
                pMark.Value = 1; // 1:〇
                pSize.Value = 5.0;
                pWidth.Value = 1;
                pLtype.Value = 1;
                insertCmd.ExecuteNonQuery();
            }

            // 17-32: 基準点
            for (int i = 17; i <= 32; i++)
            {
                pId.Value = i;
                pName.Value = $"基準点_{i - 16}";
                pColor.Value = ((i - 1) % 16) + 1;
                pMark.Value = 4; // 4:⦿
                pSize.Value = 8.0;
                pWidth.Value = 1;
                pLtype.Value = 1;
                insertCmd.ExecuteNonQuery();
            }

            // 33-48: 作図
            for (int i = 33; i <= 48; i++)
            {
                pId.Value = i;
                pName.Value = $"作図_{i - 32}";
                pColor.Value = ((i - 1) % 16) + 1;
                pMark.Value = 1;
                pSize.Value = 3.0;
                pWidth.Value = 1;
                pLtype.Value = 1;
                insertCmd.ExecuteNonQuery();
            }

            // 49-64: 遺構
            for (int i = 49; i <= 64; i++)
            {
                pId.Value = i;
                pName.Value = $"遺構_{i - 48}";
                pColor.Value = ((i - 1) % 16) + 1;
                pMark.Value = 1;
                pSize.Value = 3.0;
                pWidth.Value = 2; // Width 2
                pLtype.Value = 1; // 1: 折れ線
                insertCmd.ExecuteNonQuery();
            }
        }

        private static string GetStringVal(DataRow row, params string[] columnNames)
        {
            foreach (var col in columnNames)
            {
                if (row.Table.Columns.Contains(col))
                {
                    var val = row[col];
                    if (val != null && val != DBNull.Value)
                    {
                        string s = val.ToString()?.Trim() ?? "";
                        if (!string.IsNullOrEmpty(s)) return s;
                    }
                }
            }
            return "";
        }

        private static double GetDoubleVal(DataRow row, params string[] columnNames)
        {
            string str = GetStringVal(row, columnNames);
            if (double.TryParse(str, out double d)) return d;
            return 0.0;
        }

        private static int GetIntVal(DataRow row, params string[] columnNames)
        {
            string str = GetStringVal(row, columnNames);
            if (int.TryParse(str, out int i)) return i;
            return 0;
        }

        private static int GetLayerValue(DataRow row)
        {
            int layerVal = 1;

            // 1. Check PEN
            string penVal = GetStringVal(row, "PEN", "Pen", "pen");
            if (int.TryParse(penVal, out int penInt) && penInt > 0)
            {
                layerVal = penInt;
            }
            else
            {
                // 2. Check LagerPAGE / LayerPAGE / LAYERPAGE / PAGE
                string pageVal = GetStringVal(row, "LagerPAGE", "LayerPAGE", "LAYERPAGE", "LagerPage", "Page", "PAGE", "PageNo", "PAGENO");
                if (int.TryParse(pageVal, out int pageInt) && pageInt > 0)
                {
                    layerVal = pageInt;
                }
                else
                {
                    // 3. Check existing LAYER column
                    string lVal = GetStringVal(row, "LAYER", "Layer", "layer");
                    if (int.TryParse(lVal, out int lInt) && lInt > 0)
                    {
                        layerVal = lInt;
                    }
                }
            }

            // Clamp layerVal to range 1-16
            if (layerVal < 1) layerVal = 1;
            if (layerVal > 16) layerVal = ((layerVal - 1) % 16) + 1;

            return layerVal;
        }

        private static int PopulateIbutuTable(string folder, bool isSite5, SqliteConnection conn, SqliteTransaction tx, Action<string> log)
        {
            int count = 0;
            var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO '遺物' (ID, CHIKU, SOUI, SYUBETU, No, X, Y, Z, LAYER, DATE, S, V, H, KPNAME, BPNAME, KPH, MRH)
                VALUES (@id, @chiku, @soui, @syubetu, @no, @x, @y, @z, @layer, @date, @s, @v, @h, @kpname, @bpname, @kph, @mrh);";

            var pId = cmd.Parameters.Add("@id", SqliteType.Integer);
            var pChiku = cmd.Parameters.Add("@chiku", SqliteType.Text);
            var pSoui = cmd.Parameters.Add("@soui", SqliteType.Text);
            var pSyubetu = cmd.Parameters.Add("@syubetu", SqliteType.Text);
            var pNo = cmd.Parameters.Add("@no", SqliteType.Integer);
            var pX = cmd.Parameters.Add("@x", SqliteType.Real);
            var pY = cmd.Parameters.Add("@y", SqliteType.Real);
            var pZ = cmd.Parameters.Add("@z", SqliteType.Real);
            var pLayer = cmd.Parameters.Add("@layer", SqliteType.Integer);
            var pDate = cmd.Parameters.Add("@date", SqliteType.Text);
            var pS = cmd.Parameters.Add("@s", SqliteType.Real);
            var pV = cmd.Parameters.Add("@v", SqliteType.Real);
            var pH = cmd.Parameters.Add("@h", SqliteType.Real);
            var pKp = cmd.Parameters.Add("@kpname", SqliteType.Text);
            var pBp = cmd.Parameters.Add("@bpname", SqliteType.Text);
            var pKph = cmd.Parameters.Add("@kph", SqliteType.Real);
            var pMrh = cmd.Parameters.Add("@mrh", SqliteType.Real);

            if (isSite5)
            {
                string ibutuMdb = Path.Combine(folder, "IBUTU.MDB");
                if (File.Exists(ibutuMdb))
                {
                    try
                    {
                        using (var mdbConn = new OleDbConnection(DbHelper.GetMdbConnectionString(ibutuMdb)))
                        {
                            mdbConn.Open();
                            var schemaTable = mdbConn.GetSchema("Tables");
                            bool tableExists = false;
                            if (schemaTable != null)
                            {
                                foreach (DataRow row in schemaTable.Rows)
                                {
                                    var tName = row["TABLE_NAME"]?.ToString();
                                    if (tName != null && tName.Equals("IBUTU", StringComparison.OrdinalIgnoreCase))
                                    {
                                        tableExists = true;
                                        break;
                                    }
                                }
                            }

                            if (!tableExists)
                            {
                                log("[WARNING] Table 'IBUTU' not found in IBUTU.MDB");
                                return 0;
                            }

                            using (var adapter = new OleDbDataAdapter("SELECT * FROM [IBUTU]", mdbConn))
                            {
                                var dt = new DataTable();
                                adapter.Fill(dt);
                                log($"Loaded {dt.Rows.Count:N0} raw records from Access IBUTU.MDB.");

                                int autoId = 1;
                                foreach (DataRow row in dt.Rows)
                                {
                                    // Check DELETEFLG / INVISIBLE if columns exist
                                    string delFlg = GetStringVal(row, "DELETEFLG", "DeleteFlg", "deleteflg");
                                    string invFlg = GetStringVal(row, "INVISIBLE", "Invisible", "invisible");
                                    if (string.Equals(delFlg, "True", StringComparison.OrdinalIgnoreCase) || delFlg == "1") continue;
                                    if (string.Equals(invFlg, "True", StringComparison.OrdinalIgnoreCase) || invFlg == "1") continue;

                                    int id = GetIntVal(row, "ID", "id");
                                    if (id <= 0) id = autoId;

                                    pId.Value = id;
                                    pChiku.Value = GetStringVal(row, "CHIKU", "Chiku", "地区");
                                    pSoui.Value = GetStringVal(row, "SOUI", "Soui", "層位");
                                    pSyubetu.Value = GetStringVal(row, "SYUBETU", "Syubetu", "SHUBETSU", "種別");
                                    pNo.Value = GetIntVal(row, "No", "NO", "IBUTU_NO", "IBUTUNO", "遺物番号");
                                    pX.Value = Math.Round(GetDoubleVal(row, "X", "x"), 3);
                                    pY.Value = Math.Round(GetDoubleVal(row, "Y", "y"), 3);
                                    pZ.Value = Math.Round(GetDoubleVal(row, "Z", "z"), 3);
                                    pLayer.Value = GetLayerValue(row);
                                    pDate.Value = GetStringVal(row, "S_DATE", "SDATE", "YMD", "DATE", "Date", "日付");
                                    pS.Value = GetDoubleVal(row, "S", "s");
                                    pV.Value = GetDoubleVal(row, "V", "v");
                                    pH.Value = GetDoubleVal(row, "H", "h");
                                    pKp.Value = GetStringVal(row, "KPNAME", "KPName", "KP_NAME", "KP");
                                    pBp.Value = GetStringVal(row, "BPNAME", "BPName", "BP_NAME", "BP");
                                    pKph.Value = GetDoubleVal(row, "KPH", "kph");
                                    pMrh.Value = GetDoubleVal(row, "MRH", "mrh");

                                    cmd.ExecuteNonQuery();
                                    count++;
                                    autoId++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log($"[WARNING] MDB IBUTU load error: {ex.Message}");
                    }
                }
            }
            else
            {
                string fdbPath = Path.Combine(folder, "GENBA_DATA.FDB");
                if (File.Exists(fdbPath))
                {
                    try
                    {
                        using (var fdbConn = new FbConnection(DbHelper.GetFdbConnectionString(fdbPath)))
                        {
                            fdbConn.Open();
                            using (var adapter = new FbDataAdapter("SELECT * FROM \"IBUTU_HAND_V\"", fdbConn))
                            {
                                var dt = new DataTable();
                                adapter.Fill(dt);
                                log($"Loaded {dt.Rows.Count:N0} raw records from Firebird FDB IBUTU_HAND_V.");

                                int autoId = 1;
                                foreach (DataRow row in dt.Rows)
                                {
                                    string delFlg = GetStringVal(row, "DELETEFLG", "DeleteFlg", "GDELETEFLG");
                                    string invFlg = GetStringVal(row, "INVISIBLE", "Invisible", "GINVISIBLE");
                                    if (delFlg == "1" || string.Equals(delFlg, "True", StringComparison.OrdinalIgnoreCase)) continue;
                                    if (invFlg == "1" || string.Equals(invFlg, "True", StringComparison.OrdinalIgnoreCase)) continue;

                                    int id = GetIntVal(row, "ID", "id");
                                    if (id <= 0) id = autoId;

                                    pId.Value = id;
                                    pChiku.Value = GetStringVal(row, "CHIKU", "Chiku", "地区");
                                    pSoui.Value = GetStringVal(row, "SOUI", "Soui", "層位");
                                    pSyubetu.Value = GetStringVal(row, "SYUBETU", "Syubetu", "SHUBETSU", "種別");
                                    pNo.Value = GetIntVal(row, "IBUTU_NO", "IBUTUNO", "No", "NO", "遺物番号");
                                    pX.Value = Math.Round(GetDoubleVal(row, "X", "x"), 3);
                                    pY.Value = Math.Round(GetDoubleVal(row, "Y", "y"), 3);
                                    pZ.Value = Math.Round(GetDoubleVal(row, "Z", "z"), 3);
                                    pLayer.Value = GetLayerValue(row);
                                    pDate.Value = GetStringVal(row, "S_DATE", "SDATE", "YMD", "DATE", "Date", "日付");
                                    pS.Value = GetDoubleVal(row, "S", "s");
                                    pV.Value = GetDoubleVal(row, "V", "v");
                                    pH.Value = GetDoubleVal(row, "H", "h");
                                    pKp.Value = GetStringVal(row, "KPNAME", "KPName", "KP_NAME", "KP");
                                    pBp.Value = GetStringVal(row, "BPNAME", "BPName", "BP_NAME", "BP");
                                    pKph.Value = GetDoubleVal(row, "KPH", "kph");
                                    pMrh.Value = GetDoubleVal(row, "MRH", "mrh");

                                    cmd.ExecuteNonQuery();
                                    count++;
                                    autoId++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log($"[WARNING] FDB IBUTU load error: {ex.Message}");
                    }
                }
            }

            log($"Inserted {count:N0} records into '遺物'.");
            return count;
        }
    }
}
