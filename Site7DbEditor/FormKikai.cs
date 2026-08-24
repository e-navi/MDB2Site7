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

        // 3 Cards (Vertical layout)
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

        // Register Area Panel
        private Panel pnlRegister = null!;
        private TextBox txtNewPointName = null!;
        private ComboBox cmbNewPointLayer = null!;
        private Button btnRegister = null!;
        private Button btnClose = null!;

        // TS Measurement Timer
        private System.Windows.Forms.Timer timerMeasure = null!;
        private int _measuringTag = -1;
        private int _measureTimeoutCount = 0;

        private bool _isInitializing = true;

        public FormKikai()
        {
            gbl.FormKikai = this;
            InitializeComponent();
            BuildUi();
        }

        private void BuildUi()
        {
            this.Text = "器械点測定 (後方交会法)";
            this.ClientSize = new Size(336, 575);
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(242, 244, 248);
            this.Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Regular);

            timerMeasure = new System.Windows.Forms.Timer { Interval = 100 };
            timerMeasure.Tick += TimerMeasure_Tick;

            int pad = 10;
            int cardW = 314;

            // 1. Top Panel: Mode & Instrument/Mirror Heights (Height: 70)
            var pnlTop = new Panel
            {
                Location = new Point(pad, 8),
                Size = new Size(cardW, 68),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblMode = new Label
            {
                Text = "方式:",
                Location = new Point(6, 9),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };

            cmbMode = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(44, 6),
                Size = new Size(258, 24),
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };
            cmbMode.Items.AddRange(new object[] {
                "後方交会 ２点指定",
                "後方交会 ３点指定"
            });
            cmbMode.SelectedIndex = (gbl.KikaiMan0.kmode == gbl.KikaiMan0.KMODE_BI3) ? 1 : 0;
            cmbMode.SelectedIndexChanged += CmbMode_SelectedIndexChanged;

            var lblInstrH = new Label
            {
                Text = "器械高:",
                Location = new Point(6, 38),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Regular)
            };

            txtInstrH = new TextBox
            {
                Location = new Point(54, 35),
                Size = new Size(58, 23),
                Text = gbl.KikaiMan.kh > 0 ? gbl.KikaiMan.kh.ToString("F3") : "1.500",
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };
            txtInstrH.TextChanged += (s, e) => {
                if (double.TryParse(txtInstrH.Text.Trim(), out double khVal))
                {
                    gbl.KikaiMan0.kh = khVal;
                    gbl.KikaiMan.kh = khVal;
                    Recalculate();
                }
            };

            var lblUnit1 = new Label { Text = "m", Location = new Point(114, 38), AutoSize = true };

            var lblMirrorH = new Label
            {
                Text = "ミラー高:",
                Location = new Point(155, 38),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Regular)
            };

            txtMirrorH = new TextBox
            {
                Location = new Point(210, 35),
                Size = new Size(58, 23),
                Text = gbl.KikaiMan.mh > 0 ? gbl.KikaiMan.mh.ToString("F3") : "1.200",
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };
            txtMirrorH.TextChanged += (s, e) => {
                if (double.TryParse(txtMirrorH.Text.Trim(), out double mhVal))
                {
                    gbl.KikaiMan0.mh = mhVal;
                    gbl.KikaiMan.mh = mhVal;
                    Recalculate();
                }
            };

            var lblUnit2 = new Label { Text = "m", Location = new Point(270, 38), AutoSize = true };

            pnlTop.Controls.AddRange(new Control[] { lblMode, cmbMode, lblInstrH, txtInstrH, lblUnit1, lblMirrorH, txtMirrorH, lblUnit2 });
            this.Controls.Add(pnlTop);

            // 2. Middle: 3 Target Point Cards (Vertical Stack, Card Height: 116)
            int cardH = 116;
            string[] titles = new string[] { "📍 1点目 (左側・時計回り開始)", "📍 2点目 (右側)", "📍 3点目 (精度検定)" };
            Color[] cardColors = new Color[] { Color.FromArgb(235, 248, 255), Color.FromArgb(255, 240, 245), Color.FromArgb(255, 250, 235) };

            for (int i = 0; i < 3; i++)
            {
                int tag = i;
                var pnlCard = new Panel
                {
                    Location = new Point(pad, 82 + i * (cardH + 6)),
                    Size = new Size(cardW, cardH),
                    BackColor = cardColors[i],
                    BorderStyle = BorderStyle.FixedSingle
                };

                var lblTitle = new Label
                {
                    Text = titles[i],
                    Location = new Point(6, 5),
                    Size = new Size(cardW - 12, 18),
                    Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(20, 40, 80)
                };

                cmbPoints[i] = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(6, 26),
                    Size = new Size(205, 24),
                    Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold),
                    Tag = tag
                };
                cmbPoints[i].SelectedIndexChanged += CmbPoint_SelectedIndexChanged;

                btnMeasures[i] = new Button
                {
                    Text = "🔭 測定",
                    Location = new Point(218, 24),
                    Size = new Size(86, 27),
                    BackColor = Color.FromArgb(0, 150, 220),
                    ForeColor = Color.White,
                    Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold),
                    UseVisualStyleBackColor = false,
                    Enabled = false,
                    Tag = tag
                };
                btnMeasures[i].Click += BtnMeasure_Click;

                lblBMHeights[i] = new Label
                {
                    Text = "BM: --- m",
                    Location = new Point(6, 55),
                    Size = new Size(145, 18),
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(60, 60, 70)
                };

                lblDistances[i] = new Label
                {
                    Text = "水平距離: 未測定",
                    Location = new Point(152, 55),
                    Size = new Size(154, 18),
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 100, 200)
                };

                lblAngles[i] = new Label
                {
                    Text = "角度: ---",
                    Location = new Point(6, 75),
                    Size = new Size(300, 18),
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(80, 80, 90)
                };

                lblCalcZs[i] = new Label
                {
                    Text = "計算器械高: --- m",
                    Location = new Point(6, 94),
                    Size = new Size(300, 18),
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 130, 60)
                };

                pnlCard.Controls.AddRange(new Control[] {
                    lblTitle, cmbPoints[i], btnMeasures[i],
                    lblBMHeights[i], lblDistances[i], lblAngles[i], lblCalcZs[i]
                });

                cardPanels[i] = pnlCard;
                this.Controls.Add(pnlCard);
            }

            // 3. Calculation Results Card (Height: 120)
            pnlResult = new Panel
            {
                Location = new Point(pad, 326),
                Size = new Size(cardW, 120),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblResultStatus = new Label
            {
                Text = "⚡ 基準点を指定し、「🔭 測定」を実行してください。",
                Location = new Point(6, 6),
                Size = new Size(cardW - 12, 18),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204)
            };

            lblResultCoords = new Label
            {
                Text = "器械点座標:\n  X = --- m\n  Y = --- m\n  Z = --- m",
                Location = new Point(6, 26),
                Size = new Size(298, 56),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 20, 30)
            };

            lblResultResidual = new Label
            {
                Text = "残差: --- mm",
                Location = new Point(6, 85),
                Size = new Size(298, 30),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(70, 70, 80)
            };

            pnlResult.Controls.AddRange(new Control[] { lblResultStatus, lblResultCoords, lblResultResidual });
            this.Controls.Add(pnlResult);

            // 4. Bottom Register Area (Height: 75)
            pnlRegister = new Panel
            {
                Location = new Point(pad, 452),
                Size = new Size(cardW, 75),
                BackColor = Color.Transparent
            };

            var lblNewName = new Label
            {
                Text = "登録名:",
                Location = new Point(0, 5),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };

            txtNewPointName = new TextBox
            {
                Location = new Point(48, 2),
                Size = new Size(72, 23),
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };

            var lblNewLayer = new Label
            {
                Text = "レイヤ:",
                Location = new Point(130, 5),
                AutoSize = true,
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };

            cmbNewPointLayer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(176, 2),
                Size = new Size(136, 23),
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold)
            };

            btnRegister = new Button
            {
                Text = "💾 基準点リストへ登録",
                Location = new Point(0, 34),
                Size = new Size(210, 34),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                UseVisualStyleBackColor = false,
                Enabled = false
            };
            btnRegister.Click += BtnRegister_Click;

            btnClose = new Button
            {
                Text = "閉じる",
                Location = new Point(220, 34),
                Size = new Size(92, 34),
                BackColor = Color.FromArgb(220, 224, 230),
                Font = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold),
                UseVisualStyleBackColor = true
            };
            btnClose.Click += (s, e) => this.Close();

            pnlRegister.Controls.AddRange(new Control[] { lblNewName, txtNewPointName, lblNewLayer, cmbNewPointLayer, btnRegister, btnClose });
            this.Controls.Add(pnlRegister);

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
            int pad = 10;
            int cardW = 314;
            int cardH = 116;

            cardPanels[0].Location = new Point(pad, 82);
            cardPanels[1].Location = new Point(pad, 82 + (cardH + 6));
            cardPanels[2].Location = new Point(pad, 82 + (cardH + 6) * 2);
            cardPanels[2].Visible = is3Point;

            if (is3Point)
            {
                pnlResult.Location = new Point(pad, 82 + (cardH + 6) * 3);
                pnlRegister.Location = new Point(pad, 82 + (cardH + 6) * 3 + 126);
                this.ClientSize = new Size(336, 685);
            }
            else
            {
                pnlResult.Location = new Point(pad, 82 + (cardH + 6) * 2);
                pnlRegister.Location = new Point(pad, 82 + (cardH + 6) * 2 + 126);
                this.ClientSize = new Size(336, 565);
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
