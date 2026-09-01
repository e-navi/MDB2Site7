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

        private void InitializeComponent() {
            panelHeader = new Panel();
            btnExit = new Button();
            lblDbStatus = new Label();
            btnSaveDb = new Button();
            lblSubHeader = new Label();
            lblHeaderTitle = new Label();
            cmbOrientation = new ComboBox();
            btnResetCropZoom = new Button();
            chkAutoZoomIkou = new CheckBox();
            panelHcLeftSidebar = new Panel();
            btnEnvSettings = new Button();
            lblIkouLayerGrpHeader = new Label();
            chkLayer01 = new CheckBox();
            chkLayer02 = new CheckBox();
            chkLayer03 = new CheckBox();
            chkLayer04 = new CheckBox();
            chkLayer05 = new CheckBox();
            chkLayer06 = new CheckBox();
            chkLayer07 = new CheckBox();
            chkLayer08 = new CheckBox();
            chkLayer09 = new CheckBox();
            chkLayer10 = new CheckBox();
            chkLayer11 = new CheckBox();
            chkLayer12 = new CheckBox();
            chkLayer13 = new CheckBox();
            chkLayer14 = new CheckBox();
            chkLayer15 = new CheckBox();
            chkLayer16 = new CheckBox();
            btnLayerAllOn = new Button();
            btnLayerAllOff = new Button();
            btnLayerSettings = new Button();
            lblEntityNameHeader = new Label();
            chkShowIbutu = new CheckBox();
            chkShowIbutuName = new CheckBox();
            chkShowIkou = new CheckBox();
            chkShowIkouName = new CheckBox();
            chkShowKikai = new CheckBox();
            chkShowKikaiName = new CheckBox();
            chkShowWhiteBackground = new CheckBox();
            chkShowIkouFull = new CheckBox();
            chkShowIbutuFull = new CheckBox();
            chkShowKikaiFull = new CheckBox();
            chkShowCurveFull = new CheckBox();
            chkShowGridFull = new CheckBox();
            chkColorByIkouFull = new CheckBox();
            picCropCanvas = new PictureBox();
            panelTopRightHeader = new Panel();
            lblPaperInfoBanner = new Label();
            chkAutoZoomPaperIkou = new CheckBox();
            btnResetPaperZoom = new Button();
            lblPaperSheetTitle = new Label();
            picPaperCanvas = new PictureBox();
            splitContainerTopHorizontal = new SplitContainer();
            panelTopLeft = new Panel();
            panelTopRight = new Panel();
            panelBottomFixedGroup = new Panel();
            grpFeatureDetailPreview = new GroupBox();
            picFeatureDetailCanvas = new PictureBox();
            panelControls = new Panel();
            grpIkouProps = new GroupBox();
            grpDanmenProps = new GroupBox();
            btnSetDanmenPosition = new Button();
            txtDanmenName = new TextBox();
            lblDanmenName = new Label();
            grpCompassProps = new GroupBox();
            btnSetDirectionPosition = new Button();
            chkShowDirection = new CheckBox();
            btnSetPaperPosition = new Button();
            btnPickCropBounds = new Button();
            cmbFeatureSelect = new ComboBox();
            lblTargetIkou = new Label();
            grpDrawingProps = new GroupBox();
            cmbScale = new ComboBox();
            lblScale = new Label();
            cmbPaperSize = new ComboBox();
            lblPaperSize = new Label();
            txtDrawingName = new TextBox();
            lblDrawingName = new Label();
            panelBottomGrids = new TableLayoutPanel();
            grpDrawings = new GroupBox();
            dgvDrawings = new DataGridView();
            btnDeleteDrawing = new Button();
            btnUpdateDrawingProps = new Button();
            btnAddDrawing = new Button();
            grpDrawingIkous = new GroupBox();
            dgvDrawingIkous = new DataGridView();
            btnDeleteDrawingIkou = new Button();
            btnUpdateIkouProps = new Button();
            btnAddDrawingIkou = new Button();
            grpDanmenList = new GroupBox();
            dgvDanmen = new DataGridView();
            btnDeleteDanmen = new Button();
            btnUpdateDanmenName = new Button();
            btnAddDanmen = new Button();
            statusStripBar = new StatusStrip();
            lblStatusCoords = new ToolStripStatusLabel();
            lblStatusMessage = new ToolStripStatusLabel();
            panelHeader.SuspendLayout();
            panelHcLeftSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCropCanvas).BeginInit();
            panelTopRightHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPaperCanvas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerTopHorizontal).BeginInit();
            splitContainerTopHorizontal.Panel1.SuspendLayout();
            splitContainerTopHorizontal.Panel2.SuspendLayout();
            splitContainerTopHorizontal.SuspendLayout();
            panelTopLeft.SuspendLayout();
            panelTopRight.SuspendLayout();
            panelBottomFixedGroup.SuspendLayout();
            grpFeatureDetailPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picFeatureDetailCanvas).BeginInit();
            panelControls.SuspendLayout();
            grpIkouProps.SuspendLayout();
            grpDanmenProps.SuspendLayout();
            grpCompassProps.SuspendLayout();
            grpDrawingProps.SuspendLayout();
            panelBottomGrids.SuspendLayout();
            grpDrawings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDrawings).BeginInit();
            grpDrawingIkous.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDrawingIkous).BeginInit();
            grpDanmenList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDanmen).BeginInit();
            statusStripBar.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(24, 30, 42);
            panelHeader.Controls.Add(btnExit);
            panelHeader.Controls.Add(lblDbStatus);
            panelHeader.Controls.Add(btnSaveDb);
            panelHeader.Controls.Add(lblSubHeader);
            panelHeader.Controls.Add(lblHeaderTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1520, 60);
            panelHeader.TabIndex = 0;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.BackColor = Color.FromArgb(70, 75, 95);
            btnExit.FlatAppearance.BorderColor = Color.FromArgb(100, 105, 130);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(1365, 12);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(100, 35);
            btnExit.TabIndex = 4;
            btnExit.Text = "✖ 終了";
            btnExit.UseVisualStyleBackColor = false;
            // 
            // lblDbStatus
            // 
            lblDbStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDbStatus.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold);
            lblDbStatus.ForeColor = Color.FromArgb(255, 193, 7);
            lblDbStatus.Location = new Point(500, 15);
            lblDbStatus.Name = "lblDbStatus";
            lblDbStatus.Size = new Size(850, 30);
            lblDbStatus.TabIndex = 3;
            lblDbStatus.Text = "DB未読み込み";
            lblDbStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnSaveDb
            // 
            btnSaveDb.BackColor = Color.FromArgb(40, 167, 69);
            btnSaveDb.FlatStyle = FlatStyle.Flat;
            btnSaveDb.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold);
            btnSaveDb.ForeColor = Color.White;
            btnSaveDb.Location = new Point(360, 12);
            btnSaveDb.Name = "btnSaveDb";
            btnSaveDb.Size = new Size(120, 35);
            btnSaveDb.TabIndex = 2;
            btnSaveDb.Text = "💾 DB保存";
            btnSaveDb.UseVisualStyleBackColor = false;
            // 
            // lblSubHeader
            // 
            lblSubHeader.AutoSize = true;
            lblSubHeader.Font = new Font("Yu Gothic UI", 8.5F);
            lblSubHeader.ForeColor = Color.FromArgb(170, 185, 210);
            lblSubHeader.Location = new Point(14, 35);
            lblSubHeader.Name = "lblSubHeader";
            lblSubHeader.Size = new Size(277, 15);
            lblSubHeader.TabIndex = 1;
            lblSubHeader.Text = "全図3点切り出し・用紙図面レイアウト・断面図連動保存";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(12, 8);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(351, 25);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "📐 SITE7 遺跡調査・遺構図面作成システム";
            // 
            // cmbOrientation
            // 
            cmbOrientation.BackColor = Color.White;
            cmbOrientation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrientation.ForeColor = Color.FromArgb(33, 37, 41);
            cmbOrientation.Location = new Point(190, 47);
            cmbOrientation.Name = "cmbOrientation";
            cmbOrientation.Size = new Size(53, 23);
            cmbOrientation.TabIndex = 7;
            // 
            // btnResetCropZoom
            // 
            btnResetCropZoom.BackColor = Color.White;
            btnResetCropZoom.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnResetCropZoom.ForeColor = Color.FromArgb(20, 20, 20);
            btnResetCropZoom.Location = new Point(6, 5);
            btnResetCropZoom.Name = "btnResetCropZoom";
            btnResetCropZoom.Size = new Size(118, 25);
            btnResetCropZoom.TabIndex = 0;
            btnResetCropZoom.Text = "🔍 全図表示";
            btnResetCropZoom.UseVisualStyleBackColor = true;
            // 
            // chkAutoZoomIkou
            // 
            chkAutoZoomIkou.AutoSize = true;
            chkAutoZoomIkou.Checked = true;
            chkAutoZoomIkou.CheckState = CheckState.Checked;
            chkAutoZoomIkou.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkAutoZoomIkou.ForeColor = Color.FromArgb(30, 40, 60);
            chkAutoZoomIkou.Location = new Point(6, 32);
            chkAutoZoomIkou.Name = "chkAutoZoomIkou";
            chkAutoZoomIkou.Size = new Size(107, 19);
            chkAutoZoomIkou.TabIndex = 1;
            chkAutoZoomIkou.Text = "選択遺構を拡大";
            chkAutoZoomIkou.UseVisualStyleBackColor = true;
            // 
            // panelHcLeftSidebar
            // 
            panelHcLeftSidebar.BackColor = Color.FromArgb(240, 242, 245);
            panelHcLeftSidebar.Controls.Add(btnResetCropZoom);
            panelHcLeftSidebar.Controls.Add(chkAutoZoomIkou);
            panelHcLeftSidebar.Controls.Add(btnEnvSettings);
            panelHcLeftSidebar.Controls.Add(lblIkouLayerGrpHeader);
            panelHcLeftSidebar.Controls.Add(chkLayer01);
            panelHcLeftSidebar.Controls.Add(chkLayer02);
            panelHcLeftSidebar.Controls.Add(chkLayer03);
            panelHcLeftSidebar.Controls.Add(chkLayer04);
            panelHcLeftSidebar.Controls.Add(chkLayer05);
            panelHcLeftSidebar.Controls.Add(chkLayer06);
            panelHcLeftSidebar.Controls.Add(chkLayer07);
            panelHcLeftSidebar.Controls.Add(chkLayer08);
            panelHcLeftSidebar.Controls.Add(chkLayer09);
            panelHcLeftSidebar.Controls.Add(chkLayer10);
            panelHcLeftSidebar.Controls.Add(chkLayer11);
            panelHcLeftSidebar.Controls.Add(chkLayer12);
            panelHcLeftSidebar.Controls.Add(chkLayer13);
            panelHcLeftSidebar.Controls.Add(chkLayer14);
            panelHcLeftSidebar.Controls.Add(chkLayer15);
            panelHcLeftSidebar.Controls.Add(chkLayer16);
            panelHcLeftSidebar.Controls.Add(btnLayerAllOn);
            panelHcLeftSidebar.Controls.Add(btnLayerAllOff);
            panelHcLeftSidebar.Controls.Add(btnLayerSettings);
            panelHcLeftSidebar.Controls.Add(lblEntityNameHeader);
            panelHcLeftSidebar.Controls.Add(chkShowIbutu);
            panelHcLeftSidebar.Controls.Add(chkShowIbutuName);
            panelHcLeftSidebar.Controls.Add(chkShowIkou);
            panelHcLeftSidebar.Controls.Add(chkShowIkouName);
            panelHcLeftSidebar.Controls.Add(chkShowKikai);
            panelHcLeftSidebar.Controls.Add(chkShowKikaiName);
            panelHcLeftSidebar.Controls.Add(chkShowWhiteBackground);
            panelHcLeftSidebar.Location = new Point(0, 0);
            panelHcLeftSidebar.Name = "panelHcLeftSidebar";
            panelHcLeftSidebar.Size = new Size(130, 450);
            panelHcLeftSidebar.TabIndex = 1;
            // 
            // btnEnvSettings
            // 
            btnEnvSettings.BackColor = Color.White;
            btnEnvSettings.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnEnvSettings.ForeColor = Color.FromArgb(20, 20, 20);
            btnEnvSettings.Location = new Point(6, 55);
            btnEnvSettings.Name = "btnEnvSettings";
            btnEnvSettings.Size = new Size(118, 25);
            btnEnvSettings.TabIndex = 2;
            btnEnvSettings.Text = "遺構図面設定";
            btnEnvSettings.UseVisualStyleBackColor = true;
            // 
            // lblIkouLayerGrpHeader
            // 
            lblIkouLayerGrpHeader.AutoSize = true;
            lblIkouLayerGrpHeader.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            lblIkouLayerGrpHeader.ForeColor = Color.Red;
            lblIkouLayerGrpHeader.Location = new Point(6, 83);
            lblIkouLayerGrpHeader.Name = "lblIkouLayerGrpHeader";
            lblIkouLayerGrpHeader.Size = new Size(59, 15);
            lblIkouLayerGrpHeader.TabIndex = 3;
            lblIkouLayerGrpHeader.Text = "遺構レイヤ";
            // 
            // chkLayer01
            // 
            chkLayer01.AutoSize = true;
            chkLayer01.Checked = true;
            chkLayer01.CheckState = CheckState.Checked;
            chkLayer01.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer01.ForeColor = Color.Black;
            chkLayer01.Location = new Point(6, 101);
            chkLayer01.Name = "chkLayer01";
            chkLayer01.Size = new Size(44, 19);
            chkLayer01.TabIndex = 5;
            chkLayer01.Text = "L01";
            chkLayer01.UseVisualStyleBackColor = true;
            // 
            // chkLayer02
            // 
            chkLayer02.AutoSize = true;
            chkLayer02.Checked = true;
            chkLayer02.CheckState = CheckState.Checked;
            chkLayer02.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer02.ForeColor = Color.Red;
            chkLayer02.Location = new Point(6, 121);
            chkLayer02.Name = "chkLayer02";
            chkLayer02.Size = new Size(46, 19);
            chkLayer02.TabIndex = 6;
            chkLayer02.Text = "L02";
            chkLayer02.UseVisualStyleBackColor = true;
            // 
            // chkLayer03
            // 
            chkLayer03.AutoSize = true;
            chkLayer03.Checked = true;
            chkLayer03.CheckState = CheckState.Checked;
            chkLayer03.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer03.ForeColor = Color.FromArgb(0, 180, 0);
            chkLayer03.Location = new Point(6, 141);
            chkLayer03.Name = "chkLayer03";
            chkLayer03.Size = new Size(46, 19);
            chkLayer03.TabIndex = 7;
            chkLayer03.Text = "L03";
            chkLayer03.UseVisualStyleBackColor = true;
            // 
            // chkLayer04
            // 
            chkLayer04.AutoSize = true;
            chkLayer04.Checked = true;
            chkLayer04.CheckState = CheckState.Checked;
            chkLayer04.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer04.ForeColor = Color.Blue;
            chkLayer04.Location = new Point(6, 161);
            chkLayer04.Name = "chkLayer04";
            chkLayer04.Size = new Size(46, 19);
            chkLayer04.TabIndex = 8;
            chkLayer04.Text = "L04";
            chkLayer04.UseVisualStyleBackColor = true;
            // 
            // chkLayer05
            // 
            chkLayer05.AutoSize = true;
            chkLayer05.Checked = true;
            chkLayer05.CheckState = CheckState.Checked;
            chkLayer05.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer05.ForeColor = Color.FromArgb(200, 180, 0);
            chkLayer05.Location = new Point(6, 181);
            chkLayer05.Name = "chkLayer05";
            chkLayer05.Size = new Size(46, 19);
            chkLayer05.TabIndex = 9;
            chkLayer05.Text = "L05";
            chkLayer05.UseVisualStyleBackColor = true;
            // 
            // chkLayer06
            // 
            chkLayer06.AutoSize = true;
            chkLayer06.Checked = true;
            chkLayer06.CheckState = CheckState.Checked;
            chkLayer06.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer06.ForeColor = Color.Magenta;
            chkLayer06.Location = new Point(6, 201);
            chkLayer06.Name = "chkLayer06";
            chkLayer06.Size = new Size(46, 19);
            chkLayer06.TabIndex = 10;
            chkLayer06.Text = "L06";
            chkLayer06.UseVisualStyleBackColor = true;
            // 
            // chkLayer07
            // 
            chkLayer07.AutoSize = true;
            chkLayer07.Checked = true;
            chkLayer07.CheckState = CheckState.Checked;
            chkLayer07.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer07.ForeColor = Color.DeepSkyBlue;
            chkLayer07.Location = new Point(6, 221);
            chkLayer07.Name = "chkLayer07";
            chkLayer07.Size = new Size(45, 19);
            chkLayer07.TabIndex = 11;
            chkLayer07.Text = "L07";
            chkLayer07.UseVisualStyleBackColor = true;
            // 
            // chkLayer08
            // 
            chkLayer08.AutoSize = true;
            chkLayer08.Checked = true;
            chkLayer08.CheckState = CheckState.Checked;
            chkLayer08.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer08.ForeColor = Color.DarkGray;
            chkLayer08.Location = new Point(6, 241);
            chkLayer08.Name = "chkLayer08";
            chkLayer08.Size = new Size(46, 19);
            chkLayer08.TabIndex = 12;
            chkLayer08.Text = "L08";
            chkLayer08.UseVisualStyleBackColor = true;
            // 
            // chkLayer09
            // 
            chkLayer09.AutoSize = true;
            chkLayer09.Checked = true;
            chkLayer09.CheckState = CheckState.Checked;
            chkLayer09.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer09.ForeColor = Color.FromArgb(192, 0, 128);
            chkLayer09.Location = new Point(66, 101);
            chkLayer09.Name = "chkLayer09";
            chkLayer09.Size = new Size(46, 19);
            chkLayer09.TabIndex = 13;
            chkLayer09.Text = "L09";
            chkLayer09.UseVisualStyleBackColor = true;
            // 
            // chkLayer10
            // 
            chkLayer10.AutoSize = true;
            chkLayer10.Checked = true;
            chkLayer10.CheckState = CheckState.Checked;
            chkLayer10.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer10.ForeColor = Color.FromArgb(192, 128, 64);
            chkLayer10.Location = new Point(66, 121);
            chkLayer10.Name = "chkLayer10";
            chkLayer10.Size = new Size(44, 19);
            chkLayer10.TabIndex = 14;
            chkLayer10.Text = "L10";
            chkLayer10.UseVisualStyleBackColor = true;
            // 
            // chkLayer11
            // 
            chkLayer11.AutoSize = true;
            chkLayer11.Checked = true;
            chkLayer11.CheckState = CheckState.Checked;
            chkLayer11.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer11.ForeColor = Color.FromArgb(255, 128, 0);
            chkLayer11.Location = new Point(66, 141);
            chkLayer11.Name = "chkLayer11";
            chkLayer11.Size = new Size(42, 19);
            chkLayer11.TabIndex = 15;
            chkLayer11.Text = "L11";
            chkLayer11.UseVisualStyleBackColor = true;
            // 
            // chkLayer12
            // 
            chkLayer12.AutoSize = true;
            chkLayer12.Checked = true;
            chkLayer12.CheckState = CheckState.Checked;
            chkLayer12.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer12.ForeColor = Color.FromArgb(128, 192, 128);
            chkLayer12.Location = new Point(66, 161);
            chkLayer12.Name = "chkLayer12";
            chkLayer12.Size = new Size(44, 19);
            chkLayer12.TabIndex = 16;
            chkLayer12.Text = "L12";
            chkLayer12.UseVisualStyleBackColor = true;
            // 
            // chkLayer13
            // 
            chkLayer13.AutoSize = true;
            chkLayer13.Checked = true;
            chkLayer13.CheckState = CheckState.Checked;
            chkLayer13.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer13.ForeColor = Color.FromArgb(0, 128, 255);
            chkLayer13.Location = new Point(66, 181);
            chkLayer13.Name = "chkLayer13";
            chkLayer13.Size = new Size(44, 19);
            chkLayer13.TabIndex = 17;
            chkLayer13.Text = "L13";
            chkLayer13.UseVisualStyleBackColor = true;
            // 
            // chkLayer14
            // 
            chkLayer14.AutoSize = true;
            chkLayer14.Checked = true;
            chkLayer14.CheckState = CheckState.Checked;
            chkLayer14.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer14.ForeColor = Color.FromArgb(128, 64, 255);
            chkLayer14.Location = new Point(66, 201);
            chkLayer14.Name = "chkLayer14";
            chkLayer14.Size = new Size(44, 19);
            chkLayer14.TabIndex = 18;
            chkLayer14.Text = "L14";
            chkLayer14.UseVisualStyleBackColor = true;
            // 
            // chkLayer15
            // 
            chkLayer15.AutoSize = true;
            chkLayer15.Checked = true;
            chkLayer15.CheckState = CheckState.Checked;
            chkLayer15.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer15.ForeColor = Color.FromArgb(180, 180, 180);
            chkLayer15.Location = new Point(66, 221);
            chkLayer15.Name = "chkLayer15";
            chkLayer15.Size = new Size(44, 19);
            chkLayer15.TabIndex = 19;
            chkLayer15.Text = "L15";
            chkLayer15.UseVisualStyleBackColor = true;
            // 
            // chkLayer16
            // 
            chkLayer16.AutoSize = true;
            chkLayer16.Checked = true;
            chkLayer16.CheckState = CheckState.Checked;
            chkLayer16.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkLayer16.ForeColor = Color.FromArgb(100, 100, 100);
            chkLayer16.Location = new Point(66, 241);
            chkLayer16.Name = "chkLayer16";
            chkLayer16.Size = new Size(44, 19);
            chkLayer16.TabIndex = 20;
            chkLayer16.Text = "L16";
            chkLayer16.UseVisualStyleBackColor = true;
            // 
            // btnLayerAllOn
            // 
            btnLayerAllOn.BackColor = Color.White;
            btnLayerAllOn.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnLayerAllOn.ForeColor = Color.FromArgb(20, 20, 20);
            btnLayerAllOn.Location = new Point(6, 263);
            btnLayerAllOn.Name = "btnLayerAllOn";
            btnLayerAllOn.Size = new Size(57, 23);
            btnLayerAllOn.TabIndex = 21;
            btnLayerAllOn.Text = "全ON";
            btnLayerAllOn.UseVisualStyleBackColor = true;
            // 
            // btnLayerAllOff
            // 
            btnLayerAllOff.BackColor = Color.White;
            btnLayerAllOff.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnLayerAllOff.ForeColor = Color.FromArgb(20, 20, 20);
            btnLayerAllOff.Location = new Point(67, 263);
            btnLayerAllOff.Name = "btnLayerAllOff";
            btnLayerAllOff.Size = new Size(57, 23);
            btnLayerAllOff.TabIndex = 22;
            btnLayerAllOff.Text = "全OFF";
            btnLayerAllOff.UseVisualStyleBackColor = true;
            // 
            // btnLayerSettings
            // 
            btnLayerSettings.BackColor = Color.White;
            btnLayerSettings.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnLayerSettings.ForeColor = Color.FromArgb(20, 20, 20);
            btnLayerSettings.Location = new Point(6, 289);
            btnLayerSettings.Name = "btnLayerSettings";
            btnLayerSettings.Size = new Size(118, 23);
            btnLayerSettings.TabIndex = 23;
            btnLayerSettings.Text = "レイヤ設定";
            btnLayerSettings.UseVisualStyleBackColor = true;
            // 
            // lblEntityNameHeader
            // 
            lblEntityNameHeader.AutoSize = true;
            lblEntityNameHeader.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            lblEntityNameHeader.ForeColor = Color.Red;
            lblEntityNameHeader.Location = new Point(82, 316);
            lblEntityNameHeader.Name = "lblEntityNameHeader";
            lblEntityNameHeader.Size = new Size(31, 15);
            lblEntityNameHeader.TabIndex = 24;
            lblEntityNameHeader.Text = "名称";
            // 
            // chkShowIbutu
            // 
            chkShowIbutu.AutoSize = true;
            chkShowIbutu.Checked = true;
            chkShowIbutu.CheckState = CheckState.Checked;
            chkShowIbutu.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            chkShowIbutu.ForeColor = Color.Red;
            chkShowIbutu.Location = new Point(6, 333);
            chkShowIbutu.Name = "chkShowIbutu";
            chkShowIbutu.Size = new Size(50, 19);
            chkShowIbutu.TabIndex = 25;
            chkShowIbutu.Text = "遺物";
            chkShowIbutu.UseVisualStyleBackColor = true;
            // 
            // chkShowIbutuName
            // 
            chkShowIbutuName.AutoSize = true;
            chkShowIbutuName.Location = new Point(90, 336);
            chkShowIbutuName.Name = "chkShowIbutuName";
            chkShowIbutuName.Size = new Size(15, 14);
            chkShowIbutuName.TabIndex = 26;
            chkShowIbutuName.UseVisualStyleBackColor = true;
            // 
            // chkShowIkou
            // 
            chkShowIkou.AutoSize = true;
            chkShowIkou.Checked = true;
            chkShowIkou.CheckState = CheckState.Checked;
            chkShowIkou.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            chkShowIkou.ForeColor = Color.Blue;
            chkShowIkou.Location = new Point(6, 355);
            chkShowIkou.Name = "chkShowIkou";
            chkShowIkou.Size = new Size(50, 19);
            chkShowIkou.TabIndex = 27;
            chkShowIkou.Text = "遺構";
            chkShowIkou.UseVisualStyleBackColor = true;
            // 
            // chkShowIkouName
            // 
            chkShowIkouName.AutoSize = true;
            chkShowIkouName.Location = new Point(90, 358);
            chkShowIkouName.Name = "chkShowIkouName";
            chkShowIkouName.Size = new Size(15, 14);
            chkShowIkouName.TabIndex = 28;
            chkShowIkouName.UseVisualStyleBackColor = true;
            // 
            // chkShowKikai
            // 
            chkShowKikai.AutoSize = true;
            chkShowKikai.Checked = true;
            chkShowKikai.CheckState = CheckState.Checked;
            chkShowKikai.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            chkShowKikai.ForeColor = Color.Blue;
            chkShowKikai.Location = new Point(6, 377);
            chkShowKikai.Name = "chkShowKikai";
            chkShowKikai.Size = new Size(62, 19);
            chkShowKikai.TabIndex = 29;
            chkShowKikai.Text = "基準点";
            chkShowKikai.UseVisualStyleBackColor = true;
            // 
            // chkShowKikaiName
            // 
            chkShowKikaiName.AutoSize = true;
            chkShowKikaiName.Checked = true;
            chkShowKikaiName.CheckState = CheckState.Checked;
            chkShowKikaiName.Location = new Point(90, 380);
            chkShowKikaiName.Name = "chkShowKikaiName";
            chkShowKikaiName.Size = new Size(15, 14);
            chkShowKikaiName.TabIndex = 30;
            chkShowKikaiName.UseVisualStyleBackColor = true;
            // 
            // chkShowWhiteBackground
            // 
            chkShowWhiteBackground.AutoSize = true;
            chkShowWhiteBackground.Checked = true;
            chkShowWhiteBackground.CheckState = CheckState.Checked;
            chkShowWhiteBackground.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            chkShowWhiteBackground.ForeColor = Color.FromArgb(30, 40, 60);
            chkShowWhiteBackground.Location = new Point(6, 400);
            chkShowWhiteBackground.Name = "chkShowWhiteBackground";
            chkShowWhiteBackground.Size = new Size(74, 19);
            chkShowWhiteBackground.TabIndex = 31;
            chkShowWhiteBackground.Text = "背景白色";
            chkShowWhiteBackground.UseVisualStyleBackColor = true;
            // 
            // chkShowIkouFull
            // 
            chkShowIkouFull.Location = new Point(0, 0);
            chkShowIkouFull.Name = "chkShowIkouFull";
            chkShowIkouFull.Size = new Size(104, 24);
            chkShowIkouFull.TabIndex = 0;
            // 
            // chkShowIbutuFull
            // 
            chkShowIbutuFull.Location = new Point(0, 0);
            chkShowIbutuFull.Name = "chkShowIbutuFull";
            chkShowIbutuFull.Size = new Size(104, 24);
            chkShowIbutuFull.TabIndex = 0;
            // 
            // chkShowKikaiFull
            // 
            chkShowKikaiFull.Location = new Point(0, 0);
            chkShowKikaiFull.Name = "chkShowKikaiFull";
            chkShowKikaiFull.Size = new Size(104, 24);
            chkShowKikaiFull.TabIndex = 0;
            // 
            // chkShowCurveFull
            // 
            chkShowCurveFull.Location = new Point(0, 0);
            chkShowCurveFull.Name = "chkShowCurveFull";
            chkShowCurveFull.Size = new Size(104, 24);
            chkShowCurveFull.TabIndex = 0;
            // 
            // chkShowGridFull
            // 
            chkShowGridFull.Location = new Point(0, 0);
            chkShowGridFull.Name = "chkShowGridFull";
            chkShowGridFull.Size = new Size(104, 24);
            chkShowGridFull.TabIndex = 0;
            // 
            // chkColorByIkouFull
            // 
            chkColorByIkouFull.Location = new Point(0, 0);
            chkColorByIkouFull.Name = "chkColorByIkouFull";
            chkColorByIkouFull.Size = new Size(104, 24);
            chkColorByIkouFull.TabIndex = 0;
            // 
            // picCropCanvas
            // 
            picCropCanvas.Location = new Point(130, 35);
            picCropCanvas.Name = "picCropCanvas";
            picCropCanvas.Size = new Size(610, 415);
            picCropCanvas.TabIndex = 1;
            picCropCanvas.TabStop = false;
            // 
            // panelTopRightHeader
            // 
            panelTopRightHeader.BackColor = Color.FromArgb(233, 236, 243);
            panelTopRightHeader.Controls.Add(lblPaperInfoBanner);
            panelTopRightHeader.Controls.Add(chkAutoZoomPaperIkou);
            panelTopRightHeader.Controls.Add(btnResetPaperZoom);
            panelTopRightHeader.Controls.Add(lblPaperSheetTitle);
            panelTopRightHeader.Dock = DockStyle.Top;
            panelTopRightHeader.Location = new Point(0, 0);
            panelTopRightHeader.Name = "panelTopRightHeader";
            panelTopRightHeader.Size = new Size(736, 35);
            panelTopRightHeader.TabIndex = 0;
            // 
            // lblPaperInfoBanner
            // 
            lblPaperInfoBanner.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            lblPaperInfoBanner.ForeColor = Color.FromArgb(255, 191, 0);
            lblPaperInfoBanner.Location = new Point(445, 8);
            lblPaperInfoBanner.Name = "lblPaperInfoBanner";
            lblPaperInfoBanner.Size = new Size(280, 20);
            lblPaperInfoBanner.TabIndex = 5;
            lblPaperInfoBanner.Text = "A3 (420×297mm) | 1/10";
            lblPaperInfoBanner.TextAlign = ContentAlignment.MiddleRight;
            // 
            // chkAutoZoomPaperIkou
            // 
            chkAutoZoomPaperIkou.AutoSize = true;
            chkAutoZoomPaperIkou.Checked = true;
            chkAutoZoomPaperIkou.CheckState = CheckState.Checked;
            chkAutoZoomPaperIkou.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            chkAutoZoomPaperIkou.ForeColor = Color.FromArgb(30, 40, 60);
            chkAutoZoomPaperIkou.Location = new Point(195, 8);
            chkAutoZoomPaperIkou.Name = "chkAutoZoomPaperIkou";
            chkAutoZoomPaperIkou.Size = new Size(111, 19);
            chkAutoZoomPaperIkou.TabIndex = 2;
            chkAutoZoomPaperIkou.Text = "選択遺構を拡大";
            chkAutoZoomPaperIkou.UseVisualStyleBackColor = true;
            // 
            // btnResetPaperZoom
            // 
            btnResetPaperZoom.BackColor = Color.FromArgb(215, 222, 235);
            btnResetPaperZoom.FlatStyle = FlatStyle.Flat;
            btnResetPaperZoom.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnResetPaperZoom.ForeColor = Color.FromArgb(30, 40, 60);
            btnResetPaperZoom.Location = new Point(108, 5);
            btnResetPaperZoom.Name = "btnResetPaperZoom";
            btnResetPaperZoom.Size = new Size(75, 25);
            btnResetPaperZoom.TabIndex = 1;
            btnResetPaperZoom.Text = "全図表示";
            btnResetPaperZoom.UseVisualStyleBackColor = false;
            // 
            // lblPaperSheetTitle
            // 
            lblPaperSheetTitle.AutoSize = true;
            lblPaperSheetTitle.Font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold);
            lblPaperSheetTitle.ForeColor = Color.FromArgb(25, 45, 80);
            lblPaperSheetTitle.Location = new Point(8, 8);
            lblPaperSheetTitle.Name = "lblPaperSheetTitle";
            lblPaperSheetTitle.Size = new Size(83, 17);
            lblPaperSheetTitle.TabIndex = 0;
            lblPaperSheetTitle.Text = "📄 遺構図面";
            // 
            // picPaperCanvas
            // 
            picPaperCanvas.Location = new Point(0, 35);
            picPaperCanvas.Name = "picPaperCanvas";
            picPaperCanvas.Size = new Size(736, 415);
            picPaperCanvas.TabIndex = 1;
            picPaperCanvas.TabStop = false;
            // 
            // splitContainerTopHorizontal
            // 
            splitContainerTopHorizontal.Dock = DockStyle.Fill;
            splitContainerTopHorizontal.Location = new Point(0, 60);
            splitContainerTopHorizontal.Name = "splitContainerTopHorizontal";
            // 
            // splitContainerTopHorizontal.Panel1
            // 
            splitContainerTopHorizontal.Panel1.Controls.Add(panelTopLeft);
            // 
            // splitContainerTopHorizontal.Panel2
            // 
            splitContainerTopHorizontal.Panel2.Controls.Add(panelTopRight);
            splitContainerTopHorizontal.Size = new Size(1520, 517);
            splitContainerTopHorizontal.SplitterDistance = 760;
            splitContainerTopHorizontal.TabIndex = 1;
            // 
            // panelTopLeft
            // 
            panelTopLeft.Controls.Add(panelHcLeftSidebar);
            panelTopLeft.Controls.Add(picCropCanvas);
            panelTopLeft.Dock = DockStyle.Fill;
            panelTopLeft.Location = new Point(0, 0);
            panelTopLeft.Name = "panelTopLeft";
            panelTopLeft.Size = new Size(760, 517);
            panelTopLeft.TabIndex = 0;
            // 
            // panelTopRight
            // 
            panelTopRight.Controls.Add(picPaperCanvas);
            panelTopRight.Controls.Add(panelTopRightHeader);
            panelTopRight.Dock = DockStyle.Fill;
            panelTopRight.Location = new Point(0, 0);
            panelTopRight.Name = "panelTopRight";
            panelTopRight.Size = new Size(756, 517);
            panelTopRight.TabIndex = 0;
            // 
            // panelBottomFixedGroup
            // 
            panelBottomFixedGroup.BackColor = Color.FromArgb(245, 246, 248);
            panelBottomFixedGroup.Controls.Add(grpFeatureDetailPreview);
            panelBottomFixedGroup.Controls.Add(panelControls);
            panelBottomFixedGroup.Controls.Add(panelBottomGrids);
            panelBottomFixedGroup.Dock = DockStyle.Bottom;
            panelBottomFixedGroup.Location = new Point(0, 577);
            panelBottomFixedGroup.Name = "panelBottomFixedGroup";
            panelBottomFixedGroup.Size = new Size(1520, 361);
            panelBottomFixedGroup.TabIndex = 3;
            // 
            // grpFeatureDetailPreview
            // 
            grpFeatureDetailPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpFeatureDetailPreview.BackColor = Color.FromArgb(245, 246, 248);
            grpFeatureDetailPreview.Controls.Add(picFeatureDetailCanvas);
            grpFeatureDetailPreview.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            grpFeatureDetailPreview.ForeColor = Color.FromArgb(25, 55, 105);
            grpFeatureDetailPreview.Location = new Point(916, 0);
            grpFeatureDetailPreview.Name = "grpFeatureDetailPreview";
            grpFeatureDetailPreview.Size = new Size(604, 361);
            grpFeatureDetailPreview.TabIndex = 2;
            grpFeatureDetailPreview.TabStop = false;
            grpFeatureDetailPreview.Text = "選択遺構 詳細プレビュー";
            // 
            // picFeatureDetailCanvas
            // 
            picFeatureDetailCanvas.BackColor = Color.FromArgb(250, 250, 250);
            picFeatureDetailCanvas.Dock = DockStyle.Fill;
            picFeatureDetailCanvas.Location = new Point(3, 19);
            picFeatureDetailCanvas.Name = "picFeatureDetailCanvas";
            picFeatureDetailCanvas.Size = new Size(598, 339);
            picFeatureDetailCanvas.TabIndex = 0;
            picFeatureDetailCanvas.TabStop = false;
            // 
            // panelControls
            // 
            panelControls.AutoScroll = true;
            panelControls.BackColor = Color.FromArgb(245, 246, 248);
            panelControls.Controls.Add(grpIkouProps);
            panelControls.Controls.Add(grpDrawingProps);
            panelControls.Dock = DockStyle.Left;
            panelControls.Location = new Point(585, 0);
            panelControls.Name = "panelControls";
            panelControls.Padding = new Padding(3);
            panelControls.Size = new Size(325, 361);
            panelControls.TabIndex = 1;
            // 
            // grpIkouProps
            // 
            grpIkouProps.BackColor = Color.FromArgb(245, 246, 248);
            grpIkouProps.Controls.Add(grpDanmenProps);
            grpIkouProps.Controls.Add(grpCompassProps);
            grpIkouProps.Controls.Add(btnSetPaperPosition);
            grpIkouProps.Controls.Add(btnPickCropBounds);
            grpIkouProps.Controls.Add(cmbFeatureSelect);
            grpIkouProps.Controls.Add(lblTargetIkou);
            grpIkouProps.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            grpIkouProps.ForeColor = Color.FromArgb(25, 55, 105);
            grpIkouProps.Location = new Point(3, 86);
            grpIkouProps.Name = "grpIkouProps";
            grpIkouProps.Size = new Size(309, 260);
            grpIkouProps.TabIndex = 1;
            grpIkouProps.TabStop = false;
            grpIkouProps.Text = "遺構";
            // 
            // grpDanmenProps
            // 
            grpDanmenProps.BackColor = Color.FromArgb(245, 246, 248);
            grpDanmenProps.Controls.Add(btnSetDanmenPosition);
            grpDanmenProps.Controls.Add(txtDanmenName);
            grpDanmenProps.Controls.Add(lblDanmenName);
            grpDanmenProps.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            grpDanmenProps.ForeColor = Color.FromArgb(25, 55, 105);
            grpDanmenProps.Location = new Point(10, 168);
            grpDanmenProps.Name = "grpDanmenProps";
            grpDanmenProps.Size = new Size(292, 82);
            grpDanmenProps.TabIndex = 6;
            grpDanmenProps.TabStop = false;
            grpDanmenProps.Text = "断面 (複数定義可能)";
            // 
            // btnSetDanmenPosition
            // 
            btnSetDanmenPosition.BackColor = Color.FromArgb(225, 232, 242);
            btnSetDanmenPosition.FlatStyle = FlatStyle.Flat;
            btnSetDanmenPosition.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnSetDanmenPosition.ForeColor = Color.FromArgb(25, 45, 80);
            btnSetDanmenPosition.Location = new Point(150, 19);
            btnSetDanmenPosition.Name = "btnSetDanmenPosition";
            btnSetDanmenPosition.Size = new Size(100, 26);
            btnSetDanmenPosition.TabIndex = 2;
            btnSetDanmenPosition.Text = "断面指定";
            btnSetDanmenPosition.UseVisualStyleBackColor = false;
            // 
            // txtDanmenName
            // 
            txtDanmenName.BackColor = Color.FromArgb(255, 255, 191);
            txtDanmenName.ForeColor = Color.Black;
            txtDanmenName.Location = new Point(62, 21);
            txtDanmenName.Name = "txtDanmenName";
            txtDanmenName.Size = new Size(80, 23);
            txtDanmenName.TabIndex = 1;
            // 
            // lblDanmenName
            // 
            lblDanmenName.AutoSize = true;
            lblDanmenName.Font = new Font("Yu Gothic UI", 9F);
            lblDanmenName.ForeColor = Color.FromArgb(33, 37, 41);
            lblDanmenName.Location = new Point(10, 24);
            lblDanmenName.Name = "lblDanmenName";
            lblDanmenName.Size = new Size(43, 15);
            lblDanmenName.TabIndex = 0;
            lblDanmenName.Text = "断面名";
            // 
            // grpCompassProps
            // 
            grpCompassProps.BackColor = Color.FromArgb(245, 246, 248);
            grpCompassProps.Controls.Add(btnSetDirectionPosition);
            grpCompassProps.Controls.Add(chkShowDirection);
            grpCompassProps.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            grpCompassProps.ForeColor = Color.FromArgb(25, 55, 105);
            grpCompassProps.Location = new Point(10, 90);
            grpCompassProps.Name = "grpCompassProps";
            grpCompassProps.Size = new Size(292, 65);
            grpCompassProps.TabIndex = 5;
            grpCompassProps.TabStop = false;
            grpCompassProps.Text = "方位マーク";
            // 
            // btnSetDirectionPosition
            // 
            btnSetDirectionPosition.BackColor = Color.FromArgb(225, 232, 242);
            btnSetDirectionPosition.FlatStyle = FlatStyle.Flat;
            btnSetDirectionPosition.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnSetDirectionPosition.ForeColor = Color.FromArgb(25, 45, 80);
            btnSetDirectionPosition.Location = new Point(70, 20);
            btnSetDirectionPosition.Name = "btnSetDirectionPosition";
            btnSetDirectionPosition.Size = new Size(120, 25);
            btnSetDirectionPosition.TabIndex = 1;
            btnSetDirectionPosition.Text = "表示位置指定";
            btnSetDirectionPosition.UseVisualStyleBackColor = false;
            // 
            // chkShowDirection
            // 
            chkShowDirection.AutoSize = true;
            chkShowDirection.Checked = true;
            chkShowDirection.CheckState = CheckState.Checked;
            chkShowDirection.Font = new Font("Yu Gothic UI", 9F);
            chkShowDirection.ForeColor = Color.FromArgb(33, 37, 41);
            chkShowDirection.Location = new Point(10, 24);
            chkShowDirection.Name = "chkShowDirection";
            chkShowDirection.Size = new Size(50, 19);
            chkShowDirection.TabIndex = 0;
            chkShowDirection.Text = "表示";
            chkShowDirection.UseVisualStyleBackColor = true;
            // 
            // btnSetPaperPosition
            // 
            btnSetPaperPosition.BackColor = Color.FromArgb(225, 232, 242);
            btnSetPaperPosition.FlatStyle = FlatStyle.Flat;
            btnSetPaperPosition.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnSetPaperPosition.ForeColor = Color.FromArgb(25, 45, 80);
            btnSetPaperPosition.Location = new Point(171, 48);
            btnSetPaperPosition.Name = "btnSetPaperPosition";
            btnSetPaperPosition.Size = new Size(109, 26);
            btnSetPaperPosition.TabIndex = 4;
            btnSetPaperPosition.Text = "表示位置指定";
            btnSetPaperPosition.UseVisualStyleBackColor = false;
            // 
            // btnPickCropBounds
            // 
            btnPickCropBounds.BackColor = Color.FromArgb(225, 232, 242);
            btnPickCropBounds.FlatStyle = FlatStyle.Flat;
            btnPickCropBounds.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnPickCropBounds.ForeColor = Color.FromArgb(25, 45, 80);
            btnPickCropBounds.Location = new Point(46, 48);
            btnPickCropBounds.Name = "btnPickCropBounds";
            btnPickCropBounds.Size = new Size(115, 26);
            btnPickCropBounds.TabIndex = 3;
            btnPickCropBounds.Text = "遺構枠指示 (3点)";
            btnPickCropBounds.UseVisualStyleBackColor = false;
            // 
            // cmbFeatureSelect
            // 
            cmbFeatureSelect.BackColor = Color.FromArgb(255, 255, 191);
            cmbFeatureSelect.ForeColor = Color.Black;
            cmbFeatureSelect.Location = new Point(62, 19);
            cmbFeatureSelect.Name = "cmbFeatureSelect";
            cmbFeatureSelect.Size = new Size(155, 23);
            cmbFeatureSelect.TabIndex = 1;
            // 
            // lblTargetIkou
            // 
            lblTargetIkou.AutoSize = true;
            lblTargetIkou.Font = new Font("Yu Gothic UI", 9F);
            lblTargetIkou.ForeColor = Color.FromArgb(33, 37, 41);
            lblTargetIkou.Location = new Point(10, 22);
            lblTargetIkou.Name = "lblTargetIkou";
            lblTargetIkou.Size = new Size(43, 15);
            lblTargetIkou.TabIndex = 0;
            lblTargetIkou.Text = "遺構名";
            // 
            // grpDrawingProps
            // 
            grpDrawingProps.BackColor = Color.FromArgb(245, 246, 248);
            grpDrawingProps.Controls.Add(cmbOrientation);
            grpDrawingProps.Controls.Add(cmbScale);
            grpDrawingProps.Controls.Add(lblScale);
            grpDrawingProps.Controls.Add(cmbPaperSize);
            grpDrawingProps.Controls.Add(lblPaperSize);
            grpDrawingProps.Controls.Add(txtDrawingName);
            grpDrawingProps.Controls.Add(lblDrawingName);
            grpDrawingProps.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            grpDrawingProps.ForeColor = Color.FromArgb(25, 55, 105);
            grpDrawingProps.Location = new Point(3, 3);
            grpDrawingProps.Name = "grpDrawingProps";
            grpDrawingProps.Size = new Size(302, 80);
            grpDrawingProps.TabIndex = 0;
            grpDrawingProps.TabStop = false;
            grpDrawingProps.Text = "図面";
            // 
            // cmbScale
            // 
            cmbScale.BackColor = Color.White;
            cmbScale.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScale.ForeColor = Color.FromArgb(33, 37, 41);
            cmbScale.Location = new Point(188, 19);
            cmbScale.Name = "cmbScale";
            cmbScale.Size = new Size(55, 23);
            cmbScale.TabIndex = 5;
            // 
            // lblScale
            // 
            lblScale.AutoSize = true;
            lblScale.Font = new Font("Yu Gothic UI", 9F);
            lblScale.ForeColor = Color.FromArgb(33, 37, 41);
            lblScale.Location = new Point(141, 22);
            lblScale.Name = "lblScale";
            lblScale.Size = new Size(45, 15);
            lblScale.TabIndex = 4;
            lblScale.Text = "縮尺 1/";
            // 
            // cmbPaperSize
            // 
            cmbPaperSize.BackColor = Color.White;
            cmbPaperSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaperSize.ForeColor = Color.FromArgb(33, 37, 41);
            cmbPaperSize.Location = new Point(55, 47);
            cmbPaperSize.Name = "cmbPaperSize";
            cmbPaperSize.Size = new Size(129, 23);
            cmbPaperSize.TabIndex = 3;
            // 
            // lblPaperSize
            // 
            lblPaperSize.AutoSize = true;
            lblPaperSize.Font = new Font("Yu Gothic UI", 9F);
            lblPaperSize.ForeColor = Color.FromArgb(33, 37, 41);
            lblPaperSize.Location = new Point(20, 50);
            lblPaperSize.Name = "lblPaperSize";
            lblPaperSize.Size = new Size(31, 15);
            lblPaperSize.TabIndex = 2;
            lblPaperSize.Text = "用紙";
            // 
            // txtDrawingName
            // 
            txtDrawingName.BackColor = Color.FromArgb(255, 255, 191);
            txtDrawingName.ForeColor = Color.Black;
            txtDrawingName.Location = new Point(55, 19);
            txtDrawingName.Name = "txtDrawingName";
            txtDrawingName.Size = new Size(80, 23);
            txtDrawingName.TabIndex = 1;
            // 
            // lblDrawingName
            // 
            lblDrawingName.AutoSize = true;
            lblDrawingName.Font = new Font("Yu Gothic UI", 9F);
            lblDrawingName.ForeColor = Color.FromArgb(33, 37, 41);
            lblDrawingName.Location = new Point(8, 22);
            lblDrawingName.Name = "lblDrawingName";
            lblDrawingName.Size = new Size(43, 15);
            lblDrawingName.TabIndex = 0;
            lblDrawingName.Text = "図面名";
            // 
            // panelBottomGrids
            // 
            panelBottomGrids.BackColor = Color.FromArgb(245, 246, 248);
            panelBottomGrids.ColumnCount = 3;
            panelBottomGrids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            panelBottomGrids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            panelBottomGrids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            panelBottomGrids.Controls.Add(grpDrawings, 0, 0);
            panelBottomGrids.Controls.Add(grpDrawingIkous, 1, 0);
            panelBottomGrids.Controls.Add(grpDanmenList, 2, 0);
            panelBottomGrids.Dock = DockStyle.Left;
            panelBottomGrids.Location = new Point(0, 0);
            panelBottomGrids.Name = "panelBottomGrids";
            panelBottomGrids.RowCount = 1;
            panelBottomGrids.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelBottomGrids.Size = new Size(585, 361);
            panelBottomGrids.TabIndex = 0;
            // 
            // grpDrawings
            // 
            grpDrawings.BackColor = Color.FromArgb(245, 246, 248);
            grpDrawings.Controls.Add(dgvDrawings);
            grpDrawings.Controls.Add(btnDeleteDrawing);
            grpDrawings.Controls.Add(btnUpdateDrawingProps);
            grpDrawings.Controls.Add(btnAddDrawing);
            grpDrawings.Dock = DockStyle.Fill;
            grpDrawings.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            grpDrawings.ForeColor = Color.FromArgb(25, 55, 105);
            grpDrawings.Location = new Point(3, 3);
            grpDrawings.Name = "grpDrawings";
            grpDrawings.Size = new Size(193, 355);
            grpDrawings.TabIndex = 0;
            grpDrawings.TabStop = false;
            grpDrawings.Text = "図面名称";
            // 
            // dgvDrawings
            // 
            dgvDrawings.AllowUserToAddRows = false;
            dgvDrawings.AllowUserToDeleteRows = false;
            dgvDrawings.Dock = DockStyle.Top;
            dgvDrawings.Location = new Point(3, 19);
            dgvDrawings.Name = "dgvDrawings";
            dgvDrawings.Size = new Size(187, 290);
            dgvDrawings.TabIndex = 0;
            // 
            // btnDeleteDrawing
            // 
            btnDeleteDrawing.BackColor = Color.FromArgb(195, 55, 55);
            btnDeleteDrawing.FlatStyle = FlatStyle.Flat;
            btnDeleteDrawing.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnDeleteDrawing.ForeColor = Color.White;
            btnDeleteDrawing.Location = new Point(8, 320);
            btnDeleteDrawing.Name = "btnDeleteDrawing";
            btnDeleteDrawing.Size = new Size(54, 26);
            btnDeleteDrawing.TabIndex = 1;
            btnDeleteDrawing.Text = "削除";
            btnDeleteDrawing.UseVisualStyleBackColor = false;
            // 
            // btnUpdateDrawingProps
            // 
            btnUpdateDrawingProps.BackColor = Color.FromArgb(38, 145, 75);
            btnUpdateDrawingProps.FlatStyle = FlatStyle.Flat;
            btnUpdateDrawingProps.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnUpdateDrawingProps.ForeColor = Color.White;
            btnUpdateDrawingProps.Location = new Point(66, 320);
            btnUpdateDrawingProps.Name = "btnUpdateDrawingProps";
            btnUpdateDrawingProps.Size = new Size(54, 26);
            btnUpdateDrawingProps.TabIndex = 2;
            btnUpdateDrawingProps.Text = "更新";
            btnUpdateDrawingProps.UseVisualStyleBackColor = false;
            // 
            // btnAddDrawing
            // 
            btnAddDrawing.BackColor = Color.FromArgb(30, 115, 210);
            btnAddDrawing.FlatStyle = FlatStyle.Flat;
            btnAddDrawing.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnAddDrawing.ForeColor = Color.White;
            btnAddDrawing.Location = new Point(124, 320);
            btnAddDrawing.Name = "btnAddDrawing";
            btnAddDrawing.Size = new Size(54, 26);
            btnAddDrawing.TabIndex = 3;
            btnAddDrawing.Text = "追加";
            btnAddDrawing.UseVisualStyleBackColor = false;
            // 
            // grpDrawingIkous
            // 
            grpDrawingIkous.BackColor = Color.FromArgb(245, 246, 248);
            grpDrawingIkous.Controls.Add(dgvDrawingIkous);
            grpDrawingIkous.Controls.Add(btnDeleteDrawingIkou);
            grpDrawingIkous.Controls.Add(btnUpdateIkouProps);
            grpDrawingIkous.Controls.Add(btnAddDrawingIkou);
            grpDrawingIkous.Dock = DockStyle.Fill;
            grpDrawingIkous.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            grpDrawingIkous.ForeColor = Color.FromArgb(25, 55, 105);
            grpDrawingIkous.Location = new Point(202, 3);
            grpDrawingIkous.Name = "grpDrawingIkous";
            grpDrawingIkous.Size = new Size(187, 355);
            grpDrawingIkous.TabIndex = 1;
            grpDrawingIkous.TabStop = false;
            grpDrawingIkous.Text = "対象遺構図";
            // 
            // dgvDrawingIkous
            // 
            dgvDrawingIkous.AllowUserToAddRows = false;
            dgvDrawingIkous.AllowUserToDeleteRows = false;
            dgvDrawingIkous.Dock = DockStyle.Top;
            dgvDrawingIkous.Location = new Point(3, 19);
            dgvDrawingIkous.Name = "dgvDrawingIkous";
            dgvDrawingIkous.Size = new Size(181, 290);
            dgvDrawingIkous.TabIndex = 0;
            // 
            // btnDeleteDrawingIkou
            // 
            btnDeleteDrawingIkou.BackColor = Color.FromArgb(195, 55, 55);
            btnDeleteDrawingIkou.FlatStyle = FlatStyle.Flat;
            btnDeleteDrawingIkou.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnDeleteDrawingIkou.ForeColor = Color.White;
            btnDeleteDrawingIkou.Location = new Point(8, 320);
            btnDeleteDrawingIkou.Name = "btnDeleteDrawingIkou";
            btnDeleteDrawingIkou.Size = new Size(54, 26);
            btnDeleteDrawingIkou.TabIndex = 1;
            btnDeleteDrawingIkou.Text = "削除";
            btnDeleteDrawingIkou.UseVisualStyleBackColor = false;
            // 
            // btnUpdateIkouProps
            // 
            btnUpdateIkouProps.BackColor = Color.FromArgb(38, 145, 75);
            btnUpdateIkouProps.FlatStyle = FlatStyle.Flat;
            btnUpdateIkouProps.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnUpdateIkouProps.ForeColor = Color.White;
            btnUpdateIkouProps.Location = new Point(66, 320);
            btnUpdateIkouProps.Name = "btnUpdateIkouProps";
            btnUpdateIkouProps.Size = new Size(54, 26);
            btnUpdateIkouProps.TabIndex = 2;
            btnUpdateIkouProps.Text = "更新";
            btnUpdateIkouProps.UseVisualStyleBackColor = false;
            // 
            // btnAddDrawingIkou
            // 
            btnAddDrawingIkou.BackColor = Color.FromArgb(30, 115, 210);
            btnAddDrawingIkou.FlatStyle = FlatStyle.Flat;
            btnAddDrawingIkou.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnAddDrawingIkou.ForeColor = Color.White;
            btnAddDrawingIkou.Location = new Point(124, 320);
            btnAddDrawingIkou.Name = "btnAddDrawingIkou";
            btnAddDrawingIkou.Size = new Size(54, 26);
            btnAddDrawingIkou.TabIndex = 3;
            btnAddDrawingIkou.Text = "追加";
            btnAddDrawingIkou.UseVisualStyleBackColor = false;
            // 
            // grpDanmenList
            // 
            grpDanmenList.BackColor = Color.FromArgb(245, 246, 248);
            grpDanmenList.Controls.Add(dgvDanmen);
            grpDanmenList.Controls.Add(btnDeleteDanmen);
            grpDanmenList.Controls.Add(btnUpdateDanmenName);
            grpDanmenList.Controls.Add(btnAddDanmen);
            grpDanmenList.Dock = DockStyle.Fill;
            grpDanmenList.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            grpDanmenList.ForeColor = Color.FromArgb(25, 55, 105);
            grpDanmenList.Location = new Point(395, 3);
            grpDanmenList.Name = "grpDanmenList";
            grpDanmenList.Size = new Size(187, 355);
            grpDanmenList.TabIndex = 2;
            grpDanmenList.TabStop = false;
            grpDanmenList.Text = "断面名称";
            // 
            // dgvDanmen
            // 
            dgvDanmen.AllowUserToAddRows = false;
            dgvDanmen.AllowUserToDeleteRows = false;
            dgvDanmen.Dock = DockStyle.Top;
            dgvDanmen.Location = new Point(3, 19);
            dgvDanmen.Name = "dgvDanmen";
            dgvDanmen.Size = new Size(181, 290);
            dgvDanmen.TabIndex = 0;
            // 
            // btnDeleteDanmen
            // 
            btnDeleteDanmen.BackColor = Color.FromArgb(195, 55, 55);
            btnDeleteDanmen.FlatStyle = FlatStyle.Flat;
            btnDeleteDanmen.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnDeleteDanmen.ForeColor = Color.White;
            btnDeleteDanmen.Location = new Point(8, 320);
            btnDeleteDanmen.Name = "btnDeleteDanmen";
            btnDeleteDanmen.Size = new Size(54, 26);
            btnDeleteDanmen.TabIndex = 1;
            btnDeleteDanmen.Text = "削除";
            btnDeleteDanmen.UseVisualStyleBackColor = false;
            // 
            // btnUpdateDanmenName
            // 
            btnUpdateDanmenName.BackColor = Color.FromArgb(38, 145, 75);
            btnUpdateDanmenName.FlatStyle = FlatStyle.Flat;
            btnUpdateDanmenName.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnUpdateDanmenName.ForeColor = Color.White;
            btnUpdateDanmenName.Location = new Point(66, 320);
            btnUpdateDanmenName.Name = "btnUpdateDanmenName";
            btnUpdateDanmenName.Size = new Size(54, 26);
            btnUpdateDanmenName.TabIndex = 2;
            btnUpdateDanmenName.Text = "更新";
            btnUpdateDanmenName.UseVisualStyleBackColor = false;
            // 
            // btnAddDanmen
            // 
            btnAddDanmen.BackColor = Color.FromArgb(30, 115, 210);
            btnAddDanmen.FlatStyle = FlatStyle.Flat;
            btnAddDanmen.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            btnAddDanmen.ForeColor = Color.White;
            btnAddDanmen.Location = new Point(124, 320);
            btnAddDanmen.Name = "btnAddDanmen";
            btnAddDanmen.Size = new Size(54, 26);
            btnAddDanmen.TabIndex = 3;
            btnAddDanmen.Text = "追加";
            btnAddDanmen.UseVisualStyleBackColor = false;
            // 
            // statusStripBar
            // 
            statusStripBar.BackColor = Color.FromArgb(233, 236, 243);
            statusStripBar.Items.AddRange(new ToolStripItem[] { lblStatusCoords, lblStatusMessage });
            statusStripBar.Location = new Point(0, 938);
            statusStripBar.Name = "statusStripBar";
            statusStripBar.Size = new Size(1520, 22);
            statusStripBar.TabIndex = 2;
            // 
            // lblStatusCoords
            // 
            lblStatusCoords.ForeColor = Color.FromArgb(0, 102, 204);
            lblStatusCoords.Name = "lblStatusCoords";
            lblStatusCoords.Size = new Size(128, 17);
            lblStatusCoords.Text = "(-60262.447, 85099.983)";
            // 
            // lblStatusMessage
            // 
            lblStatusMessage.ForeColor = Color.FromArgb(50, 60, 75);
            lblStatusMessage.Name = "lblStatusMessage";
            lblStatusMessage.Size = new Size(1377, 17);
            lblStatusMessage.Spring = true;
            lblStatusMessage.Text = "準備完了";
            lblStatusMessage.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormDrawingEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 248);
            ClientSize = new Size(1520, 960);
            Controls.Add(splitContainerTopHorizontal);
            Controls.Add(panelBottomFixedGroup);
            Controls.Add(panelHeader);
            Controls.Add(statusStripBar);
            Font = new Font("Yu Gothic UI", 9F);
            MinimumSize = new Size(1280, 780);
            Name = "FormDrawingEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "遺跡調査システム(内楽) - 遺構図面作成エディタ";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelHcLeftSidebar.ResumeLayout(false);
            panelHcLeftSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCropCanvas).EndInit();
            panelTopRightHeader.ResumeLayout(false);
            panelTopRightHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPaperCanvas).EndInit();
            splitContainerTopHorizontal.Panel1.ResumeLayout(false);
            splitContainerTopHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerTopHorizontal).EndInit();
            splitContainerTopHorizontal.ResumeLayout(false);
            panelTopLeft.ResumeLayout(false);
            panelTopRight.ResumeLayout(false);
            panelBottomFixedGroup.ResumeLayout(false);
            grpFeatureDetailPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picFeatureDetailCanvas).EndInit();
            panelControls.ResumeLayout(false);
            grpIkouProps.ResumeLayout(false);
            grpIkouProps.PerformLayout();
            grpDanmenProps.ResumeLayout(false);
            grpDanmenProps.PerformLayout();
            grpCompassProps.ResumeLayout(false);
            grpCompassProps.PerformLayout();
            grpDrawingProps.ResumeLayout(false);
            grpDrawingProps.PerformLayout();
            panelBottomGrids.ResumeLayout(false);
            grpDrawings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDrawings).EndInit();
            grpDrawingIkous.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDrawingIkous).EndInit();
            grpDanmenList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDanmen).EndInit();
            statusStripBar.ResumeLayout(false);
            statusStripBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.Button btnSaveDb;
        private System.Windows.Forms.Label lblDbStatus;
        private System.Windows.Forms.Button btnExit;

        private System.Windows.Forms.SplitContainer splitContainerTopHorizontal;
        private System.Windows.Forms.Panel panelTopLeft;
        private System.Windows.Forms.Panel panelTopRight;

        private System.Windows.Forms.Button btnResetCropZoom;
        private System.Windows.Forms.CheckBox chkAutoZoomIkou;
        private System.Windows.Forms.Panel panelHcLeftSidebar;
        private System.Windows.Forms.Button btnEnvSettings;
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
        private System.Windows.Forms.CheckBox chkShowWhiteBackground;
        private System.Windows.Forms.CheckBox chkShowIkouFull;
        private System.Windows.Forms.CheckBox chkShowIbutuFull;
        private System.Windows.Forms.CheckBox chkShowKikaiFull;
        private System.Windows.Forms.CheckBox chkShowCurveFull;
        private System.Windows.Forms.CheckBox chkShowGridFull;
        private System.Windows.Forms.PictureBox picCropCanvas;

        private System.Windows.Forms.Panel panelTopRightHeader;
        private System.Windows.Forms.Label lblPaperSheetTitle;
        private System.Windows.Forms.Button btnResetPaperZoom;
        private System.Windows.Forms.CheckBox chkAutoZoomPaperIkou;
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
        private System.Windows.Forms.Button btnUpdateDanmenName;
        private System.Windows.Forms.Button btnDeleteDanmen;

        private System.Windows.Forms.Panel panelControls;

        private System.Windows.Forms.GroupBox grpDrawingProps;
        private System.Windows.Forms.Label lblDrawingName;
        private System.Windows.Forms.TextBox txtDrawingName;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.ComboBox cmbOrientation;
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

        private System.Windows.Forms.StatusStrip statusStripBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCoords;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMessage;
    }
}
