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
        
        private void FormKikaiDef_Shown(object sender, EventArgs e) {
            //gbl.FormMain.SetSijun(true);
            button1.Enabled = false;

            CBSelKikaiP.Items.Clear();
            CBSelBackP.Items.Clear();
            int kidx = 0;
            int bidx = 0;
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;

            ts.isKikaiDefSet = false;

            // foreach (KijunPRecEx rec in gbl.st7Data.KijunP.KPList) {
            for (int i = 0; i < gbl.st7Data.KijunP.KPList.Count; i++) {
                KijunPRecEx rec = gbl.st7Data.KijunP.KPList[i];
                CBSelKikaiP.Items.Add(rec.Name);
                CBSelBackP.Items.Add(rec.Name);
                if (km.kp.Name == rec.Name) {
                    kidx = i;
                }
                if (km.bp.Name == rec.Name) {
                    bidx = i;
                }

            }
            CBSelKikaiP.SelectedIndex = kidx;
            CBSelBackP.SelectedIndex = bidx;
            UpdatePlanLen();
        }

        private void button2_Click(object sender, EventArgs e) {
            Hide();
        }

        int waitCount = 0;

        private void buttonMesure01_Click(object sender, EventArgs e) {
            //後視点を視準！
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;

            double len1 = km.kp.CalcLen(km.bp);

            if (len1 == 0.0) {
                MessageBox.Show("器械点・後視点で異なる点を指定してください");
                return;
            }
            gbl.MField.isError = false;
            ts.isKikaiDefSet = true;
            waitCount = 0;
            timer1.Enabled = true;
            button1.Enabled = false;

            ts.AS_BtnClick_3();
        }
        private void UpdatePlanLen() {
            KikaiMan km = gbl.KikaiMan;
            if (km.kp != null && km.bp != null) {
                double len1 = km.kp.CalcLen(km.bp);
                L_Len1.Text = len1.ToString("0.000");
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;
            waitCount++;

            if (ts.isKikaiDefSet && gbl.MField.lng <= 0 && waitCount < 100) {
                return;
            }
            timer1.Enabled = false;
            ts.isKikaiDefSet = false;

            double len1 = km.kp.CalcLen(km.bp);
            // 測距値（斜距離 lng）と高度角（angV: 0~1.0）から実測水平距離を計算
            double len2 = 0.0;
            if (gbl.MField.lng > 0) {
                len2 = Math.Abs(gbl.MField.lng * Math.Sin(St7Lib.ToRadian(gbl.MField.angV * 360.0)));
            } else if (ts.curPos != null) {
                len2 = km.kp.CalcLen(ts.curPos);
            }

            L_Len1.Text = len1.ToString("0.000");
            L_Len2.Text = len2.ToString("0.000");
            L_Len3.Text = (len1 - len2).ToString("0.000");

            button1.Enabled = true;
        }

        private void CBSelKikaiP_SelectedIndexChanged(object sender, EventArgs e) {
            if (CBSelKikaiP.SelectedIndex >= 0 && CBSelKikaiP.SelectedIndex < gbl.st7Data.KijunP.KPList.Count) {
                KikaiMan km = gbl.KikaiMan;
                KijunPRecEx krec = gbl.st7Data.KijunP.KPList[CBSelKikaiP.SelectedIndex];
                km.kp = new TINP3(krec.Name, krec.X, krec.Y, krec.Z);
                UpdatePlanLen();
            }
        }

        private void CBSelBackP_SelectedIndexChanged(object sender, EventArgs e) {
            if (CBSelBackP.SelectedIndex >= 0 && CBSelBackP.SelectedIndex < gbl.st7Data.KijunP.KPList.Count) {
                KikaiMan km = gbl.KikaiMan;
                KijunPRecEx brec = gbl.st7Data.KijunP.KPList[CBSelBackP.SelectedIndex];
                km.bp = new TINP3(brec.Name, brec.X, brec.Y, brec.Z);
                UpdatePlanLen();
            }
        }
        private void ChangeTSMode(bool isShow) {
            if (isShow) {
                if (Env.curTSMode0 == Env.TS_MODE_TUIBI) {
                    curTSMode = Env.curTSMode0;

                    gbl.UCCtrl.SetBtns2(true);
                }
            } else {
                if (curTSMode == Env.TS_MODE_TUIBI) {

                    gbl.UCCtrl.SetBtns2(gbl.FormMain.isModeKijun());
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

