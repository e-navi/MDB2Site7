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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Do_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpTS = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CBi93IMU = new System.Windows.Forms.ComboBox();
            this.TBGPSCount = new System.Windows.Forms.TextBox();
            this.CBGPSHeight = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CBGuidLightVal = new System.Windows.Forms.ComboBox();
            this.CBGuidLightPat = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TBSearchV = new System.Windows.Forms.TextBox();
            this.TBSearchH = new System.Windows.Forms.TextBox();
            this.CBUseRC = new System.Windows.Forms.ComboBox();
            this.CBLightVal = new System.Windows.Forms.ComboBox();
            this.CBLightPat = new System.Windows.Forms.ComboBox();
            this.CBTilt = new System.Windows.Forms.ComboBox();
            this.CBSokkyoMode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TBPrismVal = new System.Windows.Forms.TextBox();
            this.CBSetPrism = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpZumen = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TBPaperAng = new System.Windows.Forms.TextBox();
            this.CBPaperScale = new System.Windows.Forms.ComboBox();
            this.CBPaperSize = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
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
            this.panel1.Location = new System.Drawing.Point(0, 485);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 46);
            this.panel1.TabIndex = 0;
            // 
            // Do_Button
            // 
            this.Do_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Do_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(214)))));
            this.Do_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Do_Button.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.Do_Button.ForeColor = System.Drawing.Color.White;
            this.Do_Button.Location = new System.Drawing.Point(352, 8);
            this.Do_Button.Name = "Do_Button";
            this.Do_Button.Size = new System.Drawing.Size(100, 30);
            this.Do_Button.TabIndex = 1;
            this.Do_Button.Text = "設定";
            this.Do_Button.UseVisualStyleBackColor = false;
            this.Do_Button.Click += new System.EventHandler(this.Do_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_Button.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F);
            this.Cancel_Button.Location = new System.Drawing.Point(244, 8);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(100, 30);
            this.Cancel_Button.TabIndex = 0;
            this.Cancel_Button.Text = "閉じる";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpTS);
            this.tabControl1.Controls.Add(this.tpZumen);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(464, 485);
            this.tabControl1.TabIndex = 1;
            // 
            // tpTS
            // 
            this.tpTS.Controls.Add(this.groupBox4);
            this.tpTS.Controls.Add(this.groupBox3);
            this.tpTS.Controls.Add(this.groupBox2);
            this.tpTS.Controls.Add(this.groupBox1);
            this.tpTS.Location = new System.Drawing.Point(4, 24);
            this.tpTS.Name = "tpTS";
            this.tpTS.Padding = new System.Windows.Forms.Padding(6);
            this.tpTS.Size = new System.Drawing.Size(456, 457);
            this.tpTS.TabIndex = 0;
            this.tpTS.Text = "TS・GPS設定";
            this.tpTS.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CBi93IMU);
            this.groupBox4.Controls.Add(this.TBGPSCount);
            this.groupBox4.Controls.Add(this.CBGPSHeight);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox4.Location = new System.Drawing.Point(8, 345);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(440, 105);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "RTK-GPS用設定";
            // 
            // CBi93IMU
            // 
            this.CBi93IMU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBi93IMU.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBi93IMU.FormattingEnabled = true;
            this.CBi93IMU.Location = new System.Drawing.Point(140, 74);
            this.CBi93IMU.Name = "CBi93IMU";
            this.CBi93IMU.Size = new System.Drawing.Size(100, 23);
            this.CBi93IMU.TabIndex = 5;
            // 
            // TBGPSCount
            // 
            this.TBGPSCount.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.TBGPSCount.Location = new System.Drawing.Point(230, 47);
            this.TBGPSCount.Name = "TBGPSCount";
            this.TBGPSCount.Size = new System.Drawing.Size(50, 23);
            this.TBGPSCount.TabIndex = 4;
            // 
            // CBGPSHeight
            // 
            this.CBGPSHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGPSHeight.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBGPSHeight.FormattingEnabled = true;
            this.CBGPSHeight.Location = new System.Drawing.Point(140, 20);
            this.CBGPSHeight.Name = "CBGPSHeight";
            this.CBGPSHeight.Size = new System.Drawing.Size(285, 23);
            this.CBGPSHeight.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label16.Location = new System.Drawing.Point(12, 77);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 15);
            this.label16.TabIndex = 2;
            this.label16.Text = "i93傾斜補正";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label15.Location = new System.Drawing.Point(12, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(211, 15);
            this.label15.TabIndex = 1;
            this.label15.Text = "基準点のRTK-Fix時の平均を求める回数";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label13.Location = new System.Drawing.Point(12, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 15);
            this.label13.TabIndex = 0;
            this.label13.Text = "GPSからの取得高さ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CBGuidLightVal);
            this.groupBox3.Controls.Add(this.CBGuidLightPat);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(8, 265);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(440, 75);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "LN100用設定";
            // 
            // CBGuidLightVal
            // 
            this.CBGuidLightVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGuidLightVal.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBGuidLightVal.FormattingEnabled = true;
            this.CBGuidLightVal.Location = new System.Drawing.Point(140, 46);
            this.CBGuidLightVal.Name = "CBGuidLightVal";
            this.CBGuidLightVal.Size = new System.Drawing.Size(120, 23);
            this.CBGuidLightVal.TabIndex = 3;
            // 
            // CBGuidLightPat
            // 
            this.CBGuidLightPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGuidLightPat.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBGuidLightPat.FormattingEnabled = true;
            this.CBGuidLightPat.Location = new System.Drawing.Point(140, 19);
            this.CBGuidLightPat.Name = "CBGuidLightPat";
            this.CBGuidLightPat.Size = new System.Drawing.Size(140, 23);
            this.CBGuidLightPat.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label12.Location = new System.Drawing.Point(12, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 15);
            this.label12.TabIndex = 1;
            this.label12.Text = "ガイドライト光量";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label11.Location = new System.Drawing.Point(12, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "ガイドライトパターン";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TBSearchV);
            this.groupBox2.Controls.Add(this.TBSearchH);
            this.groupBox2.Controls.Add(this.CBUseRC);
            this.groupBox2.Controls.Add(this.CBLightVal);
            this.groupBox2.Controls.Add(this.CBLightPat);
            this.groupBox2.Controls.Add(this.CBTilt);
            this.groupBox2.Controls.Add(this.CBSokkyoMode);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(8, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 175);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "トプコンTS用設定";
            // 
            // TBSearchV
            // 
            this.TBSearchV.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.TBSearchV.Location = new System.Drawing.Point(305, 114);
            this.TBSearchV.Name = "TBSearchV";
            this.TBSearchV.Size = new System.Drawing.Size(65, 23);
            this.TBSearchV.TabIndex = 13;
            // 
            // TBSearchH
            // 
            this.TBSearchH.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.TBSearchH.Location = new System.Drawing.Point(140, 114);
            this.TBSearchH.Name = "TBSearchH";
            this.TBSearchH.Size = new System.Drawing.Size(65, 23);
            this.TBSearchH.TabIndex = 12;
            // 
            // CBUseRC
            // 
            this.CBUseRC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBUseRC.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBUseRC.FormattingEnabled = true;
            this.CBUseRC.Location = new System.Drawing.Point(140, 143);
            this.CBUseRC.Name = "CBUseRC";
            this.CBUseRC.Size = new System.Drawing.Size(120, 23);
            this.CBUseRC.TabIndex = 11;
            // 
            // CBLightVal
            // 
            this.CBLightVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBLightVal.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBLightVal.FormattingEnabled = true;
            this.CBLightVal.Location = new System.Drawing.Point(140, 84);
            this.CBLightVal.Name = "CBLightVal";
            this.CBLightVal.Size = new System.Drawing.Size(90, 23);
            this.CBLightVal.TabIndex = 10;
            // 
            // CBLightPat
            // 
            this.CBLightPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBLightPat.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBLightPat.FormattingEnabled = true;
            this.CBLightPat.Location = new System.Drawing.Point(305, 84);
            this.CBLightPat.Name = "CBLightPat";
            this.CBLightPat.Size = new System.Drawing.Size(120, 23);
            this.CBLightPat.TabIndex = 9;
            // 
            // CBTilt
            // 
            this.CBTilt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTilt.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBTilt.FormattingEnabled = true;
            this.CBTilt.Location = new System.Drawing.Point(140, 52);
            this.CBTilt.Name = "CBTilt";
            this.CBTilt.Size = new System.Drawing.Size(160, 23);
            this.CBTilt.TabIndex = 8;
            // 
            // CBSokkyoMode
            // 
            this.CBSokkyoMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSokkyoMode.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBSokkyoMode.FormattingEnabled = true;
            this.CBSokkyoMode.Location = new System.Drawing.Point(140, 21);
            this.CBSokkyoMode.Name = "CBSokkyoMode";
            this.CBSokkyoMode.Size = new System.Drawing.Size(100, 23);
            this.CBSokkyoMode.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label10.Location = new System.Drawing.Point(12, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "リモートコントロール使用";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label9.Location = new System.Drawing.Point(235, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 15);
            this.label9.TabIndex = 5;
            this.label9.Text = "Vサーチ範囲";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label8.Location = new System.Drawing.Point(12, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "Hサーチ範囲";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label7.Location = new System.Drawing.Point(12, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "ライト光量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label6.Location = new System.Drawing.Point(235, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "ライト機能";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label5.Location = new System.Drawing.Point(12, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "チルト補正機能";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label4.Location = new System.Drawing.Point(12, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "測距モード";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TBPrismVal);
            this.groupBox1.Controls.Add(this.CBSetPrism);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "プリズム設定";
            // 
            // TBPrismVal
            // 
            this.TBPrismVal.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.TBPrismVal.Location = new System.Drawing.Point(140, 41);
            this.TBPrismVal.Name = "TBPrismVal";
            this.TBPrismVal.Size = new System.Drawing.Size(65, 23);
            this.TBPrismVal.TabIndex = 4;
            // 
            // CBSetPrism
            // 
            this.CBSetPrism.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSetPrism.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBSetPrism.FormattingEnabled = true;
            this.CBSetPrism.Location = new System.Drawing.Point(140, 15);
            this.CBSetPrism.Name = "CBSetPrism";
            this.CBSetPrism.Size = new System.Drawing.Size(220, 23);
            this.CBSetPrism.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label3.Location = new System.Drawing.Point(211, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "mm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "プリズム定数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "プリズム種別";
            // 
            // tpZumen
            // 
            this.tpZumen.Controls.Add(this.groupBox5);
            this.tpZumen.Location = new System.Drawing.Point(4, 24);
            this.tpZumen.Name = "tpZumen";
            this.tpZumen.Padding = new System.Windows.Forms.Padding(6);
            this.tpZumen.Size = new System.Drawing.Size(456, 457);
            this.tpZumen.TabIndex = 1;
            this.tpZumen.Text = "図面設定";
            this.tpZumen.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TBPaperAng);
            this.groupBox5.Controls.Add(this.CBPaperScale);
            this.groupBox5.Controls.Add(this.CBPaperSize);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox5.Location = new System.Drawing.Point(8, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(440, 120);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "用紙・縮尺設定";
            // 
            // TBPaperAng
            // 
            this.TBPaperAng.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.TBPaperAng.Location = new System.Drawing.Point(140, 78);
            this.TBPaperAng.Name = "TBPaperAng";
            this.TBPaperAng.Size = new System.Drawing.Size(65, 23);
            this.TBPaperAng.TabIndex = 6;
            // 
            // CBPaperScale
            // 
            this.CBPaperScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPaperScale.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBPaperScale.FormattingEnabled = true;
            this.CBPaperScale.Location = new System.Drawing.Point(140, 48);
            this.CBPaperScale.Name = "CBPaperScale";
            this.CBPaperScale.Size = new System.Drawing.Size(120, 23);
            this.CBPaperScale.TabIndex = 5;
            // 
            // CBPaperSize
            // 
            this.CBPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPaperSize.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.CBPaperSize.FormattingEnabled = true;
            this.CBPaperSize.Location = new System.Drawing.Point(140, 18);
            this.CBPaperSize.Name = "CBPaperSize";
            this.CBPaperSize.Size = new System.Drawing.Size(220, 23);
            this.CBPaperSize.TabIndex = 4;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label20.Location = new System.Drawing.Point(211, 81);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 15);
            this.label20.TabIndex = 3;
            this.label20.Text = "度";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label19.Location = new System.Drawing.Point(12, 81);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(71, 15);
            this.label19.TabIndex = 2;
            this.label19.Text = "用紙回転角";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label18.Location = new System.Drawing.Point(12, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 15);
            this.label18.TabIndex = 1;
            this.label18.Text = "図面縮尺";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label17.Location = new System.Drawing.Point(12, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 15);
            this.label17.TabIndex = 0;
            this.label17.Text = "用紙サイズ";
            // 
            // FormDefEnv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 531);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDefEnv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private System.Windows.Forms.Button Do_Button;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpTS;
        private System.Windows.Forms.TabPage tpZumen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CBSetPrism;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBPrismVal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CBSokkyoMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CBTilt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CBLightPat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CBLightVal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TBSearchH;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TBSearchV;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox CBUseRC;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox CBGuidLightPat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox CBGuidLightVal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox CBGPSHeight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TBGPSCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox CBi93IMU;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox CBPaperSize;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox CBPaperScale;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox TBPaperAng;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
    }
}
