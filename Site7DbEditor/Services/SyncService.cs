using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace Site7DbEditor.Services
{
    public class OutdoorDeviceCandidate
    {
        public string DriveLetter { get; set; } = "";
        public string DriveLabel { get; set; } = "";
        public DriveType DriveType { get; set; }
        public double TotalSizeGb { get; set; }
        public double FreeSpaceGb { get; set; }
        public string SiteFolderPath { get; set; } = "";
        public string DbFilePath { get; set; } = "";
        public string SiteName { get; set; } = "";
        public DateTime UpdatedAt { get; set; }

        public string DisplayText
        {
            get
            {
                string driveInfo = !string.IsNullOrEmpty(DriveLetter)
                    ? $"[{DriveLetter}] {DriveLabel}"
                    : "[外部フォルダ]";
                return $"{driveInfo} - 現場: {SiteName} ({UpdatedAt:yyyy/MM/dd HH:mm})";
            }
        }
    }

    public enum SyncDiffType
    {
        New,        // 外業にのみ存在（新規追加）
        Updated,    // 同一キーで外業側が更新されている
        Same,       // 完全一致（変更なし）
        IndoorOnly  // 内業にのみ存在
    }

    public class SyncItemDiff
    {
        public string EntityType { get; set; } = ""; // 遺構 / 遺構L / 遺物 / 基準点
        public long Id { get; set; }
        public long SubId { get; set; } = 0;
        public string Name { get; set; } = "";
        public SyncDiffType DiffType { get; set; }
        public string IndoorSummary { get; set; } = "";
        public string OutdoorSummary { get; set; } = "";

        public string DiffTypeDisplay => DiffType switch
        {
            SyncDiffType.New => "➕ 新規",
            SyncDiffType.Updated => "✏ 更新",
            SyncDiffType.Same => "＝ 同一",
            SyncDiffType.IndoorOnly => "─ 内業のみ",
            _ => "？"
        };
    }

    public class SyncDiffSummary
    {
        public string IndoorDbPath { get; set; } = "";
        public string OutdoorDbPath { get; set; } = "";
        public DateTime IndoorUpdatedAt { get; set; }
        public DateTime OutdoorUpdatedAt { get; set; }

        public int IkouNewCount { get; set; }
        public int IkouUpdatedCount { get; set; }
        public int IkouSameCount { get; set; }

        public int IkouLNewCount { get; set; }
        public int IkouLUpdatedCount { get; set; }
        public int IkouLSameCount { get; set; }

        public int IbutuNewCount { get; set; }
        public int IbutuUpdatedCount { get; set; }
        public int IbutuSameCount { get; set; }

        public int KikaiNewCount { get; set; }
        public int KikaiUpdatedCount { get; set; }
        public int KikaiSameCount { get; set; }

        public int TotalNewCount => IkouNewCount + IkouLNewCount + IbutuNewCount + KikaiNewCount;
        public int TotalUpdatedCount => IkouUpdatedCount + IkouLUpdatedCount + IbutuUpdatedCount + KikaiUpdatedCount;
        public int TotalChangesCount => TotalNewCount + TotalUpdatedCount;

        public List<SyncItemDiff> DiffItems { get; } = new List<SyncItemDiff>();
    }

    public class SyncResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? BackupFilePath { get; set; }
        public int IkouMergedCount { get; set; }
        public int IkouLMergedCount { get; set; }
        public int IbutuMergedCount { get; set; }
        public int KikaiMergedCount { get; set; }

        public int TotalMergedCount => IkouMergedCount + IkouLMergedCount + IbutuMergedCount + KikaiMergedCount;

        public string SummaryText => Success
            ? $"✔ 同期完了 (マージ合計: {TotalMergedCount}件)\n" +
              $"・遺構: {IkouMergedCount}件\n" +
              $"・遺構線(測点): {IkouLMergedCount}件\n" +
              $"・遺物: {IbutuMergedCount}件\n" +
              $"・基準点: {KikaiMergedCount}件" +
              (!string.IsNullOrEmpty(BackupFilePath) ? $"\n\n※ 自動バックアップ作成: {Path.GetFileName(BackupFilePath)}" : "")
            : $"❌ 同期エラー: {ErrorMessage}";
    }

    public static class SyncService
    {
        /// <summary>
        /// USBドライブ等の外部メディアから指定現場または利用可能な外業現場候補を検索します。
        /// </summary>
        public static List<OutdoorDeviceCandidate> FindOutdoorCandidates(string? targetSiteName = null)
        {
            var results = new List<OutdoorDeviceCandidate>();

            DriveInfo[] drives;
            try
            {
                drives = DriveInfo.GetDrives().Where(d => d.IsReady).ToArray();
            }
            catch
            {
                return results;
            }

            foreach (var drive in drives)
            {
                try
                {
                    double totalGb = Math.Round((double)drive.TotalSize / (1024 * 1024 * 1024), 1);
                    double freeGb = Math.Round((double)drive.AvailableFreeSpace / (1024 * 1024 * 1024), 1);
                    string driveLabel = !string.IsNullOrEmpty(drive.VolumeLabel) ? drive.VolumeLabel : "リムーバブル ディスク";

                    // 探索候補フォルダ
                    var searchRoots = new List<string>
                    {
                        Path.Combine(drive.RootDirectory.FullName, "SITE7", "GENBA", "DATA"),
                        Path.Combine(drive.RootDirectory.FullName, "SITE7"),
                        drive.RootDirectory.FullName
                    };

                    foreach (var root in searchRoots)
                    {
                        if (!Directory.Exists(root)) continue;

                        // 1. targetSiteName が一致するフォルダを優先探索
                        if (!string.IsNullOrEmpty(targetSiteName))
                        {
                            string directPath = Path.Combine(root, targetSiteName);
                            if (Directory.Exists(directPath))
                            {
                                AddCandidateIfValid(results, drive, directPath, targetSiteName, totalGb, freeGb, driveLabel);
                            }
                        }

                        // 2. 直下のサブディレクトリを探索
                        try
                        {
                            var dirs = Directory.GetDirectories(root);
                            foreach (var dir in dirs)
                            {
                                string siteName = Path.GetFileName(dir);
                                AddCandidateIfValid(results, drive, dir, siteName, totalGb, freeGb, driveLabel);
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }

            // 優先度ソート: 目的の現場名一致 > リムーバブルドライブ > 更新日時降順
            return results
                .DistinctBy(c => c.DbFilePath)
                .OrderByDescending(c => !string.IsNullOrEmpty(targetSiteName) && c.SiteName.Equals(targetSiteName, StringComparison.OrdinalIgnoreCase))
                .ThenByDescending(c => c.DriveType == DriveType.Removable)
                .ThenByDescending(c => c.UpdatedAt)
                .ToList();
        }

        private static void AddCandidateIfValid(
            List<OutdoorDeviceCandidate> list,
            DriveInfo drive,
            string folderPath,
            string siteName,
            double totalGb,
            double freeGb,
            string driveLabel)
        {
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath)) return;

            string[] dbNames = new[] { "Site7.db", "SITE7.db", "Site7.db3", "SITE7.db3" };
            string? foundDb = null;
            foreach (var name in dbNames)
            {
                string p = Path.Combine(folderPath, name);
                if (File.Exists(p))
                {
                    foundDb = p;
                    break;
                }
            }

            if (foundDb == null)
            {
                // *.db または *.sqlite を探索
                try
                {
                    var dbs = Directory.GetFiles(folderPath, "*.db")
                        .Concat(Directory.GetFiles(folderPath, "*.sqlite"))
                        .ToArray();
                    if (dbs.Length > 0) foundDb = dbs[0];
                }
                catch { }
            }

            if (foundDb != null)
            {
                var fi = new FileInfo(foundDb);
                list.Add(new OutdoorDeviceCandidate
                {
                    DriveLetter = drive.Name.TrimEnd('\\'),
                    DriveLabel = driveLabel,
                    DriveType = drive.DriveType,
                    TotalSizeGb = totalGb,
                    FreeSpaceGb = freeGb,
                    SiteFolderPath = folderPath,
                    DbFilePath = foundDb,
                    SiteName = siteName,
                    UpdatedAt = fi.LastWriteTime
                });
            }
        }

        /// <summary>
        /// 内業DBと外業DBを比較し、差分情報を抽出します。
        /// </summary>
        public static SyncDiffSummary CompareDatabases(string indoorDbPath, string outdoorDbPath)
        {
            var summary = new SyncDiffSummary
            {
                IndoorDbPath = indoorDbPath,
                OutdoorDbPath = outdoorDbPath,
                IndoorUpdatedAt = File.Exists(indoorDbPath) ? new FileInfo(indoorDbPath).LastWriteTime : DateTime.MinValue,
                OutdoorUpdatedAt = File.Exists(outdoorDbPath) ? new FileInfo(outdoorDbPath).LastWriteTime : DateTime.MinValue
            };

            if (!File.Exists(indoorDbPath) || !File.Exists(outdoorDbPath))
            {
                return summary;
            }

            // 内業DB読込
            var indoorDb = new EditorDbManager();
            indoorDb.LoadDatabase(indoorDbPath);

            // 外業DB読込
            var outdoorDb = new EditorDbManager();
            outdoorDb.LoadDatabase(outdoorDbPath);

            // 1. 遺構（Ikou）の差分比較
            var indoorIkouMap = indoorDb.IkouList.ToDictionary(x => x.Id);
            foreach (var outIkou in outdoorDb.IkouList)
            {
                if (!indoorIkouMap.TryGetValue(outIkou.Id, out var inIkou))
                {
                    summary.IkouNewCount++;
                    summary.DiffItems.Add(new SyncItemDiff
                    {
                        EntityType = "遺構",
                        Id = outIkou.Id,
                        Name = outIkou.Name,
                        DiffType = SyncDiffType.New,
                        OutdoorSummary = $"ID:{outIkou.Id} [{outIkou.Name}] (X:{outIkou.X:F2}, Y:{outIkou.Y:F2}, Z:{outIkou.Z:F2})"
                    });
                }
                else
                {
                    bool isDiff = inIkou.Name != outIkou.Name ||
                                 Math.Abs(inIkou.X - outIkou.X) > 0.001 ||
                                 Math.Abs(inIkou.Y - outIkou.Y) > 0.001 ||
                                 Math.Abs(inIkou.Z - outIkou.Z) > 0.001 ||
                                 inIkou.Date != outIkou.Date;

                    if (isDiff)
                    {
                        summary.IkouUpdatedCount++;
                        summary.DiffItems.Add(new SyncItemDiff
                        {
                            EntityType = "遺構",
                            Id = outIkou.Id,
                            Name = outIkou.Name,
                            DiffType = SyncDiffType.Updated,
                            IndoorSummary = $"[{inIkou.Name}] (X:{inIkou.X:F2}, Y:{inIkou.Y:F2})",
                            OutdoorSummary = $"[{outIkou.Name}] (X:{outIkou.X:F2}, Y:{outIkou.Y:F2})"
                        });
                    }
                    else
                    {
                        summary.IkouSameCount++;
                    }
                }
            }

            // 2. 遺構L（IkouL: 測点・ライン）の差分比較
            var indoorIkouLMap = indoorDb.IkouLList.ToDictionary(x => (x.Id, x.Lid));
            foreach (var outLine in outdoorDb.IkouLList)
            {
                if (!indoorIkouLMap.TryGetValue((outLine.Id, outLine.Lid), out var inLine))
                {
                    summary.IkouLNewCount++;
                    summary.DiffItems.Add(new SyncItemDiff
                    {
                        EntityType = "遺構線",
                        Id = outLine.Id,
                        SubId = outLine.Lid,
                        Name = outLine.Name,
                        DiffType = SyncDiffType.New,
                        OutdoorSummary = $"遺構ID:{outLine.Id} 点:{outLine.Lid} [{outLine.Name}] (X:{outLine.X:F2}, Y:{outLine.Y:F2})"
                    });
                }
                else
                {
                    bool isDiff = inLine.Name != outLine.Name ||
                                 inLine.Mode != outLine.Mode ||
                                 inLine.Layer != outLine.Layer ||
                                 Math.Abs(inLine.X - outLine.X) > 0.001 ||
                                 Math.Abs(inLine.Y - outLine.Y) > 0.001 ||
                                 Math.Abs(inLine.Z - outLine.Z) > 0.001 ||
                                 inLine.Precs != outLine.Precs;

                    if (isDiff)
                    {
                        summary.IkouLUpdatedCount++;
                        summary.DiffItems.Add(new SyncItemDiff
                        {
                            EntityType = "遺構線",
                            Id = outLine.Id,
                            SubId = outLine.Lid,
                            Name = outLine.Name,
                            DiffType = SyncDiffType.Updated,
                            IndoorSummary = $"点:{inLine.Lid} [{inLine.Name}] (X:{inLine.X:F2}, Y:{inLine.Y:F2})",
                            OutdoorSummary = $"点:{outLine.Lid} [{outLine.Name}] (X:{outLine.X:F2}, Y:{outLine.Y:F2})"
                        });
                    }
                    else
                    {
                        summary.IkouLSameCount++;
                    }
                }
            }

            // 3. 遺物（Ibutu）の差分比較
            var indoorIbutuMap = indoorDb.IbutuList.ToDictionary(x => x.Id);
            foreach (var outIbutu in outdoorDb.IbutuList)
            {
                if (!indoorIbutuMap.TryGetValue(outIbutu.Id, out var inIbutu))
                {
                    summary.IbutuNewCount++;
                    summary.DiffItems.Add(new SyncItemDiff
                    {
                        EntityType = "遺物",
                        Id = outIbutu.Id,
                        Name = $"{outIbutu.Syubetu} No.{outIbutu.No}",
                        DiffType = SyncDiffType.New,
                        OutdoorSummary = $"ID:{outIbutu.Id} [{outIbutu.Chiku}-{outIbutu.Soui}-{outIbutu.Syubetu}-{outIbutu.No}] (X:{outIbutu.X:F2}, Y:{outIbutu.Y:F2})"
                    });
                }
                else
                {
                    bool isDiff = inIbutu.Chiku != outIbutu.Chiku ||
                                 inIbutu.Soui != outIbutu.Soui ||
                                 inIbutu.Syubetu != outIbutu.Syubetu ||
                                 inIbutu.No != outIbutu.No ||
                                 Math.Abs(inIbutu.X - outIbutu.X) > 0.001 ||
                                 Math.Abs(inIbutu.Y - outIbutu.Y) > 0.001 ||
                                 Math.Abs(inIbutu.Z - outIbutu.Z) > 0.001;

                    if (isDiff)
                    {
                        summary.IbutuUpdatedCount++;
                        summary.DiffItems.Add(new SyncItemDiff
                        {
                            EntityType = "遺物",
                            Id = outIbutu.Id,
                            Name = $"{outIbutu.Syubetu} No.{outIbutu.No}",
                            DiffType = SyncDiffType.Updated,
                            IndoorSummary = $"[{inIbutu.Syubetu} No.{inIbutu.No}] (X:{inIbutu.X:F2}, Y:{inIbutu.Y:F2})",
                            OutdoorSummary = $"[{outIbutu.Syubetu} No.{outIbutu.No}] (X:{outIbutu.X:F2}, Y:{outIbutu.Y:F2})"
                        });
                    }
                    else
                    {
                        summary.IbutuSameCount++;
                    }
                }
            }

            // 4. 基準点（Kikai）の差分比較
            var indoorKikaiMap = indoorDb.KikaiList.ToDictionary(x => x.Id);
            foreach (var outKikai in outdoorDb.KikaiList)
            {
                if (!indoorKikaiMap.TryGetValue(outKikai.Id, out var inKikai))
                {
                    summary.KikaiNewCount++;
                    summary.DiffItems.Add(new SyncItemDiff
                    {
                        EntityType = "基準点",
                        Id = outKikai.Id,
                        Name = outKikai.Name,
                        DiffType = SyncDiffType.New,
                        OutdoorSummary = $"ID:{outKikai.Id} [{outKikai.Name}] (X:{outKikai.X:F2}, Y:{outKikai.Y:F2}, Z:{outKikai.Z:F2})"
                    });
                }
                else
                {
                    bool isDiff = inKikai.Name != outKikai.Name ||
                                 Math.Abs(inKikai.X - outKikai.X) > 0.001 ||
                                 Math.Abs(inKikai.Y - outKikai.Y) > 0.001 ||
                                 Math.Abs(inKikai.Z - outKikai.Z) > 0.001;

                    if (isDiff)
                    {
                        summary.KikaiUpdatedCount++;
                        summary.DiffItems.Add(new SyncItemDiff
                        {
                            EntityType = "基準点",
                            Id = outKikai.Id,
                            Name = outKikai.Name,
                            DiffType = SyncDiffType.Updated,
                            IndoorSummary = $"[{inKikai.Name}] (X:{inKikai.X:F2}, Y:{inKikai.Y:F2})",
                            OutdoorSummary = $"[{outKikai.Name}] (X:{outKikai.X:F2}, Y:{outKikai.Y:F2})"
                        });
                    }
                    else
                    {
                        summary.KikaiSameCount++;
                    }
                }
            }

            return summary;
        }

        /// <summary>
        /// 外業DBのデータを内業DBへマージ同期します。
        /// </summary>
        public static SyncResult ExecuteSync(
            string indoorDbPath,
            string outdoorDbPath,
            bool createBackup = true,
            Action<string, int>? progressCallback = null)
        {
            var result = new SyncResult();

            if (!File.Exists(indoorDbPath))
            {
                result.Success = false;
                result.ErrorMessage = "内業データベースファイルが見つかりません。";
                return result;
            }

            if (!File.Exists(outdoorDbPath))
            {
                result.Success = false;
                result.ErrorMessage = "外業データベースファイルが見つかりません。";
                return result;
            }

            try
            {
                // 1. 自動バックアップ
                if (createBackup)
                {
                    progressCallback?.Invoke("内業DBのバックアップを作成中...", 10);
                    string? dir = Path.GetDirectoryName(indoorDbPath);
                    string baseName = Path.GetFileNameWithoutExtension(indoorDbPath);
                    string backupFile = Path.Combine(dir ?? "", $"{baseName}_backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");
                    File.Copy(indoorDbPath, backupFile, overwrite: true);
                    result.BackupFilePath = backupFile;
                }

                // 2. 外業データの読み込み
                progressCallback?.Invoke("外業データを読込中...", 25);
                var outdoorDb = new EditorDbManager();
                outdoorDb.LoadDatabase(outdoorDbPath);

                // 3. 内業DBへトランザクションでUPSERTマージ
                progressCallback?.Invoke("内業DBへデータをマージ中...", 50);
                string connStr = $"Data Source={indoorDbPath};";

                using (var conn = new SqliteConnection(connStr))
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        // 3-1. 遺構のマージ (UPSERT)
                        using (var cmd = new SqliteCommand(@"
                            INSERT INTO '遺構' (ID, NAME, X, Y, Z, DATE)
                            VALUES (@id, @name, @x, @y, @z, @date)
                            ON CONFLICT(ID) DO UPDATE SET
                                NAME = excluded.NAME,
                                X = excluded.X,
                                Y = excluded.Y,
                                Z = excluded.Z,
                                DATE = excluded.DATE;
                        ", conn, trans))
                        {
                            var pId = cmd.Parameters.Add("@id", SqliteType.Integer);
                            var pName = cmd.Parameters.Add("@name", SqliteType.Text);
                            var pX = cmd.Parameters.Add("@x", SqliteType.Real);
                            var pY = cmd.Parameters.Add("@y", SqliteType.Real);
                            var pZ = cmd.Parameters.Add("@z", SqliteType.Real);
                            var pDate = cmd.Parameters.Add("@date", SqliteType.Text);

                            foreach (var item in outdoorDb.IkouList)
                            {
                                pId.Value = item.Id;
                                pName.Value = item.Name ?? "";
                                pX.Value = Math.Round(item.X, 3);
                                pY.Value = Math.Round(item.Y, 3);
                                pZ.Value = Math.Round(item.Z, 3);
                                pDate.Value = item.Date ?? "";
                                cmd.ExecuteNonQuery();
                                result.IkouMergedCount++;
                            }
                        }

                        // 3-2. 遺構Lのマージ (UPSERT)
                        using (var cmd = new SqliteCommand(@"
                            INSERT INTO '遺構L' (ID, LID, NAME, MODE, X, Y, Z, LAYER, DATE, PRECS)
                            VALUES (@id, @lid, @name, @mode, @x, @y, @z, @layer, @date, @precs)
                            ON CONFLICT(ID, LID) DO UPDATE SET
                                NAME = excluded.NAME,
                                MODE = excluded.MODE,
                                X = excluded.X,
                                Y = excluded.Y,
                                Z = excluded.Z,
                                LAYER = excluded.LAYER,
                                DATE = excluded.DATE,
                                PRECS = excluded.PRECS;
                        ", conn, trans))
                        {
                            var pId = cmd.Parameters.Add("@id", SqliteType.Integer);
                            var pLid = cmd.Parameters.Add("@lid", SqliteType.Integer);
                            var pName = cmd.Parameters.Add("@name", SqliteType.Text);
                            var pMode = cmd.Parameters.Add("@mode", SqliteType.Integer);
                            var pX = cmd.Parameters.Add("@x", SqliteType.Real);
                            var pY = cmd.Parameters.Add("@y", SqliteType.Real);
                            var pZ = cmd.Parameters.Add("@z", SqliteType.Real);
                            var pLayer = cmd.Parameters.Add("@layer", SqliteType.Integer);
                            var pDate = cmd.Parameters.Add("@date", SqliteType.Text);
                            var pPrecs = cmd.Parameters.Add("@precs", SqliteType.Text);

                            foreach (var item in outdoorDb.IkouLList)
                            {
                                pId.Value = item.Id;
                                pLid.Value = item.Lid;
                                pName.Value = item.Name ?? "";
                                pMode.Value = item.Mode;
                                pX.Value = Math.Round(item.X, 3);
                                pY.Value = Math.Round(item.Y, 3);
                                pZ.Value = Math.Round(item.Z, 3);
                                pLayer.Value = item.Layer;
                                pDate.Value = item.Date ?? "";
                                pPrecs.Value = item.Precs ?? "";
                                cmd.ExecuteNonQuery();
                                result.IkouLMergedCount++;
                            }
                        }

                        // 3-3. 遺物のマージ (UPSERT)
                        using (var cmd = new SqliteCommand(@"
                            INSERT INTO '遺物' (ID, CHIKU, SOUI, SYUBETU, No, X, Y, Z, LAYER, DATE, S, V, H, KPNAME, BPNAME, KPH, MRH)
                            VALUES (@id, @chiku, @soui, @syubetu, @no, @x, @y, @z, @layer, @date, @s, @v, @h, @kpname, @bpname, @kph, @mrh)
                            ON CONFLICT(ID) DO UPDATE SET
                                CHIKU = excluded.CHIKU,
                                SOUI = excluded.SOUI,
                                SYUBETU = excluded.SYUBETU,
                                No = excluded.No,
                                X = excluded.X,
                                Y = excluded.Y,
                                Z = excluded.Z,
                                LAYER = excluded.LAYER,
                                DATE = excluded.DATE,
                                S = excluded.S,
                                V = excluded.V,
                                H = excluded.H,
                                KPNAME = excluded.KPNAME,
                                BPNAME = excluded.BPNAME,
                                KPH = excluded.KPH,
                                MRH = excluded.MRH;
                        ", conn, trans))
                        {
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

                            foreach (var item in outdoorDb.IbutuList)
                            {
                                pId.Value = item.Id;
                                pChiku.Value = item.Chiku ?? "";
                                pSoui.Value = item.Soui ?? "";
                                pSyubetu.Value = item.Syubetu ?? "";
                                pNo.Value = item.No;
                                pX.Value = Math.Round(item.X, 3);
                                pY.Value = Math.Round(item.Y, 3);
                                pZ.Value = Math.Round(item.Z, 3);
                                pLayer.Value = item.Layer;
                                pDate.Value = item.Date ?? "";
                                pS.Value = Math.Round(item.S, 3);
                                pV.Value = item.V;
                                pH.Value = item.H;
                                pKp.Value = item.KPName ?? "";
                                pBp.Value = item.BPName ?? "";
                                pKph.Value = Math.Round(item.KPH, 3);
                                pMrh.Value = Math.Round(item.MRH, 3);
                                cmd.ExecuteNonQuery();
                                result.IbutuMergedCount++;
                            }
                        }

                        // 3-4. 基準点のマージ (UPSERT)
                        try
                        {
                            using (var cmd = new SqliteCommand(@"
                                INSERT INTO '基準点' (ID, NAME, X, Y, Z, LAYER, DATE, S, V, H, KPNAME, BPNAME, KPH, MRH)
                                VALUES (@id, @name, @x, @y, @z, @layer, @date, @s, @v, @h, @kpname, @bpname, @kph, @mrh)
                                ON CONFLICT(ID) DO UPDATE SET
                                    NAME = excluded.NAME,
                                    X = excluded.X,
                                    Y = excluded.Y,
                                    Z = excluded.Z,
                                    LAYER = excluded.LAYER,
                                    DATE = excluded.DATE,
                                    S = excluded.S,
                                    V = excluded.V,
                                    H = excluded.H,
                                    KPNAME = excluded.KPNAME,
                                    BPNAME = excluded.BPNAME,
                                    KPH = excluded.KPH,
                                    MRH = excluded.MRH;
                            ", conn, trans))
                            {
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

                                foreach (var item in outdoorDb.KikaiList)
                                {
                                    pId.Value = item.Id;
                                    pName.Value = item.Name ?? "";
                                    pX.Value = Math.Round(item.X, 3);
                                    pY.Value = Math.Round(item.Y, 3);
                                    pZ.Value = Math.Round(item.Z, 3);
                                    pLayer.Value = item.Layer;
                                    pDate.Value = item.Date ?? "";
                                    pS.Value = Math.Round(item.S, 3);
                                    pV.Value = item.V;
                                    pH.Value = item.H;
                                    pKp.Value = item.KPName ?? "";
                                    pBp.Value = item.BPName ?? "";
                                    pKph.Value = Math.Round(item.KPH, 3);
                                    pMrh.Value = Math.Round(item.MRH, 3);
                                    cmd.ExecuteNonQuery();
                                    result.KikaiMergedCount++;
                                }
                            }
                        }
                        catch { }

                        trans.Commit();
                    }
                }

                // 4. 定義ファイル（Def, INI）の同期（外業側に存在する場合）
                progressCallback?.Invoke("現場定義ファイルを同期中...", 85);
                SyncDirectoryFiles(
                    Path.Combine(Path.GetDirectoryName(outdoorDbPath) ?? "", "Def"),
                    Path.Combine(Path.GetDirectoryName(indoorDbPath) ?? "", "Def"));

                // 5. サムネイルの再生成
                progressCallback?.Invoke("現場サムネイルを更新中...", 95);
                try
                {
                    var updatedIndoorDb = new EditorDbManager();
                    updatedIndoorDb.LoadDatabase(indoorDbPath);
                    EditorMapRenderer.SaveThumbnail(indoorDbPath, updatedIndoorDb);
                }
                catch { }

                progressCallback?.Invoke("同期完了", 100);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private static void SyncDirectoryFiles(string sourceDir, string targetDir)
        {
            if (!Directory.Exists(sourceDir)) return;
            try
            {
                if (!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);

                foreach (var srcFile in Directory.GetFiles(sourceDir))
                {
                    string fileName = Path.GetFileName(srcFile);
                    string destFile = Path.Combine(targetDir, fileName);

                    if (!File.Exists(destFile) || File.GetLastWriteTime(srcFile) > File.GetLastWriteTime(destFile))
                    {
                        File.Copy(srcFile, destFile, overwrite: true);
                    }
                }
            }
            catch { }
        }
    }
}
