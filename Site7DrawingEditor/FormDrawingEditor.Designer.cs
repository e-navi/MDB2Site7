namespace Site7DrawingEditor
{
    partial class FormDrawingEditor
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
            this.lblQuickDb = new System.Windows.Forms.Label();
            this.cmbQuickDbSelect = new System.Windows.Forms.ComboBox();
            this.chkIsFullDrawing = new System.Windows.Forms.CheckBox();
            this.btnSaveDb = new System.Windows.Forms.Button();
            this.lblDbStatus = new System.Windows.Forms.Label();

            // Top Left Panel (Full Survey Map)
            this.panelTopLeftHeader = new System.Windows.Forms.Panel();
            this.lblFullMapTitle = new System.Windows.Forms.Label();
            this.btnResetCropZoom = new System.Windows.Forms.Button();
            this.btnCropPick3P = new System.Windows.Forms.Button();
            this.panelHcLeftSidebar = new System.Windows.Forms.Panel();
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
            this.lblEntityNameHeader = new System.Windows.Forms.Label();
            this.chkShowIbutu = new System.Windows.Forms.CheckBox();
            this.chkShowIbutuName = new System.Windows.Forms.CheckBox();
            this.chkShowIkou = new System.Windows.Forms.CheckBox();
            this.chkShowIkouName = new System.Windows.Forms.CheckBox();
            this.chkShowKikai = new System.Windows.Forms.CheckBox();
            this.chkShowKikaiName = new System.Windows.Forms.CheckBox();
            this.chkShowIkouFull = new System.Windows.Forms.CheckBox();
            this.chkShowIbutuFull = new System.Windows.Forms.CheckBox();
            this.chkShowKikaiFull = new System.Windows.Forms.CheckBox();
            this.chkShowCurveFull = new System.Windows.Forms.CheckBox();
            this.chkShowGridFull = new System.Windows.Forms.CheckBox();
            this.chkColorByIkouFull = new System.Windows.Forms.CheckBox();
            this.picCropCanvas = new System.Windows.Forms.PictureBox();

            // Top Right Panel (Paper Layout Sheet Preview)
            this.panelTopRightHeader = new System.Windows.Forms.Panel();
            this.lblPaperSheetTitle = new System.Windows.Forms.Label();
            this.btnResetPaperZoom = new System.Windows.Forms.Button();
            this.chkShowCurvePaper = new System.Windows.Forms.CheckBox();
            this.chkShowDirectionPaper = new System.Windows.Forms.CheckBox();
            this.chkShowDanmenPaper = new System.Windows.Forms.CheckBox();
            this.chkColorByIkouPaper = new System.Windows.Forms.CheckBox();
            this.lblPaperInfoBanner = new System.Windows.Forms.Label();
            this.picPaperCanvas = new System.Windows.Forms.PictureBox();

            // Top Left & Top Right Panels
            this.splitContainerTopHorizontal = new System.Windows.Forms.SplitContainer();
            this.panelTopLeft = new System.Windows.Forms.Panel();
            this.panelTopRight = new System.Windows.Forms.Panel();
            this.panelBottomFixedGroup = new System.Windows.Forms.Panel();
            this.panelBottomGrids = new System.Windows.Forms.TableLayoutPanel();

            // Grid 1: Drawings
            this.grpDrawings = new System.Windows.Forms.GroupBox();
            this.dgvDrawings = new System.Windows.Forms.DataGridView();
            this.btnAddDrawing = new System.Windows.Forms.Button();
            this.btnDeleteDrawing = new System.Windows.Forms.Button();

            // Grid 2: Target Features in Drawing
            this.grpDrawingIkous = new System.Windows.Forms.GroupBox();
            this.dgvDrawingIkous = new System.Windows.Forms.DataGridView();
            this.btnAddDrawingIkou = new System.Windows.Forms.Button();
            this.btnDeleteDrawingIkou = new System.Windows.Forms.Button();

            // Grid 3: Danmen Section Lines
            this.grpDanmenList = new System.Windows.Forms.GroupBox();
            this.dgvDanmen = new System.Windows.Forms.DataGridView();
            this.btnAddDanmen = new System.Windows.Forms.Button();
            this.btnDeleteDanmen = new System.Windows.Forms.Button();

            // Middle Control Panel & Right Detail Preview
            this.panelControls = new System.Windows.Forms.Panel();

            // Group: Drawing Controls
            this.grpDrawingProps = new System.Windows.Forms.GroupBox();
            this.lblDrawingName = new System.Windows.Forms.Label();
            this.txtDrawingName = new System.Windows.Forms.TextBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblScale = new System.Windows.Forms.Label();
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.btnUpdateDrawingProps = new System.Windows.Forms.Button();

            // Group: Feature Controls (内側に方位マーク・断面GroupBoxを内包)
            this.grpIkouProps = new System.Windows.Forms.GroupBox();
            this.lblTargetIkou = new System.Windows.Forms.Label();
            this.cmbFeatureSelect = new System.Windows.Forms.ComboBox();
            this.btnUpdateIkouProps = new System.Windows.Forms.Button();
            this.btnPickCropBounds = new System.Windows.Forms.Button();
            this.btnSetPaperPosition = new System.Windows.Forms.Button();

            // Group: Compass Mark Controls (遺構 GroupBox の子)
            this.grpCompassProps = new System.Windows.Forms.GroupBox();
            this.chkShowDirection = new System.Windows.Forms.CheckBox();
            this.btnSetDirectionPosition = new System.Windows.Forms.Button();

            // Group: Section Controls (遺構 GroupBox の子)
            this.grpDanmenProps = new System.Windows.Forms.GroupBox();
            this.lblDanmenName = new System.Windows.Forms.Label();
            this.txtDanmenName = new System.Windows.Forms.TextBox();
            this.btnSetDanmenPosition = new System.Windows.Forms.Button();

            // Rightmost Feature Detail Preview Canvas
            this.grpFeatureDetailPreview = new System.Windows.Forms.GroupBox();
            this.picFeatureDetailCanvas = new System.Windows.Forms.PictureBox();

            // Bottom Status Bar
            this.statusStripBar = new System.Windows.Forms.StatusStrip();
            this.lblStatusCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();

            this.panelHeader.SuspendLayout();

            this.panelTopLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCropCanvas)).BeginInit();
            this.panelTopRightHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPaperCanvas)).BeginInit();

            this.panelTopLeft.SuspendLayout();
            this.panelTopLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCropCanvas)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTopHorizontal)).BeginInit();
            this.splitContainerTopHorizontal.Panel1.SuspendLayout();
            this.splitContainerTopHorizontal.Panel2.SuspendLayout();
            this.splitContainerTopHorizontal.SuspendLayout();

            this.panelTopRight.SuspendLayout();
            this.panelTopRightHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPaperCanvas)).BeginInit();

            this.panelBottomFixedGroup.SuspendLayout();
            this.panelBottomGrids.SuspendLayout();
            this.grpDrawings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawings)).BeginInit();
            this.grpDrawingIkous.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingIkous)).BeginInit();
            this.grpDanmenList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanmen)).BeginInit();

            this.panelControls.SuspendLayout();
            this.grpDrawingProps.SuspendLayout();
            this.grpIkouProps.SuspendLayout();
            this.grpCompassProps.SuspendLayout();
            this.grpDanmenProps.SuspendLayout();

            this.grpFeatureDetailPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFeatureDetailCanvas)).BeginInit();

            this.statusStripBar.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.panelHeader.Controls.Add(this.lblDbStatus);
            this.panelHeader.Controls.Add(this.btnSaveDb);
            this.panelHeader.Controls.Add(this.cmbQuickDbSelect);
            this.panelHeader.Controls.Add(this.lblQuickDb);
            this.panelHeader.Controls.Add(this.btnOpenDb);
            this.panelHeader.Controls.Add(this.lblSubHeader);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1480, 60);
            this.panelHeader.TabIndex = 0;

            // lblHeaderTitle
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Yu Gothic UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(12, 8);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(320, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "📐 SITE7 遺跡調査・遺構図面作成システム";

            // lblSubHeader
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblSubHeader.Location = new System.Drawing.Point(14, 35);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(326, 15);
            this.lblSubHeader.TabIndex = 1;
            this.lblSubHeader.Text = "全図3点切り出し・用紙図面レイアウト・断面図連動保存";

            // btnOpenDb
            this.btnOpenDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnOpenDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenDb.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenDb.ForeColor = System.Drawing.Color.White;
            this.btnOpenDb.Location = new System.Drawing.Point(360, 14);
            this.btnOpenDb.Name = "btnOpenDb";
            this.btnOpenDb.Size = new System.Drawing.Size(125, 32);
            this.btnOpenDb.TabIndex = 2;
            this.btnOpenDb.Text = "📁 DBフォルダ選択";
            this.btnOpenDb.UseVisualStyleBackColor = false;

            // lblQuickDb
            this.lblQuickDb.AutoSize = true;
            this.lblQuickDb.ForeColor = System.Drawing.Color.White;
            this.lblQuickDb.Location = new System.Drawing.Point(495, 23);
            this.lblQuickDb.Name = "lblQuickDb";
            this.lblQuickDb.Size = new System.Drawing.Size(63, 15);
            this.lblQuickDb.TabIndex = 3;
            this.lblQuickDb.Text = "DB選択:";

            // cmbQuickDbSelect
            this.cmbQuickDbSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuickDbSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(50)))));
            this.cmbQuickDbSelect.ForeColor = System.Drawing.Color.White;
            this.cmbQuickDbSelect.FormattingEnabled = true;
            this.cmbQuickDbSelect.Location = new System.Drawing.Point(560, 19);
            this.cmbQuickDbSelect.Name = "cmbQuickDbSelect";
            this.cmbQuickDbSelect.Size = new System.Drawing.Size(245, 23);
            this.cmbQuickDbSelect.TabIndex = 4;

            // btnSaveDb
            this.btnSaveDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(176)))), ((int)(((byte)(0)))));
            this.btnSaveDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDb.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSaveDb.ForeColor = System.Drawing.Color.Black;
            this.btnSaveDb.Location = new System.Drawing.Point(820, 12);
            this.btnSaveDb.Name = "btnSaveDb";
            this.btnSaveDb.Size = new System.Drawing.Size(140, 35);
            this.btnSaveDb.TabIndex = 5;
            this.btnSaveDb.Text = "💾 SQLite DB保存";
            this.btnSaveDb.UseVisualStyleBackColor = false;

            // lblDbStatus
            this.lblDbStatus.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(0)))));
            this.lblDbStatus.Location = new System.Drawing.Point(970, 15);
            this.lblDbStatus.Name = "lblDbStatus";
            this.lblDbStatus.Size = new System.Drawing.Size(490, 30);
            this.lblDbStatus.TabIndex = 6;
            this.lblDbStatus.Text = "DB未読み込み";
            this.lblDbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // splitContainerTopHorizontal (上部: 全体図 vs 用紙レイアウト 分割スプリッター)
            // 
            this.splitContainerTopHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTopHorizontal.Location = new System.Drawing.Point(0, 60);
            this.splitContainerTopHorizontal.Name = "splitContainerTopHorizontal";
            this.splitContainerTopHorizontal.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitContainerTopHorizontal.Size = new System.Drawing.Size(1480, 445);
            this.splitContainerTopHorizontal.SplitterDistance = 740;
            this.splitContainerTopHorizontal.TabIndex = 1;

            // panelTopLeft
            this.panelTopLeft.Controls.Add(this.panelHcLeftSidebar);
            this.panelTopLeft.Controls.Add(this.picCropCanvas);
            this.panelTopLeft.Controls.Add(this.panelTopLeftHeader);
            this.panelTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopLeft.Location = new System.Drawing.Point(0, 0);
            this.panelTopLeft.Name = "panelTopLeft";
            this.panelTopLeft.Size = new System.Drawing.Size(740, 450);
            this.panelTopLeft.TabIndex = 0;
            this.splitContainerTopHorizontal.Panel1.Controls.Add(this.panelTopLeft);

            // panelHcLeftSidebar
            this.panelHcLeftSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(242)))));
            this.panelHcLeftSidebar.Controls.Add(this.btnBgSettings);
            this.panelHcLeftSidebar.Controls.Add(this.btnEnvSettings);
            this.panelHcLeftSidebar.Controls.Add(this.btnUndo);
            this.panelHcLeftSidebar.Controls.Add(this.btnRedo);
            this.panelHcLeftSidebar.Controls.Add(this.lblIkouLayerGrpHeader);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer01);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer02);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer03);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer04);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer05);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer06);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer07);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer08);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer09);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer10);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer11);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer12);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer13);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer14);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer15);
            this.panelHcLeftSidebar.Controls.Add(this.chkLayer16);
            this.panelHcLeftSidebar.Controls.Add(this.btnLayerAllOn);
            this.panelHcLeftSidebar.Controls.Add(this.btnLayerAllOff);
            this.panelHcLeftSidebar.Controls.Add(this.btnLayerSettings);
            this.panelHcLeftSidebar.Controls.Add(this.lblEntityNameHeader);
            this.panelHcLeftSidebar.Controls.Add(this.chkShowIbutu);
            this.panelHcLeftSidebar.Controls.Add(this.chkShowIbutuName);
            this.panelHcLeftSidebar.Controls.Add(this.chkShowIkou);
            this.panelHcLeftSidebar.Controls.Add(this.chkShowIkouName);
            this.panelHcLeftSidebar.Controls.Add(this.chkShowKikai);
            this.panelHcLeftSidebar.Controls.Add(this.chkShowKikaiName);
            this.panelHcLeftSidebar.Location = new System.Drawing.Point(0, 35);
            this.panelHcLeftSidebar.Name = "panelHcLeftSidebar";
            this.panelHcLeftSidebar.Size = new System.Drawing.Size(130, 415);
            this.panelHcLeftSidebar.TabIndex = 1;

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

            // lblEntityNameHeader
            this.lblEntityNameHeader.AutoSize = true;
            this.lblEntityNameHeader.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblEntityNameHeader.ForeColor = System.Drawing.Color.Red;
            this.lblEntityNameHeader.Location = new System.Drawing.Point(82, 316);
            this.lblEntityNameHeader.Name = "lblEntityNameHeader";
            this.lblEntityNameHeader.Size = new System.Drawing.Size(31, 15);
            this.lblEntityNameHeader.TabIndex = 24;
            this.lblEntityNameHeader.Text = "名称";

            // chkShowIbutu
            this.chkShowIbutu.AutoSize = true;
            this.chkShowIbutu.Checked = true;
            this.chkShowIbutu.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowIbutu.ForeColor = System.Drawing.Color.Red;
            this.chkShowIbutu.Location = new System.Drawing.Point(6, 333);
            this.chkShowIbutu.Name = "chkShowIbutu";
            this.chkShowIbutu.Size = new System.Drawing.Size(50, 19);
            this.chkShowIbutu.TabIndex = 25;
            this.chkShowIbutu.Text = "遺物";
            this.chkShowIbutu.UseVisualStyleBackColor = true;

            // chkShowIbutuName
            this.chkShowIbutuName.AutoSize = true;
            this.chkShowIbutuName.Location = new System.Drawing.Point(90, 336);
            this.chkShowIbutuName.Name = "chkShowIbutuName";
            this.chkShowIbutuName.Size = new System.Drawing.Size(15, 14);
            this.chkShowIbutuName.TabIndex = 26;
            this.chkShowIbutuName.UseVisualStyleBackColor = true;

            // chkShowIkou
            this.chkShowIkou.AutoSize = true;
            this.chkShowIkou.Checked = true;
            this.chkShowIkou.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowIkou.ForeColor = System.Drawing.Color.Blue;
            this.chkShowIkou.Location = new System.Drawing.Point(6, 355);
            this.chkShowIkou.Name = "chkShowIkou";
            this.chkShowIkou.Size = new System.Drawing.Size(50, 19);
            this.chkShowIkou.TabIndex = 27;
            this.chkShowIkou.Text = "遺構";
            this.chkShowIkou.UseVisualStyleBackColor = true;

            // chkShowIkouName
            this.chkShowIkouName.AutoSize = true;
            this.chkShowIkouName.Location = new System.Drawing.Point(90, 358);
            this.chkShowIkouName.Name = "chkShowIkouName";
            this.chkShowIkouName.Size = new System.Drawing.Size(15, 14);
            this.chkShowIkouName.TabIndex = 28;
            this.chkShowIkouName.UseVisualStyleBackColor = true;

            // chkShowKikai
            this.chkShowKikai.AutoSize = true;
            this.chkShowKikai.Checked = true;
            this.chkShowKikai.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkShowKikai.ForeColor = System.Drawing.Color.Blue;
            this.chkShowKikai.Location = new System.Drawing.Point(6, 377);
            this.chkShowKikai.Name = "chkShowKikai";
            this.chkShowKikai.Size = new System.Drawing.Size(62, 19);
            this.chkShowKikai.TabIndex = 29;
            this.chkShowKikai.Text = "基準点";
            this.chkShowKikai.UseVisualStyleBackColor = true;

            // chkShowKikaiName
            this.chkShowKikaiName.AutoSize = true;
            this.chkShowKikaiName.Checked = true;
            this.chkShowKikaiName.Location = new System.Drawing.Point(90, 380);
            this.chkShowKikaiName.Name = "chkShowKikaiName";
            this.chkShowKikaiName.Size = new System.Drawing.Size(15, 14);
            this.chkShowKikaiName.TabIndex = 30;
            this.chkShowKikaiName.UseVisualStyleBackColor = true;

            // picCropCanvas
            this.picCropCanvas.Location = new System.Drawing.Point(130, 35);
            this.picCropCanvas.Name = "picCropCanvas";
            this.picCropCanvas.Size = new System.Drawing.Size(610, 415);
            this.picCropCanvas.TabIndex = 1;
            this.picCropCanvas.TabStop = false;

            // panelTopRightHeader
            this.panelTopRightHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.panelTopRightHeader.Controls.Add(this.lblPaperInfoBanner);
            this.panelTopRightHeader.Controls.Add(this.btnResetPaperZoom);
            this.panelTopRightHeader.Controls.Add(this.lblPaperSheetTitle);
            this.panelTopRightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopRightHeader.Location = new System.Drawing.Point(0, 0);
            this.panelTopRightHeader.Name = "panelTopRightHeader";
            this.panelTopRightHeader.Size = new System.Drawing.Size(736, 35);
            this.panelTopRightHeader.TabIndex = 0;

            // lblPaperSheetTitle
            this.lblPaperSheetTitle.AutoSize = true;
            this.lblPaperSheetTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaperSheetTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblPaperSheetTitle.Location = new System.Drawing.Point(8, 8);
            this.lblPaperSheetTitle.Name = "lblPaperSheetTitle";
            this.lblPaperSheetTitle.Size = new System.Drawing.Size(154, 17);
            this.lblPaperSheetTitle.TabIndex = 0;
            this.lblPaperSheetTitle.Text = "📐 用紙レイアウトプレビュー";

            // btnResetPaperZoom
            this.btnResetPaperZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnResetPaperZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPaperZoom.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnResetPaperZoom.ForeColor = System.Drawing.Color.White;
            this.btnResetPaperZoom.Location = new System.Drawing.Point(170, 5);
            this.btnResetPaperZoom.Name = "btnResetPaperZoom";
            this.btnResetPaperZoom.Size = new System.Drawing.Size(85, 25);
            this.btnResetPaperZoom.TabIndex = 1;
            this.btnResetPaperZoom.Text = "リセット";
            this.btnResetPaperZoom.UseVisualStyleBackColor = false;

            // chkShowCurvePaper
            this.chkShowCurvePaper.AutoSize = true;
            this.chkShowCurvePaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(28)))));
            this.chkShowCurvePaper.Checked = true;
            this.chkShowCurvePaper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowCurvePaper.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowCurvePaper.ForeColor = System.Drawing.Color.White;
            this.chkShowCurvePaper.Location = new System.Drawing.Point(6, 42);
            this.chkShowCurvePaper.Name = "chkShowCurvePaper";
            this.chkShowCurvePaper.Size = new System.Drawing.Size(50, 19);
            this.chkShowCurvePaper.TabIndex = 0;
            this.chkShowCurvePaper.Text = "曲線";
            this.chkShowCurvePaper.UseVisualStyleBackColor = true;

            // chkShowDirectionPaper
            this.chkShowDirectionPaper.AutoSize = true;
            this.chkShowDirectionPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(28)))));
            this.chkShowDirectionPaper.Checked = true;
            this.chkShowDirectionPaper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowDirectionPaper.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowDirectionPaper.ForeColor = System.Drawing.Color.White;
            this.chkShowDirectionPaper.Location = new System.Drawing.Point(6, 64);
            this.chkShowDirectionPaper.Name = "chkShowDirectionPaper";
            this.chkShowDirectionPaper.Size = new System.Drawing.Size(50, 19);
            this.chkShowDirectionPaper.TabIndex = 1;
            this.chkShowDirectionPaper.Text = "方位";
            this.chkShowDirectionPaper.UseVisualStyleBackColor = true;

            // chkShowDanmenPaper
            this.chkShowDanmenPaper.AutoSize = true;
            this.chkShowDanmenPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(28)))));
            this.chkShowDanmenPaper.Checked = true;
            this.chkShowDanmenPaper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowDanmenPaper.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShowDanmenPaper.ForeColor = System.Drawing.Color.White;
            this.chkShowDanmenPaper.Location = new System.Drawing.Point(6, 86);
            this.chkShowDanmenPaper.Name = "chkShowDanmenPaper";
            this.chkShowDanmenPaper.Size = new System.Drawing.Size(50, 19);
            this.chkShowDanmenPaper.TabIndex = 2;
            this.chkShowDanmenPaper.Text = "断面";
            this.chkShowDanmenPaper.UseVisualStyleBackColor = true;

            // chkColorByIkouPaper
            this.chkColorByIkouPaper.AutoSize = true;
            this.chkColorByIkouPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(28)))));
            this.chkColorByIkouPaper.Checked = false;
            this.chkColorByIkouPaper.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkColorByIkouPaper.ForeColor = System.Drawing.Color.White;
            this.chkColorByIkouPaper.Location = new System.Drawing.Point(6, 108);
            this.chkColorByIkouPaper.Name = "chkColorByIkouPaper";
            this.chkColorByIkouPaper.Size = new System.Drawing.Size(95, 19);
            this.chkColorByIkouPaper.TabIndex = 3;
            this.chkColorByIkouPaper.Text = "遺構別色分け";
            this.chkColorByIkouPaper.UseVisualStyleBackColor = true;

            // lblPaperInfoBanner
            this.lblPaperInfoBanner.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaperInfoBanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(0)))));
            this.lblPaperInfoBanner.Location = new System.Drawing.Point(445, 8);
            this.lblPaperInfoBanner.Name = "lblPaperInfoBanner";
            this.lblPaperInfoBanner.Size = new System.Drawing.Size(280, 20);
            this.lblPaperInfoBanner.TabIndex = 5;
            this.lblPaperInfoBanner.Text = "A3 (420×297mm) | 1/10";
            this.lblPaperInfoBanner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // picPaperCanvas
            this.picPaperCanvas.Location = new System.Drawing.Point(100, 35);
            this.picPaperCanvas.Name = "picPaperCanvas";
            this.picPaperCanvas.Size = new System.Drawing.Size(636, 415);
            this.picPaperCanvas.TabIndex = 1;
            this.picPaperCanvas.TabStop = false;

            // 
            // panelBottomFixedGroup (下部単一Panel: 5つのGroupBox + 詳細プレビューの計6項目をすべて内包)
            // 
            this.panelBottomFixedGroup = new System.Windows.Forms.Panel();
            this.panelBottomFixedGroup.Controls.Add(this.grpFeatureDetailPreview);
            this.panelBottomFixedGroup.Controls.Add(this.panelControls);
            this.panelBottomFixedGroup.Controls.Add(this.panelBottomGrids);
            this.panelBottomFixedGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomFixedGroup.Location = new System.Drawing.Point(0, 514);
            this.panelBottomFixedGroup.Name = "panelBottomFixedGroup";
            this.panelBottomFixedGroup.Size = new System.Drawing.Size(1480, 361);
            this.panelBottomFixedGroup.TabIndex = 3;

            // panelBottomGrids
            this.panelBottomGrids.ColumnCount = 3;
            this.panelBottomGrids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.panelBottomGrids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.panelBottomGrids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.panelBottomGrids.Controls.Add(this.grpDrawings, 0, 0);
            this.panelBottomGrids.Controls.Add(this.grpDrawingIkous, 1, 0);
            this.panelBottomGrids.Controls.Add(this.grpDanmenList, 2, 0);
            this.panelBottomGrids.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBottomGrids.Location = new System.Drawing.Point(0, 0);
            this.panelBottomGrids.Name = "panelBottomGrids";
            this.panelBottomGrids.RowCount = 1;
            this.panelBottomGrids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelBottomGrids.Size = new System.Drawing.Size(585, 361);
            this.panelBottomGrids.TabIndex = 0;

            // grpDrawings
            this.grpDrawings.Controls.Add(this.dgvDrawings);
            this.grpDrawings.Controls.Add(this.btnAddDrawing);
            this.grpDrawings.Controls.Add(this.btnDeleteDrawing);
            this.grpDrawings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDrawings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.grpDrawings.Location = new System.Drawing.Point(3, 3);
            this.grpDrawings.Name = "grpDrawings";
            this.grpDrawings.Size = new System.Drawing.Size(193, 355);
            this.grpDrawings.TabIndex = 0;
            this.grpDrawings.TabStop = false;
            this.grpDrawings.Text = "図面名称";

            // dgvDrawings
            this.dgvDrawings.AllowUserToAddRows = false;
            this.dgvDrawings.AllowUserToDeleteRows = false;
            this.dgvDrawings.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvDrawings.Location = new System.Drawing.Point(3, 19);
            this.dgvDrawings.Name = "dgvDrawings";
            this.dgvDrawings.RowTemplate.Height = 25;
            this.dgvDrawings.Size = new System.Drawing.Size(187, 290);
            this.dgvDrawings.TabIndex = 0;

            // btnAddDrawing
            this.btnAddDrawing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.btnAddDrawing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDrawing.ForeColor = System.Drawing.Color.White;
            this.btnAddDrawing.Location = new System.Drawing.Point(6, 320);
            this.btnAddDrawing.Name = "btnAddDrawing";
            this.btnAddDrawing.Size = new System.Drawing.Size(75, 26);
            this.btnAddDrawing.TabIndex = 1;
            this.btnAddDrawing.Text = "➕ 追加";
            this.btnAddDrawing.UseVisualStyleBackColor = false;

            // btnDeleteDrawing
            this.btnDeleteDrawing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnDeleteDrawing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDrawing.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDrawing.Location = new System.Drawing.Point(88, 320);
            this.btnDeleteDrawing.Name = "btnDeleteDrawing";
            this.btnDeleteDrawing.Size = new System.Drawing.Size(75, 26);
            this.btnDeleteDrawing.TabIndex = 2;
            this.btnDeleteDrawing.Text = "削除";
            this.btnDeleteDrawing.UseVisualStyleBackColor = false;

            // grpDrawingIkous
            this.grpDrawingIkous.Controls.Add(this.dgvDrawingIkous);
            this.grpDrawingIkous.Controls.Add(this.btnAddDrawingIkou);
            this.grpDrawingIkous.Controls.Add(this.btnDeleteDrawingIkou);
            this.grpDrawingIkous.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDrawingIkous.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.grpDrawingIkous.Location = new System.Drawing.Point(202, 3);
            this.grpDrawingIkous.Name = "grpDrawingIkous";
            this.grpDrawingIkous.Size = new System.Drawing.Size(187, 355);
            this.grpDrawingIkous.TabIndex = 1;
            this.grpDrawingIkous.TabStop = false;
            this.grpDrawingIkous.Text = "対象遺構図";

            // dgvDrawingIkous
            this.dgvDrawingIkous.AllowUserToAddRows = false;
            this.dgvDrawingIkous.AllowUserToDeleteRows = false;
            this.dgvDrawingIkous.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvDrawingIkous.Location = new System.Drawing.Point(3, 19);
            this.dgvDrawingIkous.Name = "dgvDrawingIkous";
            this.dgvDrawingIkous.RowTemplate.Height = 25;
            this.dgvDrawingIkous.Size = new System.Drawing.Size(181, 290);
            this.dgvDrawingIkous.TabIndex = 0;

            // btnAddDrawingIkou
            this.btnAddDrawingIkou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.btnAddDrawingIkou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDrawingIkou.ForeColor = System.Drawing.Color.White;
            this.btnAddDrawingIkou.Location = new System.Drawing.Point(6, 320);
            this.btnAddDrawingIkou.Name = "btnAddDrawingIkou";
            this.btnAddDrawingIkou.Size = new System.Drawing.Size(75, 26);
            this.btnAddDrawingIkou.TabIndex = 1;
            this.btnAddDrawingIkou.Text = "➕ 追加";
            this.btnAddDrawingIkou.UseVisualStyleBackColor = false;

            // btnDeleteDrawingIkou
            this.btnDeleteDrawingIkou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnDeleteDrawingIkou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDrawingIkou.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDrawingIkou.Location = new System.Drawing.Point(88, 320);
            this.btnDeleteDrawingIkou.Name = "btnDeleteDrawingIkou";
            this.btnDeleteDrawingIkou.Size = new System.Drawing.Size(75, 26);
            this.btnDeleteDrawingIkou.TabIndex = 2;
            this.btnDeleteDrawingIkou.Text = "削除";
            this.btnDeleteDrawingIkou.UseVisualStyleBackColor = false;

            // grpDanmenList
            this.grpDanmenList.Controls.Add(this.dgvDanmen);
            this.grpDanmenList.Controls.Add(this.btnAddDanmen);
            this.grpDanmenList.Controls.Add(this.btnDeleteDanmen);
            this.grpDanmenList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDanmenList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.grpDanmenList.Location = new System.Drawing.Point(395, 3);
            this.grpDanmenList.Name = "grpDanmenList";
            this.grpDanmenList.Size = new System.Drawing.Size(187, 355);
            this.grpDanmenList.TabIndex = 2;
            this.grpDanmenList.TabStop = false;
            this.grpDanmenList.Text = "断面名称";

            // dgvDanmen
            this.dgvDanmen.AllowUserToAddRows = false;
            this.dgvDanmen.AllowUserToDeleteRows = false;
            this.dgvDanmen.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvDanmen.Location = new System.Drawing.Point(3, 19);
            this.dgvDanmen.Name = "dgvDanmen";
            this.dgvDanmen.RowTemplate.Height = 25;
            this.dgvDanmen.Size = new System.Drawing.Size(181, 290);
            this.dgvDanmen.TabIndex = 0;

            // btnAddDanmen
            this.btnAddDanmen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.btnAddDanmen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDanmen.ForeColor = System.Drawing.Color.White;
            this.btnAddDanmen.Location = new System.Drawing.Point(6, 320);
            this.btnAddDanmen.Name = "btnAddDanmen";
            this.btnAddDanmen.Size = new System.Drawing.Size(75, 26);
            this.btnAddDanmen.TabIndex = 1;
            this.btnAddDanmen.Text = "➕ 追加";
            this.btnAddDanmen.UseVisualStyleBackColor = false;

            // btnDeleteDanmen
            this.btnDeleteDanmen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.btnDeleteDanmen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDanmen.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDanmen.Location = new System.Drawing.Point(88, 320);
            this.btnDeleteDanmen.Name = "btnDeleteDanmen";
            this.btnDeleteDanmen.Size = new System.Drawing.Size(75, 26);
            this.btnDeleteDanmen.TabIndex = 2;
            this.btnDeleteDanmen.Text = "削除";
            this.btnDeleteDanmen.UseVisualStyleBackColor = false;

            // panelControls (Middle Property Controls)
            this.panelControls.AutoScroll = true;
            this.panelControls.Controls.Add(this.grpIkouProps);
            this.panelControls.Controls.Add(this.grpDrawingProps);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControls.Location = new System.Drawing.Point(585, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Padding = new System.Windows.Forms.Padding(3);
            this.panelControls.Size = new System.Drawing.Size(410, 361);
            this.panelControls.TabIndex = 1;

            // grpDrawingProps (図面)
            this.grpDrawingProps.Controls.Add(this.chkIsFullDrawing);
            this.grpDrawingProps.Controls.Add(this.btnUpdateDrawingProps);
            this.grpDrawingProps.Controls.Add(this.cmbScale);
            this.grpDrawingProps.Controls.Add(this.lblScale);
            this.grpDrawingProps.Controls.Add(this.cmbPaperSize);
            this.grpDrawingProps.Controls.Add(this.lblPaperSize);
            this.grpDrawingProps.Controls.Add(this.txtDrawingName);
            this.grpDrawingProps.Controls.Add(this.lblDrawingName);
            this.grpDrawingProps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.grpDrawingProps.Location = new System.Drawing.Point(3, 3);
            this.grpDrawingProps.Name = "grpDrawingProps";
            this.grpDrawingProps.Size = new System.Drawing.Size(400, 80);
            this.grpDrawingProps.TabIndex = 0;
            this.grpDrawingProps.TabStop = false;
            this.grpDrawingProps.Text = "図面";

            // lblDrawingName
            this.lblDrawingName.AutoSize = true;
            this.lblDrawingName.ForeColor = System.Drawing.Color.White;
            this.lblDrawingName.Location = new System.Drawing.Point(10, 22);
            this.lblDrawingName.Name = "lblDrawingName";
            this.lblDrawingName.Size = new System.Drawing.Size(46, 15);
            this.lblDrawingName.TabIndex = 0;
            this.lblDrawingName.Text = "図面名";

            // txtDrawingName
            this.txtDrawingName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.txtDrawingName.ForeColor = System.Drawing.Color.Black;
            this.txtDrawingName.Location = new System.Drawing.Point(55, 19);
            this.txtDrawingName.Name = "txtDrawingName";
            this.txtDrawingName.Size = new System.Drawing.Size(85, 23);
            this.txtDrawingName.TabIndex = 1;

            // lblPaperSize
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.ForeColor = System.Drawing.Color.White;
            this.lblPaperSize.Location = new System.Drawing.Point(145, 22);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(35, 15);
            this.lblPaperSize.TabIndex = 2;
            this.lblPaperSize.Text = "用紙";

            // cmbPaperSize
            this.cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(50)))));
            this.cmbPaperSize.ForeColor = System.Drawing.Color.White;
            this.cmbPaperSize.Location = new System.Drawing.Point(182, 19);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(135, 23);
            this.cmbPaperSize.TabIndex = 3;

            // lblScale
            this.lblScale.AutoSize = true;
            this.lblScale.ForeColor = System.Drawing.Color.White;
            this.lblScale.Location = new System.Drawing.Point(8, 50);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(50, 15);
            this.lblScale.TabIndex = 4;
            this.lblScale.Text = "縮尺 1/";

            // cmbScale
            this.cmbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(50)))));
            this.cmbScale.ForeColor = System.Drawing.Color.White;
            this.cmbScale.Location = new System.Drawing.Point(60, 47);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(60, 23);
            this.cmbScale.TabIndex = 5;

            // chkIsFullDrawing
            this.chkIsFullDrawing.AutoSize = true;
            this.chkIsFullDrawing.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkIsFullDrawing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.chkIsFullDrawing.Location = new System.Drawing.Point(125, 48);
            this.chkIsFullDrawing.Name = "chkIsFullDrawing";
            this.chkIsFullDrawing.Size = new System.Drawing.Size(150, 19);
            this.chkIsFullDrawing.TabIndex = 6;
            this.chkIsFullDrawing.Text = "全体遺構図 (トンボ表示)";
            this.chkIsFullDrawing.UseVisualStyleBackColor = true;

            // btnUpdateDrawingProps (図面プロパティの更新ボタン)
            this.btnUpdateDrawingProps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.btnUpdateDrawingProps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateDrawingProps.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateDrawingProps.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateDrawingProps.Location = new System.Drawing.Point(322, 19);
            this.btnUpdateDrawingProps.Name = "btnUpdateDrawingProps";
            this.btnUpdateDrawingProps.Size = new System.Drawing.Size(72, 26);
            this.btnUpdateDrawingProps.TabIndex = 6;
            this.btnUpdateDrawingProps.Text = "図面更新";
            this.btnUpdateDrawingProps.UseVisualStyleBackColor = false;

            // grpIkouProps (遺構 GroupBox: 内側に方位マーク・断面 GroupBox を内包)
            this.grpIkouProps.Controls.Add(this.grpDanmenProps);
            this.grpIkouProps.Controls.Add(this.grpCompassProps);
            this.grpIkouProps.Controls.Add(this.btnSetPaperPosition);
            this.grpIkouProps.Controls.Add(this.btnPickCropBounds);
            this.grpIkouProps.Controls.Add(this.btnUpdateIkouProps);
            this.grpIkouProps.Controls.Add(this.cmbFeatureSelect);
            this.grpIkouProps.Controls.Add(this.lblTargetIkou);
            this.grpIkouProps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.grpIkouProps.Location = new System.Drawing.Point(3, 86);
            this.grpIkouProps.Name = "grpIkouProps";
            this.grpIkouProps.Size = new System.Drawing.Size(400, 260);
            this.grpIkouProps.TabIndex = 1;
            this.grpIkouProps.TabStop = false;
            this.grpIkouProps.Text = "遺構";

            // lblTargetIkou
            this.lblTargetIkou.AutoSize = true;
            this.lblTargetIkou.ForeColor = System.Drawing.Color.White;
            this.lblTargetIkou.Location = new System.Drawing.Point(10, 22);
            this.lblTargetIkou.Name = "lblTargetIkou";
            this.lblTargetIkou.Size = new System.Drawing.Size(43, 15);
            this.lblTargetIkou.TabIndex = 0;
            this.lblTargetIkou.Text = "遺構名";

            // cmbFeatureSelect (編集可能な ComboBox)
            this.cmbFeatureSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbFeatureSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.cmbFeatureSelect.ForeColor = System.Drawing.Color.Black;
            this.cmbFeatureSelect.Location = new System.Drawing.Point(62, 19);
            this.cmbFeatureSelect.Name = "cmbFeatureSelect";
            this.cmbFeatureSelect.Size = new System.Drawing.Size(180, 23);
            this.cmbFeatureSelect.TabIndex = 1;

            // btnUpdateIkouProps (遺構プロパティの更新ボタン)
            this.btnUpdateIkouProps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(0)))));
            this.btnUpdateIkouProps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateIkouProps.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdateIkouProps.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateIkouProps.Location = new System.Drawing.Point(252, 17);
            this.btnUpdateIkouProps.Name = "btnUpdateIkouProps";
            this.btnUpdateIkouProps.Size = new System.Drawing.Size(83, 26);
            this.btnUpdateIkouProps.TabIndex = 2;
            this.btnUpdateIkouProps.Text = "遺構更新";
            this.btnUpdateIkouProps.UseVisualStyleBackColor = false;

            // btnPickCropBounds
            this.btnPickCropBounds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnPickCropBounds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPickCropBounds.ForeColor = System.Drawing.Color.White;
            this.btnPickCropBounds.Location = new System.Drawing.Point(10, 48);
            this.btnPickCropBounds.Name = "btnPickCropBounds";
            this.btnPickCropBounds.Size = new System.Drawing.Size(120, 26);
            this.btnPickCropBounds.TabIndex = 3;
            this.btnPickCropBounds.Text = "遺構枠指示 (3点)";
            this.btnPickCropBounds.UseVisualStyleBackColor = false;

            // btnSetPaperPosition
            this.btnSetPaperPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnSetPaperPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPaperPosition.ForeColor = System.Drawing.Color.White;
            this.btnSetPaperPosition.Location = new System.Drawing.Point(140, 48);
            this.btnSetPaperPosition.Name = "btnSetPaperPosition";
            this.btnSetPaperPosition.Size = new System.Drawing.Size(120, 26);
            this.btnSetPaperPosition.TabIndex = 4;
            this.btnSetPaperPosition.Text = "表示位置指定";
            this.btnSetPaperPosition.UseVisualStyleBackColor = false;

            // grpCompassProps (遺構 GroupBox の内側に配置)
            this.grpCompassProps.Controls.Add(this.btnSetDirectionPosition);
            this.grpCompassProps.Controls.Add(this.chkShowDirection);
            this.grpCompassProps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.grpCompassProps.Location = new System.Drawing.Point(10, 90);
            this.grpCompassProps.Name = "grpCompassProps";
            this.grpCompassProps.Size = new System.Drawing.Size(380, 65);
            this.grpCompassProps.TabIndex = 5;
            this.grpCompassProps.TabStop = false;
            this.grpCompassProps.Text = "方位マーク";

            // chkShowDirection
            this.chkShowDirection.AutoSize = true;
            this.chkShowDirection.Checked = true;
            this.chkShowDirection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowDirection.ForeColor = System.Drawing.Color.White;
            this.chkShowDirection.Location = new System.Drawing.Point(10, 24);
            this.chkShowDirection.Name = "chkShowDirection";
            this.chkShowDirection.Size = new System.Drawing.Size(50, 19);
            this.chkShowDirection.TabIndex = 0;
            this.chkShowDirection.Text = "表示";
            this.chkShowDirection.UseVisualStyleBackColor = true;

            // btnSetDirectionPosition
            this.btnSetDirectionPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnSetDirectionPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetDirectionPosition.ForeColor = System.Drawing.Color.White;
            this.btnSetDirectionPosition.Location = new System.Drawing.Point(70, 20);
            this.btnSetDirectionPosition.Name = "btnSetDirectionPosition";
            this.btnSetDirectionPosition.Size = new System.Drawing.Size(120, 25);
            this.btnSetDirectionPosition.TabIndex = 1;
            this.btnSetDirectionPosition.Text = "表示位置指定";
            this.btnSetDirectionPosition.UseVisualStyleBackColor = false;

            // grpDanmenProps (遺構 GroupBox の内側に配置)
            this.grpDanmenProps.Controls.Add(this.btnSetDanmenPosition);
            this.grpDanmenProps.Controls.Add(this.txtDanmenName);
            this.grpDanmenProps.Controls.Add(this.lblDanmenName);
            this.grpDanmenProps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.grpDanmenProps.Location = new System.Drawing.Point(10, 168);
            this.grpDanmenProps.Name = "grpDanmenProps";
            this.grpDanmenProps.Size = new System.Drawing.Size(380, 82);
            this.grpDanmenProps.TabIndex = 6;
            this.grpDanmenProps.TabStop = false;
            this.grpDanmenProps.Text = "断面 (複数定義可能)";

            // lblDanmenName
            this.lblDanmenName.AutoSize = true;
            this.lblDanmenName.ForeColor = System.Drawing.Color.White;
            this.lblDanmenName.Location = new System.Drawing.Point(10, 24);
            this.lblDanmenName.Name = "lblDanmenName";
            this.lblDanmenName.Size = new System.Drawing.Size(46, 15);
            this.lblDanmenName.TabIndex = 0;
            this.lblDanmenName.Text = "断面名";

            // txtDanmenName
            this.txtDanmenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(191)))));
            this.txtDanmenName.ForeColor = System.Drawing.Color.Black;
            this.txtDanmenName.Location = new System.Drawing.Point(62, 21);
            this.txtDanmenName.Name = "txtDanmenName";
            this.txtDanmenName.Size = new System.Drawing.Size(80, 23);
            this.txtDanmenName.TabIndex = 1;

            // btnSetDanmenPosition
            this.btnSetDanmenPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnSetDanmenPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetDanmenPosition.ForeColor = System.Drawing.Color.White;
            this.btnSetDanmenPosition.Location = new System.Drawing.Point(150, 19);
            this.btnSetDanmenPosition.Name = "btnSetDanmenPosition";
            this.btnSetDanmenPosition.Size = new System.Drawing.Size(100, 26);
            this.btnSetDanmenPosition.TabIndex = 2;
            this.btnSetDanmenPosition.Text = "断面指定";
            this.btnSetDanmenPosition.UseVisualStyleBackColor = false;

            // grpFeatureDetailPreview
            this.grpFeatureDetailPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFeatureDetailPreview.Controls.Add(this.picFeatureDetailCanvas);
            this.grpFeatureDetailPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.grpFeatureDetailPreview.Location = new System.Drawing.Point(995, 0);
            this.grpFeatureDetailPreview.Name = "grpFeatureDetailPreview";
            this.grpFeatureDetailPreview.Size = new System.Drawing.Size(485, 361);
            this.grpFeatureDetailPreview.TabIndex = 2;
            this.grpFeatureDetailPreview.TabStop = false;
            this.grpFeatureDetailPreview.Text = "選択遺構 詳細プレビュー";

            // picFeatureDetailCanvas
            this.picFeatureDetailCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.picFeatureDetailCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picFeatureDetailCanvas.Location = new System.Drawing.Point(3, 19);
            this.picFeatureDetailCanvas.Name = "picFeatureDetailCanvas";
            this.picFeatureDetailCanvas.Size = new System.Drawing.Size(479, 339);
            this.picFeatureDetailCanvas.TabIndex = 0;
            this.picFeatureDetailCanvas.TabStop = false;

            // statusStripBar
            this.statusStripBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.statusStripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusCoords,
            this.lblStatusMessage});
            this.statusStripBar.Location = new System.Drawing.Point(0, 875);
            this.statusStripBar.Name = "statusStripBar";
            this.statusStripBar.Size = new System.Drawing.Size(1480, 22);
            this.statusStripBar.TabIndex = 2;

            // lblStatusCoords
            this.lblStatusCoords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblStatusCoords.Name = "lblStatusCoords";
            this.lblStatusCoords.Size = new System.Drawing.Size(160, 17);
            this.lblStatusCoords.Text = "(-60262.447, 85099.983)";

            // lblStatusMessage
            this.lblStatusMessage.ForeColor = System.Drawing.Color.White;
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(1305, 17);
            this.lblStatusMessage.Spring = true;
            this.lblStatusMessage.Text = "準備完了";
            this.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // FormDrawingEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1520, 960);
            this.MinimumSize = new System.Drawing.Size(1280, 780);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStripBar);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FormDrawingEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "遺跡調査システム(内楽) - 遺構図面作成エディタ";

            this.Controls.Add(this.splitContainerTopHorizontal);
            this.Controls.Add(this.panelBottomFixedGroup);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStripBar);
            this.grpDrawingProps.ResumeLayout(false);
            this.grpDrawingProps.PerformLayout();
            this.grpIkouProps.ResumeLayout(false);
            this.grpIkouProps.PerformLayout();
            this.grpCompassProps.ResumeLayout(false);
            this.grpCompassProps.PerformLayout();
            this.grpDanmenProps.ResumeLayout(false);
            this.grpDanmenProps.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTopHorizontal)).EndInit();
            this.splitContainerTopHorizontal.Panel1.ResumeLayout(false);
            this.splitContainerTopHorizontal.Panel2.ResumeLayout(false);
            this.splitContainerTopHorizontal.ResumeLayout(false);

            this.panelBottomFixedGroup.ResumeLayout(false);
            this.grpFeatureDetailPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFeatureDetailCanvas)).EndInit();

            this.statusStripBar.ResumeLayout(false);
            this.statusStripBar.PerformLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
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

        private System.Windows.Forms.SplitContainer splitContainerTopHorizontal;
        private System.Windows.Forms.Panel panelTopLeft;
        private System.Windows.Forms.Panel panelTopRight;

        private System.Windows.Forms.Panel panelTopLeftHeader;
        private System.Windows.Forms.Label lblFullMapTitle;
        private System.Windows.Forms.Button btnResetCropZoom;
        private System.Windows.Forms.Button btnCropPick3P;
        private System.Windows.Forms.Panel panelHcLeftSidebar;
        private System.Windows.Forms.Button btnBgSettings;
        private System.Windows.Forms.Button btnEnvSettings;
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
        private System.Windows.Forms.Label lblEntityNameHeader;
        private System.Windows.Forms.CheckBox chkShowIbutu;
        private System.Windows.Forms.CheckBox chkShowIbutuName;
        private System.Windows.Forms.CheckBox chkShowIkou;
        private System.Windows.Forms.CheckBox chkShowIkouName;
        private System.Windows.Forms.CheckBox chkShowKikai;
        private System.Windows.Forms.CheckBox chkShowKikaiName;
        private System.Windows.Forms.CheckBox chkShowIkouFull;
        private System.Windows.Forms.CheckBox chkShowIbutuFull;
        private System.Windows.Forms.CheckBox chkShowKikaiFull;
        private System.Windows.Forms.CheckBox chkShowCurveFull;
        private System.Windows.Forms.CheckBox chkShowGridFull;
        private System.Windows.Forms.PictureBox picCropCanvas;

        private System.Windows.Forms.Panel panelTopRightHeader;
        private System.Windows.Forms.Label lblPaperSheetTitle;
        private System.Windows.Forms.Button btnResetPaperZoom;
        private System.Windows.Forms.CheckBox chkShowCurvePaper;
        private System.Windows.Forms.CheckBox chkShowDirectionPaper;
        private System.Windows.Forms.CheckBox chkShowDanmenPaper;
        private System.Windows.Forms.Label lblPaperInfoBanner;
        private System.Windows.Forms.PictureBox picPaperCanvas;

        private System.Windows.Forms.Panel panelBottomFixedGroup;
        private System.Windows.Forms.TableLayoutPanel panelBottomGrids;

        private System.Windows.Forms.GroupBox grpDrawings;
        private System.Windows.Forms.DataGridView dgvDrawings;
        private System.Windows.Forms.Button btnAddDrawing;
        private System.Windows.Forms.Button btnDeleteDrawing;

        private System.Windows.Forms.GroupBox grpDrawingIkous;
        private System.Windows.Forms.DataGridView dgvDrawingIkous;
        private System.Windows.Forms.Button btnAddDrawingIkou;
        private System.Windows.Forms.Button btnDeleteDrawingIkou;

        private System.Windows.Forms.GroupBox grpDanmenList;
        private System.Windows.Forms.DataGridView dgvDanmen;
        private System.Windows.Forms.Button btnAddDanmen;
        private System.Windows.Forms.Button btnDeleteDanmen;

        private System.Windows.Forms.Panel panelControls;

        private System.Windows.Forms.GroupBox grpDrawingProps;
        private System.Windows.Forms.CheckBox chkIsFullDrawing;
        private System.Windows.Forms.Label lblDrawingName;
        private System.Windows.Forms.TextBox txtDrawingName;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.Button btnUpdateDrawingProps;

        private System.Windows.Forms.GroupBox grpIkouProps;
        private System.Windows.Forms.Label lblTargetIkou;
        private System.Windows.Forms.ComboBox cmbFeatureSelect;
        private System.Windows.Forms.Button btnUpdateIkouProps;
        private System.Windows.Forms.Button btnPickCropBounds;
        private System.Windows.Forms.Button btnSetPaperPosition;

        private System.Windows.Forms.GroupBox grpCompassProps;
        private System.Windows.Forms.CheckBox chkShowDirection;
        private System.Windows.Forms.Button btnSetDirectionPosition;

        private System.Windows.Forms.GroupBox grpDanmenProps;
        private System.Windows.Forms.Label lblDanmenName;
        private System.Windows.Forms.TextBox txtDanmenName;
        private System.Windows.Forms.Button btnSetDanmenPosition;

        private System.Windows.Forms.GroupBox grpFeatureDetailPreview;
        private System.Windows.Forms.PictureBox picFeatureDetailCanvas;

        private System.Windows.Forms.CheckBox chkColorByIkouFull;
        private System.Windows.Forms.CheckBox chkColorByIkouPaper;

        private System.Windows.Forms.StatusStrip statusStripBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCoords;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMessage;
    }
}
