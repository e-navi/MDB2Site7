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
        public event EventHandler? PickNorthPosRequested;
        public event EventHandler? PrintRequested;

        public FormDrawingFrame(EditorDbManager? db = null)
        {
            InitializeComponent();
            _db = db;

            ApplyThemeStyles();
            BindEvents();
        }

        private void ApplyThemeStyles()
        {
            this.Text = "図枠設定・印刷";
            this.BackColor = Color.FromArgb(242, 244, 248);
            this.ForeColor = Color.FromArgb(30, 30, 30);

            chkPreviewDrawing.ForeColor = Color.FromArgb(25, 45, 80);
            chkPreviewDrawing.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold);

            foreach (TabPage tab in tabSettings.TabPages)
            {
                tab.BackColor = Color.White;
                ApplyGroupStyles(tab);
            }

            // Buttons styling
            btnResetRotation.BackColor = Color.FromArgb(240, 243, 248);
            btnResetRotation.ForeColor = Color.FromArgb(40, 40, 40);

            btnMoveCenter.BackColor = Color.FromArgb(219, 234, 254);
            btnMoveCenter.ForeColor = Color.FromArgb(29, 78, 216);
            btnMoveCenter.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);

            btnSetRotation.BackColor = Color.FromArgb(254, 243, 199);
            btnSetRotation.ForeColor = Color.FromArgb(180, 83, 9);
            btnSetRotation.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);

            btnFitAll.BackColor = Color.FromArgb(220, 252, 231);
            btnFitAll.ForeColor = Color.FromArgb(21, 128, 61);
            btnFitAll.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);

            btnPickNorthPos.BackColor = Color.FromArgb(219, 234, 254);
            btnPickNorthPos.ForeColor = Color.FromArgb(29, 78, 216);

            btnPrint.BackColor = Color.FromArgb(43, 114, 186);
            btnPrint.ForeColor = Color.White;
            btnPrint.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);

            btnClose.BackColor = Color.FromArgb(220, 224, 230);
            btnClose.ForeColor = Color.Black;
        }

        private void ApplyGroupStyles(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is GroupBox grp)
                {
                    grp.ForeColor = Color.FromArgb(25, 45, 80);
                    grp.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
                    foreach (Control sub in grp.Controls)
                    {
                        if (sub is Label lbl)
                        {
                            if (sub == lblEffectivePitch)
                            {
                                lbl.ForeColor = Color.FromArgb(20, 120, 40);
                                lbl.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
                            }
                            else
                            {
                                lbl.ForeColor = Color.FromArgb(60, 60, 60);
                                lbl.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
                            }
                        }
                        else if (sub is NumericUpDown num)
                        {
                            num.BackColor = Color.White;
                            num.ForeColor = Color.Black;
                            num.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
                        }
                        else if (sub is ComboBox cmb)
                        {
                            cmb.BackColor = Color.White;
                            cmb.ForeColor = Color.Black;
                            cmb.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
                        }
                        else if (sub is RadioButton rdo)
                        {
                            rdo.ForeColor = Color.FromArgb(40, 40, 40);
                            rdo.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
                        }
                        else if (sub is CheckBox chk)
                        {
                            chk.ForeColor = Color.FromArgb(40, 40, 40);
                            chk.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
                        }
                    }
                }
            }
        }

        private void OnDialogClosing()
        {
            if (chkPreviewDrawing.Checked)
            {
                chkPreviewDrawing.Checked = false;
            }
        }

        private void BindEvents()
        {
            this.Load += FormDrawingFrame_Load;
            this.FormClosing += (s, e) => {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    OnDialogClosing();
                    this.Hide();
                }
            };

            this.btnClose.Click += (s, e) => {
                OnDialogClosing();
                this.Hide();
            };
            this.btnPrint.Click += (s, e) => PrintRequested?.Invoke(this, EventArgs.Empty);

            // 基本・配置
            this.chkPreviewDrawing.CheckedChanged += (s, e) => OnValueChanged();
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

            // 余白・間隔
            this.numMarginLeft.ValueChanged += (s, e) => OnValueChanged();
            this.numMarginOther.ValueChanged += (s, e) => OnValueChanged();
            this.numSpacing.ValueChanged += (s, e) => OnValueChanged();

            // 方位記号
            this.chkShowNorthArrow.CheckedChanged += (s, e) => OnValueChanged();
            this.cmbNorthType.SelectedIndexChanged += (s, e) => OnValueChanged();
            this.numNorthSize.ValueChanged += (s, e) => OnValueChanged();
            this.cmbNorthPos.SelectedIndexChanged += (s, e) => OnValueChanged();
            this.btnPickNorthPos.Click += (s, e) => {
                PickNorthPosRequested?.Invoke(this, EventArgs.Empty);
            };

            // スケールバー
            this.chkShowScaleBar.CheckedChanged += (s, e) => OnValueChanged();
            this.cmbScaleBarType.SelectedIndexChanged += (s, e) => OnValueChanged();
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
                chkPreviewDrawing.Checked = frame.IsDrawingPreviewEnabled;

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

                // 余白・間隔
                numMarginLeft.Value = (decimal)frame.MarginLeftMm;
                numMarginOther.Value = (decimal)frame.MarginOtherMm;
                numSpacing.Value = (decimal)frame.OuterInnerSpacingMm;

                // 方位記号 & スケールバー
                chkShowNorthArrow.Checked = frame.ShowNorthArrow;
                int tIdx = cmbNorthType.FindStringExact(frame.NorthArrowType);
                cmbNorthType.SelectedIndex = tIdx >= 0 ? tIdx : 0; // Default "標準矢印"

                numNorthSize.Value = (decimal)Math.Max(5.0, frame.NorthArrowSizeMm);
                int nIdx = cmbNorthPos.FindStringExact(frame.NorthArrowPosition);
                cmbNorthPos.SelectedIndex = nIdx >= 0 ? nIdx : 0; // Default "右上"

                chkShowScaleBar.Checked = frame.ShowScaleBar;
                int stIdx = cmbScaleBarType.FindStringExact(frame.ScaleBarType);
                cmbScaleBarType.SelectedIndex = stIdx >= 0 ? stIdx : 0; // Default "ブロック"

                int sIdx = cmbScaleBarPos.FindStringExact(frame.ScaleBarPosition);
                cmbScaleBarPos.SelectedIndex = sIdx >= 0 ? sIdx : 0; // Default "中下"
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
            frame.IsDrawingPreviewEnabled = chkPreviewDrawing.Checked;
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

            // 余白・間隔
            frame.MarginLeftMm = (double)numMarginLeft.Value;
            frame.MarginOtherMm = (double)numMarginOther.Value;
            frame.OuterInnerSpacingMm = (double)numSpacing.Value;

            // 付加
            frame.ShowNorthArrow = chkShowNorthArrow.Checked;
            frame.NorthArrowType = cmbNorthType.SelectedItem?.ToString() ?? "標準矢印";
            frame.NorthArrowSizeMm = (double)numNorthSize.Value;
            frame.NorthArrowPosition = cmbNorthPos.SelectedItem?.ToString() ?? "右上";

            frame.ShowScaleBar = chkShowScaleBar.Checked;
            frame.ScaleBarType = cmbScaleBarType.SelectedItem?.ToString() ?? "精密線 (下縮尺)";
            frame.ScaleBarPosition = cmbScaleBarPos.SelectedItem?.ToString() ?? "中下";

            UpdateEffectivePitchLabel();

            frame.SaveToIni();
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
            var (innerWMm, innerHMm, _, _) = frame.GetInnerFrameDimensionsMeters();
            innerWMm = (innerWMm / frame.Scale) * 1000.0;
            innerHMm = (innerHMm / frame.Scale) * 1000.0;

            double scaleX = rangeX / (innerHMm / 1000.0);
            double scaleY = rangeY / (innerWMm / 1000.0);
            double fitScale = Math.Max(scaleX, scaleY) * 1.1; // 10%余裕

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
