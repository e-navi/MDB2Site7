using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MdbFdbExporter
{
    public partial class FormMain : Form
    {
        private bool _isSite5 = true;
        private string _dbRootFolder = @"c:\Proj\Antigravity\MDB2Site7";
        private string _outFolder = @"c:\Proj\Antigravity\MDB2Site7\Exported_CSV";
        private string _activeDbFolder = @"c:\Proj\Antigravity\MDB2Site7";

        private List<string> _uniqueGroupNames = new List<string>();
        private List<GroupPointData> _pointData = new List<GroupPointData>();

        // Embedded 2D Canvas Zoom & Pan (Top View)
        private float _zoomFactor2D = 1.0f;
        private PointF _panOffset2D = PointF.Empty;
        private bool _isMouseDown2D = false;
        private bool _isPanning2D = false;
        private Point _mouseDownPos2D;
        private Point _lastMousePos2D;

        // All Features Canvas Zoom & Pan (Bottom View)
        private float _zoomFactorAll = 1.0f;
        private PointF _panOffsetAll = PointF.Empty;
        private bool _isMouseDownAll = false;
        private bool _isPanningAll = false;
        private Point _mouseDownPosAll;
        private Point _lastMousePosAll;

        private bool _isSyncingSelection = false;

        private readonly Dictionary<string, RectangleF> _ikouLabelRectsAll = new Dictionary<string, RectangleF>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, List<PointF>> _ikouScreenPointsAll = new Dictionary<string, List<PointF>>(StringComparer.OrdinalIgnoreCase);

        private static readonly Color[] LineColors = new Color[]
        {
            Color.FromArgb(0, 225, 255),   // Cyan
            Color.FromArgb(50, 205, 50),   // Lime Green
            Color.FromArgb(255, 191, 0),   // Amber
            Color.FromArgb(255, 0, 128),   // Magenta/Pink
            Color.FromArgb(153, 102, 255), // Purple
            Color.FromArgb(255, 128, 0),   // Orange
            Color.FromArgb(0, 204, 153)    // Teal
        };

        public class ComboBoxItem
        {
            public string Text { get; set; } = "";
            public SplitRule Rule { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public class DbFolderItem
        {
            public string DisplayName { get; set; } = "";
            public string FullPath { get; set; } = "";

            public override string ToString()
            {
                return DisplayName;
            }
        }

        public FormMain()
        {
            InitializeComponent();
            SetupDataGridViewStyle();
            SetupPointsDataGridViewStyle();

            // Wire up event handlers
            this.Load += FormMain_Load;
            this.btnSettings.Click += btnSettings_Click;
            this.btnShowLog.Click += (s, e) => FormLog.ShowLogWindow(this);
            this.lstDbFolders.SelectedIndexChanged += lstDbFolders_SelectedIndexChanged;
            this.btnAnalyze.Click += btnAnalyze_Click;
            this.btnExport.Click += btnExport_Click;

            this.cmbRule1.SelectedIndexChanged += cmbRule_SelectedIndexChanged;
            this.cmbRule2.SelectedIndexChanged += cmbRule_SelectedIndexChanged;
            this.cmbRule3.SelectedIndexChanged += cmbRule_SelectedIndexChanged;
            this.txtRegexPattern1.TextChanged += txtRegexPattern_TextChanged;
            this.txtRegexPattern2.TextChanged += txtRegexPattern_TextChanged;
            this.txtRegexPattern3.TextChanged += txtRegexPattern_TextChanged;

            this.lstIkou.SelectedIndexChanged += lstIkou_SelectedIndexChanged;
            this.lstIkouLine.SelectedIndexChanged += lstIkouLine_SelectedIndexChanged;
            this.lstIkou.MouseDoubleClick += lstIkou_MouseDoubleClick;
            this.dgvPreview.SelectionChanged += dgvPreview_SelectionChanged;
            this.dgvPreview.CellDoubleClick += dgvPreview_CellDoubleClick;
            this.btnOpenViewer.Click += btnOpenViewer_Click;

            // View toggle handlers (Grid vs 2D Canvas)
            this.btnToggleGrid.Click += btnToggleGrid_Click;
            this.btnToggle2D.Click += btnToggle2D_Click;

            // Embedded Single-Feature 2D Canvas handlers
            this.picEmbedded2D.Paint += picEmbedded2D_Paint;
            this.picEmbedded2D.MouseDown += picEmbedded2D_MouseDown;
            this.picEmbedded2D.MouseMove += picEmbedded2D_MouseMove;
            this.picEmbedded2D.MouseUp += picEmbedded2D_MouseUp;
            this.picEmbedded2D.MouseWheel += picEmbedded2D_MouseWheel;
            this.picEmbedded2D.MouseDoubleClick += picEmbedded2D_MouseDoubleClick;

            // All Features 2D Canvas handlers
            this.picAllIkouCanvas.Paint += picAllIkouCanvas_Paint;
            this.picAllIkouCanvas.MouseDown += picAllIkouCanvas_MouseDown;
            this.picAllIkouCanvas.MouseMove += picAllIkouCanvas_MouseMove;
            this.picAllIkouCanvas.MouseUp += picAllIkouCanvas_MouseUp;
            this.picAllIkouCanvas.MouseWheel += picAllIkouCanvas_MouseWheel;
            this.picAllIkouCanvas.MouseDoubleClick += picAllIkouCanvas_MouseDoubleClick;
        }

        public void ApplyConfiguration(bool isSite5, string dbRootFolder, string outFolder)
        {
            _isSite5 = isSite5;
            _dbRootFolder = dbRootFolder;
            _outFolder = outFolder;

            lblSubHeader.Text = $"User Mode: {(_isSite5 ? "Site5 (Access MDB)" : "Site6 (Firebird FDB)")} | CSV Out: {_outFolder}";
            Log($"Configuration applied: Mode={(_isSite5 ? "Site5 MDB" : "Site6 FDB")}, Root={_dbRootFolder}");

            PopulateDbFolderList();
        }

        private void PopulateDbFolderList()
        {
            lstDbFolders.BeginUpdate();
            lstDbFolders.Items.Clear();

            if (!string.IsNullOrEmpty(_dbRootFolder) && Directory.Exists(_dbRootFolder))
            {
                // Check if root folder contains MDB/FDB directly
                bool rootHasDb = _isSite5 ?
                    (File.Exists(Path.Combine(_dbRootFolder, "IBUTU.MDB")) || File.Exists(Path.Combine(_dbRootFolder, "IKOU.MDB"))) :
                    (File.Exists(Path.Combine(_dbRootFolder, "GENBA_DATA.FDB")) || File.Exists(Path.Combine(_dbRootFolder, "IBUTU.FDB")));

                if (rootHasDb)
                {
                    lstDbFolders.Items.Add(new DbFolderItem { DisplayName = ". (Root Directory)", FullPath = _dbRootFolder });
                }

                // Add all subdirectories
                try
                {
                    var subDirs = Directory.GetDirectories(_dbRootFolder);
                    foreach (var dir in subDirs)
                    {
                        string dirName = Path.GetFileName(dir);
                        lstDbFolders.Items.Add(new DbFolderItem { DisplayName = dirName, FullPath = dir });
                    }
                }
                catch (Exception ex)
                {
                    Log($"[WARNING] Error enumerating subdirectories: {ex.Message}");
                }
            }

            lstDbFolders.EndUpdate();

            if (lstDbFolders.Items.Count > 0)
            {
                lstDbFolders.SelectedIndex = 0;
            }
            else
            {
                lblSelectedDbStatus.Text = "✖ DBフォルダが見つかりません";
                lblSelectedDbStatus.ForeColor = Color.FromArgb(239, 35, 60);
            }
        }

        private void PopulateRuleComboBox(ComboBox cmb, bool isFallbackRule)
        {
            cmb.Items.Add(new ComboBoxItem { Text = "最初の数字の終わりで分割 (P46S2U➡P46/S2Uなど)", Rule = SplitRule.FeatureNumberEnd });
            cmb.Items.Add(new ComboBoxItem { Text = "日本語サフィックス (上端/下端など)", Rule = SplitRule.JapaneseSuffix });
            cmb.Items.Add(new ComboBoxItem { Text = "区切り文字・単語を指定して分割", Rule = SplitRule.DelimiterList });
            cmb.Items.Add(new ComboBoxItem { Text = "最後のハイフン (-) で分割", Rule = SplitRule.LastHyphen });
            cmb.Items.Add(new ComboBoxItem { Text = "最後のアンダースコア (_) で分割", Rule = SplitRule.LastUnderscore });
            cmb.Items.Add(new ComboBoxItem { Text = "正規表現 (カスタムパターン)", Rule = SplitRule.CustomRegex });

            if (isFallbackRule)
            {
                cmb.Items.Add(new ComboBoxItem { Text = "なし (適用しない)", Rule = SplitRule.NoSplit });
            }
            else
            {
                cmb.Items.Add(new ComboBoxItem { Text = "分割しない (元の名前のまま)", Rule = SplitRule.NoSplit });
            }
        }

        private void FormMain_Load(object? sender, EventArgs e)
        {
            Log("Converter main window initialized.");

            PopulateRuleComboBox(cmbRule1, false);
            PopulateRuleComboBox(cmbRule2, true);
            PopulateRuleComboBox(cmbRule3, true);

            cmbRule1.SelectedIndex = 0; // Default to Feature Number End
            cmbRule2.SelectedIndex = 1; // Default to Japanese Suffix
            cmbRule3.SelectedIndex = 5; // Default to NoSplit / なし

            if (lstDbFolders.Items.Count == 0)
            {
                PopulateDbFolderList();
            }
        }

        private void btnSettings_Click(object? sender, EventArgs e)
        {
            this.Close(); // Return to FormConfig
        }

        private void lstDbFolders_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstDbFolders.SelectedItem is DbFolderItem item)
            {
                _activeDbFolder = item.FullPath;
                Log($"Selected DB Folder: {item.DisplayName} ({item.FullPath})");
                CheckActiveFolderFiles();
                btnAnalyze_Click(sender, e); // Auto analyze on folder change
            }
        }

        private void CheckActiveFolderFiles()
        {
            if (string.IsNullOrEmpty(_activeDbFolder) || !Directory.Exists(_activeDbFolder))
            {
                lblSelectedDbStatus.Text = "✖ フォルダ未指定";
                lblSelectedDbStatus.ForeColor = Color.FromArgb(239, 35, 60);
                return;
            }

            if (_isSite5)
            {
                bool ibutuOk = File.Exists(Path.Combine(_activeDbFolder, "IBUTU.MDB"));
                bool ikouOk = File.Exists(Path.Combine(_activeDbFolder, "IKOU.MDB"));
                if (ibutuOk || ikouOk)
                {
                    lblSelectedDbStatus.Text = $"✔ MDB検出: IBUTU={(ibutuOk ? "OK" : "無")} / IKOU={(ikouOk ? "OK" : "無")}";
                    lblSelectedDbStatus.ForeColor = Color.FromArgb(56, 176, 0);
                }
                else
                {
                    lblSelectedDbStatus.Text = "✖ MDBファイルが見つかりません";
                    lblSelectedDbStatus.ForeColor = Color.FromArgb(239, 35, 60);
                }
            }
            else
            {
                bool fdbOk = File.Exists(Path.Combine(_activeDbFolder, "GENBA_DATA.FDB")) || File.Exists(Path.Combine(_activeDbFolder, "IBUTU.FDB"));
                if (fdbOk)
                {
                    lblSelectedDbStatus.Text = "✔ FDBデータベースを検出";
                    lblSelectedDbStatus.ForeColor = Color.FromArgb(56, 176, 0);
                }
                else
                {
                    lblSelectedDbStatus.Text = "✖ FDBファイルが見つかりません";
                    lblSelectedDbStatus.ForeColor = Color.FromArgb(239, 35, 60);
                }
            }
        }

        private void btnToggleGrid_Click(object? sender, EventArgs e)
        {
            btnToggleGrid.BackColor = Color.FromArgb(0, 180, 216);
            btnToggleGrid.ForeColor = Color.Black;
            btnToggle2D.BackColor = Color.FromArgb(53, 55, 76);
            btnToggle2D.ForeColor = Color.White;

            dgvPoints.Visible = true;
            picEmbedded2D.Visible = false;
        }

        private void btnToggle2D_Click(object? sender, EventArgs e)
        {
            btnToggle2D.BackColor = Color.FromArgb(0, 180, 216);
            btnToggle2D.ForeColor = Color.Black;
            btnToggleGrid.BackColor = Color.FromArgb(53, 55, 76);
            btnToggleGrid.ForeColor = Color.White;

            picEmbedded2D.Visible = true;
            dgvPoints.Visible = false;

            picEmbedded2D.Invalidate();
        }

        private void SetupDataGridViewStyle()
        {
            dgvPreview.EnableHeadersVisualStyles = false;
            dgvPreview.BackgroundColor = Color.FromArgb(30, 30, 35);
            dgvPreview.ForeColor = Color.White;
            dgvPreview.GridColor = Color.FromArgb(50, 50, 60);
            dgvPreview.RowHeadersVisible = false;
            dgvPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgvPreview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(43, 45, 66);
            dgvPreview.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 180, 216);
            dgvPreview.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);

            dgvPreview.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 35);
            dgvPreview.DefaultCellStyle.ForeColor = Color.White;
            dgvPreview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 180, 216);
            dgvPreview.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void SetupPointsDataGridViewStyle()
        {
            dgvPoints.EnableHeadersVisualStyles = false;
            dgvPoints.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(43, 45, 66);
            dgvPoints.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPoints.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            dgvPoints.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 35);
            dgvPoints.DefaultCellStyle.ForeColor = Color.White;
            dgvPoints.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 180, 216);
            dgvPoints.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPoints.GridColor = Color.FromArgb(60, 60, 70);
            dgvPoints.BorderStyle = BorderStyle.None;
        }

        private void Log(string message)
        {
            FormLog.AppendLog(message);
        }

        private async void btnAnalyze_Click(object? sender, EventArgs e)
        {
            string folder = _activeDbFolder;
            if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
            {
                return;
            }

            SetUiEnabled(false);
            Log("=== Starting Database Integrity Check ===");

            await Task.Run(() =>
            {
                if (_isSite5)
                {
                    string ibutuPath = Path.Combine(folder, "IBUTU.MDB");
                    string ikouPath = Path.Combine(folder, "IKOU.MDB");

                    if (File.Exists(ibutuPath))
                    {
                        Log($"Analyzing Access IBUTU MDB '{Path.GetFileName(ibutuPath)}'...");
                        var info = DbHelper.GetMdbInfo(ibutuPath, "IBUTU");
                        if (info.success) Log($"[SUCCESS] MDB IBUTU analyzed. {info.rowCount:N0} active records.");
                        else Log($"[FAILED] MDB IBUTU: {info.message}");
                    }

                    if (File.Exists(ikouPath))
                    {
                        Log($"Analyzing Access IKOU MDB '{Path.GetFileName(ikouPath)}'...");
                        var info = DbHelper.GetMdbInfo(ikouPath, "IKOU");
                        if (info.success) Log($"[SUCCESS] MDB IKOU analyzed. {info.rowCount:N0} active records.");
                        else Log($"[FAILED] MDB IKOU: {info.message}");

                        Log("Extracting unique group names and point coordinates for preview...");
                        _uniqueGroupNames = DbHelper.ExtractUniqueGroupNames(ikouPath, "");
                        _pointData = DbHelper.ExtractGroupPointData(ikouPath, "");
                    }
                }
                else
                {
                    string fdbPath = Path.Combine(folder, "GENBA_DATA.FDB");

                    if (File.Exists(fdbPath))
                    {
                        Log($"Analyzing Firebird IBUTU_HAND_V '{Path.GetFileName(fdbPath)}'...");
                        var info = DbHelper.GetFdbInfo(fdbPath, "IBUTU_HAND_V");
                        if (info.success) Log($"[SUCCESS] FDB IBUTU analyzed. {info.rowCount:N0} active records.");
                        else Log($"[FAILED] FDB IBUTU: {info.message}");

                        Log($"Analyzing Firebird IKOU_HAND_V '{Path.GetFileName(fdbPath)}'...");
                        var infoIkou = DbHelper.GetFdbInfo(fdbPath, "IKOU_HAND_V");
                        if (infoIkou.success) Log($"[SUCCESS] FDB IKOU analyzed. {infoIkou.rowCount:N0} active records.");
                        else Log($"[FAILED] FDB IKOU: {infoIkou.message}");

                        Log("Extracting unique group names and point coordinates for preview...");
                        _uniqueGroupNames = DbHelper.ExtractUniqueGroupNames("", fdbPath);
                        _pointData = DbHelper.ExtractGroupPointData("", fdbPath);
                    }
                }

                Log($"Extracted {_uniqueGroupNames.Count:N0} unique group names.");
                UpdatePreview();
            });

            Log("=== Database Integrity Check Complete ===");
            SetUiEnabled(true);
        }

        private void cmbRule_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbRule1.SelectedItem is ComboBoxItem item1)
            {
                txtRegexPattern1.Enabled = (item1.Rule == SplitRule.CustomRegex || item1.Rule == SplitRule.DelimiterList);
                if (item1.Rule == SplitRule.CustomRegex && (string.IsNullOrEmpty(txtRegexPattern1.Text) || txtRegexPattern1.Text == "L,U,上,下"))
                {
                    txtRegexPattern1.Text = @"^(?<ikou>.*?)[-_]?(?<ikouline>[UuLl]|[上中下]端|底面|断面|底)$";
                }
                else if (item1.Rule == SplitRule.DelimiterList && (string.IsNullOrEmpty(txtRegexPattern1.Text) || txtRegexPattern1.Text.StartsWith("^")))
                {
                    txtRegexPattern1.Text = "L,U,上,下";
                }
            }
            if (cmbRule2.SelectedItem is ComboBoxItem item2)
            {
                txtRegexPattern2.Enabled = (item2.Rule == SplitRule.CustomRegex || item2.Rule == SplitRule.DelimiterList);
                if (item2.Rule == SplitRule.CustomRegex && (string.IsNullOrEmpty(txtRegexPattern2.Text) || txtRegexPattern2.Text == "L,U,上,下"))
                {
                    txtRegexPattern2.Text = @"^(?<ikou>.*?)[-_]?(?<ikouline>[UuLl]|[上中下]端|底面|断面|底)$";
                }
                else if (item2.Rule == SplitRule.DelimiterList && (string.IsNullOrEmpty(txtRegexPattern2.Text) || txtRegexPattern2.Text.StartsWith("^")))
                {
                    txtRegexPattern2.Text = "L,U,上,下";
                }
            }
            if (cmbRule3.SelectedItem is ComboBoxItem item3)
            {
                txtRegexPattern3.Enabled = (item3.Rule == SplitRule.CustomRegex || item3.Rule == SplitRule.DelimiterList);
                if (item3.Rule == SplitRule.CustomRegex && (string.IsNullOrEmpty(txtRegexPattern3.Text) || txtRegexPattern3.Text == "L,U,上,下"))
                {
                    txtRegexPattern3.Text = @"^(?<ikou>.*?)[-_]?(?<ikouline>[UuLl]|[上中下]端|底面|断面|底)$";
                }
                else if (item3.Rule == SplitRule.DelimiterList && (string.IsNullOrEmpty(txtRegexPattern3.Text) || txtRegexPattern3.Text.StartsWith("^")))
                {
                    txtRegexPattern3.Text = "L,U,上,下";
                }
            }
            UpdatePreview();
        }

        private void txtRegexPattern_TextChanged(object? sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            var dt = new DataTable();
            dt.Columns.Add("Original Group Name", typeof(string));
            dt.Columns.Add("IKOU (Feature)", typeof(string));
            dt.Columns.Add("IKOULINE (Line Suffix)", typeof(string));

            SplitRule rule1 = SplitRule.NoSplit, rule2 = SplitRule.NoSplit, rule3 = SplitRule.NoSplit;
            string pattern1 = "", pattern2 = "", pattern3 = "";

            if (cmbRule1.InvokeRequired)
            {
                cmbRule1.Invoke(new Action(() =>
                {
                    if (cmbRule1.SelectedItem is ComboBoxItem item1) rule1 = item1.Rule;
                    if (cmbRule2.SelectedItem is ComboBoxItem item2) rule2 = item2.Rule;
                    if (cmbRule3.SelectedItem is ComboBoxItem item3) rule3 = item3.Rule;
                    pattern1 = txtRegexPattern1.Text.Trim();
                    pattern2 = txtRegexPattern2.Text.Trim();
                    pattern3 = txtRegexPattern3.Text.Trim();
                }));
            }
            else
            {
                if (cmbRule1.SelectedItem is ComboBoxItem item1) rule1 = item1.Rule;
                if (cmbRule2.SelectedItem is ComboBoxItem item2) rule2 = item2.Rule;
                if (cmbRule3.SelectedItem is ComboBoxItem item3) rule3 = item3.Rule;
                pattern1 = txtRegexPattern1.Text.Trim();
                pattern2 = txtRegexPattern2.Text.Trim();
                pattern3 = txtRegexPattern3.Text.Trim();
            }

            foreach (var groupName in _uniqueGroupNames)
            {
                var split = DbHelper.SplitGroupNameChain(groupName, rule1, pattern1, rule2, pattern2, rule3, pattern3);
                dt.Rows.Add(groupName, split.ikou, split.ikouLine);
            }

            if (dgvPreview.InvokeRequired)
            {
                dgvPreview.Invoke(new Action(() => BindPreviewData(dt)));
            }
            else
            {
                BindPreviewData(dt);
            }
        }

        private void BindPreviewData(DataTable dt)
        {
            dgvPreview.DataSource = dt;
            if (dgvPreview.Columns.Count >= 3)
            {
                dgvPreview.Columns[0].Width = 350;
                dgvPreview.Columns[1].Width = 220;
                dgvPreview.Columns[2].Width = 180;
            }

            string currentSelectedIkou = lstIkou.SelectedItem?.ToString() ?? "";
            var ikouSet = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DataRow row in dt.Rows)
            {
                string ikou = row[1]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(ikou))
                    ikouSet.Add(ikou);
            }

            lstIkou.BeginUpdate();
            lstIkou.Items.Clear();
            foreach (var ikou in ikouSet)
            {
                lstIkou.Items.Add(ikou);
            }
            lstIkou.EndUpdate();

            if (!string.IsNullOrEmpty(currentSelectedIkou) && lstIkou.Items.Contains(currentSelectedIkou))
            {
                lstIkou.SelectedItem = currentSelectedIkou;
            }
            else if (lstIkou.Items.Count > 0)
            {
                lstIkou.SelectedIndex = 0;
            }

            picAllIkouCanvas.Invalidate();
        }

        private void PopulateIkouLineListForIkou(string ikou, string? targetLineToSelect)
        {
            var lineSet = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            if (dgvPreview.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string rowIkou = row[1]?.ToString() ?? "";
                    string rowLine = row[2]?.ToString() ?? "";
                    if (string.Equals(rowIkou, ikou, StringComparison.OrdinalIgnoreCase))
                    {
                        lineSet.Add(string.IsNullOrEmpty(rowLine) ? "(なし)" : rowLine);
                    }
                }
            }

            lstIkouLine.BeginUpdate();
            lstIkouLine.Items.Clear();
            foreach (var line in lineSet)
            {
                lstIkouLine.Items.Add(line);
            }
            lstIkouLine.EndUpdate();

            if (!string.IsNullOrEmpty(targetLineToSelect))
            {
                string normTarget = string.Equals(targetLineToSelect, "(なし)", StringComparison.OrdinalIgnoreCase) ? "(なし)" : targetLineToSelect;
                if (lstIkouLine.Items.Contains(normTarget))
                {
                    lstIkouLine.SelectedItem = normTarget;
                    return;
                }
            }

            if (lstIkouLine.Items.Count > 0)
            {
                lstIkouLine.SelectedIndex = 0;
            }
        }

        private void PopulatePointsGrid()
        {
            string selectedIkou = lstIkou.SelectedItem?.ToString() ?? "";
            string selectedLine = lstIkouLine.SelectedItem?.ToString() ?? "";
            if (string.Equals(selectedLine, "(なし)", StringComparison.OrdinalIgnoreCase))
                selectedLine = "";

            if (string.IsNullOrEmpty(selectedIkou))
            {
                dgvPoints.DataSource = null;
                return;
            }

            var dtPoints = new DataTable();
            dtPoints.Columns.Add("No", typeof(string));
            dtPoints.Columns.Add("X", typeof(string));
            dtPoints.Columns.Add("Y", typeof(string));
            dtPoints.Columns.Add("Z", typeof(string));

            SplitRule rule1 = SplitRule.NoSplit, rule2 = SplitRule.NoSplit, rule3 = SplitRule.NoSplit;
            string pattern1 = "", pattern2 = "", pattern3 = "";
            if (cmbRule1.SelectedItem is ComboBoxItem item1) rule1 = item1.Rule;
            if (cmbRule2.SelectedItem is ComboBoxItem item2) rule2 = item2.Rule;
            if (cmbRule3.SelectedItem is ComboBoxItem item3) rule3 = item3.Rule;
            pattern1 = txtRegexPattern1.Text.Trim();
            pattern2 = txtRegexPattern2.Text.Trim();
            pattern3 = txtRegexPattern3.Text.Trim();

            if (_pointData != null && _pointData.Count > 0)
            {
                foreach (var pt in _pointData)
                {
                    var split = DbHelper.SplitGroupNameChain(pt.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3);
                    if (string.Equals(split.ikou, selectedIkou, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(split.ikouLine, selectedLine, StringComparison.OrdinalIgnoreCase))
                    {
                        dtPoints.Rows.Add(pt.PointNo, pt.X.ToString("F3"), pt.Y.ToString("F3"), pt.Z.ToString("F3"));
                    }
                }
            }
            else
            {
                foreach (var groupName in _uniqueGroupNames)
                {
                    var split = DbHelper.SplitGroupNameChain(groupName, rule1, pattern1, rule2, pattern2, rule3, pattern3);
                    if (string.Equals(split.ikou, selectedIkou, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(split.ikouLine, selectedLine, StringComparison.OrdinalIgnoreCase))
                    {
                        dtPoints.Rows.Add(groupName, "-", "-", "-");
                    }
                }
            }

            dgvPoints.DataSource = dtPoints;
            if (dgvPoints.Columns.Count >= 4)
            {
                dgvPoints.Columns[0].Width = 65;
                dgvPoints.Columns[1].Width = 110;
                dgvPoints.Columns[2].Width = 110;
                dgvPoints.Columns[3].Width = 85;
            }
        }

        private void SyncAllSelection(string ikou, string? line, bool updateDgvPreviewSelection)
        {
            if (_isSyncingSelection) return;
            _isSyncingSelection = true;

            try
            {
                // 1. Sync lstIkou
                if (!string.IsNullOrEmpty(ikou) && lstIkou.Items.Contains(ikou))
                {
                    if (!string.Equals(lstIkou.SelectedItem?.ToString(), ikou, StringComparison.OrdinalIgnoreCase))
                    {
                        lstIkou.SelectedItem = ikou;
                    }
                }

                // 2. Sync lstIkouLine
                PopulateIkouLineListForIkou(ikou, line);

                // 3. Sync dgvPoints
                PopulatePointsGrid();

                // 4. Sync dgvPreview selection if requested (e.g. when selection came from canvas or lstIkou)
                if (updateDgvPreviewSelection && dgvPreview.DataSource is DataTable dt)
                {
                    string activeLine = lstIkouLine.SelectedItem?.ToString() ?? "";
                    if (string.Equals(activeLine, "(なし)", StringComparison.OrdinalIgnoreCase))
                        activeLine = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string rowIkou = dt.Rows[i][1]?.ToString() ?? "";
                        string rowLine = dt.Rows[i][2]?.ToString() ?? "";

                        bool ikouMatch = string.Equals(rowIkou, ikou, StringComparison.OrdinalIgnoreCase);
                        bool lineMatch = string.IsNullOrEmpty(activeLine) ?
                            string.IsNullOrEmpty(rowLine) :
                            string.Equals(rowLine, activeLine, StringComparison.OrdinalIgnoreCase);

                        if (ikouMatch && lineMatch)
                        {
                            dgvPreview.ClearSelection();
                            dgvPreview.Rows[i].Selected = true;
                            dgvPreview.FirstDisplayedScrollingRowIndex = Math.Max(0, i - 2);
                            break;
                        }
                    }
                }

                // 5. Redraw Canvases
                picEmbedded2D.Invalidate();
                picAllIkouCanvas.Invalidate();
            }
            finally
            {
                _isSyncingSelection = false;
            }
        }

        private void FocusIkouInAllCanvas(string ikouName)
        {
            if (string.IsNullOrEmpty(ikouName) || _pointData.Count == 0) return;

            SplitRule rule1 = SplitRule.NoSplit, rule2 = SplitRule.NoSplit, rule3 = SplitRule.NoSplit;
            string pattern1 = txtRegexPattern1.Text.Trim();
            string pattern2 = txtRegexPattern2.Text.Trim();
            string pattern3 = txtRegexPattern3.Text.Trim();
            if (cmbRule1.SelectedItem is ComboBoxItem item1) rule1 = item1.Rule;
            if (cmbRule2.SelectedItem is ComboBoxItem item2) rule2 = item2.Rule;
            if (cmbRule3.SelectedItem is ComboBoxItem item3) rule3 = item3.Rule;

            var ikouPoints = _pointData
                .Where(pt => string.Equals(
                    DbHelper.SplitGroupNameChain(pt.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3).ikou,
                    ikouName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (ikouPoints.Count == 0) return;

            int width = picAllIkouCanvas.Width;
            int height = picAllIkouCanvas.Height;

            // Site Bounding Box
            double siteXMin = _pointData.Min(p => p.Y);
            double siteXMax = _pointData.Max(p => p.Y);
            double siteYMin = _pointData.Min(p => p.X);
            double siteYMax = _pointData.Max(p => p.X);

            double siteRangeX = siteXMax - siteXMin;
            double siteRangeY = siteYMax - siteYMin;
            if (siteRangeX < 0.001) siteRangeX = 1.0;
            if (siteRangeY < 0.001) siteRangeY = 1.0;

            int margin = 35;
            int drawWidth = width - (margin * 2);
            int drawHeight = height - (margin * 2);

            double scaleX = drawWidth / siteRangeX;
            double scaleY = drawHeight / siteRangeY;
            double scale = Math.Min(scaleX, scaleY);

            float offsetX = (float)((width - (siteRangeX * scale)) / 2.0);
            float offsetY = (float)((height - (siteRangeY * scale)) / 2.0);

            // Feature Bounding Box
            double featXMin = ikouPoints.Min(p => p.Y);
            double featXMax = ikouPoints.Max(p => p.Y);
            double featYMin = ikouPoints.Min(p => p.X);
            double featYMax = ikouPoints.Max(p => p.X);

            double featRangeX = featXMax - featXMin;
            double featRangeY = featYMax - featYMin;
            if (featRangeX < 0.001) featRangeX = 0.5;
            if (featRangeY < 0.001) featRangeY = 0.5;

            double featWidthAtZoom1 = featRangeX * scale;
            double featHeightAtZoom1 = featRangeY * scale;

            double targetWidth = width * 0.65;
            double targetHeight = height * 0.65;

            float targetZoom = (float)Math.Min(targetWidth / featWidthAtZoom1, targetHeight / featHeightAtZoom1);
            targetZoom = Math.Clamp(targetZoom, 1.0f, 25.0f);

            double featCenterX = (featXMin + featXMax) / 2.0;
            double featCenterY = (featYMin + featYMax) / 2.0;

            float featBx = (float)(offsetX + (featCenterX - siteXMin) * scale);
            float featBy = (float)(height - offsetY - (featCenterY - siteYMin) * scale);

            float cx = width / 2f;
            float cy = height / 2f;

            float panX = -(featBx - cx) * targetZoom;
            float panY = -(featBy - cy) * targetZoom;

            _zoomFactorAll = targetZoom;
            _panOffsetAll = new PointF(panX, panY);

            picAllIkouCanvas.Invalidate();
        }

        private void lstIkou_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isSyncingSelection) return;
            string selectedIkou = lstIkou.SelectedItem?.ToString() ?? "";
            _zoomFactor2D = 1.0f;
            _panOffset2D = PointF.Empty;
            SyncAllSelection(selectedIkou, null, true);
        }

        private void lstIkouLine_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isSyncingSelection) return;
            string selectedIkou = lstIkou.SelectedItem?.ToString() ?? "";
            string selectedLine = lstIkouLine.SelectedItem?.ToString() ?? "";
            SyncAllSelection(selectedIkou, selectedLine, true);
        }

        private void dgvPreview_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isSyncingSelection) return;

            if (dgvPreview.SelectedRows.Count > 0)
            {
                var row = dgvPreview.SelectedRows[0];
                if (row.Cells.Count >= 3)
                {
                    string ikou = row.Cells[1].Value?.ToString() ?? "";
                    string line = row.Cells[2].Value?.ToString() ?? "";
                    if (string.Equals(line, "(なし)", StringComparison.OrdinalIgnoreCase))
                        line = "";

                    SyncAllSelection(ikou, line, false);
                }
            }
        }

        #region Embedded 2D Plan Canvas Rendering & Mouse Events (Single Selected Feature)

        private void picEmbedded2D_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = picEmbedded2D.Width;
            int height = picEmbedded2D.Height;

            // Draw Background Grid
            using (var gridPen = new Pen(Color.FromArgb(35, 35, 45), 1))
            {
                for (int x = 0; x < width; x += 30)
                    g.DrawLine(gridPen, x, 0, x, height);
                for (int y = 0; y < height; y += 30)
                    g.DrawLine(gridPen, 0, y, width, y);
            }

            string selectedIkou = lstIkou.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(selectedIkou))
            {
                using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Regular))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString("遺構名を選択すると2D図面を描画します", font, brush, 15, 15);
                }
                return;
            }

            SplitRule rule1 = SplitRule.NoSplit, rule2 = SplitRule.NoSplit, rule3 = SplitRule.NoSplit;
            string pattern1 = txtRegexPattern1.Text.Trim();
            string pattern2 = txtRegexPattern2.Text.Trim();
            string pattern3 = txtRegexPattern3.Text.Trim();
            if (cmbRule1.SelectedItem is ComboBoxItem item1) rule1 = item1.Rule;
            if (cmbRule2.SelectedItem is ComboBoxItem item2) rule2 = item2.Rule;
            if (cmbRule3.SelectedItem is ComboBoxItem item3) rule3 = item3.Rule;

            var ikouPoints = _pointData
                .Where(pt => string.Equals(
                    DbHelper.SplitGroupNameChain(pt.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3).ikou,
                    selectedIkou, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (ikouPoints.Count == 0)
            {
                using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Regular))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString($"'{selectedIkou}' の座標点データがありません", font, brush, 15, 15);
                }
                return;
            }

            // Calculate Bounding Box in Japanese Survey Coordinates (X=North, Y=East)
            double posXMin = ikouPoints.Min(p => p.Y);
            double posXMax = ikouPoints.Max(p => p.Y);
            double posYMin = ikouPoints.Min(p => p.X);
            double posYMax = ikouPoints.Max(p => p.X);

            double rangeX = posXMax - posXMin;
            double rangeY = posYMax - posYMin;
            if (rangeX < 0.001) rangeX = 1.0;
            if (rangeY < 0.001) rangeY = 1.0;

            int margin = 25;
            int drawWidth = width - (margin * 2);
            int drawHeight = height - (margin * 2);

            double scaleX = drawWidth / rangeX;
            double scaleY = drawHeight / rangeY;
            double scale = Math.Min(scaleX, scaleY);

            float offsetX = (float)((width - (rangeX * scale)) / 2.0);
            float offsetY = (float)((height - (rangeY * scale)) / 2.0);

            float cx = width / 2f;
            float cy = height / 2f;

            PointF ToCanvasPoint(double surveyX, double surveyY)
            {
                double posX = surveyY; // Survey Y (East) -> Screen Horizontal
                double posY = surveyX; // Survey X (North) -> Screen Vertical

                float bx = (float)(offsetX + (posX - posXMin) * scale);
                float by = (float)(height - offsetY - (posY - posYMin) * scale);
                float px = cx + (bx - cx) * _zoomFactor2D + _panOffset2D.X;
                float py = cy + (by - cy) * _zoomFactor2D + _panOffset2D.Y;
                return new PointF(px, py);
            }

            string selectedLine = lstIkouLine.SelectedItem?.ToString() ?? "";
            if (string.Equals(selectedLine, "(なし)", StringComparison.OrdinalIgnoreCase))
                selectedLine = "";

            var lineGroups = ikouPoints
                .GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3).ikouLine)
                .ToList();

            int colorIdx = 0;
            using (var ptFont = new Font("Yu Gothic UI", 7F, FontStyle.Regular))
            using (var textBrush = new SolidBrush(Color.FromArgb(190, 190, 200)))
            {
                foreach (var grp in lineGroups)
                {
                    Color color = LineColors[colorIdx % LineColors.Length];
                    colorIdx++;

                    bool isTargetLine = !string.IsNullOrEmpty(selectedLine) &&
                        string.Equals(grp.Key, selectedLine, StringComparison.OrdinalIgnoreCase);

                    var pointsList = grp.ToList();
                    var screenPts = pointsList.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();

                    if (screenPts.Length > 1)
                    {
                        float penWidth = isTargetLine ? 3.5f : 2.0f;
                        using (var linePen = new Pen(color, penWidth))
                        {
                            if (isTargetLine)
                            {
                                using (var glowPen = new Pen(Color.FromArgb(120, color.R, color.G, color.B), 7f))
                                {
                                    g.DrawLines(glowPen, screenPts);
                                }
                            }
                            g.DrawLines(linePen, screenPts);
                        }
                    }

                    float dotSize = isTargetLine ? 7f : 5f;
                    float dotRadius = dotSize / 2f;
                    using (var ptBrush = new SolidBrush(color))
                    {
                        for (int i = 0; i < screenPts.Length; i++)
                        {
                            var pt = screenPts[i];
                            g.FillEllipse(ptBrush, pt.X - dotRadius, pt.Y - dotRadius, dotSize, dotSize);
                        }
                    }
                }
            }

            // Title Box
            using (var titleFont = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
            using (var titleBrush = new SolidBrush(Color.Yellow))
            using (var bgBrush = new SolidBrush(Color.FromArgb(180, 15, 15, 20)))
            {
                string lineText = !string.IsNullOrEmpty(selectedLine) ? $" [{selectedLine}]" : "";
                string titleText = $"★ {selectedIkou}{lineText} (全{ikouPoints.Count:N0}点)";
                g.FillRectangle(bgBrush, 8, 8, 160, 20);
                g.DrawString(titleText, titleFont, titleBrush, 12, 10);
            }

            // Legend Overlay
            if (lineGroups.Count > 0)
            {
                using (var legendFont = new Font("Yu Gothic UI", 7.5F, FontStyle.Regular))
                using (var bgBrush = new SolidBrush(Color.FromArgb(190, 15, 15, 20)))
                using (var borderPen = new Pen(Color.FromArgb(60, 60, 70), 1))
                {
                    int itemH = 15;
                    int legH = 8 + (lineGroups.Count * itemH);
                    int legW = 110;
                    Rectangle legendRect = new Rectangle(width - legW - 8, 8, legW, legH);
                    g.FillRectangle(bgBrush, legendRect);
                    g.DrawRectangle(borderPen, legendRect);

                    int legY = 12;
                    int cIdx = 0;
                    foreach (var grp in lineGroups)
                    {
                        string lineName = string.IsNullOrEmpty(grp.Key) ? "(なし)" : grp.Key;
                        Color color = LineColors[cIdx % LineColors.Length];
                        cIdx++;

                        using (var colorBrush = new SolidBrush(color))
                        using (var itemTextBrush = new SolidBrush(Color.FromArgb(220, 220, 225)))
                        {
                            g.FillRectangle(colorBrush, width - legW + 2, legY + 2, 8, 8);
                            string itemText = $"{lineName} ({grp.Count()}点)";
                            g.DrawString(itemText, legendFont, itemTextBrush, width - legW + 13, legY);
                        }
                        legY += itemH;
                    }
                }
            }

            if (_zoomFactor2D != 1.0f || _panOffset2D != PointF.Empty)
            {
                using (var hintFont = new Font("Yu Gothic UI", 7.5F))
                using (var hintBrush = new SolidBrush(Color.FromArgb(160, 0, 220, 255)))
                {
                    g.DrawString($"Zoom: {_zoomFactor2D:F2}x (Wクリックでリセット)", hintFont, hintBrush, 8, height - 18);
                }
            }
        }

        private void picEmbedded2D_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                _isMouseDown2D = true;
                _isPanning2D = false;
                _mouseDownPos2D = e.Location;
                _lastMousePos2D = e.Location;
            }
        }

        private void picEmbedded2D_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isMouseDown2D)
            {
                int dx = e.X - _mouseDownPos2D.X;
                int dy = e.Y - _mouseDownPos2D.Y;
                if (!_isPanning2D && (Math.Abs(dx) > 3 || Math.Abs(dy) > 3))
                {
                    _isPanning2D = true;
                    picEmbedded2D.Cursor = Cursors.SizeAll;
                }

                if (_isPanning2D)
                {
                    int mdx = e.X - _lastMousePos2D.X;
                    int mdy = e.Y - _lastMousePos2D.Y;
                    _panOffset2D = new PointF(_panOffset2D.X + mdx, _panOffset2D.Y + mdy);
                    _lastMousePos2D = e.Location;
                    picEmbedded2D.Invalidate();
                }
            }
        }

        private void picEmbedded2D_MouseUp(object? sender, MouseEventArgs e)
        {
            _isMouseDown2D = false;
            if (_isPanning2D)
            {
                _isPanning2D = false;
                picEmbedded2D.Cursor = Cursors.Default;
            }
        }

        private void picEmbedded2D_MouseWheel(object? sender, MouseEventArgs e)
        {
            float oldZoom = _zoomFactor2D;
            float scaleFactor = e.Delta > 0 ? 1.15f : (1.0f / 1.15f);
            float newZoom = Math.Clamp(oldZoom * scaleFactor, 0.1f, 30.0f);

            if (Math.Abs(newZoom - oldZoom) > 0.0001f)
            {
                float ratio = newZoom / oldZoom;
                float cx = picEmbedded2D.Width / 2f;
                float cy = picEmbedded2D.Height / 2f;

                _panOffset2D.X = e.X - cx - (e.X - cx - _panOffset2D.X) * ratio;
                _panOffset2D.Y = e.Y - cy - (e.Y - cy - _panOffset2D.Y) * ratio;
                _zoomFactor2D = newZoom;

                picEmbedded2D.Invalidate();
            }
        }

        private void picEmbedded2D_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            _zoomFactor2D = 1.0f;
            _panOffset2D = PointF.Empty;
            picEmbedded2D.Invalidate();
        }

        #endregion

        #region All Features Canvas Rendering & Mouse Events (Entire Site Map)

        private void picAllIkouCanvas_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = picAllIkouCanvas.Width;
            int height = picAllIkouCanvas.Height;

            _ikouLabelRectsAll.Clear();
            _ikouScreenPointsAll.Clear();

            // Draw Background Grid
            using (var gridPen = new Pen(Color.FromArgb(35, 35, 45), 1))
            {
                for (int x = 0; x < width; x += 40)
                    g.DrawLine(gridPen, x, 0, x, height);
                for (int y = 0; y < height; y += 40)
                    g.DrawLine(gridPen, 0, y, width, y);
            }

            if (_pointData.Count == 0)
            {
                using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Regular))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString("描画する全遺構データがありません", font, brush, 15, 15);
                }
                return;
            }

            SplitRule rule1 = SplitRule.NoSplit, rule2 = SplitRule.NoSplit, rule3 = SplitRule.NoSplit;
            string pattern1 = txtRegexPattern1.Text.Trim();
            string pattern2 = txtRegexPattern2.Text.Trim();
            string pattern3 = txtRegexPattern3.Text.Trim();
            if (cmbRule1.SelectedItem is ComboBoxItem item1) rule1 = item1.Rule;
            if (cmbRule2.SelectedItem is ComboBoxItem item2) rule2 = item2.Rule;
            if (cmbRule3.SelectedItem is ComboBoxItem item3) rule3 = item3.Rule;

            // Calculate Bounding Box across ALL points in Japanese Survey Coordinates (X=North, Y=East)
            double posXMin = _pointData.Min(p => p.Y);
            double posXMax = _pointData.Max(p => p.Y);
            double posYMin = _pointData.Min(p => p.X);
            double posYMax = _pointData.Max(p => p.X);

            double rangeX = posXMax - posXMin;
            double rangeY = posYMax - posYMin;
            if (rangeX < 0.001) rangeX = 1.0;
            if (rangeY < 0.001) rangeY = 1.0;

            int margin = 35;
            int drawWidth = width - (margin * 2);
            int drawHeight = height - (margin * 2);

            double scaleX = drawWidth / rangeX;
            double scaleY = drawHeight / rangeY;
            double scale = Math.Min(scaleX, scaleY);

            float offsetX = (float)((width - (rangeX * scale)) / 2.0);
            float offsetY = (float)((height - (rangeY * scale)) / 2.0);

            float cx = width / 2f;
            float cy = height / 2f;

            PointF ToCanvasPoint(double surveyX, double surveyY)
            {
                double posX = surveyY; // Survey Y (East) -> Screen Horizontal
                double posY = surveyX; // Survey X (North) -> Screen Vertical

                float bx = (float)(offsetX + (posX - posXMin) * scale);
                float by = (float)(height - offsetY - (posY - posYMin) * scale);
                float px = cx + (bx - cx) * _zoomFactorAll + _panOffsetAll.X;
                float py = cy + (by - cy) * _zoomFactorAll + _panOffsetAll.Y;
                return new PointF(px, py);
            }

            string selectedIkou = lstIkou.SelectedItem?.ToString() ?? "";
            string selectedLine = lstIkouLine.SelectedItem?.ToString() ?? "";
            if (string.Equals(selectedLine, "(なし)", StringComparison.OrdinalIgnoreCase))
                selectedLine = "";

            // Group Points by IKOU
            var ikouGroups = _pointData
                .GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3).ikou)
                .ToList();

            using (var ikouFontNorm = new Font("Yu Gothic UI", 8F, FontStyle.Regular))
            using (var ikouFontSel = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
            {
                // First Pass: Non-selected IKOUs in subtle light gray
                foreach (var ikouGroup in ikouGroups)
                {
                    string ikouName = string.IsNullOrEmpty(ikouGroup.Key) ? "(未分類)" : ikouGroup.Key;
                    bool isSelected = string.Equals(ikouName, selectedIkou, StringComparison.OrdinalIgnoreCase);
                    if (isSelected) continue;

                    Color lineClr = Color.FromArgb(80, 90, 105);
                    Color dotClr = Color.FromArgb(95, 105, 120);

                    var allScreenPts = new List<PointF>();
                    var lineGroups = ikouGroup.GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3).ikouLine);

                    foreach (var lineGrp in lineGroups)
                    {
                        var pointsList = lineGrp.ToList();
                        var screenPts = pointsList.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                        allScreenPts.AddRange(screenPts);

                        if (screenPts.Length > 1)
                        {
                            using (var linePen = new Pen(lineClr, 1.2f))
                            {
                                g.DrawLines(linePen, screenPts);
                            }
                        }

                        using (var ptBrush = new SolidBrush(dotClr))
                        {
                            foreach (var pt in screenPts)
                            {
                                g.FillEllipse(ptBrush, pt.X - 2f, pt.Y - 2f, 4f, 4f);
                            }
                        }
                    }

                    _ikouScreenPointsAll[ikouName] = allScreenPts;

                    double avgX = ikouGroup.Average(p => p.X);
                    double avgY = ikouGroup.Average(p => p.Y);
                    PointF centerPt = ToCanvasPoint(avgX, avgY);

                    using (var labelBrush = new SolidBrush(Color.FromArgb(140, 150, 165)))
                    using (var bgBrush = new SolidBrush(Color.FromArgb(140, 20, 20, 25)))
                    {
                        var size = g.MeasureString(ikouName, ikouFontNorm);
                        RectangleF rect = new RectangleF(centerPt.X - 2, centerPt.Y - 14, size.Width + 4, size.Height);
                        g.FillRectangle(bgBrush, rect);
                        g.DrawString(ikouName, ikouFontNorm, labelBrush, centerPt.X, centerPt.Y - 14);

                        _ikouLabelRectsAll[ikouName] = rect;
                    }
                }

                // Second Pass: Selected IKOU on top with line-specific highlighting!
                foreach (var ikouGroup in ikouGroups)
                {
                    string ikouName = string.IsNullOrEmpty(ikouGroup.Key) ? "(未分類)" : ikouGroup.Key;
                    bool isSelected = string.Equals(ikouName, selectedIkou, StringComparison.OrdinalIgnoreCase);
                    if (!isSelected) continue;

                    var allScreenPts = new List<PointF>();
                    var lineGroups = ikouGroup.GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, rule1, pattern1, rule2, pattern2, rule3, pattern3).ikouLine).ToList();

                    int lineColorIdx = 0;
                    foreach (var lineGrp in lineGroups)
                    {
                        string lineName = string.IsNullOrEmpty(lineGrp.Key) ? "" : lineGrp.Key;
                        Color lineClr = LineColors[lineColorIdx % LineColors.Length];
                        lineColorIdx++;

                        bool isTargetLine = !string.IsNullOrEmpty(selectedLine) &&
                            string.Equals(lineName, selectedLine, StringComparison.OrdinalIgnoreCase);

                        var pointsList = lineGrp.ToList();
                        var screenPts = pointsList.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                        allScreenPts.AddRange(screenPts);

                        if (screenPts.Length > 1)
                        {
                            if (isTargetLine)
                            {
                                // Target line highlighted with extra glowing thick stroke!
                                using (var glowPen = new Pen(Color.FromArgb(140, lineClr.R, lineClr.G, lineClr.B), 8f))
                                {
                                    g.DrawLines(glowPen, screenPts);
                                }
                                using (var linePen = new Pen(lineClr, 3.5f))
                                {
                                    g.DrawLines(linePen, screenPts);
                                }
                            }
                            else if (!string.IsNullOrEmpty(selectedLine))
                            {
                                // Other non-selected lines of the active feature
                                using (var linePen = new Pen(Color.FromArgb(180, lineClr.R, lineClr.G, lineClr.B), 1.8f))
                                {
                                    g.DrawLines(linePen, screenPts);
                                }
                            }
                            else
                            {
                                // Default selection when no specific line is targeted
                                using (var glowPen = new Pen(Color.FromArgb(90, lineClr.R, lineClr.G, lineClr.B), 5f))
                                {
                                    g.DrawLines(glowPen, screenPts);
                                }
                                using (var linePen = new Pen(lineClr, 2.5f))
                                {
                                    g.DrawLines(linePen, screenPts);
                                }
                            }
                        }

                        float dotSize = isTargetLine ? 7.5f : (!string.IsNullOrEmpty(selectedLine) ? 4.5f : 6.0f);
                        float dotRadius = dotSize / 2f;
                        using (var ptBrush = new SolidBrush(lineClr))
                        {
                            foreach (var pt in screenPts)
                            {
                                g.FillEllipse(ptBrush, pt.X - dotRadius, pt.Y - dotRadius, dotSize, dotSize);
                            }
                        }
                    }

                    _ikouScreenPointsAll[ikouName] = allScreenPts;

                    double avgX = ikouGroup.Average(p => p.X);
                    double avgY = ikouGroup.Average(p => p.Y);
                    PointF centerPt = ToCanvasPoint(avgX, avgY);

                    using (var labelBrush = new SolidBrush(Color.Yellow))
                    using (var bgBrush = new SolidBrush(Color.FromArgb(220, 10, 10, 15)))
                    {
                        string lineBadge = !string.IsNullOrEmpty(selectedLine) ? $" [{selectedLine}]" : "";
                        string selText = $"★ {ikouName}{lineBadge}";
                        var size = g.MeasureString(selText, ikouFontSel);
                        RectangleF rect = new RectangleF(centerPt.X - 4, centerPt.Y - 16, size.Width + 8, size.Height + 2);
                        g.FillRectangle(bgBrush, rect);
                        using (var borderPen = new Pen(Color.Yellow, 1.5f))
                        {
                            g.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                        g.DrawString(selText, ikouFontSel, labelBrush, centerPt.X, centerPt.Y - 15);

                        _ikouLabelRectsAll[ikouName] = rect;
                    }
                }
            }

            // Hint Box
            if (_zoomFactorAll != 1.0f || _panOffsetAll != PointF.Empty)
            {
                using (var hintFont = new Font("Yu Gothic UI", 7.5F))
                using (var hintBrush = new SolidBrush(Color.FromArgb(160, 0, 255, 200)))
                {
                    g.DrawString($"Zoom: {_zoomFactorAll:F2}x | ドラッグでパン | クリックで遺構選択 | Wクリックでリセット", hintFont, hintBrush, 8, height - 18);
                }
            }
        }

        private void picAllIkouCanvas_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                _isMouseDownAll = true;
                _isPanningAll = false;
                _mouseDownPosAll = e.Location;
                _lastMousePosAll = e.Location;
            }
        }

        private void picAllIkouCanvas_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isMouseDownAll)
            {
                int dx = e.X - _mouseDownPosAll.X;
                int dy = e.Y - _mouseDownPosAll.Y;
                if (!_isPanningAll && (Math.Abs(dx) > 3 || Math.Abs(dy) > 3))
                {
                    _isPanningAll = true;
                    picAllIkouCanvas.Cursor = Cursors.SizeAll;
                }

                if (_isPanningAll)
                {
                    int mdx = e.X - _lastMousePosAll.X;
                    int mdy = e.Y - _lastMousePosAll.Y;
                    _panOffsetAll = new PointF(_panOffsetAll.X + mdx, _panOffsetAll.Y + mdy);
                    _lastMousePosAll = e.Location;
                    picAllIkouCanvas.Invalidate();
                    return;
                }
            }

            // Hit test cursor hover
            bool hover = false;
            foreach (var kvp in _ikouLabelRectsAll)
            {
                RectangleF inflated = kvp.Value;
                inflated.Inflate(5, 5);
                if (inflated.Contains(e.Location))
                {
                    hover = true;
                    break;
                }
            }

            if (!hover)
            {
                foreach (var kvp in _ikouScreenPointsAll)
                {
                    if (kvp.Value.Any(pt => Math.Abs(pt.X - e.X) <= 12 && Math.Abs(pt.Y - e.Y) <= 12))
                    {
                        hover = true;
                        break;
                    }
                }
            }

            picAllIkouCanvas.Cursor = hover ? Cursors.Hand : Cursors.Default;
        }

        private void picAllIkouCanvas_MouseUp(object? sender, MouseEventArgs e)
        {
            bool wasPanning = _isPanningAll;
            _isMouseDownAll = false;
            _isPanningAll = false;
            picAllIkouCanvas.Cursor = Cursors.Default;

            if (wasPanning) return; // Ignore drag release

            // Perform Click Hit Testing
            if (e.Button == MouseButtons.Left)
            {
                string? clickedIkou = null;

                // 1. Check Label Rectangles
                foreach (var kvp in _ikouLabelRectsAll)
                {
                    RectangleF inflated = kvp.Value;
                    inflated.Inflate(6, 6);
                    if (inflated.Contains(e.Location))
                    {
                        clickedIkou = kvp.Key;
                        break;
                    }
                }

                // 2. Check Point Proximity (12px radius)
                if (clickedIkou == null)
                {
                    foreach (var kvp in _ikouScreenPointsAll)
                    {
                        if (kvp.Value.Any(pt => Math.Abs(pt.X - e.X) <= 12 && Math.Abs(pt.Y - e.Y) <= 12))
                        {
                            clickedIkou = kvp.Key;
                            break;
                        }
                    }
                }

                // 3. Check Line Segment Proximity (8px distance)
                if (clickedIkou == null)
                {
                    foreach (var kvp in _ikouScreenPointsAll)
                    {
                        var pts = kvp.Value;
                        for (int i = 0; i < pts.Count - 1; i++)
                        {
                            if (DistanceToLineSegment(e.Location, pts[i], pts[i + 1]) <= 8.0)
                            {
                                clickedIkou = kvp.Key;
                                break;
                            }
                        }
                        if (clickedIkou != null) break;
                    }
                }

                if (!string.IsNullOrEmpty(clickedIkou) && lstIkou.Items.Contains(clickedIkou))
                {
                    SyncAllSelection(clickedIkou, null, true);
                }
            }
        }

        private static double DistanceToLineSegment(Point p, PointF a, PointF b)
        {
            double dx = b.X - a.X;
            double dy = b.Y - a.Y;
            if (dx == 0 && dy == 0)
            {
                return Math.Sqrt((p.X - a.X) * (p.X - a.X) + (p.Y - a.Y) * (p.Y - a.Y));
            }

            double t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Clamp(t, 0.0, 1.0);

            double projX = a.X + t * dx;
            double projY = a.Y + t * dy;

            return Math.Sqrt((p.X - projX) * (p.X - projX) + (p.Y - projY) * (p.Y - projY));
        }

        private void picAllIkouCanvas_MouseWheel(object? sender, MouseEventArgs e)
        {
            float oldZoom = _zoomFactorAll;
            float scaleFactor = e.Delta > 0 ? 1.15f : (1.0f / 1.15f);
            float newZoom = Math.Clamp(oldZoom * scaleFactor, 0.1f, 30.0f);

            if (Math.Abs(newZoom - oldZoom) > 0.0001f)
            {
                float ratio = newZoom / oldZoom;
                float cx = picAllIkouCanvas.Width / 2f;
                float cy = picAllIkouCanvas.Height / 2f;

                _panOffsetAll.X = e.X - cx - (e.X - cx - _panOffsetAll.X) * ratio;
                _panOffsetAll.Y = e.Y - cy - (e.Y - cy - _panOffsetAll.Y) * ratio;
                _zoomFactorAll = newZoom;

                picAllIkouCanvas.Invalidate();
            }
        }

        private void picAllIkouCanvas_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            _zoomFactorAll = 1.0f;
            _panOffsetAll = PointF.Empty;
            picAllIkouCanvas.Invalidate();
        }

        #endregion

        private void btnOpenViewer_Click(object? sender, EventArgs e)
        {
            string selectedIkou = lstIkou.SelectedItem?.ToString() ?? "";
            OpenIkouViewer(selectedIkou);
        }

        private void lstIkou_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            string selectedIkou = lstIkou.SelectedItem?.ToString() ?? "";
            if (!string.IsNullOrEmpty(selectedIkou))
            {
                FocusIkouInAllCanvas(selectedIkou);
            }
        }

        private void dgvPreview_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvPreview.Rows[e.RowIndex].Cells.Count >= 2)
            {
                string ikou = dgvPreview.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                string line = dgvPreview.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
                if (string.Equals(line, "(なし)", StringComparison.OrdinalIgnoreCase))
                    line = "";

                if (!string.IsNullOrEmpty(ikou) && lstIkou.Items.Contains(ikou))
                {
                    SyncAllSelection(ikou, line, false);
                    FocusIkouInAllCanvas(ikou);
                }
            }
        }

        private void OpenIkouViewer(string ikouName)
        {
            SplitRule rule1 = SplitRule.NoSplit, rule2 = SplitRule.NoSplit, rule3 = SplitRule.NoSplit;
            string pattern1 = "", pattern2 = "", pattern3 = "";
            if (cmbRule1.SelectedItem is ComboBoxItem item1) rule1 = item1.Rule;
            if (cmbRule2.SelectedItem is ComboBoxItem item2) rule2 = item2.Rule;
            if (cmbRule3.SelectedItem is ComboBoxItem item3) rule3 = item3.Rule;
            pattern1 = txtRegexPattern1.Text.Trim();
            pattern2 = txtRegexPattern2.Text.Trim();
            pattern3 = txtRegexPattern3.Text.Trim();

            using (var viewer = new FormIkouViewer())
            {
                viewer.InitializeViewer(ikouName, _pointData, _uniqueGroupNames, rule1, pattern1, rule2, pattern2, rule3, pattern3);
                viewer.ShowDialog(this);
            }
        }

        private async void btnExport_Click(object? sender, EventArgs e)
        {
            string folder = _activeDbFolder;
            string outDir = _outFolder;
            if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
            {
                MessageBox.Show("DB格納フォルダが指定されていないか、存在しません。", "フォルダエラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(outDir))
            {
                MessageBox.Show("CSV出力先フォルダが指定されていません。", "フォルダエラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!Directory.Exists(outDir))
                {
                    Directory.CreateDirectory(outDir);
                    Log($"Created output directory: {outDir}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not create output directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetUiEnabled(false);
            UpdateProgress(0);
            Log("=== Starting Site7 SQLite Conversion & Migration Process ===");

            bool isSite5 = _isSite5;
            bool useShiftJis = chkShiftJis.Checked;

            SplitRule rule1 = SplitRule.NoSplit, rule2 = SplitRule.NoSplit, rule3 = SplitRule.NoSplit;
            if (cmbRule1.SelectedItem is ComboBoxItem item1) rule1 = item1.Rule;
            if (cmbRule2.SelectedItem is ComboBoxItem item2) rule2 = item2.Rule;
            if (cmbRule3.SelectedItem is ComboBoxItem item3) rule3 = item3.Rule;

            string pattern1 = txtRegexPattern1.Text.Trim();
            string pattern2 = txtRegexPattern2.Text.Trim();
            string pattern3 = txtRegexPattern3.Text.Trim();

            await Task.Run(() =>
            {
                UpdateProgress(15);
                Log("Exporting directly to Site7 SQLite Database (.db3)...");

                var sqliteRes = SqliteWriter.ExportToSite7Sqlite(
                    folder,
                    outDir,
                    _dbRootFolder,
                    isSite5,
                    _pointData,
                    rule1, pattern1,
                    rule2, pattern2,
                    rule3, pattern3,
                    msg => Log(msg));

                UpdateProgress(60);

                string activeFolderName = Path.GetFileName(folder.TrimEnd('\\', '/'));
                if (string.IsNullOrEmpty(activeFolderName) || activeFolderName == ".")
                    activeFolderName = "Site7_Export";

                string subFolder = Path.Combine(outDir, activeFolderName);
                Directory.CreateDirectory(subFolder);

                if (isSite5)
                {
                    string ibutuPath = Path.Combine(folder, "IBUTU.MDB");
                    string ikouPath = Path.Combine(folder, "IKOU.MDB");

                    if (File.Exists(ibutuPath))
                    {
                        try
                        {
                            Log($"Reading Access MDB IBUTU data...");
                            var dt = DbHelper.ExportMdb(ibutuPath, "IBUTU", rule1, pattern1, rule2, pattern2, rule3, pattern3, count => Log($"Loaded {count:N0} active records from MDB IBUTU."));
                            string fileOut = Path.Combine(subFolder, "IBUTU_MDB.csv");
                            CsvWriter.SaveToCsv(dt, fileOut, useShiftJis);
                            Log($"[SUCCESS] Exported MDB IBUTU CSV to {fileOut}");
                        }
                        catch (Exception ex) { Log($"[ERROR] CSV Export failed: {ex.Message}"); }
                    }

                    if (File.Exists(ikouPath))
                    {
                        try
                        {
                            Log($"Reading Access MDB IKOU data...");
                            var dt = DbHelper.ExportMdb(ikouPath, "IKOU", rule1, pattern1, rule2, pattern2, rule3, pattern3, count => Log($"Loaded {count:N0} active records from MDB IKOU."));
                            string fileOut = Path.Combine(subFolder, "IKOU_MDB.csv");
                            CsvWriter.SaveToCsv(dt, fileOut, useShiftJis);
                            Log($"[SUCCESS] Exported MDB IKOU CSV to {fileOut}");
                        }
                        catch (Exception ex) { Log($"[ERROR] CSV Export failed: {ex.Message}"); }
                    }
                }
                else
                {
                    string fdbPath = Path.Combine(folder, "GENBA_DATA.FDB");

                    if (File.Exists(fdbPath))
                    {
                        try
                        {
                            Log("Reading Firebird FDB IBUTU_HAND_V data...");
                            var dt = DbHelper.ExportFdb(fdbPath, "IBUTU_HAND_V", rule1, pattern1, rule2, pattern2, rule3, pattern3, count => Log($"Loaded {count:N0} active records from FDB IBUTU."));
                            string fileOut = Path.Combine(subFolder, "IBUTU_FDB.csv");
                            CsvWriter.SaveToCsv(dt, fileOut, useShiftJis);
                            Log($"[SUCCESS] Exported FDB IBUTU CSV to {fileOut}");
                        }
                        catch (Exception ex) { Log($"[ERROR] CSV Export failed: {ex.Message}"); }

                        try
                        {
                            Log("Reading Firebird FDB IKOU_HAND_V data...");
                            var dt = DbHelper.ExportFdb(fdbPath, "IKOU_HAND_V", rule1, pattern1, rule2, pattern2, rule3, pattern3, count => Log($"Loaded {count:N0} active joined records from FDB IKOU."));
                            string fileOut = Path.Combine(subFolder, "IKOU_FDB.csv");
                            CsvWriter.SaveToCsv(dt, fileOut, useShiftJis);
                            Log($"[SUCCESS] Exported FDB IKOU CSV to {fileOut}");
                        }
                        catch (Exception ex) { Log($"[ERROR] CSV Export failed: {ex.Message}"); }
                    }
                }

                UpdateProgress(100);
            });

            Log("=== Site7 SQLite Conversion & Export Complete ===");
            SetUiEnabled(true);

            string activeFolderNameFinal = Path.GetFileName(folder.TrimEnd('\\', '/'));
            string finalSubFolder = Path.Combine(outDir, string.IsNullOrEmpty(activeFolderNameFinal) ? "Site7_Export" : activeFolderNameFinal);

            var result = MessageBox.Show(
                $"Data migration to Site7 SQLite DB completed successfully.\nOutput Folder: {finalSubFolder}\n\nDo you want to open the output folder?",
                "Export Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = outDir,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                catch (Exception ex)
                {
                    Log($"Failed to open directory: {ex.Message}");
                }
            }
        }

        private void UpdateProgress(int percentage)
        {
            if (pbProgress.InvokeRequired)
            {
                pbProgress.Invoke(new Action(() => UpdateProgress(percentage)));
                return;
            }
            pbProgress.Value = percentage;
            lblProgressPercent.Text = $"{percentage}%";
        }

        private void SetUiEnabled(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => SetUiEnabled(enabled)));
                return;
            }
            btnSettings.Enabled = enabled;
            btnShowLog.Enabled = enabled;
            lstDbFolders.Enabled = enabled;
            btnAnalyze.Enabled = enabled;
            btnExport.Enabled = enabled;
            chkShiftJis.Enabled = enabled;
            grpSplit.Enabled = enabled;
        }
    }
}
