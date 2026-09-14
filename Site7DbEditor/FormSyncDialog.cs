using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public class FormSyncDialog : Form
    {
        private readonly string _indoorDbPath;
        private readonly string _siteName;
        private readonly bool _isGaigyoMode;

        private List<OutdoorDeviceCandidate> _candidates = new();
        private SyncDiffSummary? _currentDiff;

        // UI Controls
        private Panel panelHeader = null!;
        private Label lblHeaderTitle = null!;
        private Label lblHeaderSubtitle = null!;

        private TabControl tabMain = null!;
        private TabPage tabSync = null!;
        private TabPage tabHistory = null!;

        // Tab 1 (Sync) Controls
        private Panel panelSource = null!;
        private ComboBox cmbSources = null!;
        private Button btnRescan = null!;
        private Button btnBrowse = null!;
        private Label lblSourceDetail = null!;

        private Panel panelStats = null!;
        private Label lblIkouStat = null!;
        private Label lblIkouLStat = null!;
        private Label lblIbutuStat = null!;
        private Label lblKikaiStat = null!;

        private Panel panelFilter = null!;
        private RadioButton radAll = null!;
        private RadioButton radDiffOnly = null!;
        private RadioButton radIkou = null!;
        private RadioButton radIkouL = null!;
        private RadioButton radIbutu = null!;
        private RadioButton radKikai = null!;

        private DataGridView dgvDiff = null!;

        // Tab 2 (History) Controls
        private Panel panelHistoryTop = null!;
        private Button btnRefreshHistory = null!;
        private Button btnOpenHistoryLog = null!;
        private DataGridView dgvHistory = null!;

        // Footer Controls
        private Panel panelFooter = null!;
        private CheckBox chkAutoBackup = null!;
        private ProgressBar progressBar = null!;
        private Label lblStatus = null!;
        private Button btnExportToSsd = null!;
        private Button btnSync = null!;
        private Button btnClose = null!;

        public FormSyncDialog(string indoorDbPath, string siteName, bool isGaigyoMode = false)
        {
            _indoorDbPath = indoorDbPath;
            _siteName = siteName;
            _isGaigyoMode = isGaigyoMode;

            InitializeCustomComponents();
            RescanOutdoorSources();
            LoadSyncHistory();
        }

        private void InitializeCustomComponents()
        {
            string modeText = _isGaigyoMode ? "【外業】" : "【内業】";
            this.Text = $"外業・内業データ同期＆ポータブルSSD連携 {modeText}";
            this.Size = new Size(1100, 780);
            this.MinimumSize = new Size(960, 640);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Yu Gothic UI", 12.0F, FontStyle.Regular);
            this.BackColor = Color.FromArgb(246, 248, 251);

            // 1. Header Panel
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.FromArgb(28, 35, 48),
                Padding = new Padding(20, 12, 20, 12)
            };

            lblHeaderTitle = new Label
            {
                Text = $"🔌 外業・内業データ同期＆ポータブルSSD連携 {modeText}",
                Font = new Font("Yu Gothic UI", 14.0F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(18, 12)
            };

            string roleDescription = _isGaigyoMode
                ? "ポータブルSSDへ外業データをワンクリック保存、または内業の最新設計データを取り込みます。"
                : "ポータブルSSDの外業測定データを検知・差分マージ、または外業端末用データをSSDへ出力します。";

            lblHeaderSubtitle = new Label
            {
                Text = $"現場: {_siteName} ({Path.GetFileName(_indoorDbPath)})  |  {roleDescription}",
                Font = new Font("Yu Gothic UI", 10.5F),
                ForeColor = Color.FromArgb(180, 195, 215),
                AutoSize = true,
                Location = new Point(20, 48)
            };

            panelHeader.Controls.Add(lblHeaderTitle);
            panelHeader.Controls.Add(lblHeaderSubtitle);

            // 2. Footer Panel
            panelFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 96,
                BackColor = Color.White,
                Padding = new Padding(20, 12, 20, 12)
            };
            panelFooter.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(220, 225, 235));
                e.Graphics.DrawLine(p, 0, 0, panelFooter.Width, 0);
            };

            chkAutoBackup = new CheckBox
            {
                Text = "同期実行前にローカルDBのバックアップを自動作成する（推奨）",
                Checked = true,
                AutoSize = true,
                Location = new Point(20, 14),
                ForeColor = Color.FromArgb(40, 50, 70),
                Font = new Font("Yu Gothic UI", 11.0F, FontStyle.Bold)
            };

            lblStatus = new Label
            {
                Text = "SSD/外部メディアを検出中...",
                AutoSize = true,
                Location = new Point(20, 44),
                ForeColor = Color.FromArgb(100, 110, 130),
                Font = new Font("Yu Gothic UI", 11.0F)
            };

            progressBar = new ProgressBar
            {
                Location = new Point(20, 70),
                Size = new Size(420, 14),
                Visible = false,
                Style = ProgressBarStyle.Continuous
            };

            btnExportToSsd = new Button
            {
                Text = "📤 現場データをSSDへ出力...",
                Size = new Size(230, 44),
                Location = new Point(panelFooter.Width - 545, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.FromArgb(240, 244, 250),
                ForeColor = Color.FromArgb(24, 75, 140),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnExportToSsd.FlatAppearance.BorderColor = Color.FromArgb(180, 205, 235);
            btnExportToSsd.Click += async (s, e) => await ExecuteExportToSsdAsync();

            btnSync = new Button
            {
                Text = "📥 差分を取り込み（マージ）",
                Size = new Size(220, 44),
                Location = new Point(panelFooter.Width - 305, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.FromArgb(24, 115, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Yu Gothic UI", 12.0F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnSync.FlatAppearance.BorderSize = 0;
            btnSync.Click += async (s, e) => await ExecuteSyncAsync();

            btnClose = new Button
            {
                Text = "閉じる",
                Size = new Size(85, 44),
                Location = new Point(panelFooter.Width - 75, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.FromArgb(235, 238, 243),
                ForeColor = Color.FromArgb(50, 60, 80),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Yu Gothic UI", 11.5F),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            panelFooter.Controls.Add(chkAutoBackup);
            panelFooter.Controls.Add(lblStatus);
            panelFooter.Controls.Add(progressBar);
            panelFooter.Controls.Add(btnExportToSsd);
            panelFooter.Controls.Add(btnSync);
            panelFooter.Controls.Add(btnClose);

            // 3. Tab Control
            tabMain = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Yu Gothic UI", 12.0F, FontStyle.Bold),
                Padding = new Point(18, 8)
            };

            tabSync = new TabPage("⚡ 差分プレビュー & 同期");
            tabHistory = new TabPage("📜 同期履歴 (ログ)");

            SetupSyncTab();
            SetupHistoryTab();

            tabMain.TabPages.Add(tabSync);
            tabMain.TabPages.Add(tabHistory);
            tabMain.SelectedIndexChanged += (s, e) =>
            {
                if (tabMain.SelectedTab == tabHistory) LoadSyncHistory();
            };

            this.Controls.Add(tabMain);
            this.Controls.Add(panelFooter);
            this.Controls.Add(panelHeader);
        }

        private void SetupSyncTab()
        {
            // Source Selection Panel
            panelSource = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };
            panelSource.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(220, 225, 235));
                e.Graphics.DrawLine(p, 0, panelSource.Height - 1, panelSource.Width, panelSource.Height - 1);
            };

            string sourcePrompt = _isGaigyoMode
                ? "取り込み対象データソース（SSD 内業データ / 外部フォルダ）:"
                : "取り込み対象データソース（SSD 外業データ / 外部フォルダ）:";

            var lblSourceTitle = new Label
            {
                Text = sourcePrompt,
                Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 50, 70),
                AutoSize = true,
                Location = new Point(15, 10)
            };

            cmbSources = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(15, 36),
                Size = new Size(620, 32),
                Font = new Font("Yu Gothic UI", 11.5F)
            };
            cmbSources.SelectedIndexChanged += cmbSources_SelectedIndexChanged;

            btnRescan = new Button
            {
                Text = "🔄 再スキャン",
                Size = new Size(130, 34),
                Location = new Point(645, 35),
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(40, 50, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Yu Gothic UI", 11.0F),
                Cursor = Cursors.Hand
            };
            btnRescan.FlatAppearance.BorderColor = Color.FromArgb(200, 210, 225);
            btnRescan.Click += (s, e) => RescanOutdoorSources();

            btnBrowse = new Button
            {
                Text = "📁 フォルダ参照...",
                Size = new Size(150, 34),
                Location = new Point(785, 35),
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(40, 50, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Yu Gothic UI", 11.0F),
                Cursor = Cursors.Hand
            };
            btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(200, 210, 225);
            btnBrowse.Click += btnBrowse_Click;

            lblSourceDetail = new Label
            {
                Text = "",
                AutoSize = true,
                Location = new Point(15, 72),
                ForeColor = Color.FromArgb(120, 130, 145),
                Font = new Font("Yu Gothic UI", 10.5F)
            };

            panelSource.Controls.Add(lblSourceTitle);
            panelSource.Controls.Add(cmbSources);
            panelSource.Controls.Add(btnRescan);
            panelSource.Controls.Add(btnBrowse);
            panelSource.Controls.Add(lblSourceDetail);

            // Stats Summary Cards
            panelStats = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.FromArgb(246, 248, 251),
                Padding = new Padding(10, 6, 10, 6)
            };

            var tableStats = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            var cardIkou = CreateStatCard("🏛 遺構データ", out lblIkouStat);
            var cardIkouL = CreateStatCard("〰 遺構線 (測点)", out lblIkouLStat);
            var cardIbutu = CreateStatCard("🏺 遺物データ", out lblIbutuStat);
            var cardKikai = CreateStatCard("📐 基準点データ", out lblKikaiStat);

            tableStats.Controls.Add(cardIkou, 0, 0);
            tableStats.Controls.Add(cardIkouL, 1, 0);
            tableStats.Controls.Add(cardIbutu, 2, 0);
            tableStats.Controls.Add(cardKikai, 3, 0);
            panelStats.Controls.Add(tableStats);

            // Filter Panel
            panelFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.White,
                Padding = new Padding(15, 8, 15, 8)
            };
            panelFilter.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(230, 233, 240));
                e.Graphics.DrawLine(p, 0, panelFilter.Height - 1, panelFilter.Width, panelFilter.Height - 1);
            };

            var lblFilter = new Label
            {
                Text = "絞り込み:",
                Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 80, 100),
                AutoSize = true,
                Location = new Point(12, 12)
            };

            radAll = new RadioButton { Text = "すべて", Checked = true, AutoSize = true, Location = new Point(100, 10), Font = new Font("Yu Gothic UI", 11.5F) };
            radDiffOnly = new RadioButton { Text = "⚡ 差分のみ", AutoSize = true, Location = new Point(185, 10), Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold), ForeColor = Color.FromArgb(200, 80, 0) };
            radIkou = new RadioButton { Text = "遺構", AutoSize = true, Location = new Point(310, 10), Font = new Font("Yu Gothic UI", 11.5F) };
            radIkouL = new RadioButton { Text = "遺構線", AutoSize = true, Location = new Point(390, 10), Font = new Font("Yu Gothic UI", 11.5F) };
            radIbutu = new RadioButton { Text = "遺物", AutoSize = true, Location = new Point(480, 10), Font = new Font("Yu Gothic UI", 11.5F) };
            radKikai = new RadioButton { Text = "基準点", AutoSize = true, Location = new Point(560, 10), Font = new Font("Yu Gothic UI", 11.5F) };

            radAll.CheckedChanged += (s, e) => RefreshDiffGrid();
            radDiffOnly.CheckedChanged += (s, e) => RefreshDiffGrid();
            radIkou.CheckedChanged += (s, e) => RefreshDiffGrid();
            radIkouL.CheckedChanged += (s, e) => RefreshDiffGrid();
            radIbutu.CheckedChanged += (s, e) => RefreshDiffGrid();
            radKikai.CheckedChanged += (s, e) => RefreshDiffGrid();

            panelFilter.Controls.Add(lblFilter);
            panelFilter.Controls.Add(radAll);
            panelFilter.Controls.Add(radDiffOnly);
            panelFilter.Controls.Add(radIkou);
            panelFilter.Controls.Add(radIkouL);
            panelFilter.Controls.Add(radIbutu);
            panelFilter.Controls.Add(radKikai);

            // DataGridView for Diff List
            dgvDiff = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 34 },
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 36,
                EnableHeadersVisualStyles = false,
                Font = new Font("Yu Gothic UI", 11.5F)
            };

            dgvDiff.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(50, 60, 80),
                Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };

            dgvDiff.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColType", HeaderText = "種別", Width = 110, FillWeight = 12 });
            dgvDiff.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColDiff", HeaderText = "状態", Width = 100, FillWeight = 11 });
            dgvDiff.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColName", HeaderText = "名称 / 項目", Width = 160, FillWeight = 20 });
            dgvDiff.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColOutdoor", HeaderText = "取り込み元データ（SSD側）", FillWeight = 31 });
            dgvDiff.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColIndoor", HeaderText = "現在のローカルデータ", FillWeight = 26 });

            dgvDiff.CellFormatting += DgvDiff_CellFormatting;

            tabSync.Controls.Add(dgvDiff);
            tabSync.Controls.Add(panelFilter);
            tabSync.Controls.Add(panelStats);
            tabSync.Controls.Add(panelSource);
        }

        private void SetupHistoryTab()
        {
            panelHistoryTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };
            panelHistoryTop.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(220, 225, 235));
                e.Graphics.DrawLine(p, 0, panelHistoryTop.Height - 1, panelHistoryTop.Width, panelHistoryTop.Height - 1);
            };

            btnRefreshHistory = new Button
            {
                Text = "🔄 履歴を再読み込み",
                Size = new Size(180, 36),
                Location = new Point(15, 10),
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(40, 50, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Yu Gothic UI", 11.0F),
                Cursor = Cursors.Hand
            };
            btnRefreshHistory.FlatAppearance.BorderColor = Color.FromArgb(200, 210, 225);
            btnRefreshHistory.Click += (s, e) => LoadSyncHistory();

            btnOpenHistoryLog = new Button
            {
                Text = "📄 ログファイルを開く (TSV)",
                Size = new Size(210, 36),
                Location = new Point(205, 10),
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(40, 50, 70),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Yu Gothic UI", 11.0F),
                Cursor = Cursors.Hand
            };
            btnOpenHistoryLog.FlatAppearance.BorderColor = Color.FromArgb(200, 210, 225);
            btnOpenHistoryLog.Click += (s, e) => OpenHistoryLogFile();

            panelHistoryTop.Controls.Add(btnRefreshHistory);
            panelHistoryTop.Controls.Add(btnOpenHistoryLog);

            dgvHistory = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 34 },
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 36,
                EnableHeadersVisualStyles = false,
                Font = new Font("Yu Gothic UI", 11.5F)
            };

            dgvHistory.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(50, 60, 80),
                Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColHTime", HeaderText = "日時", Width = 160, FillWeight = 16 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColHAction", HeaderText = "操作区分", Width = 160, FillWeight = 18 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColHMachine", HeaderText = "端末名", Width = 130, FillWeight = 12 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColHCounts", HeaderText = "同期詳細", FillWeight = 26 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColHDef", HeaderText = "Defファイル", Width = 120, FillWeight = 12 });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColHBackup", HeaderText = "バックアップ", Width = 140, FillWeight = 16 });

            tabHistory.Controls.Add(dgvHistory);
            tabHistory.Controls.Add(panelHistoryTop);
        }

        private Panel CreateStatCard(string title, out Label lblValue)
        {
            var p = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(4)
            };
            p.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(225, 230, 240));
                e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
            };

            var lblT = new Label
            {
                Text = title,
                Font = new Font("Yu Gothic UI", 11.0F, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 80, 100),
                AutoSize = true,
                Location = new Point(10, 6)
            };

            lblValue = new Label
            {
                Text = "新規: 0件 | 更新: 0件",
                Font = new Font("Yu Gothic UI", 11.5F),
                ForeColor = Color.FromArgb(100, 110, 130),
                AutoSize = true,
                Location = new Point(10, 32)
            };

            p.Controls.Add(lblT);
            p.Controls.Add(lblValue);
            return p;
        }

        private void DgvDiff_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDiff.Rows.Count) return;

            var row = dgvDiff.Rows[e.RowIndex];
            string? diffType = row.Cells["ColDiff"].Value?.ToString();

            if (diffType?.Contains("新規") == true)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(240, 253, 244);
                row.Cells["ColDiff"].Style.ForeColor = Color.FromArgb(22, 101, 52);
                row.Cells["ColDiff"].Style.Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold);
            }
            else if (diffType?.Contains("更新") == true)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 251, 235);
                row.Cells["ColDiff"].Style.ForeColor = Color.FromArgb(180, 83, 9);
                row.Cells["ColDiff"].Style.Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold);
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
                row.Cells["ColDiff"].Style.ForeColor = Color.FromArgb(120, 130, 140);
            }
        }

        private void RescanOutdoorSources()
        {
            lblStatus.Text = "外部メディアをスキャン中...";
            cmbSources.Items.Clear();

            // 内業モードなら外業フォルダを優先探索、外業モードなら内業フォルダを優先探索
            bool preferOutdoor = !_isGaigyoMode;
            _candidates = SyncService.FindOutdoorCandidates(_siteName, preferOutdoor);

            if (_candidates.Count == 0)
            {
                cmbSources.Items.Add("（同期対象のSSD/フォルダが見つかりませんでした - フォルダ参照から指定してください）");
                cmbSources.SelectedIndex = 0;
                lblSourceDetail.Text = "※ ポータブルSSDを接続後、「再スキャン」または「フォルダ参照」を押してください。";
                btnSync.Enabled = false;
                ClearDiffStats();
            }
            else
            {
                foreach (var c in _candidates)
                {
                    cmbSources.Items.Add(c.DisplayText);
                }
                cmbSources.SelectedIndex = 0;
            }
        }

        private void btnBrowse_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "取り込み元のデータベースファイルを選択してください",
                Filter = "Site7 データベース (*.db;*.sqlite)|*.db;*.sqlite|すべてのファイル (*.*)|*.*",
                FileName = "Site7.db"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                string chosenDb = ofd.FileName;
                string chosenDir = Path.GetDirectoryName(chosenDb) ?? "";
                string siteName = Path.GetFileName(chosenDir);

                var customCandidate = new OutdoorDeviceCandidate
                {
                    DriveLetter = Path.GetPathRoot(chosenDb) ?? "",
                    DriveLabel = "指定フォルダ",
                    DriveType = DriveType.Unknown,
                    SiteFolderPath = chosenDir,
                    DbFilePath = chosenDb,
                    SiteName = siteName,
                    SourceCategory = "手動指定",
                    UpdatedAt = File.GetLastWriteTime(chosenDb)
                };

                _candidates.Insert(0, customCandidate);
                cmbSources.Items.Insert(0, customCandidate.DisplayText + " ★指定");
                cmbSources.SelectedIndex = 0;
            }
        }

        private void cmbSources_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int idx = cmbSources.SelectedIndex;
            if (idx < 0 || idx >= _candidates.Count)
            {
                btnSync.Enabled = false;
                return;
            }

            var selected = _candidates[idx];
            lblSourceDetail.Text = $"パス: {selected.DbFilePath}  |  更新日時: {selected.UpdatedAt:yyyy/MM/dd HH:mm:ss}";

            AnalyzeDiff(selected.DbFilePath);
        }

        private void AnalyzeDiff(string outdoorDbPath)
        {
            lblStatus.Text = "差分を分析中...";
            try
            {
                _currentDiff = SyncService.CompareDatabases(_indoorDbPath, outdoorDbPath);

                lblIkouStat.Text = $"新規: {_currentDiff.IkouNewCount}件 | 更新: {_currentDiff.IkouUpdatedCount}件 | 一致: {_currentDiff.IkouSameCount}件";
                lblIkouLStat.Text = $"新規: {_currentDiff.IkouLNewCount}点 | 更新: {_currentDiff.IkouLUpdatedCount}点 | 一致: {_currentDiff.IkouLSameCount}点";
                lblIbutuStat.Text = $"新規: {_currentDiff.IbutuNewCount}件 | 更新: {_currentDiff.IbutuUpdatedCount}件 | 一致: {_currentDiff.IbutuSameCount}件";
                lblKikaiStat.Text = $"新規: {_currentDiff.KikaiNewCount}点 | 更新: {_currentDiff.KikaiUpdatedCount}点 | 一致: {_currentDiff.KikaiSameCount}点";

                RefreshDiffGrid();

                int totalDiff = _currentDiff.TotalChangesCount;
                if (totalDiff > 0)
                {
                    lblStatus.Text = $"差分合計 {totalDiff} 件（新規: {_currentDiff.TotalNewCount}件, 更新: {_currentDiff.TotalUpdatedCount}件）を検出しました。取り込み可能です。";
                    lblStatus.ForeColor = Color.FromArgb(22, 101, 52);
                    btnSync.Enabled = true;
                }
                else
                {
                    lblStatus.Text = "✔ 取り込み元データとローカルデータは完全に一致しています（差分なし）。";
                    lblStatus.ForeColor = Color.FromArgb(70, 80, 100);
                    btnSync.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"差分分析エラー: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
                btnSync.Enabled = false;
            }
        }

        private void ClearDiffStats()
        {
            lblIkouStat.Text = "新規: 0件 | 更新: 0件";
            lblIkouLStat.Text = "新規: 0点 | 更新: 0点";
            lblIbutuStat.Text = "新規: 0件 | 更新: 0件";
            lblKikaiStat.Text = "新規: 0点 | 更新: 0点";
            dgvDiff.Rows.Clear();
        }

        private void RefreshDiffGrid()
        {
            dgvDiff.Rows.Clear();
            if (_currentDiff == null) return;

            var items = _currentDiff.DiffItems.AsEnumerable();

            if (radDiffOnly.Checked)
            {
                items = items.Where(x => x.DiffType != SyncDiffType.Same);
            }
            else if (radIkou.Checked)
            {
                items = items.Where(x => x.EntityType == "遺構");
            }
            else if (radIkouL.Checked)
            {
                items = items.Where(x => x.EntityType == "遺構線");
            }
            else if (radIbutu.Checked)
            {
                items = items.Where(x => x.EntityType == "遺物");
            }
            else if (radKikai.Checked)
            {
                items = items.Where(x => x.EntityType == "基準点");
            }

            foreach (var item in items)
            {
                dgvDiff.Rows.Add(
                    item.EntityType,
                    item.DiffTypeDisplay,
                    item.Name,
                    item.OutdoorSummary,
                    item.IndoorSummary
                );
            }
        }

        private async Task ExecuteSyncAsync()
        {
            int idx = cmbSources.SelectedIndex;
            if (idx < 0 || idx >= _candidates.Count) return;

            var outdoor = _candidates[idx];

            string actionName = _isGaigyoMode ? "内業データから最新データを取り込み" : "外業データを内業DBへマージ";
            var confirm = MessageBox.Show(
                $"【{outdoor.DisplayText}】から\n{actionName}を実行しますか？\n\n" +
                $"・差分件数: {_currentDiff?.TotalChangesCount ?? 0} 件\n" +
                $"・現場定義ファイル(Def)も同時に同期されます。\n" +
                (chkAutoBackup.Checked ? "・実行前にローカルDBのバックアップを作成します。" : ""),
                "同期実行の確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnSync.Enabled = false;
            btnExportToSsd.Enabled = false;
            btnClose.Enabled = false;
            progressBar.Visible = true;
            progressBar.Value = 0;

            bool backup = chkAutoBackup.Checked;
            SyncResult? result = null;

            await Task.Run(() =>
            {
                result = SyncService.ExecuteSync(_indoorDbPath, outdoor.DbFilePath, backup, (msg, pct) =>
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        lblStatus.Text = msg;
                        progressBar.Value = Math.Clamp(pct, 0, 100);
                    }));
                });
            });

            progressBar.Visible = false;
            btnExportToSsd.Enabled = true;
            btnClose.Enabled = true;

            if (result != null && result.Success)
            {
                MessageBox.Show(result.SummaryText, "同期完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result?.ErrorMessage ?? "同期に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSync.Enabled = true;
            }
        }

        private async Task ExecuteExportToSsdAsync()
        {
            // 利用可能なドライブの選択
            var readyDrives = DriveInfo.GetDrives().Where(d => d.IsReady).ToList();
            if (readyDrives.Count == 0)
            {
                MessageBox.Show("利用可能なドライブが見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // リムーバブルドライブ優先、なければドライブ選択ダイアログ
            string? targetDrive = readyDrives.FirstOrDefault(d => d.DriveType == DriveType.Removable)?.Name;
            if (string.IsNullOrEmpty(targetDrive))
            {
                using var fbd = new FolderBrowserDialog
                {
                    Description = "ポータブルSSDまたは保存先のドライブ・フォルダを選択してください"
                };
                if (fbd.ShowDialog(this) != DialogResult.OK) return;
                targetDrive = fbd.SelectedPath;
            }

            string category = _isGaigyoMode ? "外業" : "内業";
            string targetPath = Path.Combine(targetDrive, SyncService.SyncBaseFolderName, _siteName, category);

            var confirm = MessageBox.Show(
                $"現在の現場データをポータブルSSDへ出力します。\n\n" +
                $"【出力先】\n{targetPath}\n\n" +
                $"・Site7.db (DB本体)\n" +
                $"・Def/ フォルダ一式 (レイヤ・入力定義.txt)\n" +
                $"・SITE7.ini (現場設定)\n" +
                $"・SITE7.png (最新サムネイル)\n\n" +
                $"出力してよろしいですか？",
                "SSD出力の確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnExportToSsd.Enabled = false;
            btnSync.Enabled = false;
            btnClose.Enabled = false;
            progressBar.Visible = true;
            progressBar.Value = 0;

            SyncResult? result = null;
            await Task.Run(() =>
            {
                result = SyncService.ExportSiteToSsd(
                    _indoorDbPath,
                    _siteName,
                    targetDrive,
                    _isGaigyoMode,
                    (msg, pct) =>
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            lblStatus.Text = msg;
                            progressBar.Value = Math.Clamp(pct, 0, 100);
                        }));
                    });
            });

            progressBar.Visible = false;
            btnExportToSsd.Enabled = true;
            btnClose.Enabled = true;

            if (result != null && result.Success)
            {
                MessageBox.Show($"✔ ポータブルSSDへの出力が完了しました！\n\n【保存先】\n{targetPath}", "出力完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RescanOutdoorSources();
                LoadSyncHistory();
            }
            else
            {
                MessageBox.Show(result?.ErrorMessage ?? "出力に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSyncHistory()
        {
            dgvHistory.Rows.Clear();
            string siteDir = Path.GetDirectoryName(_indoorDbPath) ?? "";
            var history = SyncService.GetSyncHistory(siteDir);

            foreach (var h in history)
            {
                dgvHistory.Rows.Add(
                    h.Timestamp.ToString("yyyy/MM/dd HH:mm:ss"),
                    h.ActionType,
                    h.MachineName,
                    h.CountsDetail,
                    h.DefFiles,
                    h.BackupFile
                );
            }
        }

        private void OpenHistoryLogFile()
        {
            string siteDir = Path.GetDirectoryName(_indoorDbPath) ?? "";
            string logPath = Path.Combine(siteDir, "SyncHistory.tsv");

            if (!File.Exists(logPath))
            {
                MessageBox.Show("同期履歴ログファイルがまだ作成されていません。", "案内", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = logPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ファイルを開けませんでした: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
