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

        // Point Cloud Controls
        private TextBox txtPointCloudPath = new TextBox();
        private Button btnBrowsePointCloud = new Button();
        private Button btnClearPointCloud = new Button();
        private CheckBox chkSwapPointCloudXY = new CheckBox();
        private Label lblPointCloudStatus = new Label();

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
            this.Text = "背景画像・点群設定";
            this.ClientSize = new Size(1060, 640);
            this.MinimumSize = new Size(980, 580);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(242, 244, 248);
            this.ForeColor = Color.FromArgb(30, 30, 30);
            this.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            var pnlLeft = new Panel {
                Dock = DockStyle.Left,
                Width = 360,
                BackColor = Color.FromArgb(242, 244, 248),
                Padding = new Padding(8, 6, 8, 6),
                AutoScroll = false
            };
            var pnlBottom = new Panel {
                Dock = DockStyle.Bottom,
                Height = 46,
                BackColor = Color.FromArgb(232, 235, 240)
            };
            var pnlCenter = new Panel {
                Dock = DockStyle.Fill,
                Padding = new Padding(6, 6, 6, 0),
                BackColor = Color.FromArgb(232, 235, 240)
            };

            // ==========================================
            // 1. 背景画像設定 GroupBox
            // ==========================================
            var grpBgImage = new GroupBox
            {
                Text = "🗺 背景画像設定",
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 45, 80),
                Location = new Point(8, 6),
                Size = new Size(344, 330),
                BackColor = Color.White
            };

            // 画像ファイル
            var lblImgFile = new Label { Text = "画像ファイル:", Location = new Point(10, 22), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular), ForeColor = Color.FromArgb(60, 60, 60) };
            txtImagePath.Location = new Point(10, 40);
            txtImagePath.Size = new Size(248, 23);
            txtImagePath.ReadOnly = true;
            txtImagePath.BackColor = Color.FromArgb(248, 249, 251);
            txtImagePath.ForeColor = Color.FromArgb(20, 20, 20);
            txtImagePath.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            btnBrowseImage.Text = "参照...";
            btnBrowseImage.Location = new Point(262, 39);
            btnBrowseImage.Size = new Size(72, 25);
            btnBrowseImage.BackColor = Color.FromArgb(230, 238, 248);
            btnBrowseImage.ForeColor = Color.FromArgb(20, 50, 100);
            btnBrowseImage.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            btnBrowseImage.UseVisualStyleBackColor = false;
            btnBrowseImage.Click += BtnBrowseImage_Click;

            // 基準点 1 (隙間をあけて配置)
            var lblK1 = new Label { Text = "基準点 1:", Location = new Point(10, 76), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(190, 30, 30) };
            cmbKikai1.Location = new Point(70, 73);
            cmbKikai1.Size = new Size(264, 23);
            cmbKikai1.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKikai1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
            cmbKikai1.SelectedIndexChanged += (s, e) => OnKikaiSelected(cmbKikai1, txtKikai1X, txtKikai1Y);

            var lblX1 = new Label { Text = "X:", Location = new Point(10, 102), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular), ForeColor = Color.FromArgb(60, 60, 60) };
            txtKikai1X.Location = new Point(26, 99);
            txtKikai1X.Size = new Size(110, 23);
            txtKikai1X.BackColor = Color.White;
            txtKikai1X.ForeColor = Color.Black;
            txtKikai1X.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            var lblY1 = new Label { Text = "Y:", Location = new Point(142, 102), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular), ForeColor = Color.FromArgb(60, 60, 60) };
            txtKikai1Y.Location = new Point(158, 99);
            txtKikai1Y.Size = new Size(110, 23);
            txtKikai1Y.BackColor = Color.White;
            txtKikai1Y.ForeColor = Color.Black;
            txtKikai1Y.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            btnSetPoint1.Text = "🎯 点1を指示";
            btnSetPoint1.Location = new Point(10, 126);
            btnSetPoint1.Size = new Size(126, 26);
            btnSetPoint1.BackColor = Color.FromArgb(254, 226, 226);
            btnSetPoint1.ForeColor = Color.FromArgb(185, 28, 28);
            btnSetPoint1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            btnSetPoint1.UseVisualStyleBackColor = false;
            btnSetPoint1.Click += (s, e) => SetPickMode(1);

            lblPoint1Pix.Text = "未指示";
            lblPoint1Pix.Location = new Point(142, 131);
            lblPoint1Pix.AutoSize = true;
            lblPoint1Pix.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            lblPoint1Pix.ForeColor = Color.FromArgb(185, 28, 28);

            // 基準点 2 (隙間をあけて配置)
            var lblK2 = new Label { Text = "基準点 2:", Location = new Point(10, 163), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(25, 80, 190) };
            cmbKikai2.Location = new Point(70, 160);
            cmbKikai2.Size = new Size(264, 23);
            cmbKikai2.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKikai2.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
            cmbKikai2.SelectedIndexChanged += (s, e) => OnKikaiSelected(cmbKikai2, txtKikai2X, txtKikai2Y);

            var lblX2 = new Label { Text = "X:", Location = new Point(10, 189), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular), ForeColor = Color.FromArgb(60, 60, 60) };
            txtKikai2X.Location = new Point(26, 186);
            txtKikai2X.Size = new Size(110, 23);
            txtKikai2X.BackColor = Color.White;
            txtKikai2X.ForeColor = Color.Black;
            txtKikai2X.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            var lblY2 = new Label { Text = "Y:", Location = new Point(142, 189), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular), ForeColor = Color.FromArgb(60, 60, 60) };
            txtKikai2Y.Location = new Point(158, 186);
            txtKikai2Y.Size = new Size(110, 23);
            txtKikai2Y.BackColor = Color.White;
            txtKikai2Y.ForeColor = Color.Black;
            txtKikai2Y.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            btnSetPoint2.Text = "🎯 点2を指示";
            btnSetPoint2.Location = new Point(10, 213);
            btnSetPoint2.Size = new Size(126, 26);
            btnSetPoint2.BackColor = Color.FromArgb(219, 234, 254);
            btnSetPoint2.ForeColor = Color.FromArgb(29, 78, 216);
            btnSetPoint2.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            btnSetPoint2.UseVisualStyleBackColor = false;
            btnSetPoint2.Click += (s, e) => SetPickMode(2);

            lblPoint2Pix.Text = "未指示";
            lblPoint2Pix.Location = new Point(142, 218);
            lblPoint2Pix.AutoSize = true;
            lblPoint2Pix.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            lblPoint2Pix.ForeColor = Color.FromArgb(29, 78, 216);

            // 180°反転 (隙間をあけて配置)
            var btnSwap = new Button
            {
                Text = "🔄 2点を入れ替えて180°反転",
                Location = new Point(10, 250),
                Size = new Size(324, 28),
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(40, 40, 40),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular),
                UseVisualStyleBackColor = false
            };
            btnSwap.Click += (s, e) => SwapPoints();

            // 不透明度 (隙間をあけて配置)
            var lblOpacity = new Label { Text = "不透明度:", Location = new Point(10, 292), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular), ForeColor = Color.FromArgb(60, 60, 60) };
            trkOpacity.Location = new Point(70, 285);
            trkOpacity.Size = new Size(215, 30);
            trkOpacity.Minimum = 10;
            trkOpacity.Maximum = 100;
            trkOpacity.Value = 80;
            trkOpacity.TickFrequency = 10;
            trkOpacity.ValueChanged += (s, e) => {
                lblOpacityVal.Text = $"{trkOpacity.Value}%";
            };

            lblOpacityVal.Text = "80%";
            lblOpacityVal.Location = new Point(288, 292);
            lblOpacityVal.AutoSize = true;
            lblOpacityVal.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            lblOpacityVal.ForeColor = Color.FromArgb(20, 20, 20);

            grpBgImage.Controls.AddRange(new Control[] {
                lblImgFile, txtImagePath, btnBrowseImage,
                lblK1, cmbKikai1, lblX1, txtKikai1X, lblY1, txtKikai1Y, btnSetPoint1, lblPoint1Pix,
                lblK2, cmbKikai2, lblX2, txtKikai2X, lblY2, txtKikai2Y, btnSetPoint2, lblPoint2Pix,
                btnSwap, lblOpacity, trkOpacity, lblOpacityVal
            });

            // ==========================================
            // 2. 点群設定 GroupBox
            // ==========================================
            var grpPointCloud = new GroupBox
            {
                Text = "🌐 点群データ設定",
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 45, 80),
                Location = new Point(8, 344),
                Size = new Size(344, 180),
                BackColor = Color.White
            };

            var lblPcFile = new Label { Text = "点群ファイル (XYZ / LAS / CSV):", Location = new Point(10, 20), AutoSize = true, Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular), ForeColor = Color.FromArgb(60, 60, 60) };
            txtPointCloudPath.Location = new Point(10, 38);
            txtPointCloudPath.Size = new Size(210, 23);
            txtPointCloudPath.ReadOnly = true;
            txtPointCloudPath.BackColor = Color.FromArgb(248, 249, 251);
            txtPointCloudPath.ForeColor = Color.FromArgb(20, 20, 20);
            txtPointCloudPath.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            btnBrowsePointCloud.Text = "参照...";
            btnBrowsePointCloud.Location = new Point(224, 37);
            btnBrowsePointCloud.Size = new Size(54, 25);
            btnBrowsePointCloud.BackColor = Color.FromArgb(230, 238, 248);
            btnBrowsePointCloud.ForeColor = Color.FromArgb(20, 50, 100);
            btnBrowsePointCloud.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            btnBrowsePointCloud.UseVisualStyleBackColor = false;
            btnBrowsePointCloud.Click += BtnBrowsePointCloud_Click;

            btnClearPointCloud.Text = "解除";
            btnClearPointCloud.Location = new Point(280, 37);
            btnClearPointCloud.Size = new Size(54, 25);
            btnClearPointCloud.BackColor = Color.FromArgb(254, 226, 226);
            btnClearPointCloud.ForeColor = Color.FromArgb(185, 28, 28);
            btnClearPointCloud.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
            btnClearPointCloud.UseVisualStyleBackColor = false;
            btnClearPointCloud.Click += (s, e) => {
                txtPointCloudPath.Text = "";
                PointCloudService.Instance.Clear();
                UpdatePointCloudStatusLabel();
            };

            chkSwapPointCloudXY.Text = "🔄 点群のX・Y座標を入れ替える (E/N反転)";
            chkSwapPointCloudXY.Location = new Point(10, 68);
            chkSwapPointCloudXY.AutoSize = true;
            chkSwapPointCloudXY.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
            chkSwapPointCloudXY.ForeColor = Color.FromArgb(40, 40, 40);
            chkSwapPointCloudXY.CheckedChanged += (s, e) => {
                if (PointCloudService.Instance.HasPoints)
                {
                    PointCloudService.Instance.ToggleSwapXY();
                    UpdatePointCloudStatusLabel();
                }
            };

            lblPointCloudStatus.Text = "点群未読込 (Z表示なし)";
            lblPointCloudStatus.Location = new Point(10, 93);
            lblPointCloudStatus.AutoSize = true;
            lblPointCloudStatus.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            lblPointCloudStatus.ForeColor = Color.FromArgb(100, 100, 100);

            // 3D Preview Button
            var btnOpen3D = new Button
            {
                Text = "🎮 3次元で確認 (3Dプレビュー)",
                Location = new Point(10, 120),
                Size = new Size(324, 32),
                BackColor = Color.FromArgb(43, 114, 186),
                ForeColor = Color.White,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
            btnOpen3D.Click += (s, e) => {
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
            };

            grpPointCloud.Controls.AddRange(new Control[] {
                lblPcFile, txtPointCloudPath, btnBrowsePointCloud, btnClearPointCloud,
                chkSwapPointCloudXY, lblPointCloudStatus, btnOpen3D
            });

            // Assemble left panel
            pnlLeft.Controls.Add(grpBgImage);
            pnlLeft.Controls.Add(grpPointCloud);

            // Center Preview
            picPreview.Dock = DockStyle.Fill;
            picPreview.BackColor = Color.FromArgb(24, 26, 32);
            picPreview.Paint += PicPreview_Paint;
            picPreview.MouseDown += PicPreview_MouseDown;
            picPreview.MouseMove += PicPreview_MouseMove;
            picPreview.MouseUp += PicPreview_MouseUp;
            picPreview.MouseWheel += PicPreview_MouseWheel;

            lblStatusGuide.Text = "【操作ガイド】ホイール: 拡大/縮小 | 右ドラッグ: 平行移動 | 左クリック: 点の指示";
            lblStatusGuide.Dock = DockStyle.Top;
            lblStatusGuide.Height = 26;
            lblStatusGuide.BackColor = Color.FromArgb(232, 235, 240);
            lblStatusGuide.ForeColor = Color.FromArgb(40, 50, 70);
            lblStatusGuide.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            lblStatusGuide.TextAlign = ContentAlignment.MiddleLeft;

            pnlCenter.Controls.Add(picPreview);
            pnlCenter.Controls.Add(lblStatusGuide);

            // Bottom Buttons
            btnOk.Text = "✔ 設定を適用";
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(pnlBottom.Width - 230, 8);
            btnOk.Size = new Size(110, 30);
            btnOk.BackColor = Color.FromArgb(34, 197, 94);
            btnOk.ForeColor = Color.White;
            btnOk.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold);
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += BtnOk_Click;

            btnCancel.Text = "キャンセル";
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(pnlBottom.Width - 110, 8);
            btnCancel.Size = new Size(95, 30);
            btnCancel.BackColor = Color.FromArgb(220, 224, 230);
            btnCancel.ForeColor = Color.Black;
            btnCancel.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular);
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            btnReset.Text = "🗑 解除 / リセット";
            btnReset.Location = new Point(12, 8);
            btnReset.Size = new Size(130, 30);
            btnReset.BackColor = Color.FromArgb(239, 68, 68);
            btnReset.ForeColor = Color.White;
            btnReset.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += BtnReset_Click;

            pnlBottom.Controls.AddRange(new Control[] { btnReset, btnOk, btnCancel });
            pnlBottom.Resize += (s, e) => {
                btnOk.Location = new Point(pnlBottom.Width - 230, 8);
                btnCancel.Location = new Point(pnlBottom.Width - 110, 8);
            };

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
