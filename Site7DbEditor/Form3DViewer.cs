using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public class Form3DViewer : Form
    {
        private readonly EditorDbManager _db;
        private readonly BackgroundImageService _bgService = BackgroundImageService.Instance;
        private readonly PointCloudService _pcService = PointCloudService.Instance;

        // UI Controls
        private PictureBox pic3DCanvas = new PictureBox();
        private Panel pnlToolBar = new Panel();
        private CheckBox chkShowPointCloud = new CheckBox();
        private CheckBox chkSwapXY = new CheckBox();
        private CheckBox chkShowImageMesh = new CheckBox();
        private ComboBox cmbMeshQuality = new ComboBox();
        private CheckBox chkShowIkouLines = new CheckBox();
        private CheckBox chkShowKikai = new CheckBox();
        private TrackBar trkZScale = new TrackBar();
        private Label lblZScaleVal = new Label();
        private Button btnResetView = new Button();
        private Label lblInfo = new Label();

        // 3D Camera / View Parameters
        private double _targetX;
        private double _targetY;
        private double _targetZ;
        private double _camDistance = 100.0;
        private double _camPitch = 35.0; // 仰角 (度)
        private double _camYaw = 45.0;   // 方位角 (度)
        private double _zExaggeration = 1.5; // Z強調倍率

        // Mouse Drag State
        private bool _isRotating = false;
        private bool _isPanning = false;
        private Point _lastMousePos;

        // Downsampled Point Cloud for Fast 3D Render
        private List<Point3D> _renderPoints = new List<Point3D>();

        // 3D Image Mesh Grid Points
        private struct MeshVertex
        {
            public double X;
            public double Y;
            public double Z;
            public Color Color;
        }
        private List<MeshVertex[,]> _meshPatches = new List<MeshVertex[,]>();
        private int _meshCols = 130;
        private int _meshRows = 130;

        // State Flags
        private bool _isInitializing = true;

        public Form3DViewer(EditorDbManager db)
        {
            _db = db;
            InitializeComponent();
            _isInitializing = false;
            Init3DData();
        }

        private void InitializeComponent()
        {
            this.Text = "3次元立体確認ビューア (3D Surface & Point Cloud Viewer)";
            this.Size = new Size(1260, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(20, 22, 28);
            this.ForeColor = Color.White;
            this.Font = new Font("Yu Gothic UI", 9F);

            // Top Toolbar
            pnlToolBar.Dock = DockStyle.Top;
            pnlToolBar.Height = 44;
            pnlToolBar.BackColor = Color.FromArgb(28, 30, 38);
            pnlToolBar.Padding = new Padding(8, 6, 8, 6);

            chkShowImageMesh.Text = "画像3D地形";
            chkShowImageMesh.Checked = true;
            chkShowImageMesh.Location = new Point(10, 10);
            chkShowImageMesh.AutoSize = true;
            chkShowImageMesh.ForeColor = Color.FromArgb(0, 225, 255);
            chkShowImageMesh.CheckedChanged += (s, e) => pic3DCanvas.Invalidate();

            cmbMeshQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMeshQuality.Items.AddRange(new object[] { "解像度: 低 (50x50)", "解像度: 中 (80x80)", "解像度: 高 (120x120)", "解像度: 超高精細 (160x160)" });
            cmbMeshQuality.SelectedIndex = 2; // デフォルト: 高 (120x120)
            cmbMeshQuality.Location = new Point(105, 8);
            cmbMeshQuality.Size = new Size(160, 25);
            cmbMeshQuality.BackColor = Color.FromArgb(40, 42, 54);
            cmbMeshQuality.ForeColor = Color.White;
            cmbMeshQuality.SelectedIndexChanged += (s, e) => {
                if (_isInitializing) return;
                int[] sizes = new int[] { 50, 80, 120, 160 };
                int idx = Math.Clamp(cmbMeshQuality.SelectedIndex, 0, sizes.Length - 1);
                _meshCols = sizes[idx];
                _meshRows = sizes[idx];
                Cursor = Cursors.WaitCursor;
                try {
                    BuildImageMesh();
                    pic3DCanvas.Invalidate();
                } finally {
                    Cursor = Cursors.Default;
                }
            };

            chkShowPointCloud.Text = "点群";
            chkShowPointCloud.Checked = true;
            chkShowPointCloud.Location = new Point(275, 10);
            chkShowPointCloud.AutoSize = true;
            chkShowPointCloud.ForeColor = Color.FromArgb(100, 255, 120);
            chkShowPointCloud.CheckedChanged += (s, e) => pic3DCanvas.Invalidate();

            chkSwapXY.Text = "🔄点群XY入替";
            chkSwapXY.Checked = _pcService.SwapXY;
            chkSwapXY.Location = new Point(330, 10);
            chkSwapXY.AutoSize = true;
            chkSwapXY.ForeColor = Color.FromArgb(255, 230, 100);
            chkSwapXY.CheckedChanged += (s, e) => {
                if (_pcService.HasPoints)
                {
                    _pcService.ToggleSwapXY();
                    _bgService.Config.PointCloudSwapXY = _pcService.SwapXY;
                    Init3DData();
                }
            };

            chkShowIkouLines.Text = "遺構線";
            chkShowIkouLines.Checked = true;
            chkShowIkouLines.Location = new Point(445, 10);
            chkShowIkouLines.AutoSize = true;
            chkShowIkouLines.ForeColor = Color.FromArgb(255, 220, 80);
            chkShowIkouLines.CheckedChanged += (s, e) => pic3DCanvas.Invalidate();

            chkShowKikai.Text = "基準点";
            chkShowKikai.Checked = true;
            chkShowKikai.Location = new Point(515, 10);
            chkShowKikai.AutoSize = true;
            chkShowKikai.ForeColor = Color.FromArgb(255, 100, 100);
            chkShowKikai.CheckedChanged += (s, e) => pic3DCanvas.Invalidate();

            var lblZ = new Label { Text = "高さ強調:", Location = new Point(585, 12), AutoSize = true, ForeColor = Color.LightGray };
            trkZScale.Location = new Point(645, 8);
            trkZScale.Size = new Size(95, 30);
            trkZScale.Minimum = 10;
            trkZScale.Maximum = 50;
            trkZScale.Value = 15;
            trkZScale.TickFrequency = 10;
            trkZScale.ValueChanged += (s, e) => {
                _zExaggeration = trkZScale.Value / 10.0;
                lblZScaleVal.Text = $"{_zExaggeration:F1}x";
                pic3DCanvas.Invalidate();
            };

            lblZScaleVal.Text = "1.5x";
            lblZScaleVal.Location = new Point(745, 12);
            lblZScaleVal.AutoSize = true;
            lblZScaleVal.ForeColor = Color.FromArgb(0, 225, 255);

            btnResetView.Text = "🔄 視点リセット";
            btnResetView.Location = new Point(785, 8);
            btnResetView.Size = new Size(100, 28);
            btnResetView.BackColor = Color.FromArgb(43, 114, 186);
            btnResetView.ForeColor = Color.White;
            btnResetView.FlatStyle = FlatStyle.Flat;
            btnResetView.Click += (s, e) => ResetView();

            lblInfo.Text = "【操作】左ドラッグ: 回転 | 右/中ドラッグ: 平行移動 | ホイール: ズーム";
            lblInfo.Location = new Point(895, 12);
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.FromArgb(170, 180, 200);

            pnlToolBar.Controls.AddRange(new Control[] {
                chkShowImageMesh, cmbMeshQuality, chkShowPointCloud, chkSwapXY, chkShowIkouLines, chkShowKikai,
                lblZ, trkZScale, lblZScaleVal, btnResetView, lblInfo
            });

            // 3D Canvas
            pic3DCanvas.Dock = DockStyle.Fill;
            pic3DCanvas.BackColor = Color.FromArgb(14, 15, 20);
            pic3DCanvas.Paint += Pic3DCanvas_Paint;
            pic3DCanvas.MouseDown += Pic3DCanvas_MouseDown;
            pic3DCanvas.MouseMove += Pic3DCanvas_MouseMove;
            pic3DCanvas.MouseUp += Pic3DCanvas_MouseUp;
            pic3DCanvas.MouseWheel += Pic3DCanvas_MouseWheel;
            pic3DCanvas.Resize += (s, e) => pic3DCanvas.Invalidate();

            this.Controls.Add(pic3DCanvas);
            this.Controls.Add(pnlToolBar);
        }

        private void Init3DData()
        {
            // 画像の読み込み保証
            if (_bgService.LoadedImage == null && !string.IsNullOrEmpty(_bgService.Config.ImagePath) && File.Exists(_bgService.Config.ImagePath))
            {
                _bgService.LoadImageFile(_bgService.Config.ImagePath);
            }

            // 1. 点群のサンプリング（描画パフォーマンスのため最大10万点程度に間引き）
            _renderPoints.Clear();
            if (_pcService.HasPoints)
            {
                int total = _pcService.Points.Count;
                int step = Math.Max(1, total / 80000);
                for (int i = 0; i < total; i += step)
                {
                    _renderPoints.Add(_pcService.Points[i]);
                }
            }

            // 2. 背景画像の3D標高テクスチャメッシュ作成
            BuildImageMesh();

            // 3. 視点中心と距離の初期化
            ResetView();
        }

        private void BuildImageMesh()
        {
            _meshPatches.Clear();
            if (!_bgService.Config.IsAligned) return;

            if (_bgService.LoadedImage == null && !string.IsNullOrEmpty(_bgService.Config.ImagePath) && File.Exists(_bgService.Config.ImagePath))
            {
                _bgService.LoadImageFile(_bgService.Config.ImagePath);
            }
            if (_bgService.LoadedImage == null) return;

            var bmp = _bgService.LoadedImage;
            int imgW = bmp.Width;
            int imgH = bmp.Height;
            if (imgW <= 0 || imgH <= 0) return;

            var grid = new MeshVertex[_meshRows, _meshCols];

            double defaultZ = _pcService.HasPoints ? (_pcService.MinZ + _pcService.MaxZ) / 2.0 : 0.0;
            if (!_pcService.HasPoints && _db.KikaiList.Count > 0)
            {
                defaultZ = _db.KikaiList.Average(k => k.Z);
            }

            // Fast Safe Bitmap Sampling using Marshal.Copy
            var rect = new Rectangle(0, 0, imgW, imgH);
            var bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[bmpData.Stride * imgH];
            try
            {
                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }

            int stride = bmpData.Stride;

            for (int r = 0; r < _meshRows; r++)
            {
                float v = (float)r / (_meshRows - 1) * (imgH - 1);
                int py = Math.Clamp((int)v, 0, imgH - 1);

                for (int c = 0; c < _meshCols; c++)
                {
                    float u = (float)c / (_meshCols - 1) * (imgW - 1);
                    int px = Math.Clamp((int)u, 0, imgW - 1);

                    var (sx, sy) = _bgService.PixelToSurvey(u, v);

                    double sz = defaultZ;
                    if (_pcService.HasPoints)
                    {
                        var queriedZ = _pcService.GetFastZ(sx, sy);
                        if (queriedZ.HasValue) sz = queriedZ.Value;
                    }

                    int offset = (py * stride) + (px * 4);
                    byte b = pixelBuffer[offset];
                    byte g = pixelBuffer[offset + 1];
                    byte red = pixelBuffer[offset + 2];
                    byte a = pixelBuffer[offset + 3];

                    grid[r, c] = new MeshVertex {
                        X = sx,
                        Y = sy,
                        Z = sz,
                        Color = Color.FromArgb(a > 0 ? a : (byte)255, red, g, b)
                    };
                }
            }

            _meshPatches.Add(grid);
        }

        private void ResetView()
        {
            // 範囲の計算 (点群、背景画像メッシュ、基準点)
            double minX = double.MaxValue, maxX = double.MinValue;
            double minY = double.MaxValue, maxY = double.MinValue;
            double minZ = double.MaxValue, maxZ = double.MinValue;

            if (_pcService.HasPoints)
            {
                minX = _pcService.MinX; maxX = _pcService.MaxX;
                minY = _pcService.MinY; maxY = _pcService.MaxY;
                minZ = _pcService.MinZ; maxZ = _pcService.MaxZ;
            }

            if (_meshPatches.Count > 0)
            {
                foreach (var g in _meshPatches)
                {
                    int rows = g.GetLength(0);
                    int cols = g.GetLength(1);
                    for (int r = 0; r < rows; r++)
                    {
                        for (int c = 0; c < cols; c++)
                        {
                            var v = g[r, c];
                            if (v.X < minX) minX = v.X; if (v.X > maxX) maxX = v.X;
                            if (v.Y < minY) minY = v.Y; if (v.Y > maxY) maxY = v.Y;
                            if (v.Z < minZ) minZ = v.Z; if (v.Z > maxZ) maxZ = v.Z;
                        }
                    }
                }
            }

            if (_db.KikaiList.Count > 0)
            {
                foreach (var k in _db.KikaiList)
                {
                    if (k.X < minX) minX = k.X; if (k.X > maxX) maxX = k.X;
                    if (k.Y < minY) minY = k.Y; if (k.Y > maxY) maxY = k.Y;
                    if (k.Z < minZ) minZ = k.Z; if (k.Z > maxZ) maxZ = k.Z;
                }
            }

            if (minX == double.MaxValue)
            {
                minX = -50; maxX = 50; minY = -50; maxY = 50; minZ = 0; maxZ = 10;
            }

            _targetX = (minX + maxX) / 2.0;
            _targetY = (minY + maxY) / 2.0;
            _targetZ = (minZ + maxZ) / 2.0;

            double span = Math.Max(maxX - minX, maxY - minY);
            _camDistance = Math.Max(15.0, span * 1.6);
            _camPitch = 40.0;
            _camYaw = 45.0;

            pic3DCanvas.Invalidate();
        }

        private PointF Project3DToScreen(double x, double y, double z, int screenW, int screenH)
        {
            // 測量座標系: X=北(縦/上), Y=東(横/右), Z=標高(上)
            // ターゲット中心からの相対座標
            double dx = x - _targetX;
            double dy = y - _targetY;
            double dz = (z - _targetZ) * _zExaggeration;

            // Yaw (水平回転: Z軸まわり)
            double radYaw = _camYaw * Math.PI / 180.0;
            double cosYaw = Math.Cos(radYaw);
            double sinYaw = Math.Sin(radYaw);

            double rx = dx * cosYaw - dy * sinYaw;
            double ry = dx * sinYaw + dy * cosYaw;

            // Pitch (仰角回転: 横軸まわり)
            double radPitch = _camPitch * Math.PI / 180.0;
            double cosPitch = Math.Cos(radPitch);
            double sinPitch = Math.Sin(radPitch);

            double eyeX = rx;
            double eyeY = ry * sinPitch + dz * cosPitch;
            double eyeZ = ry * cosPitch - dz * sinPitch; // 奥行き

            // 透視投影 (Perspective Projection)
            double fovDist = Math.Max(20.0, _camDistance);
            double depth = fovDist + eyeZ;
            if (depth < 0.1) depth = 0.1;

            double scale = (screenH * 0.95) / depth;

            float screenX = (float)(screenW / 2.0 + eyeX * scale);
            float screenY = (float)(screenH / 2.0 - eyeY * scale);

            return new PointF(screenX, screenY);
        }

        private void Pic3DCanvas_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isRotating = true;
                _lastMousePos = e.Location;
            }
            else if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Middle)
            {
                _isPanning = true;
                _lastMousePos = e.Location;
            }
        }

        private void Pic3DCanvas_MouseMove(object? sender, MouseEventArgs e)
        {
            int dx = e.X - _lastMousePos.X;
            int dy = e.Y - _lastMousePos.Y;
            _lastMousePos = e.Location;

            if (_isRotating)
            {
                _camYaw += dx * 0.45;
                _camPitch = Math.Clamp(_camPitch + dy * 0.45, -89.0, 89.0);
                pic3DCanvas.Invalidate();
            }
            else if (_isPanning)
            {
                // カメラ向きに応じたパン
                double radYaw = _camYaw * Math.PI / 180.0;
                double panSpeed = (_camDistance / Math.Max(100, pic3DCanvas.Height)) * 0.8;

                double forwardX = -Math.Sin(radYaw) * dy * panSpeed;
                double forwardY = -Math.Cos(radYaw) * dy * panSpeed;
                double sideX = Math.Cos(radYaw) * dx * panSpeed;
                double sideY = -Math.Sin(radYaw) * dx * panSpeed;

                _targetX -= (forwardX + sideX);
                _targetY -= (forwardY + sideY);
                pic3DCanvas.Invalidate();
            }
        }

        private void Pic3DCanvas_MouseUp(object? sender, MouseEventArgs e)
        {
            _isRotating = false;
            _isPanning = false;
        }

        private void Pic3DCanvas_MouseWheel(object? sender, MouseEventArgs e)
        {
            double factor = e.Delta > 0 ? 0.85 : 1.15;
            _camDistance = Math.Clamp(_camDistance * factor, 1.0, 10000.0);
            pic3DCanvas.Invalidate();
        }

        private void Pic3DCanvas_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int w = pic3DCanvas.Width;
            int h = pic3DCanvas.Height;
            if (w <= 0 || h <= 0) return;

            // 背景グラデーション
            using (var bgBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, h), Color.FromArgb(16, 18, 26), Color.FromArgb(8, 8, 12)))
            {
                g.FillRectangle(bgBrush, 0, 0, w, h);
            }

            // 1. 3D座標軸 (Compass / Axis) の描画
            Draw3DAxes(g, w, h);

            // 2. 背景画像テクスチャ3Dメッシュの描画
            if (chkShowImageMesh.Checked && _meshPatches.Count > 0)
            {
                DrawImageMesh(g, w, h);
            }

            // 3. 点群 (Point Cloud) の描画
            if (chkShowPointCloud.Checked && _renderPoints.Count > 0)
            {
                DrawPointCloud(g, w, h);
            }

            // 4. 遺構線 (3D Lines) の描画
            if (chkShowIkouLines.Checked)
            {
                DrawIkouLines3D(g, w, h);
            }

            // 5. 基準点 (Kikai Points) の描画
            if (chkShowKikai.Checked)
            {
                DrawKikaiPoints3D(g, w, h);
            }

            // 6. HUD / 情報オーバーレイ
            DrawHudInfo(g, w, h);
        }

        private void Draw3DAxes(Graphics g, int w, int h)
        {
            double axisLen = Math.Max(5.0, _camDistance * 0.12);
            var p0 = Project3DToScreen(_targetX, _targetY, _targetZ, w, h);
            var pX = Project3DToScreen(_targetX + axisLen, _targetY, _targetZ, w, h); // 北 (X)
            var pY = Project3DToScreen(_targetX, _targetY + axisLen, _targetZ, w, h); // 東 (Y)
            var pZ = Project3DToScreen(_targetX, _targetY, _targetZ + axisLen, w, h); // 標高 (Z)

            using (var penX = new Pen(Color.FromArgb(255, 75, 75), 2f))
            using (var penY = new Pen(Color.FromArgb(75, 230, 75), 2f))
            using (var penZ = new Pen(Color.FromArgb(60, 160, 255), 2f))
            using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
            using (var bX = new SolidBrush(Color.FromArgb(255, 100, 100)))
            using (var bY = new SolidBrush(Color.FromArgb(100, 255, 100)))
            using (var bZ = new SolidBrush(Color.FromArgb(100, 180, 255)))
            {
                g.DrawLine(penX, p0, pX);
                g.DrawString($"X(北) {axisLen:F0}m", font, bX, pX.X + 4, pX.Y - 6);

                g.DrawLine(penY, p0, pY);
                g.DrawString($"Y(東) {axisLen:F0}m", font, bY, pY.X + 4, pY.Y - 6);

                g.DrawLine(penZ, p0, pZ);
                g.DrawString($"Z(高) {axisLen:F0}m", font, bZ, pZ.X + 4, pZ.Y - 6);
            }
        }

        private void DrawImageMesh(Graphics g, int w, int h)
        {
            int step = (_isRotating || _isPanning) ? 2 : 1;
            foreach (var grid in _meshPatches)
            {
                int rows = grid.GetLength(0);
                int cols = grid.GetLength(1);

                for (int r = 0; r < rows - step; r += step)
                {
                    for (int c = 0; c < cols - step; c += step)
                    {
                        var v00 = grid[r, c];
                        var v10 = grid[r + step, c];
                        var v11 = grid[r + step, c + step];
                        var v01 = grid[r, c + step];

                        var p00 = Project3DToScreen(v00.X, v00.Y, v00.Z, w, h);
                        var p10 = Project3DToScreen(v10.X, v10.Y, v10.Z, w, h);
                        var p11 = Project3DToScreen(v11.X, v11.Y, v11.Z, w, h);
                        var p01 = Project3DToScreen(v01.X, v01.Y, v01.Z, w, h);

                        // 画面外簡易クリッピング
                        float minPx = Math.Min(Math.Min(p00.X, p10.X), Math.Min(p11.X, p01.X));
                        float maxPx = Math.Max(Math.Max(p00.X, p10.X), Math.Max(p11.X, p01.X));
                        float minPy = Math.Min(Math.Min(p00.Y, p10.Y), Math.Min(p11.Y, p01.Y));
                        float maxPy = Math.Max(Math.Max(p00.Y, p10.Y), Math.Max(p11.Y, p01.Y));
                        if (maxPx < 0 || minPx > w || maxPy < 0 || minPy > h) continue;

                        // 平均カラー
                        int avgR = (v00.Color.R + v10.Color.R + v11.Color.R + v01.Color.R) / 4;
                        int avgG = (v00.Color.G + v10.Color.G + v11.Color.G + v01.Color.G) / 4;
                        int avgB = (v00.Color.B + v10.Color.B + v11.Color.B + v01.Color.B) / 4;

                        PointF[] pts = new PointF[] { p00, p10, p11, p01 };
                        using (var brush = new SolidBrush(Color.FromArgb(230, avgR, avgG, avgB)))
                        {
                            g.FillPolygon(brush, pts);
                        }
                    }
                }
            }
        }

        private void DrawPointCloud(Graphics g, int w, int h)
        {
            double minZ = _pcService.MinZ;
            double maxZ = _pcService.MaxZ;
            double rangeZ = Math.Max(0.1, maxZ - minZ);

            foreach (var pt in _renderPoints)
            {
                var scrPt = Project3DToScreen(pt.X, pt.Y, pt.Z, w, h);
                if (scrPt.X < -10 || scrPt.X > w + 10 || scrPt.Y < -10 || scrPt.Y > h + 10) continue;

                // 標高ヒートマップカラー
                double normZ = Math.Clamp((pt.Z - minZ) / rangeZ, 0.0, 1.0);
                Color dotColor = GetElevationColor(normZ);

                using (var brush = new SolidBrush(dotColor))
                {
                    g.FillRectangle(brush, scrPt.X - 1f, scrPt.Y - 1f, 2.5f, 2.5f);
                }
            }
        }

        private Color GetElevationColor(double t)
        {
            // 青 (0.0) -> シアン (0.25) -> 緑 (0.5) -> 黄 (0.75) -> 赤 (1.0)
            int r = 0, g = 0, b = 0;
            if (t < 0.25)
            {
                double k = t / 0.25;
                r = 0; g = (int)(255 * k); b = 255;
            }
            else if (t < 0.5)
            {
                double k = (t - 0.25) / 0.25;
                r = 0; g = 255; b = (int)(255 * (1 - k));
            }
            else if (t < 0.75)
            {
                double k = (t - 0.5) / 0.25;
                r = (int)(255 * k); g = 255; b = 0;
            }
            else
            {
                double k = (t - 0.75) / 0.25;
                r = 255; g = (int)(255 * (1 - k)); b = 0;
            }
            return Color.FromArgb(Math.Clamp(r, 0, 255), Math.Clamp(g, 0, 255), Math.Clamp(b, 0, 255));
        }

        private void DrawIkouLines3D(Graphics g, int w, int h)
        {
            using (var pen = new Pen(Color.FromArgb(255, 230, 70), 2f))
            {
                foreach (var line in _db.IkouLList)
                {
                    var pts = SqliteManager.ParsePrecsText(line.Precs);
                    if (pts.Count < 2) continue;

                    var scrPts = new List<PointF>();
                    foreach (var p in pts)
                    {
                        double z = p.Z;
                        if (_pcService.HasPoints && Math.Abs(z) < 1e-4)
                        {
                            var qz = _pcService.GetInterpolatedZ(p.X, p.Y);
                            if (qz.HasValue) z = qz.Value;
                        }
                        scrPts.Add(Project3DToScreen(p.X, p.Y, z, w, h));
                    }

                    g.DrawLines(pen, scrPts.ToArray());
                }
            }
        }

        private void DrawKikaiPoints3D(Graphics g, int w, int h)
        {
            using (var brush = new SolidBrush(Color.FromArgb(255, 70, 70)))
            using (var pen = new Pen(Color.White, 1.5f))
            using (var font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
            using (var textBrush = new SolidBrush(Color.White))
            {
                foreach (var k in _db.KikaiList)
                {
                    double z = k.Z;
                    if (_pcService.HasPoints && Math.Abs(z) < 1e-4)
                    {
                        var qz = _pcService.GetInterpolatedZ(k.X, k.Y);
                        if (qz.HasValue) z = qz.Value;
                    }

                    var p = Project3DToScreen(k.X, k.Y, z, w, h);
                    g.FillEllipse(brush, p.X - 5f, p.Y - 5f, 10f, 10f);
                    g.DrawEllipse(pen, p.X - 5f, p.Y - 5f, 10f, 10f);
                    g.DrawString(k.Name, font, textBrush, p.X + 8f, p.Y - 6f);
                }
            }
        }

        private void DrawHudInfo(Graphics g, int w, int h)
        {
            using (var font = new Font("Yu Gothic UI", 9F))
            using (var brush = new SolidBrush(Color.FromArgb(180, 200, 220)))
            {
                string info = $"方位角: {_camYaw:F0}° | 仰角: {_camPitch:F0}° | 視点距離: {_camDistance:F1}m | 高さ強調: {_zExaggeration:F1}x";
                g.DrawString(info, font, brush, 12, h - 25);
            }
        }
    }
}
