using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace Site7DbEditor {
    public partial class FormKikai : Form {
        public FormKikai() {
            InitializeComponent();
        }
        public void SetKijunP() {
            cBox1.Items.Clear();
            cBox2.Items.Clear();
            cBox3.Items.Clear();

            foreach (KijunPRec rec in gbl.st7Data.KijunP.KPList) {
                cBox1.Items.Add(rec.Name);
                cBox2.Items.Add(rec.Name);
                cBox3.Items.Add(rec.Name);
            }
        }

        private void FormKikai_Load(object sender, EventArgs e) {
            St7Lib.CenterOnMainForm(this);
            //panel1.Width = 262;
            //this.Width = 279;
            comboBox1.SelectedIndex = gbl.KikaiMan.kmode;
            SetKijunP();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox1.SelectedIndex == 0) {
                label01.Text = "１点目";
                label02.Text = "２点目";
                buttonMesure01.Visible = true;
                panel03.Visible = false;
                gbl.KikaiMan0.kmode = gbl.KikaiMan0.KMODE_BI2;
            }
            if (comboBox1.SelectedIndex == 1) {
                label01.Text = "１点目";
                label02.Text = "２点目";
                label03.Text = "３点目";
                buttonMesure01.Visible = true;
                panel03.Visible = true;
                gbl.KikaiMan0.kmode = gbl.KikaiMan0.KMODE_BI3;
            }
            if (comboBox1.SelectedIndex == 2) {
                label01.Text = "器械点";
                label02.Text = "後視点";
                buttonMesure01.Visible = false;
                panel03.Visible = false;
                gbl.KikaiMan0.kmode = gbl.KikaiMan0.KMODE_KB;
            }
        }

        private void btnKikaiDef_Click(object sender, EventArgs e) {
            gbl.TStation.SetZero();
            gbl.KikaiMan0.calc();
            gbl.KikaiMan.set(gbl.KikaiMan0);
            Hide();
        }

        private void FormKikai_Activated(object sender, EventArgs e) {

        }

        private void FormKikai_Deactivate(object sender, EventArgs e) {
            gbl.FormMain.ShowZumen0();
        }

        private void cBox2_SelectedIndexChanged(object sender, EventArgs e) {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;

            int idx = cb.SelectedIndex;
            int tag = int.Parse((string)(cb.Tag));
            KikaiRec kr = gbl.KikaiMan0.kr[tag];
            KijunPRec rec = gbl.st7Data.KijunP.KPList[idx];

            kr.p.Name = rec.Name;
            kr.p.set(rec);
            if (tag == 0 && (comboBox1.SelectedIndex == 2)) {
                textBoxKH.Text = rec.Z.ToString("0.000"); 
                gbl.KikaiMan0.kh = rec.Z;
            }

            gbl.FormMain.ShowZumen0();
        }

        private void buttonMesure01_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;

            int tag = int.Parse((string)(btn.Tag));

            if (tag < 3) {
                KikaiRec kr = gbl.KikaiMan0.kr[tag];
                kr.angH = gbl.MField.angH;
                kr.angV = gbl.MField.angV;
                kr.lng = gbl.MField.lng;
                kr.isSet = true;
            }
            gbl.KikaiMan0.calc();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Hide();
        }

        private void btnSend14H_Click(object sender, EventArgs e) {
            string str;
            str = gbl.TStation.GetTCPRec(1, "\x14", 1);
        }

        private void BtnSend11H_Click(object sender, EventArgs e) {
            string str;
            str = gbl.TStation.GetTCPRec(1, "\x12", 1);
        }

        private void btnSendStr_Click(object sender, EventArgs e) {
            string str;
            str = gbl.TStation.GetTCPRec(1, textBoxSendStr.Text, 1);
        }
    }
}

