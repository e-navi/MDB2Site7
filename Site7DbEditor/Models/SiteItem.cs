using System;
using System.Drawing;
using System.IO;

namespace Site7DbEditor.Models
{
    public class SiteItem
    {
        public string Name { get; set; } = "";
        public string FolderPath { get; set; } = "";
        public string DbPath { get; set; } = "";
        public string ThumbnailPath { get; set; } = "";
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool HasThumbnail => !string.IsNullOrEmpty(ThumbnailPath) && File.Exists(ThumbnailPath);
        public long FileSizeBytes { get; set; }

        public string DisplayUpdatedAt => UpdatedAt.ToString("yyyy/MM/dd HH:mm");
        public string DisplaySize => FileSizeBytes > 0 ? $"{(FileSizeBytes / 1024.0 / 1024.0):F2} MB" : "-";

        private Image? _cachedThumbnail = null;

        public Image? GetThumbnailImage()
        {
            if (_cachedThumbnail != null) return _cachedThumbnail;

            if (HasThumbnail)
            {
                try
                {
                    // ファイルロックを防ぐためにメモリストリーム経由でロード
                    byte[] bytes = File.ReadAllBytes(ThumbnailPath);
                    using (var ms = new MemoryStream(bytes))
                    {
                        _cachedThumbnail = Image.FromStream(ms);
                    }
                }
                catch
                {
                    _cachedThumbnail = null;
                }
            }

            return _cachedThumbnail;
        }

        public void InvalidateThumbnailCache()
        {
            _cachedThumbnail?.Dispose();
            _cachedThumbnail = null;
        }
    }
}
