using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace Site7DbEditor.Services
{
    public class EditorDbManager
    {
        public string CurrentDbPath { get; set; } = "";

        public BindingList<IkouModel> IkouList { get; } = new BindingList<IkouModel>();
        public BindingList<IkouLModel> IkouLList { get; } = new BindingList<IkouLModel>();
        public BindingList<IbutuModel> IbutuList { get; } = new BindingList<IbutuModel>();
        public BindingList<KikaiModel> KikaiList { get; } = new BindingList<KikaiModel>();
        public BindingList<LayerModel> LayerList { get; } = new BindingList<LayerModel>();

        public void LoadDatabase(string dbPath)
        {
            if (!File.Exists(dbPath)) return;
            CurrentDbPath = dbPath;

            IkouList.Clear();
            IkouLList.Clear();
            IbutuList.Clear();
            KikaiList.Clear();
            LayerList.Clear();

            string connStr = $"Data Source={dbPath};";

            using (var conn = new SqliteConnection(connStr))
            {
                conn.Open();

                // 1. Read 遺構
                using (var cmd = new SqliteCommand("SELECT ID, NAME, X, Y, Z, DATE FROM '遺構' ORDER BY ID;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IkouList.Add(new IkouModel
                        {
                            Id = reader.GetInt64(0),
                            Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            X = reader.IsDBNull(2) ? 0.0 : reader.GetDouble(2),
                            Y = reader.IsDBNull(3) ? 0.0 : reader.GetDouble(3),
                            Z = reader.IsDBNull(4) ? 0.0 : reader.GetDouble(4),
                            Date = reader.IsDBNull(5) ? "" : reader.GetString(5)
                        });
                    }
                }

                // 2. Read 遺構L
                using (var cmd = new SqliteCommand("SELECT ID, LID, NAME, MODE, X, Y, Z, LAYER, DATE, PRECS FROM '遺構L' ORDER BY ID, LID;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IkouLList.Add(new IkouLModel
                        {
                            Id = reader.GetInt64(0),
                            Lid = reader.GetInt64(1),
                            Name = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Mode = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                            X = reader.IsDBNull(4) ? 0.0 : reader.GetDouble(4),
                            Y = reader.IsDBNull(5) ? 0.0 : reader.GetDouble(5),
                            Z = reader.IsDBNull(6) ? 0.0 : reader.GetDouble(6),
                            Layer = reader.IsDBNull(7) ? 1 : reader.GetInt32(7),
                            Date = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            Precs = reader.IsDBNull(9) ? "" : reader.GetString(9)
                        });
                    }
                }

                // 3. Read 遺物
                using (var cmd = new SqliteCommand("SELECT ID, CHIKU, SOUI, SYUBETU, No, X, Y, Z, LAYER, DATE, S, V, H, KPNAME, BPNAME, KPH, MRH FROM '遺物' ORDER BY ID;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IbutuList.Add(new IbutuModel
                        {
                            Id = reader.GetInt64(0),
                            Chiku = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Soui = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Syubetu = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            No = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                            X = reader.IsDBNull(5) ? 0.0 : reader.GetDouble(5),
                            Y = reader.IsDBNull(6) ? 0.0 : reader.GetDouble(6),
                            Z = reader.IsDBNull(7) ? 0.0 : reader.GetDouble(7),
                            Layer = reader.IsDBNull(8) ? 1 : reader.GetInt32(8),
                            Date = reader.IsDBNull(9) ? "" : reader.GetString(9),
                            S = reader.IsDBNull(10) ? 0.0 : reader.GetDouble(10),
                            V = reader.IsDBNull(11) ? 0.0 : reader.GetDouble(11),
                            H = reader.IsDBNull(12) ? 0.0 : reader.GetDouble(12),
                            KPName = reader.IsDBNull(13) ? "" : reader.GetString(13),
                            BPName = reader.IsDBNull(14) ? "" : reader.GetString(14),
                            KPH = reader.IsDBNull(15) ? 0.0 : reader.GetDouble(15),
                            MRH = reader.IsDBNull(16) ? 0.0 : reader.GetDouble(16)
                        });
                    }
                }

                // 4. Read 基準点
                try
                {
                    using (var cmd = new SqliteCommand("SELECT ID, NAME, X, Y, Z, LAYER, DATE, S, V, H, KPNAME, BPNAME, KPH, MRH FROM '基準点' ORDER BY ID;", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KikaiList.Add(new KikaiModel
                            {
                                Id = reader.GetInt64(0),
                                Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                X = reader.IsDBNull(2) ? 0.0 : reader.GetDouble(2),
                                Y = reader.IsDBNull(3) ? 0.0 : reader.GetDouble(3),
                                Z = reader.IsDBNull(4) ? 0.0 : reader.GetDouble(4),
                                Layer = reader.IsDBNull(5) ? 1 : reader.GetInt32(5),
                                Date = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                S = reader.IsDBNull(7) ? 0.0 : reader.GetDouble(7),
                                V = reader.IsDBNull(8) ? 0.0 : reader.GetDouble(8),
                                H = reader.IsDBNull(9) ? 0.0 : reader.GetDouble(9),
                                KPName = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                BPName = reader.IsDBNull(11) ? "" : reader.GetString(11),
                                KPH = reader.IsDBNull(12) ? 0.0 : reader.GetDouble(12),
                                MRH = reader.IsDBNull(13) ? 0.0 : reader.GetDouble(13)
                            });
                        }
                    }
                }
                catch { }

                // 5. Read LAYER
                try
                {
                    using (var cmd = new SqliteCommand("SELECT ID, NAME, COLOR, MARK, SIZE, WIDTH, LTYPE FROM 'LAYER' ORDER BY ID;", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LayerList.Add(new LayerModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                Color = reader.IsDBNull(2) ? 1 : reader.GetInt32(2),
                                Mark = reader.IsDBNull(3) ? 1 : reader.GetInt32(3),
                                Size = reader.IsDBNull(4) ? 5.0 : reader.GetDouble(4),
                                Width = reader.IsDBNull(5) ? 1 : reader.GetInt32(5),
                                LType = reader.IsDBNull(6) ? 1 : reader.GetInt32(6)
                            });
                        }
                    }
                }
                catch { }
            }
        }

        public void SaveDatabase(
            string dbPath,
            bool showIkou = true,
            bool showIbutu = true,
            bool showKikai = true,
            bool drawCurve = true)
        {
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath)) return;

            string connStr = $"Data Source={dbPath};";

            using (var conn = new SqliteConnection(connStr))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    // 1. Save 遺構
                    using (var cmd = new SqliteCommand("DELETE FROM '遺構';", conn, trans)) { cmd.ExecuteNonQuery(); }
                    foreach (var item in IkouList)
                    {
                        using (var cmd = new SqliteCommand("INSERT INTO '遺構' (ID, NAME, X, Y, Z, DATE) VALUES (@id, @name, @x, @y, @z, @date);", conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", item.Id);
                            cmd.Parameters.AddWithValue("@name", item.Name ?? "");
                            cmd.Parameters.AddWithValue("@x", item.X);
                            cmd.Parameters.AddWithValue("@y", item.Y);
                            cmd.Parameters.AddWithValue("@z", item.Z);
                            cmd.Parameters.AddWithValue("@date", item.Date ?? "");
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 2. Save 遺構L
                    using (var cmd = new SqliteCommand("DELETE FROM '遺構L';", conn, trans)) { cmd.ExecuteNonQuery(); }
                    foreach (var item in IkouLList)
                    {
                        using (var cmd = new SqliteCommand("INSERT INTO '遺構L' (ID, LID, NAME, MODE, X, Y, Z, LAYER, DATE, PRECS) VALUES (@id, @lid, @name, @mode, @x, @y, @z, @layer, @date, @precs);", conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", item.Id);
                            cmd.Parameters.AddWithValue("@lid", item.Lid);
                            cmd.Parameters.AddWithValue("@name", item.Name ?? "");
                            cmd.Parameters.AddWithValue("@mode", item.Mode);
                            cmd.Parameters.AddWithValue("@x", item.X);
                            cmd.Parameters.AddWithValue("@y", item.Y);
                            cmd.Parameters.AddWithValue("@z", item.Z);
                            cmd.Parameters.AddWithValue("@layer", item.Layer);
                            cmd.Parameters.AddWithValue("@date", item.Date ?? "");
                            cmd.Parameters.AddWithValue("@precs", item.Precs ?? "");
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 3. Save 遺物
                    using (var cmd = new SqliteCommand("DELETE FROM '遺物';", conn, trans)) { cmd.ExecuteNonQuery(); }
                    foreach (var item in IbutuList)
                    {
                        using (var cmd = new SqliteCommand("INSERT INTO '遺物' (ID, CHIKU, SOUI, SYUBETU, No, X, Y, Z, LAYER, DATE, S, V, H, KPNAME, BPNAME, KPH, MRH) VALUES (@id, @chiku, @soui, @syubetu, @no, @x, @y, @z, @layer, @date, @s, @v, @h, @kpname, @bpname, @kph, @mrh);", conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", item.Id);
                            cmd.Parameters.AddWithValue("@chiku", item.Chiku ?? "");
                            cmd.Parameters.AddWithValue("@soui", item.Soui ?? "");
                            cmd.Parameters.AddWithValue("@syubetu", item.Syubetu ?? "");
                            cmd.Parameters.AddWithValue("@no", item.No);
                            cmd.Parameters.AddWithValue("@x", item.X);
                            cmd.Parameters.AddWithValue("@y", item.Y);
                            cmd.Parameters.AddWithValue("@z", item.Z);
                            cmd.Parameters.AddWithValue("@layer", item.Layer);
                            cmd.Parameters.AddWithValue("@date", item.Date ?? "");
                            cmd.Parameters.AddWithValue("@s", item.S);
                            cmd.Parameters.AddWithValue("@v", item.V);
                            cmd.Parameters.AddWithValue("@h", item.H);
                            cmd.Parameters.AddWithValue("@kpname", item.KPName ?? "");
                            cmd.Parameters.AddWithValue("@bpname", item.BPName ?? "");
                            cmd.Parameters.AddWithValue("@kph", item.KPH);
                            cmd.Parameters.AddWithValue("@mrh", item.MRH);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 4. Save 基準点
                    try
                    {
                        using (var cmd = new SqliteCommand("DELETE FROM '基準点';", conn, trans)) { cmd.ExecuteNonQuery(); }
                        foreach (var item in KikaiList)
                        {
                            using (var cmd = new SqliteCommand("INSERT INTO '基準点' (ID, NAME, X, Y, Z, LAYER, DATE, S, V, H, KPNAME, BPNAME, KPH, MRH) VALUES (@id, @name, @x, @y, @z, @layer, @date, @s, @v, @h, @kpname, @bpname, @kph, @mrh);", conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@id", item.Id);
                                cmd.Parameters.AddWithValue("@name", item.Name ?? "");
                                cmd.Parameters.AddWithValue("@x", item.X);
                                cmd.Parameters.AddWithValue("@y", item.Y);
                                cmd.Parameters.AddWithValue("@z", item.Z);
                                cmd.Parameters.AddWithValue("@layer", item.Layer);
                                cmd.Parameters.AddWithValue("@date", item.Date ?? "");
                                cmd.Parameters.AddWithValue("@s", item.S);
                                cmd.Parameters.AddWithValue("@v", item.V);
                                cmd.Parameters.AddWithValue("@h", item.H);
                                cmd.Parameters.AddWithValue("@kpname", item.KPName ?? "");
                                cmd.Parameters.AddWithValue("@bpname", item.BPName ?? "");
                                cmd.Parameters.AddWithValue("@kph", item.KPH);
                                cmd.Parameters.AddWithValue("@mrh", item.MRH);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch { }

                    // 5. Save LAYER
                    try
                    {
                        using (var cmd = new SqliteCommand("DELETE FROM 'LAYER';", conn, trans)) { cmd.ExecuteNonQuery(); }
                        foreach (var item in LayerList)
                        {
                            using (var cmd = new SqliteCommand("INSERT INTO 'LAYER' (ID, NAME, COLOR, MARK, SIZE, WIDTH, LTYPE) VALUES (@id, @name, @color, @mark, @size, @width, @ltype);", conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@id", item.Id);
                                cmd.Parameters.AddWithValue("@name", item.Name ?? "");
                                cmd.Parameters.AddWithValue("@color", item.Color);
                                cmd.Parameters.AddWithValue("@mark", item.Mark);
                                cmd.Parameters.AddWithValue("@size", item.Size);
                                cmd.Parameters.AddWithValue("@width", item.Width);
                                cmd.Parameters.AddWithValue("@ltype", item.LType);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch { }

                    trans.Commit();
                }
            }

            // DB保存完了後、同じフォルダに256x256の全図サムネイル(SITE7.png)を白背景で保存
            EditorMapRenderer.SaveThumbnail(dbPath, this, showIkou, showIbutu, showKikai, drawCurve);
        }

        public static bool MatchesFilter(string val, string op, string filterVal)
        {
            if (op.Contains("すべて") || string.IsNullOrWhiteSpace(filterVal)) return true;

            val = val ?? "";
            filterVal = filterVal.Trim();

            if (op.Contains("前方一致")) return val.StartsWith(filterVal, StringComparison.OrdinalIgnoreCase);
            if (op.Contains("後方一致")) return val.EndsWith(filterVal, StringComparison.OrdinalIgnoreCase);
            if (op.Contains("部分一致")) return val.Contains(filterVal, StringComparison.OrdinalIgnoreCase);
            if (op.Contains("完全一致")) return string.Equals(val.Trim(), filterVal, StringComparison.OrdinalIgnoreCase);
            return true;
        }

        public List<object> GetBatchMatchingItems(string selectedTable, string filterCol, string filterOp, string filterVal)
        {
            var matchingList = new List<object>();

            if (selectedTable.Contains("遺構L"))
            {
                foreach (var item in IkouLList)
                {
                    string val = filterCol switch
                    {
                        "NAME" => item.Name,
                        "LAYER" => item.Layer.ToString(),
                        "MODE" => item.Mode.ToString(),
                        "DATE" => item.Date,
                        "ID" => item.Id.ToString(),
                        "LID" => item.Lid.ToString(),
                        _ => item.Name
                    };
                    if (MatchesFilter(val, filterOp, filterVal)) matchingList.Add(item);
                }
            }
            else if (selectedTable.Contains("遺構 (マスター)"))
            {
                foreach (var item in IkouList)
                {
                    string val = filterCol switch
                    {
                        "NAME" => item.Name,
                        "DATE" => item.Date,
                        "ID" => item.Id.ToString(),
                        _ => item.Name
                    };
                    if (MatchesFilter(val, filterOp, filterVal)) matchingList.Add(item);
                }
            }
            else if (selectedTable.Contains("遺物"))
            {
                foreach (var item in IbutuList)
                {
                    string val = filterCol switch
                    {
                        "CHIKU" => item.Chiku,
                        "SOUI" => item.Soui,
                        "SYUBETU" => item.Syubetu,
                        "NAME(Syubetu)" => item.Syubetu,
                        "LAYER" => item.Layer.ToString(),
                        "DATE" => item.Date,
                        "ID" => item.Id.ToString(),
                        _ => item.Syubetu
                    };
                    if (MatchesFilter(val, filterOp, filterVal)) matchingList.Add(item);
                }
            }
            else if (selectedTable.Contains("基準点"))
            {
                foreach (var item in KikaiList)
                {
                    string val = filterCol switch
                    {
                        "NAME" => item.Name,
                        "LAYER" => item.Layer.ToString(),
                        "DATE" => item.Date,
                        "ID" => item.Id.ToString(),
                        _ => item.Name
                    };
                    if (MatchesFilter(val, filterOp, filterVal)) matchingList.Add(item);
                }
            }

            return matchingList;
        }

        public int ExecuteBatchUpdate(string selectedTable, string filterCol, string filterOp, string filterVal, string updateCol, string updateVal)
        {
            var items = GetBatchMatchingItems(selectedTable, filterCol, filterOp, filterVal);
            int successCount = 0;
            foreach (var obj in items)
            {
                if (obj is IkouLModel line)
                {
                    if (updateCol == "LAYER" && int.TryParse(updateVal, out int layer))
                    {
                        line.Layer = Math.Clamp(layer, 1, 16);
                    }
                    else if (updateCol == "MODE" && int.TryParse(updateVal, out int mode))
                    {
                        line.Mode = mode;
                    }
                    else if (updateCol == "NAME")
                    {
                        line.Name = updateVal;
                    }
                    else if (updateCol == "DATE")
                    {
                        line.Date = updateVal;
                    }
                    successCount++;
                }
                else if (obj is IkouModel ikou)
                {
                    if (updateCol == "NAME") ikou.Name = updateVal;
                    else if (updateCol == "DATE") ikou.Date = updateVal;
                    successCount++;
                }
                else if (obj is IbutuModel ibutu)
                {
                    if (updateCol == "LAYER" && int.TryParse(updateVal, out int layer))
                    {
                        ibutu.Layer = Math.Clamp(layer, 1, 16);
                    }
                    else if (updateCol == "CHIKU") ibutu.Chiku = updateVal;
                    else if (updateCol == "SOUI") ibutu.Soui = updateVal;
                    else if (updateCol == "SYUBETU") ibutu.Syubetu = updateVal;
                    else if (updateCol == "DATE") ibutu.Date = updateVal;
                    successCount++;
                }
                else if (obj is KikaiModel kikai)
                {
                    if (updateCol == "LAYER" && int.TryParse(updateVal, out int layer))
                    {
                        kikai.Layer = Math.Clamp(layer, 1, 16);
                    }
                    else if (updateCol == "NAME") kikai.Name = updateVal;
                    else if (updateCol == "DATE") kikai.Date = updateVal;
                    successCount++;
                }
            }
            return successCount;
        }
    }
}
