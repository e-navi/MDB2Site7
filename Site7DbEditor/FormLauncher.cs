using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Site7DbEditor.Models;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public partial class FormLauncher : Form
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string? SelectedDbPath { get; private set; } = null;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsGaigyoMode { get; private set; } = false;

        private string _currentRootFolder = "";
        private List<SiteItem> _allSites = new List<SiteItem>();
        private List<SiteItem> _filteredSites = new List<SiteItem>();
        private SiteItem? _selectedSite = null;

        private enum ViewMode { ListAndPreview, ThumbnailGrid }
        private ViewMode _currentViewMode = ViewMode.ListAndPreview;

        public FormLauncher()
        {
            InitializeComponent();
            SetupColumns();
        }

        private void SetupColumns()
        {
            var colThumb = new DataGridViewImageColumn
            {
                Name = "Thumb",
                HeaderText = "図面",
                Width = 54,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Resizable = DataGridViewTriState.False
            };
            dgvSites.Columns.Add(colThumb);
            dgvSites.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "現場名", FillWeight = 45 });
            dgvSites.Columns.Add(new DataGridViewTextBoxColumn { Name = "UpdatedAt", HeaderText = "最終更新日時", FillWeight = 32 });
            dgvSites.Columns.Add(new DataGridViewTextBoxColumn { Name = "Size", HeaderText = "サイズ", FillWeight = 23 });
            dgvSites.DataError += (s, e) => { e.ThrowException = false; };
        }

        private void FormLauncher_Load(object? sender, EventArgs e)
        {
            if (DesignMode) return;

            string versionStr = GetAppVersionString();
            this.Text = $"遺跡調査システム Site7 - 現場選択  {versionStr}";
            lblSubtitle.Text = $"現場管理ランチャー  {versionStr}";

            _currentRootFolder = SiteDiscoveryService.GetDefaultRootPath();
            lblCurrentFolder.Text = $"現場フォルダ: {_currentRootFolder}";

            try
            {
                if (splitListPreview.Width > 300)
                {
                    splitListPreview.SplitterDistance = Math.Max(150, splitListPreview.Width - 235);
                }
            }
            catch { }

            RefreshSiteList();
        }

        private void PanelHeader_Paint(object? sender, PaintEventArgs e)
        {
            using var p = new Pen(Color.FromArgb(220, 224, 230));
            e.Graphics.DrawLine(p, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
        }

        private void PanelFooter_Paint(object? sender, PaintEventArgs e)
        {
            using var p = new Pen(Color.FromArgb(220, 224, 230));
            e.Graphics.DrawLine(p, 0, 0, panelFooter.Width, 0);
        }

        private void BtnViewList_Click(object? sender, EventArgs e)
        {
            SwitchViewMode(ViewMode.ListAndPreview);
        }

        private void BtnViewGrid_Click(object? sender, EventArgs e)
        {
            SwitchViewMode(ViewMode.ThumbnailGrid);
        }

        private void BtnOpenGaigyo_Click(object? sender, EventArgs e)
        {
            ConfirmAndOpenSite(isGaigyo: true);
        }

        private void BtnOpenNaigyo_Click(object? sender, EventArgs e)
        {
            ConfirmAndOpenSite(isGaigyo: false);
        }

        private void BtnNaigyoOption_Click(object? sender, EventArgs e)
        {
            menuNaigyoOption.Show(btnNaigyoOption, new Point(0, -menuNaigyoOption.PreferredSize.Height));
        }

        private void ItemDrawing_Click(object? sender, EventArgs e)
        {
            LaunchDrawingEditor();
        }

        private void ItemSection_Click(object? sender, EventArgs e)
        {
            LaunchSectionEditor();
        }

        private void BtnTool_Click(object? sender, EventArgs e)
        {
            menuTool.Show(btnTool, new Point(0, -menuTool.PreferredSize.Height));
        }

        private void ItemMasterDef_Click(object? sender, EventArgs e)
        {
            using var form = new FormMasterSettings(null);
            form.ShowDialog(this);
        }

        private void ItemMasterLayer_Click(object? sender, EventArgs e)
        {
            using var form = new FormLayerSettings(dbPath: (string?)null);
            form.ShowDialog(this);
        }

        private void ItemMasterEnv_Click(object? sender, EventArgs e)
        {
            using var form = new FormDefEnv(isMasterMode: true);
            form.ShowDialog(this);
        }

        private void ItemExporter_Click(object? sender, EventArgs e)
        {
            LaunchMdbFdbExporter();
        }

        private void BtnExit_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void DgvSites_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ConfirmAndOpenSite(isGaigyo: false);
            }
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

        public void RefreshSiteList()
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
            string lastOpenedDb = Def.GetIniStr("Site7DbEditor", "LastOpenedDb");
            int selectRowIdx = -1;

            for (int i = 0; i < _filteredSites.Count; i++)
            {
                var site = _filteredSites[i];
                Image thumb = site.GetThumbnailImage() ?? CreateDefaultPlaceholderImage();
                int rowIdx = dgvSites.Rows.Add(thumb, site.Name, site.DisplayUpdatedAt, site.DisplaySize);
                dgvSites.Rows[rowIdx].Tag = site;

                if (!string.IsNullOrEmpty(lastOpenedDb) &&
                    (string.Equals(site.DbPath, lastOpenedDb, StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(site.FolderPath, lastOpenedDb, StringComparison.OrdinalIgnoreCase)))
                {
                    selectRowIdx = rowIdx;
                }
            }

            if (dgvSites.Rows.Count > 0)
            {
                int targetIdx = (selectRowIdx >= 0) ? selectRowIdx : 0;
                dgvSites.Rows[targetIdx].Selected = true;
                try { dgvSites.CurrentCell = dgvSites.Rows[targetIdx].Cells[1]; } catch { }
                _selectedSite = dgvSites.Rows[targetIdx].Tag as SiteItem;
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

            pnl.DoubleClick += (s, e) => ConfirmAndOpenSite(isGaigyo: false);
            pic.DoubleClick += (s, e) => ConfirmAndOpenSite(isGaigyo: false);
            lblName.DoubleClick += (s, e) => ConfirmAndOpenSite(isGaigyo: false);

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
                btnNaigyoOption.Enabled = true;
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
                btnNaigyoOption.Enabled = false;
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

        private void ConfirmAndOpenSite(bool isGaigyo)
        {
            if (_selectedSite == null || string.IsNullOrEmpty(_selectedSite.DbPath))
            {
                MessageBox.Show("開く現場を選択してください。", "選択確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.SelectedDbPath = _selectedSite.DbPath;
            this.IsGaigyoMode = isGaigyo;
            Def.SetIniStr("Site7DbEditor", "LastOpenedDb", _selectedSite.DbPath);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LaunchDrawingEditor()
        {
            if (_selectedSite == null || string.IsNullOrEmpty(_selectedSite.DbPath))
            {
                MessageBox.Show("開く現場を選択してください。", "選択確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Def.SetIniStr("Site7DbEditor", "LastOpenedDb", _selectedSite.DbPath);

            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] candidateExePaths = new[]
            {
                Path.Combine(appDir, "Site7DrawingEditor.exe"),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\Site7DrawingEditor\bin\Debug\net9.0-windows\Site7DrawingEditor.exe")),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\Site7DrawingEditor\bin\Release\net9.0-windows\Site7DrawingEditor.exe")),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\..\Site7DrawingEditor\bin\Debug\net9.0-windows\Site7DrawingEditor.exe")),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\Site7DrawingEditor\bin\Debug\net9.0-windows\Site7DrawingEditor.exe"))
            };

            string? targetExe = candidateExePaths.FirstOrDefault(File.Exists);

            try
            {
                if (targetExe != null)
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = targetExe,
                        Arguments = $"\"{_selectedSite.DbPath}\"",
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                else
                {
                    string projectPath = Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\Site7DrawingEditor\Site7DrawingEditor.csproj"));
                    if (!File.Exists(projectPath))
                    {
                        projectPath = Path.GetFullPath(Path.Combine(appDir, @"..\..\..\Site7DrawingEditor\Site7DrawingEditor.csproj"));
                    }

                    if (File.Exists(projectPath))
                    {
                        var psi = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "dotnet",
                            Arguments = $"run --project \"{projectPath}\" -- \"{_selectedSite.DbPath}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        System.Diagnostics.Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("Site7DrawingEditor が見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Site7DrawingEditor の起動に失敗しました: {ex.Message}", "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LaunchSectionEditor()
        {
            if (_selectedSite == null || string.IsNullOrEmpty(_selectedSite.DbPath))
            {
                MessageBox.Show("現場を選択してください。", "選択確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("調査区断面図作成機能は現在準備中です。", "調査区断面図", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LaunchMdbFdbExporter()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] candidateExePaths = new[]
            {
                Path.Combine(appDir, "MdbFdbExporter.exe"),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\MdbFdbExporter\bin\Debug\net9.0-windows\MdbFdbExporter.exe")),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\MdbFdbExporter\bin\Release\net9.0-windows\MdbFdbExporter.exe")),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\..\MdbFdbExporter\bin\Debug\net9.0-windows\MdbFdbExporter.exe")),
                Path.GetFullPath(Path.Combine(appDir, @"..\..\MdbFdbExporter\bin\Debug\net9.0-windows\MdbFdbExporter.exe"))
            };

            string? targetExe = candidateExePaths.FirstOrDefault(File.Exists);

            try
            {
                if (targetExe != null)
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = targetExe,
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                else
                {
                    string projectPath = Path.GetFullPath(Path.Combine(appDir, @"..\..\..\..\MdbFdbExporter\MdbFdbExporter.csproj"));
                    if (!File.Exists(projectPath))
                    {
                        projectPath = Path.GetFullPath(Path.Combine(appDir, @"..\..\..\MdbFdbExporter\MdbFdbExporter.csproj"));
                    }

                    if (File.Exists(projectPath))
                    {
                        var psi = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "dotnet",
                            Arguments = $"run --project \"{projectPath}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        System.Diagnostics.Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("MdbFdbExporter が見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MdbFdbExporter の起動に失敗しました: {ex.Message}", "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetAppVersionString()
        {
            try
            {
                string infoVersion = System.Reflection.Assembly.GetExecutingAssembly()
                    .GetCustomAttribute<System.Reflection.AssemblyInformationalVersionAttribute>()?
                    .InformationalVersion ?? "0.9.6";

                string verStr = "v0.9.6";
                string gitHash = "";

                if (infoVersion.Contains("+"))
                {
                    var parts = infoVersion.Split('+');
                    verStr = $"v{parts[0]}";
                    gitHash = parts[1].Length >= 7 ? parts[1].Substring(0, 7) : parts[1];
                }
                else
                {
                    verStr = $"v{infoVersion}";
                }

                return !string.IsNullOrEmpty(gitHash) ? $"{verStr} ({gitHash})" : verStr;
            }
            catch
            {
                return "v0.9.6";
            }
        }
    }
}
