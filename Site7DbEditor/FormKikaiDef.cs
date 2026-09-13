using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public partial class FormKikaiDef : Form
    {
        private int curTSMode = 0;
        private int _measureTimeoutCount = 0;

        public FormKikaiDef()
        {
            InitializeComponent();
        }

        private void FormKikaiDef_Shown(object? sender, EventArgs e)
        {
            if (gbl.FormMain != null && !gbl.FormMain.IsDisposed)
            {
                int posX = gbl.FormMain.Location.X + 80;
                int posY = gbl.FormMain.Location.Y + 60;
                this.Location = new Point(posX, posY);
            }

            CBSelKikaiP.Items.Clear();
            CBSelBackP.Items.Clear();
            int kidx = 0;
            int bidx = 0;
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;

            if (ts != null)
            {
                ts.isKikaiDefSet = false;
            }

            var db = gbl.FormMain?.Db;
            if (db != null && db.KikaiList.Count > 0)
            {
                for (int i = 0; i < db.KikaiList.Count; i++)
                {
                    var k = db.KikaiList[i];
                    string name = string.IsNullOrEmpty(k.Name) ? $"K{k.Id}" : k.Name;
                    CBSelKikaiP.Items.Add(name);
                    CBSelBackP.Items.Add(name);
                    if (km.kp != null && km.kp.Name == name) kidx = i;
                    if (km.bp != null && km.bp.Name == name) bidx = i;
                }
            }

            if (CBSelKikaiP.Items.Count > 0)
            {
                CBSelKikaiP.SelectedIndex = kidx >= 0 && kidx < CBSelKikaiP.Items.Count ? kidx : 0;
            }
            if (CBSelBackP.Items.Count > 0)
            {
                CBSelBackP.SelectedIndex = bidx >= 0 && bidx < CBSelBackP.Items.Count ? bidx : (CBSelBackP.Items.Count > 1 ? 1 : 0);
            }
            UpdatePlanLen();
        }

        private void UpdatePlanLen()
        {
            KikaiMan km = gbl.KikaiMan;
            if (km.kp != null && km.bp != null)
            {
                double len1 = km.kp.CalcLen(km.bp);
                L_Len1.Text = $"{len1:F3} m";
            }
            else
            {
                L_Len1.Text = "--- m";
            }
        }

        private void CBSelKikaiP_SelectedIndexChanged(object? sender, EventArgs e)
        {
            KikaiMan km = gbl.KikaiMan;
            var db = gbl.FormMain?.Db;
            string selName = CBSelKikaiP.SelectedItem?.ToString() ?? "";

            if (db != null)
            {
                var kikai = db.KikaiList.FirstOrDefault(k => (!string.IsNullOrEmpty(k.Name) && k.Name == selName) || $"K{k.Id}" == selName);
                if (kikai != null)
                {
                    km.kp = new TINP3(kikai.Name, kikai.X, kikai.Y, kikai.Z);
                    UpdatePlanLen();
                }
            }
        }

        private void CBSelBackP_SelectedIndexChanged(object? sender, EventArgs e)
        {
            KikaiMan km = gbl.KikaiMan;
            var db = gbl.FormMain?.Db;
            string selName = CBSelBackP.SelectedItem?.ToString() ?? "";

            if (db != null)
            {
                var kikai = db.KikaiList.FirstOrDefault(k => (!string.IsNullOrEmpty(k.Name) && k.Name == selName) || $"K{k.Id}" == selName);
                if (kikai != null)
                {
                    km.bp = new TINP3(kikai.Name, kikai.X, kikai.Y, kikai.Z);
                    UpdatePlanLen();
                }
            }
        }

        private void buttonMesure01_Click(object? sender, EventArgs e)
        {
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;

            if (km.kp == null || km.bp == null)
            {
                MessageBox.Show(this, "器械点と後視点を指定してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double len1 = km.kp.CalcLen(km.bp);
            if (len1 <= 0.0001)
            {
                MessageBox.Show(this, "器械点と後視点には異なる点を指定してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            buttonMesure01.Enabled = false;
            buttonMesure01.Text = "測定中...";
            _measureTimeoutCount = 0;

            if (ts != null)
            {
                ts.isKikaiDefSet = true;
                timer1.Enabled = true;
                try
                {
                    ts.AS_BtnClick_3();
                }
                catch
                {
                    ts.isKikaiDefSet = false;
                }
            }
            else
            {
                // 手動フォールバック
                ProcessResult();
            }
        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            TStation ts = gbl.TStation;
            _measureTimeoutCount++;

            if (ts != null && ts.isKikaiDefSet)
            {
                if (_measureTimeoutCount < 100)
                {
                    return;
                }
                ts.isKikaiDefSet = false;
            }

            timer1.Enabled = false;
            ProcessResult();
        }

        private void ProcessResult()
        {
            buttonMesure01.Enabled = true;
            buttonMesure01.Text = "🔭 測定開始";

            KikaiMan km = gbl.KikaiMan;
            TStation ts = gbl.TStation;

            double len1 = (km.kp != null && km.bp != null) ? km.kp.CalcLen(km.bp) : 0.0;
            double len2 = 0.0;

            if (gbl.MField.lng > 0.0)
            {
                double radV = gbl.MField.angV * 2.0 * Math.PI;
                len2 = gbl.MField.lng * Math.Sin(radV);
                if (len2 <= 0.0)
                {
                    len2 = gbl.MField.lng;
                }
            }
            else if (km.kp != null && ts?.curPos != null && (ts.curPos.X != 0 || ts.curPos.Y != 0))
            {
                len2 = km.kp.CalcLen(ts.curPos);
            }

            double err = len1 - len2;
            L_Len1.Text = $"{len1:F3} m";
            L_Len2.Text = $"{len2:F3} m";
            L_Len3.Text = $"{(err >= 0 ? "+" : "")}{err:F3} m";
            L_Len3.ForeColor = Math.Abs(err) < 0.010 ? Color.FromArgb(0, 140, 60) : Color.FromArgb(220, 50, 0);

            button1.Enabled = true;
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            TStation ts = gbl.TStation;
            KikaiMan km = gbl.KikaiMan;

            if (km.kp == null || km.bp == null)
            {
                MessageBox.Show(this, "器械点と後視点を指定してください", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Def.SetIniStr("TS", "器械点", km.kp.Name);
            Def.SetIniStr("TS", "後視点", km.bp.Name);
            Env.KPName = km.kp.Name;
            Env.BPName = km.bp.Name;

            km.angK = km.calc2PAng(km.kp, km.bp);

            if (ts != null)
            {
                try
                {
                    ts.SetFILD(false);
                    ts.SetZero();
                    ts.SetFILD(true);
                }
                catch { }

                if (!ts.isLN100)
                {
                    km.ang0 = 0.0;
                }
            }

            gbl.FormMain?.EnsureKikaiPointVisible();
            gbl.FormMain?.ShowZumen0();
            gbl.FormMain?.InvalidateMap();

            MessageBox.Show(this,
                $"器械点【{km.kp.Name}】、後視点【{km.bp.Name}】を設定し、\nTSの0セットを完了しました。",
                "器械点・後視点設定完了",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeTSMode(bool isShow)
        {
            if (isShow)
            {
                if (Env.curTSMode0 == Env.TS_MODE_TUIBI)
                {
                    curTSMode = Env.curTSMode0;
                    gbl.UCCtrl?.SetBtns2(true);
                }
            }
            else
            {
                if (curTSMode == Env.TS_MODE_TUIBI)
                {
                    bool isKijun = gbl.FormMain?.isModeKijun() ?? false;
                    gbl.UCCtrl?.SetBtns2(isKijun);
                }
            }
        }

        private void FormKikaiDef_FormClosed(object? sender, FormClosedEventArgs e)
        {
            ChangeTSMode(false);
            gbl.FormMain?.InvalidateMap();
        }

        private void FormKikaiDef_VisibleChanged(object? sender, EventArgs e)
        {
            ChangeTSMode(Visible);
        }
    }
}
