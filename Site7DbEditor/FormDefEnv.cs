using System;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public partial class FormDefEnv : Form
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int Mode { get; set; } = 0; // 0: TS/GPS, 1: 図面

        public FormDefEnv()
        {
            InitializeComponent();
        }

        private void FormDefEnv_Load(object? sender, EventArgs e)
        {
            PopulateCombos();
            LoadEnvValues();

            if (Mode == 1)
            {
                tabControl1.SelectedTab = tpZumen;
            }
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

            CBPaperSize.Items.Clear();
            CBPaperSize.Items.AddRange(Env.PaperSizeStrs);

            CBPaperScale.Items.Clear();
            CBPaperScale.Items.AddRange(Env.PaperScaleStrs);

            CBSetPrism.SelectedIndexChanged += CBSetPrism_SelectedIndexChanged;
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

            if (Env.PaperSize >= 0 && Env.PaperSize < CBPaperSize.Items.Count) CBPaperSize.SelectedIndex = Env.PaperSize;
            if (Env.PaperScale >= 0 && Env.PaperScale < CBPaperScale.Items.Count) CBPaperScale.SelectedIndex = Env.PaperScale;
            TBPaperAng.Text = Env.PaperAng.ToString();
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

            if (CBPaperSize.SelectedIndex >= 0) Env.PaperSize = CBPaperSize.SelectedIndex;
            if (CBPaperScale.SelectedIndex >= 0) Env.PaperScale = CBPaperScale.SelectedIndex;
            if (int.TryParse(TBPaperAng.Text, out int paperAng)) Env.PaperAng = paperAng;

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
