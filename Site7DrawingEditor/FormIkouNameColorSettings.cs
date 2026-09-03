using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Site7DrawingEditor.Services;

namespace Site7DrawingEditor
{
    public class FormIkouNameColorSettings : Form
    {
        private readonly DrawingDbManager? _db;
        private readonly string? _dbPath;

        private ListBox listBox1 = null!;
        private TextBox txtPattern = null!;
        private Panel pnlColorPreview = null!;
        private Button btnPickColor = null!;
        private NumericUpDown numAlpha = null!;
        private Label lblColorInfo = null!;
        private Button btnAdd = null!;
        private Button btnDelete = null!;
        private Button btnMoveUp = null!;
        private Button btnMoveDown = null!;
        private Button btnExportToMaster = null!;
        private Button btnImportFromMaster = null!;
        private Button Save_Button = null!;
        private Button Cancel_Button = null!;
        private bool _isUpdatingUi = false;

        private readonly List<IkouNameColorItem> _items = new();

        public FormIkouNameColorSettings(DrawingDbManager? db)
            : this(db?.CurrentDbPath)
        {
            _db = db;
        }

        public FormIkouNameColorSettings(string? dbPath = null)
        {
            _dbPath = dbPath;
            IkouNameColorService.Instance.Load(_dbPath);

            // コピーを保持して編集
            _items.Clear();
            foreach (var it in IkouNameColorService.Instance.Items)
            {
                _items.Add(new IkouNameColorItem { NamePattern = it.NamePattern, Color = it.Color });
            }

            InitializeComponent();
            PopulateList();

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            bool isMasterMode = string.IsNullOrEmpty(_dbPath);
            this.Text = isMasterMode ? "遺構名色設定 (システム共通マスター)" : "遺構名色設定 (現場定義データ)";
            this.ClientSize = new Size(520, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular);

            var lblTitle = new Label
            {
                Text = "遺構名・プレフィックス別 表示色定義 (遺構名色.txt)",
                Location = new Point(16, 12),
                Size = new Size(480, 20),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 65)
            };

            // listBox1
            listBox1 = new ListBox
            {
                Location = new Point(16, 38),
                Size = new Size(220, 290),
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 26,
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold),
                IntegralHeight = false
            };
            listBox1.DrawItem += ListBox1_DrawItem;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            // List action buttons
            btnAdd = CreateSmallButton("➕ 追加", new Point(16, 335), new Size(50, 26));
            btnAdd.Click += BtnAdd_Click;

            btnDelete = CreateSmallButton("➖ 削除", new Point(70, 335), new Size(50, 26));
            btnDelete.Click += BtnDelete_Click;

            btnMoveUp = CreateSmallButton("▲", new Point(125, 335), new Size(35, 26));
            btnMoveUp.Click += BtnMoveUp_Click;

            btnMoveDown = CreateSmallButton("▼", new Point(164, 335), new Size(35, 26));
            btnMoveDown.Click += BtnMoveDown_Click;

