using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace Site7DbEditor.Services
{
    public class LogEntry
    {
        public int LogNo { get; set; }
        public int LogType { get; set; } // 1: NEW, 2: DEL, 3: UPD
        public int RecType { get; set; } // 1: 基準点, 2: 遺物, 3: 遺構, 4: 遺構線, 5: 頂点
        public object Rec { get; set; }

        public LogEntry(int logNo, int logType, int recType, object rec)
        {
            LogNo = logNo;
            LogType = logType;
            RecType = recType;
            Rec = rec;
        }
    }

    public class EditorLogService
    {
        public const int LOG_TYPE_NEW = 1;
        public const int LOG_TYPE_DEL = 2;
        public const int LOG_TYPE_UPD = 3;

        public const int REC_TYPE_HEADER = 0;
        public const int REC_TYPE_KIJUNP = 1; // 基準点 (KikaiModel)
        public const int REC_TYPE_IBUTU = 2;  // 遺物 (IbutuModel)
        public const int REC_TYPE_IKOU = 3;   // 遺構 (IkouModel)
        public const int REC_TYPE_IKOUL = 4;  // 遺構線 (IkouLModel)
        public const int REC_TYPE_IKOUP = 5;  // 遺構線の頂点

        public int CurLogNo { get; private set; } = 0;
        public int CurLogIdx { get; private set; } = 0;
        public List<LogEntry> Logs { get; } = new List<LogEntry>();

        public bool CanUndo => CurLogIdx > 0;
        public bool CanRedo => CurLogIdx < Logs.Count;

        public event EventHandler? StateChanged;

        public void IncLogNo()
        {
            CurLogNo++;
        }

        public void Clear(string? dbPath = null)
        {
            CurLogNo = 0;
            CurLogIdx = 0;
            Logs.Clear();

            if (!string.IsNullOrEmpty(dbPath) && File.Exists(dbPath))
            {
                ClearInDatabase(dbPath);
            }

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Push(int logType, int recType, object rec, object? rec0 = null, string? dbPath = null)
        {
            // 未来のRedo履歴を切り捨て
            if (CurLogIdx < Logs.Count)
            {
                Logs.RemoveRange(CurLogIdx, Logs.Count - CurLogIdx);
            }

            // 更新(UPD)時は変更前の状態(rec0)を保存、それ以外はrecを保存
            object targetRec = (logType == LOG_TYPE_UPD && rec0 != null) ? CloneRecord(recType, rec0) : CloneRecord(recType, rec);

            var entry = new LogEntry(CurLogNo, logType, recType, targetRec);
            Logs.Add(entry);
            CurLogIdx++;
            IncLogNo();

            if (!string.IsNullOrEmpty(dbPath) && File.Exists(dbPath))
            {
                SaveLogDB(dbPath);
            }

            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool Undo(EditorDbManager db, out int affectedRecType, out long affectedId)
        {
            affectedRecType = -1;
            affectedId = -1;

            if (!CanUndo) return false;

            CurLogIdx--;
            int no = Logs[CurLogIdx].LogNo;

            for (; CurLogIdx >= 0; CurLogIdx--)
            {
                var log = Logs[CurLogIdx];
                if (log.LogNo != no)
                {
                    CurLogIdx++; // 1つ行き過ぎたので戻す
                    break;
                }

                affectedRecType = log.RecType;

                if (log.RecType == REC_TYPE_KIJUNP && log.Rec is KikaiModel rec)
                {
                    affectedId = rec.Id;
                    var existing = db.KikaiList.FirstOrDefault(k => k.Id == rec.Id);

                    if (log.LogType == LOG_TYPE_NEW) // 新規追加のUndo -> 削除
                    {
                        if (existing != null) db.KikaiList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_DEL) // 削除のUndo -> 復元追加
                    {
                        if (existing == null) db.KikaiList.Add((KikaiModel)CloneRecord(REC_TYPE_KIJUNP, rec));
                    }
                    else if (log.LogType == LOG_TYPE_UPD) // 更新のUndo -> 変更前状態に戻す
                    {
                        if (existing != null)
                        {
                            // 現在の状態をRedo用にlog.Recにスワップ保存
                            var currentCopy = (KikaiModel)CloneRecord(REC_TYPE_KIJUNP, existing);
                            CopyKikaiProperties(rec, existing);
                            log.Rec = currentCopy;
                        }
                    }
                }
            }

            if (CurLogIdx < 0) CurLogIdx = 0;

            if (!string.IsNullOrEmpty(db.CurrentDbPath) && File.Exists(db.CurrentDbPath))
            {
                SaveLogDB(db.CurrentDbPath);
            }

            StateChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool Redo(EditorDbManager db, out int affectedRecType, out long affectedId)
        {
            affectedRecType = -1;
            affectedId = -1;

            if (!CanRedo) return false;

            int no = Logs[CurLogIdx].LogNo;

            for (; CurLogIdx < Logs.Count; CurLogIdx++)
            {
                var log = Logs[CurLogIdx];
                if (log.LogNo != no) break;

                affectedRecType = log.RecType;

                if (log.RecType == REC_TYPE_KIJUNP && log.Rec is KikaiModel rec)
                {
                    affectedId = rec.Id;
                    var existing = db.KikaiList.FirstOrDefault(k => k.Id == rec.Id);

                    if (log.LogType == LOG_TYPE_NEW) // 新規追加のRedo -> 再追加
                    {
                        if (existing == null) db.KikaiList.Add((KikaiModel)CloneRecord(REC_TYPE_KIJUNP, rec));
                    }
                    else if (log.LogType == LOG_TYPE_DEL) // 削除のRedo -> 削除
                    {
                        if (existing != null) db.KikaiList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_UPD) // 更新のRedo -> 更新後の状態に戻す
                    {
                        if (existing != null)
                        {
                            var currentCopy = (KikaiModel)CloneRecord(REC_TYPE_KIJUNP, existing);
                            CopyKikaiProperties(rec, existing);
                            log.Rec = currentCopy;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(db.CurrentDbPath) && File.Exists(db.CurrentDbPath))
            {
                SaveLogDB(db.CurrentDbPath);
            }

            StateChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        #region SQLite LogTbl Persistence & Recovery

        public void SaveLogDB(string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath)) return;

            try
            {
                using (var conn = new SqliteConnection($"Data Source={dbPath};"))
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = @"
                                CREATE TABLE IF NOT EXISTS 'LogTbl' (
                                    IDX INTEGER,
                                    NO INTEGER,
                                    LOGTYPE INTEGER,
                                    RECTYPE INTEGER,
                                    REC TEXT
                                );
                                DELETE FROM 'LogTbl';
                            ";
                            cmd.ExecuteNonQuery();

                            // 1. ヘッダーレコード (IDX = -1) に現在の状態を保存
                            cmd.CommandText = "INSERT INTO 'LogTbl' (IDX, NO, LOGTYPE, RECTYPE, REC) VALUES (-1, @no, @logType, 0, '');";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@no", CurLogNo);
                            cmd.Parameters.AddWithValue("@logType", CurLogIdx);
                            cmd.ExecuteNonQuery();

                            // 2. 各ログレコードを保存
                            for (int i = 0; i < Logs.Count; i++)
                            {
                                var log = Logs[i];
                                string recStr = Rec2Str(log.RecType, log.Rec);

                                cmd.CommandText = "INSERT INTO 'LogTbl' (IDX, NO, LOGTYPE, RECTYPE, REC) VALUES (@idx, @no, @logType, @recType, @rec);";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@idx", i);
                                cmd.Parameters.AddWithValue("@no", log.LogNo);
                                cmd.Parameters.AddWithValue("@logType", log.LogType);
                                cmd.Parameters.AddWithValue("@recType", log.RecType);
                                cmd.Parameters.AddWithValue("@rec", recStr);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        trans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SaveLogDB] Error: {ex.Message}");
            }
        }

        public void LoadLogDB(string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath)) return;

            Logs.Clear();
            CurLogNo = 0;
            CurLogIdx = 0;

            try
            {
                using (var conn = new SqliteConnection($"Data Source={dbPath};"))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 'LogTbl' (
                                IDX INTEGER,
                                NO INTEGER,
                                LOGTYPE INTEGER,
                                RECTYPE INTEGER,
                                REC TEXT
                            );
                            SELECT IDX, NO, LOGTYPE, RECTYPE, REC FROM 'LogTbl' ORDER BY IDX;
                        ";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idx = reader.GetInt32(0);
                                int no = reader.GetInt32(1);
                                int logType = reader.GetInt32(2);
                                int recType = reader.GetInt32(3);
                                string recStr = reader.IsDBNull(4) ? "" : reader.GetString(4);

                                if (idx == -1) // ヘッダー
                                {
                                    CurLogNo = no;
                                    CurLogIdx = logType;
                                }
                                else
                                {
                                    object? recObj = Str2Rec(recType, recStr);
                                    if (recObj != null)
                                    {
                                        Logs.Add(new LogEntry(no, logType, recType, recObj));
                                        if (CurLogNo < no) CurLogNo = no;
                                    }
                                }
                            }
                        }
                    }
                }

                // 起動・ロード時は index を 0 にして Redo ができる状態にする
                CurLogIdx = 0;

                StateChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoadLogDB] Error: {ex.Message}");
            }
        }

        public void Recover(EditorDbManager db)
        {
            CurLogIdx = 0;
            while (CanRedo)
            {
                Redo(db, out _, out _);
            }
        }

        public void ClearInDatabase(string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath)) return;

            try
            {
                using (var conn = new SqliteConnection($"Data Source={dbPath};"))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            CREATE TABLE IF NOT EXISTS 'LogTbl' (
                                IDX INTEGER,
                                NO INTEGER,
                                LOGTYPE INTEGER,
                                RECTYPE INTEGER,
                                REC TEXT
                            );
                            DELETE FROM 'LogTbl';
                        ";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        #endregion

        #region Serialization Helpers

        public static string Rec2Str(int recType, object rec)
        {
            if (rec == null) return "";
            if (recType == REC_TYPE_KIJUNP && rec is KikaiModel k)
            {
                return JsonSerializer.Serialize(k);
            }
            return "";
        }

        public static object? Str2Rec(int recType, string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            if (recType == REC_TYPE_KIJUNP)
            {
                try { return JsonSerializer.Deserialize<KikaiModel>(str); } catch { return null; }
            }
            return null;
        }

        private static void CopyKikaiProperties(KikaiModel src, KikaiModel dst)
        {
            dst.Name = src.Name;
            dst.X = src.X;
            dst.Y = src.Y;
            dst.Z = src.Z;
            dst.Layer = src.Layer;
            dst.Date = src.Date;
            dst.S = src.S;
            dst.V = src.V;
            dst.H = src.H;
            dst.KPName = src.KPName;
            dst.BPName = src.BPName;
            dst.KPH = src.KPH;
            dst.MRH = src.MRH;
        }

        public static object CloneRecord(int recType, object rec)
        {
            if (recType == REC_TYPE_KIJUNP && rec is KikaiModel k)
            {
                return new KikaiModel
                {
                    Id = k.Id,
                    Name = k.Name,
                    X = k.X,
                    Y = k.Y,
                    Z = k.Z,
                    Layer = k.Layer,
                    Date = k.Date,
                    S = k.S,
                    V = k.V,
                    H = k.H,
                    KPName = k.KPName,
                    BPName = k.BPName,
                    KPH = k.KPH,
                    MRH = k.MRH
                };
            }
            return rec;
        }

        #endregion
    }
}

