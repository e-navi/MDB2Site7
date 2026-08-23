namespace Site7DbEditor
{
    partial class FormDrawingFrame
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabBasic = new System.Windows.Forms.TabPage();
            this.grpPaper = new System.Windows.Forms.GroupBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.rdoPortrait = new System.Windows.Forms.RadioButton();
            this.grpScale = new System.Windows.Forms.GroupBox();
            this.lblScalePrefix = new System.Windows.Forms.Label();
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.grpTransform = new System.Windows.Forms.GroupBox();
            this.lblCenterX = new System.Windows.Forms.Label();
            this.numCenterX = new System.Windows.Forms.NumericUpDown();
            this.lblCenterY = new System.Windows.Forms.Label();
            this.numCenterY = new System.Windows.Forms.NumericUpDown();
            this.lblRotation = new System.Windows.Forms.Label();
            this.numRotation = new System.Windows.Forms.NumericUpDown();
            this.btnResetRotation = new System.Windows.Forms.Button();
            this.btnMoveCenter = new System.Windows.Forms.Button();
            this.btnSetRotation = new System.Windows.Forms.Button();
            this.btnFitAll = new System.Windows.Forms.Button();
            this.tabTombo = new System.Windows.Forms.TabPage();
            this.grpTombo = new System.Windows.Forms.GroupBox();
            this.chkShowTombo = new System.Windows.Forms.CheckBox();
            this.chkShowGridLines = new System.Windows.Forms.CheckBox();
            this.chkShowBorderCoords = new System.Windows.Forms.CheckBox();
            this.grpPitch = new System.Windows.Forms.GroupBox();
            this.rdoPitchAuto = new System.Windows.Forms.RadioButton();
            this.rdoPitchManual = new System.Windows.Forms.RadioButton();
            this.numPitchMeters = new System.Windows.Forms.NumericUpDown();
            this.lblPitchUnit = new System.Windows.Forms.Label();
            this.lblEffectivePitch = new System.Windows.Forms.Label();
            this.tabExtras = new System.Windows.Forms.TabPage();
            this.grpMargin = new System.Windows.Forms.GroupBox();
            this.lblMarginL = new System.Windows.Forms.Label();
            this.numMarginL = new System.Windows.Forms.NumericUpDown();
            this.lblMarginR = new System.Windows.Forms.Label();
            this.numMarginR = new System.Windows.Forms.NumericUpDown();
            this.lblMarginT = new System.Windows.Forms.Label();
            this.numMarginT = new System.Windows.Forms.NumericUpDown();
            this.lblMarginB = new System.Windows.Forms.Label();
            this.numMarginB = new System.Windows.Forms.NumericUpDown();
            this.grpNorthArrow = new System.Windows.Forms.GroupBox();
            this.chkShowNorthArrow = new System.Windows.Forms.CheckBox();
            this.lblNorthPos = new System.Windows.Forms.Label();
            this.cmbNorthPos = new System.Windows.Forms.ComboBox();
            this.grpScaleBar = new System.Windows.Forms.GroupBox();
            this.chkShowScaleBar = new System.Windows.Forms.CheckBox();
            this.lblScaleBarPos = new System.Windows.Forms.Label();
            this.cmbScaleBarPos = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabSettings.SuspendLayout();
            this.tabBasic.SuspendLayout();
            this.grpPaper.SuspendLayout();
            this.grpScale.SuspendLayout();
            this.grpTransform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRotation)).BeginInit();
            this.tabTombo.SuspendLayout();
            this.grpTombo.SuspendLayout();
            this.grpPitch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPitchMeters)).BeginInit();
            this.tabExtras.SuspendLayout();
            this.grpMargin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginB)).BeginInit();
            this.grpNorthArrow.SuspendLayout();
            this.grpScaleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkVisible
            // 
            this.chkVisible.AutoSize = true;
            this.chkVisible.Checked = true;
            this.chkVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisible.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkVisible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.chkVisible.Location = new System.Drawing.Point(12, 8);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(124, 21);
            this.chkVisible.TabIndex = 0;
            this.chkVisible.Text = "全図図枠を表示";
            this.chkVisible.UseVisualStyleBackColor = true;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabBasic);
            this.tabSettings.Controls.Add(this.tabTombo);
            this.tabSettings.Controls.Add(this.tabExtras);
            this.tabSettings.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tabSettings.Location = new System.Drawing.Point(8, 32);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(278, 318);
            this.tabSettings.TabIndex = 1;
            // 
            // tabBasic
            // 
            this.tabBasic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.tabBasic.Controls.Add(this.grpPaper);
            this.tabBasic.Controls.Add(this.grpScale);
            this.tabBasic.Controls.Add(this.grpTransform);
            this.tabBasic.Location = new System.Drawing.Point(4, 22);
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tabBasic.Size = new System.Drawing.Size(270, 292);
            this.tabBasic.TabIndex = 0;
            this.tabBasic.Text = "基本・配置";
            // 
            // grpPaper
            // 
            this.grpPaper.Controls.Add(this.rdoPortrait);
            this.grpPaper.Controls.Add(this.rdoLandscape);
            this.grpPaper.Controls.Add(this.cmbPaperSize);
            this.grpPaper.Controls.Add(this.lblPaperSize);
            this.grpPaper.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpPaper.ForeColor = System.Drawing.Color.White;
            this.grpPaper.Location = new System.Drawing.Point(6, 4);
            this.grpPaper.Name = "grpPaper";
            this.grpPaper.Size = new System.Drawing.Size(256, 50);
            this.grpPaper.TabIndex = 0;
            this.grpPaper.TabStop = false;
            this.grpPaper.Text = "用紙設定";
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(6, 22);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(34, 15);
            this.lblPaperSize.TabIndex = 0;
            this.lblPaperSize.Text = "サイズ:";
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Items.AddRange(new object[] {
            "A4",
            "A3",
            "A2",
            "A1",
            "A0"});
            this.cmbPaperSize.Location = new System.Drawing.Point(44, 18);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(68, 21);
            this.cmbPaperSize.TabIndex = 1;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Checked = true;
            this.rdoLandscape.Location = new System.Drawing.Point(125, 20);
            this.rdoLandscape.Name = "rdoLandscape";
            this.rdoLandscape.Size = new System.Drawing.Size(37, 19);
            this.rdoLandscape.TabIndex = 2;
            this.rdoLandscape.TabStop = true;
            this.rdoLandscape.Text = "横";
            this.rdoLandscape.UseVisualStyleBackColor = true;
            // 
            // rdoPortrait
            // 
            this.rdoPortrait.AutoSize = true;
            this.rdoPortrait.Location = new System.Drawing.Point(175, 20);
            this.rdoPortrait.Name = "rdoPortrait";
            this.rdoPortrait.Size = new System.Drawing.Size(37, 19);
            this.rdoPortrait.TabIndex = 3;
            this.rdoPortrait.Text = "縦";
            this.rdoPortrait.UseVisualStyleBackColor = true;
            // 
            // grpScale
            // 
            this.grpScale.Controls.Add(this.cmbScale);
            this.grpScale.Controls.Add(this.lblScalePrefix);
            this.grpScale.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpScale.ForeColor = System.Drawing.Color.White;
            this.grpScale.Location = new System.Drawing.Point(6, 56);
            this.grpScale.Name = "grpScale";
            this.grpScale.Size = new System.Drawing.Size(256, 48);
            this.grpScale.TabIndex = 1;
            this.grpScale.TabStop = false;
            this.grpScale.Text = "図面縮尺";
            // 
            // lblScalePrefix
            // 
            this.lblScalePrefix.AutoSize = true;
            this.lblScalePrefix.Location = new System.Drawing.Point(6, 21);
            this.lblScalePrefix.Name = "lblScalePrefix";
            this.lblScalePrefix.Size = new System.Drawing.Size(33, 15);
            this.lblScalePrefix.TabIndex = 0;
            this.lblScalePrefix.Text = "縮尺: 1 /";
            // 
            // cmbScale
            // 
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.Items.AddRange(new object[] {
            "50",
            "100",
            "200",
            "250",
            "300",
            "500",
            "1000",
            "2000",
            "5000"});
            this.cmbScale.Location = new System.Drawing.Point(66, 18);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(100, 21);
            this.cmbScale.TabIndex = 1;
            // 
            // grpTransform
            // 
            this.grpTransform.Controls.Add(this.btnResetRotation);
            this.grpTransform.Controls.Add(this.btnFitAll);
            this.grpTransform.Controls.Add(this.btnMoveCenter);
            this.grpTransform.Controls.Add(this.btnSetRotation);
            this.grpTransform.Controls.Add(this.numRotation);
            this.grpTransform.Controls.Add(this.lblRotation);
            this.grpTransform.Controls.Add(this.numCenterY);
            this.grpTransform.Controls.Add(this.lblCenterY);
            this.grpTransform.Controls.Add(this.numCenterX);
            this.grpTransform.Controls.Add(this.lblCenterX);
            this.grpTransform.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpTransform.ForeColor = System.Drawing.Color.White;
            this.grpTransform.Location = new System.Drawing.Point(6, 106);
            this.grpTransform.Name = "grpTransform";
            this.grpTransform.Size = new System.Drawing.Size(256, 180);
            this.grpTransform.TabIndex = 2;
            this.grpTransform.TabStop = false;
            this.grpTransform.Text = "中心座標 & 回転";
            // 
            // lblCenterX
            // 
            this.lblCenterX.AutoSize = true;
            this.lblCenterX.Location = new System.Drawing.Point(6, 21);
            this.lblCenterX.Name = "lblCenterX";
            this.lblCenterX.Size = new System.Drawing.Size(43, 15);
            this.lblCenterX.TabIndex = 0;
            this.lblCenterX.Text = "北 X(m):";
            // 
            // numCenterX
            // 
            this.numCenterX.DecimalPlaces = 3;
            this.numCenterX.Location = new System.Drawing.Point(58, 18);
            this.numCenterX.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numCenterX.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numCenterX.Name = "numCenterX";
            this.numCenterX.Size = new System.Drawing.Size(188, 23);
            this.numCenterX.TabIndex = 1;
            this.numCenterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCenterY
            // 
            this.lblCenterY.AutoSize = true;
            this.lblCenterY.Location = new System.Drawing.Point(6, 47);
            this.lblCenterY.Name = "lblCenterY";
            this.lblCenterY.Size = new System.Drawing.Size(42, 15);
            this.lblCenterY.TabIndex = 2;
            this.lblCenterY.Text = "東 Y(m):";
            // 
            // numCenterY
            // 
            this.numCenterY.DecimalPlaces = 3;
            this.numCenterY.Location = new System.Drawing.Point(58, 44);
            this.numCenterY.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numCenterY.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numCenterY.Name = "numCenterY";
            this.numCenterY.Size = new System.Drawing.Size(188, 23);
            this.numCenterY.TabIndex = 3;
            this.numCenterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRotation
            // 
            this.lblRotation.AutoSize = true;
            this.lblRotation.Location = new System.Drawing.Point(6, 73);
            this.lblRotation.Name = "lblRotation";
            this.lblRotation.Size = new System.Drawing.Size(46, 15);
            this.lblRotation.TabIndex = 4;
            this.lblRotation.Text = "回転(°):";
            // 
            // numRotation
            // 
            this.numRotation.DecimalPlaces = 2;
            this.numRotation.Location = new System.Drawing.Point(58, 70);
            this.numRotation.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numRotation.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numRotation.Name = "numRotation";
            this.numRotation.Size = new System.Drawing.Size(122, 23);
            this.numRotation.TabIndex = 5;
            this.numRotation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnResetRotation
            // 
            this.btnResetRotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(65)))), ((int)(((byte)(80)))));
            this.btnResetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetRotation.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnResetRotation.Location = new System.Drawing.Point(186, 70);
            this.btnResetRotation.Name = "btnResetRotation";
            this.btnResetRotation.Size = new System.Drawing.Size(60, 23);
            this.btnResetRotation.TabIndex = 6;
            this.btnResetRotation.Text = "0°リセット";
            this.btnResetRotation.UseVisualStyleBackColor = false;
            // 
            // btnMoveCenter
            // 
            this.btnMoveCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnMoveCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveCenter.Location = new System.Drawing.Point(8, 102);
            this.btnMoveCenter.Name = "btnMoveCenter";
            this.btnMoveCenter.Size = new System.Drawing.Size(115, 28);
            this.btnMoveCenter.TabIndex = 7;
            this.btnMoveCenter.Text = "📍 中心移動";
            this.btnMoveCenter.UseVisualStyleBackColor = false;
            // 
            // btnSetRotation
            // 
            this.btnSetRotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.btnSetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetRotation.Location = new System.Drawing.Point(131, 102);
            this.btnSetRotation.Name = "btnSetRotation";
            this.btnSetRotation.Size = new System.Drawing.Size(115, 28);
            this.btnSetRotation.TabIndex = 8;
            this.btnSetRotation.Text = "🔄 回転指定";
            this.btnSetRotation.UseVisualStyleBackColor = false;
            // 
            // btnFitAll
            // 
            this.btnFitAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(140)))), ((int)(((byte)(90)))));
            this.btnFitAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFitAll.Location = new System.Drawing.Point(8, 138);
            this.btnFitAll.Name = "btnFitAll";
            this.btnFitAll.Size = new System.Drawing.Size(238, 28);
            this.btnFitAll.TabIndex = 9;
            this.btnFitAll.Text = "🔍 現場全体にフィット";
            this.btnFitAll.UseVisualStyleBackColor = false;
            // 
            // tabTombo
            // 
            this.tabTombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.tabTombo.Controls.Add(this.grpTombo);
            this.tabTombo.Controls.Add(this.grpPitch);
            this.tabTombo.Location = new System.Drawing.Point(4, 22);
            this.tabTombo.Name = "tabTombo";
            this.tabTombo.Padding = new System.Windows.Forms.Padding(3);
            this.tabTombo.Size = new System.Drawing.Size(270, 292);
            this.tabTombo.TabIndex = 1;
            this.tabTombo.Text = "トンボ・座標";
            // 
            // grpTombo
            // 
            this.grpTombo.Controls.Add(this.chkShowBorderCoords);
            this.grpTombo.Controls.Add(this.chkShowGridLines);
            this.grpTombo.Controls.Add(this.chkShowTombo);
            this.grpTombo.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpTombo.ForeColor = System.Drawing.Color.White;
            this.grpTombo.Location = new System.Drawing.Point(6, 6);
            this.grpTombo.Name = "grpTombo";
            this.grpTombo.Size = new System.Drawing.Size(256, 115);
            this.grpTombo.TabIndex = 0;
            this.grpTombo.TabStop = false;
            this.grpTombo.Text = "トンボ / 格子 / 座標表示";
            // 
            // chkShowTombo
            // 
            this.chkShowTombo.AutoSize = true;
            this.chkShowTombo.Checked = true;
            this.chkShowTombo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTombo.Location = new System.Drawing.Point(12, 24);
            this.chkShowTombo.Name = "chkShowTombo";
            this.chkShowTombo.Size = new System.Drawing.Size(126, 19);
            this.chkShowTombo.TabIndex = 0;
            this.chkShowTombo.Text = "トンボ (+) を表示する";
            this.chkShowTombo.UseVisualStyleBackColor = true;
            // 
            // chkShowGridLines
            // 
            this.chkShowGridLines.AutoSize = true;
            this.chkShowGridLines.Location = new System.Drawing.Point(12, 52);
            this.chkShowGridLines.Name = "chkShowGridLines";
            this.chkShowGridLines.Size = new System.Drawing.Size(147, 19);
            this.chkShowGridLines.TabIndex = 1;
            this.chkShowGridLines.Text = "格子線（破線）を表示する";
            this.chkShowGridLines.UseVisualStyleBackColor = true;
            // 
            // chkShowBorderCoords
            // 
            this.chkShowBorderCoords.AutoSize = true;
            this.chkShowBorderCoords.Checked = true;
            this.chkShowBorderCoords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowBorderCoords.Location = new System.Drawing.Point(12, 80);
            this.chkShowBorderCoords.Name = "chkShowBorderCoords";
            this.chkShowBorderCoords.Size = new System.Drawing.Size(189, 19);
            this.chkShowBorderCoords.TabIndex = 2;
            this.chkShowBorderCoords.Text = "外枠・内枠間に座標値を表示する";
            this.chkShowBorderCoords.UseVisualStyleBackColor = true;
            // 
            // grpPitch
            // 
            this.grpPitch.Controls.Add(this.lblEffectivePitch);
            this.grpPitch.Controls.Add(this.lblPitchUnit);
            this.grpPitch.Controls.Add(this.numPitchMeters);
            this.grpPitch.Controls.Add(this.rdoPitchManual);
            this.grpPitch.Controls.Add(this.rdoPitchAuto);
            this.grpPitch.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpPitch.ForeColor = System.Drawing.Color.White;
            this.grpPitch.Location = new System.Drawing.Point(6, 130);
            this.grpPitch.Name = "grpPitch";
            this.grpPitch.Size = new System.Drawing.Size(256, 145);
            this.grpPitch.TabIndex = 1;
            this.grpPitch.TabStop = false;
            this.grpPitch.Text = "ピッチ（間隔）設定";
            // 
            // rdoPitchAuto
            // 
            this.rdoPitchAuto.AutoSize = true;
            this.rdoPitchAuto.Checked = true;
            this.rdoPitchAuto.Location = new System.Drawing.Point(12, 25);
            this.rdoPitchAuto.Name = "rdoPitchAuto";
            this.rdoPitchAuto.Size = new System.Drawing.Size(193, 19);
            this.rdoPitchAuto.TabIndex = 0;
            this.rdoPitchAuto.TabStop = true;
            this.rdoPitchAuto.Text = "自動計算（用紙 100mm ピッチ）";
            this.rdoPitchAuto.UseVisualStyleBackColor = true;
            // 
            // rdoPitchManual
            // 
            this.rdoPitchManual.AutoSize = true;
            this.rdoPitchManual.Location = new System.Drawing.Point(12, 55);
            this.rdoPitchManual.Name = "rdoPitchManual";
            this.rdoPitchManual.Size = new System.Drawing.Size(73, 19);
            this.rdoPitchManual.TabIndex = 1;
            this.rdoPitchManual.Text = "手動指定:";
            this.rdoPitchManual.UseVisualStyleBackColor = true;
            // 
            // numPitchMeters
            // 
            this.numPitchMeters.DecimalPlaces = 1;
            this.numPitchMeters.Location = new System.Drawing.Point(92, 54);
            this.numPitchMeters.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPitchMeters.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPitchMeters.Name = "numPitchMeters";
            this.numPitchMeters.Size = new System.Drawing.Size(80, 23);
            this.numPitchMeters.TabIndex = 2;
            this.numPitchMeters.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPitchMeters.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblPitchUnit
            // 
            this.lblPitchUnit.AutoSize = true;
            this.lblPitchUnit.Location = new System.Drawing.Point(176, 58);
            this.lblPitchUnit.Name = "lblPitchUnit";
            this.lblPitchUnit.Size = new System.Drawing.Size(18, 15);
            this.lblPitchUnit.TabIndex = 3;
            this.lblPitchUnit.Text = "m";
            // 
            // lblEffectivePitch
            // 
            this.lblEffectivePitch.AutoSize = true;
            this.lblEffectivePitch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblEffectivePitch.Location = new System.Drawing.Point(12, 95);
            this.lblEffectivePitch.Name = "lblEffectivePitch";
            this.lblEffectivePitch.Size = new System.Drawing.Size(126, 15);
            this.lblEffectivePitch.TabIndex = 4;
            this.lblEffectivePitch.Text = "現在の実ピッチ: 20.0m";
            // 
            // tabExtras
            // 
            this.tabExtras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.tabExtras.Controls.Add(this.grpScaleBar);
            this.tabExtras.Controls.Add(this.grpNorthArrow);
            this.tabExtras.Controls.Add(this.grpMargin);
            this.tabExtras.Location = new System.Drawing.Point(4, 22);
            this.tabExtras.Name = "tabExtras";
            this.tabExtras.Padding = new System.Windows.Forms.Padding(3);
            this.tabExtras.Size = new System.Drawing.Size(270, 292);
            this.tabExtras.TabIndex = 2;
            this.tabExtras.Text = "余白・付加";
            // 
            // grpMargin
            // 
            this.grpMargin.Controls.Add(this.numMarginB);
            this.grpMargin.Controls.Add(this.lblMarginB);
            this.grpMargin.Controls.Add(this.numMarginT);
            this.grpMargin.Controls.Add(this.lblMarginT);
            this.grpMargin.Controls.Add(this.numMarginR);
            this.grpMargin.Controls.Add(this.lblMarginR);
            this.grpMargin.Controls.Add(this.numMarginL);
            this.grpMargin.Controls.Add(this.lblMarginL);
            this.grpMargin.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMargin.ForeColor = System.Drawing.Color.White;
            this.grpMargin.Location = new System.Drawing.Point(6, 6);
            this.grpMargin.Name = "grpMargin";
            this.grpMargin.Size = new System.Drawing.Size(256, 76);
            this.grpMargin.TabIndex = 0;
            this.grpMargin.TabStop = false;
            this.grpMargin.Text = "内枠余白 (mm)";
            // 
            // lblMarginL
            // 
            this.lblMarginL.AutoSize = true;
            this.lblMarginL.Location = new System.Drawing.Point(8, 22);
            this.lblMarginL.Name = "lblMarginL";
            this.lblMarginL.Size = new System.Drawing.Size(22, 15);
            this.lblMarginL.TabIndex = 0;
            this.lblMarginL.Text = "左:";
            // 
            // numMarginL
            // 
            this.numMarginL.Location = new System.Drawing.Point(32, 19);
            this.numMarginL.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginL.Name = "numMarginL";
            this.numMarginL.Size = new System.Drawing.Size(48, 23);
            this.numMarginL.TabIndex = 1;
            this.numMarginL.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblMarginR
            // 
            this.lblMarginR.AutoSize = true;
            this.lblMarginR.Location = new System.Drawing.Point(90, 22);
            this.lblMarginR.Name = "lblMarginR";
            this.lblMarginR.Size = new System.Drawing.Size(22, 15);
            this.lblMarginR.TabIndex = 2;
            this.lblMarginR.Text = "右:";
            // 
            // numMarginR
            // 
            this.numMarginR.Location = new System.Drawing.Point(114, 19);
            this.numMarginR.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginR.Name = "numMarginR";
            this.numMarginR.Size = new System.Drawing.Size(48, 23);
            this.numMarginR.TabIndex = 3;
            this.numMarginR.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblMarginT
            // 
            this.lblMarginT.AutoSize = true;
            this.lblMarginT.Location = new System.Drawing.Point(8, 48);
            this.lblMarginT.Name = "lblMarginT";
            this.lblMarginT.Size = new System.Drawing.Size(22, 15);
            this.lblMarginT.TabIndex = 4;
            this.lblMarginT.Text = "上:";
            // 
            // numMarginT
            // 
            this.numMarginT.Location = new System.Drawing.Point(32, 45);
            this.numMarginT.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginT.Name = "numMarginT";
            this.numMarginT.Size = new System.Drawing.Size(48, 23);
            this.numMarginT.TabIndex = 5;
            this.numMarginT.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblMarginB
            // 
            this.lblMarginB.AutoSize = true;
            this.lblMarginB.Location = new System.Drawing.Point(90, 48);
            this.lblMarginB.Name = "lblMarginB";
            this.lblMarginB.Size = new System.Drawing.Size(22, 15);
            this.lblMarginB.TabIndex = 6;
            this.lblMarginB.Text = "下:";
            // 
            // numMarginB
            // 
            this.numMarginB.Location = new System.Drawing.Point(114, 45);
            this.numMarginB.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginB.Name = "numMarginB";
            this.numMarginB.Size = new System.Drawing.Size(48, 23);
            this.numMarginB.TabIndex = 7;
            this.numMarginB.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // grpNorthArrow
            // 
            this.grpNorthArrow.Controls.Add(this.cmbNorthPos);
            this.grpNorthArrow.Controls.Add(this.lblNorthPos);
            this.grpNorthArrow.Controls.Add(this.chkShowNorthArrow);
            this.grpNorthArrow.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpNorthArrow.ForeColor = System.Drawing.Color.White;
            this.grpNorthArrow.Location = new System.Drawing.Point(6, 88);
            this.grpNorthArrow.Name = "grpNorthArrow";
            this.grpNorthArrow.Size = new System.Drawing.Size(256, 80);
            this.grpNorthArrow.TabIndex = 1;
            this.grpNorthArrow.TabStop = false;
            this.grpNorthArrow.Text = "方位記号（北矢印）";
            // 
            // chkShowNorthArrow
            // 
            this.chkShowNorthArrow.AutoSize = true;
            this.chkShowNorthArrow.Checked = true;
            this.chkShowNorthArrow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowNorthArrow.Location = new System.Drawing.Point(12, 22);
            this.chkShowNorthArrow.Name = "chkShowNorthArrow";
            this.chkShowNorthArrow.Size = new System.Drawing.Size(133, 19);
            this.chkShowNorthArrow.TabIndex = 0;
            this.chkShowNorthArrow.Text = "方位記号を表示する";
            this.chkShowNorthArrow.UseVisualStyleBackColor = true;
            // 
            // lblNorthPos
            // 
            this.lblNorthPos.AutoSize = true;
            this.lblNorthPos.Location = new System.Drawing.Point(10, 49);
            this.lblNorthPos.Name = "lblNorthPos";
            this.lblNorthPos.Size = new System.Drawing.Size(56, 15);
            this.lblNorthPos.TabIndex = 1;
            this.lblNorthPos.Text = "配置位置:";
            // 
            // cmbNorthPos
            // 
            this.cmbNorthPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNorthPos.FormattingEnabled = true;
            this.cmbNorthPos.Items.AddRange(new object[] {
            "右上",
            "左上",
            "右下",
            "左下"});
            this.cmbNorthPos.Location = new System.Drawing.Point(72, 46);
            this.cmbNorthPos.Name = "cmbNorthPos";
            this.cmbNorthPos.Size = new System.Drawing.Size(80, 21);
            this.cmbNorthPos.TabIndex = 2;
            // 
            // grpScaleBar
            // 
            this.grpScaleBar.Controls.Add(this.cmbScaleBarPos);
            this.grpScaleBar.Controls.Add(this.lblScaleBarPos);
            this.grpScaleBar.Controls.Add(this.chkShowScaleBar);
            this.grpScaleBar.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpScaleBar.ForeColor = System.Drawing.Color.White;
            this.grpScaleBar.Location = new System.Drawing.Point(6, 174);
            this.grpScaleBar.Name = "grpScaleBar";
            this.grpScaleBar.Size = new System.Drawing.Size(256, 80);
            this.grpScaleBar.TabIndex = 2;
            this.grpScaleBar.TabStop = false;
            this.grpScaleBar.Text = "縮尺スケールバー";
            // 
            // chkShowScaleBar
            // 
            this.chkShowScaleBar.AutoSize = true;
            this.chkShowScaleBar.Checked = true;
            this.chkShowScaleBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowScaleBar.Location = new System.Drawing.Point(12, 22);
            this.chkShowScaleBar.Name = "chkShowScaleBar";
            this.chkShowScaleBar.Size = new System.Drawing.Size(144, 19);
            this.chkShowScaleBar.TabIndex = 0;
            this.chkShowScaleBar.Text = "スケールバーを表示する";
            this.chkShowScaleBar.UseVisualStyleBackColor = true;
            // 
            // lblScaleBarPos
            // 
            this.lblScaleBarPos.AutoSize = true;
            this.lblScaleBarPos.Location = new System.Drawing.Point(10, 49);
            this.lblScaleBarPos.Name = "lblScaleBarPos";
            this.lblScaleBarPos.Size = new System.Drawing.Size(56, 15);
            this.lblScaleBarPos.TabIndex = 1;
            this.lblScaleBarPos.Text = "配置位置:";
            // 
            // cmbScaleBarPos
            // 
            this.cmbScaleBarPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScaleBarPos.FormattingEnabled = true;
            this.cmbScaleBarPos.Items.AddRange(new object[] {
            "右下",
            "左下",
            "右上",
            "左上"});
            this.cmbScaleBarPos.Location = new System.Drawing.Point(72, 46);
            this.cmbScaleBarPos.Name = "cmbScaleBarPos";
            this.cmbScaleBarPos.Size = new System.Drawing.Size(80, 21);
            this.cmbScaleBarPos.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(65)))), ((int)(((byte)(80)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(180, 355);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(102, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // FormDrawingFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(294, 388);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.chkVisible);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormDrawingFrame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "全図・図枠設定";
            this.TopMost = true;
            this.tabSettings.ResumeLayout(false);
            this.tabBasic.ResumeLayout(false);
            this.grpPaper.ResumeLayout(false);
            this.grpPaper.PerformLayout();
            this.grpScale.ResumeLayout(false);
            this.grpScale.PerformLayout();
            this.grpTransform.ResumeLayout(false);
            this.grpTransform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRotation)).EndInit();
            this.tabTombo.ResumeLayout(false);
            this.grpTombo.ResumeLayout(false);
            this.grpTombo.PerformLayout();
            this.grpPitch.ResumeLayout(false);
            this.grpPitch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPitchMeters)).EndInit();
            this.tabExtras.ResumeLayout(false);
            this.grpMargin.ResumeLayout(false);
            this.grpMargin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginB)).EndInit();
            this.grpNorthArrow.ResumeLayout(false);
            this.grpNorthArrow.PerformLayout();
            this.grpScaleBar.ResumeLayout(false);
            this.grpScaleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabBasic;
        private System.Windows.Forms.TabPage tabTombo;
        private System.Windows.Forms.TabPage tabExtras;

        // 基本・配置
        private System.Windows.Forms.GroupBox grpPaper;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.RadioButton rdoLandscape;
        private System.Windows.Forms.RadioButton rdoPortrait;
        private System.Windows.Forms.GroupBox grpScale;
        private System.Windows.Forms.Label lblScalePrefix;
        private System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.GroupBox grpTransform;
        private System.Windows.Forms.Label lblCenterX;
        private System.Windows.Forms.NumericUpDown numCenterX;
        private System.Windows.Forms.Label lblCenterY;
        private System.Windows.Forms.NumericUpDown numCenterY;
        private System.Windows.Forms.Label lblRotation;
        private System.Windows.Forms.NumericUpDown numRotation;
        private System.Windows.Forms.Button btnResetRotation;
        public System.Windows.Forms.Button btnMoveCenter;
        public System.Windows.Forms.Button btnSetRotation;
        private System.Windows.Forms.Button btnFitAll;

        // トンボ・座標
        private System.Windows.Forms.GroupBox grpTombo;
        private System.Windows.Forms.CheckBox chkShowTombo;
        private System.Windows.Forms.CheckBox chkShowGridLines;
        private System.Windows.Forms.CheckBox chkShowBorderCoords;
        private System.Windows.Forms.GroupBox grpPitch;
        private System.Windows.Forms.RadioButton rdoPitchAuto;
        private System.Windows.Forms.RadioButton rdoPitchManual;
        private System.Windows.Forms.NumericUpDown numPitchMeters;
        private System.Windows.Forms.Label lblPitchUnit;
        private System.Windows.Forms.Label lblEffectivePitch;

        // 余白・付加
        private System.Windows.Forms.GroupBox grpMargin;
        private System.Windows.Forms.Label lblMarginL;
        private System.Windows.Forms.NumericUpDown numMarginL;
        private System.Windows.Forms.Label lblMarginR;
        private System.Windows.Forms.NumericUpDown numMarginR;
        private System.Windows.Forms.Label lblMarginT;
        private System.Windows.Forms.NumericUpDown numMarginT;
        private System.Windows.Forms.Label lblMarginB;
        private System.Windows.Forms.NumericUpDown numMarginB;
        private System.Windows.Forms.GroupBox grpNorthArrow;
        private System.Windows.Forms.CheckBox chkShowNorthArrow;
        private System.Windows.Forms.Label lblNorthPos;
        private System.Windows.Forms.ComboBox cmbNorthPos;
        private System.Windows.Forms.GroupBox grpScaleBar;
        private System.Windows.Forms.CheckBox chkShowScaleBar;
        private System.Windows.Forms.Label lblScaleBarPos;
        private System.Windows.Forms.ComboBox cmbScaleBarPos;

        private System.Windows.Forms.Button btnClose;
    }
}
