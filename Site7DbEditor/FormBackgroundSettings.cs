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
    public partial class FormBackgroundSettings : Form
    {
        private readonly EditorDbManager _db;
        private readonly BackgroundImageService _bgService = BackgroundImageService.Instance;

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
            InitializeEvents();
            PopulateKikaiCombos();
            LoadCurrentSettings();
        }

        private void InitializeEvents()
        {
            cmbKikai1.SelectedIndexChanged += (s, e) => OnKikaiSelected(cmbKikai1, txtKikai1X, txtKikai1Y);
            cmbKikai2.SelectedIndexChanged += (s, e) => OnKikaiSelected(cmbKikai2, txtKikai2X, txtKikai2Y);
            btnSetPoint1.Click += (s, e) => SetPickMode(1);
            btnSetPoint2.Click += (s, e) => SetPickMode(2);
            btnSwap.Click += (s, e) => SwapPoints();
            trkOpacity.ValueChanged += (s, e) => {
                lblOpacityVal.Text = $"{trkOpacity.Value}%";
            };
            btnClearPointCloud.Click += (s, e) => {
                txtPointCloudPath.Text = "";
                PointCloudService.Instance.Clear();
                UpdatePointCloudStatusLabel();
            };
            chkSwapPointCloudXY.CheckedChanged += (s, e) => {
                if (PointCloudService.Instance.HasPoints)
                {
                    PointCloudService.Instance.ToggleSwapXY();
                    UpdatePointCloudStatusLabel();
                }
            };
            btnOpen3D.Click += BtnOpen3D_Click;

            picPreview.Paint += PicPreview_Paint;
            picPreview.MouseDown += PicPreview_MouseDown;
            picPreview.MouseMove += PicPreview_MouseMove;
            picPreview.MouseUp += PicPreview_MouseUp;
            picPreview.MouseWheel += PicPreview_MouseWheel;
        }

        private void BtnOpen3D_Click(object? sender, EventArgs e)
        {
            var pc = PointCloudService.Instance;
            if (pc.HasPoints)
            {
                double siteX = 0, siteY = 0;
                bool hasSite = false;

                if (_db.KikaiList.Count > 0)
                {
                    siteX = _db.KikaiList.Average(k => k.X);
                    siteY = _db.KikaiList.Average(k => k.Y);
                    hasSite = true;
                }
                else if (_bgService.Config.IsAligned)
                {
                    siteX = (_bgService.Config.Pt1_SurveyX + _bgService.Config.Pt2_SurveyX) / 2.0;
                    siteY = (_bgService.Config.Pt1_SurveyY + _bgService.Config.Pt2_SurveyY) / 2.0;
                    hasSite = true;
                }

                if (hasSite)
                {
                    if (pc.AutoDetectAndSwapXY(siteX, siteY))
                    {
                        chkSwapPointCloudXY.Checked = pc.SwapXY;
                        UpdatePointCloudStatusLabel();
                    }

                    double pcMidX = (pc.MinX + pc.MaxX) / 2.0;
                    double pcMidY = (pc.MinY + pc.MaxY) / 2.0;
                    double dist = Math.Sqrt((pcMidX - siteX) * (pcMidX - siteX) + (pcMidY - siteY) * (pcMidY - siteY));

                    if (dist > 1000.0)
                    {
                        MessageBox.Show(
                            $"⚠ 点群データの座標が現場の基準点と大きく離れています。\n\n" +
                            $"・現場基準点中心: X={siteX:F1}, Y={siteY:F1}\n" +
                            $"・点群データ中心: X={pcMidX:F1}, Y={pcMidY:F1}\n" +
                            $"・離れ距離: 約 {dist / 1000.0:F1} km ({dist:N0} m)\n\n" +
                            $"点群の測量座標系やXY反転設定をご確認ください。\n3Dビューアの起動を中止します。",
                            "点群座標の不一致 (3D起動中止)",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            using (var f3d = new Form3DViewer(_db)) {
                f3d.ShowDialog(this);
            }
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

            // 初期状態は未選択
            cmbKikai1.SelectedIndex = -1;
            cmbKikai2.SelectedIndex = -1;
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

        private void SelectMatchingKikai(ComboBox cmb, double x, double y)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (cmb.Items[i] is KikaiComboItem kci && kci.Item != null)
                {
                    if (Math.Abs(kci.Item.X - x) < 0.001 && Math.Abs(kci.Item.Y - y) < 0.001)
                    {
                        cmb.SelectedIndex = i;
                        return;
                    }
                }
            }
            cmb.SelectedIndex = -1;
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

                // 設定中座標と一致する基準点名を自動選択
                SelectMatchingKikai(cmbKikai1, cfg.Pt1_SurveyX, cfg.Pt1_SurveyY);
                SelectMatchingKikai(cmbKikai2, cfg.Pt2_SurveyX, cfg.Pt2_SurveyY);
            }
            else
            {
                // 未位置合わせ時は1番目と2番目をデフォルト選択
                if (cmbKikai1.Items.Count > 0) cmbKikai1.SelectedIndex = 0;
                if (cmbKikai2.Items.Count > 1) cmbKikai2.SelectedIndex = 1;
                else if (cmbKikai2.Items.Count > 0) cmbKikai2.SelectedIndex = 0;
            }

            trkOpacity.Value = Math.Clamp((int)(cfg.Opacity * 100), 10, 100);
            lblOpacityVal.Text = $"{trkOpacity.Value}%";

            if (!string.IsNullOrEmpty(cfg.PointCloudPath) && File.Exists(cfg.PointCloudPath))
            {
                txtPointCloudPath.Text = cfg.PointCloudPath;
                chkSwapPointCloudXY.Checked = cfg.PointCloudSwapXY;
                if (!PointCloudService.Instance.HasPoints)
                {
                    PointCloudService.Instance.LoadFile(cfg.PointCloudPath, cfg.PointCloudSwapXY);
                }
            }
            UpdatePointCloudStatusLabel();
        }

        private void BtnBrowsePointCloud_Click(object? sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "点群データ (*.las;*.xyz;*.csv;*.txt;*.pts)|*.las;*.xyz;*.csv;*.txt;*.pts|すべてのファイル (*.*)|*.*";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    txtPointCloudPath.Text = ofd.FileName;
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        bool success = PointCloudService.Instance.LoadFile(ofd.FileName, chkSwapPointCloudXY.Checked);
                        if (success)
                        {
                            // 現場基準点が存在する場合は自動XY判定
                            if (_db.KikaiList.Count > 0)
                            {
                                double avgSiteX = _db.KikaiList.Average(k => k.X);
                                double avgSiteY = _db.KikaiList.Average(k => k.Y);
                                if (PointCloudService.Instance.AutoDetectAndSwapXY(avgSiteX, avgSiteY))
                                {
                                    chkSwapPointCloudXY.Checked = PointCloudService.Instance.SwapXY;
                                }
                            }

                            UpdatePointCloudStatusLabel();
                            MessageBox.Show($"✔ 点群データを読み込みました。\n点数: {PointCloudService.Instance.Points.Count:N0} 点\nZ範囲: {PointCloudService.Instance.MinZ:F3}m ～ {PointCloudService.Instance.MaxZ:F3}m" + (PointCloudService.Instance.SwapXY ? "\n(※現場座標系に合わせてXYを自動反転しました)" : ""), "読み込み完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("点群ファイルの読み込みに失敗しました。形式をご確認ください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            UpdatePointCloudStatusLabel();
                        }
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void UpdatePointCloudStatusLabel()
        {
            var pc = PointCloudService.Instance;
            if (pc.HasPoints)
            {
                lblPointCloudStatus.Text = $"✔ {pc.Points.Count:N0}点 (Z: {pc.MinZ:F2}m ～ {pc.MaxZ:F2}m)";
                lblPointCloudStatus.ForeColor = Color.FromArgb(20, 130, 40);
            }
            else
            {
                lblPointCloudStatus.Text = "点群未読込 (Z表示なし)";
                lblPointCloudStatus.ForeColor = Color.FromArgb(100, 100, 100);
            }
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
                        SetPickMode(0); // 1点のみ指示して終了
                    }
                    else if (_currentPickMode == 2)
                    {
                        _pt2Pix = imgPt;
                        _hasPt2 = true;
                        lblPoint2Pix.Text = $"({_pt2Pix.X:F0}, {_pt2Pix.Y:F0}) px";
                        SetPickMode(0); // 1点のみ指示して終了
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
            _bgService.Config.PointCloudPath = txtPointCloudPath.Text;
            _bgService.Config.PointCloudSwapXY = chkSwapPointCloudXY.Checked;

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
            var res = MessageBox.Show("背景画像・点群データの設定を解除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                _bgService.Config.IsAligned = false;
                _bgService.Config.ImagePath = "";
                _bgService.Config.PointCloudPath = "";
                PointCloudService.Instance.Clear();
                _hasPt1 = false;
                _hasPt2 = false;
                txtImagePath.Text = "";
                txtPointCloudPath.Text = "";
                lblPoint1Pix.Text = "未指示";
                lblPoint2Pix.Text = "未指示";
                UpdatePointCloudStatusLabel();
                _previewImg?.Dispose();
                _previewImg = null;
                picPreview.Invalidate();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
