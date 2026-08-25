using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public class FormBatchUpdate : Form
    {
        private readonly EditorDbManager _db;
        private ComboBox cmbBatchTable = null!;
        private ComboBox cmbBatchFilterCol = null!;
        private ComboBox cmbBatchFilterOp = null!;
        private TextBox txtBatchFilterVal = null!;
        private ComboBox cmbBatchUpdateCol = null!;
        private TextBox txtBatchUpdateVal = null!;
        private Button btnBatchExecute = null!;
        private Label lblBatchPreviewCount = null!;
        private DataGridView dgvBatchPreview = null!;
        private Button btnClose = null!;

        public FormBatchUpdate(EditorDbManager db)
        {
            _db = db;
            InitializeComponent();
            InitBatchUpdateControls();
        }

        private void InitializeComponent()
        {
            this.Text = "一括更新";
            this.Size = new Size(850, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 246, 248);
            this.ForeColor = Color.FromArgb(33, 37, 41);
            this.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            var lblHeader = new Label
            {
                Text = "⚡ 属性データ一括更新",
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 45, 80),
                Location = new Point(12, 10),
                AutoSize = true
            };

            var pnlTop = new Panel
            {
                Location = new Point(12, 38),
                Size = new Size(810, 105),
                BackColor = Color.FromArgb(233, 236, 243)
            };

            var lblTable = new Label { Text = "対象テーブル:", Location = new Point(10, 12), AutoSize = true, ForeColor = Color.FromArgb(25, 45, 80), Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold) };
            cmbBatchTable = new ComboBox { Location = new Point(90, 8), Size = new Size(150, 23), DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.White, ForeColor = Color.Black };

            var lblFilter = new Label { Text = "条件設定:", Location = new Point(10, 42), AutoSize = true, ForeColor = Color.FromArgb(25, 45, 80), Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold) };
            cmbBatchFilterCol = new ComboBox { Location = new Point(90, 38), Size = new Size(110, 23), DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.White, ForeColor = Color.Black };
            cmbBatchFilterOp = new ComboBox { Location = new Point(205, 38), Size = new Size(135, 23), DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.White, ForeColor = Color.Black };
            txtBatchFilterVal = new TextBox { Location = new Point(345, 38), Size = new Size(140, 23), BackColor = Color.White, ForeColor = Color.Black };

            var lblUpdate = new Label { Text = "更新内容:", Location = new Point(10, 72), AutoSize = true, ForeColor = Color.FromArgb(25, 45, 80), Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold) };
            cmbBatchUpdateCol = new ComboBox { Location = new Point(90, 68), Size = new Size(110, 23), DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.White, ForeColor = Color.Black };
            txtBatchUpdateVal = new TextBox { Location = new Point(205, 68), Size = new Size(180, 23), BackColor = Color.FromArgb(255, 255, 220), ForeColor = Color.Black };

            btnBatchExecute = new Button
            {
                Text = "⚡ 一括更新実行",
                Location = new Point(660, 65),
                Size = new Size(135, 30),
                BackColor = Color.FromArgb(0, 120, 215),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold)
            };
            btnBatchExecute.FlatAppearance.BorderSize = 0;
            btnBatchExecute.Click += BtnBatchExecute_Click;

            lblBatchPreviewCount = new Label
            {
                Text = "対象件数: 0 件",
                Location = new Point(500, 41),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 102, 204),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold)
            };

            pnlTop.Controls.Add(lblTable);
            pnlTop.Controls.Add(cmbBatchTable);
            pnlTop.Controls.Add(lblFilter);
            pnlTop.Controls.Add(cmbBatchFilterCol);
            pnlTop.Controls.Add(cmbBatchFilterOp);
            pnlTop.Controls.Add(txtBatchFilterVal);
            pnlTop.Controls.Add(lblUpdate);
            pnlTop.Controls.Add(cmbBatchUpdateCol);
            pnlTop.Controls.Add(txtBatchUpdateVal);
            pnlTop.Controls.Add(btnBatchExecute);
            pnlTop.Controls.Add(lblBatchPreviewCount);

            dgvBatchPreview = new DataGridView
            {
                Location = new Point(12, 150),
                Size = new Size(810, 275),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackgroundColor = Color.FromArgb(248, 249, 250),
                ForeColor = Color.FromArgb(33, 37, 41),
                GridColor = Color.FromArgb(215, 220, 228),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false
            };
            dgvBatchPreview.EnableHeadersVisualStyles = false;
            dgvBatchPreview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 235, 245);
            dgvBatchPreview.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(25, 45, 80);
            dgvBatchPreview.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            dgvBatchPreview.DefaultCellStyle.BackColor = Color.White;
            dgvBatchPreview.DefaultCellStyle.ForeColor = Color.FromArgb(20, 20, 20);
            dgvBatchPreview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 229, 252);
            dgvBatchPreview.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 30, 80);

            btnClose = new Button
            {
                Text = "閉じる",
                Location = new Point(702, 435),
                Size = new Size(120, 32),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                BackColor = Color.FromArgb(220, 225, 235),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(30, 40, 60),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblHeader);
            this.Controls.Add(pnlTop);
            this.Controls.Add(dgvBatchPreview);
            this.Controls.Add(btnClose);

            cmbBatchTable.SelectedIndexChanged += CmbBatchTable_SelectedIndexChanged;
            cmbBatchFilterCol.SelectedIndexChanged += (s, e) => RefreshBatchPreview();
            cmbBatchFilterOp.SelectedIndexChanged += (s, e) => RefreshBatchPreview();
            txtBatchFilterVal.TextChanged += (s, e) => RefreshBatchPreview();
            cmbBatchUpdateCol.SelectedIndexChanged += (s, e) => RefreshBatchPreview();
            txtBatchUpdateVal.TextChanged += (s, e) => RefreshBatchPreview();
        }

        private void InitBatchUpdateControls()
        {
            cmbBatchTable.Items.Clear();
            cmbBatchTable.Items.Add("遺構L (遺構線)");
            cmbBatchTable.Items.Add("遺構 (マスター)");
            cmbBatchTable.Items.Add("遺物");
            cmbBatchTable.Items.Add("基準点");

            cmbBatchFilterOp.Items.Clear();
            cmbBatchFilterOp.Items.Add("前方一致 (Starts with)");
            cmbBatchFilterOp.Items.Add("後方一致 (Ends with)");
            cmbBatchFilterOp.Items.Add("部分一致 (Contains)");
            cmbBatchFilterOp.Items.Add("完全一致 (Equals)");
            cmbBatchFilterOp.Items.Add("すべてのレコード (All)");

            if (cmbBatchTable.Items.Count > 0) cmbBatchTable.SelectedIndex = 0;
            if (cmbBatchFilterOp.Items.Count > 0) cmbBatchFilterOp.SelectedIndex = 0;
        }

        private void CmbBatchTable_SelectedIndexChanged(object? sender, EventArgs e)
        {
            cmbBatchFilterCol.Items.Clear();
            cmbBatchUpdateCol.Items.Clear();

            string selectedTable = cmbBatchTable.SelectedItem?.ToString() ?? "";
            if (selectedTable.StartsWith("遺構L"))
            {
                cmbBatchFilterCol.Items.AddRange(new object[] { "NAME", "LAYER", "MODE", "DATE", "ID", "LID" });
                cmbBatchUpdateCol.Items.AddRange(new object[] { "LAYER", "MODE", "NAME", "DATE" });
            }
            else if (selectedTable.StartsWith("遺構"))
            {
                cmbBatchFilterCol.Items.AddRange(new object[] { "NAME", "DATE", "ID" });
                cmbBatchUpdateCol.Items.AddRange(new object[] { "NAME", "DATE" });
            }
            else if (selectedTable.StartsWith("遺物"))
            {
                cmbBatchFilterCol.Items.AddRange(new object[] { "NAME(Syubetu)", "CHIKU", "SOUI", "SYUBETU", "LAYER", "DATE", "ID" });
                cmbBatchUpdateCol.Items.AddRange(new object[] { "LAYER", "CHIKU", "SOUI", "SYUBETU", "DATE" });
            }
            else if (selectedTable.StartsWith("基準点"))
            {
                cmbBatchFilterCol.Items.AddRange(new object[] { "NAME", "LAYER", "DATE", "ID" });
                cmbBatchUpdateCol.Items.AddRange(new object[] { "LAYER", "NAME", "DATE" });
            }

            if (cmbBatchFilterCol.Items.Count > 0) cmbBatchFilterCol.SelectedIndex = 0;
            if (cmbBatchUpdateCol.Items.Count > 0) cmbBatchUpdateCol.SelectedIndex = 0;

            RefreshBatchPreview();
        }

        private List<object> GetBatchMatchingItems()
        {
            string selectedTable = cmbBatchTable.SelectedItem?.ToString() ?? "";
            string filterCol = cmbBatchFilterCol.SelectedItem?.ToString() ?? "";
            string filterOp = cmbBatchFilterOp.SelectedItem?.ToString() ?? "";
            string filterVal = txtBatchFilterVal.Text.Trim();

            return _db.GetBatchMatchingItems(selectedTable, filterCol, filterOp, filterVal);
        }

        private void RefreshBatchPreview()
        {
            try
            {
                var matchingItems = GetBatchMatchingItems();
                lblBatchPreviewCount.Text = $"対象件数: {matchingItems.Count} 件";
                dgvBatchPreview.DataSource = new BindingList<object>(matchingItems);
            }
            catch { }
        }

        private void BtnBatchExecute_Click(object? sender, EventArgs e)
        {
            string selectedTable = cmbBatchTable.SelectedItem?.ToString() ?? "";
            string filterCol = cmbBatchFilterCol.SelectedItem?.ToString() ?? "";
            string filterOp = cmbBatchFilterOp.SelectedItem?.ToString() ?? "";
            string filterVal = txtBatchFilterVal.Text.Trim();
            string updateCol = cmbBatchUpdateCol.SelectedItem?.ToString() ?? "";
            string updateVal = txtBatchUpdateVal.Text.Trim();

            var matchingItems = GetBatchMatchingItems();
            if (matchingItems.Count == 0)
            {
                MessageBox.Show("一括更新の対象となるレコードがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dr = MessageBox.Show(
                $"対象テーブル: {selectedTable}\n更新対象列: {updateCol}\n変更後の値: '{updateVal}'\n対象件数: {matchingItems.Count} 件\n\n本当に一括更新を実行しますか？",
                "一括更新の確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dr != DialogResult.Yes) return;

            int updatedCount = _db.ExecuteBatchUpdate(selectedTable, filterCol, filterOp, filterVal, updateCol, updateVal);
            MessageBox.Show($"✔ {updatedCount} 件のデータが一括更新されました！", "一括更新完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RefreshBatchPreview();
        }
    }
}
