namespace Site7DbEditor
{
    public partial class FormDefEnv : Form
    {
        private readonly bool _isMasterMode;

        public FormDefEnv(bool isMasterMode = false)
        {
            _isMasterMode = isMasterMode;
            InitializeComponent();
            this.Text = _isMasterMode ? "TS・GPS環境設定 (マスター定義)" : "TS・GPS環境設定 (現場定義データ)";
            btnExportToMaster.Visible = !_isMasterMode;
            btnImportFromMaster.Visible = !_isMasterMode;
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
