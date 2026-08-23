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
            this.chkPreviewDrawing = new System.Windows.Forms.CheckBox();
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
            this.lblMarginLeft = new System.Windows.Forms.Label();
            this.numMarginLeft = new System.Windows.Forms.NumericUpDown();
            this.lblMarginOther = new System.Windows.Forms.Label();
            this.numMarginOther = new System.Windows.Forms.NumericUpDown();
            this.lblSpacing = new System.Windows.Forms.Label();
            this.numSpacing = new System.Windows.Forms.NumericUpDown();
            this.grpNorthArrow = new System.Windows.Forms.GroupBox();
            this.chkShowNorthArrow = new System.Windows.Forms.CheckBox();
            this.lblNorthSize = new System.Windows.Forms.Label();
            this.numNorthSize = new System.Windows.Forms.NumericUpDown();
            this.lblNorthSizeUnit = new System.Windows.Forms.Label();
            this.lblNorthPos = new System.Windows.Forms.Label();
            this.cmbNorthPos = new System.Windows.Forms.ComboBox();
            this.btnPickNorthPos = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.numMarginLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginOther)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpacing)).BeginInit();
            this.grpNorthArrow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNorthSize)).BeginInit();
            this.grpScaleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // 
            // chkPreviewDrawing
            // 
            this.chkPreviewDrawing.AutoSize = true;
            this.chkPreviewDrawing.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkPreviewDrawing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.chkPreviewDrawing.Location = new System.Drawing.Point(8, 6);
            this.chkPreviewDrawing.Name = "chkPreviewDrawing";
            this.chkPreviewDrawing.Size = new System.Drawing.Size(98, 19);
            this.chkPreviewDrawing.TabIndex = 0;
            this.chkPreviewDrawing.Text = "図面表示確認";
            this.chkPreviewDrawing.UseVisualStyleBackColor = true;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabBasic);
            this.tabSettings.Controls.Add(this.tabTombo);
            this.tabSettings.Controls.Add(this.tabExtras);
            this.tabSettings.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tabSettings.Location = new System.Drawing.Point(6, 28);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(236, 318);
            this.tabSettings.TabIndex = 2;
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
            this.tabBasic.Size = new System.Drawing.Size(228, 292);
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
            this.grpPaper.Location = new System.Drawing.Point(4, 4);
            this.grpPaper.Name = "grpPaper";
            this.grpPaper.Size = new System.Drawing.Size(218, 48);
            this.grpPaper.TabIndex = 0;
            this.grpPaper.TabStop = false;
            this.grpPaper.Text = "用紙設定";
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(4, 20);
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
            this.cmbPaperSize.Location = new System.Drawing.Point(40, 17);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(56, 21);
            this.cmbPaperSize.TabIndex = 1;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Checked = true;
            this.rdoLandscape.Location = new System.Drawing.Point(104, 19);
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
            this.rdoPortrait.Location = new System.Drawing.Point(148, 19);
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
            this.grpScale.Location = new System.Drawing.Point(4, 54);
            this.grpScale.Name = "grpScale";
            this.grpScale.Size = new System.Drawing.Size(218, 46);
            this.grpScale.TabIndex = 1;
            this.grpScale.TabStop = false;
            this.grpScale.Text = "図面縮尺";
            // 
            // lblScalePrefix
            // 
            this.lblScalePrefix.AutoSize = true;
            this.lblScalePrefix.Location = new System.Drawing.Point(6, 20);
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
            this.cmbScale.Location = new System.Drawing.Point(66, 17);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(85, 21);
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
            this.grpTransform.Location = new System.Drawing.Point(4, 102);
            this.grpTransform.Name = "grpTransform";
            this.grpTransform.Size = new System.Drawing.Size(218, 184);
            this.grpTransform.TabIndex = 2;
            this.grpTransform.TabStop = false;
            this.grpTransform.Text = "中心座標 & 回転";
            // 
            // lblCenterX
            // 
            this.lblCenterX.AutoSize = true;
            this.lblCenterX.Location = new System.Drawing.Point(4, 20);
            this.lblCenterX.Name = "lblCenterX";
            this.lblCenterX.Size = new System.Drawing.Size(43, 15);
            this.lblCenterX.TabIndex = 0;
            this.lblCenterX.Text = "北 X(m):";
            // 
            // numCenterX
            // 
            this.numCenterX.DecimalPlaces = 2;
            this.numCenterX.Location = new System.Drawing.Point(54, 17);
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
            this.numCenterX.Size = new System.Drawing.Size(155, 23);
            this.numCenterX.TabIndex = 1;
            this.numCenterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCenterY
            // 
            this.lblCenterY.AutoSize = true;
            this.lblCenterY.Location = new System.Drawing.Point(4, 46);
            this.lblCenterY.Name = "lblCenterY";
            this.lblCenterY.Size = new System.Drawing.Size(42, 15);
            this.lblCenterY.TabIndex = 2;
            this.lblCenterY.Text = "東 Y(m):";
            // 
            // numCenterY
            // 
            this.numCenterY.DecimalPlaces = 2;
            this.numCenterY.Location = new System.Drawing.Point(54, 43);
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
            this.numCenterY.Size = new System.Drawing.Size(155, 23);
            this.numCenterY.TabIndex = 3;
            this.numCenterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRotation
            // 
            this.lblRotation.AutoSize = true;
            this.lblRotation.Location = new System.Drawing.Point(4, 72);
            this.lblRotation.Name = "lblRotation";
            this.lblRotation.Size = new System.Drawing.Size(46, 15);
            this.lblRotation.TabIndex = 4;
            this.lblRotation.Text = "回転(°):";
            // 
            // numRotation
            // 
            this.numRotation.DecimalPlaces = 1;
            this.numRotation.Location = new System.Drawing.Point(54, 69);
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
            this.numRotation.Size = new System.Drawing.Size(85, 23);
            this.numRotation.TabIndex = 5;
            this.numRotation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnResetRotation
            // 
            this.btnResetRotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(65)))), ((int)(((byte)(80)))));
            this.btnResetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetRotation.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnResetRotation.Location = new System.Drawing.Point(145, 69);
            this.btnResetRotation.Name = "btnResetRotation";
            this.btnResetRotation.Size = new System.Drawing.Size(64, 23);
            this.btnResetRotation.TabIndex = 6;
            this.btnResetRotation.Text = "0°リセット";
            this.btnResetRotation.UseVisualStyleBackColor = false;
            // 
            // btnMoveCenter
            // 
            this.btnMoveCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnMoveCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveCenter.Location = new System.Drawing.Point(6, 102);
            this.btnMoveCenter.Name = "btnMoveCenter";
            this.btnMoveCenter.Size = new System.Drawing.Size(98, 27);
            this.btnMoveCenter.TabIndex = 7;
            this.btnMoveCenter.Text = "📍 中心移動";
            this.btnMoveCenter.UseVisualStyleBackColor = false;
            // 
            // btnSetRotation
            // 
            this.btnSetRotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.btnSetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetRotation.Location = new System.Drawing.Point(111, 102);
            this.btnSetRotation.Name = "btnSetRotation";
            this.btnSetRotation.Size = new System.Drawing.Size(98, 27);
            this.btnSetRotation.TabIndex = 8;
            this.btnSetRotation.Text = "🔄 回転指定";
            this.btnSetRotation.UseVisualStyleBackColor = false;
            // 
            // btnFitAll
            // 
            this.btnFitAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(140)))), ((int)(((byte)(90)))));
            this.btnFitAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFitAll.Location = new System.Drawing.Point(6, 138);
            this.btnFitAll.Name = "btnFitAll";
            this.btnFitAll.Size = new System.Drawing.Size(203, 27);
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
            this.tabTombo.Size = new System.Drawing.Size(228, 292);
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
            this.grpTombo.Location = new System.Drawing.Point(4, 6);
            this.grpTombo.Name = "grpTombo";
            this.grpTombo.Size = new System.Drawing.Size(218, 115);
            this.grpTombo.TabIndex = 0;
            this.grpTombo.TabStop = false;
            this.grpTombo.Text = "トンボ / 格子 / 座標表示";
            // 
            // chkShowTombo
            // 
            this.chkShowTombo.AutoSize = true;
            this.chkShowTombo.Checked = true;
            this.chkShowTombo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTombo.Location = new System.Drawing.Point(10, 24);
            this.chkShowTombo.Name = "chkShowTombo";
            this.chkShowTombo.Size = new System.Drawing.Size(126, 19);
            this.chkShowTombo.TabIndex = 0;
            this.chkShowTombo.Text = "トンボ (+) を表示する";
            this.chkShowTombo.UseVisualStyleBackColor = true;
            // 
            // chkShowGridLines
            // 
            this.chkShowGridLines.AutoSize = true;
            this.chkShowGridLines.Location = new System.Drawing.Point(10, 52);
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
            this.chkShowBorderCoords.Location = new System.Drawing.Point(10, 80);
            this.chkShowBorderCoords.Name = "chkShowBorderCoords";
            this.chkShowBorderCoords.Size = new System.Drawing.Size(176, 19);
            this.chkShowBorderCoords.TabIndex = 2;
            this.chkShowBorderCoords.Text = "外枠・内枠間に座標値を表示";
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
            this.grpPitch.Location = new System.Drawing.Point(4, 130);
            this.grpPitch.Name = "grpPitch";
            this.grpPitch.Size = new System.Drawing.Size(218, 145);
            this.grpPitch.TabIndex = 1;
            this.grpPitch.TabStop = false;
            this.grpPitch.Text = "ピッチ（間隔）設定";
            // 
            // rdoPitchAuto
            // 
            this.rdoPitchAuto.AutoSize = true;
            this.rdoPitchAuto.Checked = true;
            this.rdoPitchAuto.Location = new System.Drawing.Point(10, 24);
            this.rdoPitchAuto.Name = "rdoPitchAuto";
            this.rdoPitchAuto.Size = new System.Drawing.Size(178, 19);
            this.rdoPitchAuto.TabIndex = 0;
            this.rdoPitchAuto.TabStop = true;
            this.rdoPitchAuto.Text = "自動計算（用紙 100mm 基準）";
            this.rdoPitchAuto.UseVisualStyleBackColor = true;
            // 
            // rdoPitchManual
            // 
            this.rdoPitchManual.AutoSize = true;
            this.rdoPitchManual.Location = new System.Drawing.Point(10, 54);
            this.rdoPitchManual.Name = "rdoPitchManual";
            this.rdoPitchManual.Size = new System.Drawing.Size(73, 19);
            this.rdoPitchManual.TabIndex = 1;
            this.rdoPitchManual.Text = "手動指定:";
            this.rdoPitchManual.UseVisualStyleBackColor = true;
            // 
            // numPitchMeters
            // 
            this.numPitchMeters.DecimalPlaces = 1;
            this.numPitchMeters.Location = new System.Drawing.Point(86, 52);
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
            this.numPitchMeters.Size = new System.Drawing.Size(70, 23);
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
            this.lblPitchUnit.Location = new System.Drawing.Point(160, 56);
            this.lblPitchUnit.Name = "lblPitchUnit";
            this.lblPitchUnit.Size = new System.Drawing.Size(18, 15);
            this.lblPitchUnit.TabIndex = 3;
            this.lblPitchUnit.Text = "m";
            // 
            // lblEffectivePitch
            // 
            this.lblEffectivePitch.AutoSize = true;
            this.lblEffectivePitch.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEffectivePitch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblEffectivePitch.Location = new System.Drawing.Point(10, 95);
            this.lblEffectivePitch.Name = "lblEffectivePitch";
            this.lblEffectivePitch.Size = new System.Drawing.Size(117, 13);
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
            this.tabExtras.Size = new System.Drawing.Size(228, 292);
            this.tabExtras.TabIndex = 2;
            this.tabExtras.Text = "枠余白・付加";
            // 
            // grpMargin
            // 
            this.grpMargin.Controls.Add(this.numSpacing);
            this.grpMargin.Controls.Add(this.lblSpacing);
            this.grpMargin.Controls.Add(this.numMarginOther);
            this.grpMargin.Controls.Add(this.lblMarginOther);
            this.grpMargin.Controls.Add(this.numMarginLeft);
            this.grpMargin.Controls.Add(this.lblMarginLeft);
            this.grpMargin.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMargin.ForeColor = System.Drawing.Color.White;
            this.grpMargin.Location = new System.Drawing.Point(4, 4);
            this.grpMargin.Name = "grpMargin";
            this.grpMargin.Size = new System.Drawing.Size(218, 98);
            this.grpMargin.TabIndex = 0;
            this.grpMargin.TabStop = false;
            this.grpMargin.Text = "枠余白・間隔 (mm)";
            // 
            // lblMarginLeft
            // 
            this.lblMarginLeft.AutoSize = true;
            this.lblMarginLeft.Location = new System.Drawing.Point(6, 20);
            this.lblMarginLeft.Name = "lblMarginLeft";
            this.lblMarginLeft.Size = new System.Drawing.Size(69, 15);
            this.lblMarginLeft.TabIndex = 0;
            this.lblMarginLeft.Text = "外枠余白 [左]:";
            // 
            // numMarginLeft
            // 
            this.numMarginLeft.Location = new System.Drawing.Point(145, 17);
            this.numMarginLeft.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginLeft.Name = "numMarginLeft";
            this.numMarginLeft.Size = new System.Drawing.Size(55, 23);
            this.numMarginLeft.TabIndex = 1;
            this.numMarginLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMarginLeft.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblMarginOther
            // 
            this.lblMarginOther.AutoSize = true;
            this.lblMarginOther.Location = new System.Drawing.Point(6, 44);
            this.lblMarginOther.Name = "lblMarginOther";
            this.lblMarginOther.Size = new System.Drawing.Size(91, 15);
            this.lblMarginOther.TabIndex = 2;
            this.lblMarginOther.Text = "外枠余白 [左以外]:";
            // 
            // numMarginOther
            // 
            this.numMarginOther.Location = new System.Drawing.Point(145, 41);
            this.numMarginOther.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginOther.Name = "numMarginOther";
            this.numMarginOther.Size = new System.Drawing.Size(55, 23);
            this.numMarginOther.TabIndex = 3;
            this.numMarginOther.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMarginOther.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblSpacing
            // 
            this.lblSpacing.AutoSize = true;
            this.lblSpacing.Location = new System.Drawing.Point(6, 68);
            this.lblSpacing.Name = "lblSpacing";
            this.lblSpacing.Size = new System.Drawing.Size(97, 15);
            this.lblSpacing.TabIndex = 4;
            this.lblSpacing.Text = "外枠・内枠の間隔:";
            // 
            // numSpacing
            // 
            this.numSpacing.Location = new System.Drawing.Point(145, 65);
            this.numSpacing.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numSpacing.Name = "numSpacing";
            this.numSpacing.Size = new System.Drawing.Size(55, 23);
            this.numSpacing.TabIndex = 5;
            this.numSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSpacing.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // grpNorthArrow
            // 
            this.grpNorthArrow.Controls.Add(this.btnPickNorthPos);
            this.grpNorthArrow.Controls.Add(this.cmbNorthPos);
            this.grpNorthArrow.Controls.Add(this.lblNorthPos);
            this.grpNorthArrow.Controls.Add(this.lblNorthSizeUnit);
            this.grpNorthArrow.Controls.Add(this.numNorthSize);
            this.grpNorthArrow.Controls.Add(this.lblNorthSize);
            this.grpNorthArrow.Controls.Add(this.chkShowNorthArrow);
            this.grpNorthArrow.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpNorthArrow.ForeColor = System.Drawing.Color.White;
            this.grpNorthArrow.Location = new System.Drawing.Point(4, 106);
            this.grpNorthArrow.Name = "grpNorthArrow";
            this.grpNorthArrow.Size = new System.Drawing.Size(218, 102);
            this.grpNorthArrow.TabIndex = 1;
            this.grpNorthArrow.TabStop = false;
            this.grpNorthArrow.Text = "方位記号（北矢印）";
            // 
            // chkShowNorthArrow
            // 
            this.chkShowNorthArrow.AutoSize = true;
            this.chkShowNorthArrow.Checked = true;
            this.chkShowNorthArrow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowNorthArrow.Location = new System.Drawing.Point(8, 20);
            this.chkShowNorthArrow.Name = "chkShowNorthArrow";
            this.chkShowNorthArrow.Size = new System.Drawing.Size(122, 19);
            this.chkShowNorthArrow.TabIndex = 0;
            this.chkShowNorthArrow.Text = "方位記号を表示";
            this.chkShowNorthArrow.UseVisualStyleBackColor = true;
            // 
            // lblNorthSize
            // 
            this.lblNorthSize.AutoSize = true;
            this.lblNorthSize.Location = new System.Drawing.Point(6, 44);
            this.lblNorthSize.Name = "lblNorthSize";
            this.lblNorthSize.Size = new System.Drawing.Size(34, 15);
            this.lblNorthSize.TabIndex = 1;
            this.lblNorthSize.Text = "大きさ:";
            // 
            // numNorthSize
            // 
            this.numNorthSize.Location = new System.Drawing.Point(48, 41);
            this.numNorthSize.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numNorthSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numNorthSize.Name = "numNorthSize";
            this.numNorthSize.Size = new System.Drawing.Size(48, 23);
            this.numNorthSize.TabIndex = 2;
            this.numNorthSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNorthSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lblNorthSizeUnit
            // 
            this.lblNorthSizeUnit.AutoSize = true;
            this.lblNorthSizeUnit.Location = new System.Drawing.Point(98, 44);
            this.lblNorthSizeUnit.Name = "lblNorthSizeUnit";
            this.lblNorthSizeUnit.Size = new System.Drawing.Size(26, 15);
            this.lblNorthSizeUnit.TabIndex = 3;
            this.lblNorthSizeUnit.Text = "mm";
            // 
            // lblNorthPos
            // 
            this.lblNorthPos.AutoSize = true;
            this.lblNorthPos.Location = new System.Drawing.Point(6, 72);
            this.lblNorthPos.Name = "lblNorthPos";
            this.lblNorthPos.Size = new System.Drawing.Size(34, 15);
            this.lblNorthPos.TabIndex = 4;
            this.lblNorthPos.Text = "位置:";
            // 
            // cmbNorthPos
            // 
            this.cmbNorthPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNorthPos.FormattingEnabled = true;
            this.cmbNorthPos.Items.AddRange(new object[] {
            "右上",
            "左上",
            "右下",
            "左下",
            "カスタム"});
            this.cmbNorthPos.Location = new System.Drawing.Point(48, 69);
            this.cmbNorthPos.Name = "cmbNorthPos";
            this.cmbNorthPos.Size = new System.Drawing.Size(64, 21);
            this.cmbNorthPos.TabIndex = 5;
            // 
            // btnPickNorthPos
            // 
            this.btnPickNorthPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnPickNorthPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPickNorthPos.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPickNorthPos.Location = new System.Drawing.Point(118, 68);
            this.btnPickNorthPos.Name = "btnPickNorthPos";
            this.btnPickNorthPos.Size = new System.Drawing.Size(94, 23);
            this.btnPickNorthPos.TabIndex = 6;
            this.btnPickNorthPos.Text = "📍 マウス指示";
            this.btnPickNorthPos.UseVisualStyleBackColor = false;
            // 
            // grpScaleBar
            // 
            this.grpScaleBar.Controls.Add(this.cmbScaleBarPos);
            this.grpScaleBar.Controls.Add(this.lblScaleBarPos);
            this.grpScaleBar.Controls.Add(this.chkShowScaleBar);
            this.grpScaleBar.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpScaleBar.ForeColor = System.Drawing.Color.White;
            this.grpScaleBar.Location = new System.Drawing.Point(4, 212);
            this.grpScaleBar.Name = "grpScaleBar";
            this.grpScaleBar.Size = new System.Drawing.Size(218, 72);
            this.grpScaleBar.TabIndex = 2;
            this.grpScaleBar.TabStop = false;
            this.grpScaleBar.Text = "縮尺スケールバー";
            // 
            // chkShowScaleBar
            // 
            this.chkShowScaleBar.AutoSize = true;
            this.chkShowScaleBar.Checked = true;
            this.chkShowScaleBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowScaleBar.Location = new System.Drawing.Point(8, 20);
            this.chkShowScaleBar.Name = "chkShowScaleBar";
            this.chkShowScaleBar.Size = new System.Drawing.Size(122, 19);
            this.chkShowScaleBar.TabIndex = 0;
            this.chkShowScaleBar.Text = "スケールバーを表示";
            this.chkShowScaleBar.UseVisualStyleBackColor = true;
            // 
            // lblScaleBarPos
            // 
            this.lblScaleBarPos.AutoSize = true;
            this.lblScaleBarPos.Location = new System.Drawing.Point(6, 46);
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
            "中下",
            "右下"});
            this.cmbScaleBarPos.Location = new System.Drawing.Point(68, 43);
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
            this.btnClose.Location = new System.Drawing.Point(152, 350);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // FormDrawingFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(248, 382);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.chkPreviewDrawing);
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
            ((System.ComponentModel.ISupportInitialize)(this.numMarginLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginOther)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpacing)).EndInit();
            this.grpNorthArrow.ResumeLayout(false);
            this.grpNorthArrow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNorthSize)).EndInit();
            this.grpScaleBar.ResumeLayout(false);
            this.grpScaleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPreviewDrawing;
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

        // 枠余白・付加
        private System.Windows.Forms.GroupBox grpMargin;
        private System.Windows.Forms.Label lblMarginLeft;
        private System.Windows.Forms.NumericUpDown numMarginLeft;
        private System.Windows.Forms.Label lblMarginOther;
        private System.Windows.Forms.NumericUpDown numMarginOther;
        private System.Windows.Forms.Label lblSpacing;
        private System.Windows.Forms.NumericUpDown numSpacing;

        private System.Windows.Forms.GroupBox grpNorthArrow;
        private System.Windows.Forms.CheckBox chkShowNorthArrow;
        private System.Windows.Forms.Label lblNorthSize;
        private System.Windows.Forms.NumericUpDown numNorthSize;
        private System.Windows.Forms.Label lblNorthSizeUnit;
        private System.Windows.Forms.Label lblNorthPos;
        private System.Windows.Forms.ComboBox cmbNorthPos;
        public System.Windows.Forms.Button btnPickNorthPos;

        private System.Windows.Forms.GroupBox grpScaleBar;
        private System.Windows.Forms.CheckBox chkShowScaleBar;
        private System.Windows.Forms.Label lblScaleBarPos;
        private System.Windows.Forms.ComboBox cmbScaleBarPos;

        private System.Windows.Forms.Button btnClose;
    }
}
