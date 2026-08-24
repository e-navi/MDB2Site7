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
        private ComboBox cmbMode = null!;
        private TextBox txtInstrH = null!;
        private TextBox txtMirrorH = null!;

        // 3 Cards
        private Panel[] cardPanels = new Panel[3];
        private ComboBox[] cmbPoints = new ComboBox[3];
        private Button[] btnMeasures = new Button[3];
        private Label[] lblBMHeights = new Label[3];
        private Label[] lblDistances = new Label[3];
        private Label[] lblAngles = new Label[3];
        private Label[] lblCalcZs = new Label[3];

        // Result Card
        private Panel pnlResult = null!;
        private Label lblResultStatus = null!;
        private Label lblResultCoords = null!;
        private Label lblResultResidual = null!;

        // Register Area
        private TextBox txtNewPointName = null!;
        private ComboBox cmbNewPointLayer = null!;
        private Button btnRegister = null!;
        private Button btnClose = null!;

        private bool _isInitializing = true;

        public FormKikai()
        {
            InitializeComponent();
            BuildUi();
        }

        private void BuildUi()
        {
            this.Text = "器械点測定 (後方交会法)";
            this.ClientSize = new Size(700, 620);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(242, 244, 248);
            this.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular);

            // Top Panel: Mode & Instrument/Mirror Heights
            var pnlTop = new Panel
            {
                Location = new Point(16, 12),
                Size = new Size(668, 55),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblMode = new Label
            {
                Text = "測定方式:",
                Location = new Point(12, 16),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };

            cmbMode = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(85, 13),
                Size = new Size(185, 28),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            cmbMode.Items.AddRange(new object[] {
                "後方交会 ２点指定",
                "後方交会 ３点指定"
            });
            cmbMode.SelectedIndex = (gbl.KikaiMan0.kmode == gbl.KikaiMan0.KMODE_BI3) ? 1 : 0;
            cmbMode.SelectedIndexChanged += CmbMode_SelectedIndexChanged;

            var lblInstrH = new Label
            {
                Text = "器械高(m):",
                Location = new Point(290, 16),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold)
            };

            txtInstrH = new TextBox
            {
                Location = new Point(365, 13),
                Size = new Size(65, 26),
                Text = gbl.KikaiMan.kh > 0 ? gbl.KikaiMan.kh.ToString("F3") : "1.500",
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            txtInstrH.TextChanged += (s, e) => {
                if (double.TryParse(txtInstrH.Text.Trim(), out double khVal))
                {
                    gbl.KikaiMan0.kh = khVal;
                    gbl.KikaiMan.kh = khVal;
                    Recalculate();
                }
            };

            var lblMirrorH = new Label
            {
                Text = "ミラー高(m):",
                Location = new Point(455, 16),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold)
            };

            txtMirrorH = new TextBox
            {
                Location = new Point(540, 13),
                Size = new Size(65, 26),
                Text = gbl.KikaiMan.mh > 0 ? gbl.KikaiMan.mh.ToString("F3") : "1.200",
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            txtMirrorH.TextChanged += (s, e) => {
                if (double.TryParse(txtMirrorH.Text.Trim(), out double mhVal))
                {
                    gbl.KikaiMan0.mh = mhVal;
                    gbl.KikaiMan.mh = mhVal;
                    Recalculate();
                }
            };

            pnlTop.Controls.AddRange(new Control[] { lblMode, cmbMode, lblInstrH, txtInstrH, lblMirrorH, txtMirrorH });
            this.Controls.Add(pnlTop);

            // Middle: 3 Target Point Cards
            int cardW = 216;
            int cardH = 225;
            string[] titles = new string[] { "📍 1点目 (左側・時計回り開始)", "📍 2点目 (右側)", "📍 3点目 (精度検定)" };
            Color[] cardColors = new Color[] { Color.FromArgb(235, 248, 255), Color.FromArgb(255, 240, 245), Color.FromArgb(255, 250, 235) };

            for (int i = 0; i < 3; i++)
            {
                int tag = i;
                var pnlCard = new Panel
                {
                    Location = new Point(16 + i * (cardW + 10), 75),
                    Size = new Size(cardW, cardH),
                    BackColor = cardColors[i],
                    BorderStyle = BorderStyle.FixedSingle
                };

                var lblTitle = new Label
                {
                    Text = titles[i],
                    Location = new Point(8, 8),
                    Size = new Size(cardW - 16, 20),
                    Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(20, 40, 80)
                };

                var lblSelect = new Label { Text = "基準点選択:", Location = new Point(8, 34), AutoSize = true, Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold) };

                cmbPoints[i] = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(8, 54),
                    Size = new Size(115, 26),
                    Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                    Tag = tag
                };
                cmbPoints[i].SelectedIndexChanged += CmbPoint_SelectedIndexChanged;

                btnMeasures[i] = new Button
                {
                    Text = "🔭 測定",
                    Location = new Point(128, 53),
                    Size = new Size(76, 28),
                    BackColor = Color.FromArgb(0, 150, 220),
                    ForeColor = Color.White,
                    Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold),
                    UseVisualStyleBackColor = false,
                    Enabled = false,
                    Tag = tag
                };
                btnMeasures[i].Click += BtnMeasure_Click;

                lblBMHeights[i] = new Label
                {
                    Text = "BM標高: --- m",
                    Location = new Point(8, 88),
                    Size = new Size(cardW - 16, 20),
                    ForeColor = Color.FromArgb(50, 50, 60)
                };

                lblDistances[i] = new Label
                {
                    Text = "水平距離: --- m",
                    Location = new Point(8, 112),
                    Size = new Size(cardW - 16, 36),
                    ForeColor = Color.FromArgb(20, 80, 160),
                    Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold)
                };

                lblAngles[i] = new Label
                {
                    Text = "角度: ---",
                    Location = new Point(8, 152),
                    Size = new Size(cardW - 16, 34),
                    ForeColor = Color.FromArgb(80, 80, 90),
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular)
                };

                lblCalcZs[i] = new Label
                {
                    Text = "計算器械高: --- m",
                    Location = new Point(8, 190),
                    Size = new Size(cardW - 16, 22),
                    ForeColor = Color.FromArgb(0, 120, 60),
                    Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold)
                };

                pnlCard.Controls.AddRange(new Control[] {
                    lblTitle, lblSelect, cmbPoints[i], btnMeasures[i],
                    lblBMHeights[i], lblDistances[i], lblAngles[i], lblCalcZs[i]
                });

                cardPanels[i] = pnlCard;
                this.Controls.Add(pnlCard);
            }

            // Bottom: Calculation Results Card
            pnlResult = new Panel
            {
                Location = new Point(16, 310),
                Size = new Size(668, 185),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblResultStatus = new Label
            {
                Text = "⚡ 基準点を指定し、「🔭 測定」を実行してください。",
                Location = new Point(12, 10),
                Size = new Size(640, 24),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204)
            };

            lblResultCoords = new Label
            {
                Text = "器械点座標:\n  X = --- m\n  Y = --- m\n  Z = --- m (算出標高)",
                Location = new Point(16, 40),
                Size = new Size(340, 95),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 20, 30)
            };

            lblResultResidual = new Label
            {
                Text = "交点決定条件:\n  1点目を左側・2点目を右側 (時計回り)\n\n残差 (精度):\n  2D誤差: --- mm\n  標高差: --- mm",
                Location = new Point(365, 40),
                Size = new Size(290, 130),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(60, 60, 70)
            };

            pnlResult.Controls.AddRange(new Control[] { lblResultStatus, lblResultCoords, lblResultResidual });
            this.Controls.Add(pnlResult);

            // Bottom Register Controls
            var lblNewName = new Label
            {
                Text = "登録基準点名:",
                Location = new Point(16, 515),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold)
            };

            txtNewPointName = new TextBox
            {
                Location = new Point(105, 512),
                Size = new Size(110, 26),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };

            var lblNewLayer = new Label
            {
                Text = "レイヤ:",
                Location = new Point(230, 515),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold)
            };

            cmbNewPointLayer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(280, 512),
                Size = new Size(130, 26),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold)
            };

            btnRegister = new Button
            {
                Text = "💾 基準点リストへ登録",
                Location = new Point(430, 508),
                Size = new Size(155, 34),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold),
                UseVisualStyleBackColor = false,
                Enabled = false
            };
            btnRegister.Click += BtnRegister_Click;

            btnClose = new Button
            {
                Text = "閉じる",
                Location = new Point(595, 508),
                Size = new Size(88, 34),
                BackColor = Color.FromArgb(220, 224, 230),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                UseVisualStyleBackColor = true
            };
            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblNewName, txtNewPointName, lblNewLayer, cmbNewPointLayer, btnRegister, btnClose });

            this.Load += FormKikai_Load;
            this.FormClosed += (s, e) => {
                gbl.FormMain?.InvalidateMap();
            };
        }

        private void FormKikai_Load(object? sender, EventArgs e)
        {
            St7Lib.CenterOnMainForm(this);
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

                // If KikaiMan0 already has points
                if (gbl.KikaiMan0.kr[i] != null && !string.IsNullOrEmpty(gbl.KikaiMan0.kr[i].p.Name))
                {
                    int idx = cmbPoints[i].FindString(gbl.KikaiMan0.kr[i].p.Name);
                    if (idx >= 0) cmbPoints[i].SelectedIndex = idx;
                }
            }

            // Defaults: 1st point = 0, 2nd point = 1 if available
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
            cardPanels[2].Visible = is3Point;

            if (is3Point)
            {
                cardPanels[0].Size = new Size(216, 225);
                cardPanels[1].Size = new Size(216, 225);
                cardPanels[0].Location = new Point(16, 75);
                cardPanels[1].Location = new Point(16 + 216 + 10, 75);
                cardPanels[2].Location = new Point(16 + (216 + 10) * 2, 75);
            }
            else
            {
                int cardW2 = 328;
                cardPanels[0].Size = new Size(cardW2, 225);
                cardPanels[1].Size = new Size(cardW2, 225);
                cardPanels[0].Location = new Point(16, 75);
                cardPanels[1].Location = new Point(16 + cardW2 + 12, 75);
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
                kr.isSet = false; // reset measurement until measured

                lblBMHeights[tag].Text = $"BM標高: {kikai.Z:F3} m";
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

            // TSから値を取得
            double lng = gbl.MField.lng;
            double angH = gbl.MField.angH;
            double angV = gbl.MField.angV;

            // 未接続・0測定時のマニュアル入力/シミュレーションフォールバック
            if (lng <= 0.001)
            {
                string prompt = $"【{kr.p.Name}】への測定距離（斜距離 m）を入力してください:";
                string defaultVal = tag == 0 ? "12.500" : (tag == 1 ? "14.200" : "15.000");
                string input = Microsoft.VisualBasic.Interaction.InputBox(prompt, "器械点測定（手動距離入力）", defaultVal);

                if (!double.TryParse(input, out lng) || lng <= 0)
                {
                    return;
                }

                // 仰角 = 90度（水平）、水平角 = tagに応じた時計回り角度
                angV = 0.25; // 90 deg zenith
                angH = (tag == 0) ? 0.0 : (tag == 1 ? 0.15 : 0.30);
            }

            kr.lng = lng;
            kr.angH = angH;
            kr.angV = angV;
            kr.isSet = true;

            // Update UI card for this point
            double lngH = kr.getLngH();
            double kh = gbl.KikaiMan0.kh;
            double mh = gbl.KikaiMan0.mh;
            double calcZ = kr.p.Z + mh - kr.getLngV() - kh;

            lblDistances[tag].Text = $"水平距離: {lngH:F3} m\n(斜距離: {lng:F3} m)";
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
                lblResultCoords.Text = "器械点座標:\n  X = --- m\n  Y = --- m\n  Z = --- m (算出標高)";
                lblResultResidual.Text = "交点決定条件:\n  1点目を左側・2点目を右側 (時計回り)\n\n残差 (精度):\n  2D誤差: --- mm\n  標高差: --- mm";
                btnRegister.Enabled = false;
                gbl.FormMain?.InvalidateMap();
                return;
            }

            bool ok = gbl.KikaiMan0.calc();
            if (ok && gbl.KikaiMan0.isCalced)
            {
                var kp = gbl.KikaiMan0.kp;
                lblResultStatus.Text = is3Point
                    ? "✔ 後方交会 3点指定による器械点計算が完了しました。"
                    : "✔ 後方交会 2点指定による器械点計算が完了しました（1点目左・時計回り交点）。";
                lblResultStatus.ForeColor = Color.FromArgb(0, 150, 50);

                lblResultCoords.Text = $"器械点座標:\n  X = {kp.X:F3} m\n  Y = {kp.Y:F3} m\n  Z = {kp.Z:F3} m (平均器械点標高)";

                if (is3Point)
                {
                    lblResultResidual.Text = $"交点精度 (3点重心):\n  2D交点残差: {gbl.KikaiMan0.residual2D:F1} mm\n  標高Z残差: {gbl.KikaiMan0.residualZ:F1} mm";
                }
                else
                {
                    lblResultResidual.Text = $"交点決定条件:\n  1点目左側・時計回り交点\n  標高Z残差: {gbl.KikaiMan0.residualZ:F1} mm";
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
            gbl.KikaiMan.bp.set(gbl.KikaiMan0.kr[0].p); // 後視点に1点目を設定

            // TSゼロセット
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
