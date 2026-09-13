using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public partial class FormKikai : Form
    {
        // 3 Cards (Vertical layout)
        private Panel[] cardPanels = null!;
        private ComboBox[] cmbPoints = null!;
        private Button[] btnMeasures = null!;
        private Label[] lblBMHeights = null!;
        private Label[] lblDistances = null!;
        private Label[] lblAngles = null!;
        private Label[] lblCalcZs = null!;

        // TS Measurement
        private int _measuringTag = -1;
        private int _measureTimeoutCount = 0;

        private bool _isInitializing = true;

        public FormKikai()
        {
            gbl.FormKikai = this;
            InitializeComponent();

            cardPanels = new Panel[] { pnlCard1, pnlCard2, pnlCard3 };
            cmbPoints = new ComboBox[] { cmbPoint1, cmbPoint2, cmbPoint3 };
            btnMeasures = new Button[] { btnMeasure1, btnMeasure2, btnMeasure3 };
            lblBMHeights = new Label[] { lblBMHeight1, lblBMHeight2, lblBMHeight3 };
            lblDistances = new Label[] { lblDistance1, lblDistance2, lblDistance3 };
            lblAngles = new Label[] { lblAngle1, lblAngle2, lblAngle3 };
            lblCalcZs = new Label[] { lblCalcZ1, lblCalcZ2, lblCalcZ3 };

            for (int i = 0; i < 3; i++)
            {
                int tag = i;
                cmbPoints[i].Tag = tag;
                cmbPoints[i].SelectedIndexChanged += CmbPoint_SelectedIndexChanged;
                btnMeasures[i].Tag = tag;
                btnMeasures[i].Click += BtnMeasure_Click;
            }

            txtInstrH.Text = gbl.KikaiMan.kh > 0 ? gbl.KikaiMan.kh.ToString("F3") : "1.500";
            txtInstrH.TextChanged += (s, e) => {
                if (double.TryParse(txtInstrH.Text.Trim(), out double khVal))
                {
                    gbl.KikaiMan0.kh = khVal;
                    gbl.KikaiMan.kh = khVal;
                    Recalculate();
                }
            };

            txtMirrorH.Text = gbl.KikaiMan.mh > 0 ? gbl.KikaiMan.mh.ToString("F3") : "1.200";
            txtMirrorH.TextChanged += (s, e) => {
                if (double.TryParse(txtMirrorH.Text.Trim(), out double mhVal))
                {
                    gbl.KikaiMan0.mh = mhVal;
                    gbl.KikaiMan.mh = mhVal;
                    Recalculate();
                }
            };

            cmbMode.SelectedIndex = (gbl.KikaiMan0.kmode == gbl.KikaiMan0.KMODE_BI3) ? 1 : 0;
            btnClose.Click += (s, e) => this.Close();

            this.Load += FormKikai_Load;
            this.FormClosed += (s, e) => {
                gbl.FormMain?.InvalidateMap();
            };
        }

        private void FormKikai_Load(object? sender, EventArgs e)
        {
            gbl.FormMain?.SelectKikaiTab();

            // Position dialog neatly on the left side of the main form to maximize map visibility
            if (gbl.FormMain != null && !gbl.FormMain.IsDisposed)
            {
                int posX = gbl.FormMain.Location.X + 80;
                int posY = gbl.FormMain.Location.Y + 60;
                this.Location = new Point(posX, posY);
            }

            LoadKijunPoints();
            LoadLayerCombo();
            SuggestNextKikaiName();
            UpdateCardVisibility();
            _isInitializing = false;
        }

        private void LoadKijunPoints()
        {
            var db = gbl.FormMain?.Db;
            if (db == null) return;

            for (int i = 0; i < 3; i++)
            {
                cmbPoints[i].Items.Clear();
                foreach (var k in db.KikaiList)
                {
                    string label = string.IsNullOrEmpty(k.Name) ? $"K{k.Id}" : k.Name;
                    cmbPoints[i].Items.Add(label);
                }

                if (gbl.KikaiMan0.kr[i] != null && !string.IsNullOrEmpty(gbl.KikaiMan0.kr[i].p.Name))
                {
                    int idx = cmbPoints[i].FindString(gbl.KikaiMan0.kr[i].p.Name);
                    if (idx >= 0) cmbPoints[i].SelectedIndex = idx;
                }
            }

            if (cmbPoints[0].SelectedIndex < 0 && cmbPoints[0].Items.Count > 0)
                cmbPoints[0].SelectedIndex = 0;
            if (cmbPoints[1].SelectedIndex < 0 && cmbPoints[1].Items.Count > 1)
                cmbPoints[1].SelectedIndex = 1;
            if (cmbPoints[2].SelectedIndex < 0 && cmbPoints[2].Items.Count > 2)
                cmbPoints[2].SelectedIndex = 2;
        }

        private void LoadLayerCombo()
        {
            cmbNewPointLayer.Items.Clear();
            var items = LayerDefinitionService.Instance.Groups.TryGetValue(LayerGroup.Kikai, out var list)
                ? list
                : LayerDefinitionService.CreateDefaultLayers(LayerGroup.Kikai);

            foreach (var item in items)
            {
                cmbNewPointLayer.Items.Add(item.DisplayText);
            }

            if (cmbNewPointLayer.Items.Count > 0)
                cmbNewPointLayer.SelectedIndex = 0;
        }

        private void SuggestNextKikaiName()
        {
            var db = gbl.FormMain?.Db;
            if (db == null || db.KikaiList.Count == 0)
            {
                txtNewPointName.Text = "K01";
                return;
            }

            int maxNum = 0;
            foreach (var k in db.KikaiList)
            {
                string name = k.Name ?? "";
                if (name.StartsWith("K", StringComparison.OrdinalIgnoreCase) && int.TryParse(name.Substring(1), out int num))
                {
                    if (num > maxNum) maxNum = num;
                }
            }

            txtNewPointName.Text = $"K{(maxNum + 1):D2}";
        }

        private void UpdateCardVisibility()
        {
            bool is3Point = (cmbMode.SelectedIndex == 1);
            int pad = 14;
            int cardH = 124;
            int cardSpacing = 6;
            int topOffset = 94;

            cardPanels[0].Location = new Point(pad, topOffset);
            cardPanels[1].Location = new Point(pad, topOffset + (cardH + cardSpacing));
            cardPanels[2].Location = new Point(pad, topOffset + (cardH + cardSpacing) * 2);
            cardPanels[2].Visible = is3Point;

            if (is3Point)
            {
                pnlResult.Location = new Point(pad, topOffset + (cardH + cardSpacing) * 3);
                pnlRegister.Location = new Point(pad, pnlResult.Bottom + 6);
                this.ClientSize = new Size(480, pnlRegister.Bottom + 10);
            }
            else
            {
                pnlResult.Location = new Point(pad, topOffset + (cardH + cardSpacing) * 2);
                pnlRegister.Location = new Point(pad, pnlResult.Bottom + 6);
                this.ClientSize = new Size(480, pnlRegister.Bottom + 10);
            }
        }

        private void CmbMode_SelectedIndexChanged(object? sender, EventArgs e)
        {
            gbl.KikaiMan0.kmode = (cmbMode.SelectedIndex == 1) ? gbl.KikaiMan0.KMODE_BI3 : gbl.KikaiMan0.KMODE_BI2;
            UpdateCardVisibility();
            Recalculate();
        }

        private void CmbPoint_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender is not ComboBox cb || cb.Tag is not int tag) return;

            var db = gbl.FormMain?.Db;
            if (db == null || cb.SelectedIndex < 0)
            {
                btnMeasures[tag].Enabled = false;
                return;
            }

            string selName = cb.SelectedItem?.ToString() ?? "";
            var kikai = db.KikaiList.FirstOrDefault(k => (!string.IsNullOrEmpty(k.Name) && k.Name == selName) || $"K{k.Id}" == selName);

            if (kikai != null)
            {
                var kr = gbl.KikaiMan0.kr[tag];
                kr.p.Name = kikai.Name;
                kr.p.X = kikai.X;
                kr.p.Y = kikai.Y;
                kr.p.Z = kikai.Z;
                kr.isSet = false;

                lblBMHeights[tag].Text = $"BM: {kikai.Z:F3} m";
                lblDistances[tag].Text = "水平距離: 未測定";
                lblAngles[tag].Text = "角度: 未測定";
                lblCalcZs[tag].Text = "計算器械高: --- m";

                btnMeasures[tag].Enabled = true;
            }
            else
            {
                btnMeasures[tag].Enabled = false;
            }

            Recalculate();
        }

        private void BtnMeasure_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not int tag) return;

            var kr = gbl.KikaiMan0.kr[tag];
            if (string.IsNullOrEmpty(kr.p.Name)) return;

            _measuringTag = tag;
            _measureTimeoutCount = 0;

            TStation ts = gbl.TStation;
            if (ts != null)
            {
                ts.isKikaiDefSet = true;
                timerMeasure.Enabled = true;
                btnMeasures[tag].Enabled = false;
                btnMeasures[tag].Text = "測定中...";

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
                ProcessMeasurementResult();
            }
        }

        private void TimerMeasure_Tick(object? sender, EventArgs e)
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

            timerMeasure.Enabled = false;
            ProcessMeasurementResult();
        }

        private void ProcessMeasurementResult()
        {
            if (_measuringTag < 0 || _measuringTag >= 3) return;
            int tag = _measuringTag;

            btnMeasures[tag].Enabled = true;
            btnMeasures[tag].Text = "🔭 測定";

            var kr = gbl.KikaiMan0.kr[tag];
            if (string.IsNullOrEmpty(kr.p.Name)) return;

            double lng = gbl.MField.lng;
            double angH = gbl.MField.angH;
            double angV = gbl.MField.angV;

            if (lng <= 0.001)
            {
                string prompt = $"【{kr.p.Name}】への測定距離（斜距離 m）を入力してください:";
                string defaultVal = tag == 0 ? "12.500" : (tag == 1 ? "14.200" : "15.000");
                string input = Microsoft.VisualBasic.Interaction.InputBox(prompt, "器械点測定（手動距離入力）", defaultVal);

                if (!double.TryParse(input, out lng) || lng <= 0)
                {
                    return;
                }

                angV = 0.25;
                angH = (tag == 0) ? 0.0 : (tag == 1 ? 0.15 : 0.30);
            }

            kr.lng = lng;
            kr.angH = angH;
            kr.angV = angV;
            kr.isSet = true;

            double lngH = kr.getLngH();
            double kh = gbl.KikaiMan0.kh;
            double mh = gbl.KikaiMan0.mh;
            double calcZ = kr.p.Z + mh - kr.getLngV() - kh;

            lblDistances[tag].Text = $"水平: {lngH:F3} m ({lng:F3}m)";
            lblAngles[tag].Text = $"仰角: {(angV * 360.0):F1}°  水平角: {(angH * 360.0):F1}°";
            lblCalcZs[tag].Text = $"計算器械高: {calcZ:F3} m";

            Recalculate();
        }

        private void Recalculate()
        {
            if (_isInitializing) return;

            bool is3Point = (cmbMode.SelectedIndex == 1);
            bool ready = is3Point
                ? (gbl.KikaiMan0.kr[0].isSet && gbl.KikaiMan0.kr[1].isSet && gbl.KikaiMan0.kr[2].isSet)
                : (gbl.KikaiMan0.kr[0].isSet && gbl.KikaiMan0.kr[1].isSet);

            if (!ready)
            {
                lblResultStatus.Text = is3Point
                    ? "⏳ 1点目・2点目・3点目を指定して測定してください。"
                    : "⏳ 1点目（左側）と2点目（右側）を指定して測定してください。";
                lblResultStatus.ForeColor = Color.FromArgb(0, 102, 204);
                lblResultCoords.Text = "器械点座標:\n  X = --- m\n  Y = --- m\n  Z = --- m";
                lblResultResidual.Text = "交点決定条件:\n  1点目左側・時計回り交点";
                btnRegister.Enabled = false;
                gbl.FormMain?.InvalidateMap();
                return;
            }

            bool ok = gbl.KikaiMan0.calc();
            if (ok && gbl.KikaiMan0.isCalced)
            {
                var kp = gbl.KikaiMan0.kp;
                lblResultStatus.Text = is3Point
                    ? "✔ 3点後方交会 計算完了"
                    : "✔ 2点後方交会 計算完了（時計回り）";
                lblResultStatus.ForeColor = Color.FromArgb(0, 150, 50);

                lblResultCoords.Text = $"器械点座標:\n  X = {kp.X:F3} m\n  Y = {kp.Y:F3} m\n  Z = {kp.Z:F3} m";

                if (is3Point)
                {
                    lblResultResidual.Text = $"交点残差: 2D = {gbl.KikaiMan0.residual2D:F1} mm / Z = {gbl.KikaiMan0.residualZ:F1} mm";
                }
                else
                {
                    lblResultResidual.Text = $"標高Z残差: {gbl.KikaiMan0.residualZ:F1} mm (1点目左側交点)";
                }

                btnRegister.Enabled = true;
            }
            else
            {
                lblResultStatus.Text = $"❌ 計算エラー: {gbl.KikaiMan0.errMsg}";
                lblResultStatus.ForeColor = Color.Red;
                lblResultCoords.Text = "器械点座標:\n  X = --- m\n  Y = --- m\n  Z = --- m";
                btnRegister.Enabled = false;
            }

            gbl.FormMain?.InvalidateMap();
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            if (!gbl.KikaiMan0.isCalced || gbl.KikaiMan0.kp == null) return;

            var db = gbl.FormMain?.Db;
            if (db == null) return;

            string name = txtNewPointName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                SuggestNextKikaiName();
                name = txtNewPointName.Text.Trim();
            }

            int layer = cmbNewPointLayer.SelectedIndex + 1;
            if (layer < 1 || layer > 16) layer = 1;

            long newId = db.KikaiList.Count > 0 ? db.KikaiList.Max(k => k.Id) + 1 : 1;

            var newKikai = new KikaiModel
            {
                Id = newId,
                Name = name,
                Layer = layer,
                X = gbl.KikaiMan0.kp.X,
                Y = gbl.KikaiMan0.kp.Y,
                Z = gbl.KikaiMan0.kp.Z,
                Date = DateTime.Now.ToString("yyyy/MM/dd"),
                KPName = gbl.KikaiMan0.kr[0].p.Name,
                BPName = gbl.KikaiMan0.kr[1].p.Name,
                KPH = gbl.KikaiMan0.kh,
                MRH = gbl.KikaiMan0.mh
            };

            int addIdx = db.KikaiList.Count;
            db.KikaiList.Add(newKikai);

            // Undo/Redo Log
            gbl.FormMain?.LogService?.Push(EditorLogService.LOG_TYPE_NEW, EditorLogService.REC_TYPE_KIJUNP, newKikai, null, db.CurrentDbPath, addIdx);

            // KikaiManの確定設定
            gbl.KikaiMan.set(gbl.KikaiMan0);
            gbl.KikaiMan.kp.Name = name;
            gbl.KikaiMan.kp.X = newKikai.X;
            gbl.KikaiMan.kp.Y = newKikai.Y;
            gbl.KikaiMan.kp.Z = newKikai.Z;
            gbl.KikaiMan.bp.set(gbl.KikaiMan0.kr[0].p);

            try
            {
                gbl.TStation?.SetZero();
            }
            catch { }

            MessageBox.Show(this,
                $"新しい基準点【{name}】（X: {newKikai.X:F3}, Y: {newKikai.Y:F3}, Z: {newKikai.Z:F3}）を登録し、\n現在の器械点（後視点: {gbl.KikaiMan.bp.Name}）として設定しました。",
                "器械点登録完了",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            gbl.FormMain?.InvalidateMap();
            this.Close();
        }
    }
}
