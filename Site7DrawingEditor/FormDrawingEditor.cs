using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Site7DrawingEditor.Services;

namespace Site7DrawingEditor
{
    public partial class FormDrawingEditor : Form
    {
        private bool _isUpdatingSelection = false;
        private string? _initialDbPath;

        private readonly DrawingDbManager _db = new DrawingDbManager();
        private readonly CanvasViewController _vc = new CanvasViewController();

        public FormDrawingEditor(string? initialDbPath = null)
        {
            _initialDbPath = initialDbPath;
            InitializeComponent();
            SetupStyles();
            WireEvents();
        }

        private void SetupStyles()
        {
            ApplyTheme();
            ConfigureDgvColumns(dgvDrawings, "ZID", "ID", "Name", "図面名称");
            ConfigureDgvColumns(dgvDrawingIkous, "IID", "ID", "Name", "対象遺構名", isNameReadOnly: true);
            ConfigureDgvColumns(dgvDanmen, "DID", "ID", "Name", "断面名称");
        }

        private void ApplyTheme()
        {
            Color formBgColor = Color.FromArgb(245, 246, 248);
            Color headerBgColor = Color.FromArgb(233, 236, 243);
            Color headerFgColor = Color.FromArgb(25, 45, 80);
            Color subBtnBg = Color.FromArgb(225, 232, 242);
            Color subBtnFg = Color.FromArgb(25, 45, 80);
            Color darkTextColor = Color.FromArgb(33, 37, 41);
            Color grpHeaderColor = Color.FromArgb(25, 55, 105);

            this.BackColor = formBgColor;

            // 各ヘッダーバー
            panelTopLeftHeader.BackColor = headerBgColor;
            lblFullMapTitle.ForeColor = headerFgColor;
            panelTopRightHeader.BackColor = headerBgColor;
            lblPaperSheetTitle.ForeColor = headerFgColor;

            // 下部コンテナ
            panelBottomFixedGroup.BackColor = formBgColor;
            panelControls.BackColor = formBgColor;
            panelBottomGrids.BackColor = formBgColor;

            // グループボックス
            GroupBox[] grps = {
                grpDrawings, grpDrawingIkous, grpDanmenList,
                grpDrawingProps, grpIkouProps, grpCompassProps, grpDanmenProps, grpFeatureDetailPreview
            };
            foreach (var grp in grps)
            {
                if (grp != null)
                {
                    grp.BackColor = formBgColor;
                    grp.ForeColor = grpHeaderColor;
                }
            }

            // コントロール配色の自動適用
            void StyleContainer(Control parent)
            {
                foreach (Control c in parent.Controls)
                {
                    if (c is Label lbl)
                    {
                        if (lbl != lblHeaderTitle && lbl != lblSubHeader &&
                            lbl != lblFullMapTitle && lbl != lblPaperSheetTitle &&
                            lbl != lblIkouLayerGrpHeader && lbl != lblEntityNameHeader)
                        {
                            if (lbl == lblPaperInfoBanner)
                            {
                                lbl.ForeColor = Color.FromArgb(0, 102, 204);
                            }
                            else
                            {
                                lbl.ForeColor = darkTextColor;
                            }
                        }
                    }
                    else if (c is CheckBox chk)
                    {
                        if (chk == chkShowIbutu || chk == chkShowIkou || chk == chkShowKikai)
                        {
                            // レイヤ色を保持
                        }
                        else
                        {
                            chk.ForeColor = darkTextColor;
                        }
                    }
                    else if (c is TextBox txt)
                    {
                        if (txt == txtDrawingName || txt == txtDanmenName)
                        {
                            txt.BackColor = Color.FromArgb(255, 255, 220);
                            txt.ForeColor = Color.Black;
                        }
                        else
                        {
                            txt.BackColor = Color.White;
                            txt.ForeColor = darkTextColor;
                        }
                    }
                    else if (c is ComboBox cmb)
                    {
                        if (cmb == cmbFeatureSelect)
                        {
                            cmb.BackColor = Color.FromArgb(255, 255, 220);
                            cmb.ForeColor = Color.Black;
                        }
                        else
                        {
                            cmb.BackColor = Color.White;
                            cmb.ForeColor = darkTextColor;
                        }
                    }
                    else if (c is Button btn)
                    {
                        string t = btn.Text;
                        if (btn == btnExit)
                        {
                            btn.BackColor = Color.FromArgb(70, 75, 95);
                            btn.ForeColor = Color.White;
                            btn.FlatStyle = FlatStyle.Flat;
                        }
                        else if (t.Contains("削除"))
                        {
                            btn.BackColor = Color.FromArgb(195, 55, 55);
                            btn.ForeColor = Color.White;
                            btn.FlatStyle = FlatStyle.Flat;
                        }
                        else if (t.Contains("更新"))
                        {
                            btn.BackColor = Color.FromArgb(38, 145, 75);
                            btn.ForeColor = Color.White;
                            btn.FlatStyle = FlatStyle.Flat;
                        }
                        else if (t.Contains("追加") || t.Contains("保存"))
                        {
                            btn.BackColor = Color.FromArgb(30, 115, 210);
                            btn.ForeColor = Color.White;
                            btn.FlatStyle = FlatStyle.Flat;
                        }
                        else
                        {
                            btn.BackColor = subBtnBg;
                            btn.ForeColor = subBtnFg;
                            btn.FlatStyle = FlatStyle.Flat;
                        }
                    }
                    if (c.HasChildren && !(c is DataGridView))
                    {
                        StyleContainer(c);
                    }
                }
            }

            StyleContainer(this);
        }

