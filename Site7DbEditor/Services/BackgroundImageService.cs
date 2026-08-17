using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.Json;

namespace Site7DbEditor.Services
{
    public class BackgroundImageConfig
    {
        public string ImagePath { get; set; } = "";
        public float Pt1_PixelX { get; set; }
        public float Pt1_PixelY { get; set; }
        public double Pt1_SurveyX { get; set; }
        public double Pt1_SurveyY { get; set; }

        public float Pt2_PixelX { get; set; }
        public float Pt2_PixelY { get; set; }
        public double Pt2_SurveyX { get; set; }
        public double Pt2_SurveyY { get; set; }

        public bool IsAligned { get; set; }
        public float Opacity { get; set; } = 0.8f;
        public bool IsVisible { get; set; } = true;
    }

    public class BackgroundImageService
    {
        public static BackgroundImageService Instance { get; } = new BackgroundImageService();

        public BackgroundImageConfig Config { get; private set; } = new BackgroundImageConfig();
        public Bitmap? LoadedImage { get; private set; }

        private BackgroundImageService() { }

        public bool LoadConfig(string dbPath)
        {
            try
            {
                string cfgPath = GetConfigPath(dbPath);
                if (File.Exists(cfgPath))
                {
                    string json = File.ReadAllText(cfgPath);
                    var cfg = JsonSerializer.Deserialize<BackgroundImageConfig>(json);
                    if (cfg != null)
                    {
                        Config = cfg;
                        LoadImageFile(Config.ImagePath);
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        public void SaveConfig(string dbPath)
        {
            try
            {
                string cfgPath = GetConfigPath(dbPath);
                string? dir = Path.GetDirectoryName(cfgPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string json = JsonSerializer.Serialize(Config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(cfgPath, json);
            }
            catch { }
        }

        private string GetConfigPath(string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath)) return "";
            string dir = Path.GetDirectoryName(dbPath) ?? "";
            string name = Path.GetFileNameWithoutExtension(dbPath);
            return Path.Combine(dir, $"{name}_bgimage.json");
        }

        public bool LoadImageFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    LoadedImage?.Dispose();
                    // ロックを避けるためにメモリにコピー
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (var img = Image.FromStream(fs))
                    {
                        LoadedImage = new Bitmap(img);
                    }
                    Config.ImagePath = path;
                    return true;
                }
            }
            catch { }
            return false;
        }

        public void SetAlignment(PointF p1Pix, double s1X, double s1Y, PointF p2Pix, double s2X, double s2Y)
        {
            Config.Pt1_PixelX = p1Pix.X;
            Config.Pt1_PixelY = p1Pix.Y;
            Config.Pt1_SurveyX = s1X;
            Config.Pt1_SurveyY = s1Y;

            Config.Pt2_PixelX = p2Pix.X;
            Config.Pt2_PixelY = p2Pix.Y;
            Config.Pt2_SurveyX = s2X;
            Config.Pt2_SurveyY = s2Y;

            Config.IsAligned = true;
        }

        public (double surveyX, double surveyY) PixelToSurvey(float px, float py)
        {
            if (!Config.IsAligned) return (0, 0);

            double du_pix = Config.Pt2_PixelX - Config.Pt1_PixelX;
            double dv_pix = Config.Pt2_PixelY - Config.Pt1_PixelY;
            double len_pix = Math.Sqrt(du_pix * du_pix + dv_pix * dv_pix);
            if (len_pix < 1e-6) return (Config.Pt1_SurveyX, Config.Pt1_SurveyY);

            // 測量座標 (X: 縦/南北, Y: 横/東西)
            double dX_srv = Config.Pt2_SurveyX - Config.Pt1_SurveyX;
            double dY_srv = Config.Pt2_SurveyY - Config.Pt1_SurveyY;
            double len_srv = Math.Sqrt(dX_srv * dX_srv + dY_srv * dY_srv);
            if (len_srv < 1e-6) return (Config.Pt1_SurveyX, Config.Pt1_SurveyY);

            double scale = len_srv / len_pix;

            // 測量座標系 (Y: 横/東西-右+, X: 縦/南北-上+)
            // 画像ピクセル系 (u: 右+, v: 下+ -> 上向きは -v)
            // 複素数表現: (dY + i*dX) = (c + i*d) * (du - i*dv)
            // c + i*d = (dY + i*dX) * (du + i*dv) / (du^2 + dv^2)
            double du2_dv2 = len_pix * len_pix;
            double c = (du_pix * dY_srv - dv_pix * dX_srv) / du2_dv2;
            double d = (dv_pix * dY_srv + du_pix * dX_srv) / du2_dv2;

            double u = px - Config.Pt1_PixelX;
            double v = py - Config.Pt1_PixelY;

            // (dY, dX)^T = [c, -d; d, c] * (u, -v)^T = [c*u + d*v, d*u - c*v]^T
            double sY = Config.Pt1_SurveyY + (c * u + d * v);
            double sX = Config.Pt1_SurveyX + (d * u - c * v);

            return (sX, sY);
        }

        public void DrawBackground(Graphics g, Size canvasSize, EditorMapViewController vc, bool isVisible)
        {
            if (!isVisible || !Config.IsAligned || LoadedImage == null) return;

            int imgW = LoadedImage.Width;
            int imgH = LoadedImage.Height;
            if (imgW <= 0 || imgH <= 0) return;

            // 画像の3隅（左上(0,0), 右上(W,0), 左下(0,H)）の測量座標を算出
            var (tlX, tlY) = PixelToSurvey(0, 0);
            var (trX, trY) = PixelToSurvey(imgW, 0);
            var (blX, blY) = PixelToSurvey(0, imgH);

            // 測量座標 -> スクリーン座標
            PointF pTL = vc.ToCanvasPoint(tlX, tlY, canvasSize);
            PointF pTR = vc.ToCanvasPoint(trX, trY, canvasSize);
            PointF pBL = vc.ToCanvasPoint(blX, blY, canvasSize);

            PointF[] destPoints = new PointF[] { pTL, pTR, pBL };

            using (var ia = new System.Drawing.Imaging.ImageAttributes())
            {
                float alpha = Math.Clamp(Config.Opacity, 0.05f, 1.0f);
                var matrix = new System.Drawing.Imaging.ColorMatrix
                {
                    Matrix33 = alpha
                };
                ia.SetColorMatrix(matrix);

                var oldInterpolation = g.InterpolationMode;
                g.InterpolationMode = InterpolationMode.Bilinear;
                try
                {
                    g.DrawImage(
                        LoadedImage,
                        destPoints,
                        new RectangleF(0, 0, imgW, imgH),
                        GraphicsUnit.Pixel,
                        ia);
                }
                finally
                {
                    g.InterpolationMode = oldInterpolation;
                }
            }
        }
    }
}
