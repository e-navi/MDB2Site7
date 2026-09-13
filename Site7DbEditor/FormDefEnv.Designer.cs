namespace Site7DbEditor
{
    partial class FormDefEnv
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
            this.grpPrism = new System.Windows.Forms.GroupBox();
            this.lblPrism = new System.Windows.Forms.Label();
            this.CBSetPrism = new System.Windows.Forms.ComboBox();
            this.lblPrismVal = new System.Windows.Forms.Label();
            this.TBPrismVal = new System.Windows.Forms.TextBox();
            this.lblMm = new System.Windows.Forms.Label();
            this.lblSokkyo = new System.Windows.Forms.Label();
            this.CBSokkyoMode = new System.Windows.Forms.ComboBox();
            this.lblTilt = new System.Windows.Forms.Label();
            this.CBTilt = new System.Windows.Forms.ComboBox();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblH = new System.Windows.Forms.Label();
            this.TBSearchH = new System.Windows.Forms.TextBox();
            this.lblHdeg = new System.Windows.Forms.Label();
            this.lblV = new System.Windows.Forms.Label();
            this.TBSearchV = new System.Windows.Forms.TextBox();
            this.lblVdeg = new System.Windows.Forms.Label();
            this.lblRc = new System.Windows.Forms.Label();
            this.CBUseRC = new System.Windows.Forms.ComboBox();
            this.lblGuide = new System.Windows.Forms.Label();
            this.CBGuidLightPat = new System.Windows.Forms.ComboBox();
            this.CBGuidLightVal = new System.Windows.Forms.ComboBox();
            this.lblLaser = new System.Windows.Forms.Label();
            this.CBLightPat = new System.Windows.Forms.ComboBox();
            this.CBLightVal = new System.Windows.Forms.ComboBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.grpGps = new System.Windows.Forms.GroupBox();
            this.lblGpsH = new System.Windows.Forms.Label();
            this.CBGPSHeight = new System.Windows.Forms.ComboBox();
            this.lblGpsCnt = new System.Windows.Forms.Label();
            this.TBGPSCount = new System.Windows.Forms.TextBox();
            this.lblKai = new System.Windows.Forms.Label();
            this.lblImu = new System.Windows.Forms.Label();
            this.CBi93IMU = new System.Windows.Forms.ComboBox();
            this.btnExportToMaster = new System.Windows.Forms.Button();
            this.btnImportFromMaster = new System.Windows.Forms.Button();
            this.Do_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.grpPrism.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpGps.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPrism
            // 
            this.grpPrism.BackColor = System.Drawing.Color.White;
            this.grpPrism.Controls.Add(this.lblPrism);
            this.grpPrism.Controls.Add(this.CBSetPrism);
            this.grpPrism.Controls.Add(this.lblPrismVal);
            this.grpPrism.Controls.Add(this.TBPrismVal);
            this.grpPrism.Controls.Add(this.lblMm);
            this.grpPrism.Controls.Add(this.lblSokkyo);
            this.grpPrism.Controls.Add(this.CBSokkyoMode);
            this.grpPrism.Controls.Add(this.lblTilt);
            this.grpPrism.Controls.Add(this.CBTilt);
            this.grpPrism.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpPrism.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.grpPrism.Location = new System.Drawing.Point(14, 12);
            this.grpPrism.Name = "grpPrism";
            this.grpPrism.Size = new System.Drawing.Size(452, 150);
            this.grpPrism.TabIndex = 0;
            this.grpPrism.TabStop = false;
            this.grpPrism.Text = " 🎯 プリズム・測距設定 ";
            // 
            // lblPrism
            // 
            this.lblPrism.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblPrism.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblPrism.Location = new System.Drawing.Point(14, 30);
            this.lblPrism.Name = "lblPrism";
            this.lblPrism.Size = new System.Drawing.Size(75, 29);
            this.lblPrism.TabIndex = 0;
            this.lblPrism.Text = "プリズム:";
            this.lblPrism.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBSetPrism
            // 
            this.CBSetPrism.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSetPrism.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBSetPrism.FormattingEnabled = true;
            this.CBSetPrism.Location = new System.Drawing.Point(94, 28);
            this.CBSetPrism.Name = "CBSetPrism";
            this.CBSetPrism.Size = new System.Drawing.Size(160, 29);
            this.CBSetPrism.TabIndex = 1;
            this.CBSetPrism.SelectedIndexChanged += new System.EventHandler(this.CBSetPrism_SelectedIndexChanged);
            // 
            // lblPrismVal
            // 
            this.lblPrismVal.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblPrismVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblPrismVal.Location = new System.Drawing.Point(260, 30);
            this.lblPrismVal.Name = "lblPrismVal";
            this.lblPrismVal.Size = new System.Drawing.Size(50, 29);
            this.lblPrismVal.TabIndex = 2;
            this.lblPrismVal.Text = "定数:";
            this.lblPrismVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBPrismVal
            // 
            this.TBPrismVal.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.TBPrismVal.Location = new System.Drawing.Point(314, 28);
            this.TBPrismVal.Name = "TBPrismVal";
            this.TBPrismVal.Size = new System.Drawing.Size(75, 29);
            this.TBPrismVal.TabIndex = 3;
            this.TBPrismVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblMm
            // 
            this.lblMm.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblMm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblMm.Location = new System.Drawing.Point(394, 30);
            this.lblMm.Name = "lblMm";
            this.lblMm.Size = new System.Drawing.Size(40, 29);
            this.lblMm.TabIndex = 4;
            this.lblMm.Text = "mm";
            this.lblMm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSokkyo
            // 
            this.lblSokkyo.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblSokkyo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblSokkyo.Location = new System.Drawing.Point(14, 68);
            this.lblSokkyo.Name = "lblSokkyo";
            this.lblSokkyo.Size = new System.Drawing.Size(95, 29);
            this.lblSokkyo.TabIndex = 5;
            this.lblSokkyo.Text = "測距モード:";
            this.lblSokkyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBSokkyoMode
            // 
            this.CBSokkyoMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSokkyoMode.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBSokkyoMode.FormattingEnabled = true;
            this.CBSokkyoMode.Location = new System.Drawing.Point(114, 66);
            this.CBSokkyoMode.Name = "CBSokkyoMode";
            this.CBSokkyoMode.Size = new System.Drawing.Size(320, 29);
            this.CBSokkyoMode.TabIndex = 6;
            // 
            // lblTilt
            // 
            this.lblTilt.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblTilt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblTilt.Location = new System.Drawing.Point(14, 106);
            this.lblTilt.Name = "lblTilt";
            this.lblTilt.Size = new System.Drawing.Size(95, 29);
            this.lblTilt.TabIndex = 7;
            this.lblTilt.Text = "チルト補正:";
            this.lblTilt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBTilt
            // 
            this.CBTilt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTilt.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBTilt.FormattingEnabled = true;
            this.CBTilt.Location = new System.Drawing.Point(114, 104);
            this.CBTilt.Name = "CBTilt";
            this.CBTilt.Size = new System.Drawing.Size(320, 29);
            this.CBTilt.TabIndex = 8;
            // 
            // grpSearch
            // 
            this.grpSearch.BackColor = System.Drawing.Color.White;
            this.grpSearch.Controls.Add(this.lblSearch);
            this.grpSearch.Controls.Add(this.lblH);
            this.grpSearch.Controls.Add(this.TBSearchH);
            this.grpSearch.Controls.Add(this.lblHdeg);
            this.grpSearch.Controls.Add(this.lblV);
            this.grpSearch.Controls.Add(this.TBSearchV);
            this.grpSearch.Controls.Add(this.lblVdeg);
            this.grpSearch.Controls.Add(this.lblRc);
            this.grpSearch.Controls.Add(this.CBUseRC);
            this.grpSearch.Controls.Add(this.lblGuide);
            this.grpSearch.Controls.Add(this.CBGuidLightPat);
            this.grpSearch.Controls.Add(this.CBGuidLightVal);
            this.grpSearch.Controls.Add(this.lblLaser);
            this.grpSearch.Controls.Add(this.CBLightPat);
            this.grpSearch.Controls.Add(this.CBLightVal);
            this.grpSearch.Controls.Add(this.lblDesc);
            this.grpSearch.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.grpSearch.Location = new System.Drawing.Point(14, 172);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(452, 206);
            this.grpSearch.TabIndex = 1;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = " 📡 サーチ・ライト・RC設定 ";
            // 
            // lblSearch
            // 
            this.lblSearch.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblSearch.Location = new System.Drawing.Point(14, 30);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(95, 29);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "サーチ範囲:";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblH
            // 
            this.lblH.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblH.Location = new System.Drawing.Point(114, 30);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(22, 29);
            this.lblH.TabIndex = 1;
            this.lblH.Text = "H:";
            this.lblH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TBSearchH
            // 
            this.TBSearchH.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.TBSearchH.Location = new System.Drawing.Point(138, 28);
            this.TBSearchH.Name = "TBSearchH";
            this.TBSearchH.Size = new System.Drawing.Size(46, 29);
            this.TBSearchH.TabIndex = 2;
            this.TBSearchH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblHdeg
            // 
            this.lblHdeg.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblHdeg.Location = new System.Drawing.Point(186, 30);
            this.lblHdeg.Name = "lblHdeg";
            this.lblHdeg.Size = new System.Drawing.Size(18, 29);
            this.lblHdeg.TabIndex = 3;
            this.lblHdeg.Text = "°";
            this.lblHdeg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblV
            // 
            this.lblV.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblV.Location = new System.Drawing.Point(208, 30);
            this.lblV.Name = "lblV";
            this.lblV.Size = new System.Drawing.Size(22, 29);
            this.lblV.TabIndex = 4;
            this.lblV.Text = "V:";
            this.lblV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TBSearchV
            // 
            this.TBSearchV.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.TBSearchV.Location = new System.Drawing.Point(232, 28);
            this.TBSearchV.Name = "TBSearchV";
            this.TBSearchV.Size = new System.Drawing.Size(46, 29);
            this.TBSearchV.TabIndex = 5;
            this.TBSearchV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblVdeg
            // 
            this.lblVdeg.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblVdeg.Location = new System.Drawing.Point(280, 30);
            this.lblVdeg.Name = "lblVdeg";
            this.lblVdeg.Size = new System.Drawing.Size(18, 29);
            this.lblVdeg.TabIndex = 6;
            this.lblVdeg.Text = "°";
            this.lblVdeg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRc
            // 
            this.lblRc.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblRc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblRc.Location = new System.Drawing.Point(302, 30);
            this.lblRc.Name = "lblRc";
            this.lblRc.Size = new System.Drawing.Size(40, 29);
            this.lblRc.TabIndex = 7;
            this.lblRc.Text = "RC:";
            this.lblRc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBUseRC
            // 
            this.CBUseRC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBUseRC.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBUseRC.FormattingEnabled = true;
            this.CBUseRC.Location = new System.Drawing.Point(346, 28);
            this.CBUseRC.Name = "CBUseRC";
            this.CBUseRC.Size = new System.Drawing.Size(88, 29);
            this.CBUseRC.TabIndex = 8;
            // 
            // lblGuide
            // 
            this.lblGuide.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblGuide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblGuide.Location = new System.Drawing.Point(14, 68);
            this.lblGuide.Name = "lblGuide";
            this.lblGuide.Size = new System.Drawing.Size(95, 29);
            this.lblGuide.TabIndex = 9;
            this.lblGuide.Text = "ガイドライト:";
            this.lblGuide.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBGuidLightPat
            // 
            this.CBGuidLightPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGuidLightPat.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBGuidLightPat.FormattingEnabled = true;
            this.CBGuidLightPat.Location = new System.Drawing.Point(114, 66);
            this.CBGuidLightPat.Name = "CBGuidLightPat";
            this.CBGuidLightPat.Size = new System.Drawing.Size(190, 29);
            this.CBGuidLightPat.TabIndex = 10;
            // 
            // CBGuidLightVal
            // 
            this.CBGuidLightVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGuidLightVal.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBGuidLightVal.FormattingEnabled = true;
            this.CBGuidLightVal.Location = new System.Drawing.Point(310, 66);
            this.CBGuidLightVal.Name = "CBGuidLightVal";
            this.CBGuidLightVal.Size = new System.Drawing.Size(124, 29);
            this.CBGuidLightVal.TabIndex = 11;
            // 
            // lblLaser
            // 
            this.lblLaser.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblLaser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblLaser.Location = new System.Drawing.Point(14, 106);
            this.lblLaser.Name = "lblLaser";
            this.lblLaser.Size = new System.Drawing.Size(95, 29);
            this.lblLaser.TabIndex = 12;
            this.lblLaser.Text = "照射ライト:";
            this.lblLaser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBLightPat
            // 
            this.CBLightPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBLightPat.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBLightPat.FormattingEnabled = true;
            this.CBLightPat.Location = new System.Drawing.Point(114, 104);
            this.CBLightPat.Name = "CBLightPat";
            this.CBLightPat.Size = new System.Drawing.Size(190, 29);
            this.CBLightPat.TabIndex = 13;
            // 
            // CBLightVal
            // 
            this.CBLightVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBLightVal.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBLightVal.FormattingEnabled = true;
            this.CBLightVal.Location = new System.Drawing.Point(310, 104);
            this.CBLightVal.Name = "CBLightVal";
            this.CBLightVal.Size = new System.Drawing.Size(124, 29);
            this.CBLightVal.TabIndex = 14;
            // 
            // lblDesc
            // 
            this.lblDesc.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F);
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(115)))), ((int)(((byte)(125)))));
            this.lblDesc.Location = new System.Drawing.Point(14, 148);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(420, 44);
            this.lblDesc.TabIndex = 15;
            this.lblDesc.Text = "※ 接続TSの機種により対応機能のみ有効となります。";
            // 
            // grpGps
            // 
            this.grpGps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.grpGps.Controls.Add(this.lblGpsH);
            this.grpGps.Controls.Add(this.CBGPSHeight);
            this.grpGps.Controls.Add(this.lblGpsCnt);
            this.grpGps.Controls.Add(this.TBGPSCount);
            this.grpGps.Controls.Add(this.lblKai);
            this.grpGps.Controls.Add(this.lblImu);
            this.grpGps.Controls.Add(this.CBi93IMU);
            this.grpGps.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpGps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(190)))));
            this.grpGps.Location = new System.Drawing.Point(14, 388);
            this.grpGps.Name = "grpGps";
            this.grpGps.Size = new System.Drawing.Size(452, 120);
            this.grpGps.TabIndex = 2;
            this.grpGps.TabStop = false;
            this.grpGps.Text = " 🛰 RTK-GPS 設定 ";
            // 
            // lblGpsH
            // 
            this.lblGpsH.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblGpsH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblGpsH.Location = new System.Drawing.Point(14, 30);
            this.lblGpsH.Name = "lblGpsH";
            this.lblGpsH.Size = new System.Drawing.Size(95, 29);
            this.lblGpsH.TabIndex = 0;
            this.lblGpsH.Text = "アンテナ高:";
            this.lblGpsH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBGPSHeight
            // 
            this.CBGPSHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGPSHeight.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBGPSHeight.FormattingEnabled = true;
            this.CBGPSHeight.Location = new System.Drawing.Point(114, 28);
            this.CBGPSHeight.Name = "CBGPSHeight";
            this.CBGPSHeight.Size = new System.Drawing.Size(150, 29);
            this.CBGPSHeight.TabIndex = 1;
            // 
            // lblGpsCnt
            // 
            this.lblGpsCnt.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblGpsCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblGpsCnt.Location = new System.Drawing.Point(270, 30);
            this.lblGpsCnt.Name = "lblGpsCnt";
            this.lblGpsCnt.Size = new System.Drawing.Size(80, 29);
            this.lblGpsCnt.TabIndex = 2;
            this.lblGpsCnt.Text = "平均回数:";
            this.lblGpsCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBGPSCount
            // 
            this.TBGPSCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.TBGPSCount.Location = new System.Drawing.Point(354, 28);
            this.TBGPSCount.Name = "TBGPSCount";
            this.TBGPSCount.Size = new System.Drawing.Size(50, 29);
            this.TBGPSCount.TabIndex = 3;
            this.TBGPSCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblKai
            // 
            this.lblKai.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblKai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblKai.Location = new System.Drawing.Point(408, 30);
            this.lblKai.Name = "lblKai";
            this.lblKai.Size = new System.Drawing.Size(30, 29);
            this.lblKai.TabIndex = 4;
            this.lblKai.Text = "回";
            this.lblKai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImu
            // 
            this.lblImu.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblImu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblImu.Location = new System.Drawing.Point(14, 68);
            this.lblImu.Name = "lblImu";
            this.lblImu.Size = new System.Drawing.Size(95, 29);
            this.lblImu.TabIndex = 5;
            this.lblImu.Text = "i93 IMU補正:";
            this.lblImu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBi93IMU
            // 
            this.CBi93IMU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBi93IMU.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.CBi93IMU.FormattingEnabled = true;
            this.CBi93IMU.Location = new System.Drawing.Point(114, 66);
            this.CBi93IMU.Name = "CBi93IMU";
            this.CBi93IMU.Size = new System.Drawing.Size(320, 29);
            this.CBi93IMU.TabIndex = 6;
            // 
            // btnExportToMaster
            // 
            this.btnExportToMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.btnExportToMaster.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.btnExportToMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToMaster.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnExportToMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.btnExportToMaster.Location = new System.Drawing.Point(14, 522);
            this.btnExportToMaster.Name = "btnExportToMaster";
            this.btnExportToMaster.Size = new System.Drawing.Size(220, 38);
            this.btnExportToMaster.TabIndex = 3;
            this.btnExportToMaster.Text = "📤 マスターへ反映";
            this.btnExportToMaster.UseVisualStyleBackColor = false;
            this.btnExportToMaster.Click += new System.EventHandler(this.BtnExportToMaster_Click);
            // 
            // btnImportFromMaster
            // 
            this.btnImportFromMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.btnImportFromMaster.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.btnImportFromMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromMaster.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnImportFromMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.btnImportFromMaster.Location = new System.Drawing.Point(246, 522);
            this.btnImportFromMaster.Name = "btnImportFromMaster";
            this.btnImportFromMaster.Size = new System.Drawing.Size(220, 38);
            this.btnImportFromMaster.TabIndex = 4;
            this.btnImportFromMaster.Text = "📥 マスターから反映";
            this.btnImportFromMaster.UseVisualStyleBackColor = false;
            this.btnImportFromMaster.Click += new System.EventHandler(this.BtnImportFromMaster_Click);
            // 
            // Do_Button
            // 
            this.Do_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.Do_Button.FlatAppearance.BorderSize = 0;
            this.Do_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Do_Button.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.Do_Button.ForeColor = System.Drawing.Color.White;
            this.Do_Button.Location = new System.Drawing.Point(14, 568);
            this.Do_Button.Name = "Do_Button";
            this.Do_Button.Size = new System.Drawing.Size(304, 44);
            this.Do_Button.TabIndex = 5;
            this.Do_Button.Text = "💾 設定を保存";
            this.Do_Button.UseVisualStyleBackColor = false;
            this.Do_Button.Click += new System.EventHandler(this.Do_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.Cancel_Button.FlatAppearance.BorderSize = 0;
            this.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_Button.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.Cancel_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.Cancel_Button.Location = new System.Drawing.Point(328, 568);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(138, 44);
            this.Cancel_Button.TabIndex = 6;
            this.Cancel_Button.Text = "閉じる";
            this.Cancel_Button.UseVisualStyleBackColor = false;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // FormDefEnv
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(480, 626);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Do_Button);
            this.Controls.Add(this.btnImportFromMaster);
            this.Controls.Add(this.btnExportToMaster);
            this.Controls.Add(this.grpGps);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.grpPrism);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDefEnv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TS・GPS環境設定";
            this.Load += new System.EventHandler(this.FormDefEnv_Load);
            this.grpPrism.ResumeLayout(false);
            this.grpPrism.PerformLayout();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpGps.ResumeLayout(false);
            this.grpGps.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPrism;
        private System.Windows.Forms.Label lblPrism;
        public System.Windows.Forms.ComboBox CBSetPrism;
        private System.Windows.Forms.Label lblPrismVal;
        public System.Windows.Forms.TextBox TBPrismVal;
        private System.Windows.Forms.Label lblMm;
        private System.Windows.Forms.Label lblSokkyo;
        public System.Windows.Forms.ComboBox CBSokkyoMode;
        private System.Windows.Forms.Label lblTilt;
        public System.Windows.Forms.ComboBox CBTilt;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblH;
        public System.Windows.Forms.TextBox TBSearchH;
        private System.Windows.Forms.Label lblHdeg;
        private System.Windows.Forms.Label lblV;
        public System.Windows.Forms.TextBox TBSearchV;
        private System.Windows.Forms.Label lblVdeg;
        private System.Windows.Forms.Label lblRc;
        public System.Windows.Forms.ComboBox CBUseRC;
        private System.Windows.Forms.Label lblGuide;
        public System.Windows.Forms.ComboBox CBGuidLightPat;
        public System.Windows.Forms.ComboBox CBGuidLightVal;
        private System.Windows.Forms.Label lblLaser;
        public System.Windows.Forms.ComboBox CBLightPat;
        public System.Windows.Forms.ComboBox CBLightVal;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.GroupBox grpGps;
        private System.Windows.Forms.Label lblGpsH;
        public System.Windows.Forms.ComboBox CBGPSHeight;
        private System.Windows.Forms.Label lblGpsCnt;
        public System.Windows.Forms.TextBox TBGPSCount;
        private System.Windows.Forms.Label lblKai;
        private System.Windows.Forms.Label lblImu;
        public System.Windows.Forms.ComboBox CBi93IMU;
        private System.Windows.Forms.Button btnExportToMaster;
        private System.Windows.Forms.Button btnImportFromMaster;
        public System.Windows.Forms.Button Do_Button;
        public System.Windows.Forms.Button Cancel_Button;
    }
}
