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

            bool isMasterMode = string.IsNullOrEmpty(_dbPath);
            if (isMasterMode)
            {
                this.Text = "マスターDef設定 (システム共通テンプレート)";
                lblHeader.Text = "⚙ マスターDef設定 (システム共通テンプレート)";
                string sysDir = MasterDefinitionService.Instance.GetSystemDefDirectory();
                lblPathInfo.Text = $"保存先フォルダ: {sysDir}";
                btnExportToMaster.Visible = false;
                btnImportFromMaster.Visible = false;
            }
            else
            {
                this.Text = "現場Def設定 (現場定義データ)";
                lblHeader.Text = "⚙ 現場Def設定 (現場定義データ)";
                string genbaDef = MasterDefinitionService.Instance.GetGenbaDefDirectory(_dbPath!);
                lblPathInfo.Text = $"保存先フォルダ: {genbaDef}";
                btnExportToMaster.Visible = true;
                btnImportFromMaster.Visible = true;
                btnExportToMaster.Click += btnExportToMaster_Click;
                btnImportFromMaster.Click += btnImportFromMaster_Click;
            }

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
                    BackColor = Color.FromArgb(245, 246, 248),
                    Padding = new Padding(8)
                };

                var pnlBtns = new Panel
                {
                    Dock = DockStyle.Right,
                    Width = 96,
                    Padding = new Padding(6, 4, 4, 4),
                    BackColor = Color.FromArgb(245, 246, 248)
                };

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    BackgroundColor = Color.FromArgb(248, 249, 250),
                    ForeColor = Color.FromArgb(33, 37, 41),
                    GridColor = Color.FromArgb(215, 220, 228),
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
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 235, 245);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(25, 45, 80);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
                dgv.DefaultCellStyle.BackColor = Color.White;
                dgv.DefaultCellStyle.ForeColor = Color.FromArgb(20, 20, 20);
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 229, 252);
                dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 30, 80);

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
                    BackColor = Color.FromArgb(40, 167, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0, 0, 0, 6),
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold)
                };
                btnAdd.FlatAppearance.BorderSize = 0;
                btnAdd.Click += (s, e) => AddRow(type);

                var btnDel = new Button
                {
                    Text = "➖ 削除",
                    Dock = DockStyle.Top,
                    Height = 32,
                    BackColor = Color.FromArgb(220, 53, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0, 0, 0, 10),
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold)
                };
                btnDel.FlatAppearance.BorderSize = 0;
                btnDel.Click += (s, e) => DeleteRow(type);

                var btnUp = new Button
                {
                    Text = "▲ 上へ",
                    Dock = DockStyle.Top,
                    Height = 30,
                    BackColor = Color.FromArgb(225, 232, 242),
                    ForeColor = Color.FromArgb(25, 45, 80),
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0, 0, 0, 6),
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold)
                };
                btnUp.FlatAppearance.BorderSize = 0;
                btnUp.Click += (s, e) => MoveRow(type, -1);

                var btnDown = new Button
                {
                    Text = "▼ 下へ",
                    Dock = DockStyle.Top,
                    Height = 30,
                    BackColor = Color.FromArgb(225, 232, 242),
                    ForeColor = Color.FromArgb(25, 45, 80),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold)
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

                string targetDir = string.IsNullOrEmpty(_dbPath)
                    ? MasterDefinitionService.Instance.GetSystemDefDirectory()
                    : MasterDefinitionService.Instance.GetGenbaDefDirectory(_dbPath);

                var service = MasterDefinitionService.Instance;
                foreach (var kvp in _bindingLists)
                {
                    var type = kvp.Key;
                    var items = kvp.Value.Where(x => !string.IsNullOrWhiteSpace(x.Code)).ToList();
                    service.SaveMasterFile(targetDir, type, items);
                }

                string targetLabel = string.IsNullOrEmpty(_dbPath) ? "マスターDef設定" : "現場Def設定";
                MessageBox.Show($"{targetLabel}を保存しました。\n保存先: {targetDir}", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Def設定保存エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportToMaster_Click(object? sender, EventArgs e)
        {
            var res = MessageBox.Show(
                "現在の現場のDef定義データで、システム共通マスターを上書き更新しますか？\n\n※ 次回の新規現場作成時などに標準テンプレートとして使用されます。",
                "マスターへ反映確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            try
            {
                foreach (var dgv in _gridViews.Values)
                {
                    try { dgv.EndEdit(); } catch { }
                }

                string sysDir = MasterDefinitionService.Instance.GetSystemDefDirectory();
                var service = MasterDefinitionService.Instance;
                foreach (var kvp in _bindingLists)
                {
                    var type = kvp.Key;
                    var items = kvp.Value.Where(x => !string.IsNullOrWhiteSpace(x.Code)).ToList();
                    service.SaveMasterFile(sysDir, type, items);
                }

                MessageBox.Show($"✔ 現場のDef定義をシステム共通マスターへ反映しました。\n保存先: {sysDir}", "反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"マスター反映エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportFromMaster_Click(object? sender, EventArgs e)
        {
            var res = MessageBox.Show(
                "システム共通マスターのDef定義データを読み込み、現在の現場設定に反映しますか？\n\n※ 現在の編集内容はマスターデータで上書きされます。",
                "マスターから反映確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            try
            {
                string sysDir = MasterDefinitionService.Instance.GetSystemDefDirectory();
                var service = MasterDefinitionService.Instance;

                foreach (var type in _bindingLists.Keys.ToList())
                {
                    string fileName = MasterDefinitionService.FileNames[type];
                    string filePath = Path.Combine(sysDir, fileName);
                    List<MasterItem> items;
                    if (File.Exists(filePath))
                    {
                        items = service.ReadMasterFile(filePath);
                    }
                    else
                    {
                        items = MasterDefinitionService.GetDefaultMasterItems(type);
                    }

                    _bindingLists[type].Clear();
                    foreach (var item in items)
                    {
                        _bindingLists[type].Add(item);
                    }
                }

                MessageBox.Show("✔ システム共通マスターからDef定義を反映しました。\n「💾 設定を保存」を押すと現場に保存されます。", "反映完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"マスター読み込みエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
