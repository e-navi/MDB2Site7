namespace Site7DbEditor
{
    partial class FormEditor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.btnOpenDb = new System.Windows.Forms.Button();
            this.cmbQuickDbSelect = new System.Windows.Forms.ComboBox();
            this.lblQuickDb = new System.Windows.Forms.Label();
            this.btnSaveDb = new System.Windows.Forms.Button();
            this.lblDbStatus = new System.Windows.Forms.Label();
            this.btnBluetoothCtrl = new System.Windows.Forms.Button();

            // Main Splitter Container (Top: Map Canvas, Bottom: Data & Edit Controls)
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelMapHeader = new System.Windows.Forms.Panel();
            this.lblMapTitle = new System.Windows.Forms.Label();
            this.btnResetMapZoom = new System.Windows.Forms.Button();
            this.panelMapLeft = new System.Windows.Forms.Panel();
            this.panelMapRight = new System.Windows.Forms.Panel();
            this.panelRightHeader = new System.Windows.Forms.Panel();
            this.lblRightTitle = new System.Windows.Forms.Label();
            this.btnDetachWindow = new System.Windows.Forms.Button();
            this.btnCloseRight = new System.Windows.Forms.Button();
            this.panelRightContent = new System.Windows.Forms.Panel();
            this.btnBgSettings = new System.Windows.Forms.Button();
            this.btnEnvSettings = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.lblIkouLayerGrpHeader = new System.Windows.Forms.Label();
            this.chkLayer01 = new System.Windows.Forms.CheckBox();
            this.chkLayer02 = new System.Windows.Forms.CheckBox();
            this.chkLayer03 = new System.Windows.Forms.CheckBox();
            this.chkLayer04 = new System.Windows.Forms.CheckBox();
            this.chkLayer05 = new System.Windows.Forms.CheckBox();
            this.chkLayer06 = new System.Windows.Forms.CheckBox();
            this.chkLayer07 = new System.Windows.Forms.CheckBox();
            this.chkLayer08 = new System.Windows.Forms.CheckBox();
            this.chkLayer09 = new System.Windows.Forms.CheckBox();
            this.chkLayer10 = new System.Windows.Forms.CheckBox();
            this.chkLayer11 = new System.Windows.Forms.CheckBox();
            this.chkLayer12 = new System.Windows.Forms.CheckBox();
            this.chkLayer13 = new System.Windows.Forms.CheckBox();
            this.chkLayer14 = new System.Windows.Forms.CheckBox();
            this.chkLayer15 = new System.Windows.Forms.CheckBox();
            this.chkLayer16 = new System.Windows.Forms.CheckBox();
            this.btnLayerAllOn = new System.Windows.Forms.Button();
            this.btnLayerAllOff = new System.Windows.Forms.Button();
            this.btnLayerSettings = new System.Windows.Forms.Button();
            this.btnBatchUpdateModal = new System.Windows.Forms.Button();
            this.lblEntityNameHeader = new System.Windows.Forms.Label();
            this.chkShowIbutuName = new System.Windows.Forms.CheckBox();
            this.chkShowIkouName = new System.Windows.Forms.CheckBox();
            this.chkShowKikaiName = new System.Windows.Forms.CheckBox();
            this.chkShowIkou = new System.Windows.Forms.CheckBox();
            this.chkShowIbutu = new System.Windows.Forms.CheckBox();
            this.chkShowKikai = new System.Windows.Forms.CheckBox();
            this.chkShowCurve = new System.Windows.Forms.CheckBox();
            this.chkShowGrid = new System.Windows.Forms.CheckBox();
            this.chkColorByIkou = new System.Windows.Forms.CheckBox();
            this.picMapCanvas = new System.Windows.Forms.PictureBox();

            // Bottom Splitter Container (Left: Tabbed Data Grids, Right: Edit SidePanel)
            this.splitContainerBottom = new System.Windows.Forms.SplitContainer();
            this.tabControlData = new System.Windows.Forms.TabControl();
            this.tabIkou = new System.Windows.Forms.TabPage();
            this.grpIkouMaster = new System.Windows.Forms.GroupBox();
            this.dgvIkou = new System.Windows.Forms.DataGridView();
            this.lblPrefixHeader = new System.Windows.Forms.Label();
            this.lblSeqHeader = new System.Windows.Forms.Label();
            this.cmbIkouKind = new System.Windows.Forms.ComboBox();
            this.txtIkouNum = new System.Windows.Forms.TextBox();
            this.btnMaxPlusOne = new System.Windows.Forms.Button();
            this.lblIkouNameTitle = new System.Windows.Forms.Label();
            this.lblIkouNameVal = new System.Windows.Forms.Label();
            this.btnSetPos = new System.Windows.Forms.Button();
            this.btnView3D = new System.Windows.Forms.Button();
            this.btnDeleteIkouRight = new System.Windows.Forms.Button();
            this.btnUpdateIkouRight = new System.Windows.Forms.Button();
            this.btnAddIkou = new System.Windows.Forms.Button();
            this.grpIkouL = new System.Windows.Forms.GroupBox();
            this.dgvIkouL = new System.Windows.Forms.DataGridView();

            this.grpPrecs = new System.Windows.Forms.GroupBox();
            this.dgvPrecs = new System.Windows.Forms.DataGridView();

            this.tabIbutu = new System.Windows.Forms.TabPage();
            this.dgvIbutu = new System.Windows.Forms.DataGridView();
            this.grpIbutuRecord = new System.Windows.Forms.GroupBox();
            this.lblFilterIbutu = new System.Windows.Forms.Label();
            this.txtFilterIbutu = new System.Windows.Forms.TextBox();
            this.lblIbutuChiku = new System.Windows.Forms.Label();
            this.cmbIbutuChiku = new System.Windows.Forms.ComboBox();
            this.lblIbutuSoui = new System.Windows.Forms.Label();
            this.cmbIbutuSoui = new System.Windows.Forms.ComboBox();
            this.lblIbutuSyubetu = new System.Windows.Forms.Label();
            this.cmbIbutuSyubetu = new System.Windows.Forms.ComboBox();
            this.lblIbutuLayer = new System.Windows.Forms.Label();
            this.cmbIbutuLayer = new System.Windows.Forms.ComboBox();
            this.lblIbutuNo = new System.Windows.Forms.Label();
            this.txtIbutuNo = new System.Windows.Forms.TextBox();
            this.chkIbutuAutoInc = new System.Windows.Forms.CheckBox();
            this.btnIbutuMaxPlusOne = new System.Windows.Forms.Button();

            this.tabKikai = new System.Windows.Forms.TabPage();
            this.dgvKikai = new System.Windows.Forms.DataGridView();
            this.grpKikaiRecord = new System.Windows.Forms.GroupBox();
            this.lblKikaiName = new System.Windows.Forms.Label();
            this.txtKikaiName = new System.Windows.Forms.TextBox();
            this.lblKikaiLayer = new System.Windows.Forms.Label();
            this.cmbKikaiLayer = new System.Windows.Forms.ComboBox();
            this.grpPointGuidance = new System.Windows.Forms.GroupBox();
            this.chkPointGuidance = new System.Windows.Forms.CheckBox();

            this.tabLayer = new System.Windows.Forms.TabPage();
            this.dgvLayer = new System.Windows.Forms.DataGridView();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.btnDeleteLayer = new System.Windows.Forms.Button();

            this.tabBatchUpdate = new System.Windows.Forms.TabPage();
            this.grpBatchOptions = new System.Windows.Forms.GroupBox();
            this.lblBatchTable = new System.Windows.Forms.Label();
            this.cmbBatchTable = new System.Windows.Forms.ComboBox();
            this.lblBatchFilter = new System.Windows.Forms.Label();
            this.cmbBatchFilterCol = new System.Windows.Forms.ComboBox();
            this.cmbBatchFilterOp = new System.Windows.Forms.ComboBox();
            this.txtBatchFilterVal = new System.Windows.Forms.TextBox();
            this.lblBatchUpdate = new System.Windows.Forms.Label();
            this.cmbBatchUpdateCol = new System.Windows.Forms.ComboBox();
            this.txtBatchUpdateVal = new System.Windows.Forms.TextBox();
            this.btnBatchExecute = new System.Windows.Forms.Button();
            this.lblBatchPreviewCount = new System.Windows.Forms.Label();
            this.dgvBatchPreview = new System.Windows.Forms.DataGridView();

            // Ikou Master Controls
            this.lblPrefixHeader = new System.Windows.Forms.Label();
            this.lblSeqHeader = new System.Windows.Forms.Label();
            this.cmbIkouKind = new System.Windows.Forms.ComboBox();
            this.txtIkouNum = new System.Windows.Forms.TextBox();
            this.lblIkouNameTitle = new System.Windows.Forms.Label();
            this.lblIkouNameVal = new System.Windows.Forms.Label();
            this.btnMaxPlusOne = new System.Windows.Forms.Button();
            this.btnSetPos = new System.Windows.Forms.Button();
            this.btnView3D = new System.Windows.Forms.Button();
            this.btnUpdateIkouRight = new System.Windows.Forms.Button();
            this.btnDeleteIkouRight = new System.Windows.Forms.Button();
            this.btnAddIkou = new System.Windows.Forms.Button();

            this.cmbLineKind = new System.Windows.Forms.ComboBox();
            this.txtLineNum = new System.Windows.Forms.TextBox();
            this.btnLineMaxPlusOne = new System.Windows.Forms.Button();
            this.lblLinePrefixHeader = new System.Windows.Forms.Label();
            this.lblLineSeqHeader = new System.Windows.Forms.Label();
            this.lblLineNameTitle = new System.Windows.Forms.Label();
            this.lblLineNameVal = new System.Windows.Forms.Label();
            this.rdoLineOpen = new System.Windows.Forms.RadioButton();
            this.rdoLineClosed = new System.Windows.Forms.RadioButton();
            this.rdoLinePoint = new System.Windows.Forms.RadioButton();
            this.lblLineLayer = new System.Windows.Forms.Label();
            this.cmbLineLayer = new System.Windows.Forms.ComboBox();
            this.lblLineIkouMaster = new System.Windows.Forms.Label();
            this.cmbLineIkouMaster = new System.Windows.Forms.ComboBox();
            this.btnLineSetPos = new System.Windows.Forms.Button();
            this.btnDeleteLineRight = new System.Windows.Forms.Button();
            this.btnUpdateLineRight = new System.Windows.Forms.Button();
            this.btnAddIkouL = new System.Windows.Forms.Button();

            this.grpPointEdit = new System.Windows.Forms.GroupBox();
            this.lblCoordX = new System.Windows.Forms.Label();
            this.txtCoordX = new System.Windows.Forms.TextBox();
            this.lblCoordY = new System.Windows.Forms.Label();
            this.txtCoordY = new System.Windows.Forms.TextBox();
            this.lblCoordZ = new System.Windows.Forms.Label();
            this.txtCoordZ = new System.Windows.Forms.TextBox();
            this.chkScreenInput = new System.Windows.Forms.CheckBox();
            this.btnUpdatePointRight = new System.Windows.Forms.Button();
            this.btnDeletePointRight = new System.Windows.Forms.Button();
            this.btnAddPointRight = new System.Windows.Forms.Button();
            this.grpCoordValue = new System.Windows.Forms.GroupBox();
            this.pnlPrecsRight = new System.Windows.Forms.Panel();
            this.pnlIbutuRight = new System.Windows.Forms.Panel();
            this.pnlKikaiRight = new System.Windows.Forms.Panel();

            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelMapHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMapCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBottom)).BeginInit();
            this.splitContainerBottom.Panel1.SuspendLayout();
            this.splitContainerBottom.Panel2.SuspendLayout();
            this.splitContainerBottom.SuspendLayout();
            this.tabControlData.SuspendLayout();
            this.tabIkou.SuspendLayout();
            this.tabControlData.SuspendLayout();
            this.tabIkou.SuspendLayout();
            this.grpIkouMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIkou)).BeginInit();
            this.grpIkouL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIkouL)).BeginInit();
            this.grpPrecs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrecs)).BeginInit();
            this.tabIbutu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIbutu)).BeginInit();
            this.tabKikai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKikai)).BeginInit();
            this.tabLayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayer)).BeginInit();
            this.tabBatchUpdate.SuspendLayout();
            this.grpBatchOptions.SuspendLayout();
            this.grpPointEdit.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(46)))));
            this.panelHeader.Controls.Add(this.btnBluetoothCtrl);
            this.panelHeader.Controls.Add(this.lblDbStatus);
            this.panelHeader.Controls.Add(this.btnSaveDb);
            this.panelHeader.Controls.Add(this.lblQuickDb);
            this.panelHeader.Controls.Add(this.cmbQuickDbSelect);
            this.panelHeader.Controls.Add(this.btnOpenDb);
            this.panelHeader.Controls.Add(this.lblSubHeader);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1480, 75);
            this.panelHeader.TabIndex = 0;

            // lblHeaderTitle
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Yu Gothic UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(15, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(260, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "SITE7 SQLite Database Editor";

            // lblSubHeader
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblSubHeader.Location = new System.Drawing.Point(16, 42);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(320, 15);
            this.lblSubHeader.TabIndex = 1;
            this.lblSubHeader.Text = "2D 測量図面 ＆ SQLite DB 連動統合エディタ";

            // btnOpenDb
            this.btnOpenDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnOpenDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenDb.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenDb.ForeColor = System.Drawing.Color.White;
            this.btnOpenDb.Location = new System.Drawing.Point(360, 20);
            this.btnOpenDb.Name = "btnOpenDb";
            this.btnOpenDb.Size = new System.Drawing.Size(130, 35);
            this.btnOpenDb.TabIndex = 2;
            this.btnOpenDb.Text = "📂 DBフォルダ選択";
            this.btnOpenDb.UseVisualStyleBackColor = false;

            // lblQuickDb
            this.lblQuickDb.AutoSize = true;
            this.lblQuickDb.ForeColor = System.Drawing.Color.White;
            this.lblQuickDb.Location = new System.Drawing.Point(505, 30);
            this.lblQuickDb.Name = "lblQuickDb";
            this.lblQuickDb.Size = new System.Drawing.Size(64, 15);
            this.lblQuickDb.TabIndex = 3;
            this.lblQuickDb.Text = "Quick Select:";

            // cmbQuickDbSelect
            this.cmbQuickDbSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbQuickDbSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuickDbSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuickDbSelect.ForeColor = System.Drawing.Color.White;
            this.cmbQuickDbSelect.FormattingEnabled = true;
            this.cmbQuickDbSelect.Location = new System.Drawing.Point(575, 26);
            this.cmbQuickDbSelect.Name = "cmbQuickDbSelect";
            this.cmbQuickDbSelect.Size = new System.Drawing.Size(240, 23);
            this.cmbQuickDbSelect.TabIndex = 4;

            // btnBluetoothCtrl
            this.btnBluetoothCtrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(214)))));
            this.btnBluetoothCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBluetoothCtrl.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBluetoothCtrl.ForeColor = System.Drawing.Color.White;
            this.btnBluetoothCtrl.Location = new System.Drawing.Point(825, 23);
            this.btnBluetoothCtrl.Name = "btnBluetoothCtrl";
            this.btnBluetoothCtrl.Size = new System.Drawing.Size(180, 30);
            this.btnBluetoothCtrl.TabIndex = 5;
            this.btnBluetoothCtrl.Text = "📡 測量機器(Bluetooth)";
            this.btnBluetoothCtrl.UseVisualStyleBackColor = false;

            // btnSaveDb
            this.btnSaveDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(176)))), ((int)(((byte)(0)))));
            this.btnSaveDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDb.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSaveDb.ForeColor = System.Drawing.Color.Black;
            this.btnSaveDb.Location = new System.Drawing.Point(1310, 20);
            this.btnSaveDb.Name = "btnSaveDb";
            this.btnSaveDb.Size = new System.Drawing.Size(155, 35);
            this.btnSaveDb.TabIndex = 5;
            this.btnSaveDb.Text = "💾 SQLite DB保存";
            this.btnSaveDb.UseVisualStyleBackColor = false;

            // lblDbStatus
            this.lblDbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDbStatus.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(183)))), ((int)(((byte)(3)))));
            this.lblDbStatus.Location = new System.Drawing.Point(1085, 20);
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Size = new System.Drawing.Size(215, 35);
            this.lblDbStatus.TabIndex = 6;
            this.lblDbStatus.Text = "DB未読み込み";
            this.lblDbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // splitContainerMain (Top: Map Canvas, Bottom: Data & Edit)
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 75);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1 (Top Map View)
            // Dock レイアウトのルール: Fill は最後に追加すること
            // 追加順: Top → Left → Right → Fill
            this.splitContainerMain.Panel1.Controls.Add(this.panelMapHeader);
            this.splitContainerMain.Panel1.Controls.Add(this.panelMapLeft);
            this.splitContainerMain.Panel1.Controls.Add(this.panelMapRight);
            this.splitContainerMain.Panel1.Controls.Add(this.picMapCanvas);
            // 
            // splitContainerMain.Panel2 (Bottom Data Grids & Edit)
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerBottom);
            this.splitContainerMain.Size = new System.Drawing.Size(1480, 800);
            this.splitContainerMain.SplitterDistance = 440;
            this.splitContainerMain.TabIndex = 1;

            // 
            // panelRightHeader
            // 
            this.panelRightHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.panelRightHeader.Controls.Add(this.lblRightTitle);
            this.panelRightHeader.Controls.Add(this.btnDetachWindow);
            this.panelRightHeader.Controls.Add(this.btnCloseRight);
            this.panelRightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRightHeader.Location = new System.Drawing.Point(0, 0);
            this.panelRightHeader.Name = "panelRightHeader";
            this.panelRightHeader.Size = new System.Drawing.Size(264, 30);
            this.panelRightHeader.TabIndex = 0;

            this.lblRightTitle.AutoSize = true;
            this.lblRightTitle.Font = new System.Drawing.Font("MS UI Gothic", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRightTitle.ForeColor = System.Drawing.Color.White;
            this.lblRightTitle.Location = new System.Drawing.Point(6, 8);
            this.lblRightTitle.Name = "lblRightTitle";
            this.lblRightTitle.Size = new System.Drawing.Size(120, 15);
            this.lblRightTitle.Text = "📡 測量機器制御";

            this.btnDetachWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(80)))));
            this.btnDetachWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetachWindow.Font = new System.Drawing.Font("MS UI Gothic", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDetachWindow.ForeColor = System.Drawing.Color.White;
            this.btnDetachWindow.Location = new System.Drawing.Point(164, 3);
            this.btnDetachWindow.Name = "btnDetachWindow";
            this.btnDetachWindow.Size = new System.Drawing.Size(64, 24);
            this.btnDetachWindow.TabIndex = 1;
            this.btnDetachWindow.Text = "↗ 分離";
            this.btnDetachWindow.UseVisualStyleBackColor = false;

            this.btnCloseRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(80)))));
            this.btnCloseRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseRight.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCloseRight.ForeColor = System.Drawing.Color.White;
            this.btnCloseRight.Location = new System.Drawing.Point(231, 3);
            this.btnCloseRight.Name = "btnCloseRight";
            this.btnCloseRight.Size = new System.Drawing.Size(28, 24);
            this.btnCloseRight.TabIndex = 2;
            this.btnCloseRight.Text = "✖";
            this.btnCloseRight.UseVisualStyleBackColor = false;

            this.panelRightContent.AutoScroll = true;
            this.panelRightContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightContent.Location = new System.Drawing.Point(0, 30);
            this.panelRightContent.Name = "panelRightContent";
            this.panelRightContent.Size = new System.Drawing.Size(264, 420);

            this.panelMapRight.BackColor = System.Drawing.SystemColors.Control;
            this.panelMapRight.Controls.Add(this.panelRightContent);
            this.panelMapRight.Controls.Add(this.panelRightHeader);
            this.panelMapRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelMapRight.Location = new System.Drawing.Point(1195, 0);
            this.panelMapRight.Name = "panelMapRight";
            this.panelMapRight.Size = new System.Drawing.Size(264, 450);
            this.panelMapRight.TabIndex = 2;
            this.panelMapRight.Visible = false;
            this.panelMapRight.Visible = false;

            // panelMapHeader
            this.panelMapHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.panelMapHeader.Controls.Add(this.lblMapTitle);
            this.panelMapHeader.Controls.Add(this.btnResetMapZoom);
            this.panelMapHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMapHeader.Location = new System.Drawing.Point(0, 0);
            this.panelMapHeader.Name = "panelMapHeader";
            this.panelMapHeader.Size = new System.Drawing.Size(1480, 35);
            this.panelMapHeader.TabIndex = 0;

            // lblMapTitle
            this.lblMapTitle.AutoSize = true;
            this.lblMapTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMapTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblMapTitle.Location = new System.Drawing.Point(12, 8);
            this.lblMapTitle.Name = "lblMapTitle";
            this.lblMapTitle.Size = new System.Drawing.Size(175, 17);
            this.lblMapTitle.TabIndex = 0;
            this.lblMapTitle.Text = "🗺 2D 測量平面図 (CAD表示)";

            // btnResetMapZoom
            this.btnResetMapZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetMapZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnResetMapZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetMapZoom.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnResetMapZoom.ForeColor = System.Drawing.Color.White;
            this.btnResetMapZoom.Location = new System.Drawing.Point(1350, 5);
            this.btnResetMapZoom.Name = "btnResetMapZoom";
            this.btnResetMapZoom.Size = new System.Drawing.Size(115, 26);
            this.btnResetMapZoom.TabIndex = 1;
            this.btnResetMapZoom.Text = "🔍 全体表示リセット";
            this.btnResetMapZoom.UseVisualStyleBackColor = false;

            // panelMapLeft
            this.panelMapLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(242)))));
            this.panelMapLeft.Controls.Add(this.btnBgSettings);
            this.panelMapLeft.Controls.Add(this.btnEnvSettings);
            this.panelMapLeft.Controls.Add(this.btnUndo);
            this.panelMapLeft.Controls.Add(this.btnRedo);
            this.panelMapLeft.Controls.Add(this.lblIkouLayerGrpHeader);
            this.panelMapLeft.Controls.Add(this.chkLayer01);
            this.panelMapLeft.Controls.Add(this.chkLayer02);
            this.panelMapLeft.Controls.Add(this.chkLayer03);
            this.panelMapLeft.Controls.Add(this.chkLayer04);
            this.panelMapLeft.Controls.Add(this.chkLayer05);
            this.panelMapLeft.Controls.Add(this.chkLayer06);
            this.panelMapLeft.Controls.Add(this.chkLayer07);
            this.panelMapLeft.Controls.Add(this.chkLayer08);
            this.panelMapLeft.Controls.Add(this.chkLayer09);
            this.panelMapLeft.Controls.Add(this.chkLayer10);
            this.panelMapLeft.Controls.Add(this.chkLayer11);
            this.panelMapLeft.Controls.Add(this.chkLayer12);
            this.panelMapLeft.Controls.Add(this.chkLayer13);
            this.panelMapLeft.Controls.Add(this.chkLayer14);
            this.panelMapLeft.Controls.Add(this.chkLayer15);
            this.panelMapLeft.Controls.Add(this.chkLayer16);
            this.panelMapLeft.Controls.Add(this.btnLayerAllOn);
            this.panelMapLeft.Controls.Add(this.btnLayerAllOff);
            this.panelMapLeft.Controls.Add(this.btnLayerSettings);
            this.panelMapLeft.Controls.Add(this.btnBatchUpdateModal);
            this.panelMapLeft.Controls.Add(this.lblEntityNameHeader);
            this.panelMapLeft.Controls.Add(this.chkShowIbutu);
            this.panelMapLeft.Controls.Add(this.chkShowIbutuName);
            this.panelMapLeft.Controls.Add(this.chkShowIkou);
            this.panelMapLeft.Controls.Add(this.chkShowIkouName);
            this.panelMapLeft.Controls.Add(this.chkShowKikai);
            this.panelMapLeft.Controls.Add(this.chkShowKikaiName);
            this.panelMapLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMapLeft.Location = new System.Drawing.Point(0, 35);
            this.panelMapLeft.Name = "panelMapLeft";
            this.panelMapLeft.Size = new System.Drawing.Size(130, 405);
            this.panelMapLeft.TabIndex = 1;

            // btnBgSettings
            this.btnBgSettings.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnBgSettings.Location = new System.Drawing.Point(6, 5);
            this.btnBgSettings.Name = "btnBgSettings";
            this.btnBgSettings.Size = new System.Drawing.Size(118, 23);
            this.btnBgSettings.TabIndex = 0;
            this.btnBgSettings.Text = "背景設定";
            this.btnBgSettings.UseVisualStyleBackColor = true;

            // btnEnvSettings
            this.btnEnvSettings.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnEnvSettings.Location = new System.Drawing.Point(6, 30);
            this.btnEnvSettings.Name = "btnEnvSettings";
            this.btnEnvSettings.Size = new System.Drawing.Size(118, 23);
            this.btnEnvSettings.TabIndex = 1;
            this.btnEnvSettings.Text = "環境設定";
            this.btnEnvSettings.UseVisualStyleBackColor = true;

            // btnUndo
            this.btnUndo.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnUndo.Location = new System.Drawing.Point(6, 55);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(57, 23);
            this.btnUndo.TabIndex = 2;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;

            // btnRedo
            this.btnRedo.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRedo.Location = new System.Drawing.Point(67, 55);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(57, 23);
            this.btnRedo.TabIndex = 3;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;

            // lblIkouLayerGrpHeader
            this.lblIkouLayerGrpHeader.AutoSize = true;
            this.lblIkouLayerGrpHeader.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblIkouLayerGrpHeader.ForeColor = System.Drawing.Color.Red;
            this.lblIkouLayerGrpHeader.Location = new System.Drawing.Point(6, 83);
            this.lblIkouLayerGrpHeader.Name = "lblIkouLayerGrpHeader";
            this.lblIkouLayerGrpHeader.Size = new System.Drawing.Size(76, 15);
            this.lblIkouLayerGrpHeader.TabIndex = 4;
            this.lblIkouLayerGrpHeader.Text = "遺構レイヤGRP";

            // chkLayer01
            this.chkLayer01.AutoSize = true;
            this.chkLayer01.Checked = true;
            this.chkLayer01.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer01.ForeColor = System.Drawing.Color.Black;
            this.chkLayer01.Location = new System.Drawing.Point(6, 101);
            this.chkLayer01.Name = "chkLayer01";
            this.chkLayer01.Size = new System.Drawing.Size(46, 19);
            this.chkLayer01.TabIndex = 5;
            this.chkLayer01.Text = "L01";
            this.chkLayer01.UseVisualStyleBackColor = true;

            // chkLayer02
            this.chkLayer02.AutoSize = true;
            this.chkLayer02.Checked = true;
            this.chkLayer02.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer02.ForeColor = System.Drawing.Color.Red;
            this.chkLayer02.Location = new System.Drawing.Point(6, 121);
            this.chkLayer02.Name = "chkLayer02";
            this.chkLayer02.Size = new System.Drawing.Size(46, 19);
            this.chkLayer02.TabIndex = 6;
            this.chkLayer02.Text = "L02";
            this.chkLayer02.UseVisualStyleBackColor = true;

            // chkLayer03
            this.chkLayer03.AutoSize = true;
            this.chkLayer03.Checked = true;
            this.chkLayer03.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer03.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(0)))));
            this.chkLayer03.Location = new System.Drawing.Point(6, 141);
            this.chkLayer03.Name = "chkLayer03";
            this.chkLayer03.Size = new System.Drawing.Size(46, 19);
            this.chkLayer03.TabIndex = 7;
            this.chkLayer03.Text = "L03";
            this.chkLayer03.UseVisualStyleBackColor = true;

            // chkLayer04
            this.chkLayer04.AutoSize = true;
            this.chkLayer04.Checked = true;
            this.chkLayer04.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer04.ForeColor = System.Drawing.Color.Blue;
            this.chkLayer04.Location = new System.Drawing.Point(6, 161);
            this.chkLayer04.Name = "chkLayer04";
            this.chkLayer04.Size = new System.Drawing.Size(46, 19);
            this.chkLayer04.TabIndex = 8;
            this.chkLayer04.Text = "L04";
            this.chkLayer04.UseVisualStyleBackColor = true;

            // chkLayer05
            this.chkLayer05.AutoSize = true;
            this.chkLayer05.Checked = true;
            this.chkLayer05.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer05.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(180)))), ((int)(((byte)(0)))));
            this.chkLayer05.Location = new System.Drawing.Point(6, 181);
            this.chkLayer05.Name = "chkLayer05";
            this.chkLayer05.Size = new System.Drawing.Size(46, 19);
            this.chkLayer05.TabIndex = 9;
            this.chkLayer05.Text = "L05";
            this.chkLayer05.UseVisualStyleBackColor = true;

            // chkLayer06
            this.chkLayer06.AutoSize = true;
            this.chkLayer06.Checked = true;
            this.chkLayer06.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer06.ForeColor = System.Drawing.Color.Magenta;
            this.chkLayer06.Location = new System.Drawing.Point(6, 201);
            this.chkLayer06.Name = "chkLayer06";
            this.chkLayer06.Size = new System.Drawing.Size(46, 19);
            this.chkLayer06.TabIndex = 10;
            this.chkLayer06.Text = "L06";
            this.chkLayer06.UseVisualStyleBackColor = true;

            // chkLayer07
            this.chkLayer07.AutoSize = true;
            this.chkLayer07.Checked = true;
            this.chkLayer07.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer07.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.chkLayer07.Location = new System.Drawing.Point(6, 221);
            this.chkLayer07.Name = "chkLayer07";
            this.chkLayer07.Size = new System.Drawing.Size(46, 19);
            this.chkLayer07.TabIndex = 11;
            this.chkLayer07.Text = "L07";
            this.chkLayer07.UseVisualStyleBackColor = true;

            // chkLayer08
            this.chkLayer08.AutoSize = true;
            this.chkLayer08.Checked = true;
            this.chkLayer08.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer08.ForeColor = System.Drawing.Color.DarkGray;
            this.chkLayer08.Location = new System.Drawing.Point(6, 241);
            this.chkLayer08.Name = "chkLayer08";
            this.chkLayer08.Size = new System.Drawing.Size(46, 19);
            this.chkLayer08.TabIndex = 12;
            this.chkLayer08.Text = "L08";
            this.chkLayer08.UseVisualStyleBackColor = true;

            // chkLayer09
            this.chkLayer09.AutoSize = true;
            this.chkLayer09.Checked = true;
            this.chkLayer09.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer09.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.chkLayer09.Location = new System.Drawing.Point(66, 101);
            this.chkLayer09.Name = "chkLayer09";
            this.chkLayer09.Size = new System.Drawing.Size(46, 19);
            this.chkLayer09.TabIndex = 13;
            this.chkLayer09.Text = "L09";
            this.chkLayer09.UseVisualStyleBackColor = true;

            // chkLayer10
            this.chkLayer10.AutoSize = true;
            this.chkLayer10.Checked = true;
            this.chkLayer10.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(128)))), ((int)(((byte)(64)))));
            this.chkLayer10.Location = new System.Drawing.Point(66, 121);
            this.chkLayer10.Name = "chkLayer10";
            this.chkLayer10.Size = new System.Drawing.Size(46, 19);
            this.chkLayer10.TabIndex = 14;
            this.chkLayer10.Text = "L10";
            this.chkLayer10.UseVisualStyleBackColor = true;

            // chkLayer11
            this.chkLayer11.AutoSize = true;
            this.chkLayer11.Checked = true;
            this.chkLayer11.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.chkLayer11.Location = new System.Drawing.Point(66, 141);
            this.chkLayer11.Name = "chkLayer11";
            this.chkLayer11.Size = new System.Drawing.Size(46, 19);
            this.chkLayer11.TabIndex = 15;
            this.chkLayer11.Text = "L11";
            this.chkLayer11.UseVisualStyleBackColor = true;

            // chkLayer12
            this.chkLayer12.AutoSize = true;
            this.chkLayer12.Checked = true;
            this.chkLayer12.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.chkLayer12.Location = new System.Drawing.Point(66, 161);
            this.chkLayer12.Name = "chkLayer12";
            this.chkLayer12.Size = new System.Drawing.Size(46, 19);
            this.chkLayer12.TabIndex = 16;
            this.chkLayer12.Text = "L12";
            this.chkLayer12.UseVisualStyleBackColor = true;

            // chkLayer13
            this.chkLayer13.AutoSize = true;
            this.chkLayer13.Checked = true;
            this.chkLayer13.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.chkLayer13.Location = new System.Drawing.Point(66, 181);
            this.chkLayer13.Name = "chkLayer13";
            this.chkLayer13.Size = new System.Drawing.Size(46, 19);
            this.chkLayer13.TabIndex = 17;
            this.chkLayer13.Text = "L13";
            this.chkLayer13.UseVisualStyleBackColor = true;

            // chkLayer14
            this.chkLayer14.AutoSize = true;
            this.chkLayer14.Checked = true;
            this.chkLayer14.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(255)))));
            this.chkLayer14.Location = new System.Drawing.Point(66, 201);
            this.chkLayer14.Name = "chkLayer14";
            this.chkLayer14.Size = new System.Drawing.Size(46, 19);
            this.chkLayer14.TabIndex = 18;
            this.chkLayer14.Text = "L14";
            this.chkLayer14.UseVisualStyleBackColor = true;

            // chkLayer15
            this.chkLayer15.AutoSize = true;
            this.chkLayer15.Checked = true;
            this.chkLayer15.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.chkLayer15.Location = new System.Drawing.Point(66, 221);
            this.chkLayer15.Name = "chkLayer15";
            this.chkLayer15.Size = new System.Drawing.Size(46, 19);
            this.chkLayer15.TabIndex = 19;
            this.chkLayer15.Text = "L15";
            this.chkLayer15.UseVisualStyleBackColor = true;

            // chkLayer16
            this.chkLayer16.AutoSize = true;
            this.chkLayer16.Checked = true;
            this.chkLayer16.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkLayer16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.chkLayer16.Location = new System.Drawing.Point(66, 241);
            this.chkLayer16.Name = "chkLayer16";
            this.chkLayer16.Size = new System.Drawing.Size(46, 19);
            this.chkLayer16.TabIndex = 20;
            this.chkLayer16.Text = "L16";
            this.chkLayer16.UseVisualStyleBackColor = true;

            // btnLayerAllOn
            this.btnLayerAllOn.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLayerAllOn.Location = new System.Drawing.Point(6, 263);
            this.btnLayerAllOn.Name = "btnLayerAllOn";
            this.btnLayerAllOn.Size = new System.Drawing.Size(57, 23);
            this.btnLayerAllOn.TabIndex = 21;
            this.btnLayerAllOn.Text = "全ON";
            this.btnLayerAllOn.UseVisualStyleBackColor = true;

            // btnLayerAllOff
            this.btnLayerAllOff.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLayerAllOff.Location = new System.Drawing.Point(67, 263);
            this.btnLayerAllOff.Name = "btnLayerAllOff";
            this.btnLayerAllOff.Size = new System.Drawing.Size(57, 23);
            this.btnLayerAllOff.TabIndex = 22;
            this.btnLayerAllOff.Text = "全";
            this.btnLayerAllOff.UseVisualStyleBackColor = true;

            // btnLayerSettings
            this.btnLayerSettings.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnLayerSettings.Location = new System.Drawing.Point(6, 289);
            this.btnLayerSettings.Name = "btnLayerSettings";
            this.btnLayerSettings.Size = new System.Drawing.Size(118, 23);
            this.btnLayerSettings.TabIndex = 23;
            this.btnLayerSettings.Text = "レイヤ設定";
            this.btnLayerSettings.UseVisualStyleBackColor = true;

            // btnBatchUpdateModal
            this.btnBatchUpdateModal.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnBatchUpdateModal.Location = new System.Drawing.Point(6, 314);
            this.btnBatchUpdateModal.Name = "btnBatchUpdateModal";
            this.btnBatchUpdateModal.Size = new System.Drawing.Size(118, 23);
            this.btnBatchUpdateModal.TabIndex = 24;
            this.btnBatchUpdateModal.Text = "一括更新";
            this.btnBatchUpdateModal.UseVisualStyleBackColor = true;

            // lblEntityNameHeader
            this.lblEntityNameHeader.AutoSize = true;
            this.lblEntityNameHeader.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblEntityNameHeader.ForeColor = System.Drawing.Color.Red;
            this.lblEntityNameHeader.Location = new System.Drawing.Point(82, 341);
            this.lblEntityNameHeader.Name = "lblEntityNameHeader";
            this.lblEntityNameHeader.Size = new System.Drawing.Size(31, 15);
            this.lblEntityNameHeader.TabIndex = 25;
            this.lblEntityNameHeader.Text = "名称";

            // chkShowIbutu
            this.chkShowIbutu.AutoSize = true;
            this.chkShowIbutu.Checked = true;
            this.chkShowIbutu.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowIbutu.ForeColor = System.Drawing.Color.Red;
            this.chkShowIbutu.Location = new System.Drawing.Point(6, 358);
            this.chkShowIbutu.Name = "chkShowIbutu";
            this.chkShowIbutu.Size = new System.Drawing.Size(50, 19);
            this.chkShowIbutu.TabIndex = 26;
            this.chkShowIbutu.Text = "遺物";
            this.chkShowIbutu.UseVisualStyleBackColor = true;

            // chkShowIbutuName
            this.chkShowIbutuName.AutoSize = true;
            this.chkShowIbutuName.Location = new System.Drawing.Point(90, 361);
            this.chkShowIbutuName.Name = "chkShowIbutuName";
            this.chkShowIbutuName.Size = new System.Drawing.Size(15, 14);
            this.chkShowIbutuName.TabIndex = 27;
            this.chkShowIbutuName.UseVisualStyleBackColor = true;

            // chkShowIkou
            this.chkShowIkou.AutoSize = true;
            this.chkShowIkou.Checked = true;
            this.chkShowIkou.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowIkou.ForeColor = System.Drawing.Color.Blue;
            this.chkShowIkou.Location = new System.Drawing.Point(6, 380);
            this.chkShowIkou.Name = "chkShowIkou";
            this.chkShowIkou.Size = new System.Drawing.Size(50, 19);
            this.chkShowIkou.TabIndex = 28;
            this.chkShowIkou.Text = "遺構";
            this.chkShowIkou.UseVisualStyleBackColor = true;

            // chkShowIkouName
            this.chkShowIkouName.AutoSize = true;
            this.chkShowIkouName.Location = new System.Drawing.Point(90, 383);
            this.chkShowIkouName.Name = "chkShowIkouName";
            this.chkShowIkouName.Size = new System.Drawing.Size(15, 14);
            this.chkShowIkouName.TabIndex = 29;
            this.chkShowIkouName.UseVisualStyleBackColor = true;

            // chkShowKikai
            this.chkShowKikai.AutoSize = true;
            this.chkShowKikai.Checked = true;
            this.chkShowKikai.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowKikai.ForeColor = System.Drawing.Color.Blue;
            this.chkShowKikai.Location = new System.Drawing.Point(6, 402);
            this.chkShowKikai.Name = "chkShowKikai";
            this.chkShowKikai.Size = new System.Drawing.Size(62, 19);
            this.chkShowKikai.TabIndex = 30;
            this.chkShowKikai.Text = "基準点";
            this.chkShowKikai.UseVisualStyleBackColor = true;

            // chkShowKikaiName
            this.chkShowKikaiName.AutoSize = true;
            this.chkShowKikaiName.Checked = true;
            this.chkShowKikaiName.Location = new System.Drawing.Point(90, 405);
            this.chkShowKikaiName.Name = "chkShowKikaiName";
            this.chkShowKikaiName.Size = new System.Drawing.Size(15, 14);
            this.chkShowKikaiName.TabIndex = 31;
            this.chkShowKikaiName.UseVisualStyleBackColor = true;

            // picMapCanvas
            this.picMapCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this.picMapCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMapCanvas.Location = new System.Drawing.Point(130, 35);
            this.picMapCanvas.Name = "picMapCanvas";
            this.picMapCanvas.Size = new System.Drawing.Size(1350, 405);
            this.picMapCanvas.TabIndex = 2;
            this.picMapCanvas.TabStop = false;

            // 
            // splitContainerBottom (Left: Data Tab, Right: Property Edit Panel)
            // 
            this.splitContainerBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerBottom.Location = new System.Drawing.Point(0, 0);
            this.splitContainerBottom.Name = "splitContainerBottom";
            // 
            // splitContainerBottom.Panel1 (Left Data Tabs)
            // 
            this.splitContainerBottom.Panel1.Controls.Add(this.tabControlData);
            this.splitContainerBottom.Panel2Collapsed = true;
            this.splitContainerBottom.Size = new System.Drawing.Size(1480, 356);
            this.splitContainerBottom.SplitterDistance = 1480;
            this.splitContainerBottom.TabIndex = 0;

            // tabControlData
            this.tabControlData.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlData.Multiline = true;
            this.tabControlData.Controls.Add(this.tabIkou);
            this.tabControlData.Controls.Add(this.tabIbutu);
            this.tabControlData.Controls.Add(this.tabKikai);
            this.tabControlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlData.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tabControlData.Location = new System.Drawing.Point(0, 0);
            this.tabControlData.Name = "tabControlData";
            this.tabControlData.SelectedIndex = 0;
            this.tabControlData.Size = new System.Drawing.Size(1480, 356);
            this.tabControlData.TabIndex = 0;

            // tabIkou
            this.tabIkou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.tabIkou.Controls.Add(this.pnlPrecsRight);
            this.tabIkou.Controls.Add(this.grpPrecs);
            this.tabIkou.Controls.Add(this.grpIkouL);
            this.tabIkou.Controls.Add(this.grpIkouMaster);
            this.tabIkou.Location = new System.Drawing.Point(4, 26);
            this.tabIkou.Name = "tabIkou";
            this.tabIkou.Padding = new System.Windows.Forms.Padding(3);
            this.tabIkou.Size = new System.Drawing.Size(1472, 326);
            this.tabIkou.TabIndex = 0;
            this.tabIkou.Text = "遺構";

            // grpIkouMaster
            this.grpIkouMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpIkouMaster.Controls.Add(this.btnDeleteIkouRight);
            this.grpIkouMaster.Controls.Add(this.btnUpdateIkouRight);
            this.grpIkouMaster.Controls.Add(this.btnAddIkou);
            this.grpIkouMaster.Controls.Add(this.btnSetPos);
            this.grpIkouMaster.Controls.Add(this.btnView3D);
            this.grpIkouMaster.Controls.Add(this.lblIkouNameTitle);
            this.grpIkouMaster.Controls.Add(this.lblIkouNameVal);
            this.grpIkouMaster.Controls.Add(this.btnMaxPlusOne);
            this.grpIkouMaster.Controls.Add(this.txtIkouNum);
            this.grpIkouMaster.Controls.Add(this.cmbIkouKind);
            this.grpIkouMaster.Controls.Add(this.lblSeqHeader);
            this.grpIkouMaster.Controls.Add(this.lblPrefixHeader);
            this.grpIkouMaster.Controls.Add(this.dgvIkou);
            this.grpIkouMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpIkouMaster.Location = new System.Drawing.Point(3, 3);
            this.grpIkouMaster.Name = "grpIkouMaster";
            this.grpIkouMaster.Size = new System.Drawing.Size(390, 320);
            this.grpIkouMaster.TabIndex = 0;
            this.grpIkouMaster.TabStop = false;
            this.grpIkouMaster.Text = "遺構";

            // dgvIkou
            this.dgvIkou.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvIkou.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIkou.Location = new System.Drawing.Point(6, 22);
            this.dgvIkou.Name = "dgvIkou";
            this.dgvIkou.RowTemplate.Height = 25;
            this.dgvIkou.Size = new System.Drawing.Size(196, 290);
            this.dgvIkou.TabIndex = 0;

            // lblPrefixHeader ("接頭")
            this.lblPrefixHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPrefixHeader.AutoSize = true;
            this.lblPrefixHeader.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPrefixHeader.ForeColor = System.Drawing.Color.White;
            this.lblPrefixHeader.Location = new System.Drawing.Point(208, 20);
            this.lblPrefixHeader.Name = "lblPrefixHeader";
            this.lblPrefixHeader.Size = new System.Drawing.Size(31, 15);
            this.lblPrefixHeader.TabIndex = 1;
            this.lblPrefixHeader.Text = "接頭";

            // lblSeqHeader ("連番")
            this.lblSeqHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSeqHeader.AutoSize = true;
            this.lblSeqHeader.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSeqHeader.ForeColor = System.Drawing.Color.White;
            this.lblSeqHeader.Location = new System.Drawing.Point(286, 20);
            this.lblSeqHeader.Name = "lblSeqHeader";
            this.lblSeqHeader.Size = new System.Drawing.Size(31, 15);
            this.lblSeqHeader.TabIndex = 2;
            this.lblSeqHeader.Text = "連番";

            // cmbIkouKind
            this.cmbIkouKind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbIkouKind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbIkouKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbIkouKind.ForeColor = System.Drawing.Color.White;
            this.cmbIkouKind.FormattingEnabled = true;
            this.cmbIkouKind.Location = new System.Drawing.Point(208, 38);
            this.cmbIkouKind.Name = "cmbIkouKind";
            this.cmbIkouKind.Size = new System.Drawing.Size(74, 25);
            this.cmbIkouKind.TabIndex = 3;

            // txtIkouNum
            this.txtIkouNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtIkouNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.txtIkouNum.ForeColor = System.Drawing.Color.Black;
            this.txtIkouNum.Location = new System.Drawing.Point(286, 38);
            this.txtIkouNum.MaxLength = 3;
            this.txtIkouNum.Name = "txtIkouNum";
            this.txtIkouNum.Size = new System.Drawing.Size(34, 24);
            this.txtIkouNum.TabIndex = 4;

            // btnMaxPlusOne
            this.btnMaxPlusOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMaxPlusOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnMaxPlusOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaxPlusOne.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMaxPlusOne.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.btnMaxPlusOne.Location = new System.Drawing.Point(324, 37);
            this.btnMaxPlusOne.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaxPlusOne.Name = "btnMaxPlusOne";
            this.btnMaxPlusOne.Padding = new System.Windows.Forms.Padding(0);
            this.btnMaxPlusOne.Size = new System.Drawing.Size(60, 26);
            this.btnMaxPlusOne.TabIndex = 5;
            this.btnMaxPlusOne.Text = "最大+1";
            this.btnMaxPlusOne.UseVisualStyleBackColor = false;

            // lblIkouNameTitle ("遺構名")
            this.lblIkouNameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIkouNameTitle.AutoSize = true;
            this.lblIkouNameTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIkouNameTitle.ForeColor = System.Drawing.Color.White;
            this.lblIkouNameTitle.Location = new System.Drawing.Point(208, 77);
            this.lblIkouNameTitle.Name = "lblIkouNameTitle";
            this.lblIkouNameTitle.Size = new System.Drawing.Size(47, 17);
            this.lblIkouNameTitle.TabIndex = 6;
            this.lblIkouNameTitle.Text = "遺構名";

            // lblIkouNameVal
            this.lblIkouNameVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIkouNameVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.lblIkouNameVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIkouNameVal.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIkouNameVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblIkouNameVal.Location = new System.Drawing.Point(260, 74);
            this.lblIkouNameVal.Name = "lblIkouNameVal";
            this.lblIkouNameVal.Size = new System.Drawing.Size(124, 25);
            this.lblIkouNameVal.TabIndex = 7;
            this.lblIkouNameVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnSetPos ("表示位置指定")
            this.btnSetPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnSetPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPos.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSetPos.ForeColor = System.Drawing.Color.White;
            this.btnSetPos.Location = new System.Drawing.Point(208, 118);
            this.btnSetPos.Name = "btnSetPos";
            this.btnSetPos.Size = new System.Drawing.Size(176, 28);
            this.btnSetPos.TabIndex = 8;
            this.btnSetPos.Text = "表示位置指定";
            this.btnSetPos.UseVisualStyleBackColor = false;

            // btnView3D ("🧊 3D確認")
            this.btnView3D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnView3D.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnView3D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView3D.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnView3D.ForeColor = System.Drawing.Color.White;
            this.btnView3D.Location = new System.Drawing.Point(208, 154);
            this.btnView3D.Name = "btnView3D";
            this.btnView3D.Size = new System.Drawing.Size(176, 28);
            this.btnView3D.TabIndex = 9;
            this.btnView3D.Text = "🧊 3D確認";
            this.btnView3D.UseVisualStyleBackColor = false;

            // btnDeleteIkouRight (Bottom Row: 1. 削除)
            this.btnDeleteIkouRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteIkouRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnDeleteIkouRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteIkouRight.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDeleteIkouRight.ForeColor = System.Drawing.Color.White;
            this.btnDeleteIkouRight.Location = new System.Drawing.Point(208, 284);
            this.btnDeleteIkouRight.Name = "btnDeleteIkouRight";
            this.btnDeleteIkouRight.Size = new System.Drawing.Size(54, 28);
            this.btnDeleteIkouRight.TabIndex = 10;
            this.btnDeleteIkouRight.Text = "削除";
            this.btnDeleteIkouRight.UseVisualStyleBackColor = false;

            // btnUpdateIkouRight (Bottom Row: 2. 更新)
            this.btnUpdateIkouRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateIkouRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(184)))), ((int)(((byte)(1)))));
            this.btnUpdateIkouRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateIkouRight.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateIkouRight.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateIkouRight.Location = new System.Drawing.Point(268, 284);
            this.btnUpdateIkouRight.Name = "btnUpdateIkouRight";
            this.btnUpdateIkouRight.Size = new System.Drawing.Size(54, 28);
            this.btnUpdateIkouRight.TabIndex = 11;
            this.btnUpdateIkouRight.Text = "更新";
            this.btnUpdateIkouRight.UseVisualStyleBackColor = false;

            // btnAddIkou (Bottom Row: 3. 追加)
            this.btnAddIkou.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddIkou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnAddIkou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddIkou.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddIkou.ForeColor = System.Drawing.Color.White;
            this.btnAddIkou.Location = new System.Drawing.Point(328, 284);
            this.btnAddIkou.Name = "btnAddIkou";
            this.btnAddIkou.Size = new System.Drawing.Size(56, 28);
            this.btnAddIkou.TabIndex = 12;
            this.btnAddIkou.Text = "追加";
            this.btnAddIkou.UseVisualStyleBackColor = false;

            // grpIkouL
            this.grpIkouL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpIkouL.Controls.Add(this.btnAddIkouL);
            this.grpIkouL.Controls.Add(this.btnUpdateLineRight);
            this.grpIkouL.Controls.Add(this.btnDeleteLineRight);
            this.grpIkouL.Controls.Add(this.btnLineSetPos);
            this.grpIkouL.Controls.Add(this.cmbLineIkouMaster);
            this.grpIkouL.Controls.Add(this.lblLineIkouMaster);
            this.grpIkouL.Controls.Add(this.cmbLineLayer);
            this.grpIkouL.Controls.Add(this.lblLineLayer);
            this.grpIkouL.Controls.Add(this.rdoLinePoint);
            this.grpIkouL.Controls.Add(this.rdoLineClosed);
            this.grpIkouL.Controls.Add(this.rdoLineOpen);
            this.grpIkouL.Controls.Add(this.lblLineNameTitle);
            this.grpIkouL.Controls.Add(this.lblLineNameVal);
            this.grpIkouL.Controls.Add(this.btnLineMaxPlusOne);
            this.grpIkouL.Controls.Add(this.txtLineNum);
            this.grpIkouL.Controls.Add(this.cmbLineKind);
            this.grpIkouL.Controls.Add(this.lblLineSeqHeader);
            this.grpIkouL.Controls.Add(this.lblLinePrefixHeader);
            this.grpIkouL.Controls.Add(this.dgvIkouL);
            this.grpIkouL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpIkouL.Location = new System.Drawing.Point(399, 3);
            this.grpIkouL.Name = "grpIkouL";
            this.grpIkouL.Size = new System.Drawing.Size(422, 320);
            this.grpIkouL.TabIndex = 1;
            this.grpIkouL.TabStop = false;
            this.grpIkouL.Text = "遺構L";

            // dgvIkouL
            this.dgvIkouL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvIkouL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIkouL.Location = new System.Drawing.Point(6, 22);
            this.dgvIkouL.Name = "dgvIkouL";
            this.dgvIkouL.RowTemplate.Height = 25;
            this.dgvIkouL.Size = new System.Drawing.Size(235, 290);
            this.dgvIkouL.TabIndex = 0;

            // lblLinePrefixHeader ("接頭")
            this.lblLinePrefixHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLinePrefixHeader.AutoSize = true;
            this.lblLinePrefixHeader.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLinePrefixHeader.ForeColor = System.Drawing.Color.White;
            this.lblLinePrefixHeader.Location = new System.Drawing.Point(247, 20);
            this.lblLinePrefixHeader.Name = "lblLinePrefixHeader";
            this.lblLinePrefixHeader.Size = new System.Drawing.Size(31, 15);
            this.lblLinePrefixHeader.TabIndex = 1;
            this.lblLinePrefixHeader.Text = "接頭";

            // lblLineSeqHeader ("連番")
            this.lblLineSeqHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLineSeqHeader.AutoSize = true;
            this.lblLineSeqHeader.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLineSeqHeader.ForeColor = System.Drawing.Color.White;
            this.lblLineSeqHeader.Location = new System.Drawing.Point(316, 20);
            this.lblLineSeqHeader.Name = "lblLineSeqHeader";
            this.lblLineSeqHeader.Size = new System.Drawing.Size(31, 15);
            this.lblLineSeqHeader.TabIndex = 2;
            this.lblLineSeqHeader.Text = "連番";

            // cmbLineKind
            this.cmbLineKind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbLineKind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbLineKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbLineKind.ForeColor = System.Drawing.Color.White;
            this.cmbLineKind.FormattingEnabled = true;
            this.cmbLineKind.Location = new System.Drawing.Point(247, 38);
            this.cmbLineKind.Name = "cmbLineKind";
            this.cmbLineKind.Size = new System.Drawing.Size(65, 25);
            this.cmbLineKind.TabIndex = 3;

            // txtLineNum
            this.txtLineNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLineNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.txtLineNum.ForeColor = System.Drawing.Color.Black;
            this.txtLineNum.Location = new System.Drawing.Point(316, 38);
            this.txtLineNum.MaxLength = 3;
            this.txtLineNum.Name = "txtLineNum";
            this.txtLineNum.Size = new System.Drawing.Size(34, 24);
            this.txtLineNum.TabIndex = 4;

            // btnLineMaxPlusOne
            this.btnLineMaxPlusOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLineMaxPlusOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnLineMaxPlusOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLineMaxPlusOne.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLineMaxPlusOne.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.btnLineMaxPlusOne.Location = new System.Drawing.Point(354, 37);
            this.btnLineMaxPlusOne.Margin = new System.Windows.Forms.Padding(0);
            this.btnLineMaxPlusOne.Name = "btnLineMaxPlusOne";
            this.btnLineMaxPlusOne.Padding = new System.Windows.Forms.Padding(0);
            this.btnLineMaxPlusOne.Size = new System.Drawing.Size(60, 26);
            this.btnLineMaxPlusOne.TabIndex = 5;
            this.btnLineMaxPlusOne.Text = "最大+1";
            this.btnLineMaxPlusOne.UseVisualStyleBackColor = false;

            // lblLineNameTitle ("線名")
            this.lblLineNameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLineNameTitle.AutoSize = true;
            this.lblLineNameTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLineNameTitle.ForeColor = System.Drawing.Color.White;
            this.lblLineNameTitle.Location = new System.Drawing.Point(247, 77);
            this.lblLineNameTitle.Name = "lblLineNameTitle";
            this.lblLineNameTitle.Size = new System.Drawing.Size(35, 17);
            this.lblLineNameTitle.TabIndex = 6;
            this.lblLineNameTitle.Text = "線名";

            // lblLineNameVal
            this.lblLineNameVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLineNameVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.lblLineNameVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLineNameVal.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLineNameVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblLineNameVal.Location = new System.Drawing.Point(287, 74);
            this.lblLineNameVal.Name = "lblLineNameVal";
            this.lblLineNameVal.Size = new System.Drawing.Size(127, 25);
            this.lblLineNameVal.TabIndex = 7;
            this.lblLineNameVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // rdoLineOpen (Row 3: 開放)
            this.rdoLineOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoLineOpen.AutoSize = true;
            this.rdoLineOpen.Checked = true;
            this.rdoLineOpen.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rdoLineOpen.ForeColor = System.Drawing.Color.White;
            this.rdoLineOpen.Location = new System.Drawing.Point(247, 110);
            this.rdoLineOpen.Name = "rdoLineOpen";
            this.rdoLineOpen.Size = new System.Drawing.Size(50, 19);
            this.rdoLineOpen.TabIndex = 8;
            this.rdoLineOpen.TabStop = true;
            this.rdoLineOpen.Text = "開放";
            this.rdoLineOpen.UseVisualStyleBackColor = true;

            // rdoLineClosed (Row 3: 閉合)
            this.rdoLineClosed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoLineClosed.AutoSize = true;
            this.rdoLineClosed.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rdoLineClosed.ForeColor = System.Drawing.Color.White;
            this.rdoLineClosed.Location = new System.Drawing.Point(301, 110);
            this.rdoLineClosed.Name = "rdoLineClosed";
            this.rdoLineClosed.Size = new System.Drawing.Size(50, 19);
            this.rdoLineClosed.TabIndex = 9;
            this.rdoLineClosed.Text = "閉合";
            this.rdoLineClosed.UseVisualStyleBackColor = true;

            // rdoLinePoint (Row 3: 標高点)
            this.rdoLinePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoLinePoint.AutoSize = true;
            this.rdoLinePoint.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rdoLinePoint.ForeColor = System.Drawing.Color.White;
            this.rdoLinePoint.Location = new System.Drawing.Point(355, 110);
            this.rdoLinePoint.Name = "rdoLinePoint";
            this.rdoLinePoint.Size = new System.Drawing.Size(65, 19);
            this.rdoLinePoint.TabIndex = 10;
            this.rdoLinePoint.Text = "標高点";
            this.rdoLinePoint.UseVisualStyleBackColor = true;

            // lblLineLayer (Row 4: レイヤ)
            this.lblLineLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLineLayer.AutoSize = true;
            this.lblLineLayer.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLineLayer.ForeColor = System.Drawing.Color.White;
            this.lblLineLayer.Location = new System.Drawing.Point(247, 145);
            this.lblLineLayer.Name = "lblLineLayer";
            this.lblLineLayer.Size = new System.Drawing.Size(41, 15);
            this.lblLineLayer.TabIndex = 11;
            this.lblLineLayer.Text = "レイヤ";

            // cmbLineLayer (Row 4: レイヤ選択のComboBox)
            this.cmbLineLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbLineLayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbLineLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineLayer.ForeColor = System.Drawing.Color.White;
            this.cmbLineLayer.FormattingEnabled = true;
            this.cmbLineLayer.Location = new System.Drawing.Point(287, 142);
            this.cmbLineLayer.Name = "cmbLineLayer";
            this.cmbLineLayer.Size = new System.Drawing.Size(127, 25);
            this.cmbLineLayer.TabIndex = 12;

            // lblLineIkouMaster (Row 5: 遺構)
            this.lblLineIkouMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLineIkouMaster.AutoSize = true;
            this.lblLineIkouMaster.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLineIkouMaster.ForeColor = System.Drawing.Color.White;
            this.lblLineIkouMaster.Location = new System.Drawing.Point(247, 177);
            this.lblLineIkouMaster.Name = "lblLineIkouMaster";
            this.lblLineIkouMaster.Size = new System.Drawing.Size(31, 15);
            this.lblLineIkouMaster.TabIndex = 13;
            this.lblLineIkouMaster.Text = "遺構";

            // cmbLineIkouMaster (Row 5: 遺構選択 ComboBox DropDownList)
            this.cmbLineIkouMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbLineIkouMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbLineIkouMaster.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineIkouMaster.ForeColor = System.Drawing.Color.White;
            this.cmbLineIkouMaster.FormattingEnabled = true;
            this.cmbLineIkouMaster.Location = new System.Drawing.Point(287, 174);
            this.cmbLineIkouMaster.Name = "cmbLineIkouMaster";
            this.cmbLineIkouMaster.Size = new System.Drawing.Size(127, 25);
            this.cmbLineIkouMaster.TabIndex = 14;

            // btnLineSetPos (Row 6: 表示位置指定)
            this.btnLineSetPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLineSetPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnLineSetPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLineSetPos.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLineSetPos.ForeColor = System.Drawing.Color.White;
            this.btnLineSetPos.Location = new System.Drawing.Point(247, 218);
            this.btnLineSetPos.Name = "btnLineSetPos";
            this.btnLineSetPos.Size = new System.Drawing.Size(167, 28);
            this.btnLineSetPos.TabIndex = 15;
            this.btnLineSetPos.Text = "表示位置指定";
            this.btnLineSetPos.UseVisualStyleBackColor = false;

            // btnDeleteLineRight (Bottom Row: 1. 削除)
            this.btnDeleteLineRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteLineRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnDeleteLineRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteLineRight.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDeleteLineRight.ForeColor = System.Drawing.Color.White;
            this.btnDeleteLineRight.Location = new System.Drawing.Point(247, 284);
            this.btnDeleteLineRight.Name = "btnDeleteLineRight";
            this.btnDeleteLineRight.Size = new System.Drawing.Size(50, 28);
            this.btnDeleteLineRight.TabIndex = 13;
            this.btnDeleteLineRight.Text = "削除";
            this.btnDeleteLineRight.UseVisualStyleBackColor = false;

            // btnUpdateLineRight (Bottom Row: 2. 更新)
            this.btnUpdateLineRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateLineRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(184)))), ((int)(((byte)(1)))));
            this.btnUpdateLineRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateLineRight.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateLineRight.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateLineRight.Location = new System.Drawing.Point(301, 284);
            this.btnUpdateLineRight.Name = "btnUpdateLineRight";
            this.btnUpdateLineRight.Size = new System.Drawing.Size(50, 28);
            this.btnUpdateLineRight.TabIndex = 14;
            this.btnUpdateLineRight.Text = "更新";
            this.btnUpdateLineRight.UseVisualStyleBackColor = false;

            // btnAddIkouL (Bottom Row: 3. 追加)
            this.btnAddIkouL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddIkouL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnAddIkouL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddIkouL.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddIkouL.ForeColor = System.Drawing.Color.White;
            this.btnAddIkouL.Location = new System.Drawing.Point(355, 284);
            this.btnAddIkouL.Name = "btnAddIkouL";
            this.btnAddIkouL.Size = new System.Drawing.Size(59, 28);
            this.btnAddIkouL.TabIndex = 15;
            this.btnAddIkouL.Text = "追加";
            this.btnAddIkouL.UseVisualStyleBackColor = false;

            // grpPrecs
            this.grpPrecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPrecs.Controls.Add(this.dgvPrecs);
            this.grpPrecs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpPrecs.Location = new System.Drawing.Point(827, 3);
            this.grpPrecs.Name = "grpPrecs";
            this.grpPrecs.Size = new System.Drawing.Size(460, 320);
            this.grpPrecs.TabIndex = 2;
            this.grpPrecs.TabStop = false;
            this.grpPrecs.Text = "構成座標";

            // dgvPrecs
            this.dgvPrecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPrecs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrecs.Location = new System.Drawing.Point(6, 22);
            this.dgvPrecs.Name = "dgvPrecs";
            this.dgvPrecs.RowTemplate.Height = 25;
            this.dgvPrecs.Size = new System.Drawing.Size(448, 290);
            this.dgvPrecs.TabIndex = 0;

            // pnlPrecsRight
            this.pnlPrecsRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPrecsRight.Controls.Add(this.grpCoordValue);
            this.pnlPrecsRight.Location = new System.Drawing.Point(1291, 3);
            this.pnlPrecsRight.Name = "pnlPrecsRight";
            this.pnlPrecsRight.Size = new System.Drawing.Size(178, 320);
            this.pnlPrecsRight.TabIndex = 3;

            // grpCoordValue
            this.grpCoordValue.Controls.Add(this.lblCoordX);
            this.grpCoordValue.Controls.Add(this.txtCoordX);
            this.grpCoordValue.Controls.Add(this.lblCoordY);
            this.grpCoordValue.Controls.Add(this.txtCoordY);
            this.grpCoordValue.Controls.Add(this.lblCoordZ);
            this.grpCoordValue.Controls.Add(this.txtCoordZ);
            this.grpCoordValue.Controls.Add(this.chkScreenInput);
            this.grpCoordValue.Controls.Add(this.btnDeletePointRight);
            this.grpCoordValue.Controls.Add(this.btnUpdatePointRight);
            this.grpCoordValue.Controls.Add(this.btnAddPointRight);
            this.grpCoordValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCoordValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpCoordValue.Location = new System.Drawing.Point(0, 0);
            this.grpCoordValue.Name = "grpCoordValue";
            this.grpCoordValue.Size = new System.Drawing.Size(178, 320);
            this.grpCoordValue.TabIndex = 0;
            this.grpCoordValue.TabStop = false;
            this.grpCoordValue.Text = "座標値";

            // lblCoordX
            this.lblCoordX.AutoSize = true;
            this.lblCoordX.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCoordX.ForeColor = System.Drawing.Color.White;
            this.lblCoordX.Location = new System.Drawing.Point(8, 28);
            this.lblCoordX.Name = "lblCoordX";
            this.lblCoordX.Size = new System.Drawing.Size(19, 17);
            this.lblCoordX.TabIndex = 1;
            this.lblCoordX.Text = "X:";

            // txtCoordX
            this.txtCoordX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.txtCoordX.ForeColor = System.Drawing.Color.Black;
            this.txtCoordX.Location = new System.Drawing.Point(32, 25);
            this.txtCoordX.Name = "txtCoordX";
            this.txtCoordX.Size = new System.Drawing.Size(130, 24);
            this.txtCoordX.TabIndex = 2;
            this.txtCoordX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // lblCoordY
            this.lblCoordY.AutoSize = true;
            this.lblCoordY.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCoordY.ForeColor = System.Drawing.Color.White;
            this.lblCoordY.Location = new System.Drawing.Point(8, 68);
            this.lblCoordY.Name = "lblCoordY";
            this.lblCoordY.Size = new System.Drawing.Size(18, 17);
            this.lblCoordY.TabIndex = 3;
            this.lblCoordY.Text = "Y:";

            // txtCoordY
            this.txtCoordY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.txtCoordY.ForeColor = System.Drawing.Color.Black;
            this.txtCoordY.Location = new System.Drawing.Point(32, 65);
            this.txtCoordY.Name = "txtCoordY";
            this.txtCoordY.Size = new System.Drawing.Size(130, 24);
            this.txtCoordY.TabIndex = 4;
            this.txtCoordY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // lblCoordZ
            this.lblCoordZ.AutoSize = true;
            this.lblCoordZ.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCoordZ.ForeColor = System.Drawing.Color.White;
            this.lblCoordZ.Location = new System.Drawing.Point(8, 108);
            this.lblCoordZ.Name = "lblCoordZ";
            this.lblCoordZ.Size = new System.Drawing.Size(18, 17);
            this.lblCoordZ.TabIndex = 5;
            this.lblCoordZ.Text = "Z:";

            // txtCoordZ
            this.txtCoordZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.txtCoordZ.ForeColor = System.Drawing.Color.Black;
            this.txtCoordZ.Location = new System.Drawing.Point(32, 105);
            this.txtCoordZ.Name = "txtCoordZ";
            this.txtCoordZ.Size = new System.Drawing.Size(130, 24);
            this.txtCoordZ.TabIndex = 6;
            this.txtCoordZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // chkScreenInput
            this.chkScreenInput.AutoSize = true;
            this.chkScreenInput.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkScreenInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(35)))), ((int)(((byte)(60)))));
            this.chkScreenInput.Location = new System.Drawing.Point(8, 148);
            this.chkScreenInput.Name = "chkScreenInput";
            this.chkScreenInput.Size = new System.Drawing.Size(102, 21);
            this.chkScreenInput.TabIndex = 7;
            this.chkScreenInput.Text = "📌 画面入力";
            this.chkScreenInput.UseVisualStyleBackColor = true;

            // btnDeletePointRight
            this.btnDeletePointRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeletePointRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnDeletePointRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePointRight.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDeletePointRight.ForeColor = System.Drawing.Color.White;
            this.btnDeletePointRight.Location = new System.Drawing.Point(8, 284);
            this.btnDeletePointRight.Name = "btnDeletePointRight";
            this.btnDeletePointRight.Size = new System.Drawing.Size(48, 28);
            this.btnDeletePointRight.TabIndex = 8;
            this.btnDeletePointRight.Text = "削除";
            this.btnDeletePointRight.UseVisualStyleBackColor = false;

            // btnUpdatePointRight
            this.btnUpdatePointRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdatePointRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(184)))), ((int)(((byte)(1)))));
            this.btnUpdatePointRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdatePointRight.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdatePointRight.ForeColor = System.Drawing.Color.Black;
            this.btnUpdatePointRight.Location = new System.Drawing.Point(62, 284);
            this.btnUpdatePointRight.Name = "btnUpdatePointRight";
            this.btnUpdatePointRight.Size = new System.Drawing.Size(48, 28);
            this.btnUpdatePointRight.TabIndex = 9;
            this.btnUpdatePointRight.Text = "更新";
            this.btnUpdatePointRight.UseVisualStyleBackColor = false;

            // btnAddPointRight
            this.btnAddPointRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddPointRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnAddPointRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPointRight.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddPointRight.ForeColor = System.Drawing.Color.White;
            this.btnAddPointRight.Location = new System.Drawing.Point(116, 284);
            this.btnAddPointRight.Name = "btnAddPointRight";
            this.btnAddPointRight.Size = new System.Drawing.Size(48, 28);
            this.btnAddPointRight.TabIndex = 10;
            this.btnAddPointRight.Text = "追加";
            this.btnAddPointRight.UseVisualStyleBackColor = false;

            // tabIbutu
            this.tabIbutu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.tabIbutu.Controls.Add(this.pnlIbutuRight);
            this.tabIbutu.Controls.Add(this.dgvIbutu);
            this.tabIbutu.Location = new System.Drawing.Point(4, 26);
            this.tabIbutu.Name = "tabIbutu";
            this.tabIbutu.Padding = new System.Windows.Forms.Padding(3);
            this.tabIbutu.Size = new System.Drawing.Size(1472, 326);
            this.tabIbutu.TabIndex = 1;
            this.tabIbutu.Text = "遺物";

            // pnlIbutuRight
            this.pnlIbutuRight.Controls.Add(this.grpIbutuRecord);
            this.pnlIbutuRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlIbutuRight.Location = new System.Drawing.Point(1040, 3);
            this.pnlIbutuRight.Name = "pnlIbutuRight";
            this.pnlIbutuRight.Size = new System.Drawing.Size(425, 320);
            this.pnlIbutuRight.TabIndex = 5;

            // grpIbutuRecord
            this.grpIbutuRecord.Controls.Add(this.lblFilterIbutu);
            this.grpIbutuRecord.Controls.Add(this.txtFilterIbutu);
            this.grpIbutuRecord.Controls.Add(this.lblIbutuChiku);
            this.grpIbutuRecord.Controls.Add(this.cmbIbutuChiku);
            this.grpIbutuRecord.Controls.Add(this.lblIbutuSoui);
            this.grpIbutuRecord.Controls.Add(this.cmbIbutuSoui);
            this.grpIbutuRecord.Controls.Add(this.lblIbutuSyubetu);
            this.grpIbutuRecord.Controls.Add(this.cmbIbutuSyubetu);
            this.grpIbutuRecord.Controls.Add(this.lblIbutuLayer);
            this.grpIbutuRecord.Controls.Add(this.cmbIbutuLayer);
            this.grpIbutuRecord.Controls.Add(this.lblIbutuNo);
            this.grpIbutuRecord.Controls.Add(this.txtIbutuNo);
            this.grpIbutuRecord.Controls.Add(this.chkIbutuAutoInc);
            this.grpIbutuRecord.Controls.Add(this.btnIbutuMaxPlusOne);
            this.grpIbutuRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpIbutuRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpIbutuRecord.Location = new System.Drawing.Point(0, 0);
            this.grpIbutuRecord.Name = "grpIbutuRecord";
            this.grpIbutuRecord.Size = new System.Drawing.Size(238, 315);
            this.grpIbutuRecord.TabIndex = 0;
            this.grpIbutuRecord.TabStop = false;
            this.grpIbutuRecord.Text = "📦 遺物属性・検索";

            // lblFilterIbutu
            this.lblFilterIbutu.AutoSize = true;
            this.lblFilterIbutu.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblFilterIbutu.ForeColor = System.Drawing.Color.White;
            this.lblFilterIbutu.Location = new System.Drawing.Point(6, 20);
            this.lblFilterIbutu.Name = "lblFilterIbutu";
            this.lblFilterIbutu.Size = new System.Drawing.Size(70, 15);
            this.lblFilterIbutu.TabIndex = 0;
            this.lblFilterIbutu.Text = "🔍 絞り込み:";

            // txtFilterIbutu
            this.txtFilterIbutu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.txtFilterIbutu.ForeColor = System.Drawing.Color.White;
            this.txtFilterIbutu.Location = new System.Drawing.Point(80, 17);
            this.txtFilterIbutu.Name = "txtFilterIbutu";
            this.txtFilterIbutu.Size = new System.Drawing.Size(148, 23);
            this.txtFilterIbutu.TabIndex = 1;

            // lblIbutuChiku
            this.lblIbutuChiku.AutoSize = true;
            this.lblIbutuChiku.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblIbutuChiku.ForeColor = System.Drawing.Color.White;
            this.lblIbutuChiku.Location = new System.Drawing.Point(6, 44);
            this.lblIbutuChiku.Name = "lblIbutuChiku";
            this.lblIbutuChiku.Size = new System.Drawing.Size(79, 15);
            this.lblIbutuChiku.TabIndex = 2;
            this.lblIbutuChiku.Text = "出土地点/遺構";

            // cmbIbutuChiku
            this.cmbIbutuChiku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(190)))));
            this.cmbIbutuChiku.ForeColor = System.Drawing.Color.Black;
            this.cmbIbutuChiku.FormattingEnabled = true;
            this.cmbIbutuChiku.Location = new System.Drawing.Point(6, 61);
            this.cmbIbutuChiku.Name = "cmbIbutuChiku";
            this.cmbIbutuChiku.Size = new System.Drawing.Size(222, 23);
            this.cmbIbutuChiku.TabIndex = 3;

            // lblIbutuSoui
            this.lblIbutuSoui.AutoSize = true;
            this.lblIbutuSoui.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblIbutuSoui.ForeColor = System.Drawing.Color.White;
            this.lblIbutuSoui.Location = new System.Drawing.Point(6, 88);
            this.lblIbutuSoui.Name = "lblIbutuSoui";
            this.lblIbutuSoui.Size = new System.Drawing.Size(55, 15);
            this.lblIbutuSoui.TabIndex = 4;
            this.lblIbutuSoui.Text = "出土層位";

            // cmbIbutuSoui
            this.cmbIbutuSoui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(190)))));
            this.cmbIbutuSoui.ForeColor = System.Drawing.Color.Black;
            this.cmbIbutuSoui.FormattingEnabled = true;
            this.cmbIbutuSoui.Location = new System.Drawing.Point(6, 105);
            this.cmbIbutuSoui.Name = "cmbIbutuSoui";
            this.cmbIbutuSoui.Size = new System.Drawing.Size(222, 23);
            this.cmbIbutuSoui.TabIndex = 5;

            // lblIbutuSyubetu
            this.lblIbutuSyubetu.AutoSize = true;
            this.lblIbutuSyubetu.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblIbutuSyubetu.ForeColor = System.Drawing.Color.White;
            this.lblIbutuSyubetu.Location = new System.Drawing.Point(6, 132);
            this.lblIbutuSyubetu.Name = "lblIbutuSyubetu";
            this.lblIbutuSyubetu.Size = new System.Drawing.Size(31, 15);
            this.lblIbutuSyubetu.TabIndex = 6;
            this.lblIbutuSyubetu.Text = "種別";

            // cmbIbutuSyubetu
            this.cmbIbutuSyubetu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(190)))));
            this.cmbIbutuSyubetu.ForeColor = System.Drawing.Color.Black;
            this.cmbIbutuSyubetu.FormattingEnabled = true;
            this.cmbIbutuSyubetu.Location = new System.Drawing.Point(6, 149);
            this.cmbIbutuSyubetu.Name = "cmbIbutuSyubetu";
            this.cmbIbutuSyubetu.Size = new System.Drawing.Size(160, 23);
            this.cmbIbutuSyubetu.TabIndex = 7;

            // lblIbutuLayer
            this.lblIbutuLayer.AutoSize = true;
            this.lblIbutuLayer.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblIbutuLayer.ForeColor = System.Drawing.Color.White;
            this.lblIbutuLayer.Location = new System.Drawing.Point(6, 178);
            this.lblIbutuLayer.Name = "lblIbutuLayer";
            this.lblIbutuLayer.Size = new System.Drawing.Size(34, 15);
            this.lblIbutuLayer.TabIndex = 8;
            this.lblIbutuLayer.Text = "レイヤ";

            // cmbIbutuLayer
            this.cmbIbutuLayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbIbutuLayer.ForeColor = System.Drawing.Color.White;
            this.cmbIbutuLayer.FormattingEnabled = true;
            this.cmbIbutuLayer.Location = new System.Drawing.Point(50, 175);
            this.cmbIbutuLayer.Name = "cmbIbutuLayer";
            this.cmbIbutuLayer.Size = new System.Drawing.Size(178, 23);
            this.cmbIbutuLayer.TabIndex = 9;

            // lblIbutuNo
            this.lblIbutuNo.AutoSize = true;
            this.lblIbutuNo.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblIbutuNo.ForeColor = System.Drawing.Color.White;
            this.lblIbutuNo.Location = new System.Drawing.Point(6, 208);
            this.lblIbutuNo.Name = "lblIbutuNo";
            this.lblIbutuNo.Size = new System.Drawing.Size(55, 15);
            this.lblIbutuNo.TabIndex = 10;
            this.lblIbutuNo.Text = "遺物番号";

            // txtIbutuNo
            this.txtIbutuNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.txtIbutuNo.ForeColor = System.Drawing.Color.White;
            this.txtIbutuNo.Location = new System.Drawing.Point(68, 205);
            this.txtIbutuNo.MaxLength = 3;
            this.txtIbutuNo.Name = "txtIbutuNo";
            this.txtIbutuNo.Size = new System.Drawing.Size(35, 23);
            this.txtIbutuNo.TabIndex = 11;
            this.txtIbutuNo.Text = "1";

            // chkIbutuAutoInc
            this.chkIbutuAutoInc.AutoSize = true;
            this.chkIbutuAutoInc.Checked = true;
            this.chkIbutuAutoInc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIbutuAutoInc.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.chkIbutuAutoInc.ForeColor = System.Drawing.Color.White;
            this.chkIbutuAutoInc.Location = new System.Drawing.Point(129, 207);
            this.chkIbutuAutoInc.Name = "chkIbutuAutoInc";
            this.chkIbutuAutoInc.Size = new System.Drawing.Size(95, 19);
            this.chkIbutuAutoInc.TabIndex = 12;
            this.chkIbutuAutoInc.Text = "新規で自動+";
            this.chkIbutuAutoInc.UseVisualStyleBackColor = true;

            // btnIbutuMaxPlusOne
            this.btnIbutuMaxPlusOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnIbutuMaxPlusOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIbutuMaxPlusOne.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnIbutuMaxPlusOne.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.btnIbutuMaxPlusOne.Location = new System.Drawing.Point(68, 233);
            this.btnIbutuMaxPlusOne.Name = "btnIbutuMaxPlusOne";
            this.btnIbutuMaxPlusOne.Size = new System.Drawing.Size(78, 26);
            this.btnIbutuMaxPlusOne.TabIndex = 13;
            this.btnIbutuMaxPlusOne.Text = "最大+1";
            this.btnIbutuMaxPlusOne.UseVisualStyleBackColor = false;

            // dgvIbutu
            this.dgvIbutu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIbutu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIbutu.Location = new System.Drawing.Point(12, 12);
            this.dgvIbutu.Name = "dgvIbutu";
            this.dgvIbutu.RowTemplate.Height = 25;
            this.dgvIbutu.Size = new System.Drawing.Size(1015, 300);
            this.dgvIbutu.TabIndex = 2;

            // tabKikai
            this.tabKikai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.tabKikai.Controls.Add(this.pnlKikaiRight);
            this.tabKikai.Controls.Add(this.dgvKikai);
            this.tabKikai.Location = new System.Drawing.Point(4, 26);
            this.tabKikai.Name = "tabKikai";
            this.tabKikai.Padding = new System.Windows.Forms.Padding(3);
            this.tabKikai.Size = new System.Drawing.Size(1472, 326);
            this.tabKikai.TabIndex = 2;
            this.tabKikai.Text = "基準点";

            // pnlKikaiRight
            this.pnlKikaiRight.Controls.Add(this.grpKikaiRecord);
            this.pnlKikaiRight.Controls.Add(this.grpPointGuidance);
            this.pnlKikaiRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlKikaiRight.Location = new System.Drawing.Point(860, 3);
            this.pnlKikaiRight.Name = "pnlKikaiRight";
            this.pnlKikaiRight.Size = new System.Drawing.Size(600, 320);
            this.pnlKikaiRight.TabIndex = 3;

            // grpKikaiRecord
            this.grpKikaiRecord.Controls.Add(this.lblKikaiName);
            this.grpKikaiRecord.Controls.Add(this.txtKikaiName);
            this.grpKikaiRecord.Controls.Add(this.lblKikaiLayer);
            this.grpKikaiRecord.Controls.Add(this.cmbKikaiLayer);
            this.grpKikaiRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKikaiRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpKikaiRecord.Location = new System.Drawing.Point(0, 0);
            this.grpKikaiRecord.Name = "grpKikaiRecord";
            this.grpKikaiRecord.Size = new System.Drawing.Size(205, 315);
            this.grpKikaiRecord.TabIndex = 0;
            this.grpKikaiRecord.TabStop = false;
            this.grpKikaiRecord.Text = "📍 基準点属性";

            // lblKikaiName
            this.lblKikaiName.AutoSize = true;
            this.lblKikaiName.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKikaiName.ForeColor = System.Drawing.Color.White;
            this.lblKikaiName.Location = new System.Drawing.Point(6, 25);
            this.lblKikaiName.Name = "lblKikaiName";
            this.lblKikaiName.Size = new System.Drawing.Size(55, 15);
            this.lblKikaiName.TabIndex = 0;
            this.lblKikaiName.Text = "基準点名";

            // txtKikaiName
            this.txtKikaiName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(190)))));
            this.txtKikaiName.ForeColor = System.Drawing.Color.Black;
            this.txtKikaiName.Location = new System.Drawing.Point(68, 22);
            this.txtKikaiName.Name = "txtKikaiName";
            this.txtKikaiName.Size = new System.Drawing.Size(128, 23);
            this.txtKikaiName.TabIndex = 1;

            // lblKikaiLayer
            this.lblKikaiLayer.AutoSize = true;
            this.lblKikaiLayer.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKikaiLayer.ForeColor = System.Drawing.Color.White;
            this.lblKikaiLayer.Location = new System.Drawing.Point(6, 65);
            this.lblKikaiLayer.Name = "lblKikaiLayer";
            this.lblKikaiLayer.Size = new System.Drawing.Size(34, 15);
            this.lblKikaiLayer.TabIndex = 2;
            this.lblKikaiLayer.Text = "レイヤ";

            // cmbKikaiLayer
            this.cmbKikaiLayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbKikaiLayer.ForeColor = System.Drawing.Color.White;
            this.cmbKikaiLayer.FormattingEnabled = true;
            this.cmbKikaiLayer.Location = new System.Drawing.Point(50, 62);
            this.cmbKikaiLayer.Name = "cmbKikaiLayer";
            this.cmbKikaiLayer.Size = new System.Drawing.Size(146, 23);
            this.cmbKikaiLayer.TabIndex = 3;

            // grpPointGuidance
            this.grpPointGuidance.Controls.Add(this.chkPointGuidance);
            this.grpPointGuidance.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpPointGuidance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpPointGuidance.Location = new System.Drawing.Point(210, 0);
            this.grpPointGuidance.Name = "grpPointGuidance";
            this.grpPointGuidance.Size = new System.Drawing.Size(200, 315);
            this.grpPointGuidance.TabIndex = 1;
            this.grpPointGuidance.TabStop = false;
            this.grpPointGuidance.Text = "🎯 点誘導";

            // chkPointGuidance
            this.chkPointGuidance.AutoSize = true;
            this.chkPointGuidance.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkPointGuidance.ForeColor = System.Drawing.Color.White;
            this.chkPointGuidance.Location = new System.Drawing.Point(12, 25);
            this.chkPointGuidance.Name = "chkPointGuidance";
            this.chkPointGuidance.Size = new System.Drawing.Size(63, 19);
            this.chkPointGuidance.TabIndex = 0;
            this.chkPointGuidance.Text = "点誘導";
            this.chkPointGuidance.UseVisualStyleBackColor = true;

            // dgvKikai
            this.dgvKikai.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKikai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKikai.Location = new System.Drawing.Point(12, 12);
            this.dgvKikai.Name = "dgvKikai";
            this.dgvKikai.RowTemplate.Height = 25;
            this.dgvKikai.Size = new System.Drawing.Size(835, 300);
            this.dgvKikai.TabIndex = 0;

            // tabLayer
            this.tabLayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.tabLayer.Controls.Add(this.btnDeleteLayer);
            this.tabLayer.Controls.Add(this.btnAddLayer);
            this.tabLayer.Controls.Add(this.dgvLayer);
            this.tabLayer.Location = new System.Drawing.Point(4, 26);
            this.tabLayer.Name = "tabLayer";
            this.tabLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayer.Size = new System.Drawing.Size(1112, 326);
            this.tabLayer.TabIndex = 3;
            this.tabLayer.Text = "レイヤ";

            // dgvLayer
            this.dgvLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLayer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayer.Location = new System.Drawing.Point(12, 12);
            this.dgvLayer.Name = "dgvLayer";
            this.dgvLayer.RowTemplate.Height = 25;
            this.dgvLayer.Size = new System.Drawing.Size(1088, 270);
            this.dgvLayer.TabIndex = 0;

            // btnAddLayer
            this.btnAddLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddLayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnAddLayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLayer.ForeColor = System.Drawing.Color.White;
            this.btnAddLayer.Location = new System.Drawing.Point(12, 288);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(110, 30);
            this.btnAddLayer.TabIndex = 1;
            this.btnAddLayer.Text = "➕ レイヤ追加";
            this.btnAddLayer.UseVisualStyleBackColor = false;

            // btnDeleteLayer
            this.btnDeleteLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteLayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnDeleteLayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteLayer.ForeColor = System.Drawing.Color.White;
            this.btnDeleteLayer.Location = new System.Drawing.Point(130, 288);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new System.Drawing.Size(110, 30);
            this.btnDeleteLayer.TabIndex = 2;
            this.btnDeleteLayer.Text = "🗑 レイヤ削除";
            this.btnDeleteLayer.UseVisualStyleBackColor = false;

            // tabBatchUpdate
            this.tabBatchUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.tabBatchUpdate.Controls.Add(this.dgvBatchPreview);
            this.tabBatchUpdate.Controls.Add(this.grpBatchOptions);
            this.tabBatchUpdate.Location = new System.Drawing.Point(4, 26);
            this.tabBatchUpdate.Name = "tabBatchUpdate";
            this.tabBatchUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabBatchUpdate.Size = new System.Drawing.Size(1112, 326);
            this.tabBatchUpdate.TabIndex = 4;
            this.tabBatchUpdate.Text = "一括更新";

            // grpBatchOptions
            this.grpBatchOptions.Controls.Add(this.lblBatchPreviewCount);
            this.grpBatchOptions.Controls.Add(this.btnBatchExecute);
            this.grpBatchOptions.Controls.Add(this.txtBatchUpdateVal);
            this.grpBatchOptions.Controls.Add(this.cmbBatchUpdateCol);
            this.grpBatchOptions.Controls.Add(this.lblBatchUpdate);
            this.grpBatchOptions.Controls.Add(this.txtBatchFilterVal);
            this.grpBatchOptions.Controls.Add(this.cmbBatchFilterOp);
            this.grpBatchOptions.Controls.Add(this.cmbBatchFilterCol);
            this.grpBatchOptions.Controls.Add(this.lblBatchFilter);
            this.grpBatchOptions.Controls.Add(this.cmbBatchTable);
            this.grpBatchOptions.Controls.Add(this.lblBatchTable);
            this.grpBatchOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBatchOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpBatchOptions.Location = new System.Drawing.Point(3, 3);
            this.grpBatchOptions.Name = "grpBatchOptions";
            this.grpBatchOptions.Size = new System.Drawing.Size(1106, 85);
            this.grpBatchOptions.TabIndex = 0;
            this.grpBatchOptions.TabStop = false;
            this.grpBatchOptions.Text = "一括置換・更新条件";

            // lblBatchTable
            this.lblBatchTable.AutoSize = true;
            this.lblBatchTable.ForeColor = System.Drawing.Color.White;
            this.lblBatchTable.Location = new System.Drawing.Point(10, 22);
            this.lblBatchTable.Name = "lblBatchTable";
            this.lblBatchTable.Size = new System.Drawing.Size(65, 17);
            this.lblBatchTable.TabIndex = 0;
            this.lblBatchTable.Text = "対象テーブル:";

            // cmbBatchTable
            this.cmbBatchTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbBatchTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchTable.ForeColor = System.Drawing.Color.White;
            this.cmbBatchTable.FormattingEnabled = true;
            this.cmbBatchTable.Location = new System.Drawing.Point(80, 19);
            this.cmbBatchTable.Name = "cmbBatchTable";
            this.cmbBatchTable.Size = new System.Drawing.Size(120, 25);
            this.cmbBatchTable.TabIndex = 1;

            // lblBatchFilter
            this.lblBatchFilter.AutoSize = true;
            this.lblBatchFilter.ForeColor = System.Drawing.Color.White;
            this.lblBatchFilter.Location = new System.Drawing.Point(215, 22);
            this.lblBatchFilter.Name = "lblBatchFilter";
            this.lblBatchFilter.Size = new System.Drawing.Size(55, 17);
            this.lblBatchFilter.TabIndex = 2;
            this.lblBatchFilter.Text = "絞り込み:";

            // cmbBatchFilterCol
            this.cmbBatchFilterCol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbBatchFilterCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchFilterCol.ForeColor = System.Drawing.Color.White;
            this.cmbBatchFilterCol.FormattingEnabled = true;
            this.cmbBatchFilterCol.Location = new System.Drawing.Point(275, 19);
            this.cmbBatchFilterCol.Name = "cmbBatchFilterCol";
            this.cmbBatchFilterCol.Size = new System.Drawing.Size(110, 25);
            this.cmbBatchFilterCol.TabIndex = 3;

            // cmbBatchFilterOp
            this.cmbBatchFilterOp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbBatchFilterOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchFilterOp.ForeColor = System.Drawing.Color.White;
            this.cmbBatchFilterOp.FormattingEnabled = true;
            this.cmbBatchFilterOp.Location = new System.Drawing.Point(390, 19);
            this.cmbBatchFilterOp.Name = "cmbBatchFilterOp";
            this.cmbBatchFilterOp.Size = new System.Drawing.Size(70, 25);
            this.cmbBatchFilterOp.TabIndex = 4;

            // txtBatchFilterVal
            this.txtBatchFilterVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.txtBatchFilterVal.ForeColor = System.Drawing.Color.White;
            this.txtBatchFilterVal.Location = new System.Drawing.Point(465, 19);
            this.txtBatchFilterVal.Name = "txtBatchFilterVal";
            this.txtBatchFilterVal.Size = new System.Drawing.Size(110, 24);
            this.txtBatchFilterVal.TabIndex = 5;

            // lblBatchUpdate
            this.lblBatchUpdate.AutoSize = true;
            this.lblBatchUpdate.ForeColor = System.Drawing.Color.White;
            this.lblBatchUpdate.Location = new System.Drawing.Point(590, 22);
            this.lblBatchUpdate.Name = "lblBatchUpdate";
            this.lblBatchUpdate.Size = new System.Drawing.Size(65, 17);
            this.lblBatchUpdate.TabIndex = 6;
            this.lblBatchUpdate.Text = "変更内容:";

            // cmbBatchUpdateCol
            this.cmbBatchUpdateCol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbBatchUpdateCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchUpdateCol.ForeColor = System.Drawing.Color.White;
            this.cmbBatchUpdateCol.FormattingEnabled = true;
            this.cmbBatchUpdateCol.Location = new System.Drawing.Point(660, 19);
            this.cmbBatchUpdateCol.Name = "cmbBatchUpdateCol";
            this.cmbBatchUpdateCol.Size = new System.Drawing.Size(110, 25);
            this.cmbBatchUpdateCol.TabIndex = 7;

            // txtBatchUpdateVal
            this.txtBatchUpdateVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.txtBatchUpdateVal.ForeColor = System.Drawing.Color.Black;
            this.txtBatchUpdateVal.Location = new System.Drawing.Point(775, 19);
            this.txtBatchUpdateVal.Name = "txtBatchUpdateVal";
            this.txtBatchUpdateVal.Size = new System.Drawing.Size(110, 24);
            this.txtBatchUpdateVal.TabIndex = 8;

            // btnBatchExecute
            this.btnBatchExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(184)))), ((int)(((byte)(1)))));
            this.btnBatchExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatchExecute.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBatchExecute.ForeColor = System.Drawing.Color.Black;
            this.btnBatchExecute.Location = new System.Drawing.Point(895, 17);
            this.btnBatchExecute.Name = "btnBatchExecute";
            this.btnBatchExecute.Size = new System.Drawing.Size(100, 28);
            this.btnBatchExecute.TabIndex = 9;
            this.btnBatchExecute.Text = "⚡ 一括更新実行";
            this.btnBatchExecute.UseVisualStyleBackColor = false;

            // lblBatchPreviewCount
            this.lblBatchPreviewCount.AutoSize = true;
            this.lblBatchPreviewCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblBatchPreviewCount.Location = new System.Drawing.Point(10, 55);
            this.lblBatchPreviewCount.Name = "lblBatchPreviewCount";
            this.lblBatchPreviewCount.Size = new System.Drawing.Size(125, 17);
            this.lblBatchPreviewCount.TabIndex = 10;
            this.lblBatchPreviewCount.Text = "対象レコード数: 0 件";

            // dgvBatchPreview
            this.dgvBatchPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBatchPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatchPreview.Location = new System.Drawing.Point(3, 88);
            this.dgvBatchPreview.Name = "dgvBatchPreview";
            this.dgvBatchPreview.RowTemplate.Height = 25;
            this.dgvBatchPreview.Size = new System.Drawing.Size(1106, 235);
            this.dgvBatchPreview.TabIndex = 1;

            // 
            // panelRightEdit (Property Controls SidePanel)
            // 
            // FormEditor
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);

            // FormEditor
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1520, 960);
            this.MinimumSize = new System.Drawing.Size(1280, 780);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelHeader);
            this.Name = "FormEditor";
            this.Text = "SITE7 SQLite Database Editor (2D CAD & Data Editor)";

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelMapHeader.ResumeLayout(false);
            this.panelMapHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMapCanvas)).EndInit();
            this.splitContainerBottom.Panel1.ResumeLayout(false);
            this.splitContainerBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBottom)).EndInit();
            this.splitContainerBottom.ResumeLayout(false);
            this.tabControlData.ResumeLayout(false);
            this.tabIkou.ResumeLayout(false);
            this.grpIkouMaster.ResumeLayout(false);
            this.grpIkouMaster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIkou)).EndInit();
            this.grpIkouL.ResumeLayout(false);
            this.grpIkouL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIkouL)).EndInit();
            this.grpPrecs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrecs)).EndInit();
            this.tabIbutu.ResumeLayout(false);
            this.tabIbutu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIbutu)).EndInit();
            this.tabKikai.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKikai)).EndInit();
            this.tabLayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayer)).EndInit();
            this.tabBatchUpdate.ResumeLayout(false);
            this.grpBatchOptions.ResumeLayout(false);
            this.grpBatchOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchPreview)).EndInit();
            this.grpPointEdit.SuspendLayout();
            this.grpPointEdit.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.Button btnOpenDb;
        private System.Windows.Forms.Label lblQuickDb;
        private System.Windows.Forms.ComboBox cmbQuickDbSelect;
        private System.Windows.Forms.Button btnSaveDb;
        private System.Windows.Forms.Label lblDbStatus;

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelMapHeader;
        private System.Windows.Forms.Label lblMapTitle;
        private System.Windows.Forms.Button btnResetMapZoom;
        private System.Windows.Forms.Panel panelMapLeft;
        private System.Windows.Forms.CheckBox chkShowIkou;
        private System.Windows.Forms.CheckBox chkShowIbutu;
        private System.Windows.Forms.CheckBox chkShowKikai;
        private System.Windows.Forms.CheckBox chkShowCurve;
        private System.Windows.Forms.CheckBox chkShowGrid;
        private System.Windows.Forms.CheckBox chkColorByIkou;
        private System.Windows.Forms.PictureBox picMapCanvas;

        private System.Windows.Forms.SplitContainer splitContainerBottom;
        private System.Windows.Forms.TabControl tabControlData;
        private System.Windows.Forms.TabPage tabIkou;
        private System.Windows.Forms.GroupBox grpIkouMaster;
        private System.Windows.Forms.DataGridView dgvIkou;
        private System.Windows.Forms.Button btnAddIkou;

        private System.Windows.Forms.GroupBox grpIkouL;
        private System.Windows.Forms.DataGridView dgvIkouL;

        private System.Windows.Forms.GroupBox grpPrecs;
        private System.Windows.Forms.DataGridView dgvPrecs;

        private System.Windows.Forms.TabPage tabIbutu;
        private System.Windows.Forms.GroupBox grpIbutuRecord;
        private System.Windows.Forms.Label lblFilterIbutu;
        private System.Windows.Forms.TextBox txtFilterIbutu;
        private System.Windows.Forms.Label lblIbutuChiku;
        private System.Windows.Forms.ComboBox cmbIbutuChiku;
        private System.Windows.Forms.Label lblIbutuSoui;
        private System.Windows.Forms.ComboBox cmbIbutuSoui;
        private System.Windows.Forms.Label lblIbutuSyubetu;
        private System.Windows.Forms.ComboBox cmbIbutuSyubetu;
        private System.Windows.Forms.Label lblIbutuLayer;
        private System.Windows.Forms.ComboBox cmbIbutuLayer;
        private System.Windows.Forms.Label lblIbutuNo;
        private System.Windows.Forms.TextBox txtIbutuNo;
        private System.Windows.Forms.CheckBox chkIbutuAutoInc;
        private System.Windows.Forms.Button btnIbutuMaxPlusOne;
        private System.Windows.Forms.DataGridView dgvIbutu;

        private System.Windows.Forms.TabPage tabKikai;
        private System.Windows.Forms.GroupBox grpKikaiRecord;
        private System.Windows.Forms.Label lblKikaiName;
        private System.Windows.Forms.TextBox txtKikaiName;
        private System.Windows.Forms.Label lblKikaiLayer;
        private System.Windows.Forms.ComboBox cmbKikaiLayer;
        private System.Windows.Forms.GroupBox grpPointGuidance;
        private System.Windows.Forms.CheckBox chkPointGuidance;
        private System.Windows.Forms.DataGridView dgvKikai;

        private System.Windows.Forms.TabPage tabLayer;
        private System.Windows.Forms.DataGridView dgvLayer;
        private System.Windows.Forms.Button btnAddLayer;
        private System.Windows.Forms.Button btnDeleteLayer;

        private System.Windows.Forms.TabPage tabBatchUpdate;
        private System.Windows.Forms.GroupBox grpBatchOptions;
        private System.Windows.Forms.Label lblBatchTable;
        private System.Windows.Forms.ComboBox cmbBatchTable;
        private System.Windows.Forms.Label lblBatchFilter;
        private System.Windows.Forms.ComboBox cmbBatchFilterCol;
        private System.Windows.Forms.ComboBox cmbBatchFilterOp;
        private System.Windows.Forms.TextBox txtBatchFilterVal;
        private System.Windows.Forms.Label lblBatchUpdate;
        private System.Windows.Forms.ComboBox cmbBatchUpdateCol;
        private System.Windows.Forms.TextBox txtBatchUpdateVal;
        private System.Windows.Forms.Button btnBatchExecute;
        private System.Windows.Forms.Label lblBatchPreviewCount;
        private System.Windows.Forms.DataGridView dgvBatchPreview;
        private System.Windows.Forms.Label lblPrefixHeader;
        private System.Windows.Forms.Label lblSeqHeader;
        private System.Windows.Forms.ComboBox cmbIkouKind;
        private System.Windows.Forms.TextBox txtIkouNum;
        private System.Windows.Forms.Label lblIkouNameTitle;
        private System.Windows.Forms.Label lblIkouNameVal;
        private System.Windows.Forms.Button btnMaxPlusOne;
        private System.Windows.Forms.Button btnSetPos;
        private System.Windows.Forms.Button btnView3D;
        private System.Windows.Forms.Button btnUpdateIkouRight;
        private System.Windows.Forms.Button btnDeleteIkouRight;

        private System.Windows.Forms.Label lblLinePrefixHeader;
        private System.Windows.Forms.Label lblLineSeqHeader;
        private System.Windows.Forms.ComboBox cmbLineKind;
        private System.Windows.Forms.TextBox txtLineNum;
        private System.Windows.Forms.Label lblLineNameTitle;
        private System.Windows.Forms.Label lblLineNameVal;
        private System.Windows.Forms.Button btnLineMaxPlusOne;
        private System.Windows.Forms.RadioButton rdoLineOpen;
        private System.Windows.Forms.RadioButton rdoLineClosed;
        private System.Windows.Forms.RadioButton rdoLinePoint;
        private System.Windows.Forms.Label lblLineLayer;
        private System.Windows.Forms.ComboBox cmbLineLayer;
        private System.Windows.Forms.Label lblLineIkouMaster;
        private System.Windows.Forms.ComboBox cmbLineIkouMaster;
        private System.Windows.Forms.Button btnLineSetPos;
        private System.Windows.Forms.Button btnDeleteLineRight;
        private System.Windows.Forms.Button btnUpdateLineRight;
        private System.Windows.Forms.Button btnAddIkouL;

        private System.Windows.Forms.GroupBox grpPointEdit;
        private System.Windows.Forms.Label lblCoordX;
        private System.Windows.Forms.TextBox txtCoordX;
        private System.Windows.Forms.Label lblCoordY;
        private System.Windows.Forms.TextBox txtCoordY;
        private System.Windows.Forms.Label lblCoordZ;
        private System.Windows.Forms.TextBox txtCoordZ;
        private System.Windows.Forms.CheckBox chkScreenInput;
        private System.Windows.Forms.Button btnUpdatePointRight;
        private System.Windows.Forms.Button btnDeletePointRight;
        private System.Windows.Forms.Button btnAddPointRight;
        private System.Windows.Forms.GroupBox grpCoordValue;
        private System.Windows.Forms.Panel pnlPrecsRight;
        private System.Windows.Forms.Panel pnlIbutuRight;
        private System.Windows.Forms.Panel pnlKikaiRight;

        private System.Windows.Forms.Button btnBgSettings;
        private System.Windows.Forms.Button btnEnvSettings;
        public System.Windows.Forms.Button btnBluetoothCtrl;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Label lblIkouLayerGrpHeader;
        private System.Windows.Forms.CheckBox chkLayer01;
        private System.Windows.Forms.CheckBox chkLayer02;
        private System.Windows.Forms.CheckBox chkLayer03;
        private System.Windows.Forms.CheckBox chkLayer04;
        private System.Windows.Forms.CheckBox chkLayer05;
        private System.Windows.Forms.CheckBox chkLayer06;
        private System.Windows.Forms.CheckBox chkLayer07;
        private System.Windows.Forms.CheckBox chkLayer08;
        private System.Windows.Forms.CheckBox chkLayer09;
        private System.Windows.Forms.CheckBox chkLayer10;
        private System.Windows.Forms.CheckBox chkLayer11;
        private System.Windows.Forms.CheckBox chkLayer12;
        private System.Windows.Forms.CheckBox chkLayer13;
        private System.Windows.Forms.CheckBox chkLayer14;
        private System.Windows.Forms.CheckBox chkLayer15;
        private System.Windows.Forms.CheckBox chkLayer16;
        private System.Windows.Forms.Button btnLayerAllOn;
        private System.Windows.Forms.Button btnLayerAllOff;
        private System.Windows.Forms.Button btnLayerSettings;
        private System.Windows.Forms.Button btnBatchUpdateModal;
        private System.Windows.Forms.Label lblEntityNameHeader;
        private System.Windows.Forms.CheckBox chkShowIbutuName;
        private System.Windows.Forms.CheckBox chkShowIkouName;
        private System.Windows.Forms.CheckBox chkShowKikaiName;
        public System.Windows.Forms.Panel panelMapRight;
        public System.Windows.Forms.Panel panelRightHeader;
        public System.Windows.Forms.Label lblRightTitle;
        public System.Windows.Forms.Button btnDetachWindow;
        public System.Windows.Forms.Button btnCloseRight;
        public System.Windows.Forms.Panel panelRightContent;
    }
}
