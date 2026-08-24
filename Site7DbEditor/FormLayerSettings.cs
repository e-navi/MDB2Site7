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
        private Button Save_Button = null!;
        private Button Cancel_Button = null!;
        private bool _isUpdatingUi = false;

        public FormLayerSettings(EditorDbManager db, LayerGroup initialGroup = LayerGroup.Ikou)
        {
            _db = db;
            _dbPath = db.CurrentDbPath;
            LayerDefinitionService.Instance.LoadAll(_dbPath);
            InitializeComponent();

            int groupIdx = (int)initialGroup;
            if (groupIdx >= 0 && groupIdx < comboBoxLayerG.Items.Count)
            {
                comboBoxLayerG.SelectedIndex = groupIdx;
            }
        }

        private void InitializeComponent()
        {
            this.Text = "レイヤ設定";
            this.ClientSize = new Size(540, 375);
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
                Size = new Size(205, 26),
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
                Size = new Size(205, 316),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular),
                IntegralHeight = false
            };
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            // Labels
            var lblLayerName = CreateLabel("レイヤ名", new Point(236, 45));
            var lblColor = CreateLabel("表示色", new Point(236, 88));
            var lblMark = CreateLabel("マーク", new Point(236, 131));
            var lblSize = CreateLabel("サイズ", new Point(236, 174));
            var lblWidth = CreateLabel("線幅", new Point(236, 217));
            var lblLineStyle = CreateLabel("線種", new Point(236, 260));

            // Edit controls
            textBox1 = new TextBox
            {
                Location = new Point(312, 42),
                Size = new Size(210, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };

            CBoxColor = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawFixed,
                Location = new Point(312, 85),
                Size = new Size(140, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };
            CBoxColor.Items.AddRange(new object[] {
                "黒", "赤", "緑", "青", "黄", "マゼンタ", "シアン", "白",
                "牡丹", "茶", "橙", "薄緑", "明青", "青紫", "明灰", "暗灰"
            });
            CBoxColor.DrawItem += CBoxColor_DrawItem;

            CBoxMark = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(312, 128),
                Size = new Size(85, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };
            CBoxMark.Items.AddRange(new object[] { "〇", "□", "△", "⦿", "✕", "＋", "◇", "★" });

            CBoxSize = new ComboBox
            {
                Location = new Point(312, 171),
                Size = new Size(85, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };
            CBoxSize.Items.AddRange(new object[] { "0.5", "1.0", "1.5", "2.0", "3.0", "4.0", "5.0", "6.0", "8.0", "10.0" });

            CBoxWidth = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(312, 214),
                Size = new Size(85, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };
            CBoxWidth.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });

            CBoxLineStyle = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(312, 257),
                Size = new Size(100, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
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
                Location = new Point(312, 295),
                Size = new Size(150, 30),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(230, 235, 245),
                UseVisualStyleBackColor = true
            };
            button1.Click += Button1_Click;

            Save_Button = new Button
            {
                Text = "💾 設定を保存",
                Location = new Point(255, 332),
                Size = new Size(140, 32),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(56, 176, 0),
                ForeColor = Color.Black,
                UseVisualStyleBackColor = false
            };
            Save_Button.Click += Save_Button_Click;

            Cancel_Button = new Button
            {
                Text = "閉じる",
                Location = new Point(405, 332),
                Size = new Size(100, 32),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 224, 230),
                UseVisualStyleBackColor = true
            };
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
            return (LayerGroup)Math.Clamp(comboBoxLayerG.SelectedIndex, 0, 3);
        }

        private void ComboBoxLayerG_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _isUpdatingUi = true;
            try
            {
                listBox1.Items.Clear();
                var group = GetSelectedGroup();
                var items = LayerDefinitionService.Instance.Groups.TryGetValue(group, out var list) ? list : new List<LayerItem>();

                foreach (var item in items)
                {
                    listBox1.Items.Add(item.DisplayText);
                }

                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                }
            }
            finally
            {
                _isUpdatingUi = false;
            }

            ListBox1_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void ListBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            _isUpdatingUi = true;
            try
            {
                var group = GetSelectedGroup();
                int itemIdx = listBox1.SelectedIndex;
                var item = LayerDefinitionService.Instance.GetLayer(group, itemIdx + 1);

                textBox1.Text = item.Name;
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
            MessageBox.Show(this, "レイヤ設定（Layer遺構.txt, Layer遺物.txt, Layer基準点.txt, Layer作図.txt）を保存しました。", "レイヤ設定保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveLayers()
        {
            string? genbaDir = !string.IsNullOrEmpty(_dbPath) ? Path.GetDirectoryName(_dbPath) : null;
            string targetDir = !string.IsNullOrEmpty(genbaDir) 
                ? Path.Combine(genbaDir, "Def") 
                : LayerDefinitionService.DefaultSystemDefDir;

            LayerDefinitionService.Instance.SaveAll(targetDir);
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
