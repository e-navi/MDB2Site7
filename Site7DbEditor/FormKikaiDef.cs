using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Site7DbEditor {
    public partial class FormKikaiDef : Form {

        int curTSMode = 0;

        public FormKikaiDef() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            //gbl.FormMain.SetSijun(false);
            //後視点を視準！
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;


            Def.SetIniStr("TS", "器械点", km.kp.Name);
            Def.SetIniStr("TS", "後視点", km.bp.Name);

            km.angK = km.calc2PAng(km.kp, km.bp);


            //gbl.TStation.SetAutoTrack(false);
            gbl.TStation.SetFILD(false);

            ts.SetZero();

            gbl.TStation.SetFILD(true);
            //gbl.TStation.SetAutoTrack(true);

            //Thread.Sleep(1000);
            //gbl.TStation.SetFILD(true);

            if (!ts.isLN100) {
                km.ang0 = 0.0;
            }
            //ts.SetITRGT();
            gbl.FormMain?.ShowZumen0();
            Hide();
        }
        
        private void UpdatePlanLen() {
            KikaiMan km = gbl.KikaiMan;
            if (km.kp != null && km.bp != null) {
                double len1 = km.kp.CalcLen(km.bp);
                L_Len1.Text = len1.ToString("0.000");
            }
        }

        private void FormKikaiDef_Shown(object sender, EventArgs e) {
            button1.Enabled = false;

            CBSelKikaiP.Items.Clear();
            CBSelBackP.Items.Clear();
            int kidx = 0;
            int bidx = 0;
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;

            ts.isKikaiDefSet = false;

            for (int i = 0; i < gbl.st7Data.KijunP.KPList.Count; i++) {
                KijunPRecEx rec = gbl.st7Data.KijunP.KPList[i];
                CBSelKikaiP.Items.Add(rec.Name);
                CBSelBackP.Items.Add(rec.Name);
                if (km.kp != null && km.kp.Name == rec.Name) {
                    kidx = i;
                }
                if (km.bp != null && km.bp.Name == rec.Name) {
                    bidx = i;
                }
            }
            if (CBSelKikaiP.Items.Count > 0) {
                CBSelKikaiP.SelectedIndex = kidx;
            }
            if (CBSelBackP.Items.Count > 0) {
                CBSelBackP.SelectedIndex = bidx;
            }
            UpdatePlanLen();
        }

        private void button2_Click(object sender, EventArgs e) {
            Hide();
        }

        private void buttonMesure01_Click(object sender, EventArgs e) {
            //後視点を視準！
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;

            if (km.kp == null || km.bp == null) {
                MessageBox.Show("器械点と後視点を指定してください");
                return;
            }

            double len1 = km.kp.CalcLen(km.bp);

            if (len1 == 0.0) {
                MessageBox.Show("器械点・後視点で異なる点を指定してください");
                return;
            }
            ts.isKikaiDefSet = true;
            timer1.Enabled = true;
            button1.Enabled = false;

            ts.AS_BtnClick_3();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;

            if (ts.isKikaiDefSet) {
                return;
            }
            timer1.Enabled = false;

            double len1 = (km.kp != null && km.bp != null) ? km.kp.CalcLen(km.bp) : 0.0;
            
            // TS測定値（水平距離）
            double len2 = 0.0;
            if (gbl.MField.lng > 0.0) {
                // 斜距離 * sin(鉛直角)
                double radV = gbl.MField.angV * 2.0 * Math.PI;
                len2 = gbl.MField.lng * Math.Sin(radV);
                if (len2 <= 0.0) {
                    len2 = gbl.MField.lng;
                }
            } else if (km.kp != null && ts.curPos != null) {
                len2 = km.kp.CalcLen(ts.curPos);
            }

            L_Len1.Text = len1.ToString("0.000");
            L_Len2.Text = len2.ToString("0.000");
            L_Len3.Text = (len1 - len2).ToString("0.000");

            button1.Enabled = true;
        }

        private void CBSelKikaiP_SelectedIndexChanged(object sender, EventArgs e) {
            KikaiMan km = gbl.KikaiMan;
            if (CBSelKikaiP.SelectedIndex >= 0 && CBSelKikaiP.SelectedIndex < gbl.st7Data.KijunP.KPList.Count) {
                KijunPRecEx krec = gbl.st7Data.KijunP.KPList[CBSelKikaiP.SelectedIndex];
                km.kp = new TINP3(krec.Name, krec.X, krec.Y, krec.Z);
                UpdatePlanLen();
            }
        }

        private void CBSelBackP_SelectedIndexChanged(object sender, EventArgs e) {
            KikaiMan km = gbl.KikaiMan;
            if (CBSelBackP.SelectedIndex >= 0 && CBSelBackP.SelectedIndex < gbl.st7Data.KijunP.KPList.Count) {
                KijunPRecEx brec = gbl.st7Data.KijunP.KPList[CBSelBackP.SelectedIndex];
                km.bp = new TINP3(brec.Name, brec.X, brec.Y, brec.Z);
                UpdatePlanLen();
            }
        }
        private void ChangeTSMode(bool isShow) {
            if (isShow) {
                if (Env.curTSMode0 == Env.TS_MODE_TUIBI) {
                    curTSMode = Env.curTSMode0;

                    gbl.UCCtrl?.SetBtns2(true);
                }
            } else {
                if (curTSMode == Env.TS_MODE_TUIBI) {
                    bool isKijun = gbl.FormMain?.isModeKijun() ?? false;
                    gbl.UCCtrl?.SetBtns2(isKijun);
                }
            }
        }
        private void FormKikaiDef_FormClosed(object sender, FormClosedEventArgs e) {
            ChangeTSMode(false);
        }

        private void FormKikaiDef_VisibleChanged(object sender, EventArgs e) {
            ChangeTSMode(Visible);
        }

    }
}
