using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public class FormLayerSettings : Form
    {
        private readonly EditorDbManager _db;
        private readonly string? _dbPath;

        private ComboBox comboBoxLayerG = null!;
        private ListBox listBox1 = null!;
        private TextBox textBox1 = null!;
        private ComboBox CBoxColor = null!;
        private ComboBox CBoxMark = null!;
        private ComboBox CBoxSize = null!;
        private ComboBox CBoxWidth = null!;
        private ComboBox CBoxLineStyle = null!;
        private Button button1 = null!;
        private Button btnExportToMaster = null!;
        private Button btnImportFromMaster = null!;
        private Button Save_Button = null!;
        private Button Cancel_Button = null!;
        private bool _isUpdatingUi = false;

        public FormLayerSettings(EditorDbManager? db, LayerGroup initialGroup = LayerGroup.Ikou)
            : this(db?.CurrentDbPath, initialGroup)
        {
            _db = db;
        }

        public FormLayerSettings(string? dbPath = null, LayerGroup initialGroup = LayerGroup.Ikou)
        {
            _dbPath = dbPath;
            LayerDefinitionService.Instance.LoadAll(_dbPath);
            InitializeComponent();

            int groupIdx = (int)initialGroup;
            if (groupIdx >= 0 && groupIdx < comboBoxLayerG.Items.Count)
            {
                comboBoxLayerG.SelectedIndex = groupIdx;
            }
            if (listBox1.Items.Count > 0 && listBox1.SelectedIndex < 0)
            {
                listBox1.SelectedIndex = 0;
            }
            UpdateRightEditControls();
        }

        private void InitializeComponent()
        {
            bool isMasterMode = string.IsNullOrEmpty(_dbPath);
            this.Text = isMasterMode ? "マスターレイヤ設定 (システム共通テンプレート)" : "現場レイヤ設定 (現場定義データ)";
            this.ClientSize = new Size(540, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular);

            // comboBoxLayerG
            comboBoxLayerG = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(16, 12),
                Size = new Size(215, 26),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold)
            };
            comboBoxLayerG.Items.AddRange(new object[] {
                "🏛 遺構 (Layer遺構.txt)",
                "🏺 遺物 (Layer遺物.txt)",
                "📍 基準点 (Layer基準点.txt)",
                "📏 作図 (Layer作図.txt)"
            });
            comboBoxLayerG.SelectedIndexChanged += ComboBoxLayerG_SelectedIndexChanged;

            // listBox1 (16件がスクロールバーなしでぴったり収まる高さ)
            listBox1 = new ListBox
            {
                Location = new Point(16, 45),
                Size = new Size(215, 305),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular),
                IntegralHeight = true
            };
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            // Labels
            var lblLayerName = CreateLabel("レイヤ名", new Point(245, 45));
            var lblColor = CreateLabel("表示色", new Point(245, 82));
            var lblMark = CreateLabel("マーク", new Point(245, 119));
            var lblSize = CreateLabel("サイズ", new Point(245, 156));
            var lblWidth = CreateLabel("線幅", new Point(245, 193));
            var lblLineStyle = CreateLabel("線種", new Point(245, 230));

            // Edit controls
            textBox1 = new TextBox
            {
                Location = new Point(320, 42),
                Size = new Size(185, 26),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };

            CBoxColor = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawFixed,
                Location = new Point(320, 79),
                Size = new Size(130, 26),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            CBoxColor.Items.AddRange(new object[] {
                "黒", "赤", "緑", "青", "黄", "マゼンタ", "シアン", "白",
                "牡丹", "茶", "橙", "薄緑", "明青", "青紫", "明灰", "暗灰"
            });
            CBoxColor.DrawItem += CBoxColor_DrawItem;

            CBoxMark = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(320, 116),
                Size = new Size(80, 26),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            CBoxMark.Items.AddRange(new object[] { "〇", "□", "△", "⦿", "✕", "＋", "◇", "★" });

            CBoxSize = new ComboBox
            {
                Location = new Point(320, 153),
                Size = new Size(80, 26),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            CBoxSize.Items.AddRange(new object[] { "0.5", "1.0", "1.5", "2.0", "3.0", "4.0", "5.0", "6.0", "8.0", "10.0" });

            CBoxWidth = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(320, 190),
                Size = new Size(80, 26),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            CBoxWidth.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });

            CBoxLineStyle = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(320, 227),
                Size = new Size(95, 26),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            CBoxLineStyle.Items.AddRange(new object[] { "折線", "曲線" });

            // リアルタイム編集連動
            textBox1.TextChanged += (s, e) => AutoApplyCurrentItem();
            CBoxColor.SelectedIndexChanged += (s, e) => AutoApplyCurrentItem();
            CBoxMark.SelectedIndexChanged += (s, e) => AutoApplyCurrentItem();
            CBoxSize.TextChanged += (s, e) => AutoApplyCurrentItem();
            CBoxSize.SelectedIndexChanged += (s, e) => AutoApplyCurrentItem();
            CBoxWidth.SelectedIndexChanged += (s, e) => AutoApplyCurrentItem();
            CBoxLineStyle.SelectedIndexChanged += (s, e) => AutoApplyCurrentItem();

            // Buttons
            button1 = new Button
            {
                Text = "✔ このレイヤに適用",
                Location = new Point(320, 268),
                Size = new Size(140, 28),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(230, 235, 245),
                UseVisualStyleBackColor = true
            };
            button1.Click += Button1_Click;

            btnExportToMaster = new Button
            {
                Text = "📤 マスターへ反映",
                Location = new Point(245, 308),
                Size = new Size(135, 30),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(233, 236, 243),
                ForeColor = Color.FromArgb(25, 45, 80),
                FlatStyle = FlatStyle.Flat,
                Visible = !isMasterMode
            };
            btnExportToMaster.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnExportToMaster.Click += BtnExportToMaster_Click;

            btnImportFromMaster = new Button
            {
                Text = "📥 マスターから反映",
                Location = new Point(388, 308),
                Size = new Size(135, 30),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(233, 236, 243),
                ForeColor = Color.FromArgb(25, 45, 80),
                FlatStyle = FlatStyle.Flat,
                Visible = !isMasterMode
            };
            btnImportFromMaster.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnImportFromMaster.Click += BtnImportFromMaster_Click;

            Save_Button = new Button
            {
                Text = "💾 設定を保存",
                Location = new Point(245, 350),
                Size = new Size(155, 34),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            Save_Button.FlatAppearance.BorderSize = 0;
            Save_Button.Click += Save_Button_Click;

            Cancel_Button = new Button
            {
                Text = "閉じる",
                Location = new Point(410, 350),
                Size = new Size(113, 34),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 225, 235),
                ForeColor = Color.FromArgb(30, 40, 60),
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };

            this.Controls.Add(comboBoxLayerG);
            this.Controls.Add(listBox1);
            this.Controls.Add(lblLayerName);
            this.Controls.Add(lblColor);
            this.Controls.Add(lblMark);
            this.Controls.Add(lblSize);
            this.Controls.Add(lblWidth);
            this.Controls.Add(lblLineStyle);
            this.Controls.Add(textBox1);
            this.Controls.Add(CBoxColor);
            this.Controls.Add(CBoxMark);
            this.Controls.Add(CBoxSize);
            this.Controls.Add(CBoxWidth);
            this.Controls.Add(CBoxLineStyle);
            this.Controls.Add(button1);
            this.Controls.Add(btnExportToMaster);
            this.Controls.Add(btnImportFromMaster);
            this.Controls.Add(Save_Button);
            this.Controls.Add(Cancel_Button);
        }

        private Label CreateLabel(string text, Point location)
        {
            return new Label
            {
                Text = text,
                Location = location,
                Size = new Size(68, 22),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(235, 238, 245),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.Black
            };
        }

        private LayerGroup GetSelectedGroup()
        {
            return comboBoxLayerG.SelectedIndex switch
            {
                1 => LayerGroup.Ibutu,
                2 => LayerGroup.Kikai,
                3 => LayerGroup.Sakuzu,
                _ => LayerGroup.Ikou
            };
        }

        private void ComboBoxLayerG_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var group = GetSelectedGroup();
            var list = LayerDefinitionService.Instance.GetGroup(group);

            _isUpdatingUi = true;
            try
            {
                listBox1.Items.Clear();
                foreach (var item in list)
                {
                    listBox1.Items.Add(item.DisplayText);
                }
            }
            finally
            {
                _isUpdatingUi = false;
            }

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
            UpdateRightEditControls();
        }

        private void ListBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingUi || listBox1.SelectedIndex < 0) return;
            UpdateRightEditControls();
        }

        private void UpdateRightEditControls()
        {
            if (listBox1.SelectedIndex < 0) return;

            var group = GetSelectedGroup();
            int itemIdx = listBox1.SelectedIndex;
            var item = LayerDefinitionService.Instance.GetLayer(group, itemIdx + 1);

            _isUpdatingUi = true;
            try
            {
                textBox1.Text = item.Name;
                if (group == LayerGroup.Sakuzu)
                {
                    textBox1.ReadOnly = true;
                    textBox1.BackColor = Color.FromArgb(235, 238, 245);
                    textBox1.ForeColor = Color.FromArgb(70, 70, 80);
                }
                else
                {
                    textBox1.ReadOnly = false;
                    textBox1.BackColor = Color.White;
                    textBox1.ForeColor = Color.Black;
                }

                CBoxColor.SelectedIndex = Math.Clamp(item.Color - 1, 0, CBoxColor.Items.Count - 1);
                CBoxMark.SelectedIndex = Math.Clamp(item.Mark - 1, 0, CBoxMark.Items.Count - 1);
                CBoxSize.Text = item.Size.ToString("F1");
                CBoxWidth.SelectedIndex = Math.Clamp(item.Width - 1, 0, CBoxWidth.Items.Count - 1);
                CBoxLineStyle.SelectedIndex = (item.LType == 2) ? 1 : 0;
            }
            finally
            {
                _isUpdatingUi = false;
            }
        }

        private void AutoApplyCurrentItem()
        {
            if (_isUpdatingUi || listBox1.SelectedIndex < 0) return;

            var group = GetSelectedGroup();
            int itemIdx = listBox1.SelectedIndex;
            var item = LayerDefinitionService.Instance.GetLayer(group, itemIdx + 1);

            item.Name = textBox1.Text.Trim();
            item.Color = Math.Clamp(CBoxColor.SelectedIndex + 1, 1, 16);
            item.Mark = Math.Clamp(CBoxMark.SelectedIndex + 1, 1, 8);
            item.Size = double.TryParse(CBoxSize.Text, out double sizeVal) ? sizeVal : 1.0;
            item.Width = Math.Clamp(CBoxWidth.SelectedIndex + 1, 1, 10);
            item.LType = (CBoxLineStyle.SelectedIndex == 1) ? 2 : 1;

            _isUpdatingUi = true;
            try
            {
                listBox1.Items[itemIdx] = item.DisplayText;
            }
            finally
            {
                _isUpdatingUi = false;
            }
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            AutoApplyCurrentItem();
            SaveLayers();
        }

        private void Save_Button_Click(object? sender, EventArgs e)
        {
            AutoApplyCurrentItem();
            SaveLayers();
            string targetLabel = string.IsNullOrEmpty(_dbPath) ? "マスターレイヤ設定" : "現場レイヤ設定";
            MessageBox.Show(this, $"{targetLabel}（Layer遺構.txt, Layer遺物.txt, Layer基準点.txt, Layer作図.txt）を保存しました。", "レイヤ設定保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveLayers()
        {
            string targetDir = string.IsNullOrEmpty(_dbPath)
                ? LayerDefinitionService.Instance.GetSystemDefDirectory()
                : LayerDefinitionService.Instance.GetEffectiveDefDirectory(_dbPath);

            LayerDefinitionService.Instance.SaveAll(targetDir);
        }

        private void BtnExportToMaster_Click(object? sender, EventArgs e)
        {
            var res = MessageBox.Show(
                "現在の現場のレイヤ定義データで、システム共通マスターを上書き更新しますか？\n\n※ 次回の新規現場作成時などに標準テンプレートとして使用されます。",
                "マスターへ反映確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            try
            {
                AutoApplyCurrentItem();
                string sysDir = LayerDefinitionService.Instance.GetSystemDefDirectory();
                LayerDefinitionService.Instance.SaveAll(sysDir);
                MessageBox.Show($"✔ 現場のレイヤ定義をシステム共通マスターへ反映しました。\n保存先: {sysDir}", "反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"マスター反映エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImportFromMaster_Click(object? sender, EventArgs e)
        {
            var res = MessageBox.Show(
                "システム共通マスターのレイヤ定義データを読み込み、現在の現場設定に反映しますか？\n\n※ 現在の編集内容はマスターデータで上書きされます。",
                "マスターから反映確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            try
            {
                LayerDefinitionService.Instance.LoadAll(null);
                ComboBoxLayerG_SelectedIndexChanged(null, EventArgs.Empty);
                MessageBox.Show("✔ システム共通マスターからレイヤ定義を反映しました。\n「💾 設定を保存」を押すと現場に保存されます。", "反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"マスター読み込みエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CBoxColor_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (sender is not ComboBox cmb || e.Index < 0) return;

            e.DrawBackground();
            string txt = cmb.Items[e.Index]?.ToString() ?? "";
            Color col = (e.Index + 1 < EditorLayerService.LayerTableColors.Length)
                ? EditorLayerService.LayerTableColors[e.Index + 1]
                : e.ForeColor;

            int boxSize = 14;
            int boxX = e.Bounds.X + 4;
            int boxY = e.Bounds.Y + (e.Bounds.Height - boxSize) / 2;

            using (var b = new SolidBrush(col))
            {
                e.Graphics.FillRectangle(b, boxX, boxY, boxSize, boxSize);
            }
            using (var borderPen = new Pen(Color.FromArgb(120, 120, 120)))
            {
                e.Graphics.DrawRectangle(borderPen, boxX, boxY, boxSize, boxSize);
            }

            Color textColor = col;
            if (col.R > 220 && col.G > 220 && col.B > 220)
            {
                textColor = Color.FromArgb(60, 60, 60);
            }

            using (var textBrush = new SolidBrush(textColor))
            using (var font = new Font("Yu Gothic UI", 10F, FontStyle.Bold))
            {
                float ym = (e.Bounds.Height - e.Graphics.MeasureString(txt, font).Height) / 2;
                e.Graphics.DrawString(txt, font, textBrush, boxX + boxSize + 6, e.Bounds.Y + ym);
            }

            e.DrawFocusRectangle();
        }
    }
}
