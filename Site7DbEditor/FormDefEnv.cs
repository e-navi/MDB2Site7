using System;
using System.Drawing;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public partial class FormDefEnv : Form
    {
        private readonly bool _isMasterMode;
        private Button btnExportToMaster = null!;
        private Button btnImportFromMaster = null!;

        public FormDefEnv(bool isMasterMode = false)
        {
            _isMasterMode = isMasterMode;
            InitializeComponent();
            BuildUi();
        }

        private void BuildUi()
        {
            this.Text = _isMasterMode ? "TS・GPS環境設定 (マスター定義)" : "TS・GPS環境設定 (現場定義データ)";
            this.ClientSize = new Size(370, 560);
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

            // 4. Bottom Action Area (Height: 90)
            var pnlBottom = new Panel
            {
                Location = new Point(pad, 456),
                Size = new Size(cardW, 90),
                BackColor = Color.Transparent
            };

            btnExportToMaster = new Button
            {
                Text = "📤 マスターへ反映",
                Location = new Point(0, 4),
                Size = new Size(170, 32),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(233, 236, 243),
                ForeColor = Color.FromArgb(25, 45, 80),
                FlatStyle = FlatStyle.Flat,
                Visible = !_isMasterMode
            };
            btnExportToMaster.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnExportToMaster.Click += BtnExportToMaster_Click;

            btnImportFromMaster = new Button
            {
                Text = "📥 マスターから反映",
                Location = new Point(180, 4),
                Size = new Size(170, 32),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(233, 236, 243),
                ForeColor = Color.FromArgb(25, 45, 80),
                FlatStyle = FlatStyle.Flat,
                Visible = !_isMasterMode
            };
            btnImportFromMaster.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnImportFromMaster.Click += BtnImportFromMaster_Click;

            Do_Button = new Button
            {
                Text = "💾 設定を保存",
                Location = new Point(0, 42),
                Size = new Size(230, 38),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            Do_Button.FlatAppearance.BorderSize = 0;
            Do_Button.Click += Do_Button_Click;

            Cancel_Button = new Button
            {
                Text = "閉じる",
                Location = new Point(240, 42),
                Size = new Size(110, 38),
                BackColor = Color.FromArgb(220, 225, 235),
                ForeColor = Color.FromArgb(30, 40, 60),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.Click += Cancel_Button_Click;

            pnlBottom.Controls.AddRange(new Control[] { btnExportToMaster, btnImportFromMaster, Do_Button, Cancel_Button });
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

        private void LoadEnvValues(string? specificIniPath = null)
        {
            string iniPath = specificIniPath ?? (_isMasterMode ? Def.GetSystemIniFileName() : Def.iniFileName);

            int prism = Def.GetIniInt(iniPath, "TS", "Prism", Env.Prism);
            if (prism >= 0 && prism < CBSetPrism.Items.Count) CBSetPrism.SelectedIndex = prism;

            int prismVal = Def.GetIniInt(iniPath, "TS", "PrismVal", Env.PrismVal);
            TBPrismVal.Text = prismVal.ToString();

            int sokkyoMode = Def.GetIniInt(iniPath, "TS", "SokkyoMode", Env.SokkyoMode);
            if (sokkyoMode >= 0 && sokkyoMode < CBSokkyoMode.Items.Count) CBSokkyoMode.SelectedIndex = sokkyoMode;

            int tilt = Def.GetIniInt(iniPath, "TS", "Tilt", Env.Tilt);
            if (tilt >= 0 && tilt < CBTilt.Items.Count) CBTilt.SelectedIndex = tilt;

            int lightPat = Def.GetIniInt(iniPath, "TS", "LightPat", Env.LightPat);
            if (lightPat >= 0 && lightPat < CBLightPat.Items.Count) CBLightPat.SelectedIndex = lightPat;

            int lightVal = Def.GetIniInt(iniPath, "TS", "LightVal", Env.LightVal);
            if (lightVal >= 0 && lightVal < CBLightVal.Items.Count) CBLightVal.SelectedIndex = lightVal;

            int searchH = Def.GetIniInt(iniPath, "TS", "SearchH", Env.SearchH);
            TBSearchH.Text = searchH.ToString();

            int searchV = Def.GetIniInt(iniPath, "TS", "SearchV", Env.SearchV);
            TBSearchV.Text = searchV.ToString();

            int useRC = Def.GetIniInt(iniPath, "TS", "UseRC", Env.UseRC);
            if (useRC >= 0 && useRC < CBUseRC.Items.Count) CBUseRC.SelectedIndex = useRC;

            int guideLightPat = Def.GetIniInt(iniPath, "TS", "GuideLightPat", Env.GuideLightPat);
            if (guideLightPat >= 0 && guideLightPat < CBGuidLightPat.Items.Count) CBGuidLightPat.SelectedIndex = guideLightPat;

            int guideLightVal = Def.GetIniInt(iniPath, "TS", "GuideLightVal", Env.GuideLightVal);
            if (guideLightVal >= 0 && guideLightVal < CBGuidLightVal.Items.Count) CBGuidLightVal.SelectedIndex = guideLightVal;

            int gpsHeight = Def.GetIniInt(iniPath, "TS", "GPSHeight", Env.GPSHeight);
            if (gpsHeight >= 0 && gpsHeight < CBGPSHeight.Items.Count) CBGPSHeight.SelectedIndex = gpsHeight;

            int gpsCount = Def.GetIniInt(iniPath, "TS", "GPSCount", Env.GPSCount);
            TBGPSCount.Text = gpsCount.ToString();

            int i93IMU = Def.GetIniInt(iniPath, "TS", "i93IMU", Env.i93IMU);
            if (i93IMU >= 0 && i93IMU < CBi93IMU.Items.Count) CBi93IMU.SelectedIndex = i93IMU;
        }

        private void SaveToIni(string targetIniPath)
        {
            int prism = CBSetPrism.SelectedIndex >= 0 ? CBSetPrism.SelectedIndex : 0;
            int prismVal = int.TryParse(TBPrismVal.Text, out int pv) ? pv : 0;
            int sokkyoMode = CBSokkyoMode.SelectedIndex >= 0 ? CBSokkyoMode.SelectedIndex : 0;
            int tilt = CBTilt.SelectedIndex >= 0 ? CBTilt.SelectedIndex : 0;
            int lightPat = CBLightPat.SelectedIndex >= 0 ? CBLightPat.SelectedIndex : 0;
            int lightVal = CBLightVal.SelectedIndex >= 0 ? CBLightVal.SelectedIndex : 0;
            int searchH = int.TryParse(TBSearchH.Text, out int sh) ? sh : 10;
            int searchV = int.TryParse(TBSearchV.Text, out int sv) ? sv : 10;
            int useRC = CBUseRC.SelectedIndex >= 0 ? CBUseRC.SelectedIndex : 0;
            int guideLightPat = CBGuidLightPat.SelectedIndex >= 0 ? CBGuidLightPat.SelectedIndex : 0;
            int guideLightVal = CBGuidLightVal.SelectedIndex >= 0 ? CBGuidLightVal.SelectedIndex : 0;
            int gpsHeight = CBGPSHeight.SelectedIndex >= 0 ? CBGPSHeight.SelectedIndex : 0;
            int gpsCount = int.TryParse(TBGPSCount.Text, out int gc) ? gc : 1;
            int i93IMU = CBi93IMU.SelectedIndex >= 0 ? CBi93IMU.SelectedIndex : 0;

            Def.SetIniInt(targetIniPath, "TS", "Prism", prism);
            Def.SetIniInt(targetIniPath, "TS", "PrismVal", prismVal);
            Def.SetIniInt(targetIniPath, "TS", "SokkyoMode", sokkyoMode);
            Def.SetIniInt(targetIniPath, "TS", "Tilt", tilt);
            Def.SetIniInt(targetIniPath, "TS", "LightPat", lightPat);
            Def.SetIniInt(targetIniPath, "TS", "LightVal", lightVal);
            Def.SetIniInt(targetIniPath, "TS", "SearchH", searchH);
            Def.SetIniInt(targetIniPath, "TS", "SearchV", searchV);
            Def.SetIniInt(targetIniPath, "TS", "UseRC", useRC);
            Def.SetIniInt(targetIniPath, "TS", "GuideLightPat", guideLightPat);
            Def.SetIniInt(targetIniPath, "TS", "GuideLightVal", guideLightVal);
            Def.SetIniInt(targetIniPath, "TS", "GPSHeight", gpsHeight);
            Def.SetIniInt(targetIniPath, "TS", "GPSCount", gpsCount);
            Def.SetIniInt(targetIniPath, "TS", "i93IMU", i93IMU);

            if (!_isMasterMode && targetIniPath == Def.iniFileName)
            {
                Env.Prism = prism;
                Env.PrismVal = prismVal;
                Env.SokkyoMode = sokkyoMode;
                Env.Tilt = tilt;
                Env.LightPat = lightPat;
                Env.LightVal = lightVal;
                Env.SearchH = searchH;
                Env.SearchV = searchV;
                Env.UseRC = useRC;
                Env.GuideLightPat = guideLightPat;
                Env.GuideLightVal = guideLightVal;
                Env.GPSHeight = gpsHeight;
                Env.GPSCount = gpsCount;
                Env.i93IMU = i93IMU;
            }
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
            string targetIni = _isMasterMode ? Def.GetSystemIniFileName() : Def.iniFileName;
            SaveToIni(targetIni);

            string targetLabel = _isMasterMode ? "マスターTS・GPS環境設定" : "現場TS・GPS環境設定";
            MessageBox.Show($"{targetLabel}を保存しました。\n保存先: {targetIni}", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnExportToMaster_Click(object? sender, EventArgs e)
        {
            var res = MessageBox.Show(
                "現在の現場のTS・GPS設定で、システム共通マスター設定を上書き更新しますか？\n\n※ 次回の新規現場作成時などに標準設定として使用されます。",
                "マスターへ反映確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            try
            {
                string sysIni = Def.GetSystemIniFileName();
                SaveToIni(sysIni);
                MessageBox.Show($"✔ 現場のTS・GPS設定をシステム共通マスターへ反映しました。\n保存先: {sysIni}", "反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"マスター反映エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImportFromMaster_Click(object? sender, EventArgs e)
        {
            var res = MessageBox.Show(
                "システム共通マスターのTS・GPS設定を読み込み、現在の現場設定に反映しますか？\n\n※ 現在の編集内容はマスター設定で上書きされます。",
                "マスターから反映確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            try
            {
                string sysIni = Def.GetSystemIniFileName();
                LoadEnvValues(sysIni);
                MessageBox.Show("✔ システム共通マスターからTS・GPS設定を反映しました。\n「💾 設定を保存」を押すと現場に保存されます。", "反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"マスター読み込みエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancel_Button_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
