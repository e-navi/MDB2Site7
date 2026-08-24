using System;
using System.Drawing;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public partial class FormDefEnv : Form
    {
        public FormDefEnv()
        {
            InitializeComponent();
            BuildUi();
        }

        private void BuildUi()
        {
            this.Text = "TS・GPS環境設定";
            this.ClientSize = new Size(370, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(242, 244, 248);
            this.Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Regular);

            int pad = 10;
            int cardW = 350;

            // 1. Card 1: プリズム・測距設定 (Height: 118)
            var pnlCard1 = new Panel
            {
                Location = new Point(pad, 8),
                Size = new Size(cardW, 118),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblTitle1 = new Label
            {
                Text = "🎯 プリズム・測距設定",
                Location = new Point(8, 8),
                Size = new Size(200, 18),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 40, 80)
            };

            var lblPrism = new Label { Text = "プリズム:", Location = new Point(8, 33), Size = new Size(60, 18) };
            CBSetPrism = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(72, 30),
                Size = new Size(135, 23),
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };
            CBSetPrism.SelectedIndexChanged += CBSetPrism_SelectedIndexChanged;

            var lblPrismVal = new Label { Text = "定数:", Location = new Point(216, 33), Size = new Size(36, 18) };
            TBPrismVal = new TextBox
            {
                Location = new Point(254, 30),
                Size = new Size(55, 23),
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };
            var lblMm = new Label { Text = "mm", Location = new Point(313, 33), Size = new Size(28, 18) };

            var lblSokkyo = new Label { Text = "測距モード:", Location = new Point(8, 62), Size = new Size(70, 18) };
            CBSokkyoMode = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(80, 59),
                Size = new Size(260, 23)
            };

            var lblTilt = new Label { Text = "チルト補正:", Location = new Point(8, 90), Size = new Size(70, 18) };
            CBTilt = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(80, 87),
                Size = new Size(260, 23)
            };

            pnlCard1.Controls.AddRange(new Control[] {
                lblTitle1, lblPrism, CBSetPrism, lblPrismVal, TBPrismVal, lblMm,
                lblSokkyo, CBSokkyoMode, lblTilt, CBTilt
            });
            this.Controls.Add(pnlCard1);

            // 2. Card 2: サーチ・ガイドライト設定 (Height: 188)
            var pnlCard2 = new Panel
            {
                Location = new Point(pad, 134),
                Size = new Size(cardW, 188),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblTitle2 = new Label
            {
                Text = "📡 サーチ・ライト・RC設定",
                Location = new Point(8, 8),
                Size = new Size(200, 18),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 40, 80)
            };

            var lblSearch = new Label { Text = "サーチ範囲:", Location = new Point(8, 33), Size = new Size(70, 18) };
            var lblH = new Label { Text = "H:", Location = new Point(80, 33), Size = new Size(18, 18) };
            TBSearchH = new TextBox { Location = new Point(98, 30), Size = new Size(40, 23), TextAlign = HorizontalAlignment.Right };
            var lblHdeg = new Label { Text = "°", Location = new Point(140, 33), Size = new Size(16, 18) };

            var lblV = new Label { Text = "V:", Location = new Point(165, 33), Size = new Size(18, 18) };
            TBSearchV = new TextBox { Location = new Point(183, 30), Size = new Size(40, 23), TextAlign = HorizontalAlignment.Right };
            var lblVdeg = new Label { Text = "°", Location = new Point(225, 33), Size = new Size(16, 18) };

            var lblRc = new Label { Text = "RC:", Location = new Point(245, 33), Size = new Size(26, 18) };
            CBUseRC = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(272, 30), Size = new Size(68, 23) };

            var lblGuide = new Label { Text = "ガイドライト:", Location = new Point(8, 62), Size = new Size(76, 18) };
            CBGuidLightPat = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(88, 59), Size = new Size(150, 23) };
            CBGuidLightVal = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(244, 59), Size = new Size(96, 23) };

            var lblLaser = new Label { Text = "照射ライト:", Location = new Point(8, 91), Size = new Size(76, 18) };
            CBLightPat = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(88, 88), Size = new Size(150, 23) };
            CBLightVal = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(244, 88), Size = new Size(96, 23) };

            var lblDesc = new Label
            {
                Text = "※ 接続TSの機種により対応機能のみ有効となります。",
                Location = new Point(8, 122),
                Size = new Size(330, 36),
                Font = new Font("Yu Gothic UI", 8.0F, FontStyle.Regular),
                ForeColor = Color.FromArgb(120, 120, 130)
            };

            pnlCard2.Controls.AddRange(new Control[] {
                lblTitle2, lblSearch, lblH, TBSearchH, lblHdeg, lblV, TBSearchV, lblVdeg, lblRc, CBUseRC,
                lblGuide, CBGuidLightPat, CBGuidLightVal, lblLaser, CBLightPat, CBLightVal, lblDesc
            });
            this.Controls.Add(pnlCard2);

            // 3. Card 3: RTK-GPS用設定 (Height: 116)
            var pnlCard3 = new Panel
            {
                Location = new Point(pad, 330),
                Size = new Size(cardW, 116),
                BackColor = Color.FromArgb(235, 248, 255),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblTitle3 = new Label
            {
                Text = "🛰 RTK-GPS 設定",
                Location = new Point(8, 8),
                Size = new Size(200, 18),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204)
            };

            var lblGpsH = new Label { Text = "アンテナ高:", Location = new Point(8, 33), Size = new Size(70, 18) };
            CBGPSHeight = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(82, 30), Size = new Size(125, 23) };

            var lblGpsCnt = new Label { Text = "平均回数:", Location = new Point(218, 33), Size = new Size(58, 18) };
            TBGPSCount = new TextBox { Location = new Point(278, 30), Size = new Size(40, 23), TextAlign = HorizontalAlignment.Right };
            var lblKai = new Label { Text = "回", Location = new Point(322, 33), Size = new Size(20, 18) };

            var lblImu = new Label { Text = "i93 IMU補正:", Location = new Point(8, 62), Size = new Size(75, 18) };
            CBi93IMU = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(86, 59), Size = new Size(254, 23) };

            pnlCard3.Controls.AddRange(new Control[] {
                lblTitle3, lblGpsH, CBGPSHeight, lblGpsCnt, TBGPSCount, lblKai, lblImu, CBi93IMU
            });
            this.Controls.Add(pnlCard3);

            // 4. Bottom Action Area (Height: 52)
            var pnlBottom = new Panel
            {
                Location = new Point(pad, 456),
                Size = new Size(cardW, 52),
                BackColor = Color.Transparent
            };

            Do_Button = new Button
            {
                Text = "💾 設定を保存",
                Location = new Point(0, 6),
                Size = new Size(230, 38),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
            Do_Button.Click += Do_Button_Click;

            Cancel_Button = new Button
            {
                Text = "閉じる",
                Location = new Point(240, 6),
                Size = new Size(110, 38),
                BackColor = Color.FromArgb(220, 224, 230),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                UseVisualStyleBackColor = true
            };
            Cancel_Button.Click += Cancel_Button_Click;

            pnlBottom.Controls.AddRange(new Control[] { Do_Button, Cancel_Button });
            this.Controls.Add(pnlBottom);
        }

        private void FormDefEnv_Load(object? sender, EventArgs e)
        {
            PopulateCombos();
            LoadEnvValues();
        }

        private void PopulateCombos()
        {
            CBSetPrism.Items.Clear();
            CBSetPrism.Items.AddRange(Env.PrismStrs);

            CBSokkyoMode.Items.Clear();
            CBSokkyoMode.Items.AddRange(Env.SokkyoModeStrs);

            CBTilt.Items.Clear();
            CBTilt.Items.AddRange(Env.TiltStrs);

            CBLightPat.Items.Clear();
            CBLightPat.Items.AddRange(Env.LightPatStrs);

            CBLightVal.Items.Clear();
            CBLightVal.Items.AddRange(Env.LightValStrs);

            CBUseRC.Items.Clear();
            CBUseRC.Items.AddRange(Env.UseRCStrs);

            CBGuidLightPat.Items.Clear();
            CBGuidLightPat.Items.AddRange(Env.GuideLightPatStrs);

            CBGuidLightVal.Items.Clear();
            CBGuidLightVal.Items.AddRange(Env.LightValStrs);

            CBGPSHeight.Items.Clear();
            CBGPSHeight.Items.AddRange(Env.GPSHeightStrs);

            CBi93IMU.Items.Clear();
            CBi93IMU.Items.AddRange(Env.i93IMUStrs);
        }

        private void LoadEnvValues()
        {
            if (Env.Prism >= 0 && Env.Prism < CBSetPrism.Items.Count) CBSetPrism.SelectedIndex = Env.Prism;
            TBPrismVal.Text = Env.PrismVal.ToString();

            if (Env.SokkyoMode >= 0 && Env.SokkyoMode < CBSokkyoMode.Items.Count) CBSokkyoMode.SelectedIndex = Env.SokkyoMode;
            if (Env.Tilt >= 0 && Env.Tilt < CBTilt.Items.Count) CBTilt.SelectedIndex = Env.Tilt;
            if (Env.LightPat >= 0 && Env.LightPat < CBLightPat.Items.Count) CBLightPat.SelectedIndex = Env.LightPat;
            if (Env.LightVal >= 0 && Env.LightVal < CBLightVal.Items.Count) CBLightVal.SelectedIndex = Env.LightVal;

            TBSearchH.Text = Env.SearchH.ToString();
            TBSearchV.Text = Env.SearchV.ToString();

            if (Env.UseRC >= 0 && Env.UseRC < CBUseRC.Items.Count) CBUseRC.SelectedIndex = Env.UseRC;
            if (Env.GuideLightPat >= 0 && Env.GuideLightPat < CBGuidLightPat.Items.Count) CBGuidLightPat.SelectedIndex = Env.GuideLightPat;
            if (Env.GuideLightVal >= 0 && Env.GuideLightVal < CBGuidLightVal.Items.Count) CBGuidLightVal.SelectedIndex = Env.GuideLightVal;

            if (Env.GPSHeight >= 0 && Env.GPSHeight < CBGPSHeight.Items.Count) CBGPSHeight.SelectedIndex = Env.GPSHeight;
            TBGPSCount.Text = Env.GPSCount.ToString();
            if (Env.i93IMU >= 0 && Env.i93IMU < CBi93IMU.Items.Count) CBi93IMU.SelectedIndex = Env.i93IMU;
        }

        private void CBSetPrism_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int idx = CBSetPrism.SelectedIndex;
            if (idx >= 0 && idx < Env.PrismVals.Length)
            {
                TBPrismVal.Text = Env.PrismVals[idx].ToString();
            }
        }

        private void Do_Button_Click(object? sender, EventArgs e)
        {
            if (CBSetPrism.SelectedIndex >= 0) Env.Prism = CBSetPrism.SelectedIndex;
            if (int.TryParse(TBPrismVal.Text, out int prismVal)) Env.PrismVal = prismVal;

            if (CBSokkyoMode.SelectedIndex >= 0) Env.SokkyoMode = CBSokkyoMode.SelectedIndex;
            if (CBTilt.SelectedIndex >= 0) Env.Tilt = CBTilt.SelectedIndex;
            if (CBLightPat.SelectedIndex >= 0) Env.LightPat = CBLightPat.SelectedIndex;
            if (CBLightVal.SelectedIndex >= 0) Env.LightVal = CBLightVal.SelectedIndex;

            if (int.TryParse(TBSearchH.Text, out int searchH)) Env.SearchH = searchH;
            if (int.TryParse(TBSearchV.Text, out int searchV)) Env.SearchV = searchV;

            if (CBUseRC.SelectedIndex >= 0) Env.UseRC = CBUseRC.SelectedIndex;
            if (CBGuidLightPat.SelectedIndex >= 0) Env.GuideLightPat = CBGuidLightPat.SelectedIndex;
            if (CBGuidLightVal.SelectedIndex >= 0) Env.GuideLightVal = CBGuidLightVal.SelectedIndex;

            if (CBGPSHeight.SelectedIndex >= 0) Env.GPSHeight = CBGPSHeight.SelectedIndex;
            if (int.TryParse(TBGPSCount.Text, out int gpsCount)) Env.GPSCount = gpsCount;
            if (CBi93IMU.SelectedIndex >= 0) Env.i93IMU = CBi93IMU.SelectedIndex;

            Env.SaveEnvVal();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Button_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
