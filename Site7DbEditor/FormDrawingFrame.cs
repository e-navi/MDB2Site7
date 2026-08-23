using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public partial class FormDrawingFrame : Form
    {
        private readonly EditorDbManager? _db;
        private bool _isUpdatingUi = false;

        public event EventHandler? FrameChanged;
        public event EventHandler? MoveCenterRequested;
        public event EventHandler? SetRotationRequested;

        public FormDrawingFrame(EditorDbManager? db = null)
        {
            InitializeComponent();
            _db = db;

            ApplyThemeStyles();
            BindEvents();
        }

        private void ApplyThemeStyles()
        {
            this.BackColor = Color.FromArgb(28, 30, 38);
            
            // TabControlの背景
            foreach (TabPage tab in tabSettings.TabPages)
            {
                tab.BackColor = Color.FromArgb(30, 32, 42);
                ApplyGroupStyles(tab);
            }
        }

        private void ApplyGroupStyles(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is GroupBox grp)
                {
                    grp.ForeColor = Color.FromArgb(220, 225, 235);
                    foreach (Control sub in grp.Controls)
                    {
                        if (sub is Label lbl && sub != lblEffectivePitch)
                        {
                            lbl.ForeColor = Color.FromArgb(190, 195, 205);
                        }
                        else if (sub is NumericUpDown num)
                        {
                            num.BackColor = Color.FromArgb(42, 45, 56);
                            num.ForeColor = Color.White;
                        }
                        else if (sub is ComboBox cmb)
                        {
                            cmb.BackColor = Color.FromArgb(42, 45, 56);
                            cmb.ForeColor = Color.White;
                        }
                        else if (sub is RadioButton rdo)
                        {
                            rdo.ForeColor = Color.FromArgb(220, 225, 235);
                        }
                        else if (sub is CheckBox chk)
                        {
                            chk.ForeColor = Color.FromArgb(220, 225, 235);
                        }
                    }
                }
            }
        }

        private void BindEvents()
        {
            this.Load += FormDrawingFrame_Load;
            this.FormClosing += (s, e) => {
                // 閉じるボタンが押されたら閉じる代わりに非表示（Hide）
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    this.Hide();
                }
            };

            this.btnClose.Click += (s, e) => this.Hide();

            // 基本・配置
            this.chkVisible.CheckedChanged += (s, e) => OnValueChanged();
            this.cmbPaperSize.SelectedIndexChanged += (s, e) => OnValueChanged();
            this.rdoLandscape.CheckedChanged += (s, e) => OnValueChanged();
            this.rdoPortrait.CheckedChanged += (s, e) => OnValueChanged();
            this.cmbScale.SelectedIndexChanged += (s, e) => OnValueChanged();
            this.cmbScale.TextChanged += (s, e) => OnValueChanged();

            this.numCenterX.ValueChanged += (s, e) => OnValueChanged();
            this.numCenterY.ValueChanged += (s, e) => OnValueChanged();
            this.numRotation.ValueChanged += (s, e) => OnValueChanged();

            this.btnResetRotation.Click += (s, e) => {
                numRotation.Value = 0;
            };

            this.btnFitAll.Click += (s, e) => FitToAllData();

            this.btnMoveCenter.Click += (s, e) => {
                MoveCenterRequested?.Invoke(this, EventArgs.Empty);
            };

            this.btnSetRotation.Click += (s, e) => {
                SetRotationRequested?.Invoke(this, EventArgs.Empty);
            };

            // トンボ・座標
            this.chkShowTombo.CheckedChanged += (s, e) => OnValueChanged();
            this.chkShowGridLines.CheckedChanged += (s, e) => OnValueChanged();
            this.chkShowBorderCoords.CheckedChanged += (s, e) => OnValueChanged();
            this.rdoPitchAuto.CheckedChanged += (s, e) => {
                numPitchMeters.Enabled = rdoPitchManual.Checked;
                OnValueChanged();
            };
            this.rdoPitchManual.CheckedChanged += (s, e) => {
                numPitchMeters.Enabled = rdoPitchManual.Checked;
                OnValueChanged();
            };
            this.numPitchMeters.ValueChanged += (s, e) => OnValueChanged();

            // 余白・付加
            this.numMarginL.ValueChanged += (s, e) => OnValueChanged();
            this.numMarginR.ValueChanged += (s, e) => OnValueChanged();
            this.numMarginT.ValueChanged += (s, e) => OnValueChanged();
            this.numMarginB.ValueChanged += (s, e) => OnValueChanged();

            this.chkShowNorthArrow.CheckedChanged += (s, e) => OnValueChanged();
            this.cmbNorthPos.SelectedIndexChanged += (s, e) => OnValueChanged();
            this.chkShowScaleBar.CheckedChanged += (s, e) => OnValueChanged();
            this.cmbScaleBarPos.SelectedIndexChanged += (s, e) => OnValueChanged();
        }

        private void FormDrawingFrame_Load(object? sender, EventArgs e)
        {
            SyncFromService();
        }

        /// <summary>
        /// サービスの最新値をUIに反映
        /// </summary>
        public void SyncFromService()
        {
            _isUpdatingUi = true;
            try
            {
                var frame = DrawingFrameService.Instance;
                chkVisible.Checked = frame.IsVisible;

                // 用紙 & 向き
                int idx = cmbPaperSize.FindStringExact(frame.PaperSizeName);
                cmbPaperSize.SelectedIndex = idx >= 0 ? idx : 1; // Default A3

                if (frame.IsLandscape) rdoLandscape.Checked = true;
                else rdoPortrait.Checked = true;

                cmbScale.Text = frame.Scale.ToString("0");

                numCenterX.Value = (decimal)frame.CenterX;
                numCenterY.Value = (decimal)frame.CenterY;
                numRotation.Value = (decimal)frame.RotationAngleDeg;

                // トンボ & ピッチ & 座標
                chkShowTombo.Checked = frame.ShowTombo;
                chkShowGridLines.Checked = frame.ShowGridLines;
                chkShowBorderCoords.Checked = frame.ShowBorderCoords;

                if (frame.IsPitchAuto)
                {
                    rdoPitchAuto.Checked = true;
                    numPitchMeters.Enabled = false;
                }
                else
                {
                    rdoPitchManual.Checked = true;
                    numPitchMeters.Enabled = true;
                }
                numPitchMeters.Value = (decimal)Math.Max(0.1, frame.PitchMeters);
                UpdateEffectivePitchLabel();

                // 余白
                numMarginL.Value = (decimal)frame.MarginLeftMm;
                numMarginR.Value = (decimal)frame.MarginRightMm;
                numMarginT.Value = (decimal)frame.MarginTopMm;
                numMarginB.Value = (decimal)frame.MarginBottomMm;

                // 方位記号 & スケールバー
                chkShowNorthArrow.Checked = frame.ShowNorthArrow;
                int nIdx = cmbNorthPos.FindStringExact(frame.NorthArrowPosition);
                cmbNorthPos.SelectedIndex = nIdx >= 0 ? nIdx : 0; // Default "右上"

                chkShowScaleBar.Checked = frame.ShowScaleBar;
                int sIdx = cmbScaleBarPos.FindStringExact(frame.ScaleBarPosition);
                cmbScaleBarPos.SelectedIndex = sIdx >= 0 ? sIdx : 0; // Default "右下"
            }
            finally
            {
                _isUpdatingUi = false;
            }
        }

        private void UpdateEffectivePitchLabel()
        {
            var frame = DrawingFrameService.Instance;
            double p = frame.GetEffectivePitchMeters();
            lblEffectivePitch.Text = $"現在の実ピッチ: {p:0.#}m (用紙換算: {(p / frame.Scale * 1000.0):0}mm)";
        }

        private void OnValueChanged()
        {
            if (_isUpdatingUi) return;

            var frame = DrawingFrameService.Instance;
            frame.IsVisible = chkVisible.Checked;
            frame.PaperSizeName = cmbPaperSize.SelectedItem?.ToString() ?? "A3";
            frame.IsLandscape = rdoLandscape.Checked;

            if (double.TryParse(cmbScale.Text.Trim(), out double scale) && scale > 0)
            {
                frame.Scale = scale;
            }

            frame.CenterX = (double)numCenterX.Value;
            frame.CenterY = (double)numCenterY.Value;
            frame.RotationAngleDeg = (double)numRotation.Value;

            // トンボ
            frame.ShowTombo = chkShowTombo.Checked;
            frame.ShowGridLines = chkShowGridLines.Checked;
            frame.ShowBorderCoords = chkShowBorderCoords.Checked;
            frame.IsPitchAuto = rdoPitchAuto.Checked;
            frame.PitchMeters = (double)numPitchMeters.Value;

            // 余白
            frame.MarginLeftMm = (double)numMarginL.Value;
            frame.MarginRightMm = (double)numMarginR.Value;
            frame.MarginTopMm = (double)numMarginT.Value;
            frame.MarginBottomMm = (double)numMarginB.Value;

            // 付加
            frame.ShowNorthArrow = chkShowNorthArrow.Checked;
            frame.NorthArrowPosition = cmbNorthPos.SelectedItem?.ToString() ?? "右上";
            frame.ShowScaleBar = chkShowScaleBar.Checked;
            frame.ScaleBarPosition = cmbScaleBarPos.SelectedItem?.ToString() ?? "右下";

            UpdateEffectivePitchLabel();

            FrameChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 現場全データに外接する最適な中心・縮尺を自動計算して設定
        /// </summary>
        public void FitToAllData()
        {
            if (_db == null) return;

            double minX = double.MaxValue, maxX = double.MinValue;
            double minY = double.MaxValue, maxY = double.MinValue;

            foreach (var line in _db.IkouLList)
            {
                var pts = SqliteManager.ParsePrecsText(line.Precs);
                foreach (var pt in pts)
                {
                    if (pt.X < minX) minX = pt.X;
                    if (pt.X > maxX) maxX = pt.X;
                    if (pt.Y < minY) minY = pt.Y;
                    if (pt.Y > maxY) maxY = pt.Y;
                }
            }

            foreach (var k in _db.KikaiList)
            {
                if (k.X < minX) minX = k.X;
                if (k.X > maxX) maxX = k.X;
                if (k.Y < minY) minY = k.Y;
                if (k.Y > maxY) maxY = k.Y;
            }

            if (minX == double.MaxValue) return;

            double cx = (minX + maxX) / 2.0;
            double cy = (minY + maxY) / 2.0;
            double rangeX = Math.Max(1.0, maxX - minX);
            double rangeY = Math.Max(1.0, maxY - minY);

            var frame = DrawingFrameService.Instance;
            frame.CenterX = cx;
            frame.CenterY = cy;
            frame.RotationAngleDeg = 0.0;

            // 内枠寸法（mm）
            var (wMm, hMm) = frame.GetPaperDimensionsMm();
            double innerWMm = Math.Max(10.0, wMm - (frame.MarginLeftMm + frame.MarginRightMm));
            double innerHMm = Math.Max(10.0, hMm - (frame.MarginTopMm + frame.MarginBottomMm));

            // 実空間 m / (用紙 mm / 1000) = 縮尺
            double scaleX = rangeX / (innerHMm / 1000.0);
            double scaleY = rangeY / (innerWMm / 1000.0);
            double fitScale = Math.Max(scaleX, scaleY) * 1.1; // 10%余裕

            // キリの良い標準縮尺に切り上げ
            double[] standardScales = new double[] { 50, 100, 200, 250, 300, 500, 1000, 2000, 5000, 10000 };
            double finalScale = standardScales[standardScales.Length - 1];
            foreach (var s in standardScales)
            {
                if (s >= fitScale)
                {
                    finalScale = s;
                    break;
                }
            }

            frame.Scale = finalScale;

            SyncFromService();
            FrameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