        private void ConfigureDgvColumns(DataGridView dgv, string idPropName, string idHeaderText, string namePropName, string nameHeaderText, bool isNameReadOnly = false)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            dgv.EnableHeadersVisualStyles = false;
            dgv.DataError += (s, e) => { e.ThrowException = false; };
            dgv.BackgroundColor = Color.FromArgb(248, 249, 250);
            dgv.ForeColor = Color.FromArgb(33, 37, 41);
            dgv.GridColor = Color.FromArgb(215, 220, 228);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 235, 245);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(25, 45, 80);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(20, 20, 20);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(179, 229, 252);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 30, 80);

            var colId = new DataGridViewTextBoxColumn
            {
                DataPropertyName = idPropName,
                HeaderText = idHeaderText,
                Width = 45,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colName = new DataGridViewTextBoxColumn
            {
                DataPropertyName = namePropName,
                HeaderText = nameHeaderText,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = isNameReadOnly
            };

            dgv.Columns.Add(colId);
            dgv.Columns.Add(colName);
        }

        private void WireEvents()
        {
            this.Load += FormDrawingEditor_Load;
            this.btnExit.Click += (s, e) => this.Close();
            this.btnSaveDb.Click += btnSaveDb_Click;

            this.dgvDrawings.SelectionChanged += dgvDrawings_SelectionChanged;
            this.dgvDrawingIkous.SelectionChanged += dgvDrawingIkous_SelectionChanged;
            this.dgvDrawingIkous.CellDoubleClick += (s, e) => FocusSelectedFeature(force: true);
            this.dgvDanmen.SelectionChanged += dgvDanmen_SelectionChanged;

            this.btnAddDrawing.Click += btnAddDrawing_Click;
            this.btnDeleteDrawing.Click += btnDeleteDrawing_Click;
            this.btnAddDrawingIkou.Click += btnAddDrawingIkou_Click;
            this.btnDeleteDrawingIkou.Click += btnDeleteDrawingIkou_Click;
            this.btnAddDanmen.Click += btnAddDanmen_Click;
            this.btnUpdateDanmenName.Click += btnUpdateDanmenName_Click;
            this.btnDeleteDanmen.Click += btnDeleteDanmen_Click;

            this.btnUpdateDrawingProps.Click += btnUpdateDrawingProps_Click;
            this.btnUpdateIkouProps.Click += btnUpdateIkouProps_Click;

            this.btnPickCropBounds.Click += (s, e) => Start3PointPick();
            this.btnSetPaperPosition.Click += (s, e) => StartPaperPositionPick();

            this.chkShowDirection.CheckedChanged += chkShowDirection_CheckedChanged;
            this.btnSetDirectionPosition.Click += (s, e) => StartDirectionPositionPick();
            this.btnSetDanmenPosition.Click += (s, e) => StartDanmenPositionPick();

            this.btnResetCropZoom.Click += (s, e) => { _vc.ResetCropZoom(); picCropCanvas.Invalidate(); };
            this.btnResetPaperZoom.Click += (s, e) => { _vc.ResetPaperZoom(); picPaperCanvas.Invalidate(); };
            this.chkAutoZoomIkou.CheckedChanged += (s, e) => { if (chkAutoZoomIkou.Checked) FocusSelectedFeature(); };
            this.chkAutoZoomPaperIkou.CheckedChanged += (s, e) => { if (chkAutoZoomPaperIkou.Checked) FocusSelectedFeature(); };

            _chkLayers = new CheckBox[]
            {
                chkLayer01, chkLayer02, chkLayer03, chkLayer04,
                chkLayer05, chkLayer06, chkLayer07, chkLayer08,
                chkLayer09, chkLayer10, chkLayer11, chkLayer12,
                chkLayer13, chkLayer14, chkLayer15, chkLayer16
            };

            foreach (var chk in _chkLayers)
            {
                chk.CheckedChanged += (s, e) => picCropCanvas.Invalidate();
            }

            btnLayerAllOn.Click += (s, e) =>
            {
                foreach (var chk in _chkLayers) chk.Checked = true;
                picCropCanvas.Invalidate();
            };

            btnLayerAllOff.Click += (s, e) =>
            {
                foreach (var chk in _chkLayers) chk.Checked = false;
                picCropCanvas.Invalidate();
            };

            btnBgSettings.Click += (s, e) =>
            {
                _isDarkCanvasBackground = !_isDarkCanvasBackground;
                picCropCanvas.Invalidate();
            };

            chkShowIbutu.CheckedChanged += (s, e) => picCropCanvas.Invalidate();
            chkShowIbutuName.CheckedChanged += (s, e) => picCropCanvas.Invalidate();
            chkShowIkou.CheckedChanged += (s, e) => picCropCanvas.Invalidate();
            chkShowIkouName.CheckedChanged += (s, e) => picCropCanvas.Invalidate();
            chkShowKikai.CheckedChanged += (s, e) => picCropCanvas.Invalidate();
            chkShowKikaiName.CheckedChanged += (s, e) => picCropCanvas.Invalidate();

            this.chkShowCurvePaper.CheckedChanged += (s, e) => picPaperCanvas.Invalidate();
            this.chkShowDirectionPaper.CheckedChanged += (s, e) => picPaperCanvas.Invalidate();
            this.chkShowDanmenPaper.CheckedChanged += (s, e) => picPaperCanvas.Invalidate();

            // リアルタイムに入力テキストが変更された際のボタン有効化更新ハンドラー
            this.txtDrawingName.TextChanged += (s, e) => UpdateControlEnableStates();
            this.cmbFeatureSelect.TextChanged += (s, e) => UpdateControlEnableStates();
            this.cmbFeatureSelect.SelectedIndexChanged += (s, e) =>
            {
                UpdateControlEnableStates();
                FocusSelectedFeature();
            };
            this.txtDanmenName.TextChanged += (s, e) => UpdateControlEnableStates();

            this.cmbOrientation.SelectedIndexChanged += (s, e) =>
            {
                if (_isUpdatingSelection) return;
                if (GetSelectedDataBoundItem<DrawingModel>(dgvDrawings) is DrawingModel curDrawing)
                {
                    curDrawing.Type = (cmbOrientation.SelectedIndex == 1) ? 1 : 0;
                    lblPaperInfoBanner.Text = $"{curDrawing.PaperInfo.Name} ({curDrawing.PaperInfo.WidthMm}×{curDrawing.PaperInfo.HeightMm}mm) | 1/{curDrawing.Scale}";
                    RefreshAllCanvases();
                }
            };

            this.chkColorByIkouFull.CheckedChanged += (s, e) =>
            {
                if (_isUpdatingSelection) return;
                _isUpdatingSelection = true;
                this.chkColorByIkouPaper.Checked = this.chkColorByIkouFull.Checked;
                _isUpdatingSelection = false;
                RefreshAllCanvases();
            };

            this.chkColorByIkouPaper.CheckedChanged += (s, e) =>
            {
                if (_isUpdatingSelection) return;
                _isUpdatingSelection = true;
                this.chkColorByIkouFull.Checked = this.chkColorByIkouPaper.Checked;
                _isUpdatingSelection = false;
                RefreshAllCanvases();
            };

            // Canvas Paint & Interaction Events
            this.picCropCanvas.Paint += picCropCanvas_Paint;
            this.picCropCanvas.MouseDown += picCropCanvas_MouseDown;
            this.picCropCanvas.MouseMove += picCropCanvas_MouseMove;
            this.picCropCanvas.MouseUp += picCropCanvas_MouseUp;
            this.picCropCanvas.MouseWheel += picCropCanvas_MouseWheel;
            this.picCropCanvas.DoubleClick += (s, e) => { _vc.ResetCropZoom(); picCropCanvas.Invalidate(); };

            this.picPaperCanvas.Paint += picPaperCanvas_Paint;
            this.picPaperCanvas.MouseDown += picPaperCanvas_MouseDown;
            this.picPaperCanvas.MouseMove += picPaperCanvas_MouseMove;
            this.picPaperCanvas.MouseUp += picPaperCanvas_MouseUp;
            this.picPaperCanvas.MouseWheel += picPaperCanvas_MouseWheel;
            this.picPaperCanvas.DoubleClick += (s, e) => { _vc.ResetPaperZoom(); picPaperCanvas.Invalidate(); };

            this.picFeatureDetailCanvas.Paint += picFeatureDetailCanvas_Paint;
            this.picFeatureDetailCanvas.MouseDown += picFeatureDetailCanvas_MouseDown;
            this.picFeatureDetailCanvas.MouseMove += picFeatureDetailCanvas_MouseMove;

            // Canvas Resize & Splitter Re-layout Redraw Handlers
            this.picCropCanvas.Resize += (s, e) => picCropCanvas.Invalidate();
            this.picPaperCanvas.Resize += (s, e) => picPaperCanvas.Invalidate();
            this.picFeatureDetailCanvas.Resize += (s, e) => picFeatureDetailCanvas.Invalidate();
            this.splitContainerTopHorizontal.SplitterMoved += (s, e) => { PerformTopLeftLayout(); PerformTopRightLayout(); };

            this.panelTopLeft.Resize += (s, e) => PerformTopLeftLayout();
            this.panelTopRight.Resize += (s, e) => PerformTopRightLayout();

            this.Shown += (s, e) => { PerformTopLeftLayout(); PerformTopRightLayout(); picFeatureDetailCanvas.Invalidate(); };

            this.Resize += (s, e) =>
            {
                if (this.splitContainerTopHorizontal != null && this.picCropCanvas != null)
                {
                    string info = $"Form: {this.ClientSize.Width}x{this.ClientSize.Height} | Split: {this.splitContainerTopHorizontal.Width}x{this.splitContainerTopHorizontal.Height} | Panel1: {this.splitContainerTopHorizontal.Panel1.Width}x{this.splitContainerTopHorizontal.Panel1.Height} | picCrop: {this.picCropCanvas.Width}x{this.picCropCanvas.Height}";
                    System.Diagnostics.Debug.WriteLine($"[RESIZE DIAG] {info}");
                    this.lblStatusMessage.Text = info;
                }
            };
        }

        private void PerformTopLeftLayout()
        {
            if (this.panelTopLeft == null || this.picCropCanvas == null || this.panelHcLeftSidebar == null) return;
            int sidebarW = 130;
            this.panelHcLeftSidebar.Bounds = new System.Drawing.Rectangle(0, 35, sidebarW, Math.Max(10, this.panelTopLeft.Height - 35));
            this.picCropCanvas.Bounds = new System.Drawing.Rectangle(sidebarW, 35, Math.Max(10, this.panelTopLeft.Width - sidebarW), Math.Max(10, this.panelTopLeft.Height - 35));
            this.panelHcLeftSidebar.BringToFront();
            this.picCropCanvas.Invalidate();
        }

        private void PerformTopRightLayout()
        {
            if (this.panelTopRight == null || this.picPaperCanvas == null) return;
            int sidebarW = 100;
            if (this.panelTopRightHeader != null)
            {
                this.panelTopRightHeader.Bounds = new System.Drawing.Rectangle(0, 0, this.panelTopRight.Width, 35);
                this.panelTopRightHeader.BringToFront();
            }
            this.picPaperCanvas.Bounds = new System.Drawing.Rectangle(sidebarW, 35, Math.Max(10, this.panelTopRight.Width - sidebarW), Math.Max(10, this.panelTopRight.Height - 35));
            this.chkShowCurvePaper.BringToFront();
            this.chkShowDirectionPaper.BringToFront();
            this.chkShowDanmenPaper.BringToFront();
            this.chkColorByIkouPaper.BringToFront();
            this.picPaperCanvas.Invalidate();
        }

        private void FormDrawingEditor_Load(object? sender, EventArgs e)
        {
            InitComboBoxes();
            PerformTopLeftLayout();
            PerformTopRightLayout();

            string? targetDb = !string.IsNullOrEmpty(_initialDbPath) ? _initialDbPath : Def.GetIniStr("Site7DbEditor", "LastOpenedDb");
            string? resolvedDb = ResolveDbPath(targetDb);

            if (!string.IsNullOrEmpty(resolvedDb) && File.Exists(resolvedDb))
            {
                LoadDatabase(resolvedDb);
            }
            else
            {
                lblDbStatus.Text = "現場未選択 (現場管理ランチャで現場を選択して起動してください)";
                lblDbStatus.ForeColor = Color.FromArgb(255, 193, 7);
            }
        }

        private string? ResolveDbPath(string? inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath)) return null;

            if (File.Exists(inputPath)) return inputPath;

            if (Directory.Exists(inputPath))
            {
                var files = Directory.GetFiles(inputPath, "*.db3")
                    .Concat(Directory.GetFiles(inputPath, "*.db"))
                    .OrderBy(f => f)
                    .ToList();

                if (files.Count > 0) return files[0];
            }

            return null;
        }

        private void InitComboBoxes()
        {
            cmbPaperSize.Items.Clear();
            foreach (var p in PaperSizeInfo.PaperSizes)
            {
                cmbPaperSize.Items.Add($"{p.Name} ({p.WidthMm} × {p.HeightMm} mm)");
            }
            if (cmbPaperSize.Items.Count > 3) cmbPaperSize.SelectedIndex = 3; // Default A3

            cmbOrientation.Items.Clear();
            cmbOrientation.Items.AddRange(new object[] { "横", "縦" });
            cmbOrientation.SelectedIndex = 0; // Default 横

            cmbScale.Items.Clear();
            int[] defaultScales = new[] { 10, 20, 30, 50, 100, 200 };
            foreach (var s in defaultScales)
            {
                cmbScale.Items.Add($"{s}");
            }
            cmbScale.SelectedIndex = 1; // Default 1/20
        }

        private void LoadDatabase(string dbPath)
        {
            try
            {
                _db.LoadDatabase(dbPath);
                Def.SetIniStr("Site7DbEditor", "LastOpenedDb", dbPath);

                cmbFeatureSelect.Items.Clear();
                foreach (var ik in _db.MasterIkouList)
                {
                    string displayName = string.IsNullOrWhiteSpace(ik.Name) ? $"遺構{ik.Id}" : ik.Name;
                    cmbFeatureSelect.Items.Add(displayName);
                }
                if (cmbFeatureSelect.Items.Count > 0) cmbFeatureSelect.SelectedIndex = 0;

                BindAllData();

                string dirName = Path.GetFileName(Path.GetDirectoryName(dbPath)) ?? "";
                string fileName = Path.GetFileName(dbPath);
                string siteName = !string.IsNullOrEmpty(dirName) ? dirName : fileName;

                this.Text = $"遺跡調査システム(内業) - 遺構図面作成エディタ [{siteName}]";
                lblDbStatus.Text = $"✔ [{siteName}]  {_db.DrawingsList.Count}図面 | {_db.DrawingIkousList.Count}配置遺構 | {_db.MasterIkouList.Count}遺構 | {_db.MasterIbutuList.Count}遺物 | {_db.MasterKikaiList.Count}基準点";
                lblDbStatus.ForeColor = Color.FromArgb(56, 176, 0);

                _vc.ResetCropZoom();
                _vc.ResetPaperZoom();

                RefreshAllCanvases();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB読み込みエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblDbStatus.Text = "✖ 読み込み失敗";
                lblDbStatus.ForeColor = Color.FromArgb(239, 35, 60);
            }
        }

        private void BindAllData()
        {
            dgvDrawings.DataSource = _db.DrawingsList;
            dgvDrawings_SelectionChanged(this, EventArgs.Empty);
            UpdateControlEnableStates();
        }

        private void RefreshAllCanvases()
        {
            picCropCanvas.Invalidate();
            picPaperCanvas.Invalidate();
            picFeatureDetailCanvas.Invalidate();
        }

        private static T? GetSelectedDataBoundItem<T>(DataGridView dgv) where T : class
        {
            try
            {
                if (dgv.SelectedRows.Count > 0 && dgv.SelectedRows[0].Index >= 0)
                {
                    return dgv.SelectedRows[0].DataBoundItem as T;
                }
                if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex >= 0 && dgv.CurrentCell.RowIndex < dgv.Rows.Count)
                {
                    return dgv.Rows[dgv.CurrentCell.RowIndex].DataBoundItem as T;
                }
            }
            catch { }
            return null;
        }

        private void UpdateControlEnableStates()
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            bool isDbLoaded = (_db.MasterIkouList != null && _db.MasterIkouList.Count > 0);

            DrawingModel? curDrawing = GetSelectedDataBoundItem<DrawingModel>(dgvDrawings);
            DrawingIkouModel? curIkou = GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous);
            DanmenRec? curDanmen = GetSelectedDataBoundItem<DanmenRec>(dgvDanmen);

            bool hasDrawing = isDbLoaded && (curDrawing != null);
            bool hasIkou = hasDrawing && (curIkou != null);
            bool hasDanmen = hasIkou && (curDanmen != null);

            // 1. 図面入力の重複・空白判定
            string drawNameInput = txtDrawingName.Text.Trim();
            bool isDrawNameNotEmpty = !string.IsNullOrEmpty(drawNameInput);
            bool isDrawNameNotRegistered = isDrawNameNotEmpty && !_db.DrawingsList.Any(d => d.Name.Equals(drawNameInput, StringComparison.OrdinalIgnoreCase));
            bool isDrawNameNotOtherDuplicate = hasDrawing && isDrawNameNotEmpty && !_db.DrawingsList.Any(d => d != curDrawing && d.Name.Equals(drawNameInput, StringComparison.OrdinalIgnoreCase));

            btnAddDrawing.Enabled = isDbLoaded && isDrawNameNotRegistered;
            btnDeleteDrawing.Enabled = hasDrawing;
            btnUpdateDrawingProps.Enabled = hasDrawing && isDrawNameNotOtherDuplicate;

            // 2. 対象遺構入力の重複・空白判定
            string ikouNameInput = cmbFeatureSelect.Text.Trim();
            bool isIkouNameNotEmpty = !string.IsNullOrEmpty(ikouNameInput);

            var currentDrawingIkous = hasDrawing
                ? _db.DrawingIkousList.Where(di => di.ZID == curDrawing!.ZID).ToList()
                : new List<DrawingIkouModel>();

            bool isIkouNameNotRegistered = hasDrawing && isIkouNameNotEmpty && !currentDrawingIkous.Any(di => di.Name.Equals(ikouNameInput, StringComparison.OrdinalIgnoreCase));
            bool isIkouNameNotOtherDuplicate = hasIkou && isIkouNameNotEmpty && !currentDrawingIkous.Any(di => di != curIkou && di.Name.Equals(ikouNameInput, StringComparison.OrdinalIgnoreCase));

            btnAddDrawingIkou.Enabled = hasDrawing && isIkouNameNotRegistered;
            btnDeleteDrawingIkou.Enabled = hasIkou;
            btnUpdateIkouProps.Enabled = hasIkou && isIkouNameNotOtherDuplicate;

            // 3. 断面入力の重複・空白判定
            string danmenNameInput = txtDanmenName.Text.Trim();
            bool isDanmenNameNotEmpty = !string.IsNullOrEmpty(danmenNameInput);

            var currentDanmenList = hasIkou ? curIkou!.DmList : new List<DanmenRec>();
            bool isDanmenNameNotRegistered = hasIkou && isDanmenNameNotEmpty && !currentDanmenList.Any(d => d.Name.Equals(danmenNameInput, StringComparison.OrdinalIgnoreCase));

            btnAddDanmen.Enabled = hasIkou && isDanmenNameNotRegistered;
            btnDeleteDanmen.Enabled = hasDanmen;

            // 4. 指示ボタン
            btnPickCropBounds.Enabled = hasIkou;
            btnSetPaperPosition.Enabled = hasIkou;
            btnSetDirectionPosition.Enabled = hasIkou;
            btnSetDanmenPosition.Enabled = hasDanmen;
        }

        private void dgvDrawings_SelectionChanged(object? sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                if (_isUpdatingSelection) return;
                _isUpdatingSelection = true;
                try
                {
                    DrawingModel? selectedDrawing = GetSelectedDataBoundItem<DrawingModel>(dgvDrawings);
                    if (selectedDrawing != null)
                    {
                        txtDrawingName.Text = selectedDrawing.Name;
                        if (selectedDrawing.PaperSize >= 0 && selectedDrawing.PaperSize < cmbPaperSize.Items.Count)
                            cmbPaperSize.SelectedIndex = selectedDrawing.PaperSize;

                        int scaleIdx = selectedDrawing.Scale switch
                        {
                            10 => 0,
                            20 => 1,
                            30 => 2,
                            50 => 3,
                            100 => 4,
                            200 => 5,
                            _ => 1
                        };
                        cmbScale.SelectedIndex = scaleIdx;
                        cmbOrientation.SelectedIndex = (selectedDrawing.Type == 1) ? 1 : 0;
                        lblPaperInfoBanner.Text = $"{selectedDrawing.PaperInfo.Name} ({selectedDrawing.PaperInfo.WidthMm}×{selectedDrawing.PaperInfo.HeightMm}mm) | 1/{selectedDrawing.Scale}";

                        var subIkous = _db.DrawingIkousList.Where(di => di.ZID == selectedDrawing.ZID).ToList();

                        int seqId = 1;
                        foreach (var ikou in subIkous)
                        {
                            ikou.IID = seqId++;
                        }

                        dgvDrawingIkous.DataSource = new BindingList<DrawingIkouModel>(subIkous);
                    }
                    else
                    {
                        dgvDrawingIkous.DataSource = new BindingList<DrawingIkouModel>();
                    }

                    dgvDrawingIkous_SelectionChanged(this, EventArgs.Empty);
                }
                catch { }
                finally
                {
                    _isUpdatingSelection = false;
                    RefreshAllCanvases();
                    UpdateControlEnableStates();
                }
            }));
        }

        private void FocusSelectedFeature(bool force = false)
        {
            DrawingIkouModel? curIkou = GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous);
            string targetName = cmbFeatureSelect.Text.Trim();
            if (string.IsNullOrWhiteSpace(targetName) && curIkou != null)
            {
                targetName = curIkou.Name;
            }

            DrawingModel? curDrawing = GetSelectedDataBoundItem<DrawingModel>(dgvDrawings);
            if (curIkou != null && !string.IsNullOrWhiteSpace(targetName) && !curIkou.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase))
            {
                if (curDrawing != null)
                {
                    var matched = _db.DrawingIkousList.FirstOrDefault(di => di.ZID == curDrawing.ZID && di.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase));
                    curIkou = matched; // 一致する図面遺構が無ければ null にして Master 実測線から検索
                }
                else
                {
                    curIkou = null;
                }
            }

            // 1. 全体遺構図の自動フォーカス
            if (force || chkAutoZoomIkou.Checked)
            {
                _vc.FocusFeatureByNameOnFullMap(targetName, curIkou, picCropCanvas.Size, _db.MasterIkouList, _db.MasterIkouLList, _db.MasterIbutuList, _db.MasterKikaiList);
                picCropCanvas.Invalidate();
            }

            // 2. 遺構図面 (用紙ビュー) の自動フォーカス
            if ((force || chkAutoZoomPaperIkou.Checked) && curIkou != null && curDrawing != null)
            {
                _vc.FocusPaperFeature(curIkou, picPaperCanvas.Size, curDrawing.PaperInfo, curDrawing.Scale);
                picPaperCanvas.Invalidate();
            }
        }

        private void dgvDrawingIkous_SelectionChanged(object? sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    DrawingIkouModel? selectedIkou = GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous);
                    if (selectedIkou != null)
                    {
                        _db.DanmenList.Clear();
                        foreach (var dm in selectedIkou.DmList) _db.DanmenList.Add(dm);
                        dgvDanmen.DataSource = _db.DanmenList;
                        chkShowDirection.Checked = (selectedIkou.IsShowDirection == 1);

                        if (!_isUpdatingSelection)
                        {
                            cmbFeatureSelect.Text = selectedIkou.Name;
                        }

                        dgvDanmen_SelectionChanged(this, EventArgs.Empty);

                        if (chkAutoZoomIkou.Checked)
                        {
                            _vc.FocusFeatureByNameOnFullMap(selectedIkou.Name, selectedIkou, picCropCanvas.Size, _db.MasterIkouList, _db.MasterIkouLList, _db.MasterIbutuList, _db.MasterKikaiList);
                        }
                    }
                    else
                    {
                        _db.DanmenList.Clear();
                        dgvDanmen.DataSource = _db.DanmenList;
                    }
                    RefreshAllCanvases();
                }
                catch { }
            }));
        }

        private void dgvDanmen_SelectionChanged(object? sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    DanmenRec? selectedDm = GetSelectedDataBoundItem<DanmenRec>(dgvDanmen);
                    if (selectedDm != null)
                    {
                        txtDanmenName.Text = selectedDm.Name;
                    }
                    RefreshAllCanvases();
                    UpdateControlEnableStates();
                }
                catch { }
            }));
        }

        private void chkShowDirection_CheckedChanged(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel curIkou)
            {
                curIkou.IsShowDirection = chkShowDirection.Checked ? 1 : 0;
                picPaperCanvas.Invalidate();
                picFeatureDetailCanvas.Invalidate();
            }
        }

        private void btnUpdateDrawingProps_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingModel>(dgvDrawings) is DrawingModel sel)
            {
                sel.Name = txtDrawingName.Text.Trim();
                sel.PaperSize = cmbPaperSize.SelectedIndex;
                sel.Scale = int.Parse(cmbScale.SelectedItem?.ToString() ?? "20");
                sel.Type = (cmbOrientation.SelectedIndex == 1) ? 1 : 0;
                dgvDrawings.Refresh();
                lblPaperInfoBanner.Text = $"{sel.PaperInfo.Name} ({sel.PaperInfo.WidthMm}×{sel.PaperInfo.HeightMm}mm) | 1/{sel.Scale}";
                RefreshAllCanvases();

                lblStatusMessage.Text = $"✔ 図面 [{sel.Name}] のプロパティを更新しました";
                lblStatusMessage.ForeColor = Color.FromArgb(56, 176, 0);
                UpdateControlEnableStates();
            }
        }

        private void btnUpdateIkouProps_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel curIkou && GetSelectedDataBoundItem<DrawingModel>(dgvDrawings) is DrawingModel curDrawing)
            {
                string targetName = cmbFeatureSelect.Text.Trim();
                if (string.IsNullOrEmpty(targetName)) return;

                var otherIkous = _db.DrawingIkousList.Where(di => di.ZID == curDrawing.ZID && di != curIkou).ToList();
                if (otherIkous.Any(di => di.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show($"遺構名 [{targetName}] はこの図面の他の行に既に登録されています。\n重複しない名称を指定してください。", "重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatusMessage.Text = $"✖ 他の行に同名の遺構 [{targetName}] が存在するため更新できません";
                    lblStatusMessage.ForeColor = Color.FromArgb(239, 35, 60);
                    return;
                }

                bool isNameChanged = !curIkou.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase);
                curIkou.Name = targetName;
                if (isNameChanged)
                {
                    curIkou.P1 = new XYZ(0, 0);
                    curIkou.P2 = new XYZ(0, 0);
                    curIkou.P3 = new XYZ(0, 0);
                }

                var (msg, isSuccess) = _db.AutoExtractFeatureLines(curIkou, targetName);
                lblStatusMessage.Text = msg;
                lblStatusMessage.ForeColor = isSuccess ? Color.FromArgb(56, 176, 0) : Color.FromArgb(0, 225, 255);
                dgvDrawingIkous.Refresh();
                RefreshAllCanvases();
            }
        }

        private void Start3PointPick()
        {
            _vc.CropStep = 1;
            lblStatusMessage.Text = "指示手順: 全体図(測量座標系)上で 1.左下(p1) をクリックしてください";
            lblStatusMessage.ForeColor = Color.FromArgb(255, 191, 0);
        }

        private void StartPaperPositionPick()
        {
            _vc.IsPickingPaperPosition = true;
            lblStatusMessage.Text = "指示手順: 用紙ビュー上で 遺構枠中心の配置位置 (PX, PY: 用紙中心0,0基準) をクリックしてください";
            lblStatusMessage.ForeColor = Color.FromArgb(255, 191, 0);
        }

        private void StartDirectionPositionPick()
        {
            _vc.IsPickingDirectionPosition = true;
            lblStatusMessage.Text = "指示手順: 選択遺構詳細プレビュー(右下)上で 方位マークの表示位置 (遺構中心0,0基準) をクリックしてください";
            lblStatusMessage.ForeColor = Color.FromArgb(255, 191, 0);
        }

        private void StartDanmenPositionPick()
        {
            if (GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel curIkou)
            {
                DanmenRec? selectedDm = GetSelectedDataBoundItem<DanmenRec>(dgvDanmen);
                if (selectedDm == null && !string.IsNullOrWhiteSpace(txtDanmenName.Text))
                {
                    string inputName = txtDanmenName.Text.Trim();
                    selectedDm = curIkou.DmList.FirstOrDefault(d => d.Name == inputName);
                    if (selectedDm == null)
                    {
                        int nextDid = curIkou.DmList.Count > 0 ? curIkou.DmList.Max(d => d.DID) + 1 : 1;
                        selectedDm = new DanmenRec(nextDid, inputName, new XYZ(), new XYZ(), new XYZ());
                    }
                }

                using (var dlg = new FormIkou3D(curIkou, selectedDm))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK && dlg.ResultDanmenRec != null)
                    {
                        int existingIdx = curIkou.DmList.FindIndex(d => d.DID == dlg.ResultDanmenRec.DID || d.Name == dlg.ResultDanmenRec.Name);
                        if (existingIdx >= 0)
                        {
                            curIkou.DmList[existingIdx] = dlg.ResultDanmenRec;
                        }
                        else
                        {
                            curIkou.DmList.Add(dlg.ResultDanmenRec);
                        }

                        lblStatusMessage.Text = $"✔ 遺構3Dダイアログより 断面 {dlg.ResultDanmenRec.Name} を更新・保存しました";
                        lblStatusMessage.ForeColor = Color.FromArgb(56, 176, 0);
                        dgvDrawingIkous_SelectionChanged(this, EventArgs.Empty);
                        RefreshAllCanvases();
                    }
                }
            }
            else
            {
                MessageBox.Show("断面を設定する対象の遺構を選択してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddDrawing_Click(object? sender, EventArgs e)
        {
            int newZid = _db.DrawingsList.Count > 0 ? _db.DrawingsList.Max(d => d.ZID) + 1 : 1;
            var newDrawing = new DrawingModel { ZID = newZid, Name = $"遺構図{newZid}", PaperSize = 3, Scale = 20, Type = 0 };
            _db.DrawingsList.Add(newDrawing);

            string featureName = _db.MasterIkouList.Count > 0
                ? (string.IsNullOrWhiteSpace(_db.MasterIkouList[0].Name) ? $"遺構{_db.MasterIkouList[0].Id}" : _db.MasterIkouList[0].Name)
                : "遺構1";

            var newIkou = new DrawingIkouModel
            {
                ZID = newZid,
                IID = 1,
                Name = featureName,
                P1 = new XYZ(0, 0),
                P2 = new XYZ(0, 0),
                P3 = new XYZ(0, 0),
                PP = new Point3D(0, 0, 0)
            };
            _db.AutoExtractFeatureLines(newIkou, featureName);
            _db.DrawingIkousList.Add(newIkou);

            dgvDrawings.Refresh();
            dgvDrawings_SelectionChanged(this, EventArgs.Empty);
        }

        private void btnDeleteDrawing_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingModel>(dgvDrawings) is DrawingModel selected)
            {
                for (int i = _db.DrawingIkousList.Count - 1; i >= 0; i--)
                {
                    if (_db.DrawingIkousList[i].ZID == selected.ZID) _db.DrawingIkousList.RemoveAt(i);
                }
                _db.DrawingsList.Remove(selected);
                dgvDrawings_SelectionChanged(this, EventArgs.Empty);
            }
        }

        private void btnAddDrawingIkou_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingModel>(dgvDrawings) is DrawingModel selectedDrawing)
            {
                string featureName = cmbFeatureSelect.Text.Trim();
                var existing = _db.DrawingIkousList.Where(di => di.ZID == selectedDrawing.ZID).ToList();

                if (string.IsNullOrEmpty(featureName))
                {
                    int tempId = existing.Count + 1;
                    featureName = $"遺構{tempId}";
                }

                if (existing.Any(di => di.Name.Equals(featureName, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show($"遺構名 [{featureName}] は既にこの図面に登録されています。\n異なる名称を指定してください。", "重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatusMessage.Text = $"✖ 遺構名 [{featureName}] は既に登録されています";
                    lblStatusMessage.ForeColor = Color.FromArgb(239, 35, 60);
                    return;
                }

                int nextSeqId = existing.Count + 1;

                var newItem = new DrawingIkouModel
                {
                    ZID = selectedDrawing.ZID,
                    IID = nextSeqId,
                    Name = featureName,
                    P1 = new XYZ(0, 0),
                    P2 = new XYZ(0, 0),
                    P3 = new XYZ(0, 0),
                    PP = new Point3D(0, 0, 0)
                };

                _db.AutoExtractFeatureLines(newItem, featureName);
                _db.DrawingIkousList.Add(newItem);

                var subIkous = _db.DrawingIkousList.Where(di => di.ZID == selectedDrawing.ZID).ToList();
                int seqId = 1;
                foreach (var ik in subIkous) ik.IID = seqId++;

                dgvDrawingIkous.DataSource = new BindingList<DrawingIkouModel>(subIkous);

                int newIndex = subIkous.IndexOf(newItem);
                if (newIndex >= 0 && newIndex < dgvDrawingIkous.Rows.Count)
                {
                    dgvDrawingIkous.ClearSelection();
                    dgvDrawingIkous.Rows[newIndex].Selected = true;
                }

                lblStatusMessage.Text = $"✔ 編集中の遺構 [{featureName}] を対象遺構図リストに追加しました";
                lblStatusMessage.ForeColor = Color.FromArgb(56, 176, 0);
            }
        }

        private void btnDeleteDrawingIkou_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel selected)
            {
                _db.DrawingIkousList.Remove(selected);
                dgvDrawings_SelectionChanged(this, EventArgs.Empty);
            }
        }

        private void btnAddDanmen_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel selIkou)
            {
                int newDid = selIkou.DmList.Count > 0 ? selIkou.DmList.Max(d => d.DID) + 1 : 1;
                string dmName = string.IsNullOrWhiteSpace(txtDanmenName.Text)
                    ? ((char)('A' + (newDid - 1))).ToString()
                    : txtDanmenName.Text.Trim();

                var newDm = new DanmenRec(newDid, dmName, new XYZ(0, 0), new XYZ(5, 0), new XYZ(newDid * 15, 10));
                newDm.DmpList.Add(new DanmenPRec(0, 10.5));
                newDm.DmpList.Add(new DanmenPRec(2.5, 9.8));
                newDm.DmpList.Add(new DanmenPRec(5.0, 10.5));
                selIkou.DmList.Add(newDm);

                dgvDrawingIkous_SelectionChanged(this, EventArgs.Empty);
            }
        }

        private void btnUpdateDanmenName_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel selIkou &&
                GetSelectedDataBoundItem<DanmenRec>(dgvDanmen) is DanmenRec selDm)
            {
                string newName = txtDanmenName.Text.Trim();
                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("断面名を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                selDm.Name = newName;
                selIkou.DmListStr = selIkou.DmList2Str();

                int curRow = dgvDanmen.CurrentCell?.RowIndex ?? -1;
                dgvDrawingIkous_SelectionChanged(this, EventArgs.Empty);
                if (curRow >= 0 && curRow < dgvDanmen.Rows.Count)
                {
                    dgvDanmen.CurrentCell = dgvDanmen.Rows[curRow].Cells[0];
                }
                RefreshAllCanvases();
                lblStatusMessage.Text = $"✔ 断面「{selDm.DID}」の名称を「{newName}」に更新しました";
                lblStatusMessage.ForeColor = Color.FromArgb(38, 145, 75);
            }
            else
            {
                MessageBox.Show("名称変更する断面を選択してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteDanmen_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel selIkou && GetSelectedDataBoundItem<DanmenRec>(dgvDanmen) is DanmenRec selDm)
            {
                selIkou.DmList.Remove(selDm);
                dgvDrawingIkous_SelectionChanged(this, EventArgs.Empty);
            }
        }

        private void btnSaveDb_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_db.CurrentDbPath) || !File.Exists(_db.CurrentDbPath))
            {
                MessageBox.Show("保存対象の SQLite DB ファイルが指定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _db.SaveDatabase(_db.CurrentDbPath);
                MessageBox.Show($"SQLite データベースへの【図面】・【図面遺構】保存が完了しました。\n{_db.CurrentDbPath}", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB保存エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Canvas Event Delegates

        private bool _isDarkCanvasBackground = false;
        private CheckBox[]? _chkLayers = null;

        private bool IsLayerVisible(int layerId)
        {
            if (_chkLayers == null || _chkLayers.Length == 0) return true;
            int idx = (layerId - 1) % 16;
            if (idx < 0) idx += 16;
            return _chkLayers[idx].Checked;
        }

        private void picCropCanvas_Paint(object? sender, PaintEventArgs e)
        {
            DrawingIkouModel? curSelectedIkou = GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous);
            DrawingRenderer.DrawCropCanvas(
                e.Graphics,
                picCropCanvas.Size,
                _vc,
                _db,
                curSelectedIkou,
                chkShowGridFull.Checked,
                chkShowIkou.Checked,
                chkShowCurveFull.Checked,
                chkColorByIkouFull.Checked,
                chkShowIbutu.Checked,
                chkShowKikai.Checked,
                isLayerVisible: IsLayerVisible,
                showIkouName: chkShowIkouName.Checked,
                showIbutuName: chkShowIbutuName.Checked,
                showKikaiName: chkShowKikaiName.Checked,
                isDarkBackground: _isDarkCanvasBackground);
        }

        private void picCropCanvas_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && _vc.CropStep > 0)
            {
                if (_vc.CropStep == 3)
                {
                    _vc.CropStep = 2;
                    lblStatusMessage.Text = "指示手順: 2.右下(p2) をクリックしてください (右クリックで1点戻る)";
                    lblStatusMessage.ForeColor = Color.FromArgb(255, 191, 0);
                }
                else if (_vc.CropStep == 2)
                {
                    _vc.CropStep = 1;
                    lblStatusMessage.Text = "指示手順: 全体図(測量座標系)上で 1.左下(p1) をクリックしてください (右クリックでキャンセル)";
                    lblStatusMessage.ForeColor = Color.FromArgb(255, 191, 0);
                }
                else if (_vc.CropStep == 1)
                {
                    _vc.CropStep = 0;
                    lblStatusMessage.Text = "3点枠指示をキャンセルしました";
                    lblStatusMessage.ForeColor = Color.FromArgb(220, 220, 220);
                }
                picCropCanvas.Invalidate();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (_vc.CropStep > 0 && GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel curIkou)
                {
                    var (sx, sy) = _vc.CanvasToSurveyCrop(e.Location, picCropCanvas.Size, _db.MasterIkouList, _db.MasterIkouLList, _db.MasterIbutuList, _db.MasterKikaiList);
                    if (_vc.CropStep == 1)
                    {
                        curIkou.P1 = new XYZ(sx, sy);
                        _vc.CropStep = 2;
                        lblStatusMessage.Text = "指示手順: 2.右下(p2) をクリックしてください (右クリックで1点戻る)";
                    }
                    else if (_vc.CropStep == 2)
                    {
                        curIkou.P2 = new XYZ(sx, sy);
                        _vc.CropStep = 3;
                        lblStatusMessage.Text = "指示手順: 3.高さ指示点(p3) をクリックしてください (右クリックで1点戻る)";
                    }
                    else if (_vc.CropStep == 3)
                    {
                        curIkou.P3 = GeometryMath.ProjectToPerpendicular(curIkou.P1, curIkou.P2, sx, sy);
                        _vc.CropStep = 0;
                        lblStatusMessage.Text = "✔ 3点指示による長方形枠定義が完了しました (左下 ➡ 右下 ➡ 高さ指示)";
                        lblStatusMessage.ForeColor = Color.FromArgb(56, 176, 0);
                        var (msg, isSuccess) = _db.AutoExtractFeatureLines(curIkou, cmbFeatureSelect.Text.Trim());
                    }
                    RefreshAllCanvases();
                    return;
                }
            }

            _vc.IsCropMouseDown = true;
            _vc.CropLastMousePos = e.Location;
        }

        private void picCropCanvas_MouseMove(object? sender, MouseEventArgs e)
        {
            var (sx, sy) = _vc.CanvasToSurveyCrop(e.Location, picCropCanvas.Size, _db.MasterIkouList, _db.MasterIkouLList, _db.MasterIbutuList, _db.MasterKikaiList);
            lblStatusCoords.Text = $"({sx:0.000}, {sy:0.000})";

            if (_vc.IsCropMouseDown)
            {
                int mdx = e.X - _vc.CropLastMousePos.X;
                int mdy = e.Y - _vc.CropLastMousePos.Y;
                _vc.CropPan = new PointF(_vc.CropPan.X + mdx, _vc.CropPan.Y + mdy);
                picCropCanvas.Invalidate();
            }
            else if (_vc.CropStep > 0)
            {
                picCropCanvas.Invalidate();
            }

            _vc.CropLastMousePos = e.Location;
        }

        private void picCropCanvas_MouseUp(object? sender, MouseEventArgs e)
        {
            _vc.IsCropMouseDown = false;
        }

        private void picCropCanvas_MouseWheel(object? sender, MouseEventArgs e)
        {
            float oldZoom = _vc.CropZoom;
            float scaleFactor = e.Delta > 0 ? 1.15f : (1.0f / 1.15f);
            float newZoom = Math.Clamp(oldZoom * scaleFactor, 0.1f, 30.0f);
            float zoomRatio = newZoom / oldZoom;

            float cx = picCropCanvas.Width / 2f;
            float cy = picCropCanvas.Height / 2f;
            float mouseRelX = e.X - (cx + _vc.CropPan.X);
            float mouseRelY = e.Y - (cy + _vc.CropPan.Y);

            _vc.CropPan = new PointF(_vc.CropPan.X - mouseRelX * (zoomRatio - 1.0f), _vc.CropPan.Y - mouseRelY * (zoomRatio - 1.0f));
            _vc.CropZoom = newZoom;

            picCropCanvas.Invalidate();
        }

        private void picPaperCanvas_Paint(object? sender, PaintEventArgs e)
        {
            DrawingModel? curDrawing = GetSelectedDataBoundItem<DrawingModel>(dgvDrawings);
            DrawingIkouModel? curSelectedIkou = GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous);
            DrawingRenderer.DrawPaperCanvas(
                e.Graphics,
                picPaperCanvas.Size,
                _vc,
                _db,
                curDrawing,
                curSelectedIkou,
                chkShowCurvePaper.Checked,
                chkColorByIkouPaper.Checked,
                chkShowDirectionPaper.Checked,
                chkShowDanmenPaper.Checked);
        }

        private void picPaperCanvas_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (_vc.IsPickingPaperPosition)
                {
                    _vc.IsPickingPaperPosition = false;
                    lblStatusMessage.Text = "遺構枠配置位置指定をキャンセルしました";
                    lblStatusMessage.ForeColor = Color.FromArgb(220, 220, 220);
                    picPaperCanvas.Invalidate();
                    return;
                }
                if (_vc.IsPickingDanmenPosition)
                {
                    _vc.IsPickingDanmenPosition = false;
                    lblStatusMessage.Text = "断面位置指定をキャンセルしました";
                    lblStatusMessage.ForeColor = Color.FromArgb(220, 220, 220);
                    picPaperCanvas.Invalidate();
                    return;
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                if ((_vc.IsPickingPaperPosition || _vc.IsPickingDanmenPosition) &&
                    GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel curIkou && GetSelectedDataBoundItem<DrawingModel>(dgvDrawings) is DrawingModel curDrawing)
                {
                    var pInfo = curDrawing.PaperInfo;
                    int canvasWidth = picPaperCanvas.Width;
                    int canvasHeight = picPaperCanvas.Height;

                    int margin = 25;
                    double paperAspect = pInfo.WidthMm / pInfo.HeightMm;
                    double screenAspect = (double)(canvasWidth - margin * 2) / (canvasHeight - margin * 2);

                    double renderPaperWidth, renderPaperHeight;
                    if (screenAspect > paperAspect)
                    {
                        renderPaperHeight = canvasHeight - margin * 2;
                        renderPaperWidth = renderPaperHeight * paperAspect;
                    }
                    else
                    {
                        renderPaperWidth = canvasWidth - margin * 2;
                        renderPaperHeight = renderPaperWidth / paperAspect;
                    }

                    float cx = canvasWidth / 2f;
                    float cy = canvasHeight / 2f;

                    float paperCenterX = cx + _vc.PaperPan.X;
                    float paperCenterY = cy + _vc.PaperPan.Y;
                    float paperW = (float)(renderPaperWidth * _vc.PaperZoom);
                    float paperH = (float)(renderPaperHeight * _vc.PaperZoom);

                    double mmX = (e.X - paperCenterX) / paperW * pInfo.WidthMm;
                    double mmY = -(e.Y - paperCenterY) / paperH * pInfo.HeightMm;

                    if (_vc.IsPickingPaperPosition)
                    {
                        curIkou.PP = new Point3D(mmX, mmY, 0);
                        _vc.IsPickingPaperPosition = false;
                        lblStatusMessage.Text = $"✔ 遺構枠中心の配置位置 (PX: {mmX:0.0}mm, PY: {mmY:0.0}mm [用紙中心基準]) を更新しました";
                        lblStatusMessage.ForeColor = Color.FromArgb(56, 176, 0);
                    }
                    else if (_vc.IsPickingDanmenPosition)
                    {
                        int newDid = curIkou.DmList.Count > 0 ? curIkou.DmList.Max(d => d.DID) + 1 : 1;
                        string dmName = string.IsNullOrWhiteSpace(txtDanmenName.Text) ? ((char)('A' + (newDid - 1))).ToString() : txtDanmenName.Text.Trim();
                        var newDm = new DanmenRec(newDid, dmName, new XYZ(0, 0), new XYZ(5, 0), new XYZ(mmX, mmY));
                        newDm.DmpList.Add(new DanmenPRec(0, 10.5));
                        newDm.DmpList.Add(new DanmenPRec(2.5, 9.8));
                        newDm.DmpList.Add(new DanmenPRec(5.0, 10.5));
                        curIkou.DmList.Add(newDm);

                        _vc.IsPickingDanmenPosition = false;
                        lblStatusMessage.Text = $"✔ 断面 {dmName} を追加しました";
                        lblStatusMessage.ForeColor = Color.FromArgb(56, 176, 0);
                        dgvDrawingIkous_SelectionChanged(this, EventArgs.Empty);
                    }

                    RefreshAllCanvases();
                    return;
                }
            }

            _vc.IsPaperMouseDown = true;
            _vc.PaperLastMousePos = e.Location;
        }

        private void picPaperCanvas_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_vc.IsPaperMouseDown)
            {
                int mdx = e.X - _vc.PaperLastMousePos.X;
                int mdy = e.Y - _vc.PaperLastMousePos.Y;
                _vc.PaperPan = new PointF(_vc.PaperPan.X + mdx, _vc.PaperPan.Y + mdy);
                picPaperCanvas.Invalidate();
            }
            else if (_vc.IsPickingPaperPosition || _vc.IsPickingDanmenPosition)
            {
                picPaperCanvas.Invalidate();
            }

            _vc.PaperLastMousePos = e.Location;
        }

        private void picPaperCanvas_MouseUp(object? sender, MouseEventArgs e)
        {
            _vc.IsPaperMouseDown = false;
        }

        private void picPaperCanvas_MouseWheel(object? sender, MouseEventArgs e)
        {
            float oldZoom = _vc.PaperZoom;
            float scaleFactor = e.Delta > 0 ? 1.15f : (1.0f / 1.15f);
            float newZoom = Math.Clamp(oldZoom * scaleFactor, 0.1f, 30.0f);
            float zoomRatio = newZoom / oldZoom;

            float cx = picPaperCanvas.Width / 2f;
            float cy = picPaperCanvas.Height / 2f;
            float mouseRelX = e.X - (cx + _vc.PaperPan.X);
            float mouseRelY = e.Y - (cy + _vc.PaperPan.Y);

            _vc.PaperPan = new PointF(_vc.PaperPan.X - mouseRelX * (zoomRatio - 1.0f), _vc.PaperPan.Y - mouseRelY * (zoomRatio - 1.0f));
            _vc.PaperZoom = newZoom;

            picPaperCanvas.Invalidate();
        }

        private void picFeatureDetailCanvas_Paint(object? sender, PaintEventArgs e)
        {
            DrawingModel? curDrawing = GetSelectedDataBoundItem<DrawingModel>(dgvDrawings);
            DrawingIkouModel? curIkou = GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous);
            DrawingRenderer.DrawDetailCanvas(
                e.Graphics,
                picFeatureDetailCanvas.Size,
                _vc,
                _db,
                curDrawing,
                curIkou,
                chkColorByIkouFull.Checked,
                chkShowDirection.Checked);
        }

        private void picFeatureDetailCanvas_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && _vc.IsPickingDirectionPosition)
            {
                _vc.IsPickingDirectionPosition = false;
                lblStatusMessage.Text = "方位マーク位置指定をキャンセルしました";
                lblStatusMessage.ForeColor = Color.FromArgb(220, 220, 220);
                picFeatureDetailCanvas.Invalidate();
                return;
            }

            if (e.Button == MouseButtons.Left && _vc.IsPickingDirectionPosition)
            {
                if (GetSelectedDataBoundItem<DrawingIkouModel>(dgvDrawingIkous) is DrawingIkouModel curIkou &&
                    GetSelectedDataBoundItem<DrawingModel>(dgvDrawings) is DrawingModel curDrawing)
                {
                    int width = picFeatureDetailCanvas.Width;
                    int height = picFeatureDetailCanvas.Height;

                    var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(curIkou.P1, curIkou.P2, curIkou.P3);
                    int margin = 35;
                    double scale = Math.Min((width - margin * 2) / widthM, (height - margin * 2) / heightM) * 0.70;

                    double xLocalM = (e.X - width / 2f) / scale;
                    double yLocalM = -(e.Y - height / 2f) / scale;

                    double dxMm = xLocalM * (1000.0 / curDrawing.Scale);
                    double dyMm = yLocalM * (1000.0 / curDrawing.Scale);

                    curIkou.PDirection = new Point3D(dxMm, dyMm, 0);
                    _vc.IsPickingDirectionPosition = false;

                    lblStatusMessage.Text = $"✔ 選択遺構プレビュー上で 方位マーク位置 (DX: {dxMm:0.0}mm, DY: {dyMm:0.0}mm [遺構中心基準]) を指定しました";
                    lblStatusMessage.ForeColor = Color.FromArgb(56, 176, 0);

                    RefreshAllCanvases();
                }
            }
        }

        private void picFeatureDetailCanvas_MouseMove(object? sender, MouseEventArgs e)
        {
            _vc.DetailLastMousePos = e.Location;
            if (_vc.IsPickingDirectionPosition)
            {
                picFeatureDetailCanvas.Invalidate();
            }
        }

        #endregion
    }
}
