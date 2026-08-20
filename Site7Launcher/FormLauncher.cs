using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Site7Launcher.Models;
using Site7Launcher.Services;

namespace Site7Launcher
{
    public class FormLauncher : Form
    {
        private string _currentRootFolder = "";
        private List<SiteItem> _allSites = new List<SiteItem>();
        private List<SiteItem> _filteredSites = new List<SiteItem>();
        private SiteItem? _selectedSite = null;

        private enum ViewMode { ListAndPreview, ThumbnailGrid }
        private ViewMode _currentViewMode = ViewMode.ListAndPreview;

        // UI Controls
        private Panel panelHeader = null!;
        private Panel panelMain = null!;
        private Panel panelFooter = null!;

        // Header controls
        private Label lblTitle = null!;
        private TextBox txtSearch = null!;
        private Button btnBrowseFolder = null!;
        private Label lblCurrentFolder = null!;
        private Button btnViewList = null!;
        private Button btnViewGrid = null!;

        // Mode 1: List + Preview
        private SplitContainer splitListPreview = null!;
        private DataGridView dgvSites = null!;
        private Panel panelPreviewCard = null!;
        private PictureBox picPreview = null!;
        private Label lblPreviewName = null!;
        private Label lblPreviewDate = null!;
        private Label lblPreviewSize = null!;
        private Label lblPreviewPath = null!;

        // Mode 2: Thumbnail Grid
        private FlowLayoutPanel flowThumbnails = null!;

        // Footer controls
        private Button btnNewSite = null!;
        private Button btnOpenGaigyo = null!;
        private Button btnOpenNaigyo = null!;
        private Button btnExit = null!;

        public FormLauncher()
        {
            InitializeComponent();
            _currentRootFolder = SiteDiscoveryService.GetDefaultRootPath();
            lblCurrentFolder.Text = $"現場フォルダ: {_currentRootFolder}";
            RefreshSiteList();
        }

        private void InitializeComponent()
        {
            this.Text = "遺跡調査システム Site7 - 現場選択";
            this.Size = new Size(1000, 680);
            this.MinimumSize = new Size(820, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular);
            this.BackColor = Color.FromArgb(244, 246, 249);

            // 1. Header Panel
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = Color.White,
                Padding = new Padding(15, 10, 15, 10)
            };
            panelHeader.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(220, 224, 230));
                e.Graphics.DrawLine(p, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
            };

            lblTitle = new Label
            {
                Text = "遺跡調査システム Site7",
                Font = new Font("Yu Gothic UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 32, 47),
                Location = new Point(15, 8),
                AutoSize = true
            };

            var lblSubtitle = new Label
            {
                Text = "現場管理ランチャー",
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(120, 130, 145),
                Location = new Point(255, 14),
                AutoSize = true
            };

            lblCurrentFolder = new Label
            {
                Text = "現場フォルダ: ...",
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 110, 125),
                Location = new Point(16, 45),
                Size = new Size(380, 20),
                AutoEllipsis = true
            };

            btnBrowseFolder = new Button
            {
                Text = "📂 フォルダ変更",
                Location = new Point(400, 40),
                Size = new Size(115, 28),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(235, 238, 243),
                FlatStyle = FlatStyle.Flat
            };
            btnBrowseFolder.FlatAppearance.BorderColor = Color.FromArgb(200, 205, 215);
            btnBrowseFolder.Click += BtnBrowseFolder_Click;

