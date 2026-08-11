using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Site7 {
    public partial class FormDefLayer : Form {
        private St7Data st7Data;
        public FormDefLayer(St7Data data) {
            InitializeComponent();
            this.st7Data = data;
        }

        private void FormDefLayer_Load(object sender, EventArgs e) {
            CBoxColor.Items.Clear();
            foreach (ColorRec rec in st7Data.ColorTbl.ColorList) {
                CBoxColor.Items.Add(rec.Name);
            }
            comboBoxLayerG.SelectedIndex = 0;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            int n = comboBoxLayerG.SelectedIndex * 16;
            int i = listBox1.SelectedIndex;
            LayerRec rec = st7Data.Layer.LayerList[n+i+1];
            textBox1.Text = rec.Name;
            if (0 < rec.Color) {
                CBoxColor.SelectedIndex = rec.Color - 1;
            } else {
                CBoxColor.SelectedIndex = 0;
            }
            //PEN1.BackColor = st7Data.ColorTbl.ColorList[rec.Color-1].Color;
            CBoxMark.SelectedIndex = (rec.Mark < 1) ? 0 : rec.Mark - 1;
            CBoxSize.Text = rec.Size.ToString();
            CBoxWidth.SelectedIndex = (rec.Width < 1)? 0: rec.Width - 1;
            CBoxLineStyle.SelectedIndex = (rec.LType == 2) ? 1 : 0;
        }

        private void button1_Click(object sender, EventArgs e) {
            int n = comboBoxLayerG.SelectedIndex * 16;
            int i = listBox1.SelectedIndex;
            LayerRec rec = st7Data.Layer.LayerList[n+i + 1];
            rec.Name = textBox1.Text;
            rec.Color = CBoxColor.SelectedIndex+1;
            rec.Mark = CBoxMark.SelectedIndex + 1;
            rec.Size = St7Lib.CheckDouble(CBoxSize.Text, 2);
            rec.Width = CBoxWidth.SelectedIndex + 1;
            rec.LType = CBoxLineStyle.SelectedIndex + 1;
            st7Data.st7DB.SetLayerRec(n+i+1, rec);
            listBox1.Items[i] = "L" + rec.ID.ToString("00") + " " + rec.Name;
        }

        private void Cancel_Button_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void comboBoxLayerG_SelectedIndexChanged(object sender, EventArgs e) {
            listBox1.Items.Clear();
            int n = comboBoxLayerG.SelectedIndex * 16;
            for (int i = 1; i <= 16; i++) {
                LayerRec rec = st7Data.Layer.LayerList[n+i];
                listBox1.Items.Add("L" + i.ToString("00") + " " + rec.Name);
            }
            listBox1.SetSelected(0, true);
        }

        private void CBoxColor_SelectedIndexChanged(object sender, EventArgs e) {
            Color col = st7Data.ColorTbl.ColorList[CBoxColor.SelectedIndex].Color;
            if (col == Color.White) {
                //PEN1.BackColor = Color.Black;
            } else {
                //PEN1.BackColor = Color.White;
            }
            CBoxColor.ForeColor = col;
        }

        private void CBoxColor_DrawItem(object sender, DrawItemEventArgs e) {
            e.DrawBackground();

            ComboBox cmb = (ComboBox)sender;
            //項目に表示する文字列
            string txt = e.Index > -1 ? cmb.Items[e.Index].ToString() : cmb.Text;
            //使用するフォント
            Font f = new Font(txt, cmb.Font.Size);
            //使用するブラシ
            Color col = e.Index > -1 ? st7Data.ColorTbl.ColorList[e.Index].Color : e.ForeColor;
            Brush b = new SolidBrush(col);
            //文字列を描画する
            float ym =
                (e.Bounds.Height - e.Graphics.MeasureString(txt, f).Height) / 2;
            e.Graphics.DrawString(txt, f, b, e.Bounds.X, e.Bounds.Y + ym);

            f.Dispose();
            b.Dispose();

            //フォーカスを示す四角形を描画
            e.DrawFocusRectangle();
        }
    }
}
