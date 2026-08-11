namespace Site7DbEditor
{
    partial class UCCtrl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelCtrl = new System.Windows.Forms.Panel();
            this.panelBottomCoords = new System.Windows.Forms.Panel();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabTS = new System.Windows.Forms.TabPage();
            this.labelStatus = new System.Windows.Forms.Label();
            this.Mirrorkou1 = new System.Windows.Forms.TextBox();
            this.Kikaikou1 = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.cBoxTS = new System.Windows.Forms.ComboBox();
            this.btnLight = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSijun = new System.Windows.Forms.Button();
            this.btnAutoTsuibi = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnLeft2 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnRight2 = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnDefKikaiBack = new System.Windows.Forms.Button();
            this.SelKikaiTenBackTenBtn1 = new System.Windows.Forms.Button();
            this.tabGPS = new System.Windows.Forms.TabPage();
            this.cBoxKei = new System.Windows.Forms.ComboBox();
            this.labelStatus2 = new System.Windows.Forms.Label();
            this.cBoxGPSStatus = new System.Windows.Forms.ComboBox();
            this.cBoxGPS = new System.Windows.Forms.ComboBox();
            this.textBoxKikaikou2 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.labelGPS2 = new System.Windows.Forms.Label();
            this.labelGPS1 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnConnect2 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.btnUpdPos = new System.Windows.Forms.Button();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chbContMeasure = new System.Windows.Forms.CheckBox();
            this.chkAutoSet = new System.Windows.Forms.CheckBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelCtrl.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tabGPS.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottomCoords
            // 
            this.panelBottomCoords.Controls.Add(this.btnUpdPos);
            this.panelBottomCoords.Controls.Add(this.textBoxX);
            this.panelBottomCoords.Controls.Add(this.label18);
            this.panelBottomCoords.Controls.Add(this.textBoxY);
            this.panelBottomCoords.Controls.Add(this.label11);
            this.panelBottomCoords.Controls.Add(this.textBoxZ);
            this.panelBottomCoords.Controls.Add(this.label10);
            this.panelBottomCoords.Controls.Add(this.chbContMeasure);
            this.panelBottomCoords.Controls.Add(this.chkAutoSet);
            this.panelBottomCoords.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomCoords.Location = new System.Drawing.Point(0, 258);
            this.panelBottomCoords.Name = "panelBottomCoords";
            this.panelBottomCoords.Size = new System.Drawing.Size(254, 96);
            this.panelBottomCoords.TabIndex = 175;
            // 
            // panelCtrl
            // 
            this.panelCtrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCtrl.Controls.Add(this.tabControl4);
            this.panelCtrl.Controls.Add(this.panelBottomCoords);
            this.panelCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCtrl.Location = new System.Drawing.Point(0, 0);
            this.panelCtrl.Name = "panelCtrl";
            this.panelCtrl.Size = new System.Drawing.Size(254, 354);
            this.panelCtrl.TabIndex = 172;
            // 
            // tabControl4
            // 
            this.tabControl4.Alignment = System.Windows.Forms.TabAlignment.Top;
            this.tabControl4.Controls.Add(this.tabTS);
            this.tabControl4.Controls.Add(this.tabGPS);
            this.tabControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tabControl4.Location = new System.Drawing.Point(0, 0);
            this.tabControl4.Multiline = true;
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(254, 258);
            this.tabControl4.TabIndex = 170;
            this.tabControl4.Tag = "3";
            this.tabControl4.SelectedIndexChanged += new System.EventHandler(this.tabControl4_SelectedIndexChanged);
            // 
            // tabTS
            // 
            this.tabTS.Controls.Add(this.cBoxTS);
            this.tabTS.Controls.Add(this.comboBox1);
            this.tabTS.Controls.Add(this.btnConnect);
            this.tabTS.Controls.Add(this.btnDefKikaiBack);
            this.tabTS.Controls.Add(this.Label12);
            this.tabTS.Controls.Add(this.Kikaikou1);
            this.tabTS.Controls.Add(this.Label14);
            this.tabTS.Controls.Add(this.SelKikaiTenBackTenBtn1);
            this.tabTS.Controls.Add(this.Label13);
            this.tabTS.Controls.Add(this.Mirrorkou1);
            this.tabTS.Controls.Add(this.Label16);
            this.tabTS.Controls.Add(this.btnLight);
            this.tabTS.Controls.Add(this.btnSearch);
            this.tabTS.Controls.Add(this.btnUp);
            this.tabTS.Controls.Add(this.btnAutoTsuibi);
            this.tabTS.Controls.Add(this.btnSijun);
            this.tabTS.Controls.Add(this.btnLeft2);
            this.tabTS.Controls.Add(this.btnLeft);
            this.tabTS.Controls.Add(this.btnStop);
            this.tabTS.Controls.Add(this.btnRight);
            this.tabTS.Controls.Add(this.btnRight2);
            this.tabTS.Controls.Add(this.trackBar1);
            this.tabTS.Controls.Add(this.btnDown);
            this.tabTS.Controls.Add(this.labelStatus);
            this.tabTS.Location = new System.Drawing.Point(4, 26);
            this.tabTS.Name = "tabTS";
            this.tabTS.Padding = new System.Windows.Forms.Padding(3);
            this.tabTS.Size = new System.Drawing.Size(240, 228);
            this.tabTS.TabIndex = 2;
            this.tabTS.Text = "TS";
            this.tabTS.UseVisualStyleBackColor = true;
            // 
            // cBoxTS
            // 
            this.cBoxTS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxTS.FormattingEnabled = true;
            this.cBoxTS.Items.AddRange(new object[] {
            "LN100/150",
            "PS-A GT100 MS(自動追尾)",
            "DS-AC PS-AC GT-500(自動視準)",
            "OS ES GM(手動視準)"});
            this.cBoxTS.Location = new System.Drawing.Point(2, 4);
            this.cBoxTS.Name = "cBoxTS";
            this.cBoxTS.Size = new System.Drawing.Size(210, 24);
            this.cBoxTS.TabIndex = 182;
            this.cBoxTS.SelectionChangeCommitted += new System.EventHandler(this.cBoxTS_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(2, 34);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(156, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnConnect.Location = new System.Drawing.Point(160, 33);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(52, 26);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Tag = "1";
            this.btnConnect.Text = "接続";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDefKikaiBack
            // 
            this.btnDefKikaiBack.BackColor = System.Drawing.SystemColors.Control;
            this.btnDefKikaiBack.Font = new System.Drawing.Font("MS UI Gothic", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDefKikaiBack.Location = new System.Drawing.Point(2, 64);
            this.btnDefKikaiBack.Name = "btnDefKikaiBack";
            this.btnDefKikaiBack.Size = new System.Drawing.Size(83, 28);
            this.btnDefKikaiBack.TabIndex = 168;
            this.btnDefKikaiBack.Text = "器械点設定";
            this.btnDefKikaiBack.UseVisualStyleBackColor = true;
            this.btnDefKikaiBack.Click += new System.EventHandler(this.btnDefKikaiBack_Click);
            // 
            // Label12
            // 
            this.Label12.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label12.Location = new System.Drawing.Point(87, 70);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(46, 16);
            this.Label12.TabIndex = 188;
            this.Label12.Text = "器械高";
            // 
            // Kikaikou1
            // 
            this.Kikaikou1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Kikaikou1.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Kikaikou1.Location = new System.Drawing.Point(134, 64);
            this.Kikaikou1.Name = "Kikaikou1";
            this.Kikaikou1.Size = new System.Drawing.Size(60, 28);
            this.Kikaikou1.TabIndex = 183;
            this.Kikaikou1.Tag = "1";
            this.Kikaikou1.Text = "1.500";
            this.Kikaikou1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Kikaikou1.TextChanged += new System.EventHandler(this.Kikaikou1_TextChanged);
            // 
            // Label14
            // 
            this.Label14.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label14.Location = new System.Drawing.Point(196, 70);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(20, 16);
            this.Label14.TabIndex = 186;
            this.Label14.Text = "ｍ";
            // 
            // SelKikaiTenBackTenBtn1
            // 
            this.SelKikaiTenBackTenBtn1.BackColor = System.Drawing.SystemColors.Control;
            this.SelKikaiTenBackTenBtn1.Enabled = false;
            this.SelKikaiTenBackTenBtn1.Font = new System.Drawing.Font("MS UI Gothic", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SelKikaiTenBackTenBtn1.Location = new System.Drawing.Point(2, 95);
            this.SelKikaiTenBackTenBtn1.Name = "SelKikaiTenBackTenBtn1";
            this.SelKikaiTenBackTenBtn1.Size = new System.Drawing.Size(83, 28);
            this.SelKikaiTenBackTenBtn1.TabIndex = 168;
            this.SelKikaiTenBackTenBtn1.Text = "器械点測定";
            this.SelKikaiTenBackTenBtn1.UseVisualStyleBackColor = true;
            this.SelKikaiTenBackTenBtn1.Click += new System.EventHandler(this.SelKikaiTenBackTenBtn1_Click);
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label13.Location = new System.Drawing.Point(87, 101);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(48, 15);
            this.Label13.TabIndex = 187;
            this.Label13.Text = "ミラー高";
            // 
            // Mirrorkou1
            // 
            this.Mirrorkou1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Mirrorkou1.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Mirrorkou1.Location = new System.Drawing.Point(134, 95);
            this.Mirrorkou1.Name = "Mirrorkou1";
            this.Mirrorkou1.Size = new System.Drawing.Size(60, 28);
            this.Mirrorkou1.TabIndex = 184;
            this.Mirrorkou1.Tag = "2";
            this.Mirrorkou1.Text = "1.200";
            this.Mirrorkou1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Mirrorkou1.TextChanged += new System.EventHandler(this.Kikaikou1_TextChanged);
            // 
            // Label16
            // 
            this.Label16.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label16.Location = new System.Drawing.Point(196, 101);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(20, 16);
            this.Label16.TabIndex = 185;
            this.Label16.Text = "ｍ";
            // 
            // btnLight
            // 
            this.btnLight.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnLight.Location = new System.Drawing.Point(1, 126);
            this.btnLight.Name = "btnLight";
            this.btnLight.Size = new System.Drawing.Size(41, 32);
            this.btnLight.TabIndex = 170;
            this.btnLight.Tag = "0";
            this.btnLight.Text = "消灯";
            this.btnLight.UseVisualStyleBackColor = true;
            this.btnLight.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearch.Location = new System.Drawing.Point(43, 126);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(41, 32);
            this.btnSearch.TabIndex = 171;
            this.btnSearch.Tag = "2";
            this.btnSearch.Text = "G";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            // 
            // btnUp
            // 
            this.btnUp.Enabled = false;
            this.btnUp.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUp.Location = new System.Drawing.Point(85, 126);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(41, 32);
            this.btnUp.TabIndex = 172;
            this.btnUp.Tag = "9";
            this.btnUp.Text = "△";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // btnAutoTsuibi
            // 
            this.btnAutoTsuibi.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAutoTsuibi.Location = new System.Drawing.Point(127, 126);
            this.btnAutoTsuibi.Name = "btnAutoTsuibi";
            this.btnAutoTsuibi.Size = new System.Drawing.Size(72, 32);
            this.btnAutoTsuibi.TabIndex = 181;
            this.btnAutoTsuibi.Tag = "3";
            this.btnAutoTsuibi.Text = "自動追尾";
            this.btnAutoTsuibi.UseVisualStyleBackColor = true;
            this.btnAutoTsuibi.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            // 
            // btnSijun
            // 
            this.btnSijun.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSijun.Location = new System.Drawing.Point(200, 126);
            this.btnSijun.Name = "btnSijun";
            this.btnSijun.Size = new System.Drawing.Size(18, 32);
            this.btnSijun.TabIndex = 181;
            this.btnSijun.Tag = "12";
            this.btnSijun.Text = "▶";
            this.btnSijun.UseVisualStyleBackColor = true;
            this.btnSijun.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            // 
            // btnLeft2
            // 
            this.btnLeft2.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnLeft2.Location = new System.Drawing.Point(1, 160);
            this.btnLeft2.Name = "btnLeft2";
            this.btnLeft2.Size = new System.Drawing.Size(41, 32);
            this.btnLeft2.TabIndex = 173;
            this.btnLeft2.Tag = "5";
            this.btnLeft2.Text = "<<";
            this.btnLeft2.UseVisualStyleBackColor = true;
            this.btnLeft2.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            this.btnLeft2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnLeft2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // btnLeft
            // 
            this.btnLeft.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnLeft.Location = new System.Drawing.Point(43, 160);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(41, 32);
            this.btnLeft.TabIndex = 175;
            this.btnLeft.Tag = "6";
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStop.Location = new System.Drawing.Point(87, 160);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(34, 32);
            this.btnStop.TabIndex = 177;
            this.btnStop.Tag = "11";
            this.btnStop.Text = "■";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            // 
            // btnRight
            // 
            this.btnRight.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRight.Location = new System.Drawing.Point(124, 160);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(41, 32);
            this.btnRight.TabIndex = 176;
            this.btnRight.Tag = "7";
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // btnRight2
            // 
            this.btnRight2.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRight2.Location = new System.Drawing.Point(167, 160);
            this.btnRight2.Name = "btnRight2";
            this.btnRight2.Size = new System.Drawing.Size(41, 32);
            this.btnRight2.TabIndex = 174;
            this.btnRight2.Tag = "8";
            this.btnRight2.Text = ">>";
            this.btnRight2.UseVisualStyleBackColor = true;
            this.btnRight2.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            this.btnRight2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnRight2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(-1, 194);
            this.trackBar1.Maximum = 5;
            this.trackBar1.Minimum = 2;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(80, 32);
            this.trackBar1.TabIndex = 178;
            this.trackBar1.Value = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btnDown
            // 
            this.btnDown.Enabled = false;
            this.btnDown.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDown.Location = new System.Drawing.Point(85, 194);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(41, 32);
            this.btnDown.TabIndex = 179;
            this.btnDown.Tag = "10";
            this.btnDown.Text = "▽";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnAutoTsuibi_Click);
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(130, 202);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(67, 14);
            this.labelStatus.TabIndex = 189;
            this.labelStatus.Text = "TSの状況";
            // 
            // tabGPS
            // 
            this.tabGPS.Controls.Add(this.cBoxGPS);
            this.tabGPS.Controls.Add(this.label27);
            this.tabGPS.Controls.Add(this.cBoxKei);
            this.tabGPS.Controls.Add(this.comboBox2);
            this.tabGPS.Controls.Add(this.btnConnect2);
            this.tabGPS.Controls.Add(this.label20);
            this.tabGPS.Controls.Add(this.cBoxGPSStatus);
            this.tabGPS.Controls.Add(this.label24);
            this.tabGPS.Controls.Add(this.textBoxKikaikou2);
            this.tabGPS.Controls.Add(this.label25);
            this.tabGPS.Controls.Add(this.labelStatus2);
            this.tabGPS.Controls.Add(this.labelGPS1);
            this.tabGPS.Controls.Add(this.labelGPS2);
            this.tabGPS.Location = new System.Drawing.Point(4, 26);
            this.tabGPS.Name = "tabGPS";
            this.tabGPS.Padding = new System.Windows.Forms.Padding(3);
            this.tabGPS.Size = new System.Drawing.Size(240, 228);
            this.tabGPS.TabIndex = 3;
            this.tabGPS.Text = "GPS";
            this.tabGPS.UseVisualStyleBackColor = true;
            // 
            // cBoxGPS
            // 
            this.cBoxGPS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxGPS.FormattingEnabled = true;
            this.cBoxGPS.Items.AddRange(new object[] {
            "ポチスタ",
            "i93"});
            this.cBoxGPS.Location = new System.Drawing.Point(2, 4);
            this.cBoxGPS.Name = "cBoxGPS";
            this.cBoxGPS.Size = new System.Drawing.Size(86, 24);
            this.cBoxGPS.TabIndex = 193;
            this.cBoxGPS.SelectedIndexChanged += new System.EventHandler(this.cBoxGPS_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label27.Location = new System.Drawing.Point(90, 8);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(78, 16);
            this.label27.TabIndex = 191;
            this.label27.Text = "直角座標系:";
            // 
            // cBoxKei
            // 
            this.cBoxKei.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxKei.FormattingEnabled = true;
            this.cBoxKei.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19"});
            this.cBoxKei.Location = new System.Drawing.Point(168, 4);
            this.cBoxKei.Name = "cBoxKei";
            this.cBoxKei.Size = new System.Drawing.Size(38, 24);
            this.cBoxKei.TabIndex = 196;
            this.cBoxKei.SelectedIndexChanged += new System.EventHandler(this.cBoxKei_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(2, 34);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(156, 24);
            this.comboBox2.TabIndex = 161;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // btnConnect2
            // 
            this.btnConnect2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnConnect2.Location = new System.Drawing.Point(160, 33);
            this.btnConnect2.Name = "btnConnect2";
            this.btnConnect2.Size = new System.Drawing.Size(52, 26);
            this.btnConnect2.TabIndex = 162;
            this.btnConnect2.Tag = "2";
            this.btnConnect2.Text = "接続";
            this.btnConnect2.UseVisualStyleBackColor = true;
            this.btnConnect2.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.Location = new System.Drawing.Point(2, 68);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(112, 16);
            this.label20.TabIndex = 160;
            this.label20.Text = "有効GPSステータス";
            // 
            // cBoxGPSStatus
            // 
            this.cBoxGPSStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxGPSStatus.FormattingEnabled = true;
            this.cBoxGPSStatus.Items.AddRange(new object[] {
            "RTK-fix",
            "RTK-float",
            "DGPS-fix",
            "単独測位"});
            this.cBoxGPSStatus.Location = new System.Drawing.Point(116, 64);
            this.cBoxGPSStatus.Name = "cBoxGPSStatus";
            this.cBoxGPSStatus.Size = new System.Drawing.Size(92, 24);
            this.cBoxGPSStatus.TabIndex = 194;
            this.cBoxGPSStatus.SelectedIndexChanged += new System.EventHandler(this.cBoxGPSStatus_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label24.Location = new System.Drawing.Point(2, 100);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(56, 16);
            this.label24.TabIndex = 191;
            this.label24.Text = "器械高";
            // 
            // textBoxKikaikou2
            // 
            this.textBoxKikaikou2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBoxKikaikou2.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxKikaikou2.Location = new System.Drawing.Point(60, 94);
            this.textBoxKikaikou2.Name = "textBoxKikaikou2";
            this.textBoxKikaikou2.Size = new System.Drawing.Size(64, 29);
            this.textBoxKikaikou2.TabIndex = 189;
            this.textBoxKikaikou2.Tag = "3";
            this.textBoxKikaikou2.Text = "1.500";
            this.textBoxKikaikou2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxKikaikou2.TextChanged += new System.EventHandler(this.Kikaikou1_TextChanged);
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label25.Location = new System.Drawing.Point(128, 100);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(24, 16);
            this.label25.TabIndex = 190;
            this.label25.Text = "ｍ";
            // 
            // labelStatus2
            // 
            this.labelStatus2.AutoSize = true;
            this.labelStatus2.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelStatus2.ForeColor = System.Drawing.Color.Red;
            this.labelStatus2.Location = new System.Drawing.Point(2, 132);
            this.labelStatus2.Name = "labelStatus2";
            this.labelStatus2.Size = new System.Drawing.Size(74, 14);
            this.labelStatus2.TabIndex = 195;
            this.labelStatus2.Text = "GPSの状況";
            // 
            // labelGPS1
            // 
            this.labelGPS1.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelGPS1.Location = new System.Drawing.Point(2, 154);
            this.labelGPS1.Name = "labelGPS1";
            this.labelGPS1.Size = new System.Drawing.Size(231, 19);
            this.labelGPS1.TabIndex = 191;
            this.labelGPS1.Text = "取得状況：";
            // 
            // labelGPS2
            // 
            this.labelGPS2.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelGPS2.Location = new System.Drawing.Point(2, 176);
            this.labelGPS2.Name = "labelGPS2";
            this.labelGPS2.Size = new System.Drawing.Size(236, 19);
            this.labelGPS2.TabIndex = 191;
            this.labelGPS2.Text = "HDOP: 衛星数：";
            // 
            // btnUpdPos
            // 
            this.btnUpdPos.Font = new System.Drawing.Font("MS UI Gothic", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdPos.Location = new System.Drawing.Point(149, 25);
            this.btnUpdPos.Name = "btnUpdPos";
            this.btnUpdPos.Size = new System.Drawing.Size(48, 36);
            this.btnUpdPos.TabIndex = 164;
            this.btnUpdPos.Text = "↓";
            this.btnUpdPos.UseVisualStyleBackColor = true;
            this.btnUpdPos.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBoxX
            // 
            this.textBoxX.Enabled = false;
            this.textBoxX.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxX.Location = new System.Drawing.Point(41, 4);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(103, 29);
            this.textBoxX.TabIndex = 3;
            this.textBoxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(2, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 16);
            this.label10.TabIndex = 161;
            this.label10.Text = "Z座標";
            // 
            // textBoxZ
            // 
            this.textBoxZ.Enabled = false;
            this.textBoxZ.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxZ.Location = new System.Drawing.Point(41, 60);
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.Size = new System.Drawing.Size(103, 29);
            this.textBoxZ.TabIndex = 5;
            this.textBoxZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(2, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 16);
            this.label11.TabIndex = 162;
            this.label11.Text = "Y座標";
            // 
            // chbContMeasure
            // 
            this.chbContMeasure.AutoSize = true;
            this.chbContMeasure.Checked = true;
            this.chbContMeasure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbContMeasure.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chbContMeasure.Location = new System.Drawing.Point(147, 6);
            this.chbContMeasure.Name = "chbContMeasure";
            this.chbContMeasure.Size = new System.Drawing.Size(86, 18);
            this.chbContMeasure.TabIndex = 173;
            this.chbContMeasure.Text = "連続測定";
            this.chbContMeasure.UseVisualStyleBackColor = true;
            this.chbContMeasure.CheckedChanged += new System.EventHandler(this.chbContMeasure_CheckedChanged);
            // 
            // chkAutoSet
            // 
            this.chkAutoSet.AutoSize = true;
            this.chkAutoSet.Checked = true;
            this.chkAutoSet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoSet.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAutoSet.Location = new System.Drawing.Point(147, 64);
            this.chkAutoSet.Name = "chkAutoSet";
            this.chkAutoSet.Size = new System.Drawing.Size(79, 18);
            this.chkAutoSet.TabIndex = 173;
            this.chkAutoSet.Text = "自動Set";
            this.chkAutoSet.UseVisualStyleBackColor = true;
            // 
            // textBoxY
            // 
            this.textBoxY.Enabled = false;
            this.textBoxY.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxY.Location = new System.Drawing.Point(41, 32);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(103, 29);
            this.textBoxY.TabIndex = 4;
            this.textBoxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(2, 10);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 16);
            this.label18.TabIndex = 163;
            this.label18.Text = "X座標";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UCCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelCtrl);
            this.Name = "UCCtrl";
            this.Size = new System.Drawing.Size(250, 406);
            this.panelCtrl.ResumeLayout(false);
            this.panelCtrl.PerformLayout();
            this.tabControl4.ResumeLayout(false);
            this.tabTS.ResumeLayout(false);
            this.tabTS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tabGPS.ResumeLayout(false);
            this.tabGPS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCtrl;
        private System.Windows.Forms.Panel panelBottomCoords;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabTS;
        private System.Windows.Forms.Label labelStatus;
        public System.Windows.Forms.TextBox Mirrorkou1;
        public System.Windows.Forms.TextBox Kikaikou1;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        private System.Windows.Forms.ComboBox cBoxTS;
        private System.Windows.Forms.Button btnLight;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSijun;
        private System.Windows.Forms.Button btnAutoTsuibi;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnLeft2;
        private System.Windows.Forms.Button btnRight2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox comboBox1;
        internal System.Windows.Forms.Button btnDefKikaiBack;
        internal System.Windows.Forms.Button SelKikaiTenBackTenBtn1;
        private System.Windows.Forms.TabPage tabGPS;
        private System.Windows.Forms.ComboBox cBoxKei;
        private System.Windows.Forms.Label labelStatus2;
        private System.Windows.Forms.ComboBox cBoxGPSStatus;
        private System.Windows.Forms.ComboBox cBoxGPS;
        public System.Windows.Forms.TextBox textBoxKikaikou2;
        internal System.Windows.Forms.Label label27;
        internal System.Windows.Forms.Label labelGPS2;
        internal System.Windows.Forms.Label labelGPS1;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnConnect2;
        private System.Windows.Forms.ComboBox comboBox2;
        internal System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnUpdPos;
        private System.Windows.Forms.TextBox textBoxX;
        internal System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxZ;
        internal System.Windows.Forms.Label label11;
        public System.Windows.Forms.CheckBox chkAutoSet;
        private System.Windows.Forms.TextBox textBoxY;
        internal System.Windows.Forms.Label label18;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar1;
        public System.Windows.Forms.CheckBox chbContMeasure;
    }
}
