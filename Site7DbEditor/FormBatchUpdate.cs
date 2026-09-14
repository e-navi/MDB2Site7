using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public partial class FormBatchUpdate : Form
    {
        private readonly EditorDbManager _db;

        public FormBatchUpdate(EditorDbManager db)
        {
            _db = db;
            InitializeComponent();
            InitializeEvents();
            InitBatchUpdateControls();
        }

        private void InitializeEvents()
        {
            btnBatchExecute.Click += BtnBatchExecute_Click;
            btnClose.Click += (s, e) => this.Close();

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

                string selectedTable = cmbBatchTable.SelectedItem?.ToString() ?? "";
                if (selectedTable.Contains("遺構L"))
                {
                    dgvBatchPreview.DataSource = new BindingList<IkouLModel>(matchingItems.Cast<IkouLModel>().ToList());
                }
                else if (selectedTable.Contains("遺構 (マスター)"))
                {
                    dgvBatchPreview.DataSource = new BindingList<IkouModel>(matchingItems.Cast<IkouModel>().ToList());
                }
                else if (selectedTable.Contains("遺物"))
                {
                    dgvBatchPreview.DataSource = new BindingList<IbutuModel>(matchingItems.Cast<IbutuModel>().ToList());
                }
                else if (selectedTable.Contains("基準点"))
                {
                    dgvBatchPreview.DataSource = new BindingList<KikaiModel>(matchingItems.Cast<KikaiModel>().ToList());
                }
                else
                {
                    dgvBatchPreview.DataSource = new BindingList<object>(matchingItems);
                }

                var hiddenCols = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "S", "V", "H", "KPName", "BPName", "KPH", "MRH"
                };
                foreach (DataGridViewColumn col in dgvBatchPreview.Columns)
                {
                    if (hiddenCols.Contains(col.Name))
                    {
                        col.Visible = false;
                    }
                }
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
