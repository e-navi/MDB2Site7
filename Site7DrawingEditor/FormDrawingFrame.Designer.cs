namespace Site7DrawingEditor
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
            this.grpPresets = new System.Windows.Forms.GroupBox();
            this.cmbPresets = new System.Windows.Forms.ComboBox();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.btnLoadPreset = new System.Windows.Forms.Button();
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
            this.lblNorthType = new System.Windows.Forms.Label();
            this.cmbNorthType = new System.Windows.Forms.ComboBox();
            this.lblNorthSize = new System.Windows.Forms.Label();
            this.numNorthSize = new System.Windows.Forms.NumericUpDown();
            this.lblNorthPos = new System.Windows.Forms.Label();
            this.cmbNorthPos = new System.Windows.Forms.ComboBox();
            this.btnPickNorthPos = new System.Windows.Forms.Button();
            this.grpScaleBar = new System.Windows.Forms.GroupBox();
            this.chkShowScaleBar = new System.Windows.Forms.CheckBox();
            this.lblScaleBarType = new System.Windows.Forms.Label();
            this.cmbScaleBarType = new System.Windows.Forms.ComboBox();
            this.lblScaleBarPos = new System.Windows.Forms.Label();
            this.cmbScaleBarPos = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpPresets.SuspendLayout();
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
            // chkPreviewDrawing
            // 
            this.chkPreviewDrawing.AutoSize = true;
            this.chkPreviewDrawing.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.chkPreviewDrawing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.chkPreviewDrawing.Location = new System.Drawing.Point(8, 6);
            this.chkPreviewDrawing.Name = "chkPreviewDrawing";
            this.chkPreviewDrawing.Size = new System.Drawing.Size(125, 25);
            this.chkPreviewDrawing.TabIndex = 0;
            this.chkPreviewDrawing.Text = "図面表示確認";
            this.chkPreviewDrawing.UseVisualStyleBackColor = true;
            // 
            // grpPresets
            // 
            this.grpPresets.BackColor = System.Drawing.Color.White;
            this.grpPresets.Controls.Add(this.btnLoadPreset);
            this.grpPresets.Controls.Add(this.btnSavePreset);
            this.grpPresets.Controls.Add(this.cmbPresets);
            this.grpPresets.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpPresets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpPresets.Location = new System.Drawing.Point(6, 32);
            this.grpPresets.Name = "grpPresets";
            this.grpPresets.Size = new System.Drawing.Size(244, 60);
            this.grpPresets.TabIndex = 1;
            this.grpPresets.TabStop = false;
            this.grpPresets.Text = "📁 プリセット";
            // 
            // cmbPresets
            // 
            this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresets.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbPresets.FormattingEnabled = true;
            this.cmbPresets.Location = new System.Drawing.Point(6, 24);
            this.cmbPresets.Name = "cmbPresets";
            this.cmbPresets.Size = new System.Drawing.Size(158, 29);
            this.cmbPresets.TabIndex = 0;
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnSavePreset.FlatAppearance.BorderSize = 0;
            this.btnSavePreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePreset.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.btnSavePreset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnSavePreset.Location = new System.Drawing.Point(168, 23);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(32, 31);
            this.btnSavePreset.TabIndex = 1;
            this.btnSavePreset.Text = "💾";
            this.btnSavePreset.UseVisualStyleBackColor = false;
            // 
            // btnLoadPreset
            // 
            this.btnLoadPreset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.btnLoadPreset.FlatAppearance.BorderSize = 0;
            this.btnLoadPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadPreset.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.btnLoadPreset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnLoadPreset.Location = new System.Drawing.Point(204, 23);
            this.btnLoadPreset.Name = "btnLoadPreset";
            this.btnLoadPreset.Size = new System.Drawing.Size(32, 31);
            this.btnLoadPreset.TabIndex = 2;
            this.btnLoadPreset.Text = "📂";
            this.btnLoadPreset.UseVisualStyleBackColor = false;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabBasic);
            this.tabSettings.Controls.Add(this.tabTombo);
            this.tabSettings.Controls.Add(this.tabExtras);
            this.tabSettings.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.tabSettings.Location = new System.Drawing.Point(6, 96);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(244, 370);
            this.tabSettings.TabIndex = 2;
            // 
            // tabBasic
            // 
            this.tabBasic.BackColor = System.Drawing.Color.White;
            this.tabBasic.Controls.Add(this.grpPaper);
            this.tabBasic.Controls.Add(this.grpScale);
            this.tabBasic.Controls.Add(this.grpTransform);
            this.tabBasic.Location = new System.Drawing.Point(4, 30);
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.Padding = new System.Windows.Forms.Padding(4);
            this.tabBasic.Size = new System.Drawing.Size(236, 336);
            this.tabBasic.TabIndex = 0;
            this.tabBasic.Text = "基本・配置";
            // 
            // grpPaper
            // 
            this.grpPaper.Controls.Add(this.rdoPortrait);
            this.grpPaper.Controls.Add(this.rdoLandscape);
            this.grpPaper.Controls.Add(this.cmbPaperSize);
            this.grpPaper.Controls.Add(this.lblPaperSize);
            this.grpPaper.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpPaper.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpPaper.Location = new System.Drawing.Point(4, 4);
            this.grpPaper.Name = "grpPaper";
            this.grpPaper.Size = new System.Drawing.Size(228, 56);
            this.grpPaper.TabIndex = 0;
            this.grpPaper.TabStop = false;
            this.grpPaper.Text = "用紙設定";
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblPaperSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPaperSize.Location = new System.Drawing.Point(2, 22);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(44, 24);
            this.lblPaperSize.TabIndex = 0;
            this.lblPaperSize.Text = "用紙:";
            this.lblPaperSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperSize.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Items.AddRange(new object[] {
            "A4",
            "A3",
            "A2",
            "A1",
            "A0"});
            this.cmbPaperSize.Location = new System.Drawing.Point(48, 20);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(58, 29);
            this.cmbPaperSize.TabIndex = 1;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Checked = true;
            this.rdoLandscape.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.rdoLandscape.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.rdoLandscape.Location = new System.Drawing.Point(112, 22);
            this.rdoLandscape.Name = "rdoLandscape";
            this.rdoLandscape.Size = new System.Drawing.Size(44, 25);
            this.rdoLandscape.TabIndex = 2;
            this.rdoLandscape.TabStop = true;
            this.rdoLandscape.Text = "横";
            this.rdoLandscape.UseVisualStyleBackColor = true;
            // 
            // rdoPortrait
            // 
            this.rdoPortrait.AutoSize = true;
            this.rdoPortrait.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.rdoPortrait.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.rdoPortrait.Location = new System.Drawing.Point(164, 22);
            this.rdoPortrait.Name = "rdoPortrait";
            this.rdoPortrait.Size = new System.Drawing.Size(44, 25);
            this.rdoPortrait.TabIndex = 3;
            this.rdoPortrait.Text = "縦";
            this.rdoPortrait.UseVisualStyleBackColor = true;
            // 
            // grpScale
            // 
            this.grpScale.Controls.Add(this.cmbScale);
            this.grpScale.Controls.Add(this.lblScalePrefix);
            this.grpScale.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpScale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpScale.Location = new System.Drawing.Point(4, 62);
            this.grpScale.Name = "grpScale";
            this.grpScale.Size = new System.Drawing.Size(228, 56);
            this.grpScale.TabIndex = 1;
            this.grpScale.TabStop = false;
            this.grpScale.Text = "図面縮尺";
            // 
            // lblScalePrefix
            // 
            this.lblScalePrefix.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblScalePrefix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblScalePrefix.Location = new System.Drawing.Point(2, 22);
            this.lblScalePrefix.Name = "lblScalePrefix";
            this.lblScalePrefix.Size = new System.Drawing.Size(74, 24);
            this.lblScalePrefix.TabIndex = 0;
            this.lblScalePrefix.Text = "縮尺: 1 /";
            this.lblScalePrefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbScale
            // 
            this.cmbScale.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
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
            this.cmbScale.Location = new System.Drawing.Point(80, 20);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(120, 29);
            this.cmbScale.TabIndex = 1;
            // 
            // grpTransform
            // 
            this.grpTransform.Controls.Add(this.btnFitAll);
            this.grpTransform.Controls.Add(this.btnSetRotation);
            this.grpTransform.Controls.Add(this.btnMoveCenter);
            this.grpTransform.Controls.Add(this.btnResetRotation);
            this.grpTransform.Controls.Add(this.numRotation);
            this.grpTransform.Controls.Add(this.lblRotation);
            this.grpTransform.Controls.Add(this.numCenterY);
            this.grpTransform.Controls.Add(this.lblCenterY);
            this.grpTransform.Controls.Add(this.numCenterX);
            this.grpTransform.Controls.Add(this.lblCenterX);
            this.grpTransform.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpTransform.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpTransform.Location = new System.Drawing.Point(4, 120);
            this.grpTransform.Name = "grpTransform";
            this.grpTransform.Size = new System.Drawing.Size(228, 208);
            this.grpTransform.TabIndex = 2;
            this.grpTransform.TabStop = false;
            this.grpTransform.Text = "中心座標 & 回転";
            // 
            // lblCenterX
            // 
            this.lblCenterX.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblCenterX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCenterX.Location = new System.Drawing.Point(2, 20);
            this.lblCenterX.Name = "lblCenterX";
            this.lblCenterX.Size = new System.Drawing.Size(64, 24);
            this.lblCenterX.TabIndex = 0;
            this.lblCenterX.Text = "北 X(m):";
            this.lblCenterX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numCenterX
            // 
            this.numCenterX.DecimalPlaces = 2;
            this.numCenterX.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.numCenterX.Location = new System.Drawing.Point(68, 18);
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
            this.numCenterX.Size = new System.Drawing.Size(152, 29);
            this.numCenterX.TabIndex = 1;
            this.numCenterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCenterY
            // 
            this.lblCenterY.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblCenterY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCenterY.Location = new System.Drawing.Point(2, 50);
            this.lblCenterY.Name = "lblCenterY";
            this.lblCenterY.Size = new System.Drawing.Size(64, 24);
            this.lblCenterY.TabIndex = 2;
            this.lblCenterY.Text = "東 Y(m):";
            this.lblCenterY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numCenterY
            // 
            this.numCenterY.DecimalPlaces = 2;
            this.numCenterY.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.numCenterY.Location = new System.Drawing.Point(68, 48);
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
            this.numCenterY.Size = new System.Drawing.Size(152, 29);
            this.numCenterY.TabIndex = 3;
            this.numCenterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRotation
            // 
            this.lblRotation.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblRotation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblRotation.Location = new System.Drawing.Point(2, 80);
            this.lblRotation.Name = "lblRotation";
            this.lblRotation.Size = new System.Drawing.Size(60, 24);
            this.lblRotation.TabIndex = 4;
            this.lblRotation.Text = "回転(°):";
            this.lblRotation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numRotation
            // 
            this.numRotation.DecimalPlaces = 1;
            this.numRotation.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.numRotation.Location = new System.Drawing.Point(64, 78);
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
            this.numRotation.Size = new System.Drawing.Size(64, 29);
            this.numRotation.TabIndex = 5;
            this.numRotation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnResetRotation
            // 
            this.btnResetRotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.btnResetRotation.FlatAppearance.BorderSize = 0;
            this.btnResetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetRotation.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.btnResetRotation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnResetRotation.Location = new System.Drawing.Point(132, 77);
            this.btnResetRotation.Name = "btnResetRotation";
            this.btnResetRotation.Size = new System.Drawing.Size(88, 30);
            this.btnResetRotation.TabIndex = 6;
            this.btnResetRotation.Text = "0°リセット";
            this.btnResetRotation.UseVisualStyleBackColor = false;
            // 
            // btnMoveCenter
            // 
            this.btnMoveCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnMoveCenter.FlatAppearance.BorderSize = 0;
            this.btnMoveCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveCenter.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMoveCenter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnMoveCenter.Location = new System.Drawing.Point(6, 112);
            this.btnMoveCenter.Name = "btnMoveCenter";
            this.btnMoveCenter.Size = new System.Drawing.Size(104, 32);
            this.btnMoveCenter.TabIndex = 7;
            this.btnMoveCenter.Text = "📍 中心移動";
            this.btnMoveCenter.UseVisualStyleBackColor = false;
            // 
            // btnSetRotation
            // 
            this.btnSetRotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.btnSetRotation.FlatAppearance.BorderSize = 0;
            this.btnSetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetRotation.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSetRotation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.btnSetRotation.Location = new System.Drawing.Point(116, 112);
            this.btnSetRotation.Name = "btnSetRotation";
            this.btnSetRotation.Size = new System.Drawing.Size(104, 32);
            this.btnSetRotation.TabIndex = 8;
            this.btnSetRotation.Text = "🔄 回転指定";
            this.btnSetRotation.UseVisualStyleBackColor = false;
            // 
            // btnFitAll
            // 
            this.btnFitAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.btnFitAll.FlatAppearance.BorderSize = 0;
            this.btnFitAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFitAll.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnFitAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnFitAll.Location = new System.Drawing.Point(6, 148);
            this.btnFitAll.Name = "btnFitAll";
            this.btnFitAll.Size = new System.Drawing.Size(214, 34);
            this.btnFitAll.TabIndex = 9;
            this.btnFitAll.Text = "🔍 現場全体フィット";
            this.btnFitAll.UseVisualStyleBackColor = false;
            // 
            // tabTombo
            // 
            this.tabTombo.BackColor = System.Drawing.Color.White;
            this.tabTombo.Controls.Add(this.grpPitch);
            this.tabTombo.Controls.Add(this.grpTombo);
            this.tabTombo.Location = new System.Drawing.Point(4, 30);
            this.tabTombo.Name = "tabTombo";
            this.tabTombo.Padding = new System.Windows.Forms.Padding(4);
            this.tabTombo.Size = new System.Drawing.Size(236, 336);
            this.tabTombo.TabIndex = 1;
            this.tabTombo.Text = "トンボ・座標";
            // 
            // grpTombo
            // 
            this.grpTombo.Controls.Add(this.chkShowBorderCoords);
            this.grpTombo.Controls.Add(this.chkShowGridLines);
            this.grpTombo.Controls.Add(this.chkShowTombo);
            this.grpTombo.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpTombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpTombo.Location = new System.Drawing.Point(4, 4);
            this.grpTombo.Name = "grpTombo";
            this.grpTombo.Size = new System.Drawing.Size(228, 112);
            this.grpTombo.TabIndex = 0;
            this.grpTombo.TabStop = false;
            this.grpTombo.Text = "トンボ / 格子 / 座標表示";
            // 
            // chkShowTombo
            // 
            this.chkShowTombo.AutoSize = true;
            this.chkShowTombo.Checked = true;
            this.chkShowTombo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTombo.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.chkShowTombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.chkShowTombo.Location = new System.Drawing.Point(6, 22);
            this.chkShowTombo.Name = "chkShowTombo";
            this.chkShowTombo.Size = new System.Drawing.Size(161, 25);
            this.chkShowTombo.TabIndex = 0;
            this.chkShowTombo.Text = "トンボ (+) を表示する";
            this.chkShowTombo.UseVisualStyleBackColor = true;
            // 
            // chkShowGridLines
            // 
            this.chkShowGridLines.AutoSize = true;
            this.chkShowGridLines.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.chkShowGridLines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.chkShowGridLines.Location = new System.Drawing.Point(6, 50);
            this.chkShowGridLines.Name = "chkShowGridLines";
            this.chkShowGridLines.Size = new System.Drawing.Size(189, 25);
            this.chkShowGridLines.TabIndex = 1;
            this.chkShowGridLines.Text = "格子線（破線）を表示する";
            this.chkShowGridLines.UseVisualStyleBackColor = true;
            // 
            // chkShowBorderCoords
            // 
            this.chkShowBorderCoords.AutoSize = true;
            this.chkShowBorderCoords.Checked = true;
            this.chkShowBorderCoords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowBorderCoords.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.chkShowBorderCoords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.chkShowBorderCoords.Location = new System.Drawing.Point(6, 78);
            this.chkShowBorderCoords.Name = "chkShowBorderCoords";
            this.chkShowBorderCoords.Size = new System.Drawing.Size(206, 25);
            this.chkShowBorderCoords.TabIndex = 2;
            this.chkShowBorderCoords.Text = "枠間に座標値を表示する";
            this.chkShowBorderCoords.UseVisualStyleBackColor = true;
            // 
            // grpPitch
            // 
            this.grpPitch.Controls.Add(this.lblEffectivePitch);
            this.grpPitch.Controls.Add(this.lblPitchUnit);
            this.grpPitch.Controls.Add(this.numPitchMeters);
            this.grpPitch.Controls.Add(this.rdoPitchManual);
            this.grpPitch.Controls.Add(this.rdoPitchAuto);
            this.grpPitch.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpPitch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpPitch.Location = new System.Drawing.Point(4, 118);
            this.grpPitch.Name = "grpPitch";
            this.grpPitch.Size = new System.Drawing.Size(228, 150);
            this.grpPitch.TabIndex = 1;
            this.grpPitch.TabStop = false;
            this.grpPitch.Text = "ピッチ（間隔）設定";
            // 
            // rdoPitchAuto
            // 
            this.rdoPitchAuto.AutoSize = true;
            this.rdoPitchAuto.Checked = true;
            this.rdoPitchAuto.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.rdoPitchAuto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.rdoPitchAuto.Location = new System.Drawing.Point(6, 22);
            this.rdoPitchAuto.Name = "rdoPitchAuto";
            this.rdoPitchAuto.Size = new System.Drawing.Size(188, 25);
            this.rdoPitchAuto.TabIndex = 0;
            this.rdoPitchAuto.TabStop = true;
            this.rdoPitchAuto.Text = "自動（100mm 基準）";
            this.rdoPitchAuto.UseVisualStyleBackColor = true;
            // 
            // rdoPitchManual
            // 
            this.rdoPitchManual.AutoSize = true;
            this.rdoPitchManual.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.rdoPitchManual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.rdoPitchManual.Location = new System.Drawing.Point(6, 50);
            this.rdoPitchManual.Name = "rdoPitchManual";
            this.rdoPitchManual.Size = new System.Drawing.Size(92, 25);
            this.rdoPitchManual.TabIndex = 1;
            this.rdoPitchManual.Text = "手動指定:";
            this.rdoPitchManual.UseVisualStyleBackColor = true;
            // 
            // numPitchMeters
            // 
            this.numPitchMeters.DecimalPlaces = 1;
            this.numPitchMeters.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.numPitchMeters.Location = new System.Drawing.Point(100, 48);
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
            this.numPitchMeters.Size = new System.Drawing.Size(84, 29);
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
            this.lblPitchUnit.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblPitchUnit.Location = new System.Drawing.Point(188, 50);
            this.lblPitchUnit.Name = "lblPitchUnit";
            this.lblPitchUnit.Size = new System.Drawing.Size(28, 24);
            this.lblPitchUnit.TabIndex = 3;
            this.lblPitchUnit.Text = "m";
            this.lblPitchUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEffectivePitch
            // 
            this.lblEffectivePitch.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEffectivePitch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(40)))));
            this.lblEffectivePitch.Location = new System.Drawing.Point(6, 80);
            this.lblEffectivePitch.Name = "lblEffectivePitch";
            this.lblEffectivePitch.Size = new System.Drawing.Size(216, 44);
            this.lblEffectivePitch.TabIndex = 4;
            this.lblEffectivePitch.Text = "現在の実ピッチ: 20.0m";
            // 
            // tabExtras
            // 
            this.tabExtras.BackColor = System.Drawing.Color.White;
            this.tabExtras.Controls.Add(this.grpScaleBar);
            this.tabExtras.Controls.Add(this.grpNorthArrow);
            this.tabExtras.Controls.Add(this.grpMargin);
            this.tabExtras.Location = new System.Drawing.Point(4, 30);
            this.tabExtras.Name = "tabExtras";
            this.tabExtras.Padding = new System.Windows.Forms.Padding(4);
            this.tabExtras.Size = new System.Drawing.Size(236, 336);
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
            this.grpMargin.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpMargin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpMargin.Location = new System.Drawing.Point(4, 4);
            this.grpMargin.Name = "grpMargin";
            this.grpMargin.Size = new System.Drawing.Size(228, 108);
            this.grpMargin.TabIndex = 0;
            this.grpMargin.TabStop = false;
            this.grpMargin.Text = "枠余白・間隔 (mm)";
            // 
            // lblMarginLeft
            // 
            this.lblMarginLeft.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblMarginLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblMarginLeft.Location = new System.Drawing.Point(2, 18);
            this.lblMarginLeft.Name = "lblMarginLeft";
            this.lblMarginLeft.Size = new System.Drawing.Size(124, 24);
            this.lblMarginLeft.TabIndex = 0;
            this.lblMarginLeft.Text = "外枠余白 [左]:";
            this.lblMarginLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numMarginLeft
            // 
            this.numMarginLeft.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.numMarginLeft.Location = new System.Drawing.Point(130, 16);
            this.numMarginLeft.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginLeft.Name = "numMarginLeft";
            this.numMarginLeft.Size = new System.Drawing.Size(88, 29);
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
            this.lblMarginOther.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblMarginOther.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblMarginOther.Location = new System.Drawing.Point(2, 46);
            this.lblMarginOther.Name = "lblMarginOther";
            this.lblMarginOther.Size = new System.Drawing.Size(124, 24);
            this.lblMarginOther.TabIndex = 2;
            this.lblMarginOther.Text = "外枠余白 [他]:";
            this.lblMarginOther.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numMarginOther
            // 
            this.numMarginOther.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.numMarginOther.Location = new System.Drawing.Point(130, 44);
            this.numMarginOther.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginOther.Name = "numMarginOther";
            this.numMarginOther.Size = new System.Drawing.Size(88, 29);
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
            this.lblSpacing.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblSpacing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblSpacing.Location = new System.Drawing.Point(2, 74);
            this.lblSpacing.Name = "lblSpacing";
            this.lblSpacing.Size = new System.Drawing.Size(124, 24);
            this.lblSpacing.TabIndex = 4;
            this.lblSpacing.Text = "外・内枠間隔:";
            this.lblSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numSpacing
            // 
            this.numSpacing.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.numSpacing.Location = new System.Drawing.Point(130, 72);
            this.numSpacing.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numSpacing.Name = "numSpacing";
            this.numSpacing.Size = new System.Drawing.Size(88, 29);
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
            this.grpNorthArrow.Controls.Add(this.numNorthSize);
            this.grpNorthArrow.Controls.Add(this.lblNorthSize);
            this.grpNorthArrow.Controls.Add(this.cmbNorthType);
            this.grpNorthArrow.Controls.Add(this.lblNorthType);
            this.grpNorthArrow.Controls.Add(this.chkShowNorthArrow);
            this.grpNorthArrow.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpNorthArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpNorthArrow.Location = new System.Drawing.Point(4, 114);
            this.grpNorthArrow.Name = "grpNorthArrow";
            this.grpNorthArrow.Size = new System.Drawing.Size(228, 120);
            this.grpNorthArrow.TabIndex = 1;
            this.grpNorthArrow.TabStop = false;
            this.grpNorthArrow.Text = "方位記号（北矢印）";
            // 
            // chkShowNorthArrow
            // 
            this.chkShowNorthArrow.AutoSize = true;
            this.chkShowNorthArrow.Checked = true;
            this.chkShowNorthArrow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowNorthArrow.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.chkShowNorthArrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.chkShowNorthArrow.Location = new System.Drawing.Point(6, 20);
            this.chkShowNorthArrow.Name = "chkShowNorthArrow";
            this.chkShowNorthArrow.Size = new System.Drawing.Size(126, 25);
            this.chkShowNorthArrow.TabIndex = 0;
            this.chkShowNorthArrow.Text = "方位記号を表示";
            this.chkShowNorthArrow.UseVisualStyleBackColor = true;
            // 
            // lblNorthType
            // 
            this.lblNorthType.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblNorthType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblNorthType.Location = new System.Drawing.Point(2, 48);
            this.lblNorthType.Name = "lblNorthType";
            this.lblNorthType.Size = new System.Drawing.Size(44, 24);
            this.lblNorthType.TabIndex = 7;
            this.lblNorthType.Text = "種類:";
            this.lblNorthType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbNorthType
            // 
            this.cmbNorthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNorthType.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbNorthType.FormattingEnabled = true;
            this.cmbNorthType.Items.AddRange(new object[] {
            "標準矢印",
            "シンプル",
            "円形コンパス",
            "モダン"});
            this.cmbNorthType.Location = new System.Drawing.Point(48, 46);
            this.cmbNorthType.Name = "cmbNorthType";
            this.cmbNorthType.Size = new System.Drawing.Size(84, 29);
            this.cmbNorthType.TabIndex = 8;
            // 
            // lblNorthSize
            // 
            this.lblNorthSize.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblNorthSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblNorthSize.Location = new System.Drawing.Point(134, 48);
            this.lblNorthSize.Name = "lblNorthSize";
            this.lblNorthSize.Size = new System.Drawing.Size(32, 24);
            this.lblNorthSize.TabIndex = 1;
            this.lblNorthSize.Text = "寸:";
            this.lblNorthSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numNorthSize
            // 
            this.numNorthSize.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.numNorthSize.Location = new System.Drawing.Point(168, 46);
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
            this.numNorthSize.Size = new System.Drawing.Size(50, 29);
            this.numNorthSize.TabIndex = 2;
            this.numNorthSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNorthSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lblNorthPos
            // 
            this.lblNorthPos.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblNorthPos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblNorthPos.Location = new System.Drawing.Point(2, 80);
            this.lblNorthPos.Name = "lblNorthPos";
            this.lblNorthPos.Size = new System.Drawing.Size(44, 24);
            this.lblNorthPos.TabIndex = 4;
            this.lblNorthPos.Text = "位置:";
            this.lblNorthPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbNorthPos
            // 
            this.cmbNorthPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNorthPos.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbNorthPos.FormattingEnabled = true;
            this.cmbNorthPos.Items.AddRange(new object[] {
            "右上",
            "左上",
            "右下",
            "左下",
            "カスタム"});
            this.cmbNorthPos.Location = new System.Drawing.Point(48, 78);
            this.cmbNorthPos.Name = "cmbNorthPos";
            this.cmbNorthPos.Size = new System.Drawing.Size(76, 29);
            this.cmbNorthPos.TabIndex = 5;
            // 
            // btnPickNorthPos
            // 
            this.btnPickNorthPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnPickNorthPos.FlatAppearance.BorderSize = 0;
            this.btnPickNorthPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPickNorthPos.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPickNorthPos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnPickNorthPos.Location = new System.Drawing.Point(128, 77);
            this.btnPickNorthPos.Name = "btnPickNorthPos";
            this.btnPickNorthPos.Size = new System.Drawing.Size(90, 31);
            this.btnPickNorthPos.TabIndex = 6;
            this.btnPickNorthPos.Text = "📍 指示";
            this.btnPickNorthPos.UseVisualStyleBackColor = false;
            // 
            // grpScaleBar
            // 
            this.grpScaleBar.Controls.Add(this.cmbScaleBarPos);
            this.grpScaleBar.Controls.Add(this.lblScaleBarPos);
            this.grpScaleBar.Controls.Add(this.cmbScaleBarType);
            this.grpScaleBar.Controls.Add(this.lblScaleBarType);
            this.grpScaleBar.Controls.Add(this.chkShowScaleBar);
            this.grpScaleBar.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpScaleBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpScaleBar.Location = new System.Drawing.Point(4, 236);
            this.grpScaleBar.Name = "grpScaleBar";
            this.grpScaleBar.Size = new System.Drawing.Size(228, 92);
            this.grpScaleBar.TabIndex = 2;
            this.grpScaleBar.TabStop = false;
            this.grpScaleBar.Text = "縮尺スケールバー";
            // 
            // chkShowScaleBar
            // 
            this.chkShowScaleBar.AutoSize = true;
            this.chkShowScaleBar.Checked = true;
            this.chkShowScaleBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowScaleBar.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.chkShowScaleBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.chkShowScaleBar.Location = new System.Drawing.Point(6, 20);
            this.chkShowScaleBar.Name = "chkShowScaleBar";
            this.chkShowScaleBar.Size = new System.Drawing.Size(148, 25);
            this.chkShowScaleBar.TabIndex = 0;
            this.chkShowScaleBar.Text = "スケールバーを表示";
            this.chkShowScaleBar.UseVisualStyleBackColor = true;
            // 
            // lblScaleBarType
            // 
            this.lblScaleBarType.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblScaleBarType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblScaleBarType.Location = new System.Drawing.Point(2, 46);
            this.lblScaleBarType.Name = "lblScaleBarType";
            this.lblScaleBarType.Size = new System.Drawing.Size(44, 24);
            this.lblScaleBarType.TabIndex = 3;
            this.lblScaleBarType.Text = "種類:";
            this.lblScaleBarType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbScaleBarType
            // 
            this.cmbScaleBarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScaleBarType.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.cmbScaleBarType.FormattingEnabled = true;
            this.cmbScaleBarType.Items.AddRange(new object[] {
            "精密線 (下縮尺)",
            "ブロック",
            "シンプル線",
            "二重枠",
            "目盛付き"});
            this.cmbScaleBarType.Location = new System.Drawing.Point(48, 44);
            this.cmbScaleBarType.Name = "cmbScaleBarType";
            this.cmbScaleBarType.Size = new System.Drawing.Size(90, 28);
            this.cmbScaleBarType.TabIndex = 4;
            // 
            // lblScaleBarPos
            // 
            this.lblScaleBarPos.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblScaleBarPos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblScaleBarPos.Location = new System.Drawing.Point(140, 46);
            this.lblScaleBarPos.Name = "lblScaleBarPos";
            this.lblScaleBarPos.Size = new System.Drawing.Size(34, 24);
            this.lblScaleBarPos.TabIndex = 1;
            this.lblScaleBarPos.Text = "位:";
            this.lblScaleBarPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbScaleBarPos
            // 
            this.cmbScaleBarPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScaleBarPos.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbScaleBarPos.FormattingEnabled = true;
            this.cmbScaleBarPos.Items.AddRange(new object[] {
            "中下",
            "右下"});
            this.cmbScaleBarPos.Location = new System.Drawing.Point(174, 44);
            this.cmbScaleBarPos.Name = "cmbScaleBarPos";
            this.cmbScaleBarPos.Size = new System.Drawing.Size(44, 29);
            this.cmbScaleBarPos.TabIndex = 2;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(114)))), ((int)(((byte)(186)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(6, 472);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 36);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "🖨 印刷...";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnClose.Location = new System.Drawing.Point(150, 472);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 36);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // FormDrawingFrame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(256, 514);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.grpPresets);
            this.Controls.Add(this.chkPreviewDrawing);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormDrawingFrame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "図枠設定・印刷";
            this.TopMost = true;
            this.grpPresets.ResumeLayout(false);
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

        public System.Windows.Forms.Button btnPrint;
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
        private System.Windows.Forms.Label lblNorthType;
        private System.Windows.Forms.ComboBox cmbNorthType;
        private System.Windows.Forms.Label lblNorthSize;
        private System.Windows.Forms.NumericUpDown numNorthSize;
        private System.Windows.Forms.Label lblNorthPos;
        private System.Windows.Forms.ComboBox cmbNorthPos;
        public System.Windows.Forms.Button btnPickNorthPos;

        private System.Windows.Forms.GroupBox grpScaleBar;
        private System.Windows.Forms.CheckBox chkShowScaleBar;
        private System.Windows.Forms.Label lblScaleBarType;
        private System.Windows.Forms.ComboBox cmbScaleBarType;
        private System.Windows.Forms.Label lblScaleBarPos;
        private System.Windows.Forms.ComboBox cmbScaleBarPos;

        private System.Windows.Forms.GroupBox grpPresets;
        private System.Windows.Forms.ComboBox cmbPresets;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Button btnLoadPreset;
        private System.Windows.Forms.Button btnClose;
    }
}
