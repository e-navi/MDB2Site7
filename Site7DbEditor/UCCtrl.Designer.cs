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

        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            panelCtrl = new Panel();
            tabControl4 = new TabControl();
            tabTS = new TabPage();
            cBoxTS = new ComboBox();
            comboBox1 = new ComboBox();
            btnConnect = new Button();
            btnDefKikaiBack = new Button();
            Label12 = new Label();
            Kikaikou1 = new TextBox();
            Label14 = new Label();
            SelKikaiTenBackTenBtn1 = new Button();
            Label13 = new Label();
            Mirrorkou1 = new TextBox();
            Label16 = new Label();
            btnLight = new Button();
            btnSearch = new Button();
            btnUp = new Button();
            btnAutoTsuibi = new Button();
            btnSijun = new Button();
            btnLeft2 = new Button();
            btnLeft = new Button();
            btnStop = new Button();
            btnRight = new Button();
            btnRight2 = new Button();
            trackBar1 = new TrackBar();
            btnDown = new Button();
            labelStatus = new Label();
            tabGPS = new TabPage();
            cBoxGPS = new ComboBox();
            label27 = new Label();
            cBoxKei = new ComboBox();
            comboBox2 = new ComboBox();
            btnConnect2 = new Button();
            label20 = new Label();
            cBoxGPSStatus = new ComboBox();
            label24 = new Label();
            textBoxKikaikou2 = new TextBox();
            label25 = new Label();
            labelStatus2 = new Label();
            labelGPS1 = new Label();
            labelGPS2 = new Label();
            panelBottomCoords = new Panel();
            btnUpdPos = new Button();
            textBoxX = new TextBox();
            label18 = new Label();
            textBoxY = new TextBox();
            label11 = new Label();
            textBoxZ = new TextBox();
            label10 = new Label();
            chbContMeasure = new CheckBox();
            chkAutoSet = new CheckBox();
            timer1 = new System.Windows.Forms.Timer(components);
            panelCtrl.SuspendLayout();
            tabControl4.SuspendLayout();
            tabTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            tabGPS.SuspendLayout();
            panelBottomCoords.SuspendLayout();
            SuspendLayout();
            // 
            // panelCtrl
            // 
            panelCtrl.BorderStyle = BorderStyle.FixedSingle;
            panelCtrl.Controls.Add(panelBottomCoords);
            panelCtrl.Controls.Add(tabControl4);
            panelCtrl.Dock = DockStyle.Fill;
            panelCtrl.Location = new Point(0, 0);
            panelCtrl.Margin = new Padding(4);
            panelCtrl.Name = "panelCtrl";
            panelCtrl.Size = new Size(292, 508);
            panelCtrl.TabIndex = 172;
            // 
            // tabControl4
            // 
            tabControl4.Controls.Add(tabTS);
            tabControl4.Controls.Add(tabGPS);
            tabControl4.Dock = DockStyle.Top;
            tabControl4.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            tabControl4.Location = new Point(0, 0);
            tabControl4.Margin = new Padding(4);
            tabControl4.Multiline = true;
            tabControl4.Name = "tabControl4";
            tabControl4.SelectedIndex = 0;
            tabControl4.Size = new Size(290, 322);
            tabControl4.TabIndex = 170;
            tabControl4.Tag = "3";
            tabControl4.SelectedIndexChanged += tabControl4_SelectedIndexChanged;
            // 
            // tabTS
            // 
            tabTS.Controls.Add(cBoxTS);
            tabTS.Controls.Add(comboBox1);
            tabTS.Controls.Add(btnConnect);
            tabTS.Controls.Add(btnDefKikaiBack);
            tabTS.Controls.Add(Label12);
            tabTS.Controls.Add(Kikaikou1);
            tabTS.Controls.Add(Label14);
            tabTS.Controls.Add(SelKikaiTenBackTenBtn1);
            tabTS.Controls.Add(Label13);
            tabTS.Controls.Add(Mirrorkou1);
            tabTS.Controls.Add(Label16);
            tabTS.Controls.Add(btnLight);
            tabTS.Controls.Add(btnSearch);
            tabTS.Controls.Add(btnUp);
            tabTS.Controls.Add(btnAutoTsuibi);
            tabTS.Controls.Add(btnSijun);
            tabTS.Controls.Add(btnLeft2);
            tabTS.Controls.Add(btnLeft);
            tabTS.Controls.Add(btnStop);
            tabTS.Controls.Add(btnRight);
            tabTS.Controls.Add(btnRight2);
            tabTS.Controls.Add(trackBar1);
            tabTS.Controls.Add(btnDown);
            tabTS.Controls.Add(labelStatus);
            tabTS.Location = new Point(4, 23);
            tabTS.Margin = new Padding(4);
            tabTS.Name = "tabTS";
            tabTS.Padding = new Padding(4);
            tabTS.Size = new Size(282, 295);
            tabTS.TabIndex = 2;
            tabTS.Text = "TS";
            tabTS.UseVisualStyleBackColor = true;
            // 
            // cBoxTS
            // 
            cBoxTS.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxTS.FormattingEnabled = true;
            cBoxTS.Items.AddRange(new object[] { "LN100/150", "PS-A GT100 MS(自動追尾)", "DS-AC PS-AC GT-500(自動視準)", "OS ES GM(手動視準)" });
            cBoxTS.Location = new Point(2, 5);
            cBoxTS.Margin = new Padding(4);
            cBoxTS.Name = "cBoxTS";
            cBoxTS.Size = new Size(244, 21);
            cBoxTS.TabIndex = 182;
            cBoxTS.SelectionChangeCommitted += cBoxTS_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("MS UI Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(2, 42);
            comboBox1.Margin = new Padding(4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(191, 21);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnConnect
            // 
            btnConnect.Font = new Font("MS UI Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnConnect.Location = new Point(195, 34);
            btnConnect.Margin = new Padding(4);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(51, 36);
            btnConnect.TabIndex = 1;
            btnConnect.Tag = "1";
            btnConnect.Text = "接続";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDefKikaiBack
            // 
            btnDefKikaiBack.BackColor = SystemColors.Control;
            btnDefKikaiBack.Font = new Font("MS UI Gothic", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnDefKikaiBack.Location = new Point(2, 80);
            btnDefKikaiBack.Margin = new Padding(4);
            btnDefKikaiBack.Name = "btnDefKikaiBack";
            btnDefKikaiBack.Size = new Size(97, 35);
            btnDefKikaiBack.TabIndex = 168;
            btnDefKikaiBack.Text = "器械点設定";
            btnDefKikaiBack.UseVisualStyleBackColor = true;
            btnDefKikaiBack.Click += btnDefKikaiBack_Click;
            // 
            // Label12
            // 
            Label12.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            Label12.Location = new Point(102, 88);
            Label12.Margin = new Padding(4, 0, 4, 0);
            Label12.Name = "Label12";
            Label12.Size = new Size(54, 20);
            Label12.TabIndex = 188;
            Label12.Text = "器械高";
            // 
            // Kikaikou1
            // 
            Kikaikou1.BackColor = Color.FromArgb(255, 255, 128);
            Kikaikou1.Font = new Font("MS UI Gothic", 16F, FontStyle.Bold, GraphicsUnit.Point, 128);
            Kikaikou1.Location = new Point(156, 80);
            Kikaikou1.Margin = new Padding(4);
            Kikaikou1.Name = "Kikaikou1";
            Kikaikou1.Size = new Size(69, 29);
            Kikaikou1.TabIndex = 183;
            Kikaikou1.Tag = "1";
            Kikaikou1.Text = "1.500";
            Kikaikou1.TextAlign = HorizontalAlignment.Right;
            Kikaikou1.TextChanged += Kikaikou1_TextChanged;
            // 
            // Label14
            // 
            Label14.Font = new Font("MS UI Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            Label14.Location = new Point(229, 88);
            Label14.Margin = new Padding(4, 0, 4, 0);
            Label14.Name = "Label14";
            Label14.Size = new Size(23, 20);
            Label14.TabIndex = 186;
            Label14.Text = "ｍ";
            // 
            // SelKikaiTenBackTenBtn1
            // 
            SelKikaiTenBackTenBtn1.BackColor = SystemColors.Control;
            SelKikaiTenBackTenBtn1.Enabled = false;
            SelKikaiTenBackTenBtn1.Font = new Font("MS UI Gothic", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 128);
            SelKikaiTenBackTenBtn1.Location = new Point(2, 119);
            SelKikaiTenBackTenBtn1.Margin = new Padding(4);
            SelKikaiTenBackTenBtn1.Name = "SelKikaiTenBackTenBtn1";
            SelKikaiTenBackTenBtn1.Size = new Size(97, 35);
            SelKikaiTenBackTenBtn1.TabIndex = 168;
            SelKikaiTenBackTenBtn1.Text = "器械点測定";
            SelKikaiTenBackTenBtn1.UseVisualStyleBackColor = true;
            SelKikaiTenBackTenBtn1.Click += SelKikaiTenBackTenBtn1_Click;
            // 
            // Label13
            // 
            Label13.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            Label13.Location = new Point(102, 126);
            Label13.Margin = new Padding(4, 0, 4, 0);
            Label13.Name = "Label13";
            Label13.Size = new Size(56, 19);
            Label13.TabIndex = 187;
            Label13.Text = "ミラー高";
            // 
            // Mirrorkou1
            // 
            Mirrorkou1.BackColor = Color.FromArgb(255, 255, 128);
            Mirrorkou1.Font = new Font("MS UI Gothic", 16F, FontStyle.Bold, GraphicsUnit.Point, 128);
            Mirrorkou1.Location = new Point(156, 119);
            Mirrorkou1.Margin = new Padding(4);
            Mirrorkou1.Name = "Mirrorkou1";
            Mirrorkou1.Size = new Size(69, 29);
            Mirrorkou1.TabIndex = 184;
            Mirrorkou1.Tag = "2";
            Mirrorkou1.Text = "1.200";
            Mirrorkou1.TextAlign = HorizontalAlignment.Right;
            Mirrorkou1.TextChanged += Kikaikou1_TextChanged;
            // 
            // Label16
            // 
            Label16.Font = new Font("MS UI Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            Label16.Location = new Point(229, 126);
            Label16.Margin = new Padding(4, 0, 4, 0);
            Label16.Name = "Label16";
            Label16.Size = new Size(23, 20);
            Label16.TabIndex = 185;
            Label16.Text = "ｍ";
            // 
            // btnLight
            // 
            btnLight.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnLight.Location = new Point(1, 158);
            btnLight.Margin = new Padding(4);
            btnLight.Name = "btnLight";
            btnLight.Size = new Size(48, 40);
            btnLight.TabIndex = 170;
            btnLight.Tag = "0";
            btnLight.Text = "消灯";
            btnLight.UseVisualStyleBackColor = true;
            btnLight.Click += btnAutoTsuibi_Click;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnSearch.Location = new Point(50, 158);
            btnSearch.Margin = new Padding(4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(48, 40);
            btnSearch.TabIndex = 171;
            btnSearch.Tag = "2";
            btnSearch.Text = "G";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnAutoTsuibi_Click;
            // 
            // btnUp
            // 
            btnUp.Enabled = false;
            btnUp.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnUp.Location = new Point(99, 158);
            btnUp.Margin = new Padding(4);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(48, 40);
            btnUp.TabIndex = 172;
            btnUp.Tag = "9";
            btnUp.Text = "△";
            btnUp.UseVisualStyleBackColor = true;
            btnUp.Click += btnAutoTsuibi_Click;
            btnUp.MouseDown += btnUp_MouseDown;
            btnUp.MouseUp += btnUp_MouseUp;
            // 
            // btnAutoTsuibi
            // 
            btnAutoTsuibi.Font = new Font("MS UI Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnAutoTsuibi.Location = new Point(148, 158);
            btnAutoTsuibi.Margin = new Padding(4);
            btnAutoTsuibi.Name = "btnAutoTsuibi";
            btnAutoTsuibi.Size = new Size(84, 40);
            btnAutoTsuibi.TabIndex = 181;
            btnAutoTsuibi.Tag = "3";
            btnAutoTsuibi.Text = "自動追尾";
            btnAutoTsuibi.UseVisualStyleBackColor = true;
            btnAutoTsuibi.Click += btnAutoTsuibi_Click;
            // 
            // btnSijun
            // 
            btnSijun.Font = new Font("MS UI Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnSijun.Location = new Point(233, 158);
            btnSijun.Margin = new Padding(4);
            btnSijun.Name = "btnSijun";
            btnSijun.Size = new Size(21, 40);
            btnSijun.TabIndex = 181;
            btnSijun.Tag = "12";
            btnSijun.Text = "▶";
            btnSijun.UseVisualStyleBackColor = true;
            btnSijun.Click += btnAutoTsuibi_Click;
            // 
            // btnLeft2
            // 
            btnLeft2.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnLeft2.Location = new Point(1, 200);
            btnLeft2.Margin = new Padding(4);
            btnLeft2.Name = "btnLeft2";
            btnLeft2.Size = new Size(48, 40);
            btnLeft2.TabIndex = 173;
            btnLeft2.Tag = "5";
            btnLeft2.Text = "<<";
            btnLeft2.UseVisualStyleBackColor = true;
            btnLeft2.Click += btnAutoTsuibi_Click;
            btnLeft2.MouseDown += btnUp_MouseDown;
            btnLeft2.MouseUp += btnUp_MouseUp;
            // 
            // btnLeft
            // 
            btnLeft.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnLeft.Location = new Point(50, 200);
            btnLeft.Margin = new Padding(4);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(48, 40);
            btnLeft.TabIndex = 175;
            btnLeft.Tag = "6";
            btnLeft.Text = "<";
            btnLeft.UseVisualStyleBackColor = true;
            btnLeft.Click += btnAutoTsuibi_Click;
            btnLeft.MouseDown += btnUp_MouseDown;
            btnLeft.MouseUp += btnUp_MouseUp;
            // 
            // btnStop
            // 
            btnStop.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnStop.Location = new Point(102, 200);
            btnStop.Margin = new Padding(4);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(40, 40);
            btnStop.TabIndex = 177;
            btnStop.Tag = "11";
            btnStop.Text = "■";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnAutoTsuibi_Click;
            // 
            // btnRight
            // 
            btnRight.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnRight.Location = new Point(145, 200);
            btnRight.Margin = new Padding(4);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(48, 40);
            btnRight.TabIndex = 176;
            btnRight.Tag = "7";
            btnRight.Text = ">";
            btnRight.UseVisualStyleBackColor = true;
            btnRight.Click += btnAutoTsuibi_Click;
            btnRight.MouseDown += btnUp_MouseDown;
            btnRight.MouseUp += btnUp_MouseUp;
            // 
            // btnRight2
            // 
            btnRight2.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnRight2.Location = new Point(195, 200);
            btnRight2.Margin = new Padding(4);
            btnRight2.Name = "btnRight2";
            btnRight2.Size = new Size(48, 40);
            btnRight2.TabIndex = 174;
            btnRight2.Tag = "8";
            btnRight2.Text = ">>";
            btnRight2.UseVisualStyleBackColor = true;
            btnRight2.Click += btnAutoTsuibi_Click;
            btnRight2.MouseDown += btnUp_MouseDown;
            btnRight2.MouseUp += btnUp_MouseUp;
            // 
            // trackBar1
            // 
            trackBar1.AutoSize = false;
            trackBar1.LargeChange = 1;
            trackBar1.Location = new Point(-1, 242);
            trackBar1.Margin = new Padding(4);
            trackBar1.Maximum = 5;
            trackBar1.Minimum = 2;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(93, 40);
            trackBar1.TabIndex = 178;
            trackBar1.Value = 3;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // btnDown
            // 
            btnDown.Enabled = false;
            btnDown.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnDown.Location = new Point(99, 242);
            btnDown.Margin = new Padding(4);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(48, 40);
            btnDown.TabIndex = 179;
            btnDown.Tag = "10";
            btnDown.Text = "▽";
            btnDown.UseVisualStyleBackColor = true;
            btnDown.Click += btnAutoTsuibi_Click;
            btnDown.MouseDown += btnUp_MouseDown;
            btnDown.MouseUp += btnUp_MouseUp;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            labelStatus.ForeColor = Color.Red;
            labelStatus.Location = new Point(152, 252);
            labelStatus.Margin = new Padding(4, 0, 4, 0);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(67, 14);
            labelStatus.TabIndex = 189;
            labelStatus.Text = "TSの状況";
            // 
            // tabGPS
            // 
            tabGPS.Controls.Add(cBoxGPS);
            tabGPS.Controls.Add(label27);
            tabGPS.Controls.Add(cBoxKei);
            tabGPS.Controls.Add(comboBox2);
            tabGPS.Controls.Add(btnConnect2);
            tabGPS.Controls.Add(label20);
            tabGPS.Controls.Add(cBoxGPSStatus);
            tabGPS.Controls.Add(label24);
            tabGPS.Controls.Add(textBoxKikaikou2);
            tabGPS.Controls.Add(label25);
            tabGPS.Controls.Add(labelStatus2);
            tabGPS.Controls.Add(labelGPS1);
            tabGPS.Controls.Add(labelGPS2);
            tabGPS.Location = new Point(4, 23);
            tabGPS.Margin = new Padding(4);
            tabGPS.Name = "tabGPS";
            tabGPS.Padding = new Padding(4);
            tabGPS.Size = new Size(282, 295);
            tabGPS.TabIndex = 3;
            tabGPS.Text = "GPS";
            tabGPS.UseVisualStyleBackColor = true;
            // 
            // cBoxGPS
            // 
            cBoxGPS.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxGPS.FormattingEnabled = true;
            cBoxGPS.Items.AddRange(new object[] { "ポチスタ", "i93" });
            cBoxGPS.Location = new Point(2, 5);
            cBoxGPS.Margin = new Padding(4);
            cBoxGPS.Name = "cBoxGPS";
            cBoxGPS.Size = new Size(100, 21);
            cBoxGPS.TabIndex = 193;
            cBoxGPS.SelectedIndexChanged += cBoxGPS_SelectedIndexChanged;
            // 
            // label27
            // 
            label27.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label27.Location = new Point(105, 10);
            label27.Margin = new Padding(4, 0, 4, 0);
            label27.Name = "label27";
            label27.Size = new Size(91, 20);
            label27.TabIndex = 191;
            label27.Text = "直角座標系:";
            // 
            // cBoxKei
            // 
            cBoxKei.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxKei.FormattingEnabled = true;
            cBoxKei.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" });
            cBoxKei.Location = new Point(196, 5);
            cBoxKei.Margin = new Padding(4);
            cBoxKei.Name = "cBoxKei";
            cBoxKei.Size = new Size(44, 21);
            cBoxKei.TabIndex = 196;
            cBoxKei.SelectedIndexChanged += cBoxKei_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Font = new Font("MS UI Gothic", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(2, 42);
            comboBox2.Margin = new Padding(4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(194, 21);
            comboBox2.TabIndex = 161;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // btnConnect2
            // 
            btnConnect2.Font = new Font("MS UI Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnConnect2.Location = new Point(196, 35);
            btnConnect2.Margin = new Padding(4);
            btnConnect2.Name = "btnConnect2";
            btnConnect2.Size = new Size(51, 35);
            btnConnect2.TabIndex = 162;
            btnConnect2.Tag = "2";
            btnConnect2.Text = "接続";
            btnConnect2.UseVisualStyleBackColor = true;
            btnConnect2.Click += btnConnect_Click;
            // 
            // label20
            // 
            label20.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label20.Location = new Point(2, 85);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(131, 20);
            label20.TabIndex = 160;
            label20.Text = "有効GPSステータス";
            // 
            // cBoxGPSStatus
            // 
            cBoxGPSStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxGPSStatus.FormattingEnabled = true;
            cBoxGPSStatus.Items.AddRange(new object[] { "RTK-fix", "RTK-float", "DGPS-fix", "単独測位" });
            cBoxGPSStatus.Location = new Point(135, 80);
            cBoxGPSStatus.Margin = new Padding(4);
            cBoxGPSStatus.Name = "cBoxGPSStatus";
            cBoxGPSStatus.Size = new Size(107, 21);
            cBoxGPSStatus.TabIndex = 194;
            cBoxGPSStatus.SelectedIndexChanged += cBoxGPSStatus_SelectedIndexChanged;
            // 
            // label24
            // 
            label24.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label24.Location = new Point(2, 125);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(65, 20);
            label24.TabIndex = 191;
            label24.Text = "器械高";
            // 
            // textBoxKikaikou2
            // 
            textBoxKikaikou2.BackColor = Color.FromArgb(255, 255, 128);
            textBoxKikaikou2.Font = new Font("MS UI Gothic", 16F, FontStyle.Bold, GraphicsUnit.Point, 128);
            textBoxKikaikou2.Location = new Point(70, 118);
            textBoxKikaikou2.Margin = new Padding(4);
            textBoxKikaikou2.Name = "textBoxKikaikou2";
            textBoxKikaikou2.Size = new Size(74, 29);
            textBoxKikaikou2.TabIndex = 189;
            textBoxKikaikou2.Tag = "3";
            textBoxKikaikou2.Text = "1.500";
            textBoxKikaikou2.TextAlign = HorizontalAlignment.Right;
            textBoxKikaikou2.TextChanged += Kikaikou1_TextChanged;
            // 
            // label25
            // 
            label25.Font = new Font("MS UI Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label25.Location = new Point(149, 125);
            label25.Margin = new Padding(4, 0, 4, 0);
            label25.Name = "label25";
            label25.Size = new Size(28, 20);
            label25.TabIndex = 190;
            label25.Text = "ｍ";
            // 
            // labelStatus2
            // 
            labelStatus2.AutoSize = true;
            labelStatus2.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            labelStatus2.ForeColor = Color.Red;
            labelStatus2.Location = new Point(2, 165);
            labelStatus2.Margin = new Padding(4, 0, 4, 0);
            labelStatus2.Name = "labelStatus2";
            labelStatus2.Size = new Size(79, 14);
            labelStatus2.TabIndex = 195;
            labelStatus2.Text = "GPSの状況";
            // 
            // labelGPS1
            // 
            labelGPS1.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            labelGPS1.Location = new Point(2, 192);
            labelGPS1.Margin = new Padding(4, 0, 4, 0);
            labelGPS1.Name = "labelGPS1";
            labelGPS1.Size = new Size(270, 24);
            labelGPS1.TabIndex = 191;
            labelGPS1.Text = "取得状況：";
            // 
            // labelGPS2
            // 
            labelGPS2.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            labelGPS2.Location = new Point(2, 220);
            labelGPS2.Margin = new Padding(4, 0, 4, 0);
            labelGPS2.Name = "labelGPS2";
            labelGPS2.Size = new Size(275, 24);
            labelGPS2.TabIndex = 191;
            labelGPS2.Text = "HDOP: 衛星数：";
            // 
            // panelBottomCoords
            // 
            panelBottomCoords.Controls.Add(btnUpdPos);
            panelBottomCoords.Controls.Add(textBoxX);
            panelBottomCoords.Controls.Add(label18);
            panelBottomCoords.Controls.Add(textBoxY);
            panelBottomCoords.Controls.Add(label11);
            panelBottomCoords.Controls.Add(textBoxZ);
            panelBottomCoords.Controls.Add(label10);
            panelBottomCoords.Controls.Add(chbContMeasure);
            panelBottomCoords.Controls.Add(chkAutoSet);
            panelBottomCoords.Dock = DockStyle.Top;
            panelBottomCoords.Location = new Point(0, 322);
            panelBottomCoords.Margin = new Padding(4);
            panelBottomCoords.Name = "panelBottomCoords";
            panelBottomCoords.Size = new Size(290, 120);
            panelBottomCoords.TabIndex = 175;
            // 
            // btnUpdPos
            // 
            btnUpdPos.Font = new Font("MS UI Gothic", 28F, FontStyle.Bold, GraphicsUnit.Point, 128);
            btnUpdPos.Location = new Point(174, 31);
            btnUpdPos.Margin = new Padding(4);
            btnUpdPos.Name = "btnUpdPos";
            btnUpdPos.Size = new Size(56, 45);
            btnUpdPos.TabIndex = 164;
            btnUpdPos.Text = "↓";
            btnUpdPos.UseVisualStyleBackColor = true;
            btnUpdPos.Click += button8_Click;
            // 
            // textBoxX
            // 
            textBoxX.Enabled = false;
            textBoxX.Font = new Font("MS UI Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBoxX.Location = new Point(32, 5);
            textBoxX.Margin = new Padding(4);
            textBoxX.Name = "textBoxX";
            textBoxX.Size = new Size(135, 29);
            textBoxX.TabIndex = 3;
            textBoxX.TextAlign = HorizontalAlignment.Right;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label18.Location = new Point(8, 12);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(20, 14);
            label18.TabIndex = 163;
            label18.Text = "X:";
            // 
            // textBoxY
            // 
            textBoxY.Enabled = false;
            textBoxY.Font = new Font("MS UI Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBoxY.Location = new Point(32, 40);
            textBoxY.Margin = new Padding(4);
            textBoxY.Name = "textBoxY";
            textBoxY.Size = new Size(135, 29);
            textBoxY.TabIndex = 4;
            textBoxY.TextAlign = HorizontalAlignment.Right;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label11.Location = new Point(8, 48);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(20, 14);
            label11.TabIndex = 162;
            label11.Text = "Y:";
            // 
            // textBoxZ
            // 
            textBoxZ.Enabled = false;
            textBoxZ.Font = new Font("MS UI Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBoxZ.Location = new Point(32, 75);
            textBoxZ.Margin = new Padding(4);
            textBoxZ.Name = "textBoxZ";
            textBoxZ.Size = new Size(135, 29);
            textBoxZ.TabIndex = 5;
            textBoxZ.TextAlign = HorizontalAlignment.Right;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label10.Location = new Point(8, 82);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(20, 14);
            label10.TabIndex = 161;
            label10.Text = "Z:";
            // 
            // chbContMeasure
            // 
            chbContMeasure.AutoSize = true;
            chbContMeasure.Checked = true;
            chbContMeasure.CheckState = CheckState.Checked;
            chbContMeasure.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            chbContMeasure.Location = new Point(172, 8);
            chbContMeasure.Margin = new Padding(4);
            chbContMeasure.Name = "chbContMeasure";
            chbContMeasure.Size = new Size(86, 18);
            chbContMeasure.TabIndex = 173;
            chbContMeasure.Text = "連続測定";
            chbContMeasure.UseVisualStyleBackColor = true;
            chbContMeasure.CheckedChanged += chbContMeasure_CheckedChanged;
            // 
            // chkAutoSet
            // 
            chkAutoSet.AutoSize = true;
            chkAutoSet.Checked = true;
            chkAutoSet.CheckState = CheckState.Checked;
            chkAutoSet.Font = new Font("MS UI Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            chkAutoSet.Location = new Point(172, 80);
            chkAutoSet.Margin = new Padding(4);
            chkAutoSet.Name = "chkAutoSet";
            chkAutoSet.Size = new Size(79, 18);
            chkAutoSet.TabIndex = 173;
            chkAutoSet.Text = "自動Set";
            chkAutoSet.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // UCCtrl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelCtrl);
            Margin = new Padding(4);
            Name = "UCCtrl";
            Size = new Size(292, 508);
            panelCtrl.ResumeLayout(false);
            tabControl4.ResumeLayout(false);
            tabTS.ResumeLayout(false);
            tabTS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            tabGPS.ResumeLayout(false);
            tabGPS.PerformLayout();
            panelBottomCoords.ResumeLayout(false);
            panelBottomCoords.PerformLayout();
            ResumeLayout(false);

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