            txtSearch = new TextBox
            {
                Location = new Point(530, 40),
                Size = new Size(200, 26),
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular),
                PlaceholderText = "🔍 現場名を検索..."
            };
            txtSearch.TextChanged += (s, e) => ApplyFilter();

            btnViewList = new Button
            {
                Text = "📄 リスト",
                Location = new Point(745, 38),
                Size = new Size(95, 30),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnViewList.FlatAppearance.BorderSize = 0;
            btnViewList.Click += (s, e) => SwitchViewMode(ViewMode.ListAndPreview);

            btnViewGrid = new Button
            {
                Text = "🖼 グリッド",
                Location = new Point(845, 38),
                Size = new Size(95, 30),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(235, 238, 243),
                ForeColor = Color.FromArgb(50, 60, 75),
                FlatStyle = FlatStyle.Flat
            };
            btnViewGrid.FlatAppearance.BorderColor = Color.FromArgb(200, 205, 215);
            btnViewGrid.Click += (s, e) => SwitchViewMode(ViewMode.ThumbnailGrid);

            panelHeader.Controls.AddRange(new Control[] {
                lblTitle, lblSubtitle, lblCurrentFolder, btnBrowseFolder,
                txtSearch, btnViewList, btnViewGrid
            });

            // 2. Footer Panel
            panelFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.White,
                Padding = new Padding(20, 12, 20, 12)
            };
            panelFooter.Paint += (s, e) =>
            {
                using var p = new Pen(Color.FromArgb(220, 224, 230));
                e.Graphics.DrawLine(p, 0, 0, panelFooter.Width, 0);
            };

            btnNewSite = new Button
            {
                Text = "＋ 新規現場",
                Location = new Point(20, 12),
                Size = new Size(130, 38),
                Font = new Font("Yu Gothic UI", 10.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(240, 243, 248),
                ForeColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat
            };
            btnNewSite.FlatAppearance.BorderColor = Color.FromArgb(190, 200, 215);
            btnNewSite.Click += BtnNewSite_Click;

            btnOpenGaigyo = new Button
            {
                Text = "📡 外業で開く",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(570, 12),
                Size = new Size(130, 38),
                Font = new Font("Yu Gothic UI", 10.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(34, 197, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOpenGaigyo.FlatAppearance.BorderSize = 0;
            btnOpenGaigyo.Click += (s, e) => LaunchSiteEditor(isGaigyo: true);

            btnOpenNaigyo = new Button
            {
                Text = "💻 内業で開く",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(710, 12),
                Size = new Size(130, 38),
                Font = new Font("Yu Gothic UI", 10.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(14, 116, 144),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOpenNaigyo.FlatAppearance.BorderSize = 0;
            btnOpenNaigyo.Click += (s, e) => LaunchSiteEditor(isGaigyo: false);

            btnExit = new Button
            {
                Text = "✖ 終了",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(850, 12),
                Size = new Size(110, 38),
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(100, 116, 139),
                FlatStyle = FlatStyle.Flat
            };
            btnExit.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnExit.Click += (s, e) => this.Close();

            panelFooter.Controls.AddRange(new Control[] {
                btnNewSite, btnOpenGaigyo, btnOpenNaigyo, btnExit
            });

            // 3. Main Panel (Container)
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(244, 246, 249),
                Padding = new Padding(15)
            };

            // Mode 1: SplitContainer (List + Preview)
            splitListPreview = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 600,
                SplitterWidth = 8,
                BackColor = Color.FromArgb(244, 246, 249)
            };

            dgvSites = new DataGridView
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
                Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular),
                RowTemplate = { Height = 34 }
            };
            dgvSites.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "現場名", FillWeight = 45 });
            dgvSites.Columns.Add(new DataGridViewTextBoxColumn { Name = "UpdatedAt", HeaderText = "最終更新日時", FillWeight = 30 });
            dgvSites.Columns.Add(new DataGridViewTextBoxColumn { Name = "Size", HeaderText = "サイズ", FillWeight = 25 });
            dgvSites.SelectionChanged += DgvSites_SelectionChanged;
            dgvSites.CellDoubleClick += (s, e) => LaunchSiteEditor(isGaigyo: false);

            var panelDgvWrapper = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(1)
            };
            panelDgvWrapper.Controls.Add(dgvSites);
            splitListPreview.Panel1.Controls.Add(panelDgvWrapper);

            // Preview Card Panel
            panelPreviewCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lblPreviewHeader = new Label
            {
                Text = "現場プレビュー",
                Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(15, 12),
                AutoSize = true
            };

            picPreview = new PictureBox
            {
                Location = new Point(15, 45),
                Size = new Size(256, 256),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            lblPreviewName = new Label
            {
                Text = "現場名: -",
                Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(15, 315),
                Size = new Size(320, 26),
                AutoEllipsis = true
            };

            lblPreviewDate = new Label
            {
                Text = "更新日時: -",
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(15, 345),
                Size = new Size(320, 20)
            };

            lblPreviewSize = new Label
            {
                Text = "データ容量: -",
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(15, 370),
                Size = new Size(320, 20)
            };

            lblPreviewPath = new Label
            {
                Text = "フォルダ: -",
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(15, 400),
                Size = new Size(320, 40),
                AutoEllipsis = true
            };

            panelPreviewCard.Controls.AddRange(new Control[] {
                lblPreviewHeader, picPreview, lblPreviewName,
                lblPreviewDate, lblPreviewSize, lblPreviewPath
            });
            splitListPreview.Panel2.Controls.Add(panelPreviewCard);

            // Mode 2: Thumbnail Flow Panel (Grid)
            flowThumbnails = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(244, 246, 249),
                Padding = new Padding(10),
                Visible = false
            };

            panelMain.Controls.Add(splitListPreview);
            panelMain.Controls.Add(flowThumbnails);

            this.Controls.Add(panelMain);
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelFooter);
        }

        private void SwitchViewMode(ViewMode mode)
        {
            _currentViewMode = mode;
            if (mode == ViewMode.ListAndPreview)
            {
                btnViewList.BackColor = Color.FromArgb(0, 122, 255);
                btnViewList.ForeColor = Color.White;
                btnViewList.FlatAppearance.BorderSize = 0;

                btnViewGrid.BackColor = Color.FromArgb(235, 238, 243);
                btnViewGrid.ForeColor = Color.FromArgb(50, 60, 75);
                btnViewGrid.FlatAppearance.BorderSize = 1;

                splitListPreview.Visible = true;
                flowThumbnails.Visible = false;
                UpdateListSelection();
            }
            else
            {
                btnViewGrid.BackColor = Color.FromArgb(0, 122, 255);
                btnViewGrid.ForeColor = Color.White;
                btnViewGrid.FlatAppearance.BorderSize = 0;

                btnViewList.BackColor = Color.FromArgb(235, 238, 243);
                btnViewList.ForeColor = Color.FromArgb(50, 60, 75);
                btnViewList.FlatAppearance.BorderSize = 1;

                splitListPreview.Visible = false;
                flowThumbnails.Visible = true;
                PopulateThumbnailGrid();
            }
        }

        private void RefreshSiteList()
        {
            _allSites = SiteDiscoveryService.DiscoverSites(_currentRootFolder);
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                _filteredSites = new List<SiteItem>(_allSites);
            }
            else
            {
                _filteredSites = _allSites
                    .Where(s => s.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            PopulateListView();
            if (_currentViewMode == ViewMode.ThumbnailGrid)
            {
                PopulateThumbnailGrid();
            }
        }

        private void PopulateListView()
        {
            dgvSites.Rows.Clear();
            foreach (var site in _filteredSites)
            {
                int rowIdx = dgvSites.Rows.Add(site.Name, site.DisplayUpdatedAt, site.DisplaySize);
                dgvSites.Rows[rowIdx].Tag = site;
            }

            if (dgvSites.Rows.Count > 0)
            {
                dgvSites.Rows[0].Selected = true;
                _selectedSite = dgvSites.Rows[0].Tag as SiteItem;
                UpdatePreviewCard(_selectedSite);
            }
            else
            {
                _selectedSite = null;
                UpdatePreviewCard(null);
            }
        }

        private void PopulateThumbnailGrid()
        {
            flowThumbnails.SuspendLayout();
            flowThumbnails.Controls.Clear();

            foreach (var site in _filteredSites)
            {
                var card = CreateSiteCard(site);
                flowThumbnails.Controls.Add(card);
            }

            flowThumbnails.ResumeLayout();
        }

        private Control CreateSiteCard(SiteItem site)
        {
            var pnl = new Panel
            {
                Size = new Size(200, 230),
                BackColor = (_selectedSite == site) ? Color.FromArgb(235, 243, 255) : Color.White,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = site
            };

            pnl.Paint += (s, e) =>
            {
                bool isSelected = (_selectedSite == site);
                using var borderPen = new Pen(isSelected ? Color.FromArgb(0, 122, 255) : Color.FromArgb(220, 224, 230), isSelected ? 2.5f : 1f);
                e.Graphics.DrawRectangle(borderPen, 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            var pic = new PictureBox
            {
                Location = new Point(8, 8),
                Size = new Size(184, 150),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(250, 250, 252),
                Image = site.GetThumbnailImage() ?? CreateDefaultPlaceholderImage()
            };

            var lblName = new Label
            {
                Text = site.Name,
                Font = new Font("Yu Gothic UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(6, 162),
                Size = new Size(188, 24),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true
            };

            var lblDate = new Label
            {
                Text = site.DisplayUpdatedAt,
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(6, 188),
                Size = new Size(188, 18),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Event forwarding for click & double-click
            Action selectCard = () =>
            {
                _selectedSite = site;
                UpdatePreviewCard(site);
                foreach (Control c in flowThumbnails.Controls)
                {
                    if (c is Panel cardPnl)
                    {
                        cardPnl.BackColor = (cardPnl.Tag == _selectedSite) ? Color.FromArgb(235, 243, 255) : Color.White;
                        cardPnl.Invalidate();
                    }
                }
            };

            pnl.Click += (s, e) => selectCard();
            pic.Click += (s, e) => selectCard();
            lblName.Click += (s, e) => selectCard();
            lblDate.Click += (s, e) => selectCard();

            pnl.DoubleClick += (s, e) => LaunchSiteEditor(isGaigyo: false);
            pic.DoubleClick += (s, e) => LaunchSiteEditor(isGaigyo: false);
            lblName.DoubleClick += (s, e) => LaunchSiteEditor(isGaigyo: false);

            pnl.Controls.AddRange(new Control[] { pic, lblName, lblDate });
            return pnl;
        }

        private Image CreateDefaultPlaceholderImage()
        {
            var bmp = new Bitmap(184, 150);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(245, 247, 250));
            using var p = new Pen(Color.FromArgb(210, 215, 225));
            g.DrawRectangle(p, 0, 0, bmp.Width - 1, bmp.Height - 1);
            using var font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);
            using var brush = new SolidBrush(Color.FromArgb(140, 150, 165));
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("No Preview\n(SITE7.png)", font, brush, new RectangleF(0, 0, bmp.Width, bmp.Height), sf);
            return bmp;
        }

        private void DgvSites_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvSites.SelectedRows.Count > 0)
            {
                _selectedSite = dgvSites.SelectedRows[0].Tag as SiteItem;
                UpdatePreviewCard(_selectedSite);
            }
        }

        private void UpdateListSelection()
        {
            if (_selectedSite == null) return;
            foreach (DataGridViewRow row in dgvSites.Rows)
            {
                if (row.Tag == _selectedSite)
                {
                    row.Selected = true;
                    break;
                }
            }
        }

        private void UpdatePreviewCard(SiteItem? site)
        {
            if (site != null)
            {
                picPreview.Image = site.GetThumbnailImage() ?? CreateDefaultPlaceholderImage();
                lblPreviewName.Text = $"現場名: {site.Name}";
                lblPreviewDate.Text = $"更新日時: {site.DisplayUpdatedAt}";
                lblPreviewSize.Text = $"データ容量: {site.DisplaySize}";
                lblPreviewPath.Text = $"フォルダ: {site.FolderPath}";
                btnOpenGaigyo.Enabled = true;
                btnOpenNaigyo.Enabled = true;
            }
            else
            {
                picPreview.Image = null;
                lblPreviewName.Text = "現場名: (選択なし)";
                lblPreviewDate.Text = "更新日時: -";
                lblPreviewSize.Text = "データ容量: -";
                lblPreviewPath.Text = "フォルダ: -";
                btnOpenGaigyo.Enabled = false;
                btnOpenNaigyo.Enabled = false;
            }
        }

        private void BtnBrowseFolder_Click(object? sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Site7 の現場データが保存されているフォルダを選択してください",
                SelectedPath = Directory.Exists(_currentRootFolder) ? _currentRootFolder : AppDomain.CurrentDomain.BaseDirectory
            };

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                _currentRootFolder = fbd.SelectedPath;
                lblCurrentFolder.Text = $"現場フォルダ: {_currentRootFolder}";
                RefreshSiteList();
            }
        }

        private void BtnNewSite_Click(object? sender, EventArgs e)
        {
            using var prompt = new Form
            {
                Width = 420,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "新規現場の作成",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Regular)
            };

            var textLabel = new Label { Left = 20, Top = 18, Text = "現場名を入力してください:", AutoSize = true };
            var textBox = new TextBox { Left = 20, Top = 42, Width = 360, Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold) };
            var confirmation = new Button { Text = "作成", Left = 200, Width = 85, Top = 85, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(0, 122, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            var cancel = new Button { Text = "キャンセル", Left = 295, Width = 85, Top = 85, DialogResult = DialogResult.Cancel, BackColor = Color.FromArgb(240, 240, 240), FlatStyle = FlatStyle.Flat };

            prompt.Controls.AddRange(new Control[] { textLabel, textBox, confirmation, cancel });
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            if (prompt.ShowDialog(this) == DialogResult.OK)
            {
                string siteName = textBox.Text.Trim();
                if (string.IsNullOrEmpty(siteName))
                {
                    MessageBox.Show("現場名を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string newFolderPath = Path.Combine(_currentRootFolder, siteName);
                if (Directory.Exists(newFolderPath))
                {
                    MessageBox.Show("同名の現場フォルダが既に存在します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    Directory.CreateDirectory(newFolderPath);
                    RefreshSiteList();

                    var createdSite = _allSites.FirstOrDefault(s => s.FolderPath == newFolderPath);
                    if (createdSite != null)
                    {
                        _selectedSite = createdSite;
                        UpdateListSelection();
                    }
                    MessageBox.Show($"現場 [{siteName}] を作成しました！", "作成完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"現場作成時にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LaunchSiteEditor(bool isGaigyo)
        {
            if (_selectedSite == null || string.IsNullOrEmpty(_selectedSite.DbPath))
            {
                MessageBox.Show("開く現場を選択してください。", "選択確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Site7DbEditor を起動
                string appDir = AppDomain.CurrentDomain.BaseDirectory;
                string editorExe = Path.Combine(appDir, "Site7DbEditor.exe");

                if (!File.Exists(editorExe))
                {
                    // 開発環境用の代替パス
                    string[] candidates = new[]
                    {
                        Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\Site7DbEditor\bin\Debug\net9.0-windows\Site7DbEditor.exe")),
                        Path.GetFullPath(Path.Combine(appDir, @"..\Site7DbEditor\Site7DbEditor.exe"))
                    };
                    editorExe = candidates.FirstOrDefault(File.Exists) ?? editorExe;
                }

                if (File.Exists(editorExe))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = editorExe,
                        Arguments = $"\"{_selectedSite.DbPath}\""
                    };
                    Process.Start(psi);
                }
                else
                {
                    MessageBox.Show($"Site7DbEditor.exe が見つかりませんでした。\n検索パス: {editorExe}", "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"本体プログラム起動エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
