using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Site7 {
    public partial class FormDefEnv : Form {

        public int Mode = 0;
        public FormDefEnv() {
            InitializeComponent();
        }

        private void FormDefEnv_Load(object sender, EventArgs e) {
            CBSetPrism.SelectedIndex = Env.Prism;
            TBPrismVal.Text = Env.PrismVal.ToString();
            CBSokkyoMode.SelectedIndex = Env.SokkyoMode;
            CBTilt.SelectedIndex = Env.Tilt;
            CBLightPat.SelectedIndex = Env.LightPat;
            CBLightVal.SelectedIndex = Env.LightVal;
            TBSearchH.Text = Env.SearchH.ToString();
            TBSearchV.Text = Env.SearchV.ToString();
            CBUseRC.SelectedIndex = Env.UseRC;
            CBGuidLightPat.SelectedIndex = Env.GuideLightPat;
            CBGuidLightVal.SelectedIndex = Env.GuideLightVal;
            //CBGPSStatus.SelectedIndex = Env.GPSStatus;
            CBGPSHeight.SelectedIndex = Env.GPSHeight;
            TBGPSCount.Text = Env.GPSCount.ToString();
            CBi93IMU.SelectedIndex = Env.i93IMU;

            CBPaperSize.SelectedIndex = Env.PaperSize;
            CBPaperScale.SelectedIndex = Env.PaperScale;
            TBPaperAng.Text = Env.PaperAng.ToString();
            if (Mode == 1) {
                tabControl1.SelectedTab = tpZumen;
            }
        }
        private void Do_Button_Click(object sender, EventArgs e) {
            Env.Prism = CBSetPrism.SelectedIndex;
            //Env.PrismVal = int.Parse(TBPrismVal.Text);
            int.TryParse(TBPrismVal.Text, out Env.PrismVal);
            Env.SokkyoMode = CBSokkyoMode.SelectedIndex;
            Env.Tilt = CBTilt.SelectedIndex;
            Env.LightPat = CBLightPat.SelectedIndex;
            Env.LightVal = CBLightVal.SelectedIndex;
            Env.SearchH = int.Parse(TBSearchH.Text);
            Env.SearchV = int.Parse(TBSearchV.Text);
            Env.UseRC = CBUseRC.SelectedIndex;
            Env.GuideLightPat = CBLightPat.SelectedIndex;
            Env.GuideLightVal = CBLightVal.SelectedIndex;
            //Env.GPSStatus = CBGPSStatus.SelectedIndex;
            Env.GPSHeight = CBGPSHeight.SelectedIndex;
            Env.GPSCount = int.Parse(TBGPSCount.Text);
            Env.i93IMU = CBi93IMU.SelectedIndex;

            Env.PaperSize = CBPaperSize.SelectedIndex;
            Env.PaperScale = CBPaperScale.SelectedIndex;
            Env.PaperAng = int.Parse(TBPaperAng.Text);

            Env.SaveEnvVal();
        }
        private void Cancel_Button_Click(object sender, EventArgs e) {

        }
        private void CBSetPrism_SelectedIndexChanged(object sender, EventArgs e) {
            TBPrismVal.Text = Env.PrismVals[CBSetPrism.SelectedIndex].ToString();
        }

        private void TBPrismVal_KeyPress(object sender, KeyPressEventArgs e) {
            //バックスペースが押された時は有効（Deleteキーも有効）
            if (e.KeyChar == '\b') {
                return;
            }
            if (e.KeyChar == '-') {
                return;
            }
            //数値0～9以外が押された時はイベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void TBSearchH_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\b') {
                return;
            }
            //数値0～9以外が押された時はイベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar)) {
                e.Handled = true;
            }
        }

    }
}
