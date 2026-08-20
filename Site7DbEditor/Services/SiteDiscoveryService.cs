using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Site7DbEditor.Models;

namespace Site7DbEditor.Services
{
    public static class SiteDiscoveryService
    {
        /// <summary>
        /// デフォルトの現場探索ルートパスを取得します。
        /// </summary>
        public static string GetDefaultRootPath()
        {
            const string primaryPath = @"C:\SITE7\GENBA\DATA";
            if (Directory.Exists(primaryPath))
            {
                return primaryPath;
            }

            try
            {
                Directory.CreateDirectory(primaryPath);
                return primaryPath;
            }
            catch
            {
                string appDir = AppDomain.CurrentDomain.BaseDirectory;
                string[] candidatePaths = new[]
                {
                    Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\Site7TestData")),
                    Path.GetFullPath(Path.Combine(appDir, @"..\..\..\Site7TestData")),
                    Path.GetFullPath(Path.Combine(appDir, @"Site7TestData"))
                };

                foreach (var path in candidatePaths)
                {
                    if (Directory.Exists(path)) return path;
                }

                return appDir;
            }
        }

        /// <summary>
        /// 指定されたルートフォルダから現場一覧を探索して取得します。
        /// SITE7.png が存在するフォルダのみを対象とします。
        /// </summary>
        public static List<SiteItem> DiscoverSites(string rootPath)
        {
            var results = new List<SiteItem>();
            if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(rootPath)) return results;

            // 1. 直下のサブディレクトリを探索
            var subDirs = Directory.GetDirectories(rootPath);
            foreach (var dir in subDirs)
            {
                var site = ScanDirectoryForSite(dir);
                if (site != null)
                {
                    results.Add(site);
                }
            }

            // 2. ルート直下に直接 SITE7.png が存在する場合
            var rootSite = ScanDirectoryForSite(rootPath, isRoot: true);
            if (rootSite != null && !results.Any(r => r.FolderPath.Equals(rootSite.FolderPath, StringComparison.OrdinalIgnoreCase)))
            {
                results.Insert(0, rootSite);
            }

            // 更新日時が新しい順にソート
            return results.OrderByDescending(s => s.UpdatedAt).ToList();
        }

        private static SiteItem? ScanDirectoryForSite(string dirPath, bool isRoot = false)
        {
            try
            {
                // SITE7.png (大文字小文字を区別しない) の存在確認
                string[] pngFiles = Directory.GetFiles(dirPath, "SITE7.png", SearchOption.TopDirectoryOnly);
                if (pngFiles.Length == 0)
                {
                    // 大文字小文字の念のため検索
                    pngFiles = Directory.GetFiles(dirPath, "*.png")
                        .Where(f => Path.GetFileName(f).Equals("SITE7.png", StringComparison.OrdinalIgnoreCase))
                        .ToArray();
                }

                if (pngFiles.Length == 0)
                {
                    // SITE7.png がないフォルダは対象外
                    return null;
                }

                string pngPath = pngFiles[0];
                var pngFileInfo = new FileInfo(pngPath);

                // DBファイルの探索 (SITE7.db, SITE7.db3, Site7.db3, *.db3, *.db, *.sqlite)
                string dbPath = "";
                long fileSizeBytes = 0;
                DateTime lastUpdated = pngFileInfo.LastWriteTime;

                string[] candidateDbNames = new[] { "SITE7.db", "SITE7.db3", "Site7.db3", "site7.db", "site7.db3" };
                foreach (var cname in candidateDbNames)
                {
                    string candidatePath = Path.Combine(dirPath, cname);
                    if (File.Exists(candidatePath))
                    {
                        dbPath = candidatePath;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(dbPath))
                {
                    var anyDbFiles = Directory.GetFiles(dirPath, "*.*")
                        .Where(f => f.EndsWith(".db", StringComparison.OrdinalIgnoreCase) ||
                                    f.EndsWith(".db3", StringComparison.OrdinalIgnoreCase) ||
                                    f.EndsWith(".sqlite", StringComparison.OrdinalIgnoreCase))
                        .ToArray();

                    if (anyDbFiles.Length > 0)
                    {
                        dbPath = anyDbFiles[0];
                    }
                    else
                    {
                        dbPath = Path.Combine(dirPath, "SITE7.db");
                    }
                }

                if (File.Exists(dbPath))
                {
                    var dbInfo = new FileInfo(dbPath);
                    fileSizeBytes = dbInfo.Length;
                    if (dbInfo.LastWriteTime > lastUpdated)
                    {
                        lastUpdated = dbInfo.LastWriteTime;
                    }
                }
                else
                {
                    fileSizeBytes = pngFileInfo.Length;
                }

                string dirName = Path.GetFileName(dirPath);
                if (string.IsNullOrEmpty(dirName) && isRoot)
                {
                    dirName = "ルート現場";
                }

                return new SiteItem
                {
                    Name = dirName,
                    FolderPath = dirPath,
                    DbPath = dbPath,
                    ThumbnailPath = pngPath,
                    UpdatedAt = lastUpdated,
                    CreatedAt = Directory.GetCreationTime(dirPath),
                    FileSizeBytes = fileSizeBytes
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
