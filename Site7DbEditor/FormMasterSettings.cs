using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public partial class FormMasterSettings : Form
    {
        private readonly string? _dbPath;
        private readonly Dictionary<MasterType, BindingList<MasterItem>> _bindingLists = new();
        private readonly Dictionary<MasterType, DataGridView> _gridViews = new();

        public FormMasterSettings(string? dbPath = null)
        {
            _dbPath = dbPath;
            InitializeComponent();
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.Icon = SystemIcons.Application;

            bool hasGenba = !string.IsNullOrEmpty(_dbPath) && File.Exists(_dbPath);
            rdoSaveGenba.Enabled = hasGenba;
            if (!hasGenba)
            {
                rdoSaveSystem.Checked = true;
                rdoSaveGenba.Text = "現在の現場専用として保存 (現場未選択)";
            }
            else
            {
                string genbaDir = Path.GetDirectoryName(_dbPath) ?? "";
                string genbaDef = Path.Combine(genbaDir, "Def");
                rdoSaveGenba.Text = $"現在の現場専用として保存 ({genbaDef})";
            }

            string effDir = MasterDefinitionService.Instance.GetEffectiveDefDirectory(_dbPath);
            lblPathInfo.Text = $"現在の読み込み元フォルダ: {effDir}";

            btnSave.Click += btnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            // Build tabs for each master type
            MasterType[] types = new[]
            {
                MasterType.Ikou,
                MasterType.IkouLine,
                MasterType.IbutuSyubetu,
                MasterType.IbutuSoui,
                MasterType.IbutuChiku
            };

            string[] tabTitles = new[]
            {
                "🏛 遺構 (遺構.txt)",
                "📏 遺構線 (遺構線.txt)",
                "🏺 遺物種別 (遺物_種別.txt)",
                "🌍 遺物層位 (遺物_層位.txt)",
                "📍 遺物地区 (遺物_地区.txt)"
            };

            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                var tabPage = new TabPage(tabTitles[i])
                {
                    BackColor = Color.FromArgb(35, 38, 48),
                    Padding = new Padding(8)
                };

                var pnlBtns = new Panel
                {
                    Dock = DockStyle.Right,
                    Width = 96,
                    Padding = new Padding(6, 4, 4, 4),
                    BackColor = Color.FromArgb(35, 38, 48)
                };

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    BackgroundColor = Color.FromArgb(28, 30, 38),
                    ForeColor = Color.White,
                    GridColor = Color.FromArgb(50, 54, 68),
                    AutoGenerateColumns = false,
                    AllowUserToAddRows = true,
                    AllowUserToDeleteRows = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    RowHeadersWidth = 28,
                    BorderStyle = BorderStyle.None,
                    ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
                };

                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(43, 45, 66);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 225, 255);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
                dgv.DefaultCellStyle.BackColor = Color.FromArgb(28, 30, 38);
                dgv.DefaultCellStyle.ForeColor = Color.White;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;

                var colCode = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(MasterItem.Code),
                    HeaderText = "コード・略称 (入力値)",
                    Width = 220
                };
                var colDesc = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(MasterItem.Description),
                    HeaderText = "説明・詳細 (ドロップダウン表示)",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };

                dgv.Columns.AddRange(colCode, colDesc);

                var btnAdd = new Button
                {
                    Text = "➕ 行追加",
                    Dock = DockStyle.Top,
                    Height = 32,
                    BackColor = Color.FromArgb(50, 54, 68),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0, 0, 0, 6)
                };
                btnAdd.FlatAppearance.BorderSize = 0;
                btnAdd.Click += (s, e) => AddRow(type);

                var btnDel = new Button
                {
                    Text = "➖ 削除",
                    Dock = DockStyle.Top,
                    Height = 32,
                    BackColor = Color.FromArgb(50, 54, 68),
                    ForeColor = Color.FromArgb(239, 35, 60),
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0, 0, 0, 10)
                };
                btnDel.FlatAppearance.BorderSize = 0;
                btnDel.Click += (s, e) => DeleteRow(type);

                var btnUp = new Button
                {
                    Text = "▲ 上へ",
                    Dock = DockStyle.Top,
                    Height = 30,
                    BackColor = Color.FromArgb(43, 45, 66),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0, 0, 0, 6)
                };
                btnUp.FlatAppearance.BorderSize = 0;
                btnUp.Click += (s, e) => MoveRow(type, -1);

                var btnDown = new Button
                {
                    Text = "▼ 下へ",
                    Dock = DockStyle.Top,
                    Height = 30,
                    BackColor = Color.FromArgb(43, 45, 66),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnDown.FlatAppearance.BorderSize = 0;
                btnDown.Click += (s, e) => MoveRow(type, 1);

                pnlBtns.Controls.Add(btnDown);
                pnlBtns.Controls.Add(btnUp);
                pnlBtns.Controls.Add(btnDel);
                pnlBtns.Controls.Add(btnAdd);

                tabPage.Controls.Add(dgv);
                tabPage.Controls.Add(pnlBtns);

                tabControlMasters.TabPages.Add(tabPage);
                _gridViews[type] = dgv;
            }
        }

        private void LoadData()
        {
            var service = MasterDefinitionService.Instance;
            service.LoadAll(_dbPath);

            foreach (MasterType type in Enum.GetValues(typeof(MasterType)))
            {
                var list = service.Masters.TryGetValue(type, out var items) ? items : new List<MasterItem>();
                var bl = new BindingList<MasterItem>(list.Select(x => new MasterItem { Code = x.Code, Description = x.Description }).ToList());
                _bindingLists[type] = bl;
                if (_gridViews.TryGetValue(type, out var dgv))
                {
                    dgv.DataSource = bl;
                }
            }
        }

        private void AddRow(MasterType type)
        {
            if (_bindingLists.TryGetValue(type, out var bl) && _gridViews.TryGetValue(type, out var dgv))
            {
                var newItem = new MasterItem { Code = "新規", Description = "" };
                bl.Add(newItem);
                int idx = bl.Count - 1;
                dgv.ClearSelection();
                if (idx >= 0 && idx < dgv.Rows.Count)
                {
                    dgv.Rows[idx].Selected = true;
                    dgv.CurrentCell = dgv.Rows[idx].Cells[0];
                }
            }
        }

        private void DeleteRow(MasterType type)
        {
            if (_bindingLists.TryGetValue(type, out var bl) && _gridViews.TryGetValue(type, out var dgv))
            {
                if (dgv.SelectedRows.Count > 0 && dgv.SelectedRows[0].Index >= 0)
                {
                    int idx = dgv.SelectedRows[0].Index;
                    if (idx < bl.Count)
                    {
                        bl.RemoveAt(idx);
                    }
                }
            }
        }

        private void MoveRow(MasterType type, int offset)
        {
            if (_bindingLists.TryGetValue(type, out var bl) && _gridViews.TryGetValue(type, out var dgv))
            {
                if (dgv.SelectedRows.Count > 0 && dgv.SelectedRows[0].Index >= 0)
                {
                    int idx = dgv.SelectedRows[0].Index;
                    int newIdx = idx + offset;
                    if (newIdx >= 0 && newIdx < bl.Count)
                    {
                        var item = bl[idx];
                        bl.RemoveAt(idx);
                        bl.Insert(newIdx, item);
                        dgv.ClearSelection();
                        dgv.Rows[newIdx].Selected = true;
                        dgv.CurrentCell = dgv.Rows[newIdx].Cells[0];
                    }
                }
            }
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                // Commit any ongoing edit in active grid
                foreach (var dgv in _gridViews.Values)
                {
                    try { dgv.EndEdit(); } catch { }
                }

                string targetDir;
                if (rdoSaveGenba.Checked)
                {
                    if (string.IsNullOrEmpty(_dbPath))
                    {
                        MessageBox.Show("現場データベースが開かれていません。", "保存先エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    targetDir = MasterDefinitionService.Instance.GetGenbaDefDirectory(_dbPath);
                }
                else
                {
                    targetDir = MasterDefinitionService.DefaultSystemDefDir;
                }

                var service = MasterDefinitionService.Instance;
                foreach (var kvp in _bindingLists)
                {
                    var type = kvp.Key;
                    var items = kvp.Value.Where(x => !string.IsNullOrWhiteSpace(x.Code)).ToList();
                    service.SaveMasterFile(targetDir, type, items);
                }

                MessageBox.Show($"マスター設定を保存しました。\n保存先: {targetDir}", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"マスター設定保存エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
