using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Site7DrawingEditor.Services;

namespace Site7DrawingEditor
{
    public partial class FormIkouNameColorSettings : Form
    {
        private readonly DrawingDbManager? _db;
        private readonly string? _dbPath;
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

            _items.Clear();
            foreach (var it in IkouNameColorService.Instance.Items)
            {
                _items.Add(new IkouNameColorItem { NamePattern = it.NamePattern, ColorIndex = it.ColorIndex });
            }

            InitializeComponent();
            SetupUI();
            PopulateList();

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void SetupUI()
        {
            bool isMasterMode = string.IsNullOrEmpty(_dbPath);
            this.Text = isMasterMode ? "遺構名色設定 (システム共通マスター)" : "遺構名色設定 (現場定義データ)";
            btnExportToMaster.Visible = !isMasterMode;
            btnImportFromMaster.Visible = !isMasterMode;

            // ListBox
            listBox1.DrawItem += ListBox1_DrawItem;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            // List action buttons
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnMoveUp.Click += BtnMoveUp_Click;
            btnMoveDown.Click += BtnMoveDown_Click;

            // Edit controls
            txtPattern.TextChanged += (s, e) => AutoApplyCurrentItem();

            CBoxColor.Items.Clear();
            CBoxColor.Items.AddRange(IkouNameColorService.ColorNames.Cast<object>().ToArray());
            CBoxColor.DrawItem += CBoxColor_DrawItem;
            CBoxColor.SelectedIndexChanged += (s, e) => AutoApplyCurrentItem();

            // Bottom buttons
            btnExportToMaster.Click += BtnExportToMaster_Click;
            btnImportFromMaster.Click += BtnImportFromMaster_Click;
            Save_Button.Click += Save_Button_Click;
            Cancel_Button.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };
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
            var colorRect = new Rectangle(e.Bounds.Left + 4, e.Bounds.Top + 4, 20, 20);
            using (var brush = new SolidBrush(item.Color))
            {
                e.Graphics.FillRectangle(brush, colorRect);
            }
            e.Graphics.DrawRectangle(Pens.Gray, colorRect);

            // Text (遺構名 + 色名)
            string colorName = (item.ColorIndex >= 1 && item.ColorIndex <= IkouNameColorService.ColorNames.Length)
                ? IkouNameColorService.ColorNames[item.ColorIndex - 1]
                : item.ColorIndex.ToString();

            string text = $"{item.NamePattern}  ({colorName})";
            using (var textBrush = new SolidBrush(isSelected ? Color.White : Color.Black))
            {
                e.Graphics.DrawString(text, e.Font ?? this.Font, textBrush, e.Bounds.Left + 30, e.Bounds.Top + 3);
            }

            e.DrawFocusRectangle();
        }

        private void CBoxColor_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (sender is not ComboBox cmb || e.Index < 0) return;
            e.DrawBackground();

            string txt = cmb.Items[e.Index]?.ToString() ?? "";
            Color col = (e.Index < LayerManager.LayerTableColors.Length)
                ? LayerManager.LayerTableColors[e.Index]
                : e.ForeColor;

            int boxSize = 16;
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
            using (var font = new Font("Yu Gothic UI", 11F, FontStyle.Bold))
            {
                float ym = (e.Bounds.Height - e.Graphics.MeasureString(txt, font).Height) / 2;
                e.Graphics.DrawString(txt, font, textBrush, boxX + boxSize + 6, e.Bounds.Y + ym);
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
                CBoxColor.SelectedIndex = Math.Clamp(item.ColorIndex - 1, 0, CBoxColor.Items.Count - 1);
                lblColorSample.BackColor = item.Color;
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
            item.ColorIndex = Math.Clamp(CBoxColor.SelectedIndex + 1, 1, 16);
            lblColorSample.BackColor = item.Color;

            listBox1.Invalidate();
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var newItem = new IkouNameColorItem
            {
                NamePattern = "NEW",
                ColorIndex = 4
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

            IkouNameColorService.Instance.Items.Clear();
            foreach (var it in _items)
            {
                IkouNameColorService.Instance.Items.Add(new IkouNameColorItem
                {
                    NamePattern = it.NamePattern,
                    ColorIndex = it.ColorIndex
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
            masterService.Load(null);

            _items.Clear();
            foreach (var it in masterService.Items)
            {
                _items.Add(new IkouNameColorItem { NamePattern = it.NamePattern, ColorIndex = it.ColorIndex });
            }

            PopulateList();
            if (_items.Count > 0) listBox1.SelectedIndex = 0;
            MessageBox.Show(this, "マスターから遺構名色設定を読み込みました。", "マスター反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
