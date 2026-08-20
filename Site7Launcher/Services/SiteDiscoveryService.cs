using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Site7Launcher.Models;

namespace Site7Launcher.Services
{
    public static class SiteDiscoveryService
    {
        /// <summary>
        /// デフォルトの現場探索ルートパスを取得します。
        /// </summary>
        public static string GetDefaultRootPath()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            // 開発環境やプロジェクト構造に合わせて探す
            string[] candidatePaths = new[]
            {
                Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\Site7TestData")),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\..\Site7TestData")),
                Path.GetFullPath(Path.Combine(appDir, @"Site7TestData")),
                @"C:\Site7\Data",
                @"C:\Site7Data"
            };

            foreach (var path in candidatePaths)
            {
                if (Directory.Exists(path)) return path;
            }

            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 指定されたルートフォルダから現場一覧を探索して取得します。
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

            // 2. ルート直下に直接 DB が存在する場合
            var rootSite = ScanDirectoryForSite(rootPath, isRoot: true);
            if (rootSite != null && !results.Any(r => r.DbPath == rootSite.DbPath))
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
                // SITE7.db または *.db を検索
                string dbPath = Path.Combine(dirPath, "SITE7.db");
                if (!File.Exists(dbPath))
                {
                    var dbFiles = Directory.GetFiles(dirPath, "*.db");
                    if (dbFiles.Length > 0)
                    {
                        dbPath = dbFiles[0];
                    }
                    else
                    {
                        return null; // DBファイルがない場合は現場とみなさない
                    }
                }

                var dbFileInfo = new FileInfo(dbPath);
                string dirName = Path.GetFileName(dirPath);
                if (string.IsNullOrEmpty(dirName) && isRoot)
                {
                    dirName = "ルート現場";
                }

                string pngPath = Path.Combine(dirPath, "SITE7.png");

                return new SiteItem
                {
                    Name = dirName,
                    FolderPath = dirPath,
                    DbPath = dbPath,
                    ThumbnailPath = pngPath,
                    UpdatedAt = dbFileInfo.LastWriteTime,
                    CreatedAt = dbFileInfo.CreationTime,
                    FileSizeBytes = dbFileInfo.Length
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