            // Right edit group
            var grpEdit = new GroupBox
            {
                Text = "遺構色設定",
                Location = new Point(248, 38),
                Size = new Size(255, 290),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 40, 60)
            };

            var lblPattern = new Label { Text = "遺構名 / プレフィックス:", Location = new Point(15, 28), AutoSize = true };
            txtPattern = new TextBox
            {
                Location = new Point(18, 50),
                Size = new Size(220, 26),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            txtPattern.TextChanged += (s, e) => AutoApplyCurrentItem();

            var lblColor = new Label { Text = "表示色:", Location = new Point(15, 88), AutoSize = true };

            pnlColorPreview = new Panel
            {
                Location = new Point(18, 110),
                Size = new Size(60, 30),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Red
            };

            btnPickColor = new Button
            {
                Text = "🎨 色を選択...",
                Location = new Point(88, 110),
                Size = new Size(150, 30),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold),
                BackColor = Color.White,
                UseVisualStyleBackColor = true
            };
            btnPickColor.Click += BtnPickColor_Click;

            var lblAlpha = new Label { Text = "不透明度 (Alpha 0-255):", Location = new Point(15, 155), AutoSize = true };
            numAlpha = new NumericUpDown
            {
                Location = new Point(18, 178),
                Size = new Size(90, 26),
                Minimum = 10,
                Maximum = 255,
                Value = 255,
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold)
            };
            numAlpha.ValueChanged += (s, e) => AutoApplyCurrentItem();

            lblColorInfo = new Label
            {
                Text = "RGBA: (255, 0, 0, 255)",
                Location = new Point(18, 220),
                Size = new Size(220, 45),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 90)
            };

            grpEdit.Controls.Add(lblPattern);
            grpEdit.Controls.Add(txtPattern);
            grpEdit.Controls.Add(lblColor);
            grpEdit.Controls.Add(pnlColorPreview);
            grpEdit.Controls.Add(btnPickColor);
            grpEdit.Controls.Add(lblAlpha);
            grpEdit.Controls.Add(numAlpha);
            grpEdit.Controls.Add(lblColorInfo);

            // Bottom Buttons
            btnExportToMaster = new Button
            {
                Text = "📤 マスターへ反映",
                Location = new Point(16, 375),
                Size = new Size(115, 30),
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
                Location = new Point(136, 375),
                Size = new Size(115, 30),
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
                Location = new Point(275, 372),
                Size = new Size(130, 34),
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
                Location = new Point(415, 372),
                Size = new Size(90, 34),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 225, 235),
                ForeColor = Color.FromArgb(30, 40, 60),
                FlatStyle = FlatStyle.Flat,
                UseVisualStyleBackColor = false
            };
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };

            this.Controls.Add(lblTitle);
            this.Controls.Add(listBox1);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnMoveUp);
            this.Controls.Add(btnMoveDown);
            this.Controls.Add(grpEdit);
            this.Controls.Add(btnExportToMaster);
            this.Controls.Add(btnImportFromMaster);
            this.Controls.Add(Save_Button);
            this.Controls.Add(Cancel_Button);
        }

        private Button CreateSmallButton(string text, Point location, Size size)
        {
            return new Button
            {
                Text = text,
                Location = location,
                Size = size,
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                BackColor = Color.White,
                UseVisualStyleBackColor = true
            };
        }

        private void PopulateList()
        {
            _isUpdatingUi = true;
            try
            {
                listBox1.Items.Clear();
                foreach (var it in _items)
                {
                    listBox1.Items.Add(it);
                }
            }
            finally
            {
                _isUpdatingUi = false;
            }
        }

        private void ListBox1_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= _items.Count) return;
            e.DrawBackground();

            var item = _items[e.Index];
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Color box
            var colorRect = new Rectangle(e.Bounds.Left + 4, e.Bounds.Top + 4, 18, 18);
            using (var brush = new SolidBrush(item.Color))
            {
                e.Graphics.FillRectangle(brush, colorRect);
            }
            e.Graphics.DrawRectangle(Pens.Gray, colorRect);

            // Text
            string text = $"{item.NamePattern}  ({item.Color.R},{item.Color.G},{item.Color.B})";
            using (var textBrush = new SolidBrush(isSelected ? Color.White : Color.Black))
            {
                e.Graphics.DrawString(text, e.Font ?? this.Font, textBrush, e.Bounds.Left + 28, e.Bounds.Top + 4);
            }

            e.DrawFocusRectangle();
        }

        private void ListBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingUi || listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= _items.Count) return;

            var item = _items[listBox1.SelectedIndex];
            _isUpdatingUi = true;
            try
            {
                txtPattern.Text = item.NamePattern;
                pnlColorPreview.BackColor = item.Color;
                numAlpha.Value = Math.Clamp((int)item.Color.A, 10, 255);
                lblColorInfo.Text = $"RGBA: ({item.Color.R}, {item.Color.G}, {item.Color.B}, {item.Color.A})\nHEX: #{item.Color.R:X2}{item.Color.G:X2}{item.Color.B:X2}";
            }
            finally
            {
                _isUpdatingUi = false;
            }
        }

        private void AutoApplyCurrentItem()
        {
            if (_isUpdatingUi || listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= _items.Count) return;

            var item = _items[listBox1.SelectedIndex];
            item.NamePattern = txtPattern.Text.Trim();
            int a = (int)numAlpha.Value;
            Color curC = pnlColorPreview.BackColor;
            item.Color = Color.FromArgb(a, curC.R, curC.G, curC.B);

            lblColorInfo.Text = $"RGBA: ({item.Color.R}, {item.Color.G}, {item.Color.B}, {item.Color.A})\nHEX: #{item.Color.R:X2}{item.Color.G:X2}{item.Color.B:X2}";
            listBox1.Invalidate();
        }

        private void BtnPickColor_Click(object? sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= _items.Count) return;

            var item = _items[listBox1.SelectedIndex];
            using (var cd = new ColorDialog())
            {
                cd.Color = item.Color;
                cd.FullOpen = true;
                if (cd.ShowDialog(this) == DialogResult.OK)
                {
                    int a = (int)numAlpha.Value;
                    item.Color = Color.FromArgb(a, cd.Color.R, cd.Color.G, cd.Color.B);
                    pnlColorPreview.BackColor = item.Color;
                    AutoApplyCurrentItem();
                }
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var newItem = new IkouNameColorItem
            {
                NamePattern = "NEW",
                Color = Color.FromArgb(255, 30, 115, 210)
            };
            _items.Add(newItem);
            PopulateList();
            listBox1.SelectedIndex = _items.Count - 1;
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= _items.Count) return;

            int idx = listBox1.SelectedIndex;
            _items.RemoveAt(idx);
            PopulateList();
            if (_items.Count > 0)
            {
                listBox1.SelectedIndex = Math.Clamp(idx, 0, _items.Count - 1);
            }
        }

        private void BtnMoveUp_Click(object? sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;
            if (idx <= 0 || idx >= _items.Count) return;

            var item = _items[idx];
            _items.RemoveAt(idx);
            _items.Insert(idx - 1, item);
            PopulateList();
            listBox1.SelectedIndex = idx - 1;
        }

        private void BtnMoveDown_Click(object? sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;
            if (idx < 0 || idx >= _items.Count - 1) return;

            var item = _items[idx];
            _items.RemoveAt(idx);
            _items.Insert(idx + 1, item);
            PopulateList();
            listBox1.SelectedIndex = idx + 1;
        }

        private void Save_Button_Click(object? sender, EventArgs e)
        {
            AutoApplyCurrentItem();

            // IkouNameColorService に反映して保存
            IkouNameColorService.Instance.Items.Clear();
            foreach (var it in _items)
            {
                IkouNameColorService.Instance.Items.Add(new IkouNameColorItem
                {
                    NamePattern = it.NamePattern,
                    Color = it.Color
                });
            }

            IkouNameColorService.Instance.SaveToGenbaOrSystem(_dbPath);
            MessageBox.Show(this, "遺構名色設定（遺構名色.txt）を保存しました。", "遺構名色設定", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnExportToMaster_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show(this, "現在の遺構名色設定を【マスターテンプレート (C:\\SITE7\\GENBA\\NEW\\Def)】へ上書き保存しますか？", "マスターへ反映", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            AutoApplyCurrentItem();
            string sysDef = Directory.Exists(IkouNameColorService.DefaultSystemDefDir) ? IkouNameColorService.DefaultSystemDefDir : IkouNameColorService.FallbackSystemDefDir;
            string filePath = Path.Combine(sysDef, IkouNameColorService.FileName);
            
            // サービスを一時更新して保存
            var temp = new IkouNameColorService();
            temp.Items.Clear();
            foreach (var it in _items) temp.Items.Add(it);
            temp.SaveToFile(filePath);

            MessageBox.Show(this, $"マスター（{filePath}）へ設定を反映しました。", "マスター反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnImportFromMaster_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show(this, "マスターテンプレートから遺構名色設定を取り込みますか？\n（現在の編集内容は上書きされます）", "マスターから反映", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var masterService = new IkouNameColorService();
            masterService.Load(null); // マスターから読み込み

            _items.Clear();
            foreach (var it in masterService.Items)
            {
                _items.Add(new IkouNameColorItem { NamePattern = it.NamePattern, Color = it.Color });
            }

            PopulateList();
            if (_items.Count > 0) listBox1.SelectedIndex = 0;
            MessageBox.Show(this, "マスターから遺構名色設定を読み込みました。", "マスター反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
