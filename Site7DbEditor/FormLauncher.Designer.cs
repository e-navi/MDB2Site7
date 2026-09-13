using System.Drawing;
using System.Windows.Forms;

namespace Site7DbEditor
{
    partial class FormLauncher
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
            this.components = new System.ComponentModel.Container();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblCurrentFolder = new System.Windows.Forms.Label();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnViewList = new System.Windows.Forms.Button();
            this.btnViewGrid = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnNewSite = new System.Windows.Forms.Button();
            this.btnOpenGaigyo = new System.Windows.Forms.Button();
            this.btnOpenNaigyo = new System.Windows.Forms.Button();
            this.btnNaigyoOption = new System.Windows.Forms.Button();
            this.btnTool = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitListPreview = new System.Windows.Forms.SplitContainer();
            this.dgvSites = new System.Windows.Forms.DataGridView();
            this.panelPreviewCard = new System.Windows.Forms.Panel();
            this.lblPreviewHeader = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.lblPreviewName = new System.Windows.Forms.Label();
            this.lblPreviewDate = new System.Windows.Forms.Label();
            this.lblPreviewSize = new System.Windows.Forms.Label();
            this.lblPreviewPath = new System.Windows.Forms.Label();
            this.flowThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.menuNaigyoOption = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemDrawing = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTool = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemMasterDef = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMasterLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMasterEnv = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSep = new System.Windows.Forms.ToolStripSeparator();
            this.itemExporter = new System.Windows.Forms.ToolStripMenuItem();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitListPreview)).BeginInit();
            this.splitListPreview.Panel1.SuspendLayout();
            this.splitListPreview.Panel2.SuspendLayout();
            this.splitListPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSites)).BeginInit();
            this.panelPreviewCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.menuNaigyoOption.SuspendLayout();
            this.menuTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblCurrentFolder);
            this.panelHeader.Controls.Add(this.btnBrowseFolder);
            this.panelHeader.Controls.Add(this.txtSearch);
            this.panelHeader.Controls.Add(this.btnViewList);
            this.panelHeader.Controls.Add(this.btnViewGrid);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelHeader.Size = new System.Drawing.Size(1164, 92);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelHeader_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(47)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(275, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "遺跡調査システム Site7";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(145)))));
            this.lblSubtitle.Location = new System.Drawing.Point(300, 15);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(130, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "現場管理ランチャー";
            // 
            // lblCurrentFolder
            // 
            this.lblCurrentFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentFolder.AutoEllipsis = true;
            this.lblCurrentFolder.Font = new System.Drawing.Font("Yu Gothic UI", 10F);
            this.lblCurrentFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(125)))));
            this.lblCurrentFolder.Location = new System.Drawing.Point(16, 48);
            this.lblCurrentFolder.Name = "lblCurrentFolder";
            this.lblCurrentFolder.Size = new System.Drawing.Size(550, 24);
            this.lblCurrentFolder.TabIndex = 2;
            this.lblCurrentFolder.Text = "現場フォルダ: ...";
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.btnBrowseFolder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(215)))));
            this.btnBrowseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFolder.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBrowseFolder.Location = new System.Drawing.Point(578, 42);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(130, 34);
            this.btnBrowseFolder.TabIndex = 3;
            this.btnBrowseFolder.Text = "📂 フォルダ変更";
            this.btnBrowseFolder.UseVisualStyleBackColor = false;
            this.btnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(718, 45);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "🔍 現場名を検索...";
            this.txtSearch.Size = new System.Drawing.Size(220, 27);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // btnViewList
            // 
            this.btnViewList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(255)))));
            this.btnViewList.FlatAppearance.BorderSize = 0;
            this.btnViewList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewList.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnViewList.ForeColor = System.Drawing.Color.White;
            this.btnViewList.Location = new System.Drawing.Point(948, 42);
            this.btnViewList.Name = "btnViewList";
            this.btnViewList.Size = new System.Drawing.Size(95, 34);
            this.btnViewList.TabIndex = 5;
            this.btnViewList.Text = "📄 リスト";
            this.btnViewList.UseVisualStyleBackColor = false;
            this.btnViewList.Click += new System.EventHandler(this.BtnViewList_Click);
            // 
            // btnViewGrid
            // 
            this.btnViewGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.btnViewGrid.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(215)))));
            this.btnViewGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewGrid.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnViewGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.btnViewGrid.Location = new System.Drawing.Point(1053, 42);
            this.btnViewGrid.Name = "btnViewGrid";
            this.btnViewGrid.Size = new System.Drawing.Size(95, 34);
            this.btnViewGrid.TabIndex = 6;
            this.btnViewGrid.Text = "🖼 グリッド";
            this.btnViewGrid.UseVisualStyleBackColor = false;
            this.btnViewGrid.Click += new System.EventHandler(this.BtnViewGrid_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnNewSite);
            this.panelFooter.Controls.Add(this.btnOpenGaigyo);
            this.panelFooter.Controls.Add(this.btnOpenNaigyo);
            this.panelFooter.Controls.Add(this.btnNaigyoOption);
            this.panelFooter.Controls.Add(this.btnTool);
            this.panelFooter.Controls.Add(this.btnExit);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 608);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.panelFooter.Size = new System.Drawing.Size(1164, 72);
            this.panelFooter.TabIndex = 2;
            this.panelFooter.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelFooter_Paint);
            // 
            // btnNewSite
            // 
            this.btnNewSite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.btnNewSite.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(200)))), ((int)(((byte)(215)))));
            this.btnNewSite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewSite.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnNewSite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNewSite.Location = new System.Drawing.Point(20, 14);
            this.btnNewSite.Name = "btnNewSite";
            this.btnNewSite.Size = new System.Drawing.Size(125, 44);
            this.btnNewSite.TabIndex = 0;
            this.btnNewSite.Text = "＋ 新規現場";
            this.btnNewSite.UseVisualStyleBackColor = false;
            this.btnNewSite.Click += new System.EventHandler(this.BtnNewSite_Click);
            // 
            // btnOpenGaigyo
            // 
            this.btnOpenGaigyo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnOpenGaigyo.FlatAppearance.BorderSize = 0;
            this.btnOpenGaigyo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenGaigyo.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpenGaigyo.ForeColor = System.Drawing.Color.White;
            this.btnOpenGaigyo.Location = new System.Drawing.Point(155, 14);
            this.btnOpenGaigyo.Name = "btnOpenGaigyo";
            this.btnOpenGaigyo.Size = new System.Drawing.Size(115, 44);
            this.btnOpenGaigyo.TabIndex = 1;
            this.btnOpenGaigyo.Text = "📡 外業";
            this.btnOpenGaigyo.UseVisualStyleBackColor = false;
            this.btnOpenGaigyo.Click += new System.EventHandler(this.BtnOpenGaigyo_Click);
            // 
            // btnOpenNaigyo
            // 
            this.btnOpenNaigyo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.btnOpenNaigyo.FlatAppearance.BorderSize = 0;
            this.btnOpenNaigyo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenNaigyo.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpenNaigyo.ForeColor = System.Drawing.Color.White;
            this.btnOpenNaigyo.Location = new System.Drawing.Point(280, 14);
            this.btnOpenNaigyo.Name = "btnOpenNaigyo";
            this.btnOpenNaigyo.Size = new System.Drawing.Size(115, 44);
            this.btnOpenNaigyo.TabIndex = 2;
            this.btnOpenNaigyo.Text = "💻 内業";
            this.btnOpenNaigyo.UseVisualStyleBackColor = false;
            this.btnOpenNaigyo.Click += new System.EventHandler(this.BtnOpenNaigyo_Click);
            // 
            // btnNaigyoOption
            // 
            this.btnNaigyoOption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(246)))));
            this.btnNaigyoOption.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnNaigyoOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNaigyoOption.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnNaigyoOption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnNaigyoOption.Location = new System.Drawing.Point(405, 14);
            this.btnNaigyoOption.Name = "btnNaigyoOption";
            this.btnNaigyoOption.Size = new System.Drawing.Size(160, 44);
            this.btnNaigyoOption.TabIndex = 3;
            this.btnNaigyoOption.Text = "📐 内業オプション ▾";
            this.btnNaigyoOption.UseVisualStyleBackColor = false;
            this.btnNaigyoOption.Click += new System.EventHandler(this.BtnNaigyoOption_Click);
            // 
            // btnTool
            // 
            this.btnTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(246)))));
            this.btnTool.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTool.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnTool.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnTool.Location = new System.Drawing.Point(575, 14);
            this.btnTool.Name = "btnTool";
            this.btnTool.Size = new System.Drawing.Size(125, 44);
            this.btnTool.TabIndex = 4;
            this.btnTool.Text = "🛠 ツール ▾";
            this.btnTool.UseVisualStyleBackColor = false;
            this.btnTool.Click += new System.EventHandler(this.BtnTool_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnExit.Location = new System.Drawing.Point(1044, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 44);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "✖ 終了";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelMain.Controls.Add(this.splitListPreview);
            this.panelMain.Controls.Add(this.flowThumbnails);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 92);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(12);
            this.panelMain.Size = new System.Drawing.Size(1164, 516);
            this.panelMain.TabIndex = 1;
            // 
            // splitListPreview
            // 
            this.splitListPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.splitListPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitListPreview.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitListPreview.Location = new System.Drawing.Point(12, 12);
            this.splitListPreview.Name = "splitListPreview";
            // 
            // splitListPreview.Panel1
            // 
            this.splitListPreview.Panel1.Controls.Add(this.dgvSites);
            this.splitListPreview.Panel1MinSize = 50;
            // 
            // splitListPreview.Panel2
            // 
            this.splitListPreview.Panel2.Controls.Add(this.panelPreviewCard);
            this.splitListPreview.Panel2MinSize = 50;
            this.splitListPreview.Size = new System.Drawing.Size(1140, 492);
            this.splitListPreview.SplitterDistance = 865;
            this.splitListPreview.SplitterWidth = 8;
            this.splitListPreview.TabIndex = 0;
            // 
            // dgvSites
            // 
            this.dgvSites.AllowUserToAddRows = false;
            this.dgvSites.AllowUserToDeleteRows = false;
            this.dgvSites.AllowUserToResizeRows = false;
            this.dgvSites.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSites.BackgroundColor = System.Drawing.Color.White;
            this.dgvSites.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSites.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.dgvSites.Location = new System.Drawing.Point(0, 0);
            this.dgvSites.MultiSelect = false;
            this.dgvSites.Name = "dgvSites";
            this.dgvSites.ReadOnly = true;
            this.dgvSites.RowHeadersVisible = false;
            this.dgvSites.RowTemplate.Height = 52;
            this.dgvSites.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSites.Size = new System.Drawing.Size(865, 492);
            this.dgvSites.TabIndex = 0;
            this.dgvSites.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSites_CellDoubleClick);
            this.dgvSites.SelectionChanged += new System.EventHandler(this.DgvSites_SelectionChanged);
            // 
            // panelPreviewCard
            // 
            this.panelPreviewCard.AutoScroll = true;
            this.panelPreviewCard.BackColor = System.Drawing.Color.White;
            this.panelPreviewCard.Controls.Add(this.lblPreviewHeader);
            this.panelPreviewCard.Controls.Add(this.picPreview);
            this.panelPreviewCard.Controls.Add(this.lblPreviewName);
            this.panelPreviewCard.Controls.Add(this.lblPreviewDate);
            this.panelPreviewCard.Controls.Add(this.lblPreviewSize);
            this.panelPreviewCard.Controls.Add(this.lblPreviewPath);
            this.panelPreviewCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreviewCard.Location = new System.Drawing.Point(0, 0);
            this.panelPreviewCard.Name = "panelPreviewCard";
            this.panelPreviewCard.Padding = new System.Windows.Forms.Padding(12);
            this.panelPreviewCard.Size = new System.Drawing.Size(267, 492);
            this.panelPreviewCard.TabIndex = 0;
            // 
            // lblPreviewHeader
            // 
            this.lblPreviewHeader.AutoSize = true;
            this.lblPreviewHeader.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPreviewHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPreviewHeader.Location = new System.Drawing.Point(12, 10);
            this.lblPreviewHeader.Name = "lblPreviewHeader";
            this.lblPreviewHeader.Size = new System.Drawing.Size(104, 21);
            this.lblPreviewHeader.TabIndex = 0;
            this.lblPreviewHeader.Text = "現場プレビュー";
            // 
            // picPreview
            // 
            this.picPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPreview.BackColor = System.Drawing.Color.White;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(12, 38);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(239, 200);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 1;
            this.picPreview.TabStop = false;
            // 
            // lblPreviewName
            // 
            this.lblPreviewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewName.AutoEllipsis = true;
            this.lblPreviewName.Font = new System.Drawing.Font("Yu Gothic UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPreviewName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPreviewName.Location = new System.Drawing.Point(12, 248);
            this.lblPreviewName.Name = "lblPreviewName";
            this.lblPreviewName.Size = new System.Drawing.Size(239, 28);
            this.lblPreviewName.TabIndex = 2;
            this.lblPreviewName.Text = "現場名: -";
            // 
            // lblPreviewDate
            // 
            this.lblPreviewDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewDate.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F);
            this.lblPreviewDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPreviewDate.Location = new System.Drawing.Point(12, 280);
            this.lblPreviewDate.Name = "lblPreviewDate";
            this.lblPreviewDate.Size = new System.Drawing.Size(239, 22);
            this.lblPreviewDate.TabIndex = 3;
            this.lblPreviewDate.Text = "更新日時: -";
            // 
            // lblPreviewSize
            // 
            this.lblPreviewSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewSize.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F);
            this.lblPreviewSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPreviewSize.Location = new System.Drawing.Point(12, 305);
            this.lblPreviewSize.Name = "lblPreviewSize";
            this.lblPreviewSize.Size = new System.Drawing.Size(239, 22);
            this.lblPreviewSize.TabIndex = 4;
            this.lblPreviewSize.Text = "データ容量: -";
            // 
            // lblPreviewPath
            // 
            this.lblPreviewPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewPath.AutoEllipsis = true;
            this.lblPreviewPath.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F);
            this.lblPreviewPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPreviewPath.Location = new System.Drawing.Point(12, 330);
            this.lblPreviewPath.Name = "lblPreviewPath";
            this.lblPreviewPath.Size = new System.Drawing.Size(239, 48);
            this.lblPreviewPath.TabIndex = 5;
            this.lblPreviewPath.Text = "フォルダ: -";
            // 
            // flowThumbnails
            // 
            this.flowThumbnails.AutoScroll = true;
            this.flowThumbnails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.flowThumbnails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowThumbnails.Location = new System.Drawing.Point(12, 12);
            this.flowThumbnails.Name = "flowThumbnails";
            this.flowThumbnails.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.flowThumbnails.Size = new System.Drawing.Size(1140, 492);
            this.flowThumbnails.TabIndex = 1;
            this.flowThumbnails.Visible = false;
            // 
            // menuNaigyoOption
            // 
            this.menuNaigyoOption.Font = new System.Drawing.Font("Yu Gothic UI", 13F);
            this.menuNaigyoOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemDrawing,
            this.itemSection});
            this.menuNaigyoOption.Name = "menuNaigyoOption";
            this.menuNaigyoOption.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.menuNaigyoOption.ShowImageMargin = false;
            this.menuNaigyoOption.Size = new System.Drawing.Size(220, 96);
            // 
            // itemDrawing
            // 
            this.itemDrawing.Name = "itemDrawing";
            this.itemDrawing.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.itemDrawing.Size = new System.Drawing.Size(219, 41);
            this.itemDrawing.Text = "📐 個別遺構図作成";
            this.itemDrawing.Click += new System.EventHandler(this.ItemDrawing_Click);
            // 
            // itemSection
            // 
            this.itemSection.Name = "itemSection";
            this.itemSection.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.itemSection.Size = new System.Drawing.Size(219, 41);
            this.itemSection.Text = "📐 調査区断面図";
            this.itemSection.Click += new System.EventHandler(this.ItemSection_Click);
            // 
            // menuTool
            // 
            this.menuTool.Font = new System.Drawing.Font("Yu Gothic UI", 13F);
            this.menuTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemMasterDef,
            this.itemMasterLayer,
            this.itemMasterEnv,
            this.itemSep,
            this.itemExporter});
            this.menuTool.Name = "menuTool";
            this.menuTool.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.menuTool.ShowImageMargin = false;
            this.menuTool.Size = new System.Drawing.Size(330, 190);
            // 
            // itemMasterDef
            // 
            this.itemMasterDef.Name = "itemMasterDef";
            this.itemMasterDef.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.itemMasterDef.Size = new System.Drawing.Size(329, 41);
            this.itemMasterDef.Text = "⚙ マスターDef設定 (入力定義)...";
            this.itemMasterDef.Click += new System.EventHandler(this.ItemMasterDef_Click);
            // 
            // itemMasterLayer
            // 
            this.itemMasterLayer.Name = "itemMasterLayer";
            this.itemMasterLayer.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.itemMasterLayer.Size = new System.Drawing.Size(329, 41);
            this.itemMasterLayer.Text = "📐 マスターレイヤ設定...";
            this.itemMasterLayer.Click += new System.EventHandler(this.ItemMasterLayer_Click);
            // 
            // itemMasterEnv
            // 
            this.itemMasterEnv.Name = "itemMasterEnv";
            this.itemMasterEnv.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.itemMasterEnv.Size = new System.Drawing.Size(329, 41);
            this.itemMasterEnv.Text = "📡 マスターTS・GPS環境設定...";
            this.itemMasterEnv.Click += new System.EventHandler(this.ItemMasterEnv_Click);
            // 
            // itemSep
            // 
            this.itemSep.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.itemSep.Name = "itemSep";
            this.itemSep.Size = new System.Drawing.Size(326, 6);
            // 
            // itemExporter
            // 
            this.itemExporter.Name = "itemExporter";
            this.itemExporter.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.itemExporter.Size = new System.Drawing.Size(329, 41);
            this.itemExporter.Text = "💾 旧DB移行 (MDB/FDB Exporter)";
            this.itemExporter.Click += new System.EventHandler(this.ItemExporter_Click);
            // 
            // FormLauncher
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1164, 868);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelFooter);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "FormLauncher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "遺跡調査システム Site7 - 現場選択";
            this.Load += new System.EventHandler(this.FormLauncher_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.splitListPreview.Panel1.ResumeLayout(false);
            this.splitListPreview.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitListPreview)).EndInit();
            this.splitListPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSites)).EndInit();
            this.panelPreviewCard.ResumeLayout(false);
            this.panelPreviewCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.menuNaigyoOption.ResumeLayout(false);
            this.menuTool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblCurrentFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnViewList;
        private System.Windows.Forms.Button btnViewGrid;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnNewSite;
        private System.Windows.Forms.Button btnOpenGaigyo;
        private System.Windows.Forms.Button btnOpenNaigyo;
        private System.Windows.Forms.Button btnNaigyoOption;
        private System.Windows.Forms.Button btnTool;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.SplitContainer splitListPreview;
        private System.Windows.Forms.DataGridView dgvSites;
        private System.Windows.Forms.Panel panelPreviewCard;
        private System.Windows.Forms.Label lblPreviewHeader;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label lblPreviewName;
        private System.Windows.Forms.Label lblPreviewDate;
        private System.Windows.Forms.Label lblPreviewSize;
        private System.Windows.Forms.Label lblPreviewPath;
        private System.Windows.Forms.FlowLayoutPanel flowThumbnails;
        private System.Windows.Forms.ContextMenuStrip menuNaigyoOption;
        private System.Windows.Forms.ToolStripMenuItem itemDrawing;
        private System.Windows.Forms.ToolStripMenuItem itemSection;
        private System.Windows.Forms.ContextMenuStrip menuTool;
        private System.Windows.Forms.ToolStripMenuItem itemMasterDef;
        private System.Windows.Forms.ToolStripMenuItem itemMasterLayer;
        private System.Windows.Forms.ToolStripMenuItem itemMasterEnv;
        private System.Windows.Forms.ToolStripSeparator itemSep;
        private System.Windows.Forms.ToolStripMenuItem itemExporter;
    }
}
