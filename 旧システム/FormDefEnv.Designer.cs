namespace Site7 {
    partial class FormDefEnv {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Do_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpTS = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TBGPSCount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.CBi93IMU = new System.Windows.Forms.ComboBox();
            this.CBGPSHeight = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CBGuidLightPat = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.CBGuidLightVal = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TBSearchV = new System.Windows.Forms.TextBox();
            this.TBSearchH = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CBLightVal = new System.Windows.Forms.ComboBox();
            this.CBUseRC = new System.Windows.Forms.ComboBox();
            this.CBLightPat = new System.Windows.Forms.ComboBox();
            this.CBTilt = new System.Windows.Forms.ComboBox();
            this.CBSokkyoMode = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TBPrismVal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CBSetPrism = new System.Windows.Forms.ComboBox();
            this.tpZumen = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TBPaperAng = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.CBPaperScale = new System.Windows.Forms.ComboBox();
            this.CBPaperSize = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpTS.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpZumen.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Do_Button);
            this.panel1.Controls.Add(this.Cancel_Button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 492);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 51);
            this.panel1.TabIndex = 0;
            // 
            // Do_Button
            // 
            this.Do_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Do_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Do_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Do_Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Do_Button.Location = new System.Drawing.Point(327, 10);
            this.Do_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Do_Button.Name = "Do_Button";
            this.Do_Button.Size = new System.Drawing.Size(100, 28);
            this.Do_Button.TabIndex = 66;
            this.Do_Button.Text = "設定";
            this.Do_Button.UseVisualStyleBackColor = true;
            this.Do_Button.Click += new System.EventHandler(this.Do_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancel_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cancel_Button.Location = new System.Drawing.Point(219, 10);
            this.Cancel_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(100, 28);
            this.Cancel_Button.TabIndex = 66;
            this.Cancel_Button.Text = "閉じる";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpTS);
            this.tabControl1.Controls.Add(this.tpZumen);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(440, 492);
            this.tabControl1.TabIndex = 1;
            // 
            // tpTS
            // 
            this.tpTS.Controls.Add(this.groupBox4);
            this.tpTS.Controls.Add(this.groupBox3);
            this.tpTS.Controls.Add(this.groupBox2);
            this.tpTS.Controls.Add(this.groupBox1);
            this.tpTS.Location = new System.Drawing.Point(4, 22);
            this.tpTS.Name = "tpTS";
            this.tpTS.Padding = new System.Windows.Forms.Padding(3);
            this.tpTS.Size = new System.Drawing.Size(432, 466);
            this.tpTS.TabIndex = 0;
            this.tpTS.Text = "TS・GPS設定";
            this.tpTS.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TBGPSCount);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.CBi93IMU);
            this.groupBox4.Controls.Add(this.CBGPSHeight);
            this.groupBox4.Location = new System.Drawing.Point(10, 353);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(415, 109);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "RTK-GPS用設定";
            // 
            // TBGPSCount
            // 
            this.TBGPSCount.Location = new System.Drawing.Point(218, 52);
            this.TBGPSCount.Name = "TBGPSCount";
            this.TBGPSCount.Size = new System.Drawing.Size(47, 19);
            this.TBGPSCount.TabIndex = 175;
            this.TBGPSCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBSearchH_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 12);
            this.label13.TabIndex = 174;
            this.label13.Text = "GPSからの取得高さ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 83);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 12);
            this.label16.TabIndex = 174;
            this.label16.Text = "i93傾斜補正";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 55);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(205, 12);
            this.label15.TabIndex = 174;
            this.label15.Text = "基準点のRTK-Fix時の平均を求める回数";
            // 
            // CBi93IMU
            // 
            this.CBi93IMU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBi93IMU.DropDownHeight = 400;
            this.CBi93IMU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBi93IMU.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBi93IMU.FormattingEnabled = true;
            this.CBi93IMU.IntegralHeight = false;
            this.CBi93IMU.Items.AddRange(new object[] {
            "補正なし",
            "補正あり"});
            this.CBi93IMU.Location = new System.Drawing.Point(113, 79);
            this.CBi93IMU.Name = "CBi93IMU";
            this.CBi93IMU.Size = new System.Drawing.Size(82, 21);
            this.CBi93IMU.TabIndex = 173;
            // 
            // CBGPSHeight
            // 
            this.CBGPSHeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBGPSHeight.DropDownHeight = 400;
            this.CBGPSHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGPSHeight.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBGPSHeight.FormattingEnabled = true;
            this.CBGPSHeight.IntegralHeight = false;
            this.CBGPSHeight.Items.AddRange(new object[] {
            "平均海水面からの標高(ジオイド2024)",
            "平均海水面からの標高(ジオイド2011)",
            "WGS84楕円体からの高さ"});
            this.CBGPSHeight.Location = new System.Drawing.Point(113, 24);
            this.CBGPSHeight.Name = "CBGPSHeight";
            this.CBGPSHeight.Size = new System.Drawing.Size(240, 21);
            this.CBGPSHeight.TabIndex = 173;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CBGuidLightPat);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.CBGuidLightVal);
            this.groupBox3.Location = new System.Drawing.Point(10, 273);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(415, 74);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "LN100用設定";
            // 
            // CBGuidLightPat
            // 
            this.CBGuidLightPat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBGuidLightPat.DropDownHeight = 400;
            this.CBGuidLightPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGuidLightPat.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBGuidLightPat.FormattingEnabled = true;
            this.CBGuidLightPat.IntegralHeight = false;
            this.CBGuidLightPat.Items.AddRange(new object[] {
            "パターン１",
            "パターン２"});
            this.CBGuidLightPat.Location = new System.Drawing.Point(113, 18);
            this.CBGuidLightPat.Name = "CBGuidLightPat";
            this.CBGuidLightPat.Size = new System.Drawing.Size(123, 21);
            this.CBGuidLightPat.TabIndex = 173;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 12);
            this.label12.TabIndex = 174;
            this.label12.Text = "ガイドライト光量";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 12);
            this.label11.TabIndex = 174;
            this.label11.Text = "ガイドライトパターン";
            // 
            // CBGuidLightVal
            // 
            this.CBGuidLightVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBGuidLightVal.DropDownHeight = 400;
            this.CBGuidLightVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGuidLightVal.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBGuidLightVal.FormattingEnabled = true;
            this.CBGuidLightVal.IntegralHeight = false;
            this.CBGuidLightVal.Items.AddRange(new object[] {
            "暗い",
            "普通",
            "明るい"});
            this.CBGuidLightVal.Location = new System.Drawing.Point(113, 42);
            this.CBGuidLightVal.Name = "CBGuidLightVal";
            this.CBGuidLightVal.Size = new System.Drawing.Size(71, 21);
            this.CBGuidLightVal.TabIndex = 173;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TBSearchV);
            this.groupBox2.Controls.Add(this.TBSearchH);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.CBLightVal);
            this.groupBox2.Controls.Add(this.CBUseRC);
            this.groupBox2.Controls.Add(this.CBLightPat);
            this.groupBox2.Controls.Add(this.CBTilt);
            this.groupBox2.Controls.Add(this.CBSokkyoMode);
            this.groupBox2.Location = new System.Drawing.Point(10, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 178);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "トプコンTS用設定";
            // 
            // TBSearchV
            // 
            this.TBSearchV.Location = new System.Drawing.Point(275, 111);
            this.TBSearchV.Name = "TBSearchV";
            this.TBSearchV.Size = new System.Drawing.Size(69, 19);
            this.TBSearchV.TabIndex = 175;
            this.TBSearchV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBSearchH_KeyPress);
            // 
            // TBSearchH
            // 
            this.TBSearchH.Location = new System.Drawing.Point(115, 111);
            this.TBSearchH.Name = "TBSearchH";
            this.TBSearchH.Size = new System.Drawing.Size(69, 19);
            this.TBSearchH.TabIndex = 175;
            this.TBSearchH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBSearchH_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 12);
            this.label7.TabIndex = 174;
            this.label7.Text = "ライト光量";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(203, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 12);
            this.label9.TabIndex = 174;
            this.label9.Text = "Vサーチ範囲";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 12);
            this.label10.TabIndex = 174;
            this.label10.Text = "リモートコントロール使用";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 12);
            this.label8.TabIndex = 174;
            this.label8.Text = "Hサーチ範囲";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(203, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 12);
            this.label6.TabIndex = 174;
            this.label6.Text = "ライト機能";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 12);
            this.label5.TabIndex = 174;
            this.label5.Text = "チルト補正機能";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 174;
            this.label4.Text = "測距モード";
            // 
            // CBLightVal
            // 
            this.CBLightVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBLightVal.DropDownHeight = 400;
            this.CBLightVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBLightVal.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBLightVal.FormattingEnabled = true;
            this.CBLightVal.IntegralHeight = false;
            this.CBLightVal.Items.AddRange(new object[] {
            "暗い",
            "普通",
            "明るい"});
            this.CBLightVal.Location = new System.Drawing.Point(115, 82);
            this.CBLightVal.Name = "CBLightVal";
            this.CBLightVal.Size = new System.Drawing.Size(69, 21);
            this.CBLightVal.TabIndex = 173;
            // 
            // CBUseRC
            // 
            this.CBUseRC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBUseRC.DropDownHeight = 400;
            this.CBUseRC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBUseRC.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBUseRC.FormattingEnabled = true;
            this.CBUseRC.IntegralHeight = false;
            this.CBUseRC.Items.AddRange(new object[] {
            "使用しない",
            "使用する"});
            this.CBUseRC.Location = new System.Drawing.Point(135, 140);
            this.CBUseRC.Name = "CBUseRC";
            this.CBUseRC.Size = new System.Drawing.Size(90, 21);
            this.CBUseRC.TabIndex = 173;
            // 
            // CBLightPat
            // 
            this.CBLightPat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBLightPat.DropDownHeight = 400;
            this.CBLightPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBLightPat.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBLightPat.FormattingEnabled = true;
            this.CBLightPat.IntegralHeight = false;
            this.CBLightPat.Items.AddRange(new object[] {
            "ガイドライト・ＬＥＤ",
            "レーザー照準"});
            this.CBLightPat.Location = new System.Drawing.Point(274, 83);
            this.CBLightPat.Name = "CBLightPat";
            this.CBLightPat.Size = new System.Drawing.Size(135, 21);
            this.CBLightPat.TabIndex = 173;
            // 
            // CBTilt
            // 
            this.CBTilt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBTilt.DropDownHeight = 400;
            this.CBTilt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTilt.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBTilt.FormattingEnabled = true;
            this.CBTilt.IntegralHeight = false;
            this.CBTilt.Items.AddRange(new object[] {
            "TILT補正(H,V)",
            "TILT補正なし",
            "TILT補正(Vのみ)"});
            this.CBTilt.Location = new System.Drawing.Point(115, 51);
            this.CBTilt.Name = "CBTilt";
            this.CBTilt.Size = new System.Drawing.Size(121, 21);
            this.CBTilt.TabIndex = 173;
            // 
            // CBSokkyoMode
            // 
            this.CBSokkyoMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBSokkyoMode.DropDownHeight = 400;
            this.CBSokkyoMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSokkyoMode.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBSokkyoMode.FormattingEnabled = true;
            this.CBSokkyoMode.IntegralHeight = false;
            this.CBSokkyoMode.Items.AddRange(new object[] {
            "精密",
            "高速"});
            this.CBSokkyoMode.Location = new System.Drawing.Point(115, 23);
            this.CBSokkyoMode.Name = "CBSokkyoMode";
            this.CBSokkyoMode.Size = new System.Drawing.Size(80, 21);
            this.CBSokkyoMode.TabIndex = 173;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TBPrismVal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CBSetPrism);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "プリズム設定";
            // 
            // TBPrismVal
            // 
            this.TBPrismVal.Location = new System.Drawing.Point(117, 48);
            this.TBPrismVal.Name = "TBPrismVal";
            this.TBPrismVal.Size = new System.Drawing.Size(69, 19);
            this.TBPrismVal.TabIndex = 175;
            this.TBPrismVal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBPrismVal_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 12);
            this.label3.TabIndex = 174;
            this.label3.Text = "(mm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 174;
            this.label2.Text = "プリズム定数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 174;
            this.label1.Text = "プリズム名";
            // 
            // CBSetPrism
            // 
            this.CBSetPrism.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBSetPrism.DropDownHeight = 400;
            this.CBSetPrism.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSetPrism.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBSetPrism.FormattingEnabled = true;
            this.CBSetPrism.IntegralHeight = false;
            this.CBSetPrism.Items.AddRange(new object[] {
            "プリズム(0mm)",
            "360°プリズム ATP2(-7mm)",
            "360°プリズム A7P(-2mm)",
            "ノンプリズム(0mm)"});
            this.CBSetPrism.Location = new System.Drawing.Point(117, 18);
            this.CBSetPrism.Name = "CBSetPrism";
            this.CBSetPrism.Size = new System.Drawing.Size(213, 21);
            this.CBSetPrism.TabIndex = 173;
            this.CBSetPrism.SelectedIndexChanged += new System.EventHandler(this.CBSetPrism_SelectedIndexChanged);
            // 
            // tpZumen
            // 
            this.tpZumen.Controls.Add(this.groupBox5);
            this.tpZumen.Location = new System.Drawing.Point(4, 22);
            this.tpZumen.Name = "tpZumen";
            this.tpZumen.Padding = new System.Windows.Forms.Padding(3);
            this.tpZumen.Size = new System.Drawing.Size(432, 466);
            this.tpZumen.TabIndex = 1;
            this.tpZumen.Text = "作図設定";
            this.tpZumen.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TBPaperAng);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.CBPaperScale);
            this.groupBox5.Controls.Add(this.CBPaperSize);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(417, 106);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "全図用紙設定";
            // 
            // TBPaperAng
            // 
            this.TBPaperAng.Location = new System.Drawing.Point(125, 77);
            this.TBPaperAng.Name = "TBPaperAng";
            this.TBPaperAng.Size = new System.Drawing.Size(47, 19);
            this.TBPaperAng.TabIndex = 175;
            this.TBPaperAng.Text = "0";
            this.TBPaperAng.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBPrismVal_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(178, 80);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 12);
            this.label20.TabIndex = 174;
            this.label20.Text = "度";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(14, 80);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 12);
            this.label19.TabIndex = 174;
            this.label19.Text = "回転角度(時計回り)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 174;
            this.label17.Text = "縮尺";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 12);
            this.label18.TabIndex = 174;
            this.label18.Text = "用紙サイズ";
            // 
            // CBPaperScale
            // 
            this.CBPaperScale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBPaperScale.DropDownHeight = 400;
            this.CBPaperScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPaperScale.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBPaperScale.FormattingEnabled = true;
            this.CBPaperScale.IntegralHeight = false;
            this.CBPaperScale.Items.AddRange(new object[] {
            "1 / 20",
            "1 / 50",
            "1 /100",
            "1 /200",
            "1 /500",
            "1 /1000"});
            this.CBPaperScale.Location = new System.Drawing.Point(117, 45);
            this.CBPaperScale.Name = "CBPaperScale";
            this.CBPaperScale.Size = new System.Drawing.Size(78, 21);
            this.CBPaperScale.TabIndex = 173;
            // 
            // CBPaperSize
            // 
            this.CBPaperSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBPaperSize.DropDownHeight = 400;
            this.CBPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPaperSize.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBPaperSize.FormattingEnabled = true;
            this.CBPaperSize.IntegralHeight = false;
            this.CBPaperSize.Items.AddRange(new object[] {
            "A0横(1189mm x 841mm)",
            "A1横(841mm x 594mm)",
            "A2横(594mm x 420mm)",
            "A3横(420mm x 297mm)",
            "A4横(297mm x 210mm)",
            "A5横(210mm x 148mm)"});
            this.CBPaperSize.Location = new System.Drawing.Point(117, 18);
            this.CBPaperSize.Name = "CBPaperSize";
            this.CBPaperSize.Size = new System.Drawing.Size(163, 21);
            this.CBPaperSize.TabIndex = 173;
            // 
            // FormDefEnv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(440, 543);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "FormDefEnv";
            this.Text = "環境設定";
            this.Load += new System.EventHandler(this.FormDefEnv_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpTS.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpZumen.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpTS;
        private System.Windows.Forms.TabPage tpZumen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox CBSetPrism;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox CBSokkyoMode;
        private System.Windows.Forms.TextBox TBPrismVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBSearchV;
        private System.Windows.Forms.TextBox TBSearchH;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox CBLightVal;
        public System.Windows.Forms.ComboBox CBLightPat;
        public System.Windows.Forms.ComboBox CBTilt;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ComboBox CBGuidLightPat;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox CBGuidLightVal;
        public System.Windows.Forms.ComboBox CBUseRC;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox TBGPSCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.ComboBox CBGPSHeight;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.ComboBox CBi93IMU;
        internal System.Windows.Forms.Button Do_Button;
        internal System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.ComboBox CBPaperSize;
        private System.Windows.Forms.TextBox TBPaperAng;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.ComboBox CBPaperScale;
    }
}