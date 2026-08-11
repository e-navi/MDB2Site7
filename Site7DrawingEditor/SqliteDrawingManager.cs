using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace Site7DrawingEditor
{
    public static class SqliteDrawingManager
    {
        public static void EnsureDrawingTables(SqliteConnection conn, SqliteTransaction? tx = null)
        {
            string sql = @"
CREATE TABLE IF NOT EXISTS '図面' (
    'ZID' INTEGER,
    'TYPE' INTEGER,
    'NAME' TEXT,
    'PAPERSIZE' INTEGER,
    'SCALE' INTEGER,
    PRIMARY KEY('ZID')
);

CREATE TABLE IF NOT EXISTS '図面遺構' (
    'ZID' INTEGER,
    'IID' INTEGER,
    'NAME' TEXT,
    'X1' REAL,
    'Y1' REAL,
    'X2' REAL,
    'Y2' REAL,
    'X3' REAL,
    'Y3' REAL,
    'PX' REAL,
    'PY' REAL,
    'LLISTSTR' TEXT,
    'DMLISTSTR' TEXT,
    PRIMARY KEY('ZID','IID')
);
";
            using (var cmd = new SqliteCommand(sql, conn, tx))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static (List<DrawingModel> drawings, List<DrawingIkouModel> drawingIkous) LoadDrawings(string dbPath)
        {
            var drawings = new List<DrawingModel>();
            var drawingIkous = new List<DrawingIkouModel>();

            if (!File.Exists(dbPath)) return (drawings, drawingIkous);

            using (var conn = new SqliteConnection($"Data Source={dbPath};"))
            {
                conn.Open();
                EnsureDrawingTables(conn);

                // 1. Read 図面
                using (var cmd = new SqliteCommand("SELECT ZID, TYPE, NAME, PAPERSIZE, SCALE FROM '図面' ORDER BY ZID;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drawings.Add(new DrawingModel
                        {
                            ZID = reader.GetInt32(0),
                            Type = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                            Name = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            PaperSize = reader.IsDBNull(3) ? 3 : reader.GetInt32(3),
                            Scale = reader.IsDBNull(4) ? 20 : reader.GetInt32(4)
                        });
                    }
                }

                // 2. Read 図面遺構
                using (var cmd = new SqliteCommand("SELECT ZID, IID, NAME, X1, Y1, X2, Y2, X3, Y3, PX, PY, LLISTSTR, DMLISTSTR FROM '図面遺構' ORDER BY ZID, IID;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new DrawingIkouModel
                        {
                            ZID = reader.GetInt32(0),
                            IID = reader.GetInt32(1),
                            Name = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            P1 = new XYZ(reader.IsDBNull(3) ? 0 : reader.GetDouble(3), reader.IsDBNull(4) ? 0 : reader.GetDouble(4)),
                            P2 = new XYZ(reader.IsDBNull(5) ? 0 : reader.GetDouble(5), reader.IsDBNull(6) ? 0 : reader.GetDouble(6)),
                            P3 = new XYZ(reader.IsDBNull(7) ? 0 : reader.GetDouble(7), reader.IsDBNull(8) ? 0 : reader.GetDouble(8)),
                            PP = new Point3D(reader.IsDBNull(9) ? 50 : reader.GetDouble(9), reader.IsDBNull(10) ? 50 : reader.GetDouble(10)),
                            LListStr = reader.IsDBNull(11) ? "" : reader.GetString(11),
                            DmListStr = reader.IsDBNull(12) ? "" : reader.GetString(12)
                        };

                        item.Str2LList(item.LListStr);
                        item.Str2DmList(item.DmListStr);
                        drawingIkous.Add(item);
                    }
                }
            }

            return (drawings, drawingIkous);
        }

        public static (List<MasterIkouModel> ikouList, List<MasterIkouLModel> ikouLList, List<MasterIbutuModel> ibutuList, List<MasterKikaiModel> kikaiList, List<MasterLayerModel> layerList) LoadMasterSurveyData(string dbPath)
        {
            var ikouList = new List<MasterIkouModel>();
            var ikouLList = new List<MasterIkouLModel>();
            var ibutuList = new List<MasterIbutuModel>();
            var kikaiList = new List<MasterKikaiModel>();

            if (!File.Exists(dbPath)) return (ikouList, ikouLList, ibutuList, kikaiList, new List<MasterLayerModel>());

            using (var conn = new SqliteConnection($"Data Source={dbPath};"))
            {
                conn.Open();

                // Read 遺構
                try
                {
                    using (var cmd = new SqliteCommand("SELECT ID, NAME, X, Y, Z FROM '遺構' ORDER BY ID;", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ikouList.Add(new MasterIkouModel
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                X = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                Y = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                Z = reader.IsDBNull(4) ? 0 : reader.GetDouble(4)
                            });
                        }
                    }
                }
                catch { }

                // Read 遺構L
                try
                {
                    using (var cmd = new SqliteCommand("SELECT ID, LID, NAME, MODE, LAYER, PRECS FROM '遺構L' ORDER BY ID, LID;", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ikouLList.Add(new MasterIkouLModel
                            {
                                Id = reader.GetInt64(0),
                                Lid = reader.GetInt64(1),
                                Name = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Mode = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                Layer = reader.IsDBNull(4) ? 1 : reader.GetInt32(4),
                                Precs = reader.IsDBNull(5) ? "" : reader.GetString(5)
                            });
                        }
                    }
                }
                catch { }

                // Read 遺物
                try
                {
                    using (var cmd = new SqliteCommand("SELECT ID, CHIKU, SOUI, SYUBETU, NO, X, Y, Z, LAYER FROM '遺物' ORDER BY ID;", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ibutuList.Add(new MasterIbutuModel
                            {
                                Id = reader.GetInt64(0),
                                Chiku = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                Soui = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Syubetu = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                No = reader.IsDBNull(4) ? 0 : reader.GetInt64(4),
                                X = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                Y = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                                Z = reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                                Layer = reader.IsDBNull(8) ? 1 : reader.GetInt32(8)
                            });
                        }
                    }
                }
                catch { }

                // Read 基準点
                try
                {
                    using (var cmd = new SqliteCommand("SELECT ID, NAME, X, Y, Z, SYUBETU FROM '基準点' ORDER BY ID;", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kikaiList.Add(new MasterKikaiModel
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                X = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                Y = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                Z = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                Syubetu = reader.IsDBNull(5) ? 0 : reader.GetInt32(5)
                            });
                        }
                    }
                }
                catch { }

                // Read LAYER
                var layerList = new List<MasterLayerModel>();
                try
                {
                    using (var cmd = new SqliteCommand("SELECT ID, NAME, LTYPE FROM 'LAYER' ORDER BY ID;", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            layerList.Add(new MasterLayerModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                LType = reader.IsDBNull(2) ? 1 : reader.GetInt32(2)
                            });
                        }
                    }
                }
                catch { }

                return (ikouList, ikouLList, ibutuList, kikaiList, layerList);
            }
        }

        public static void SaveDrawings(string dbPath, List<DrawingModel> drawings, List<DrawingIkouModel> drawingIkous)
        {
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath)) return;

            using (var conn = new SqliteConnection($"Data Source={dbPath};"))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    EnsureDrawingTables(conn, tx);

                    // Clear tables
                    using (var clearCmd = new SqliteCommand("DELETE FROM '図面'; DELETE FROM '図面遺構';", conn, tx))
                    {
                        clearCmd.ExecuteNonQuery();
                    }

                    // Insert 図面
                    using (var cmd = new SqliteCommand("INSERT INTO '図面' (ZID, TYPE, NAME, PAPERSIZE, SCALE) VALUES (@zid, @type, @name, @paper, @scale);", conn, tx))
                    {
                        var pZid = cmd.Parameters.Add("@zid", SqliteType.Integer);
                        var pType = cmd.Parameters.Add("@type", SqliteType.Integer);
                        var pName = cmd.Parameters.Add("@name", SqliteType.Text);
                        var pPaper = cmd.Parameters.Add("@paper", SqliteType.Integer);
                        var pScale = cmd.Parameters.Add("@scale", SqliteType.Integer);

                        foreach (var item in drawings)
                        {
                            pZid.Value = item.ZID;
                            pType.Value = item.Type;
                            pName.Value = item.Name;
                            pPaper.Value = item.PaperSize;
                            pScale.Value = item.Scale;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Insert 図面遺構
                    using (var cmd = new SqliteCommand("INSERT INTO '図面遺構' (ZID, IID, NAME, X1, Y1, X2, Y2, X3, Y3, PX, PY, LLISTSTR, DMLISTSTR) VALUES (@zid, @iid, @name, @x1, @y1, @x2, @y2, @x3, @y3, @px, @py, @llist, @dmlist);", conn, tx))
                    {
                        var pZid = cmd.Parameters.Add("@zid", SqliteType.Integer);
                        var pIid = cmd.Parameters.Add("@iid", SqliteType.Integer);
                        var pName = cmd.Parameters.Add("@name", SqliteType.Text);
                        var pX1 = cmd.Parameters.Add("@x1", SqliteType.Real);
                        var pY1 = cmd.Parameters.Add("@y1", SqliteType.Real);
                        var pX2 = cmd.Parameters.Add("@x2", SqliteType.Real);
                        var pY2 = cmd.Parameters.Add("@y2", SqliteType.Real);
                        var pX3 = cmd.Parameters.Add("@x3", SqliteType.Real);
                        var pY3 = cmd.Parameters.Add("@y3", SqliteType.Real);
                        var pPx = cmd.Parameters.Add("@px", SqliteType.Real);
                        var pPy = cmd.Parameters.Add("@py", SqliteType.Real);
                        var pLList = cmd.Parameters.Add("@llist", SqliteType.Text);
                        var pDmList = cmd.Parameters.Add("@dmlist", SqliteType.Text);

                        foreach (var item in drawingIkous)
                        {
                            item.LListStr = item.LList2Str();
                            item.DmListStr = item.DmList2Str();

                            pZid.Value = item.ZID;
                            pIid.Value = item.IID;
                            pName.Value = item.Name;
                            pX1.Value = item.P1.X;
                            pY1.Value = item.P1.Y;
                            pX2.Value = item.P2.X;
                            pY2.Value = item.P2.Y;
                            pX3.Value = item.P3.X;
                            pY3.Value = item.P3.Y;
                            pPx.Value = item.PP.X;
                            pPy.Value = item.PP.Y;
                            pLList.Value = item.LListStr;
                            pDmList.Value = item.DmListStr;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                }
            }
        }

        public static List<Point3D> ParsePrecsText(string precsText)
        {
            var result = new List<Point3D>();
            if (string.IsNullOrWhiteSpace(precsText)) return result;

            string[] lines = precsText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('\t');
                if (parts.Length >= 4)
                {
                    if (double.TryParse(parts[1].Trim(), CultureInfo.InvariantCulture, out double x) &&
                        double.TryParse(parts[2].Trim(), CultureInfo.InvariantCulture, out double y) &&
                        double.TryParse(parts[3].Trim(), CultureInfo.InvariantCulture, out double z))
                    {
                        result.Add(new Point3D(x, y, z));
                    }
                }
                else if (parts.Length >= 3)
                {
                    if (double.TryParse(parts[0].Trim(), CultureInfo.InvariantCulture, out double x) &&
                        double.TryParse(parts[1].Trim(), CultureInfo.InvariantCulture, out double y) &&
                        double.TryParse(parts[2].Trim(), CultureInfo.InvariantCulture, out double z))
                    {
                        result.Add(new Point3D(x, y, z));
                    }
                }
            }
            return result;
        }
    }
}
