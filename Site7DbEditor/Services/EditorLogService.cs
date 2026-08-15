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
        public int RowIndex { get; set; } = -1; // 挿入/削除時の元の行インデックス
        public object? OldRec { get; set; } // 変更前（Undo用 / 削除時のスナップショット）
        public object? NewRec { get; set; } // 変更後（Redo用 / 新規時のスナップショット）

        public LogEntry(int logNo, int logType, int recType, object? oldRec, object? newRec, int rowIndex = -1)
        {
            LogNo = logNo;
            LogType = logType;
            RecType = recType;
            OldRec = oldRec;
            NewRec = newRec;
            RowIndex = rowIndex;
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

        public void Push(int logType, int recType, object rec, object? rec0 = null, string? dbPath = null, int rowIndex = -1, bool autoIncLogNo = true)
        {
            // 未来のRedo履歴を切り捨て
            if (CurLogIdx < Logs.Count)
            {
                Logs.RemoveRange(CurLogIdx, Logs.Count - CurLogIdx);
            }

            object? oldSnapshot = null;
            object? newSnapshot = null;

            if (logType == LOG_TYPE_NEW)
            {
                newSnapshot = CloneRecord(recType, rec);
            }
            else if (logType == LOG_TYPE_DEL)
            {
                oldSnapshot = CloneRecord(recType, rec);
            }
            else if (logType == LOG_TYPE_UPD)
            {
                oldSnapshot = rec0 != null ? CloneRecord(recType, rec0) : null;
                newSnapshot = CloneRecord(recType, rec);
            }

            var entry = new LogEntry(CurLogNo, logType, recType, oldSnapshot, newSnapshot, rowIndex);
            Logs.Add(entry);
            CurLogIdx++;

            if (autoIncLogNo)
            {
                IncLogNo();
            }

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

                if (log.RecType == REC_TYPE_KIJUNP)
                {
                    if (log.LogType == LOG_TYPE_NEW && log.NewRec is KikaiModel newK)
                    {
                        affectedId = newK.Id;
                        var existing = db.KikaiList.FirstOrDefault(k => k.Id == newK.Id);
                        if (existing != null) db.KikaiList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_DEL && log.OldRec is KikaiModel oldK)
                    {
                        affectedId = oldK.Id;
                        var existing = db.KikaiList.FirstOrDefault(k => k.Id == oldK.Id);
                        if (existing == null)
                        {
                            int insertIdx = (log.RowIndex >= 0 && log.RowIndex <= db.KikaiList.Count) ? log.RowIndex : db.KikaiList.Count;
                            db.KikaiList.Insert(insertIdx, (KikaiModel)CloneRecord(REC_TYPE_KIJUNP, oldK));
                        }
                    }
                    else if (log.LogType == LOG_TYPE_UPD && log.OldRec is KikaiModel updOldK)
                    {
                        affectedId = updOldK.Id;
                        var existing = db.KikaiList.FirstOrDefault(k => k.Id == updOldK.Id);
                        if (existing != null)
                        {
                            CopyKikaiProperties(updOldK, existing);
                        }
                    }
                }
                else if (log.RecType == REC_TYPE_IBUTU)
                {
                    if (log.LogType == LOG_TYPE_NEW && log.NewRec is IbutuModel newI)
                    {
                        affectedId = newI.Id;
                        var existing = db.IbutuList.FirstOrDefault(i => i.Id == newI.Id);
                        if (existing != null) db.IbutuList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_DEL && log.OldRec is IbutuModel oldI)
                    {
                        affectedId = oldI.Id;
                        var existing = db.IbutuList.FirstOrDefault(i => i.Id == oldI.Id);
                        if (existing == null)
                        {
                            int insertIdx = (log.RowIndex >= 0 && log.RowIndex <= db.IbutuList.Count) ? log.RowIndex : db.IbutuList.Count;
                            db.IbutuList.Insert(insertIdx, (IbutuModel)CloneRecord(REC_TYPE_IBUTU, oldI));
                        }
                    }
                    else if (log.LogType == LOG_TYPE_UPD && log.OldRec is IbutuModel updOldI)
                    {
                        affectedId = updOldI.Id;
                        var existing = db.IbutuList.FirstOrDefault(i => i.Id == updOldI.Id);
                        if (existing != null)
                        {
                            CopyIbutuProperties(updOldI, existing);
                        }
                    }
                }
                else if (log.RecType == REC_TYPE_IKOU)
                {
                    if (log.LogType == LOG_TYPE_NEW && log.NewRec is IkouModel newIk)
                    {
                        affectedId = newIk.Id;
                        var existing = db.IkouList.FirstOrDefault(ik => ik.Id == newIk.Id);
                        if (existing != null) db.IkouList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_DEL && log.OldRec is IkouModel oldIk)
                    {
                        affectedId = oldIk.Id;
                        var existing = db.IkouList.FirstOrDefault(ik => ik.Id == oldIk.Id);
                        if (existing == null)
                        {
                            int insertIdx = (log.RowIndex >= 0 && log.RowIndex <= db.IkouList.Count) ? log.RowIndex : db.IkouList.Count;
                            db.IkouList.Insert(insertIdx, (IkouModel)CloneRecord(REC_TYPE_IKOU, oldIk));
                        }
                    }
                    else if (log.LogType == LOG_TYPE_UPD && log.OldRec is IkouModel updOldIk)
                    {
                        affectedId = updOldIk.Id;
                        var existing = db.IkouList.FirstOrDefault(ik => ik.Id == updOldIk.Id);
                        if (existing != null)
                        {
                            CopyIkouProperties(updOldIk, existing);
                        }
                    }
                }
                else if (log.RecType == REC_TYPE_IKOUL)
                {
                    if (log.LogType == LOG_TYPE_NEW && log.NewRec is IkouLModel newL)
                    {
                        affectedId = newL.Id;
                        var existing = db.IkouLList.FirstOrDefault(l => l.Id == newL.Id && l.Lid == newL.Lid);
                        if (existing != null) db.IkouLList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_DEL && log.OldRec is IkouLModel oldL)
                    {
                        affectedId = oldL.Id;
                        var existing = db.IkouLList.FirstOrDefault(l => l.Id == oldL.Id && l.Lid == oldL.Lid);
                        if (existing == null)
                        {
                            int insertIdx = (log.RowIndex >= 0 && log.RowIndex <= db.IkouLList.Count) ? log.RowIndex : db.IkouLList.Count;
                            db.IkouLList.Insert(insertIdx, (IkouLModel)CloneRecord(REC_TYPE_IKOUL, oldL));
                        }
                    }
                    else if (log.LogType == LOG_TYPE_UPD && log.OldRec is IkouLModel updOldL)
                    {
                        affectedId = updOldL.Id;
                        var existing = db.IkouLList.FirstOrDefault(l => l.Id == updOldL.Id && l.Lid == updOldL.Lid);
                        if (existing != null)
                        {
                            CopyIkouLProperties(updOldL, existing);
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

                if (log.RecType == REC_TYPE_KIJUNP)
                {
                    if (log.LogType == LOG_TYPE_NEW && log.NewRec is KikaiModel newK)
                    {
                        affectedId = newK.Id;
                        var existing = db.KikaiList.FirstOrDefault(k => k.Id == newK.Id);
                        if (existing == null)
                        {
                            int insertIdx = (log.RowIndex >= 0 && log.RowIndex <= db.KikaiList.Count) ? log.RowIndex : db.KikaiList.Count;
                            db.KikaiList.Insert(insertIdx, (KikaiModel)CloneRecord(REC_TYPE_KIJUNP, newK));
                        }
                    }
                    else if (log.LogType == LOG_TYPE_DEL && log.OldRec is KikaiModel oldK)
                    {
                        affectedId = oldK.Id;
                        var existing = db.KikaiList.FirstOrDefault(k => k.Id == oldK.Id);
                        if (existing != null) db.KikaiList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_UPD && log.NewRec is KikaiModel updNewK)
                    {
                        affectedId = updNewK.Id;
                        var existing = db.KikaiList.FirstOrDefault(k => k.Id == updNewK.Id);
                        if (existing != null)
                        {
                            CopyKikaiProperties(updNewK, existing);
                        }
                    }
                }
                else if (log.RecType == REC_TYPE_IBUTU)
                {
                    if (log.LogType == LOG_TYPE_NEW && log.NewRec is IbutuModel newI)
                    {
                        affectedId = newI.Id;
                        var existing = db.IbutuList.FirstOrDefault(i => i.Id == newI.Id);
                        if (existing == null)
                        {
                            int insertIdx = (log.RowIndex >= 0 && log.RowIndex <= db.IbutuList.Count) ? log.RowIndex : db.IbutuList.Count;
                            db.IbutuList.Insert(insertIdx, (IbutuModel)CloneRecord(REC_TYPE_IBUTU, newI));
                        }
                    }
                    else if (log.LogType == LOG_TYPE_DEL && log.OldRec is IbutuModel oldI)
                    {
                        affectedId = oldI.Id;
                        var existing = db.IbutuList.FirstOrDefault(i => i.Id == oldI.Id);
                        if (existing != null) db.IbutuList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_UPD && log.NewRec is IbutuModel updNewI)
                    {
                        affectedId = updNewI.Id;
                        var existing = db.IbutuList.FirstOrDefault(i => i.Id == updNewI.Id);
                        if (existing != null)
                        {
                            CopyIbutuProperties(updNewI, existing);
                        }
                    }
                }
                else if (log.RecType == REC_TYPE_IKOU)
                {
                    if (log.LogType == LOG_TYPE_NEW && log.NewRec is IkouModel newIk)
                    {
                        affectedId = newIk.Id;
                        var existing = db.IkouList.FirstOrDefault(ik => ik.Id == newIk.Id);
                        if (existing == null)
                        {
                            int insertIdx = (log.RowIndex >= 0 && log.RowIndex <= db.IkouList.Count) ? log.RowIndex : db.IkouList.Count;
                            db.IkouList.Insert(insertIdx, (IkouModel)CloneRecord(REC_TYPE_IKOU, newIk));
                        }
                    }
                    else if (log.LogType == LOG_TYPE_DEL && log.OldRec is IkouModel oldIk)
                    {
                        affectedId = oldIk.Id;
                        var existing = db.IkouList.FirstOrDefault(ik => ik.Id == oldIk.Id);
                        if (existing != null) db.IkouList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_UPD && log.NewRec is IkouModel updNewIk)
                    {
                        affectedId = updNewIk.Id;
                        var existing = db.IkouList.FirstOrDefault(ik => ik.Id == updNewIk.Id);
                        if (existing != null)
                        {
                            CopyIkouProperties(updNewIk, existing);
                        }
                    }
                }
                else if (log.RecType == REC_TYPE_IKOUL)
                {
                    if (log.LogType == LOG_TYPE_NEW && log.NewRec is IkouLModel newL)
                    {
                        affectedId = newL.Id;
                        var existing = db.IkouLList.FirstOrDefault(l => l.Id == newL.Id && l.Lid == newL.Lid);
                        if (existing == null)
                        {
                            int insertIdx = (log.RowIndex >= 0 && log.RowIndex <= db.IkouLList.Count) ? log.RowIndex : db.IkouLList.Count;
                            db.IkouLList.Insert(insertIdx, (IkouLModel)CloneRecord(REC_TYPE_IKOUL, newL));
                        }
                    }
                    else if (log.LogType == LOG_TYPE_DEL && log.OldRec is IkouLModel oldL)
                    {
                        affectedId = oldL.Id;
                        var existing = db.IkouLList.FirstOrDefault(l => l.Id == oldL.Id && l.Lid == oldL.Lid);
                        if (existing != null) db.IkouLList.Remove(existing);
                    }
                    else if (log.LogType == LOG_TYPE_UPD && log.NewRec is IkouLModel updNewL)
                    {
                        affectedId = updNewL.Id;
                        var existing = db.IkouLList.FirstOrDefault(l => l.Id == updNewL.Id && l.Lid == updNewL.Lid);
                        if (existing != null)
                        {
                            CopyIkouLProperties(updNewL, existing);
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
                                object? targetRec = (log.LogType == LOG_TYPE_UPD) ? (log.NewRec ?? log.OldRec) : (log.NewRec ?? log.OldRec);
                                string recStr = Rec2Str(log.RecType, targetRec, log.RowIndex);

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

        public void LoadLogDB(string dbPath, EditorDbManager? db = null)
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
                                    var (recObj, rowIndex) = Str2Rec(recType, recStr);
                                    if (recObj != null)
                                    {
                                        object? oldRec = null;
                                        object? newRec = null;

                                        if (logType == LOG_TYPE_NEW)
                                        {
                                            newRec = recObj;
                                        }
                                        else if (logType == LOG_TYPE_DEL)
                                        {
                                            oldRec = recObj;
                                        }
                                        else if (logType == LOG_TYPE_UPD)
                                        {
                                            newRec = recObj; // LogTbl に入っているのは更新後の値
                                            if (db != null)
                                            {
                                                if (recType == REC_TYPE_KIJUNP && recObj is KikaiModel k)
                                                {
                                                    var existing = db.KikaiList.FirstOrDefault(item => item.Id == k.Id);
                                                    if (existing != null)
                                                    {
                                                        oldRec = CloneRecord(recType, existing);
                                                    }
                                                }
                                                else if (recType == REC_TYPE_IBUTU && recObj is IbutuModel ib)
                                                {
                                                    var existing = db.IbutuList.FirstOrDefault(item => item.Id == ib.Id);
                                                    if (existing != null)
                                                    {
                                                        oldRec = CloneRecord(recType, existing);
                                                    }
                                                }
                                                else if (recType == REC_TYPE_IKOU && recObj is IkouModel ik)
                                                {
                                                    var existing = db.IkouList.FirstOrDefault(item => item.Id == ik.Id);
                                                    if (existing != null)
                                                    {
                                                        oldRec = CloneRecord(recType, existing);
                                                    }
                                                }
                                                else if (recType == REC_TYPE_IKOUL && recObj is IkouLModel l)
                                                {
                                                    var existing = db.IkouLList.FirstOrDefault(item => item.Id == l.Id && item.Lid == l.Lid);
                                                    if (existing != null)
                                                    {
                                                        oldRec = CloneRecord(recType, existing);
                                                    }
                                                }
                                            }
                                        }

                                        Logs.Add(new LogEntry(no, logType, recType, oldRec, newRec, rowIndex));
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

        public static string Rec2Str(int recType, object? rec, int rowIndex = -1)
        {
            if (rec == null) return "";
            try
            {
                var envelope = new
                {
                    RowIndex = rowIndex,
                    Data = rec
                };
                return JsonSerializer.Serialize(envelope);
            }
            catch
            {
                return "";
            }
        }

        public static (object? Rec, int RowIndex) Str2Rec(int recType, string str)
        {
            if (string.IsNullOrEmpty(str)) return (null, -1);
            try
            {
                using (var doc = JsonDocument.Parse(str))
                {
                    var root = doc.RootElement;
                    int rowIndex = root.TryGetProperty("RowIndex", out var rowProp) ? rowProp.GetInt32() : -1;
                    if (root.TryGetProperty("Data", out var dataProp))
                    {
                        string dataJson = dataProp.GetRawText();
                        if (recType == REC_TYPE_KIJUNP)
                        {
                            var model = JsonSerializer.Deserialize<KikaiModel>(dataJson);
                            return (model, rowIndex);
                        }
                        else if (recType == REC_TYPE_IBUTU)
                        {
                            var model = JsonSerializer.Deserialize<IbutuModel>(dataJson);
                            return (model, rowIndex);
                        }
                        else if (recType == REC_TYPE_IKOU)
                        {
                            var model = JsonSerializer.Deserialize<IkouModel>(dataJson);
                            return (model, rowIndex);
                        }
                        else if (recType == REC_TYPE_IKOUL)
                        {
                            var model = JsonSerializer.Deserialize<IkouLModel>(dataJson);
                            return (model, rowIndex);
                        }
                    }
                }
            }
            catch
            {
                try
                {
                    if (recType == REC_TYPE_KIJUNP)
                    {
                        return (JsonSerializer.Deserialize<KikaiModel>(str), -1);
                    }
                    else if (recType == REC_TYPE_IBUTU)
                    {
                        return (JsonSerializer.Deserialize<IbutuModel>(str), -1);
                    }
                    else if (recType == REC_TYPE_IKOU)
                    {
                        return (JsonSerializer.Deserialize<IkouModel>(str), -1);
                    }
                    else if (recType == REC_TYPE_IKOUL)
                    {
                        return (JsonSerializer.Deserialize<IkouLModel>(str), -1);
                    }
                }
                catch { }
            }
            return (null, -1);
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

        private static void CopyIbutuProperties(IbutuModel src, IbutuModel dst)
        {
            dst.Chiku = src.Chiku;
            dst.Soui = src.Soui;
            dst.Syubetu = src.Syubetu;
            dst.No = src.No;
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

        private static void CopyIkouProperties(IkouModel src, IkouModel dst)
        {
            dst.Name = src.Name;
            dst.X = src.X;
            dst.Y = src.Y;
            dst.Z = src.Z;
            dst.Date = src.Date;
        }

        private static void CopyIkouLProperties(IkouLModel src, IkouLModel dst)
        {
            dst.Name = src.Name;
            dst.Mode = src.Mode;
            dst.X = src.X;
            dst.Y = src.Y;
            dst.Z = src.Z;
            dst.Layer = src.Layer;
            dst.Date = src.Date;
            dst.Precs = src.Precs;
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
            else if (recType == REC_TYPE_IBUTU && rec is IbutuModel ib)
            {
                return new IbutuModel
                {
                    Id = ib.Id,
                    Chiku = ib.Chiku,
                    Soui = ib.Soui,
                    Syubetu = ib.Syubetu,
                    No = ib.No,
                    X = ib.X,
                    Y = ib.Y,
                    Z = ib.Z,
                    Layer = ib.Layer,
                    Date = ib.Date,
                    S = ib.S,
                    V = ib.V,
                    H = ib.H,
                    KPName = ib.KPName,
                    BPName = ib.BPName,
                    KPH = ib.KPH,
                    MRH = ib.MRH
                };
            }
            else if (recType == REC_TYPE_IKOU && rec is IkouModel ik)
            {
                return new IkouModel
                {
                    Id = ik.Id,
                    Name = ik.Name,
                    X = ik.X,
                    Y = ik.Y,
                    Z = ik.Z,
                    Date = ik.Date
                };
            }
            else if (recType == REC_TYPE_IKOUL && rec is IkouLModel l)
            {
                return new IkouLModel
                {
                    Id = l.Id,
                    Lid = l.Lid,
                    Name = l.Name,
                    Mode = l.Mode,
                    X = l.X,
                    Y = l.Y,
                    Z = l.Z,
                    Layer = l.Layer,
                    Date = l.Date,
                    Precs = l.Precs
                };
            }
            return rec;
        }

        #endregion
    }
}
