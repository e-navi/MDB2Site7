using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public class FormLayerSettings : Form
    {
        private readonly EditorDbManager _db;

        private ComboBox comboBoxLayerG = null!;
        private ListBox listBox1 = null!;
        private TextBox textBox1 = null!;
        private ComboBox CBoxColor = null!;
        private ComboBox CBoxMark = null!;
        private ComboBox CBoxSize = null!;
        private ComboBox CBoxWidth = null!;
        private ComboBox CBoxLineStyle = null!;
        private Button button1 = null!;
        private Button Cancel_Button = null!;

        public FormLayerSettings(EditorDbManager db)
        {
            _db = db;
            EnsureAll64LayersExist();
            InitializeComponent();
        }

        private void EnsureAll64LayersExist()
        {
            for (int id = 1; id <= 64; id++)
            {
                if (!_db.LayerList.Any(l => l.Id == id))
                {
                    _db.LayerList.Add(new LayerModel
                    {
                        Id = id,
                        Name = GetDefaultLayerName(id),
                        Color = (id - 1) % 16 + 1,
                        Mark = 1,
                        Size = 5.0,
                        Width = 1,
                        LType = 1
                    });
                }
            }
        }

        private string GetDefaultLayerName(int id)
        {
            if (id >= 49 && id <= 64) return $"L{(id - 48):D2}";
            if (id >= 1 && id <= 16) return $"L{id:D2}";
            if (id >= 17 && id <= 32) return $"K{id - 16:D2}";
            return $"D{id - 32:D2}";
        }

        private void InitializeComponent()
        {
            this.Text = "レイヤ設定";
            this.ClientSize = new Size(470, 370);
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
                Location = new Point(21, 12),
                Size = new Size(143, 25),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold)
            };
            comboBoxLayerG.Items.AddRange(new object[] {
                "遺物レイヤGRP",
                "基準点レイヤGRP",
                "作図レイヤGRP",
                "遺構レイヤGRP"
            });
            comboBoxLayerG.SelectedIndexChanged += ComboBoxLayerG_SelectedIndexChanged;

            // listBox1
            listBox1 = new ListBox
            {
                Location = new Point(21, 42),
                Size = new Size(143, 276),
                Font = new Font("Yu Gothic UI", 10.5F, FontStyle.Regular)
            };
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            // Labels
            var lblLayerName = CreateLabel("レイヤ名", new Point(185, 43));
            var lblColor = CreateLabel("表示色", new Point(185, 83));
            var lblMark = CreateLabel("マーク", new Point(185, 123));
            var lblSize = CreateLabel("サイズ", new Point(185, 163));
            var lblWidth = CreateLabel("線幅", new Point(185, 203));
            var lblLineStyle = CreateLabel("線種", new Point(185, 243));

            // Edit controls
            textBox1 = new TextBox
            {
                Location = new Point(255, 38),
                Size = new Size(190, 29),
                Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold)
            };

            CBoxColor = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawFixed,
                Location = new Point(255, 78),
                Size = new Size(125, 29),
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
                Location = new Point(255, 118),
                Size = new Size(75, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };
            CBoxMark.Items.AddRange(new object[] { "〇", "□", "△", "⦿" });

            CBoxSize = new ComboBox
            {
                Location = new Point(255, 158),
                Size = new Size(75, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };
            CBoxSize.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "10", "20" });

            CBoxWidth = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(255, 198),
                Size = new Size(60, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };
            CBoxWidth.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });

            CBoxLineStyle = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(255, 238),
                Size = new Size(90, 29),
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold)
            };
            CBoxLineStyle.Items.AddRange(new object[] { "折線", "曲線" });

            // Buttons
            button1 = new Button
            {
                Text = "設定",
                Location = new Point(185, 285),
                Size = new Size(120, 30),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(230, 235, 245),
                UseVisualStyleBackColor = true
            };
            button1.Click += Button1_Click;

            Cancel_Button = new Button
            {
                Text = "閉じる",
                Location = new Point(345, 325),
                Size = new Size(100, 30),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(230, 235, 245),
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
            this.Controls.Add(Cancel_Button);

            comboBoxLayerG.SelectedIndex = 3; // Default to 遺構レイヤGRP (Index 3)
        }

        private Label CreateLabel(string text, Point location)
        {
            return new Label
            {
                Text = text,
                Location = location,
                Size = new Size(62, 22),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(255, 228, 225), // MistyRose style
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.Black
            };
        }

        private void ComboBoxLayerG_SelectedIndexChanged(object? sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int grpIdx = comboBoxLayerG.SelectedIndex;
            int baseId = grpIdx * 16;

            for (int i = 1; i <= 16; i++)
            {
                int layerId = baseId + i;
                var layer = _db.LayerList.FirstOrDefault(l => l.Id == layerId);
                string name = layer?.Name ?? "";
                listBox1.Items.Add($"L{i:D2} {name}");
            }

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void ListBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            int grpIdx = comboBoxLayerG.SelectedIndex;
            int itemIdx = listBox1.SelectedIndex;
            int layerId = (grpIdx * 16) + itemIdx + 1;

            var rec = _db.LayerList.FirstOrDefault(l => l.Id == layerId);
            if (rec == null) return;

            textBox1.Text = rec.Name;
            CBoxColor.SelectedIndex = Math.Clamp(rec.Color - 1, 0, CBoxColor.Items.Count - 1);
            CBoxMark.SelectedIndex = Math.Clamp(rec.Mark - 1, 0, CBoxMark.Items.Count - 1);
            CBoxSize.Text = rec.Size.ToString();
            CBoxWidth.SelectedIndex = Math.Clamp(rec.Width - 1, 0, CBoxWidth.Items.Count - 1);
            CBoxLineStyle.SelectedIndex = (rec.LType == 2) ? 1 : 0;
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            int grpIdx = comboBoxLayerG.SelectedIndex;
            int itemIdx = listBox1.SelectedIndex;
            int layerId = (grpIdx * 16) + itemIdx + 1;

            var rec = _db.LayerList.FirstOrDefault(l => l.Id == layerId);
            if (rec == null)
            {
                rec = new LayerModel { Id = layerId };
                _db.LayerList.Add(rec);
            }

            rec.Name = textBox1.Text;
            rec.Color = CBoxColor.SelectedIndex + 1;
            rec.Mark = CBoxMark.SelectedIndex + 1;
            rec.Size = double.TryParse(CBoxSize.Text, out double sizeVal) ? sizeVal : 5.0;
            rec.Width = CBoxWidth.SelectedIndex + 1;
            rec.LType = CBoxLineStyle.SelectedIndex + 1;

            listBox1.Items[itemIdx] = $"L{itemIdx + 1:D2} {rec.Name}";
        }

        private void CBoxColor_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (sender is not ComboBox cmb || e.Index < 0) return;

            e.DrawBackground();
            string txt = cmb.Items[e.Index]?.ToString() ?? "";
            Color col = (e.Index + 1 < EditorLayerService.LayerTableColors.Length)
                ? EditorLayerService.LayerTableColors[e.Index + 1]
                : e.ForeColor;

            // 色見本四角形（スウォッチ）の描画
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

            // テキストの描画 (白など明るい色の場合はテキストを濃色にして視認性を確保)
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
