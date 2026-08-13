using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Site7DbEditor.Services;

namespace Site7DbEditor
{
    public class IkouComboItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public override string ToString() => $"ID:{Id}  {Name}";
    }

    public partial class FormEditor : Form
    {
        private bool _isUpdatingSelection = false;
        private bool _isLoadingDatabase = false;

        private long _selectedIkouId = -1;
        private long _selectedLid = -1;
        private int _selectedPointIndex = -1;
        private long _selectedIbutuId = -1;
        private long _selectedKikaiId = -1;

        private readonly EditorDbManager _db = new EditorDbManager();
        private readonly EditorMapViewController _vc = new EditorMapViewController();

        private UCCtrl _ucCtrl = new UCCtrl();
        private FormBluetoothCtrl? _dlgBth = null;
        private FormLeftPanelCtrl? _dlgLeft = null;
        private bool _isLeftPanelFloating = false;
        private FormBottomPanelCtrl? _dlgBottom = null;
        private bool _isBottomPanelFloating = false;

        public class DbItem
        {
            public string DisplayName { get; set; } = "";
            public string FullPath { get; set; } = "";
            public override string ToString() => DisplayName;
        }

        public FormEditor()
        {
            InitializeComponent();
            gbl.FormMain = this;
            SetupStyles();
            InitRightEditControls();
            WireEvents();
            tabControlData_SelectedIndexChanged(this, EventArgs.Empty);
            WireDebugLayoutInfo();
        }

        private void WireDebugLayoutInfo()
        {
            this.Resize += (s, e) => UpdateDebugLayoutInfo();
            if (tabIkou != null)
            {
                tabIkou.Resize += (s, e) => UpdateDebugLayoutInfo();
            }
            if (pnlPrecsRight != null)
            {
                pnlPrecsRight.Resize += (s, e) => UpdateDebugLayoutInfo();
                pnlPrecsRight.LocationChanged += (s, e) => UpdateDebugLayoutInfo();
            }
            UpdateDebugLayoutInfo();
        }

        public void UpdateDebugLayoutInfo()
        {
            if (tabIkou != null && pnlPrecsRight != null)
            {
                string info = $"tabIkou[Pos:({tabIkou.Location.X},{tabIkou.Location.Y}) Size:{tabIkou.Width}x{tabIkou.Height}] | pnlPrecsRight[Dock:{pnlPrecsRight.Dock} Pos:({pnlPrecsRight.Location.X},{pnlPrecsRight.Location.Y}) Size:{pnlPrecsRight.Width}x{pnlPrecsRight.Height}]";
                if (lblBottomTitle != null)
                {
                    lblBottomTitle.Text = info;
                }
                if (_dlgBottom != null && !_dlgBottom.IsDisposed && _dlgBottom.lblTitle != null)
                {
                    _dlgBottom.lblTitle.Text = info;
                }
            }
        }

        private void SetupStyles()
        {
            ApplyDgvStyle(dgvIkou);
            ApplyDgvStyle(dgvIkouL);
            ApplyDgvStyle(dgvPrecs);
            ApplyDgvStyle(dgvIbutu);
            ApplyDgvStyle(dgvKikai);
            ApplyDgvStyle(dgvLayer);
            if (dgvBatchPreview != null) ApplyDgvStyle(dgvBatchPreview);

            dgvIkou.DataBindingComplete += (s, e) => ApplyDgvIkouColumns();
            dgvIkouL.DataBindingComplete += (s, e) => ApplyDgvIkouLColumns();
            dgvPrecs.DataBindingComplete += (s, e) => ApplyDgvPrecsColumns();
            dgvIbutu.DataBindingComplete += (s, e) => ApplyDgvIbutuColumns();
            dgvKikai.DataBindingComplete += (s, e) => ApplyDgvKikaiColumns();
        }

        private void ApplyDgvIkouColumns()
        {
            foreach (DataGridViewColumn col in dgvIkou.Columns)
            {
                col.Visible = (col.Name == "Id" || col.Name == "Name");
            }
            var colId = dgvIkou.Columns["Id"];
            if (colId != null)
            {
                colId.DisplayIndex = 0;
                colId.HeaderText = "ID";
                colId.Width = 55;
                colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            var colName = dgvIkou.Columns["Name"];
            if (colName != null) { colName.DisplayIndex = 1; colName.HeaderText = "遺構名"; colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; }
        }

        private void ApplyDgvIkouLColumns()
        {
            foreach (DataGridViewColumn col in dgvIkouL.Columns)
            {
                col.Visible = (col.Name == "Lid" || col.Name == "Name" || col.Name == "Mode" || col.Name == "Layer");
            }
            var colId = dgvIkouL.Columns["Id"];
            if (colId != null) { colId.Visible = false; }
            var colLid = dgvIkouL.Columns["Lid"];
            if (colLid != null)
            {
                colLid.DisplayIndex = 0;
                colLid.HeaderText = "LID";
                colLid.Width = 42;
                colLid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            var colName = dgvIkouL.Columns["Name"];
            if (colName != null) { colName.DisplayIndex = 1; colName.HeaderText = "線名"; colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; }
            var colMode = dgvIkouL.Columns["Mode"];
            if (colMode != null) { colMode.DisplayIndex = 2; colMode.HeaderText = "開閉"; colMode.Width = 45; }
            var colLayer = dgvIkouL.Columns["Layer"];
            if (colLayer != null) { colLayer.DisplayIndex = 3; colLayer.HeaderText = "レイヤ"; colLayer.Width = 50; }
        }

        private void ApplyDgvPrecsColumns()
        {
            foreach (DataGridViewColumn col in dgvPrecs.Columns)
            {
                col.Visible = (col.Name == "Pid" || col.Name == "X" || col.Name == "Y" || col.Name == "Z");
            }
            var colPid = dgvPrecs.Columns["Pid"];
            if (colPid != null) { colPid.DisplayIndex = 0; colPid.HeaderText = "PID"; colPid.Width = 40; colPid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; }
            var colX = dgvPrecs.Columns["X"];
            if (colX != null)
            {
                colX.DisplayIndex = 1;
                colX.HeaderText = "X";
                colX.Width = 92;
                colX.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colX.DefaultCellStyle.Format = "0.000";
            }
            var colY = dgvPrecs.Columns["Y"];
            if (colY != null)
            {
                colY.DisplayIndex = 2;
                colY.HeaderText = "Y";
                colY.Width = 92;
                colY.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colY.DefaultCellStyle.Format = "0.000";
            }
            var colZ = dgvPrecs.Columns["Z"];
            if (colZ != null)
            {
                colZ.DisplayIndex = 3;
                colZ.HeaderText = "Z";
                colZ.Width = 70;
                colZ.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colZ.DefaultCellStyle.Format = "0.000";
            }
        }

        private void ApplyDgvIbutuColumns()
        {
            var visibleNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Id", "Chiku", "Soui", "Syubetu", "No", "X", "Y", "Z", "Layer"
            };
            foreach (DataGridViewColumn col in dgvIbutu.Columns)
            {
                col.Visible = visibleNames.Contains(col.Name);
                if (col.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) || col.Name.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (col.Name.Equals("X", StringComparison.OrdinalIgnoreCase) || col.Name.Equals("Y", StringComparison.OrdinalIgnoreCase) || col.Name.Equals("Z", StringComparison.OrdinalIgnoreCase))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.Format = "0.000";
                }
            }
        }

        private void ApplyDgvKikaiColumns()
        {
            var visibleNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Id", "Name", "X", "Y", "Z", "Layer"
            };
            foreach (DataGridViewColumn col in dgvKikai.Columns)
            {
                col.Visible = visibleNames.Contains(col.Name);
                if (col.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (col.Name.Equals("X", StringComparison.OrdinalIgnoreCase) || col.Name.Equals("Y", StringComparison.OrdinalIgnoreCase) || col.Name.Equals("Z", StringComparison.OrdinalIgnoreCase))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.Format = "0.000";
                }
            }
        }

        private void ApplyDgvStyle(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.DataError += (s, e) => { e.ThrowException = false; };
            dgv.BackgroundColor = Color.FromArgb(30, 30, 38);
            dgv.ForeColor = Color.White;
            dgv.GridColor = Color.FromArgb(55, 55, 65);
            dgv.RowHeadersVisible = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(43, 45, 66);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 180, 216);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 38);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 180, 216);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void InitRightEditControls()
        {
            cmbIkouKind.Items.Clear();
            cmbIkouKind.Items.AddRange(new object[] { "Pit", "SD", "SX", "SI", "SK", "SDa", "SB", "SP", "遺構" });
            if (cmbIkouKind.Items.Count > 0) cmbIkouKind.SelectedIndex = 0;

            cmbLineKind.Items.Clear();
            cmbLineKind.Items.AddRange(new object[] { "土師器", "須恵器", "上", "下端", "瓦", "灰", "焼土", "SP", "sp10", "sp15", "測量線" });
            if (cmbLineKind.Items.Count > 0) cmbLineKind.SelectedIndex = 0;

            cmbLineLayer.Items.Clear();
            cmbLineLayer.Items.AddRange(new object[] { "L01", "L02", "L03", "L04", "L05", "L06", "L07", "L08", "L09", "L10", "L16", "L32", "L64" });
            if (cmbLineLayer.Items.Count > 0) cmbLineLayer.SelectedIndex = 0;

            cmbIbutuLayer.Items.Clear();
            cmbIbutuLayer.Items.AddRange(new object[] { "L01 遺物L01", "L02 遺物L02", "L03 遺物L03", "L04 遺物L04", "L05 遺物L05", "L06 遺物L06", "L07 遺物L07", "L08 遺物L08", "L09 遺物L09", "L10 遺物L10", "L16 遺物L16" });
            if (cmbIbutuLayer.Items.Count > 4) cmbIbutuLayer.SelectedIndex = 4;

            cmbKikaiLayer.Items.Clear();
            cmbKikaiLayer.Items.AddRange(new object[] { "L01 基準点", "L02 基準点L02", "L03 基準点L03", "L04 基準点L04", "L05 基準点L05", "L16 基準点L16" });
            if (cmbKikaiLayer.Items.Count > 0) cmbKikaiLayer.SelectedIndex = 0;
        }

        private long _editingOldIkouId = -1;

        private void WireEvents()
        {
            this.Load += FormEditor_Load;
            this.Resize += (s, e) => UpdatePanelWidthsDisplay();
            this.panelMapLeft.Resize += (s, e) => UpdatePanelWidthsDisplay();
            this.panelMapRight.Resize += (s, e) => UpdatePanelWidthsDisplay();
            this.panelMapRight.VisibleChanged += (s, e) => UpdatePanelWidthsDisplay();
            this.picMapCanvas.SizeChanged += (s, e) => { _vc.InvalidateBoundsCache(); UpdatePanelWidthsDisplay(); picMapCanvas.Invalidate(); };
            this.btnOpenDb.Click += btnOpenDb_Click;
            this.cmbQuickDbSelect.SelectedIndexChanged += cmbQuickDbSelect_SelectedIndexChanged;
            this.btnSaveDb.Click += btnSaveDb_Click;

            this.dgvIkou.SelectionChanged += dgvIkou_SelectionChanged;
            this.dgvIkouL.SelectionChanged += dgvIkouL_SelectionChanged;
            this.dgvPrecs.SelectionChanged += dgvPrecs_SelectionChanged;
            this.dgvIbutu.SelectionChanged += dgvIbutu_SelectionChanged;
            this.dgvKikai.SelectionChanged += dgvKikai_SelectionChanged;

            this.dgvIkou.CellBeginEdit += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvIkou.Rows.Count && dgvIkou.Rows[e.RowIndex].DataBoundItem is IkouModel ikou)
                    _editingOldIkouId = ikou.Id;
            };

            this.dgvIkou.CellValueChanged += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvIkou.Rows.Count) return;
                string colName = dgvIkou.Columns[e.ColumnIndex].Name;

                if (colName.Equals("Id", StringComparison.OrdinalIgnoreCase) && dgvIkou.Rows[e.RowIndex].DataBoundItem is IkouModel ikou)
                {
                    long newId = ikou.Id;
                    if (_editingOldIkouId > 0 && _editingOldIkouId != newId)
                    {
                        foreach (var line in _db.IkouLList)
                        {
                            if (line.Id == _editingOldIkouId) line.Id = newId;
                        }
                        _editingOldIkouId = newId;
                        dgvIkou_SelectionChanged(this, EventArgs.Empty);
                    }
                }
            };

            this.dgvIkou.CellValidating += dgvIkou_CellValidating;
            this.dgvIkouL.CellValidating += dgvIkouL_CellValidating;
            this.dgvIbutu.CellValidating += dgvIbutu_CellValidating;
            this.dgvKikai.CellValidating += dgvKikai_CellValidating;
            this.dgvLayer.CellValidating += dgvLayer_CellValidating;

            this.dgvPrecs.CellValueChanged += dgvPrecs_CellValueChanged;

            this.btnAddIkou.Click += btnAddIkou_Click;
            this.btnDeleteIkouRight.Click += btnDeleteIkou_Click;

            this.btnAddIkouL.Click += btnAddIkouL_Click;
            this.btnDeleteLineRight.Click += btnDeleteIkouL_Click;

            this.txtFilterIbutu.TextChanged += txtFilterIbutu_TextChanged;
            this.btnIbutuMaxPlusOne.Click += btnIbutuMaxPlusOne_Click;

            this.btnAddLayer.Click += btnAddLayer_Click;
            this.btnDeleteLayer.Click += btnDeleteLayer_Click;

            // Right Property Controls Events
            this.btnView3D.Click += btnView3D_Click;
            this.btnUpdateIkouRight.Click += btnUpdateIkouRight_Click;
            this.btnUpdateLineRight.Click += btnUpdateLineRight_Click;
            this.btnUpdatePointRight.Click += btnUpdatePointRight_Click;
            this.btnDeletePointRight.Click += btnDeletePointRight_Click;
            this.btnAddPointRight.Click += btnAddPointRight_Click;

            // Double Click Map Centering Events
            this.dgvIkou.CellDoubleClick += dgvIkou_CellDoubleClick;
            this.dgvIkouL.CellDoubleClick += dgvIkouL_CellDoubleClick;
            this.dgvPrecs.CellDoubleClick += dgvPrecs_CellDoubleClick;
            this.dgvIbutu.CellDoubleClick += dgvIbutu_CellDoubleClick;
            this.dgvKikai.CellDoubleClick += dgvKikai_CellDoubleClick;

            // Batch Update Events
            this.cmbBatchTable.SelectedIndexChanged += cmbBatchTable_SelectedIndexChanged;
            this.cmbBatchFilterCol.SelectedIndexChanged += (s, e) => RefreshBatchPreview();
            this.cmbBatchFilterOp.SelectedIndexChanged += (s, e) => RefreshBatchPreview();
            this.txtBatchFilterVal.TextChanged += (s, e) => RefreshBatchPreview();
            this.cmbBatchUpdateCol.SelectedIndexChanged += (s, e) => RefreshBatchPreview();
            this.txtBatchUpdateVal.TextChanged += (s, e) => RefreshBatchPreview();
            this.btnBatchExecute.Click += btnBatchExecute_Click;

            _chkMapLayers = new CheckBox[]
            {
                chkLayer01, chkLayer02, chkLayer03, chkLayer04,
                chkLayer05, chkLayer06, chkLayer07, chkLayer08,
                chkLayer09, chkLayer10, chkLayer11, chkLayer12,
                chkLayer13, chkLayer14, chkLayer15, chkLayer16
            };

            foreach (var chk in _chkMapLayers)
            {
                chk.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            }

            btnLayerAllOn.Click += (s, e) =>
            {
                foreach (var chk in _chkMapLayers) chk.Checked = true;
                picMapCanvas.Invalidate();
            };

            btnLayerAllOff.Click += (s, e) =>
            {
                foreach (var chk in _chkMapLayers) chk.Checked = false;
                picMapCanvas.Invalidate();
            };

            btnBgSettings.Click += (s, e) =>
            {
                _isDarkMapBackground = !_isDarkMapBackground;
                picMapCanvas.Invalidate();
            };

            btnEnvSettings.Click += (s, e) =>
            {
                using (var form = new FormDefEnv())
                {
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        picMapCanvas.Invalidate();
                    }
                }
            };

            _ucCtrl.CoordinateReceived += (x, y, z) =>
            {
                SetCXYZ(x, y, z);
            };

            btnDetachWindow.Click += (s, e) =>
            {
                SetBluetoothDisplayMode(true);
            };

            btnDetachLeftPanel.Click += (s, e) => SetLeftPanelDisplayMode(!_isLeftPanelFloating);
            btnDetachBottomPanel.Click += (s, e) => SetBottomPanelDisplayMode(!_isBottomPanelFloating);

            btnLayerSettings.Click += (s, e) =>
            {
                using (var form = new FormLayerSettings(_db))
                {
                    form.ShowDialog(this);
                    PopulateIkouLineLayerCombo();
                    PopulateIbutuCombos();
                    PopulateIkouMasterCombo();
                    picMapCanvas.Invalidate();
                }
            };

            btnBatchUpdateModal.Click += (s, e) =>
            {
                using (var form = new FormBatchUpdate(_db))
                {
                    form.ShowDialog(this);
                    PopulateIkouLineLayerCombo();
                    PopulateIbutuCombos();
                    PopulateIkouMasterCombo();
                    if (dgvIkou.DataSource is BindingList<IkouModel> blIkou) blIkou.ResetBindings();
                    if (dgvIbutu.DataSource is BindingList<IbutuModel> blIbutu) blIbutu.ResetBindings();
                    if (dgvKikai.DataSource is BindingList<KikaiModel> blKikai) blKikai.ResetBindings();
                    picMapCanvas.Invalidate();
                }
            };

            this.chkShowIkou.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.chkShowIkouName.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.chkShowIbutu.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.chkShowIbutuName.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.chkShowKikai.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.chkShowKikaiName.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.chkShowCurve.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.chkShowGrid.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.chkColorByIkou.CheckedChanged += (s, e) => picMapCanvas.Invalidate();
            this.tabControlData.SelectedIndexChanged += tabControlData_SelectedIndexChanged;
            this.cmbIkouKind.SelectedIndexChanged += (s, e) => UpdateCombinedIkouNameLabel();
            this.cmbIkouKind.TextChanged += (s, e) => UpdateCombinedIkouNameLabel();
            this.txtIkouNum.TextChanged += (s, e) => UpdateCombinedIkouNameLabel();
            this.btnMaxPlusOne.Click += btnMaxPlusOne_Click;
            this.btnSetPos.Click += (s, e) => MessageBox.Show("平面図上の表示位置をクリック指定してください。", "表示位置指定", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.btnLineSetPos.Click += (s, e) => MessageBox.Show("平面図上の表示位置をクリック指定してください。", "表示位置指定", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.cmbLineKind.SelectedIndexChanged += (s, e) => UpdateCombinedLineNameLabel();
            this.cmbLineKind.TextChanged += (s, e) => UpdateCombinedLineNameLabel();
            this.txtLineNum.TextChanged += (s, e) => UpdateCombinedLineNameLabel();
            this.btnLineMaxPlusOne.Click += btnLineMaxPlusOne_Click;
            this.btnResetMapZoom.Click += (s, e) => { _vc.ResetZoom(); picMapCanvas.Invalidate(); };

            this.picMapCanvas.Paint += picMapCanvas_Paint;
            this.picMapCanvas.MouseDown += picMapCanvas_MouseDown;
            this.picMapCanvas.MouseMove += picMapCanvas_MouseMove;
            this.picMapCanvas.MouseUp += picMapCanvas_MouseUp;
            this.picMapCanvas.MouseWheel += picMapCanvas_MouseWheel;
            this.picMapCanvas.MouseDoubleClick += picMapCanvas_MouseDoubleClick;
        }

        private void EnsureUCCtrlValid()
        {
            if (_ucCtrl == null || _ucCtrl.IsDisposed)
            {
                _ucCtrl = new UCCtrl();
                _ucCtrl.CoordinateReceived += (x, y, z) =>
                {
                    SetCXYZ(x, y, z);
                };
            }
        }

        public void SetBluetoothDisplayMode(bool isFloatingForm)
        {
            EnsureUCCtrlValid();

            if (isFloatingForm)
            {
                Point targetLoc = panelMapRight.PointToScreen(Point.Empty);
                Size targetSize = panelMapRight.Size;

                if (panelRightContent.Controls.Contains(_ucCtrl))
                {
                    panelRightContent.Controls.Remove(_ucCtrl);
                }
                panelMapRight.Visible = false;

                if (_dlgBth == null || _dlgBth.IsDisposed)
                {
                    _dlgBth = new FormBluetoothCtrl();
                    _dlgBth.DockToPanelRequested += (s, e) => SetBluetoothDisplayMode(false);
                    _dlgBth.FormClosing += (s, e) =>
                    {
                        if (_dlgBth != null && _dlgBth.panelBthContent.Controls.Contains(_ucCtrl))
                        {
                            _dlgBth.panelBthContent.Controls.Remove(_ucCtrl);
                        }
                        EnsureUCCtrlValid();
                        if (!_ucCtrl.IsDisposed && !panelRightContent.Controls.Contains(_ucCtrl))
                        {
                            _ucCtrl.Dock = DockStyle.Fill;
                            panelRightContent.Controls.Add(_ucCtrl);
                        }
                        panelMapRight.Visible = true;
                        _vc.InvalidateBoundsCache();
                        UpdatePanelWidthsDisplay();
                        picMapCanvas.Invalidate();
                    };
                    _dlgBth.FormClosed += (s, e) => { _dlgBth = null; };
                }

                _dlgBth.StartPosition = FormStartPosition.Manual;
                _dlgBth.Location = targetLoc;
                if (targetSize.Width > 0 && targetSize.Height > 0)
                {
                    _dlgBth.Size = targetSize;
                }

                _ucCtrl.Dock = DockStyle.Fill;
                if (!_dlgBth.panelBthContent.Controls.Contains(_ucCtrl))
                {
                    _dlgBth.panelBthContent.Controls.Add(_ucCtrl);
                }
                _ucCtrl.BringToFront();
                _dlgBth.Show(this);
                _dlgBth.BringToFront();
                picMapCanvas.Invalidate();
            }
            else
            {
                if (_dlgBth != null && !_dlgBth.IsDisposed)
                {
                    if (_dlgBth.Controls.Contains(_ucCtrl))
                    {
                        _dlgBth.Controls.Remove(_ucCtrl);
                    }
                    _dlgBth.Close();
                    _dlgBth = null;
                }

                _ucCtrl.Dock = DockStyle.Fill;
                if (!panelRightContent.Controls.Contains(_ucCtrl))
                {
                    panelRightContent.Controls.Add(_ucCtrl);
                }
                _ucCtrl.BringToFront();

                panelMapRight.Visible = true;
                panelMapRight.SendToBack();
                panelMapLeft.SendToBack();
                picMapCanvas.BringToFront();
                picMapCanvas.Invalidate();
            }
        }

        public void SetLeftPanelDisplayMode(bool isFloatingForm)
        {
            _isLeftPanelFloating = isFloatingForm;

            if (isFloatingForm)
            {
                Point targetLoc = panelMapLeft.PointToScreen(Point.Empty);
                Size targetSize = panelMapLeft.Size;

                if (_dlgLeft == null || _dlgLeft.IsDisposed)
                {
                    _dlgLeft = new FormLeftPanelCtrl();
                    _dlgLeft.DockToPanelRequested += (s, e) => SetLeftPanelDisplayMode(false);
                    _dlgLeft.FormClosing += (s, e) =>
                    {
                        if (_isLeftPanelFloating)
                        {
                            SetLeftPanelDisplayMode(false);
                        }
                    };
                    _dlgLeft.FormClosed += (s, e) => { _dlgLeft = null; };
                }

                if (panelMapArea.Controls.Contains(panelMapLeft))
                {
                    panelMapArea.Controls.Remove(panelMapLeft);
                }

                panelLeftHeader.Visible = false;
                panelMapLeft.Dock = DockStyle.Fill;
                if (!_dlgLeft.panelLeftContent.Controls.Contains(panelMapLeft))
                {
                    _dlgLeft.panelLeftContent.Controls.Add(panelMapLeft);
                }

                _dlgLeft.StartPosition = FormStartPosition.Manual;
                _dlgLeft.Location = targetLoc;
                if (targetSize.Width > 0 && targetSize.Height > 0)
                {
                    _dlgLeft.Size = targetSize;
                }
                _dlgLeft.Show(this);
            }
            else
            {
                if (_dlgLeft != null && !_dlgLeft.IsDisposed)
                {
                    if (_dlgLeft.panelLeftContent.Controls.Contains(panelMapLeft))
                    {
                        _dlgLeft.panelLeftContent.Controls.Remove(panelMapLeft);
                    }
                    _dlgLeft.Close();
                    _dlgLeft = null;
                }

                panelLeftHeader.Visible = true;
                panelMapLeft.Dock = DockStyle.Left;
                if (!panelMapArea.Controls.Contains(panelMapLeft))
                {
                    panelMapArea.Controls.Add(panelMapLeft);
                }

                panelMapRight.SendToBack();
                panelMapLeft.SendToBack();
                picMapCanvas.BringToFront();
            }

            _vc.InvalidateBoundsCache();
            UpdatePanelWidthsDisplay();
            picMapCanvas.Invalidate();
        }

        public void SetBottomPanelDisplayMode(bool isFloatingForm)
        {
            _isBottomPanelFloating = isFloatingForm;

            if (isFloatingForm)
            {
                Point targetLoc = panelMapBottom.PointToScreen(Point.Empty);
                Size targetSize = panelMapBottom.Size;

                if (_dlgBottom == null || _dlgBottom.IsDisposed)
                {
                    _dlgBottom = new FormBottomPanelCtrl();
                    _dlgBottom.DockToPanelRequested += (s, e) => SetBottomPanelDisplayMode(false);
                    _dlgBottom.Resize += (s, e) => UpdateDebugLayoutInfo();
                    _dlgBottom.FormClosing += (s, e) =>
                    {
                        if (_isBottomPanelFloating)
                        {
                            SetBottomPanelDisplayMode(false);
                        }
                    };
                    _dlgBottom.FormClosed += (s, e) => { _dlgBottom = null; };
                }

                if (panelBottomContent.Controls.Contains(splitContainerBottom))
                {
                    panelBottomContent.Controls.Remove(splitContainerBottom);
                }

                panelBottomHeader.Visible = false;
                panelMapBottom.Visible = false;

                splitContainerBottom.Dock = DockStyle.Fill;
                if (!_dlgBottom.panelBottomContent.Controls.Contains(splitContainerBottom))
                {
                    _dlgBottom.panelBottomContent.Controls.Add(splitContainerBottom);
                }

                _dlgBottom.StartPosition = FormStartPosition.Manual;
                _dlgBottom.Location = targetLoc;
                if (targetSize.Width > 0 && targetSize.Height > 0)
                {
                    _dlgBottom.Size = targetSize;
                }
                _dlgBottom.Show(this);
            }
            else
            {
                if (_dlgBottom != null && !_dlgBottom.IsDisposed)
                {
                    if (_dlgBottom.panelBottomContent.Controls.Contains(splitContainerBottom))
                    {
                        _dlgBottom.panelBottomContent.Controls.Remove(splitContainerBottom);
                    }
                    _dlgBottom.Close();
                    _dlgBottom = null;
                }

                splitContainerBottom.Dock = DockStyle.Fill;
                if (!panelBottomContent.Controls.Contains(splitContainerBottom))
                {
                    panelBottomContent.Controls.Add(splitContainerBottom);
                }

                panelBottomHeader.Visible = true;
                panelMapBottom.Visible = true;
            }

            _vc.InvalidateBoundsCache();
            UpdatePanelWidthsDisplay();
            picMapCanvas.Invalidate();
        }

        private void UpdatePanelWidthsDisplay()
        {
            int formW = this.Width;
            int leftW = panelMapLeft.Visible ? panelMapLeft.Width : 0;
            int rightW = panelMapRight.Visible ? panelMapRight.Width : 0;
            int mapW = picMapCanvas.Width;

            lblMapTitle.Text = $"🗺 2D 測量平面図 (Form: {formW}px | LeftPanel: {leftW}px | MapCanvas: {mapW}px | RightPanel: {rightW}px)";
        }

        private void FormEditor_Load(object? sender, EventArgs e)
        {
            PopulateQuickDbList();
            InitBatchUpdateControls();

            SetBluetoothDisplayMode(false);

            UpdatePanelWidthsDisplay();
        }



        private void PopulateQuickDbList()
        {
            cmbQuickDbSelect.Items.Clear();

            string defaultFolder = @"C:\SITE7\GENBA\DATA";
            if (!Directory.Exists(defaultFolder))
            {
                try { Directory.CreateDirectory(defaultFolder); } catch { }
            }

            var searchFolders = new[] { defaultFolder, @"c:\Proj\Antigravity\MDB2Site7\ExportedSite7" };

            foreach (var folder in searchFolders)
            {
                if (Directory.Exists(folder))
                {
                    var files = Directory.GetFiles(folder, "*.db3", SearchOption.AllDirectories)
                        .Concat(Directory.GetFiles(folder, "*.db", SearchOption.AllDirectories))
                        .Concat(Directory.GetFiles(folder, "*.sqlite", SearchOption.AllDirectories))
                        .Distinct();

                    foreach (var file in files)
                    {
                        string parent = Path.GetFileName(Path.GetDirectoryName(file) ?? "");
                        string fileName = Path.GetFileName(file);
                        string displayName = string.IsNullOrEmpty(parent) ? fileName : $"{parent}\\{fileName}";
                        if (folder == defaultFolder) displayName = $"[GENBA] {displayName}";

                        cmbQuickDbSelect.Items.Add(new DbItem { DisplayName = displayName, FullPath = file });
                    }
                }
            }

            if (cmbQuickDbSelect.Items.Count > 0)
            {
                cmbQuickDbSelect.SelectedIndex = 0;
            }
        }

        private void cmbQuickDbSelect_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbQuickDbSelect.SelectedItem is DbItem item)
            {
                LoadDatabase(item.FullPath);
            }
        }

        private void btnOpenDb_Click(object? sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Site7 SQLite DB (*.db3;*.db;*.sqlite)|*.db3;*.db;*.sqlite|All Files (*.*)|*.*";
                string defaultFolder = @"C:\SITE7\GENBA\DATA";
                if (!Directory.Exists(defaultFolder))
                {
                    try { Directory.CreateDirectory(defaultFolder); } catch { }
                }
                ofd.InitialDirectory = defaultFolder;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadDatabase(ofd.FileName);
                }
            }
        }

        private void LoadDatabase(string dbPath)
        {
            _isLoadingDatabase = true;
            try
            {
                try { dgvIkou.CancelEdit(); } catch { }
                try { dgvIkouL.CancelEdit(); } catch { }
                try { dgvPrecs.CancelEdit(); } catch { }
                try { dgvIbutu.CancelEdit(); } catch { }
                try { dgvKikai.CancelEdit(); } catch { }
                try { dgvLayer.CancelEdit(); } catch { }

                _db.LoadDatabase(dbPath);
                PopulateIkouLineLayerCombo();
                BindAllData();

                lblDbStatus.Text = $"✔ {_db.IkouList.Count}遺構 | {_db.IkouLList.Count}線 | {_db.IbutuList.Count}遺物 | {_db.KikaiList.Count}基準点";
                lblDbStatus.ForeColor = Color.FromArgb(56, 176, 0);

                _vc.ResetZoom();
                picMapCanvas.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB読み込みエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblDbStatus.Text = "✖ 読み込み失敗";
                lblDbStatus.ForeColor = Color.FromArgb(239, 35, 60);
            }
            finally
            {
                _isLoadingDatabase = false;
            }
        }

        private void BindAllData()
        {
            _selectedIkouId = -1;
            _selectedLid = -1;
            _selectedPointIndex = -1;
            _selectedIbutuId = -1;
            _selectedKikaiId = -1;

            _vc.InvalidateBoundsCache();

            dgvIkou.DataSource = null;
            dgvIkouL.DataSource = null;
            dgvPrecs.DataSource = null;
            dgvIbutu.DataSource = null;
            dgvKikai.DataSource = null;
            dgvLayer.DataSource = null;

            dgvIkou.DataSource = _db.IkouList;
            dgvIbutu.DataSource = _db.IbutuList;
            dgvKikai.DataSource = _db.KikaiList;
            dgvLayer.DataSource = _db.LayerList;

            PopulateIkouMasterCombo();
            PopulateIbutuCombos();

            if (dgvIkou.Rows.Count > 0) SetCurrentRowSafe(dgvIkou, 0);
            dgvIkou_SelectionChanged(this, EventArgs.Empty);

            if (dgvIbutu.Rows.Count > 0) SetCurrentRowSafe(dgvIbutu, 0);
            dgvIbutu_SelectionChanged(this, EventArgs.Empty);

            if (dgvKikai.Rows.Count > 0) SetCurrentRowSafe(dgvKikai, 0);
            dgvKikai_SelectionChanged(this, EventArgs.Empty);
        }

        private void PopulateIbutuCombos()
        {
            var chikuSet = new HashSet<string>(_db.IbutuList.Select(i => i.Chiku).Where(s => !string.IsNullOrWhiteSpace(s)));
            chikuSet.Add("F区 東"); chikuSet.Add("A区"); chikuSet.Add("B区"); chikuSet.Add("C区");
            cmbIbutuChiku.Items.Clear();
            cmbIbutuChiku.Items.AddRange(chikuSet.ToArray());
            if (cmbIbutuChiku.Items.Count > 0 && string.IsNullOrEmpty(cmbIbutuChiku.Text)) cmbIbutuChiku.SelectedIndex = 0;

            var souiSet = new HashSet<string>(_db.IbutuList.Select(i => i.Soui).Where(s => !string.IsNullOrWhiteSpace(s)));
            souiSet.Add("黒色土層"); souiSet.Add("褐色土層"); souiSet.Add("表土層");
            cmbIbutuSoui.Items.Clear();
            cmbIbutuSoui.Items.AddRange(souiSet.ToArray());
            if (cmbIbutuSoui.Items.Count > 0 && string.IsNullOrEmpty(cmbIbutuSoui.Text)) cmbIbutuSoui.SelectedIndex = 0;

            var syubetuSet = new HashSet<string>(_db.IbutuList.Select(i => i.Syubetu).Where(s => !string.IsNullOrWhiteSpace(s)));
            syubetuSet.Add("鉄製品"); syubetuSet.Add("土師器"); syubetuSet.Add("須恵器"); syubetuSet.Add("陶磁器"); syubetuSet.Add("石器"); syubetuSet.Add("木製品"); syubetuSet.Add("遺物");
            cmbIbutuSyubetu.Items.Clear();
            cmbIbutuSyubetu.Items.AddRange(syubetuSet.ToArray());
            if (cmbIbutuSyubetu.Items.Count > 0 && string.IsNullOrEmpty(cmbIbutuSyubetu.Text)) cmbIbutuSyubetu.SelectedIndex = 0;
        }

        private void PopulateIkouMasterCombo()
        {
            cmbLineIkouMaster.Items.Clear();
            foreach (var ikou in _db.IkouList)
            {
                cmbLineIkouMaster.Items.Add(new IkouComboItem { Id = ikou.Id, Name = ikou.Name });
            }
        }

        private void PopulateIkouLineLayerCombo()
        {
            cmbLineLayer.Items.Clear();
            for (int i = 1; i <= 16; i++)
            {
                int dbLayerId = 48 + i;
                string layerCode = $"L{i:D2}";
                var matchedLayer = _db.LayerList.FirstOrDefault(l => l.Id == dbLayerId);
                if (matchedLayer != null && !string.IsNullOrEmpty(matchedLayer.Name))
                {
                    layerCode += $" {matchedLayer.Name}";
                }
                cmbLineLayer.Items.Add(layerCode);
            }
            UpdateLayerCheckboxColors();
        }

        private ToolTip? _layerToolTip;

        public void UpdateLayerCheckboxColors()
        {
            if (_chkMapLayers == null) return;
            if (_layerToolTip == null) _layerToolTip = new ToolTip();

            int activeTab = tabControlData.SelectedIndex;
            int baseLayerId = 48; // Default 遺構レイヤ GRP (49..64)
            string groupName = "遺構レイヤGRP";

            if (activeTab == 1) // 遺物
            {
                baseLayerId = 0; // 遺物レイヤ GRP (1..16)
                groupName = "遺物レイヤGRP";
            }
            else if (activeTab == 2) // 基準点
            {
                baseLayerId = 16; // 基準点レイヤ GRP (17..32)
                groupName = "基準点レイヤGRP";
            }

            if (lblIkouLayerGrpHeader != null)
            {
                lblIkouLayerGrpHeader.Text = groupName;
            }

            for (int i = 0; i < 16; i++)
            {
                int dbLayerId = baseLayerId + (i + 1);
                Color col = EditorLayerService.GetControlColor(dbLayerId, _db.LayerList);
                _chkMapLayers[i].ForeColor = col;
                _chkMapLayers[i].Text = $"L{i + 1:D2}";

                var layer = _db.LayerList.FirstOrDefault(l => l.Id == dbLayerId);
                string layerName = layer?.Name ?? "";
                if (!string.IsNullOrEmpty(layerName))
                {
                    _layerToolTip.SetToolTip(_chkMapLayers[i], $"L{i + 1:D2} {layerName}");
                }
                else
                {
                    _layerToolTip.SetToolTip(_chkMapLayers[i], $"L{i + 1:D2}");
                }
            }
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

        public static (string Prefix, string No) ParseIkouName(string name)
        {
            if (string.IsNullOrEmpty(name)) return ("", "");

            int i = name.Length - 1;
            while (i >= 0 && char.IsDigit(name[i]))
            {
                i--;
            }

            string prefix = name.Substring(0, i + 1);
            string no = name.Substring(i + 1);
            return (prefix, no);
        }

        private void UpdateCombinedIkouNameLabel()
        {
            if (_isUpdatingSelection) return;
            string prefix = cmbIkouKind.Text ?? "";
            string no = txtIkouNum.Text ?? "";
            lblIkouNameVal.Text = $"{prefix}{no}";
        }

        private void UpdateCombinedLineNameLabel()
        {
            if (_isUpdatingSelection) return;
            string prefix = cmbLineKind.Text ?? "";
            string no = txtLineNum.Text ?? "";
            lblLineNameVal.Text = $"{prefix}{no}";
        }

        private void dgvIkou_SelectionChanged(object? sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                if (_isUpdatingSelection) return;
                _isUpdatingSelection = true;
                try
                {
                    IkouModel? selectedIkou = GetSelectedDataBoundItem<IkouModel>(dgvIkou);
                    _selectedIkouId = selectedIkou?.Id ?? -1;

                    if (selectedIkou != null)
                    {
                        var (prefix, no) = ParseIkouName(selectedIkou.Name);
                        txtIkouNum.Text = no;
                        lblIkouNameVal.Text = selectedIkou.Name;

                        bool matched = false;
                        foreach (var item in cmbIkouKind.Items)
                        {
                            if (string.Equals(item.ToString(), prefix, StringComparison.OrdinalIgnoreCase))
                            {
                                cmbIkouKind.SelectedItem = item;
                                matched = true;
                                break;
                            }
                        }
                        if (!matched) cmbIkouKind.Text = prefix;

                        var lines = _db.IkouLList.Where(l => l.Id == selectedIkou.Id).ToList();
                        dgvIkouL.DataSource = new BindingList<IkouLModel>(lines);

                        IkouLModel? selectedLine = null;
                        if (lines.Count > 0)
                        {
                            int targetIdx = lines.FindIndex(l => l.Lid == _selectedLid);
                            if (targetIdx < 0) targetIdx = 0;

                            if (targetIdx < dgvIkouL.Rows.Count)
                            {
                                SetCurrentRowSafe(dgvIkouL, targetIdx);
                            }
                            selectedLine = lines[targetIdx];
                        }

                        UpdateIkouLSelection(selectedLine);
                    }
                    else
                    {
                        txtIkouNum.Text = "";
                        cmbIkouKind.SelectedIndex = -1;
                        lblIkouNameVal.Text = "";
                        dgvIkouL.DataSource = new BindingList<IkouLModel>();
                        UpdateIkouLSelection(null);
                    }
                }
                catch { }
                finally
                {
                    _isUpdatingSelection = false;
                    picMapCanvas.Invalidate();
                }
            }));
        }

        private void UpdateIkouLSelection(IkouLModel? lineOverride = null)
        {
            try
            {
                IkouLModel? selectedLine = lineOverride ?? GetSelectedDataBoundItem<IkouLModel>(dgvIkouL);
                _selectedLid = selectedLine?.Lid ?? -1;

                if (selectedLine != null)
                {
                    var (prefix, no) = ParseIkouName(selectedLine.Name);
                    txtLineNum.Text = no;
                    lblLineNameVal.Text = selectedLine.Name;

                    bool matched = false;
                    foreach (var item in cmbLineKind.Items)
                    {
                        if (string.Equals(item.ToString(), prefix, StringComparison.OrdinalIgnoreCase))
                        {
                            cmbLineKind.SelectedItem = item;
                            matched = true;
                            break;
                        }
                    }
                    if (!matched) cmbLineKind.Text = prefix;

                    rdoLineOpen.Checked = (selectedLine.Mode == 0);
                    rdoLineClosed.Checked = (selectedLine.Mode == 1);
                    rdoLinePoint.Checked = (selectedLine.Mode == 2);

                    int rawLayerId = selectedLine.Layer;
                    int userLayerNum = rawLayerId >= 49 ? (rawLayerId - 48) : rawLayerId;
                    if (userLayerNum < 1 || userLayerNum > 16) userLayerNum = 1;
                    string targetCode = $"L{userLayerNum:D2}";

                    bool layerMatched = false;
                    foreach (var item in cmbLineLayer.Items)
                    {
                        if (item.ToString()?.StartsWith(targetCode) == true)
                        {
                            cmbLineLayer.SelectedItem = item;
                            layerMatched = true;
                            break;
                        }
                    }
                    if (!layerMatched && cmbLineLayer.Items.Count > 0) cmbLineLayer.SelectedIndex = 0;

                    long targetIkouId = selectedLine.Id;
                    bool ikouComboMatched = false;
                    foreach (var item in cmbLineIkouMaster.Items)
                    {
                        if (item is IkouComboItem comboItem && comboItem.Id == targetIkouId)
                        {
                            cmbLineIkouMaster.SelectedItem = comboItem;
                            ikouComboMatched = true;
                            break;
                        }
                    }
                    if (!ikouComboMatched) cmbLineIkouMaster.SelectedIndex = -1;

                    var points = SqliteManager.ParsePrecsText(selectedLine.Precs);
                    dgvPrecs.DataSource = new BindingList<IkouPointRecord>(points);
                }
                else
                {
                    txtLineNum.Text = "";
                    cmbLineKind.SelectedIndex = -1;
                    lblLineNameVal.Text = "";
                    cmbLineIkouMaster.SelectedIndex = -1;
                    dgvPrecs.DataSource = new BindingList<IkouPointRecord>();
                }
                _selectedPointIndex = -1;
                dgvPrecs_SelectionChanged(this, EventArgs.Empty);
            }
            catch { }
        }

        private void dgvIkouL_SelectionChanged(object? sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                if (_isUpdatingSelection) return;
                _isUpdatingSelection = true;
                try
                {
                    UpdateIkouLSelection();
                }
                catch { }
                finally
                {
                    _isUpdatingSelection = false;
                    picMapCanvas.Invalidate();
                }
            }));
        }

        private void dgvPrecs_SelectionChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dgvPrecs.SelectedRows.Count > 0 && dgvPrecs.SelectedRows[0].Index >= 0)
                {
                    _selectedPointIndex = dgvPrecs.SelectedRows[0].Index;
                }
                else if (dgvPrecs.CurrentCell != null && dgvPrecs.CurrentCell.RowIndex >= 0)
                {
                    _selectedPointIndex = dgvPrecs.CurrentCell.RowIndex;
                }
                else
                {
                    _selectedPointIndex = -1;
                }

                if (_selectedPointIndex >= 0 && dgvPrecs.DataSource is BindingList<IkouPointRecord> pts && _selectedPointIndex < pts.Count)
                {
                    var pt = pts[_selectedPointIndex];
                    txtCoordX.Text = pt.X.ToString("F3");
                    txtCoordY.Text = pt.Y.ToString("F3");
                    txtCoordZ.Text = pt.Z.ToString("F3");
                }
                else
                {
                    txtCoordX.Text = "";
                    txtCoordY.Text = "";
                    txtCoordZ.Text = "";
                }
                picMapCanvas.Invalidate();
            }
            catch { }
        }

        private void dgvIbutu_SelectionChanged(object? sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    IbutuModel? selectedIbutu = GetSelectedDataBoundItem<IbutuModel>(dgvIbutu);
                    _selectedIbutuId = selectedIbutu?.Id ?? -1;
                    if (selectedIbutu != null)
                    {
                        cmbIbutuChiku.Text = selectedIbutu.Chiku ?? "";
                        cmbIbutuSoui.Text = selectedIbutu.Soui ?? "";
                        cmbIbutuSyubetu.Text = selectedIbutu.Syubetu ?? "";

                        string targetLayer = selectedIbutu.Layer < 10 ? $"L0{selectedIbutu.Layer}" : $"L{selectedIbutu.Layer}";
                        int lIdx = cmbIbutuLayer.FindString(targetLayer);
                        if (lIdx >= 0) cmbIbutuLayer.SelectedIndex = lIdx;
                        else cmbIbutuLayer.Text = $"{targetLayer} 遺物{targetLayer}";

                        txtIbutuNo.Text = selectedIbutu.No.ToString();

                        txtCoordX.Text = selectedIbutu.X.ToString("F3");
                        txtCoordY.Text = selectedIbutu.Y.ToString("F3");
                        txtCoordZ.Text = selectedIbutu.Z.ToString("F3");
                    }
                    else
                    {
                        txtCoordX.Text = "";
                        txtCoordY.Text = "";
                        txtCoordZ.Text = "";
                    }
                    picMapCanvas.Invalidate();
                }
                catch { }
            }));
        }

        private void dgvKikai_SelectionChanged(object? sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    KikaiModel? selectedKikai = GetSelectedDataBoundItem<KikaiModel>(dgvKikai);
                    _selectedKikaiId = selectedKikai?.Id ?? -1;
                    if (selectedKikai != null)
                    {
                        txtKikaiName.Text = selectedKikai.Name ?? "";

                        string targetLayer = selectedKikai.Layer < 10 ? $"L0{selectedKikai.Layer}" : $"L{selectedKikai.Layer}";
                        int lIdx = cmbKikaiLayer.FindString(targetLayer);
                        if (lIdx >= 0) cmbKikaiLayer.SelectedIndex = lIdx;
                        else cmbKikaiLayer.Text = $"{targetLayer} 基準点{targetLayer}";

                        txtCoordX.Text = selectedKikai.X.ToString("F3");
                        txtCoordY.Text = selectedKikai.Y.ToString("F3");
                        txtCoordZ.Text = selectedKikai.Z.ToString("F3");
                    }
                    else
                    {
                        txtCoordX.Text = "";
                        txtCoordY.Text = "";
                        txtCoordZ.Text = "";
                    }
                    picMapCanvas.Invalidate();
                }
                catch { }
            }));
        }

        private void tabControlData_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int idx = tabControlData.SelectedIndex;
            grpCoordValue.Dock = DockStyle.Fill;
            pnlPrecsRight.Dock = DockStyle.Right;
            pnlPrecsRight.Width = 178;

            if (idx == 0) // 遺構
            {
                pnlPrecsRight.Controls.Add(grpCoordValue);
                pnlPrecsRight.Controls.SetChildIndex(grpCoordValue, 0);
            }
            else if (idx == 1) // 遺物
            {
                pnlIbutuRight.Controls.Add(grpCoordValue);
                pnlIbutuRight.Controls.SetChildIndex(grpIbutuRecord, 0);
                pnlIbutuRight.Controls.SetChildIndex(grpCoordValue, 1);
                dgvIbutu_SelectionChanged(this, EventArgs.Empty);
            }
            else if (idx == 2) // 基準点
            {
                pnlKikaiRight.Controls.Add(grpCoordValue);
                pnlKikaiRight.Controls.SetChildIndex(grpKikaiRecord, 0);
                pnlKikaiRight.Controls.SetChildIndex(grpPointGuidance, 1);
                pnlKikaiRight.Controls.SetChildIndex(grpCoordValue, 2);
                dgvKikai_SelectionChanged(this, EventArgs.Empty);
            }
            UpdateLayerCheckboxColors();
            picMapCanvas.Invalidate();
        }

        #region DataGridView Double Click Map Centering

        private void CenterMapOnPoint(double surveyX, double surveyY)
        {
            _vc.UpdateMapBounds(picMapCanvas.ClientSize, _db.IkouLList, _db.IbutuList);
            _vc.CenterOnPoint(surveyX, surveyY, picMapCanvas.ClientSize);
            picMapCanvas.Invalidate();
        }

        private void dgvIkou_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (GetSelectedDataBoundItem<IkouModel>(dgvIkou) is IkouModel selected)
            {
                var lines = _db.IkouLList.Where(l => l.Id == selected.Id).ToList();
                double sumX = 0, sumY = 0;
                int count = 0;
                foreach (var line in lines)
                {
                    var pts = SqliteManager.ParsePrecsText(line.Precs);
                    foreach (var pt in pts)
                    {
                        sumX += pt.X;
                        sumY += pt.Y;
                        count++;
                    }
                }
                if (count > 0)
                {
                    CenterMapOnPoint(sumX / count, sumY / count);
                }
            }
        }

        private void dgvIkouL_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (GetSelectedDataBoundItem<IkouLModel>(dgvIkouL) is IkouLModel selected)
            {
                var pts = SqliteManager.ParsePrecsText(selected.Precs);
                if (pts.Count > 0)
                {
                    double sumX = pts.Average(p => p.X);
                    double sumY = pts.Average(p => p.Y);
                    CenterMapOnPoint(sumX, sumY);
                }
            }
        }

        private void dgvPrecs_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvPrecs.DataSource is BindingList<IkouPointRecord> pts && e.RowIndex < pts.Count)
            {
                var pt = pts[e.RowIndex];
                CenterMapOnPoint(pt.X, pt.Y);
            }
        }

        private void dgvIbutu_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (GetSelectedDataBoundItem<IbutuModel>(dgvIbutu) is IbutuModel selected)
            {
                CenterMapOnPoint(selected.X, selected.Y);
            }
        }

        private void dgvKikai_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (GetSelectedDataBoundItem<KikaiModel>(dgvKikai) is KikaiModel selected)
            {
                CenterMapOnPoint(selected.X, selected.Y);
            }
        }

        #endregion

        #region Real-Time DataGridView Cell Validation & Cascade Updates

        private void dgvIkou_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_isLoadingDatabase || _isUpdatingSelection) return;
            if (e.RowIndex < 0 || e.RowIndex >= dgvIkou.Rows.Count) return;
            var currentItem = dgvIkou.Rows[e.RowIndex].DataBoundItem as IkouModel;
            if (currentItem == null) return;

            string colName = dgvIkou.Columns[e.ColumnIndex].Name;
            string formattedValue = e.FormattedValue?.ToString()?.Trim() ?? "";

            if (colName.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(formattedValue, out long newId))
                {
                    if (_db.IkouList.Any(i => i != currentItem && i.Id == newId))
                    {
                        MessageBox.Show($"入力された 遺構ID [{newId}] は既に他の行で使用されています。\n変更前の値に戻します。", "ID重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvIkou.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
            else if (colName.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(formattedValue))
                {
                    if (_db.IkouList.Any(i => i != currentItem && string.Equals(i.Name.Trim(), formattedValue, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"入力された 遺構名 [{formattedValue}] は既に他の行で使用されています。\n変更前の値に戻します。", "遺構名重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvIkou.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgvIkouL_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_isLoadingDatabase || _isUpdatingSelection) return;
            if (e.RowIndex < 0 || e.RowIndex >= dgvIkouL.Rows.Count) return;
            var currentItem = dgvIkouL.Rows[e.RowIndex].DataBoundItem as IkouLModel;
            if (currentItem == null) return;

            string colName = dgvIkouL.Columns[e.ColumnIndex].Name;
            string formattedValue = e.FormattedValue?.ToString()?.Trim() ?? "";

            if (colName.Equals("Lid", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(formattedValue, out long newLid))
                {
                    if (_db.IkouLList.Any(l => l != currentItem && l.Id == currentItem.Id && l.Lid == newLid))
                    {
                        MessageBox.Show($"入力された 遺構線LID [{newLid}] は対象遺構(ID:{currentItem.Id})内で既に定義されています。\n変更前の値に戻します。", "LID重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvIkouL.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
            else if (colName.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(formattedValue))
                {
                    if (_db.IkouLList.Any(l => l != currentItem && l.Id == currentItem.Id && string.Equals(l.Name.Trim(), formattedValue, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"入力された 遺構線名 [{formattedValue}] は対象遺構(ID:{currentItem.Id})内で既に定義されています。\n変更前の値に戻します。", "遺構線名重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvIkouL.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgvIbutu_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_isLoadingDatabase || _isUpdatingSelection) return;
            if (e.RowIndex < 0 || e.RowIndex >= dgvIbutu.Rows.Count) return;
            var currentItem = dgvIbutu.Rows[e.RowIndex].DataBoundItem as IbutuModel;
            if (currentItem == null) return;

            string colName = dgvIbutu.Columns[e.ColumnIndex].Name;
            string formattedValue = e.FormattedValue?.ToString()?.Trim() ?? "";

            if (colName.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(formattedValue, out long newId))
                {
                    if (_db.IbutuList.Any(i => i != currentItem && i.Id == newId))
                    {
                        MessageBox.Show($"入力された 遺物ID [{newId}] は既に他の行で使用されています。\n変更前の値に戻します。", "ID重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvIbutu.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgvKikai_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_isLoadingDatabase || _isUpdatingSelection) return;
            if (e.RowIndex < 0 || e.RowIndex >= dgvKikai.Rows.Count) return;
            var currentItem = dgvKikai.Rows[e.RowIndex].DataBoundItem as KikaiModel;
            if (currentItem == null) return;

            string colName = dgvKikai.Columns[e.ColumnIndex].Name;
            string formattedValue = e.FormattedValue?.ToString()?.Trim() ?? "";

            if (colName.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(formattedValue, out long newId))
                {
                    if (_db.KikaiList.Any(k => k != currentItem && k.Id == newId))
                    {
                        MessageBox.Show($"入力された 基準点ID [{newId}] は既に他の行で使用されています。\n変更前の値に戻します。", "ID重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvKikai.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
            else if (colName.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(formattedValue))
                {
                    if (_db.KikaiList.Any(k => k != currentItem && string.Equals(k.Name.Trim(), formattedValue, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"入力された 基準点名 [{formattedValue}] は既に他の行で使用されています。\n変更前の値に戻します。", "基準点名重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvKikai.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgvLayer_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_isLoadingDatabase || _isUpdatingSelection) return;
            if (e.RowIndex < 0 || e.RowIndex >= dgvLayer.Rows.Count) return;
            var currentItem = dgvLayer.Rows[e.RowIndex].DataBoundItem as LayerModel;
            if (currentItem == null) return;

            string colName = dgvLayer.Columns[e.ColumnIndex].Name;
            string formattedValue = e.FormattedValue?.ToString()?.Trim() ?? "";

            if (colName.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(formattedValue, out int newId))
                {
                    if (_db.LayerList.Any(l => l != currentItem && l.Id == newId))
                    {
                        MessageBox.Show($"入力された レイヤーID [{newId}] は既に他の行で使用されています。\n変更前の値に戻します。", "ID重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvLayer.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
            else if (colName.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(formattedValue))
                {
                    if (_db.LayerList.Any(l => l != currentItem && string.Equals(l.Name.Trim(), formattedValue, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"入力された レイヤー名 [{formattedValue}] は既に他の行で使用されています。\n変更前の値に戻します。", "レイヤー名重複エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvLayer.CancelEdit();
                        e.Cancel = true;
                    }
                }
            }
        }

        #endregion

        private void dgvPrecs_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (GetSelectedDataBoundItem<IkouLModel>(dgvIkouL) is IkouLModel selectedLine && dgvPrecs.DataSource is BindingList<IkouPointRecord> pts)
            {
                selectedLine.Precs = SqliteManager.FormatPrecsText(pts.ToList());

                if (pts.Count > 0)
                {
                    selectedLine.X = Math.Round(pts.Average(p => p.X), 3);
                    selectedLine.Y = Math.Round(pts.Average(p => p.Y), 3);
                    selectedLine.Z = Math.Round(pts.Average(p => p.Z), 3);

                    if (pts.Count > 1)
                    {
                        var f = pts.First();
                        var l = pts.Last();
                        bool match = (Math.Abs(f.X - l.X) < 0.0015 && Math.Abs(f.Y - l.Y) < 0.0015 && Math.Abs(f.Z - l.Z) < 0.010);
                        selectedLine.Mode = match ? 1 : 0;
                    }
                    dgvIkouL.Refresh();
                }

                if (_selectedPointIndex >= 0 && _selectedPointIndex < pts.Count)
                {
                    var pt = pts[_selectedPointIndex];
                    txtCoordX.Text = pt.X.ToString("F3");
                    txtCoordY.Text = pt.Y.ToString("F3");
                    txtCoordZ.Text = pt.Z.ToString("F3");
                }

                picMapCanvas.Invalidate();
            }
        }

        private void txtFilterIbutu_TextChanged(object? sender, EventArgs e)
        {
            string keyword = txtFilterIbutu.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                dgvIbutu.DataSource = _db.IbutuList;
            }
            else
            {
                var filtered = _db.IbutuList.Where(i =>
                    i.Chiku.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    i.Soui.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    i.Syubetu.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    i.No.ToString().Contains(keyword)).ToList();
                dgvIbutu.DataSource = new BindingList<IbutuModel>(filtered);
            }
        }

        private void btnAddIkou_Click(object? sender, EventArgs e)
        {
            long newId = _db.IkouList.Count > 0 ? _db.IkouList.Max(i => i.Id) + 1 : 1;

            string prefix = cmbIkouKind.Text.Trim();
            if (string.IsNullOrEmpty(prefix)) prefix = "遺構";

            string noStr = txtIkouNum.Text.Trim();
            if (!int.TryParse(noStr, out int numVal))
            {
                int maxVal = 0;
                foreach (var ikou in _db.IkouList)
                {
                    var (p, n) = ParseIkouName(ikou.Name);
                    if (string.Equals(p, prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        if (int.TryParse(n, out int val) && val > maxVal) maxVal = val;
                    }
                }
                numVal = maxVal + 1;
            }

            string fullName = prefix.Equals("遺構", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(txtIkouNum.Text.Trim())
                ? $"遺構_{newId}"
                : (numVal < 100 ? $"{prefix}{numVal:D2}" : $"{prefix}{numVal}");

            int attempts = 0;
            while (_db.IkouList.Any(i => string.Equals(i.Name.Trim(), fullName, StringComparison.OrdinalIgnoreCase)) && attempts < 100)
            {
                numVal++;
                fullName = numVal < 100 ? $"{prefix}{numVal:D2}" : $"{prefix}{numVal}";
                attempts++;
            }

            var newItem = new IkouModel { Id = newId, Name = fullName, Date = DateTime.Now.ToString("yyyy/MM/dd") };
            _db.IkouList.Add(newItem);

            PopulateIkouMasterCombo();
            SelectRowInDgv<IkouModel>(dgvIkou, item => item.Id == newId);
        }

        private void btnDeleteIkou_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<IkouModel>(dgvIkou) is IkouModel selected)
            {
                for (int i = _db.IkouLList.Count - 1; i >= 0; i--)
                {
                    if (_db.IkouLList[i].Id == selected.Id) _db.IkouLList.RemoveAt(i);
                }
                _db.IkouList.Remove(selected);
                dgvIkou_SelectionChanged(this, EventArgs.Empty);
            }
        }

        private void btnAddIkouL_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<IkouModel>(dgvIkou) is IkouModel selectedIkou)
            {
                var existingLines = _db.IkouLList.Where(l => l.Id == selectedIkou.Id).ToList();
                long newLid = existingLines.Count > 0 ? existingLines.Max(l => l.Lid) + 1 : 1;

                string prefix = cmbLineKind.Text.Trim();
                if (string.IsNullOrEmpty(prefix)) prefix = "線";

                string noStr = txtLineNum.Text.Trim();
                if (!int.TryParse(noStr, out int numVal))
                {
                    int maxVal = 0;
                    foreach (var line in existingLines)
                    {
                        var (p, n) = ParseIkouName(line.Name);
                        if (string.Equals(p, prefix, StringComparison.OrdinalIgnoreCase))
                        {
                            if (int.TryParse(n, out int val) && val > maxVal) maxVal = val;
                        }
                    }
                    numVal = maxVal + 1;
                }

                string fullName = prefix.Equals("線", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(txtLineNum.Text.Trim())
                    ? $"線_{newLid}"
                    : $"{prefix}{numVal}";

                int attempts = 0;
                while (existingLines.Any(l => string.Equals(l.Name.Trim(), fullName, StringComparison.OrdinalIgnoreCase)) && attempts < 100)
                {
                    numVal++;
                    fullName = $"{prefix}{numVal}";
                    attempts++;
                }

                int modeVal = rdoLineClosed.Checked ? 1 : (rdoLinePoint.Checked ? 2 : 0);

                int selectedIdx = cmbLineLayer.SelectedIndex;
                int layerVal = 1;
                if (selectedIdx >= 0 && selectedIdx < 16)
                {
                    layerVal = selectedIdx + 1;
                }
                else
                {
                    string selLayer = cmbLineLayer.SelectedItem?.ToString() ?? cmbLineLayer.Text;
                    if (selLayer.StartsWith("L") && int.TryParse(selLayer.Substring(1, 2), out int parsedLayer))
                    {
                        layerVal = parsedLayer >= 49 ? (parsedLayer - 48) : parsedLayer;
                    }
                }

                var newLine = new IkouLModel
                {
                    Id = selectedIkou.Id,
                    Lid = newLid,
                    Name = fullName,
                    Mode = modeVal,
                    Layer = layerVal,
                    Date = DateTime.Now.ToString("yyyy/MM/dd"),
                    Precs = ""
                };
                _db.IkouLList.Add(newLine);

                dgvIkou_SelectionChanged(this, EventArgs.Empty);
                SelectRowInDgv<IkouLModel>(dgvIkouL, item => item.Lid == newLid);
            }
            else
            {
                MessageBox.Show("対象の遺構を選択してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteIkouL_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<IkouLModel>(dgvIkouL) is IkouLModel selected)
            {
                _db.IkouLList.Remove(selected);
                dgvIkou_SelectionChanged(this, EventArgs.Empty);
            }
        }

        private void btnAddLayer_Click(object? sender, EventArgs e)
        {
            int newId = _db.LayerList.Count > 0 ? _db.LayerList.Max(l => l.Id) + 1 : 1;
            var newItem = new LayerModel { Id = newId, Name = $"L{newId:D2}" };
            _db.LayerList.Add(newItem);
        }

        private void btnDeleteLayer_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<LayerModel>(dgvLayer) is LayerModel selected)
            {
                _db.LayerList.Remove(selected);
            }
        }

        #region Right Edit Control Handlers

        private void btnView3D_Click(object? sender, EventArgs e)
        {
            if (_selectedIkouId <= 0)
            {
                var selIkou = GetSelectedDataBoundItem<IkouModel>(dgvIkou);
                if (selIkou != null)
                {
                    _selectedIkouId = selIkou.Id;
                }
                else if (_db.IkouList.Count > 0)
                {
                    _selectedIkouId = _db.IkouList[0].Id;
                }
            }

            if (_selectedIkouId <= 0 && _db.IkouLList.Count > 0)
            {
                var selLine = GetSelectedDataBoundItem<IkouLModel>(dgvIkouL);
                var targetLine = selLine ?? _db.IkouLList[0];
                _selectedIkouId = targetLine.Id;
            }

            if (_selectedIkouId <= 0)
            {
                MessageBox.Show("3D表示対象の遺構を選択してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var targetIkou = _db.IkouList.FirstOrDefault(i => i.Id == _selectedIkouId);
            if (targetIkou == null)
            {
                MessageBox.Show("選択された遺構データが見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var matchingLines = _db.IkouLList.Where(l =>
                (!string.IsNullOrEmpty(l.Name) && !string.IsNullOrEmpty(targetIkou.Name) && l.Name == targetIkou.Name) ||
                l.Id == targetIkou.Id).ToList();

            if (matchingLines.Count == 0)
            {
                matchingLines = _db.IkouLList.ToList();
            }

            if (matchingLines.Count == 0)
            {
                MessageBox.Show("3D表示に必要な測量点データ（遺構L）が存在しません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var drawingIkou = new Site7DrawingEditor.DrawingIkouModel
            {
                IID = (int)targetIkou.Id,
                Name = targetIkou.Name ?? $"Ikou{targetIkou.Id}"
            };

            double minX = double.MaxValue, minY = double.MaxValue, minZ = double.MaxValue;
            double maxX = double.MinValue, maxY = double.MinValue, maxZ = double.MinValue;

            foreach (var line in matchingLines)
            {
                var pts = SqliteManager.ParsePrecsText(line.Precs);
                if (pts.Count == 0) continue;

                var zPnts = new List<Site7DrawingEditor.Point3D>();
                foreach (var p in pts)
                {
                    zPnts.Add(new Site7DrawingEditor.Point3D(p.X, p.Y, p.Z));

                    if (p.X < minX) minX = p.X;
                    if (p.Y < minY) minY = p.Y;
                    if (p.Z < minZ) minZ = p.Z;
                    if (p.X > maxX) maxX = p.X;
                    if (p.Y > maxY) maxY = p.Y;
                    if (p.Z > maxZ) maxZ = p.Z;
                }

                var zRec = new Site7DrawingEditor.ZIkouLRec((int)line.Lid, line.Layer, line.Mode == 1 ? 1 : 0, zPnts);
                drawingIkou.LList.Add(zRec);
            }

            if (minX == double.MaxValue)
            {
                minX = 0; minY = 0; minZ = 0;
                maxX = 100; maxY = 100; maxZ = 10;
            }

            double rangeX = Math.Max(0.5, maxX - minX);
            double rangeY = Math.Max(0.5, maxY - minY);
            double padX = rangeX * 0.125;
            double padY = rangeY * 0.125;

            drawingIkou.P1 = new Site7DrawingEditor.XYZ { X = minX - padX, Y = minY - padY, Z = minZ };
            drawingIkou.P2 = new Site7DrawingEditor.XYZ { X = maxX + padX, Y = minY - padY, Z = minZ };
            drawingIkou.P3 = new Site7DrawingEditor.XYZ { X = maxX + padX, Y = maxY + padY, Z = maxZ };

            using (var dlg3D = new Site7DrawingEditor.FormIkou3D(drawingIkou))
            {
                dlg3D.SwitchViewMode(true);
                dlg3D.ShowDialog(this);
            }
        }

        private void btnMaxPlusOne_Click(object? sender, EventArgs e)
        {
            string currentPrefix = cmbIkouKind.Text.Trim();
            int maxVal = 0;

            foreach (var ikou in _db.IkouList)
            {
                var (p, n) = ParseIkouName(ikou.Name);
                if (string.Equals(p, currentPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(n, out int val))
                    {
                        if (val > maxVal) maxVal = val;
                    }
                }
            }

            int nextVal = maxVal + 1;
            txtIkouNum.Text = nextVal < 100 ? nextVal.ToString("D2") : nextVal.ToString();
        }

        private void btnUpdateIkouRight_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<IkouModel>(dgvIkou) is IkouModel selected)
            {
                string prefix = cmbIkouKind.Text.Trim();
                string noStr = txtIkouNum.Text.Trim();
                string fullName = $"{prefix}{noStr}";

                selected.Name = fullName;
                lblIkouNameVal.Text = fullName;
                dgvIkou.Refresh();
                picMapCanvas.Invalidate();
            }
        }

        private void btnLineMaxPlusOne_Click(object? sender, EventArgs e)
        {
            string currentPrefix = cmbLineKind.Text.Trim();
            int maxVal = 0;

            foreach (var line in _db.IkouLList)
            {
                if (line.Id == _selectedIkouId)
                {
                    var (p, n) = ParseIkouName(line.Name);
                    if (string.Equals(p, currentPrefix, StringComparison.OrdinalIgnoreCase))
                    {
                        if (int.TryParse(n, out int val))
                        {
                            if (val > maxVal) maxVal = val;
                        }
                    }
                }
            }

            int nextVal = maxVal + 1;
            txtLineNum.Text = nextVal.ToString();
        }

        private void btnUpdateLineRight_Click(object? sender, EventArgs e)
        {
            if (GetSelectedDataBoundItem<IkouLModel>(dgvIkouL) is IkouLModel selected)
            {
                string prefix = cmbLineKind.Text.Trim();
                string noStr = txtLineNum.Text.Trim();
                string fullName = $"{prefix}{noStr}";

                selected.Name = fullName;
                lblLineNameVal.Text = fullName;

                if (rdoLineOpen.Checked) selected.Mode = 0;
                else if (rdoLineClosed.Checked) selected.Mode = 1;
                else if (rdoLinePoint.Checked) selected.Mode = 2;

                int selectedIdx = cmbLineLayer.SelectedIndex;
                if (selectedIdx >= 0 && selectedIdx < 16)
                {
                    selected.Layer = selectedIdx + 1;
                }
                else
                {
                    string selLayer = cmbLineLayer.SelectedItem?.ToString() ?? "";
                    if (selLayer.StartsWith("L") && int.TryParse(selLayer.Substring(1, 2), out int parsedLayer))
                    {
                        selected.Layer = parsedLayer >= 49 ? (parsedLayer - 48) : parsedLayer;
                    }
                }

                dgvIkouL.Refresh();
                picMapCanvas.Invalidate();
            }
        }

        private int GetSelectedIbutuLayer()
        {
            string text = cmbIbutuLayer.SelectedItem?.ToString() ?? cmbIbutuLayer.Text;
            if (text.StartsWith("L") && int.TryParse(text.Substring(1, 2), out int layerVal))
            {
                return layerVal;
            }
            return 5;
        }

        private void btnIbutuMaxPlusOne_Click(object? sender, EventArgs e)
        {
            int maxNo = 0;
            foreach (var ib in _db.IbutuList)
            {
                if (ib.No > maxNo) maxNo = ib.No;
            }
            txtIbutuNo.Text = (maxNo + 1).ToString();
        }

        private int GetSelectedKikaiLayer()
        {
            string text = cmbKikaiLayer.SelectedItem?.ToString() ?? cmbKikaiLayer.Text;
            if (text.StartsWith("L") && int.TryParse(text.Substring(1, 2), out int layerVal))
            {
                return layerVal;
            }
            return 1;
        }

        private void btnUpdatePointRight_Click(object? sender, EventArgs e)
        {
            int tabIdx = tabControlData.SelectedIndex;
            double.TryParse(txtCoordX.Text.Trim(), out double x);
            double.TryParse(txtCoordY.Text.Trim(), out double y);
            double.TryParse(txtCoordZ.Text.Trim(), out double z);

            if (tabIdx == 0) // 遺構
            {
                if (_selectedPointIndex >= 0 && dgvPrecs.DataSource is BindingList<IkouPointRecord> pts && _selectedPointIndex < pts.Count)
                {
                    pts[_selectedPointIndex].X = x;
                    pts[_selectedPointIndex].Y = y;
                    pts[_selectedPointIndex].Z = z;

                    dgvPrecs.Refresh();
                    dgvPrecs_CellValueChanged(this, new DataGridViewCellEventArgs(0, _selectedPointIndex));
                }
            }
            else if (tabIdx == 1) // 遺物
            {
                if (GetSelectedDataBoundItem<IbutuModel>(dgvIbutu) is IbutuModel selected)
                {
                    selected.Chiku = cmbIbutuChiku.Text.Trim();
                    selected.Soui = cmbIbutuSoui.Text.Trim();
                    selected.Syubetu = cmbIbutuSyubetu.Text.Trim();
                    selected.Layer = GetSelectedIbutuLayer();
                    if (int.TryParse(txtIbutuNo.Text.Trim(), out int noVal)) selected.No = noVal;

                    selected.X = x;
                    selected.Y = y;
                    selected.Z = z;
                    dgvIbutu.Refresh();
                    picMapCanvas.Invalidate();
                }
            }
            else if (tabIdx == 2) // 基準点
            {
                if (GetSelectedDataBoundItem<KikaiModel>(dgvKikai) is KikaiModel selected)
                {
                    selected.Name = txtKikaiName.Text.Trim();
                    selected.Layer = GetSelectedKikaiLayer();
                    selected.X = x;
                    selected.Y = y;
                    selected.Z = z;
                    dgvKikai.Refresh();
                    picMapCanvas.Invalidate();
                }
            }
        }

        private void btnDeletePointRight_Click(object? sender, EventArgs e)
        {
            int tabIdx = tabControlData.SelectedIndex;
            if (tabIdx == 0) // 遺構 (構成座標)
            {
                if (_selectedPointIndex >= 0 && dgvPrecs.DataSource is BindingList<IkouPointRecord> pts && _selectedPointIndex < pts.Count)
                {
                    pts.RemoveAt(_selectedPointIndex);
                    dgvPrecs.Refresh();
                    dgvPrecs_CellValueChanged(this, new DataGridViewCellEventArgs(0, 0));
                }
            }
            else if (tabIdx == 1) // 遺物
            {
                if (GetSelectedDataBoundItem<IbutuModel>(dgvIbutu) is IbutuModel selected)
                {
                    _db.IbutuList.Remove(selected);
                    dgvIbutu_SelectionChanged(this, EventArgs.Empty);
                    picMapCanvas.Invalidate();
                }
            }
            else if (tabIdx == 2) // 基準点
            {
                if (GetSelectedDataBoundItem<KikaiModel>(dgvKikai) is KikaiModel selected)
                {
                    _db.KikaiList.Remove(selected);
                    dgvKikai_SelectionChanged(this, EventArgs.Empty);
                    picMapCanvas.Invalidate();
                }
            }
        }

        private void btnAddPointRight_Click(object? sender, EventArgs e)
        {
            int tabIdx = tabControlData.SelectedIndex;
            double.TryParse(txtCoordX.Text.Trim(), out double x);
            double.TryParse(txtCoordY.Text.Trim(), out double y);
            double.TryParse(txtCoordZ.Text.Trim(), out double z);

            if (tabIdx == 0) // 遺構 (構成座標)
            {
                if (GetSelectedDataBoundItem<IkouLModel>(dgvIkouL) is IkouLModel selectedLine)
                {
                    var pts = SqliteManager.ParsePrecsText(selectedLine.Precs);

                    int nextPid = pts.Count > 0 ? pts.Max(p => p.Pid) + 1 : 1;
                    pts.Add(new IkouPointRecord
                    {
                        Pid = nextPid,
                        X = x,
                        Y = y,
                        Z = z
                    });

                    selectedLine.Precs = SqliteManager.FormatPrecsText(pts);
                    dgvPrecs.DataSource = new BindingList<IkouPointRecord>(pts);

                    int newIndex = pts.Count - 1;
                    _selectedPointIndex = newIndex;
                    if (newIndex >= 0 && newIndex < dgvPrecs.Rows.Count)
                    {
                        SetCurrentRowSafe(dgvPrecs, newIndex);
                    }

                    picMapCanvas.Invalidate();
                }
                else
                {
                    MessageBox.Show("対象の遺構線を選択してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (tabIdx == 1) // 遺物
            {
                long newId = _db.IbutuList.Count > 0 ? _db.IbutuList.Max(i => i.Id) + 1 : 1;
                int.TryParse(txtIbutuNo.Text.Trim(), out int targetNo);
                if (targetNo <= 0) targetNo = 1;

                var newItem = new IbutuModel
                {
                    Id = newId,
                    Chiku = cmbIbutuChiku.Text.Trim(),
                    Soui = cmbIbutuSoui.Text.Trim(),
                    Syubetu = cmbIbutuSyubetu.Text.Trim(),
                    Layer = GetSelectedIbutuLayer(),
                    No = targetNo,
                    X = x,
                    Y = y,
                    Z = z,
                    Date = DateTime.Now.ToString("yyyy/MM/dd")
                };
                _db.IbutuList.Add(newItem);

                if (chkIbutuAutoInc.Checked)
                {
                    txtIbutuNo.Text = (targetNo + 1).ToString();
                }

                PopulateIbutuCombos();
                SelectRowInDgv<IbutuModel>(dgvIbutu, item => item.Id == newId);
                picMapCanvas.Invalidate();
            }
            else if (tabIdx == 2) // 基準点
            {
                long newId = _db.KikaiList.Count > 0 ? _db.KikaiList.Max(k => k.Id) + 1 : 1;
                string kName = txtKikaiName.Text.Trim();
                if (string.IsNullOrEmpty(kName)) kName = $"P{newId:D2}";

                var newItem = new KikaiModel
                {
                    Id = newId,
                    Name = kName,
                    Layer = GetSelectedKikaiLayer(),
                    X = x,
                    Y = y,
                    Z = z,
                    Date = DateTime.Now.ToString("yyyy/MM/dd")
                };
                _db.KikaiList.Add(newItem);

                SelectRowInDgv<KikaiModel>(dgvKikai, item => item.Id == newId);
                picMapCanvas.Invalidate();
            }
        }

        #endregion

        #region Save DB Operation

        private void btnSaveDb_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_db.CurrentDbPath) || !File.Exists(_db.CurrentDbPath))
            {
                MessageBox.Show("保存先のSQLite DBが正しく読み込まれていません。", "保存エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"現在の編集内容を DBファイル [{Path.GetFileName(_db.CurrentDbPath)}] に上書き保存しますか？",
                "SQLite DB保存確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _db.SaveDatabase(_db.CurrentDbPath);
                MessageBox.Show("✔ SQLite DB に正常に上書き保存しました！", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB保存時にエラーが発生しました: {ex.Message}", "保存エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 2D Map Rendering & Mouse Events

        private bool _isDarkMapBackground = true;
        private CheckBox[]? _chkMapLayers = null;

        private bool IsMapLayerVisible(int layerId)
        {
            if (_chkMapLayers == null || _chkMapLayers.Length == 0) return true;
            int idx = (layerId - 1) % 16;
            if (idx < 0) idx += 16;
            return _chkMapLayers[idx].Checked;
        }

        private void picMapCanvas_Paint(object? sender, PaintEventArgs e)
        {
            EditorMapRenderer.DrawMapCanvas(
                e.Graphics,
                picMapCanvas.Size,
                _vc,
                _db,
                _selectedIkouId,
                _selectedLid,
                _selectedPointIndex,
                _selectedIbutuId,
                _selectedKikaiId,
                tabControlData.SelectedIndex,
                chkShowIkou.Checked,
                chkShowIbutu.Checked,
                chkShowKikai.Checked,
                chkShowCurve.Checked,
                chkShowGrid.Checked,
                chkColorByIkou.Checked,
                isLayerVisible: IsMapLayerVisible,
                showIkouName: chkShowIkouName.Checked,
                showIbutuName: chkShowIbutuName.Checked,
                showKikaiName: chkShowKikaiName.Checked,
                isDarkBackground: _isDarkMapBackground);
        }

        private void picMapCanvas_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                _vc.IsMouseDownMap = true;
                _vc.IsPanningMap = false;
                _vc.MouseDownPosMap = e.Location;
                _vc.LastMousePosMap = e.Location;

                if (chkScreenInput.Checked && e.Button == MouseButtons.Left)
                {
                    var (clickX, clickY) = _vc.CanvasToSurvey(e.Location, picMapCanvas.Size);
                    txtCoordX.Text = clickX.ToString("F3");
                    txtCoordY.Text = clickY.ToString("F3");
                }
            }
        }

        private void picMapCanvas_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_vc.IsMouseDownMap)
            {
                int dx = e.X - _vc.MouseDownPosMap.X;
                int dy = e.Y - _vc.MouseDownPosMap.Y;
                if (!_vc.IsPanningMap && (Math.Abs(dx) > 3 || Math.Abs(dy) > 3))
                {
                    _vc.IsPanningMap = true;
                    picMapCanvas.Cursor = Cursors.SizeAll;
                }

                if (_vc.IsPanningMap)
                {
                    int mdx = e.X - _vc.LastMousePosMap.X;
                    int mdy = e.Y - _vc.LastMousePosMap.Y;
                    _vc.PanOffsetMap = new PointF(_vc.PanOffsetMap.X + mdx, _vc.PanOffsetMap.Y + mdy);
                    _vc.LastMousePosMap = e.Location;
                    picMapCanvas.Invalidate();
                }
            }
        }

        private void picMapCanvas_MouseUp(object? sender, MouseEventArgs e)
        {
            bool wasPanning = _vc.IsPanningMap;
            _vc.IsMouseDownMap = false;
            if (_vc.IsPanningMap)
            {
                _vc.IsPanningMap = false;
                picMapCanvas.Cursor = Cursors.Default;
            }

            if (!wasPanning && e.Button == MouseButtons.Left && !chkScreenInput.Checked)
            {
                int dx = e.X - _vc.MouseDownPosMap.X;
                int dy = e.Y - _vc.MouseDownPosMap.Y;
                if (Math.Abs(dx) <= 5 && Math.Abs(dy) <= 5)
                {
                    PerformMapHitTest(e.Location);
                }
            }
        }

        private void PerformMapHitTest(Point clickPos)
        {
            if (picMapCanvas.Width <= 0 || picMapCanvas.Height <= 0) return;

            PointF ToCanvasPointLocal(double surveyX, double surveyY)
            {
                return _vc.ToCanvasPoint(surveyX, surveyY, picMapCanvas.Size);
            }

            double thresholdPx = 14.0;
            int activeTab = tabControlData.SelectedIndex; // 0: 遺構, 1: 遺物, 2: 基準点

            if (activeTab == 1 && chkShowIbutu.Checked && HitTestIbutu(clickPos, ToCanvasPointLocal, thresholdPx)) return;
            if (activeTab == 2 && chkShowKikai.Checked && HitTestKikai(clickPos, ToCanvasPointLocal, thresholdPx)) return;
            if (activeTab == 0 && chkShowIkou.Checked && HitTestIkou(clickPos, ToCanvasPointLocal, thresholdPx)) return;

            if (chkShowIkou.Checked && HitTestIkou(clickPos, ToCanvasPointLocal, thresholdPx)) return;
            if (chkShowIbutu.Checked && HitTestIbutu(clickPos, ToCanvasPointLocal, thresholdPx)) return;
            if (chkShowKikai.Checked && HitTestKikai(clickPos, ToCanvasPointLocal, thresholdPx)) return;
        }

        private bool HitTestIbutu(Point clickPos, Func<double, double, PointF> toCanvasPoint, double thresholdPx)
        {
            IbutuModel? bestIbutu = null;
            double minDist = thresholdPx;

            foreach (var ibutu in _db.IbutuList)
            {
                PointF p = toCanvasPoint(ibutu.X, ibutu.Y);
                double d = Math.Sqrt((p.X - clickPos.X) * (p.X - clickPos.X) + (p.Y - clickPos.Y) * (p.Y - clickPos.Y));
                if (d < minDist)
                {
                    minDist = d;
                    bestIbutu = ibutu;
                }
            }

            if (bestIbutu != null)
            {
                _isUpdatingSelection = true;
                try
                {
                    tabControlData.SelectedIndex = 1;
                    SelectRowInDgv<IbutuModel>(dgvIbutu, item => item.Id == bestIbutu.Id);
                    _selectedIbutuId = bestIbutu.Id;
                }
                finally
                {
                    _isUpdatingSelection = false;
                    picMapCanvas.Invalidate();
                }
                return true;
            }
            return false;
        }

        private bool HitTestKikai(Point clickPos, Func<double, double, PointF> toCanvasPoint, double thresholdPx)
        {
            KikaiModel? bestKikai = null;
            double minDist = thresholdPx;

            foreach (var kikai in _db.KikaiList)
            {
                PointF p = toCanvasPoint(kikai.X, kikai.Y);
                double d = Math.Sqrt((p.X - clickPos.X) * (p.X - clickPos.X) + (p.Y - clickPos.Y) * (p.Y - clickPos.Y));
                if (d < minDist)
                {
                    minDist = d;
                    bestKikai = kikai;
                }
            }

            if (bestKikai != null)
            {
                _isUpdatingSelection = true;
                try
                {
                    tabControlData.SelectedIndex = 2;
                    SelectRowInDgv<KikaiModel>(dgvKikai, item => item.Id == bestKikai.Id);
                    _selectedKikaiId = bestKikai.Id;
                }
                finally
                {
                    _isUpdatingSelection = false;
                    picMapCanvas.Invalidate();
                }
                return true;
            }
            return false;
        }

        private bool HitTestIkou(Point clickPos, Func<double, double, PointF> toCanvasPoint, double thresholdPx)
        {
            IkouLModel? bestLine = null;
            int bestVertexIndex = -1;
            double minDist = thresholdPx;
            var spline = new Xross_Spline();

            foreach (var line in _db.IkouLList)
            {
                var pts = SqliteManager.ParsePrecsText(line.Precs);
                if (pts.Count == 0) continue;

                for (int i = 0; i < pts.Count; i++)
                {
                    PointF vp = toCanvasPoint(pts[i].X, pts[i].Y);
                    double vd = Math.Sqrt((vp.X - clickPos.X) * (vp.X - clickPos.X) + (vp.Y - clickPos.Y) * (vp.Y - clickPos.Y));
                    if (vd < minDist)
                    {
                        minDist = vd;
                        bestLine = line;
                        bestVertexIndex = i;
                    }
                }

                PointF[] screenPts;
                if (chkShowCurve.Checked && pts.Count >= 3)
                {
                    var curve = (line.Mode == 1) ? spline.Calc3DCloseCurvePoints(pts, 5) : spline.Calc3DCurvePoints(pts, 5);
                    screenPts = curve.Select(p => toCanvasPoint(p.X, p.Y)).ToArray();
                }
                else
                {
                    screenPts = pts.Select(p => toCanvasPoint(p.X, p.Y)).ToArray();
                }

                for (int i = 0; i < screenPts.Length - 1; i++)
                {
                    double sd = EditorMapViewController.DistanceToLineSegment(clickPos, screenPts[i], screenPts[i + 1]);
                    if (sd < minDist)
                    {
                        minDist = sd;
                        bestLine = line;
                    }
                }
            }

            if (bestLine != null)
            {
                _isUpdatingSelection = true;
                try
                {
                    tabControlData.SelectedIndex = 0;
                    _selectedIkouId = bestLine.Id;
                    _selectedLid = bestLine.Lid;

                    SelectRowInDgv<IkouModel>(dgvIkou, item => item.Id == bestLine.Id);

                    var linesForIkou = _db.IkouLList.Where(l => l.Id == bestLine.Id).ToList();
                    dgvIkouL.DataSource = new BindingList<IkouLModel>(linesForIkou);

                    SelectRowInDgv<IkouLModel>(dgvIkouL, item => item.Lid == bestLine.Lid);

                    UpdateIkouLSelection();

                    var pointsForLine = SqliteManager.ParsePrecsText(bestLine.Precs);
                    dgvPrecs.DataSource = new BindingList<IkouPointRecord>(pointsForLine);

                    if (bestVertexIndex >= 0 && bestVertexIndex < pointsForLine.Count)
                    {
                        _selectedPointIndex = bestVertexIndex;
                        SetCurrentRowSafe(dgvPrecs, bestVertexIndex);
                    }
                    else
                    {
                        _selectedPointIndex = -1;
                    }
                }
                finally
                {
                    _isUpdatingSelection = false;
                    picMapCanvas.Invalidate();
                }
                return true;
            }
            return false;
        }

        private static void SetCurrentRowSafe(DataGridView dgv, int rowIndex)
        {
            if (dgv == null || rowIndex < 0 || rowIndex >= dgv.Rows.Count) return;
            try
            {
                dgv.ClearSelection();
                dgv.Rows[rowIndex].Selected = true;
                foreach (DataGridViewCell cell in dgv.Rows[rowIndex].Cells)
                {
                    if (cell.Visible)
                    {
                        dgv.CurrentCell = cell;
                        break;
                    }
                }
            }
            catch { }
        }

        private static void SelectRowInDgv<T>(DataGridView dgv, Func<T, bool> predicate) where T : class
        {
            if (dgv == null || dgv.Rows.Count == 0) return;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].DataBoundItem is T item && predicate(item))
                {
                    SetCurrentRowSafe(dgv, i);
                    break;
                }
            }
        }

        private void picMapCanvas_MouseWheel(object? sender, MouseEventArgs e)
        {
            float oldZoom = _vc.ZoomFactorMap;
            float scaleFactor = e.Delta > 0 ? 1.15f : (1.0f / 1.15f);
            float newZoom = Math.Clamp(oldZoom * scaleFactor, 0.1f, 30.0f);

            if (Math.Abs(newZoom - oldZoom) > 0.0001f)
            {
                float ratio = newZoom / oldZoom;
                float cx = picMapCanvas.Width / 2f;
                float cy = picMapCanvas.Height / 2f;

                _vc.PanOffsetMap = new PointF(
                    e.X - cx - (e.X - cx - _vc.PanOffsetMap.X) * ratio,
                    e.Y - cy - (e.Y - cy - _vc.PanOffsetMap.Y) * ratio);
                _vc.ZoomFactorMap = newZoom;

                picMapCanvas.Invalidate();
            }
        }

        private void picMapCanvas_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            _vc.ResetZoom();
            picMapCanvas.Invalidate();
        }

        #endregion

        #region Batch Update Operations

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

        private void cmbBatchTable_SelectedIndexChanged(object? sender, EventArgs e)
        {
            cmbBatchFilterCol.Items.Clear();
            cmbBatchUpdateCol.Items.Clear();

            string selectedTable = cmbBatchTable.SelectedItem?.ToString() ?? "";
            if (selectedTable.Contains("遺構L"))
            {
                cmbBatchFilterCol.Items.AddRange(new object[] { "NAME", "LAYER", "MODE", "DATE", "ID", "LID" });
                cmbBatchUpdateCol.Items.AddRange(new object[] { "LAYER", "MODE", "NAME", "DATE" });
            }
            else if (selectedTable.Contains("遺構 (マスター)"))
            {
                cmbBatchFilterCol.Items.AddRange(new object[] { "NAME", "DATE", "ID" });
                cmbBatchUpdateCol.Items.AddRange(new object[] { "NAME", "DATE" });
            }
            else if (selectedTable.Contains("遺物"))
            {
                cmbBatchFilterCol.Items.AddRange(new object[] { "NAME(Syubetu)", "CHIKU", "SOUI", "SYUBETU", "LAYER", "DATE", "ID" });
                cmbBatchUpdateCol.Items.AddRange(new object[] { "LAYER", "CHIKU", "SOUI", "SYUBETU", "DATE" });
            }
            else if (selectedTable.Contains("基準点"))
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

        private void btnBatchExecute_Click(object? sender, EventArgs e)
        {
            var matchingItems = GetBatchMatchingItems();
            if (matchingItems.Count == 0)
            {
                MessageBox.Show("条件に一致するデータがありません。", "一括更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string updateCol = cmbBatchUpdateCol.SelectedItem?.ToString() ?? "";
            string updateVal = txtBatchUpdateVal.Text.Trim();

            if (string.IsNullOrEmpty(updateCol))
            {
                MessageBox.Show("変更対象の項目が選択されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"条件に一致する {matchingItems.Count} 件のデータに対して、\n項目 [{updateCol}] を [{updateVal}] に一括更新しますか？",
                "一括更新の確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            int successCount = 0;
            try
            {
                foreach (var obj in matchingItems)
                {
                    if (obj is IkouLModel line)
                    {
                        if (updateCol == "LAYER" && int.TryParse(updateVal, out int layer))
                        {
                            line.Layer = Math.Clamp(layer, 1, 16);
                        }
                        else if (updateCol == "MODE" && int.TryParse(updateVal, out int mode))
                        {
                            line.Mode = mode;
                        }
                        else if (updateCol == "NAME")
                        {
                            line.Name = updateVal;
                        }
                        else if (updateCol == "DATE")
                        {
                            line.Date = updateVal;
                        }
                        successCount++;
                    }
                    else if (obj is IkouModel ikou)
                    {
                        if (updateCol == "NAME") ikou.Name = updateVal;
                        else if (updateCol == "DATE") ikou.Date = updateVal;
                        successCount++;
                    }
                    else if (obj is IbutuModel ibutu)
                    {
                        if (updateCol == "LAYER" && int.TryParse(updateVal, out int layer))
                        {
                            ibutu.Layer = Math.Clamp(layer, 1, 16);
                        }
                        else if (updateCol == "CHIKU") ibutu.Chiku = updateVal;
                        else if (updateCol == "SOUI") ibutu.Soui = updateVal;
                        else if (updateCol == "SYUBETU") ibutu.Syubetu = updateVal;
                        else if (updateCol == "DATE") ibutu.Date = updateVal;
                        successCount++;
                    }
                    else if (obj is KikaiModel kikai)
                    {
                        if (updateCol == "LAYER" && int.TryParse(updateVal, out int layer))
                        {
                            kikai.Layer = Math.Clamp(layer, 1, 16);
                        }
                        else if (updateCol == "NAME") kikai.Name = updateVal;
                        else if (updateCol == "DATE") kikai.Date = updateVal;
                        successCount++;
                    }
                }

                dgvIkou.Refresh();
                dgvIkouL.Refresh();
                dgvIbutu.Refresh();
                dgvKikai.Refresh();
                picMapCanvas.Invalidate();

                RefreshBatchPreview();

                MessageBox.Show($"✔ {successCount} 件のデータを正常に一括更新しました！", "一括更新完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"一括更新中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetCXYZ(double x, double y, double z)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => SetCXYZ(x, y, z)));
                return;
            }

            if (_db == null) return;

            txtCoordX.Text = x.ToString("F3");
            txtCoordY.Text = y.ToString("F3");
            txtCoordZ.Text = z.ToString("F3");

            lblDbStatus.Text = $"📡 測量値取り込み: X={x:F3}, Y={y:F3}, Z={z:F3}";
        }

        #endregion
    }
}
