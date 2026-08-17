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
    public class FormBackgroundSettings : Form
    {
        private readonly EditorDbManager _db;
        private readonly BackgroundImageService _bgService = BackgroundImageService.Instance;

        // UI Controls
        private TextBox txtImagePath = new TextBox();
        private Button btnBrowseImage = new Button();

        private ComboBox cmbKikai1 = new ComboBox();
        private TextBox txtKikai1X = new TextBox();
        private TextBox txtKikai1Y = new TextBox();
        private Button btnSetPoint1 = new Button();
        private Label lblPoint1Pix = new Label();

        private ComboBox cmbKikai2 = new ComboBox();
        private TextBox txtKikai2X = new TextBox();
        private TextBox txtKikai2Y = new TextBox();
        private Button btnSetPoint2 = new Button();
        private Label lblPoint2Pix = new Label();

        private TrackBar trkOpacity = new TrackBar();
        private Label lblOpacityVal = new Label();

        private PictureBox picPreview = new PictureBox();
        private Button btnOk = new Button();
        private Button btnCancel = new Button();
        private Button btnReset = new Button();
        private Label lblStatusGuide = new Label();

        // State
        private Bitmap? _previewImg;
        private PointF _pt1Pix;
        private PointF _pt2Pix;
        private bool _hasPt1;
        private bool _hasPt2;
        private int _currentPickMode = 0; // 0: None, 1: Pick Point 1, 2: Pick Point 2

        // View Transform for Preview
        private float _zoom = 1.0f;
        private PointF _panOffset = new PointF(0, 0);
        private bool _isPanning = false;
        private Point _lastMousePos;

        public FormBackgroundSettings(EditorDbManager db)
        {
            _db = db;
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void InitializeComponent()
        {
            this.Text = "背景画像・位置合わせ設定 (2点アライメント)";
            this.Size = new Size(1020, 680);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(32, 34, 42);
            this.ForeColor = Color.White;
            this.Font = new Font("Yu Gothic UI", 9F);

            var pnlLeft = new Panel { Dock = DockStyle.Left, Width = 360, BackColor = Color.FromArgb(24, 26, 32), Padding = new Padding(12) };
            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 48, BackColor = Color.FromArgb(20, 22, 28) };
            var pnlCenter = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8), BackColor = Color.FromArgb(16, 16, 20) };

            // Top - Image File Selection
            var grpImage = new GroupBox { Text = "① 画像ファイルの選択", ForeColor = Color.FromArgb(0, 225, 255), Dock = DockStyle.Top, Height = 80, Padding = new Padding(8) };
            txtImagePath.Location = new Point(10, 24);
            txtImagePath.Size = new Size(240, 23);
            txtImagePath.ReadOnly = true;
            txtImagePath.BackColor = Color.FromArgb(40, 42, 54);
            txtImagePath.ForeColor = Color.White;

            btnBrowseImage.Text = "参照...";
            btnBrowseImage.Location = new Point(256, 23);
            btnBrowseImage.Size = new Size(74, 25);
            btnBrowseImage.BackColor = Color.FromArgb(43, 114, 186);
            btnBrowseImage.ForeColor = Color.White;
            btnBrowseImage.FlatStyle = FlatStyle.Flat;
            btnBrowseImage.Click += BtnBrowseImage_Click;

            grpImage.Controls.Add(txtImagePath);
            grpImage.Controls.Add(btnBrowseImage);

            // Point 1 Group
            var grpPt1 = new GroupBox { Text = "② 基準点 1 の対応付け", ForeColor = Color.FromArgb(255, 110, 110), Dock = DockStyle.Top, Height = 140, Padding = new Padding(8) };
            var lblK1 = new Label { Text = "基準点:", Location = new Point(10, 22), AutoSize = true, ForeColor = Color.LightGray };
            cmbKikai1.Location = new Point(65, 19);
            cmbKikai1.Size = new Size(265, 23);
            cmbKikai1.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKikai1.SelectedIndexChanged += (s, e) => OnKikaiSelected(cmbKikai1, txtKikai1X, txtKikai1Y);

            var lblX1 = new Label { Text = "X:", Location = new Point(10, 50), AutoSize = true, ForeColor = Color.LightGray };
            txtKikai1X.Location = new Point(30, 47);
            txtKikai1X.Size = new Size(130, 23);
            txtKikai1X.BackColor = Color.FromArgb(40, 42, 54);
            txtKikai1X.ForeColor = Color.White;

            var lblY1 = new Label { Text = "Y:", Location = new Point(170, 50), AutoSize = true, ForeColor = Color.LightGray };
            txtKikai1Y.Location = new Point(190, 47);
            txtKikai1Y.Size = new Size(140, 23);
            txtKikai1Y.BackColor = Color.FromArgb(40, 42, 54);
            txtKikai1Y.ForeColor = Color.White;

            btnSetPoint1.Text = "🎯 画像上の点1を指示";
            btnSetPoint1.Location = new Point(10, 78);
            btnSetPoint1.Size = new Size(160, 28);
            btnSetPoint1.BackColor = Color.FromArgb(210, 50, 50);
            btnSetPoint1.ForeColor = Color.White;
            btnSetPoint1.FlatStyle = FlatStyle.Flat;
            btnSetPoint1.Click += (s, e) => SetPickMode(1);

            lblPoint1Pix.Text = "未指示";
            lblPoint1Pix.Location = new Point(176, 84);
            lblPoint1Pix.AutoSize = true;
            lblPoint1Pix.ForeColor = Color.FromArgb(255, 200, 200);

            grpPt1.Controls.AddRange(new Control[] { lblK1, cmbKikai1, lblX1, txtKikai1X, lblY1, txtKikai1Y, btnSetPoint1, lblPoint1Pix });

            // Point 2 Group
            var grpPt2 = new GroupBox { Text = "③ 基準点 2 の対応付け", ForeColor = Color.FromArgb(100, 200, 255), Dock = DockStyle.Top, Height = 140, Padding = new Padding(8) };
            var lblK2 = new Label { Text = "基準点:", Location = new Point(10, 22), AutoSize = true, ForeColor = Color.LightGray };
            cmbKikai2.Location = new Point(65, 19);
            cmbKikai2.Size = new Size(265, 23);
            cmbKikai2.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKikai2.SelectedIndexChanged += (s, e) => OnKikaiSelected(cmbKikai2, txtKikai2X, txtKikai2Y);

            var lblX2 = new Label { Text = "X:", Location = new Point(10, 50), AutoSize = true, ForeColor = Color.LightGray };
            txtKikai2X.Location = new Point(30, 47);
            txtKikai2X.Size = new Size(130, 23);
            txtKikai2X.BackColor = Color.FromArgb(40, 42, 54);
            txtKikai2X.ForeColor = Color.White;

            var lblY2 = new Label { Text = "Y:", Location = new Point(170, 50), AutoSize = true, ForeColor = Color.LightGray };
            txtKikai2Y.Location = new Point(190, 47);
            txtKikai2Y.Size = new Size(140, 23);
            txtKikai2Y.BackColor = Color.FromArgb(40, 42, 54);
            txtKikai2Y.ForeColor = Color.White;

            btnSetPoint2.Text = "🎯 画像上の点2を指示";
            btnSetPoint2.Location = new Point(10, 78);
            btnSetPoint2.Size = new Size(160, 28);
            btnSetPoint2.BackColor = Color.FromArgb(30, 115, 210);
            btnSetPoint2.ForeColor = Color.White;
            btnSetPoint2.FlatStyle = FlatStyle.Flat;
            btnSetPoint2.Click += (s, e) => SetPickMode(2);

            lblPoint2Pix.Text = "未指示";
            lblPoint2Pix.Location = new Point(176, 84);
            lblPoint2Pix.AutoSize = true;
            lblPoint2Pix.ForeColor = Color.FromArgb(200, 230, 255);

            grpPt2.Controls.AddRange(new Control[] { lblK2, cmbKikai2, lblX2, txtKikai2X, lblY2, txtKikai2Y, btnSetPoint2, lblPoint2Pix });

            // Swap Points (180deg Flip)
            var btnSwap = new Button {
                Text = "🔄 2点を入れ替えて180°反転",
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.FromArgb(60, 70, 90),
                ForeColor = Color.FromArgb(255, 230, 100),
                FlatStyle = FlatStyle.Flat
            };
            btnSwap.Click += (s, e) => SwapPoints();

            // Opacity Group
            var grpOpacity = new GroupBox { Text = "④ 不透明度設定", ForeColor = Color.FromArgb(200, 200, 200), Dock = DockStyle.Top, Height = 75, Padding = new Padding(8) };
            trkOpacity.Location = new Point(10, 22);
            trkOpacity.Size = new Size(250, 45);
            trkOpacity.Minimum = 10;
            trkOpacity.Maximum = 100;
            trkOpacity.Value = 80;
            trkOpacity.TickFrequency = 10;
            trkOpacity.ValueChanged += (s, e) => {
                lblOpacityVal.Text = $"{trkOpacity.Value}%";
            };

            lblOpacityVal.Text = "80%";
            lblOpacityVal.Location = new Point(270, 25);
            lblOpacityVal.AutoSize = true;

            grpOpacity.Controls.Add(trkOpacity);
            grpOpacity.Controls.Add(lblOpacityVal);

            // Left panel assembly
            pnlLeft.Controls.Add(grpOpacity);
            pnlLeft.Controls.Add(btnSwap);
            pnlLeft.Controls.Add(grpPt2);
            pnlLeft.Controls.Add(grpPt1);
            pnlLeft.Controls.Add(grpImage);

            // Center Preview
            picPreview.Dock = DockStyle.Fill;
            picPreview.BackColor = Color.FromArgb(12, 12, 16);
            picPreview.Paint += PicPreview_Paint;
            picPreview.MouseDown += PicPreview_MouseDown;
            picPreview.MouseMove += PicPreview_MouseMove;
            picPreview.MouseUp += PicPreview_MouseUp;
            picPreview.MouseWheel += PicPreview_MouseWheel;

            lblStatusGuide.Text = "【操作ガイド】ホイール: 拡大/縮小 | 右ドラッグ: 平行移動 | 左クリック: 点の指示";
            lblStatusGuide.Dock = DockStyle.Top;
            lblStatusGuide.Height = 26;
            lblStatusGuide.ForeColor = Color.FromArgb(0, 225, 255);
            lblStatusGuide.TextAlign = ContentAlignment.MiddleLeft;

            pnlCenter.Controls.Add(picPreview);
            pnlCenter.Controls.Add(lblStatusGuide);

            // Bottom Buttons
            btnOk.Text = "✔ 設定を適用";
            btnOk.Location = new Point(this.Width - 240, 10);
            btnOk.Size = new Size(105, 30);
            btnOk.BackColor = Color.FromArgb(38, 145, 75);
            btnOk.ForeColor = Color.White;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Click += BtnOk_Click;

            btnCancel.Text = "キャンセル";
            btnCancel.Location = new Point(this.Width - 125, 10);
            btnCancel.Size = new Size(95, 30);
            btnCancel.BackColor = Color.FromArgb(60, 64, 75);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            btnReset.Text = "🗑 解除 / リセット";
            btnReset.Location = new Point(12, 10);
            btnReset.Size = new Size(120, 30);
            btnReset.BackColor = Color.FromArgb(180, 50, 50);
            btnReset.ForeColor = Color.White;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Click += BtnReset_Click;

            pnlBottom.Controls.AddRange(new Control[] { btnReset, btnOk, btnCancel });

            this.Controls.Add(pnlCenter);
            this.Controls.Add(pnlLeft);
            this.Controls.Add(pnlBottom);

            PopulateKikaiCombos();
        }

        private void PopulateKikaiCombos()
        {
            cmbKikai1.Items.Clear();
            cmbKikai2.Items.Clear();

            foreach (var k in _db.KikaiList)
            {
                string text = $"{k.Name} (X:{k.X:F3}, Y:{k.Y:F3})";
                cmbKikai1.Items.Add(new KikaiComboItem { Item = k, DisplayText = text });
                cmbKikai2.Items.Add(new KikaiComboItem { Item = k, DisplayText = text });
            }

            if (cmbKikai1.Items.Count > 0) cmbKikai1.SelectedIndex = 0;
            if (cmbKikai2.Items.Count > 1) cmbKikai2.SelectedIndex = 1;
            else if (cmbKikai2.Items.Count > 0) cmbKikai2.SelectedIndex = 0;
        }

        private class KikaiComboItem
        {
            public KikaiModel? Item { get; set; }
            public string DisplayText { get; set; } = "";
            public override string ToString() => DisplayText;
        }

        private void OnKikaiSelected(ComboBox cmb, TextBox txtX, TextBox txtY)
        {
            if (cmb.SelectedItem is KikaiComboItem kci && kci.Item != null)
            {
                txtX.Text = kci.Item.X.ToString("F3");
                txtY.Text = kci.Item.Y.ToString("F3");
            }
        }

        private void LoadCurrentSettings()
        {
            var cfg = _bgService.Config;
            if (!string.IsNullOrEmpty(cfg.ImagePath) && File.Exists(cfg.ImagePath))
            {
                txtImagePath.Text = cfg.ImagePath;
                LoadImage(cfg.ImagePath);
            }

            if (cfg.IsAligned)
            {
                _pt1Pix = new PointF(cfg.Pt1_PixelX, cfg.Pt1_PixelY);
                _pt2Pix = new PointF(cfg.Pt2_PixelX, cfg.Pt2_PixelY);
                _hasPt1 = true;
                _hasPt2 = true;

                txtKikai1X.Text = cfg.Pt1_SurveyX.ToString("F3");
                txtKikai1Y.Text = cfg.Pt1_SurveyY.ToString("F3");
                txtKikai2X.Text = cfg.Pt2_SurveyX.ToString("F3");
                txtKikai2Y.Text = cfg.Pt2_SurveyY.ToString("F3");

                lblPoint1Pix.Text = $"({_pt1Pix.X:F0}, {_pt1Pix.Y:F0}) px";
                lblPoint2Pix.Text = $"({_pt2Pix.X:F0}, {_pt2Pix.Y:F0}) px";
            }

            trkOpacity.Value = Math.Clamp((int)(cfg.Opacity * 100), 10, 100);
            lblOpacityVal.Text = $"{trkOpacity.Value}%";
        }

        private void BtnBrowseImage_Click(object? sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "画像ファイル (*.png;*.jpg;*.jpeg;*.bmp;*.tif;*.tiff)|*.png;*.jpg;*.jpeg;*.bmp;*.tif;*.tiff|すべてのファイル (*.*)|*.*";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    txtImagePath.Text = ofd.FileName;
                    LoadImage(ofd.FileName);
                }
            }
        }

        private void LoadImage(string path)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var img = Image.FromStream(fs))
                {
                    _previewImg?.Dispose();
                    _previewImg = new Bitmap(img);
                }
                ResetPreviewView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"画像読み込みエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetPreviewView()
        {
            if (_previewImg == null || picPreview.Width <= 0 || picPreview.Height <= 0) return;
            float zX = (float)picPreview.Width / _previewImg.Width;
            float zY = (float)picPreview.Height / _previewImg.Height;
            _zoom = Math.Min(zX, zY) * 0.9f;
            if (_zoom <= 0) _zoom = 1.0f;

            float cx = (_previewImg.Width * _zoom) / 2f;
            float cy = (_previewImg.Height * _zoom) / 2f;
            _panOffset = new PointF(picPreview.Width / 2f - cx, picPreview.Height / 2f - cy);
            picPreview.Invalidate();
        }

        private void SetPickMode(int mode)
        {
            if (_previewImg == null)
            {
                MessageBox.Show("先に画像ファイルを選択してください。", "案内", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _currentPickMode = mode;
            if (mode == 1)
            {
                lblStatusGuide.Text = "👉 【点1指示モード】画像上で基準点1に対応する位置を左クリックしてください。";
                picPreview.Cursor = Cursors.Cross;
            }
            else if (mode == 2)
            {
                lblStatusGuide.Text = "👉 【点2指示モード】画像上で基準点2に対応する位置を左クリックしてください。";
                picPreview.Cursor = Cursors.Cross;
            }
            else
            {
                lblStatusGuide.Text = "【操作ガイド】ホイール: 拡大/縮小 | 右ドラッグ: 平行移動 | 左クリック: 点の指示";
                picPreview.Cursor = Cursors.Default;
            }
        }

        private PointF ScreenToImage(Point screenPt)
        {
            float x = (screenPt.X - _panOffset.X) / _zoom;
            float y = (screenPt.Y - _panOffset.Y) / _zoom;
            return new PointF(x, y);
        }

        private PointF ImageToScreen(PointF imgPt)
        {
            float x = imgPt.X * _zoom + _panOffset.X;
            float y = imgPt.Y * _zoom + _panOffset.Y;
            return new PointF(x, y);
        }

        private void PicPreview_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || (e.Button == MouseButtons.Left && _currentPickMode == 0))
            {
                _isPanning = true;
                _lastMousePos = e.Location;
            }
            else if (e.Button == MouseButtons.Left && _currentPickMode != 0 && _previewImg != null)
            {
                var imgPt = ScreenToImage(e.Location);
                if (imgPt.X >= 0 && imgPt.X <= _previewImg.Width && imgPt.Y >= 0 && imgPt.Y <= _previewImg.Height)
                {
                    if (_currentPickMode == 1)
                    {
                        _pt1Pix = imgPt;
                        _hasPt1 = true;
                        lblPoint1Pix.Text = $"({_pt1Pix.X:F0}, {_pt1Pix.Y:F0}) px";
                        SetPickMode(2); // 自動で点2モードへ
                    }
                    else if (_currentPickMode == 2)
                    {
                        _pt2Pix = imgPt;
                        _hasPt2 = true;
                        lblPoint2Pix.Text = $"({_pt2Pix.X:F0}, {_pt2Pix.Y:F0}) px";
                        SetPickMode(0); // 完了
                    }
                    picPreview.Invalidate();
                }
            }
        }

        private void PicPreview_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isPanning)
            {
                int dx = e.X - _lastMousePos.X;
                int dy = e.Y - _lastMousePos.Y;
                _panOffset = new PointF(_panOffset.X + dx, _panOffset.Y + dy);
                _lastMousePos = e.Location;
                picPreview.Invalidate();
            }
        }

        private void PicPreview_MouseUp(object? sender, MouseEventArgs e)
        {
            _isPanning = false;
        }

        private void PicPreview_MouseWheel(object? sender, MouseEventArgs e)
        {
            float oldZoom = _zoom;
            float zoomFactor = e.Delta > 0 ? 1.15f : 0.85f;
            _zoom = Math.Clamp(_zoom * zoomFactor, 0.01f, 50.0f);

            // マウス位置を中心にズーム
            float mouseX = e.X;
            float mouseY = e.Y;
            _panOffset = new PointF(
                mouseX - (mouseX - _panOffset.X) * (_zoom / oldZoom),
                mouseY - (mouseY - _panOffset.Y) * (_zoom / oldZoom));

            picPreview.Invalidate();
        }

        private void PicPreview_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (_previewImg != null)
            {
                float w = _previewImg.Width * _zoom;
                float h = _previewImg.Height * _zoom;
                e.Graphics.DrawImage(_previewImg, _panOffset.X, _panOffset.Y, w, h);

                // 枠線
                using (var pen = new Pen(Color.FromArgb(100, 100, 120), 1f))
                {
                    e.Graphics.DrawRectangle(pen, _panOffset.X, _panOffset.Y, w, h);
                }

                // 点1描画
                if (_hasPt1)
                {
                    var p = ImageToScreen(_pt1Pix);
                    DrawTargetPoint(e.Graphics, p, "点1", Color.FromArgb(255, 80, 80));
                }

                // 点2描画
                if (_hasPt2)
                {
                    var p = ImageToScreen(_pt2Pix);
                    DrawTargetPoint(e.Graphics, p, "点2", Color.FromArgb(80, 180, 255));
                }

                // 2点間の線
                if (_hasPt1 && _hasPt2)
                {
                    var p1 = ImageToScreen(_pt1Pix);
                    var p2 = ImageToScreen(_pt2Pix);
                    using (var linePen = new Pen(Color.FromArgb(255, 230, 0), 1.5f) { DashStyle = DashStyle.Dash })
                    {
                        e.Graphics.DrawLine(linePen, p1, p2);
                    }
                }
            }
            else
            {
                using (var font = new Font("Yu Gothic UI", 12F))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    e.Graphics.DrawString("左側の「参照...」ボタンから画像ファイルを選択してください", font, brush, 40, 40);
                }
            }
        }

        private void DrawTargetPoint(Graphics g, PointF p, string label, Color color)
        {
            using (var brush = new SolidBrush(color))
            using (var pen = new Pen(Color.White, 2f))
            using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
            using (var textBrush = new SolidBrush(Color.White))
            {
                g.FillEllipse(brush, p.X - 5f, p.Y - 5f, 10f, 10f);
                g.DrawEllipse(pen, p.X - 8f, p.Y - 8f, 16f, 16f);
                g.DrawString(label, font, textBrush, p.X + 10f, p.Y - 8f);
            }
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtImagePath.Text) || !File.Exists(txtImagePath.Text))
            {
                MessageBox.Show("画像ファイルを選択してください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_hasPt1 || !_hasPt2)
            {
                MessageBox.Show("画像上の2点（点1・点2）をマウスで指示してください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtKikai1X.Text, out double s1X) || !double.TryParse(txtKikai1Y.Text, out double s1Y) ||
                !double.TryParse(txtKikai2X.Text, out double s2X) || !double.TryParse(txtKikai2Y.Text, out double s2Y))
            {
                MessageBox.Show("基準点1および基準点2の測量座標(X, Y)を正しく入力してください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _bgService.LoadImageFile(txtImagePath.Text);
            _bgService.SetAlignment(_pt1Pix, s1X, s1Y, _pt2Pix, s2X, s2Y);
            _bgService.Config.Opacity = trkOpacity.Value / 100.0f;
            _bgService.Config.IsVisible = true;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SwapPoints()
        {
            if (!_hasPt1 && !_hasPt2) return;

            var tmpPix = _pt1Pix;
            _pt1Pix = _pt2Pix;
            _pt2Pix = tmpPix;

            bool tmpHas = _hasPt1;
            _hasPt1 = _hasPt2;
            _hasPt2 = tmpHas;

            lblPoint1Pix.Text = _hasPt1 ? $"({_pt1Pix.X:F0}, {_pt1Pix.Y:F0}) px" : "未指示";
            lblPoint2Pix.Text = _hasPt2 ? $"({_pt2Pix.X:F0}, {_pt2Pix.Y:F0}) px" : "未指示";

            picPreview.Invalidate();
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            var res = MessageBox.Show("背景画像と位置合わせの設定を解除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                _bgService.Config.IsAligned = false;
                _bgService.Config.ImagePath = "";
                _hasPt1 = false;
                _hasPt2 = false;
                txtImagePath.Text = "";
                lblPoint1Pix.Text = "未指示";
                lblPoint2Pix.Text = "未指示";
                _previewImg?.Dispose();
                _previewImg = null;
                picPreview.Invalidate();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
