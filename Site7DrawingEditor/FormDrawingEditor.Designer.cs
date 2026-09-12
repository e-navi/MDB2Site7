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
            btnDrawingFrame = new Button();
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
            btnIkouNameColorSettings = new Button();
            lblEntityNameHeader = new Label();
            chkShowKikai = new CheckBox();
            chkShowKikaiName = new CheckBox();
            chkShowIbutu = new CheckBox();
            chkShowIbutuName = new CheckBox();
            chkShowIkou = new CheckBox();
            chkShowIkouName = new CheckBox();
            chkShowDrawingFrame = new CheckBox();
            chkColorByIkouFull = new CheckBox();
            chkShowWhiteBackground = new CheckBox();
            btnEnvSettings = new Button();
            chkShowIkouFull = new CheckBox();
            chkShowIbutuFull = new CheckBox();
            chkShowKikaiFull = new CheckBox();
            chkShowCurveFull = new CheckBox();
            chkShowGridFull = new CheckBox();
            picCropCanvas = new PictureBox();
            panelTopRightHeader = new Panel();
            lblPaperInfoBanner = new Label();
            btnPrintPaper = new Button();
            chkAutoZoomPaperIkou = new CheckBox();
            btnResetPaperZoom = new Button();
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
            btnRecalcIkouDrawing = new Button();
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
            panelMiddle = new Panel();
            panelRight = new Panel();
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
            panelMiddle.SuspendLayout();
            panelRight.SuspendLayout();
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
            btnExit.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
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
            lblDbStatus.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblDbStatus.ForeColor = Color.FromArgb(255, 193, 7);
            lblDbStatus.Location = new Point(550, 15);
            lblDbStatus.Name = "lblDbStatus";
            lblDbStatus.Size = new Size(800, 30);
            lblDbStatus.TabIndex = 3;
            lblDbStatus.Text = "DB未読み込み";
            lblDbStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnSaveDb
            // 
            btnSaveDb.BackColor = Color.FromArgb(40, 167, 69);
            btnSaveDb.FlatStyle = FlatStyle.Flat;
            btnSaveDb.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnSaveDb.ForeColor = Color.White;
            btnSaveDb.Location = new Point(337, 13);
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
            lblSubHeader.Size = new Size(244, 15);
            lblSubHeader.TabIndex = 1;
            lblSubHeader.Text = "断面を含めた個別遺構図を用紙にレイアウトします";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Yu Gothic UI", 14F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(12, 8);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(309, 25);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "📐 SITE7 遺跡調査・個別遺構図作成";
            // 
            // cmbOrientation
            // 
            cmbOrientation.BackColor = Color.White;
            cmbOrientation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrientation.Font = new Font("Yu Gothic UI", 12F);
            cmbOrientation.ForeColor = Color.FromArgb(33, 37, 41);
            cmbOrientation.Location = new Point(238, 56);
            cmbOrientation.Name = "cmbOrientation";
            cmbOrientation.Size = new Size(65, 29);
            cmbOrientation.TabIndex = 7;
            // 
            // btnResetCropZoom
            // 
            btnResetCropZoom.BackColor = Color.White;
            btnResetCropZoom.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnResetCropZoom.ForeColor = Color.FromArgb(20, 20, 20);
            btnResetCropZoom.Location = new Point(2, 4);
            btnResetCropZoom.Name = "btnResetCropZoom";
            btnResetCropZoom.Size = new Size(126, 30);
            btnResetCropZoom.TabIndex = 0;
            btnResetCropZoom.Text = "🔍 全図表示";
            btnResetCropZoom.UseVisualStyleBackColor = true;
            // 
            // chkAutoZoomIkou
            // 
            chkAutoZoomIkou.AutoSize = true;
            chkAutoZoomIkou.Checked = true;
            chkAutoZoomIkou.CheckState = CheckState.Checked;
            chkAutoZoomIkou.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkAutoZoomIkou.ForeColor = Color.FromArgb(30, 40, 60);
            chkAutoZoomIkou.Location = new Point(5, 36);
            chkAutoZoomIkou.Name = "chkAutoZoomIkou";
            chkAutoZoomIkou.Size = new Size(125, 25);
            chkAutoZoomIkou.TabIndex = 1;
            chkAutoZoomIkou.Text = "選択遺構拡大";
            chkAutoZoomIkou.UseVisualStyleBackColor = true;
            // 
            // panelHcLeftSidebar
            // 
            panelHcLeftSidebar.BackColor = Color.FromArgb(240, 242, 245);
            panelHcLeftSidebar.Controls.Add(btnResetCropZoom);
            panelHcLeftSidebar.Controls.Add(chkAutoZoomIkou);
            panelHcLeftSidebar.Controls.Add(btnDrawingFrame);
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
            panelHcLeftSidebar.Controls.Add(btnIkouNameColorSettings);
            panelHcLeftSidebar.Controls.Add(lblEntityNameHeader);
            panelHcLeftSidebar.Controls.Add(chkShowKikai);
            panelHcLeftSidebar.Controls.Add(chkShowKikaiName);
            panelHcLeftSidebar.Controls.Add(chkShowIbutu);
            panelHcLeftSidebar.Controls.Add(chkShowIbutuName);
            panelHcLeftSidebar.Controls.Add(chkShowIkou);
            panelHcLeftSidebar.Controls.Add(chkShowIkouName);
            panelHcLeftSidebar.Controls.Add(chkShowDrawingFrame);
            panelHcLeftSidebar.Controls.Add(chkColorByIkouFull);
            panelHcLeftSidebar.Controls.Add(chkShowWhiteBackground);
            panelHcLeftSidebar.Dock = DockStyle.Left;
            panelHcLeftSidebar.Location = new Point(0, 0);
            panelHcLeftSidebar.Name = "panelHcLeftSidebar";
            panelHcLeftSidebar.Size = new Size(130, 878);
            panelHcLeftSidebar.TabIndex = 1;
            // 
            // btnDrawingFrame
            // 
            btnDrawingFrame.BackColor = Color.White;
            btnDrawingFrame.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnDrawingFrame.ForeColor = Color.FromArgb(20, 20, 20);
            btnDrawingFrame.Location = new Point(2, 64);
            btnDrawingFrame.Name = "btnDrawingFrame";
            btnDrawingFrame.Size = new Size(126, 30);
            btnDrawingFrame.TabIndex = 2;
            btnDrawingFrame.Text = "図枠・印刷";
            btnDrawingFrame.UseVisualStyleBackColor = true;
            // 
            // lblIkouLayerGrpHeader
            // 
            lblIkouLayerGrpHeader.AutoSize = true;
            lblIkouLayerGrpHeader.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblIkouLayerGrpHeader.ForeColor = Color.Red;
            lblIkouLayerGrpHeader.Location = new Point(5, 98);
            lblIkouLayerGrpHeader.Name = "lblIkouLayerGrpHeader";
            lblIkouLayerGrpHeader.Size = new Size(79, 21);
            lblIkouLayerGrpHeader.TabIndex = 3;
            lblIkouLayerGrpHeader.Text = "遺構レイヤ";
            // 
            // chkLayer01
            // 
            chkLayer01.AutoSize = true;
            chkLayer01.Checked = true;
            chkLayer01.CheckState = CheckState.Checked;
            chkLayer01.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer01.ForeColor = Color.Black;
            chkLayer01.Location = new Point(5, 120);
            chkLayer01.Name = "chkLayer01";
            chkLayer01.Size = new Size(52, 25);
            chkLayer01.TabIndex = 5;
            chkLayer01.Text = "L01";
            chkLayer01.UseVisualStyleBackColor = true;
            // 
            // chkLayer02
            // 
            chkLayer02.AutoSize = true;
            chkLayer02.Checked = true;
            chkLayer02.CheckState = CheckState.Checked;
            chkLayer02.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer02.ForeColor = Color.Red;
            chkLayer02.Location = new Point(5, 140);
            chkLayer02.Name = "chkLayer02";
            chkLayer02.Size = new Size(55, 25);
            chkLayer02.TabIndex = 6;
            chkLayer02.Text = "L02";
            chkLayer02.UseVisualStyleBackColor = true;
            // 
            // chkLayer03
            // 
            chkLayer03.AutoSize = true;
            chkLayer03.Checked = true;
            chkLayer03.CheckState = CheckState.Checked;
            chkLayer03.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer03.ForeColor = Color.FromArgb(0, 180, 0);
            chkLayer03.Location = new Point(5, 160);
            chkLayer03.Name = "chkLayer03";
            chkLayer03.Size = new Size(55, 25);
            chkLayer03.TabIndex = 7;
            chkLayer03.Text = "L03";
            chkLayer03.UseVisualStyleBackColor = true;
            // 
            // chkLayer04
            // 
            chkLayer04.AutoSize = true;
            chkLayer04.Checked = true;
            chkLayer04.CheckState = CheckState.Checked;
            chkLayer04.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer04.ForeColor = Color.Blue;
            chkLayer04.Location = new Point(5, 180);
            chkLayer04.Name = "chkLayer04";
            chkLayer04.Size = new Size(55, 25);
            chkLayer04.TabIndex = 8;
            chkLayer04.Text = "L04";
            chkLayer04.UseVisualStyleBackColor = true;
            // 
            // chkLayer05
            // 
            chkLayer05.AutoSize = true;
            chkLayer05.Checked = true;
            chkLayer05.CheckState = CheckState.Checked;
            chkLayer05.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer05.ForeColor = Color.FromArgb(200, 180, 0);
            chkLayer05.Location = new Point(5, 200);
            chkLayer05.Name = "chkLayer05";
            chkLayer05.Size = new Size(55, 25);
            chkLayer05.TabIndex = 9;
            chkLayer05.Text = "L05";
            chkLayer05.UseVisualStyleBackColor = true;
            // 
            // chkLayer06
            // 
            chkLayer06.AutoSize = true;
            chkLayer06.Checked = true;
            chkLayer06.CheckState = CheckState.Checked;
            chkLayer06.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer06.ForeColor = Color.Magenta;
            chkLayer06.Location = new Point(5, 220);
            chkLayer06.Name = "chkLayer06";
            chkLayer06.Size = new Size(55, 25);
            chkLayer06.TabIndex = 10;
            chkLayer06.Text = "L06";
            chkLayer06.UseVisualStyleBackColor = true;
            // 
            // chkLayer07
            // 
            chkLayer07.AutoSize = true;
            chkLayer07.Checked = true;
            chkLayer07.CheckState = CheckState.Checked;
            chkLayer07.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer07.ForeColor = Color.DeepSkyBlue;
            chkLayer07.Location = new Point(5, 240);
            chkLayer07.Name = "chkLayer07";
            chkLayer07.Size = new Size(55, 25);
            chkLayer07.TabIndex = 11;
            chkLayer07.Text = "L07";
            chkLayer07.UseVisualStyleBackColor = true;
            // 
            // chkLayer08
            // 
            chkLayer08.AutoSize = true;
            chkLayer08.Checked = true;
            chkLayer08.CheckState = CheckState.Checked;
            chkLayer08.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer08.ForeColor = Color.DarkGray;
            chkLayer08.Location = new Point(5, 260);
            chkLayer08.Name = "chkLayer08";
            chkLayer08.Size = new Size(55, 25);
            chkLayer08.TabIndex = 12;
            chkLayer08.Text = "L08";
            chkLayer08.UseVisualStyleBackColor = true;
            // 
            // chkLayer09
            // 
            chkLayer09.AutoSize = true;
            chkLayer09.Checked = true;
            chkLayer09.CheckState = CheckState.Checked;
            chkLayer09.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer09.ForeColor = Color.FromArgb(192, 0, 128);
            chkLayer09.Location = new Point(65, 120);
            chkLayer09.Name = "chkLayer09";
            chkLayer09.Size = new Size(55, 25);
            chkLayer09.TabIndex = 13;
            chkLayer09.Text = "L09";
            chkLayer09.UseVisualStyleBackColor = true;
            // 
            // chkLayer10
            // 
            chkLayer10.AutoSize = true;
            chkLayer10.Checked = true;
            chkLayer10.CheckState = CheckState.Checked;
            chkLayer10.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer10.ForeColor = Color.FromArgb(192, 128, 64);
            chkLayer10.Location = new Point(65, 140);
            chkLayer10.Name = "chkLayer10";
            chkLayer10.Size = new Size(52, 25);
            chkLayer10.TabIndex = 14;
            chkLayer10.Text = "L10";
            chkLayer10.UseVisualStyleBackColor = true;
            // 
            // chkLayer11
            // 
            chkLayer11.AutoSize = true;
            chkLayer11.Checked = true;
            chkLayer11.CheckState = CheckState.Checked;
            chkLayer11.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer11.ForeColor = Color.FromArgb(255, 128, 0);
            chkLayer11.Location = new Point(65, 160);
            chkLayer11.Name = "chkLayer11";
            chkLayer11.Size = new Size(49, 25);
            chkLayer11.TabIndex = 15;
            chkLayer11.Text = "L11";
            chkLayer11.UseVisualStyleBackColor = true;
            // 
            // chkLayer12
            // 
            chkLayer12.AutoSize = true;
            chkLayer12.Checked = true;
            chkLayer12.CheckState = CheckState.Checked;
            chkLayer12.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer12.ForeColor = Color.FromArgb(128, 192, 128);
            chkLayer12.Location = new Point(65, 180);
            chkLayer12.Name = "chkLayer12";
            chkLayer12.Size = new Size(52, 25);
            chkLayer12.TabIndex = 16;
            chkLayer12.Text = "L12";
            chkLayer12.UseVisualStyleBackColor = true;
            // 
            // chkLayer13
            // 
            chkLayer13.AutoSize = true;
            chkLayer13.Checked = true;
            chkLayer13.CheckState = CheckState.Checked;
            chkLayer13.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer13.ForeColor = Color.FromArgb(0, 128, 255);
            chkLayer13.Location = new Point(65, 200);
            chkLayer13.Name = "chkLayer13";
            chkLayer13.Size = new Size(52, 25);
            chkLayer13.TabIndex = 17;
            chkLayer13.Text = "L13";
            chkLayer13.UseVisualStyleBackColor = true;
            // 
            // chkLayer14
            // 
            chkLayer14.AutoSize = true;
            chkLayer14.Checked = true;
            chkLayer14.CheckState = CheckState.Checked;
            chkLayer14.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer14.ForeColor = Color.FromArgb(128, 64, 255);
            chkLayer14.Location = new Point(65, 220);
            chkLayer14.Name = "chkLayer14";
            chkLayer14.Size = new Size(52, 25);
            chkLayer14.TabIndex = 18;
            chkLayer14.Text = "L14";
            chkLayer14.UseVisualStyleBackColor = true;
            // 
            // chkLayer15
            // 
            chkLayer15.AutoSize = true;
            chkLayer15.Checked = true;
            chkLayer15.CheckState = CheckState.Checked;
            chkLayer15.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer15.ForeColor = Color.FromArgb(180, 180, 180);
            chkLayer15.Location = new Point(65, 240);
            chkLayer15.Name = "chkLayer15";
            chkLayer15.Size = new Size(52, 25);
            chkLayer15.TabIndex = 19;
            chkLayer15.Text = "L15";
            chkLayer15.UseVisualStyleBackColor = true;
            // 
            // chkLayer16
            // 
            chkLayer16.AutoSize = true;
            chkLayer16.Checked = true;
            chkLayer16.CheckState = CheckState.Checked;
            chkLayer16.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkLayer16.ForeColor = Color.FromArgb(100, 100, 100);
            chkLayer16.Location = new Point(65, 260);
            chkLayer16.Name = "chkLayer16";
            chkLayer16.Size = new Size(52, 25);
            chkLayer16.TabIndex = 20;
            chkLayer16.Text = "L16";
            chkLayer16.UseVisualStyleBackColor = true;
            // 
            // btnLayerAllOn
            // 
            btnLayerAllOn.BackColor = Color.White;
            btnLayerAllOn.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnLayerAllOn.ForeColor = Color.FromArgb(20, 20, 20);
            btnLayerAllOn.Location = new Point(2, 282);
            btnLayerAllOn.Name = "btnLayerAllOn";
            btnLayerAllOn.Size = new Size(64, 32);
            btnLayerAllOn.TabIndex = 21;
            btnLayerAllOn.Text = "全ON";
            btnLayerAllOn.UseVisualStyleBackColor = true;
            // 
            // btnLayerAllOff
            // 
            btnLayerAllOff.BackColor = Color.White;
            btnLayerAllOff.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnLayerAllOff.ForeColor = Color.FromArgb(20, 20, 20);
            btnLayerAllOff.Location = new Point(65, 282);
            btnLayerAllOff.Name = "btnLayerAllOff";
            btnLayerAllOff.Size = new Size(64, 32);
            btnLayerAllOff.TabIndex = 22;
            btnLayerAllOff.Text = "全OFF";
            btnLayerAllOff.UseVisualStyleBackColor = true;
            // 
            // btnLayerSettings
            // 
            btnLayerSettings.BackColor = Color.White;
            btnLayerSettings.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnLayerSettings.ForeColor = Color.FromArgb(20, 20, 20);
            btnLayerSettings.Location = new Point(2, 316);
            btnLayerSettings.Name = "btnLayerSettings";
            btnLayerSettings.Size = new Size(126, 30);
            btnLayerSettings.TabIndex = 23;
            btnLayerSettings.Text = "レイヤ設定";
            btnLayerSettings.UseVisualStyleBackColor = true;
            // 
            // btnIkouNameColorSettings
            // 
            btnIkouNameColorSettings.BackColor = Color.White;
            btnIkouNameColorSettings.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnIkouNameColorSettings.ForeColor = Color.FromArgb(20, 20, 20);
            btnIkouNameColorSettings.Location = new Point(2, 348);
            btnIkouNameColorSettings.Name = "btnIkouNameColorSettings";
            btnIkouNameColorSettings.Size = new Size(126, 30);
            btnIkouNameColorSettings.TabIndex = 24;
            btnIkouNameColorSettings.Text = "遺構名色設定";
            btnIkouNameColorSettings.UseVisualStyleBackColor = true;
            // 
            // lblEntityNameHeader
            // 
            lblEntityNameHeader.AutoSize = true;
            lblEntityNameHeader.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblEntityNameHeader.ForeColor = Color.Red;
            lblEntityNameHeader.Location = new Point(76, 382);
            lblEntityNameHeader.Name = "lblEntityNameHeader";
            lblEntityNameHeader.Size = new Size(42, 21);
            lblEntityNameHeader.TabIndex = 25;
            lblEntityNameHeader.Text = "名称";
            // 
            // chkShowKikai
            // 
            chkShowKikai.AutoSize = true;
            chkShowKikai.Checked = true;
            chkShowKikai.CheckState = CheckState.Checked;
            chkShowKikai.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkShowKikai.ForeColor = Color.Blue;
            chkShowKikai.Location = new Point(5, 404);
            chkShowKikai.Name = "chkShowKikai";
            chkShowKikai.Size = new Size(77, 25);
            chkShowKikai.TabIndex = 26;
            chkShowKikai.Text = "基準点";
            chkShowKikai.UseVisualStyleBackColor = true;
            // 
            // chkShowKikaiName
            // 
            chkShowKikaiName.AutoSize = true;
            chkShowKikaiName.Checked = true;
            chkShowKikaiName.CheckState = CheckState.Checked;
            chkShowKikaiName.Location = new Point(90, 409);
            chkShowKikaiName.Name = "chkShowKikaiName";
            chkShowKikaiName.Size = new Size(15, 14);
            chkShowKikaiName.TabIndex = 27;
            chkShowKikaiName.UseVisualStyleBackColor = true;
            // 
            // chkShowIbutu
            // 
            chkShowIbutu.AutoSize = true;
            chkShowIbutu.Checked = true;
            chkShowIbutu.CheckState = CheckState.Checked;
            chkShowIbutu.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkShowIbutu.ForeColor = Color.Red;
            chkShowIbutu.Location = new Point(5, 428);
            chkShowIbutu.Name = "chkShowIbutu";
            chkShowIbutu.Size = new Size(61, 25);
            chkShowIbutu.TabIndex = 28;
            chkShowIbutu.Text = "遺物";
            chkShowIbutu.UseVisualStyleBackColor = true;
            // 
            // chkShowIbutuName
            // 
            chkShowIbutuName.AutoSize = true;
            chkShowIbutuName.Location = new Point(90, 433);
            chkShowIbutuName.Name = "chkShowIbutuName";
            chkShowIbutuName.Size = new Size(15, 14);
            chkShowIbutuName.TabIndex = 29;
            chkShowIbutuName.UseVisualStyleBackColor = true;
            // 
            // chkShowIkou
            // 
            chkShowIkou.AutoSize = true;
            chkShowIkou.Checked = true;
            chkShowIkou.CheckState = CheckState.Checked;
            chkShowIkou.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkShowIkou.ForeColor = Color.Blue;
            chkShowIkou.Location = new Point(5, 452);
            chkShowIkou.Name = "chkShowIkou";
            chkShowIkou.Size = new Size(61, 25);
            chkShowIkou.TabIndex = 30;
            chkShowIkou.Text = "遺構";
            chkShowIkou.UseVisualStyleBackColor = true;
            // 
            // chkShowIkouName
            // 
            chkShowIkouName.AutoSize = true;
            chkShowIkouName.Location = new Point(90, 457);
            chkShowIkouName.Name = "chkShowIkouName";
            chkShowIkouName.Size = new Size(15, 14);
            chkShowIkouName.TabIndex = 31;
            chkShowIkouName.UseVisualStyleBackColor = true;
            // 
            // chkShowDrawingFrame
            // 
            chkShowDrawingFrame.AutoSize = true;
            chkShowDrawingFrame.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkShowDrawingFrame.ForeColor = Color.FromArgb(30, 40, 60);
            chkShowDrawingFrame.Location = new Point(5, 478);
            chkShowDrawingFrame.Name = "chkShowDrawingFrame";
            chkShowDrawingFrame.Size = new Size(61, 25);
            chkShowDrawingFrame.TabIndex = 32;
            chkShowDrawingFrame.Text = "図枠";
            chkShowDrawingFrame.UseVisualStyleBackColor = true;
            // 
            // chkColorByIkouFull
            // 
            chkColorByIkouFull.AutoSize = true;
            chkColorByIkouFull.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkColorByIkouFull.ForeColor = Color.FromArgb(30, 40, 60);
            chkColorByIkouFull.Location = new Point(5, 502);
            chkColorByIkouFull.Name = "chkColorByIkouFull";
            chkColorByIkouFull.Size = new Size(125, 25);
            chkColorByIkouFull.TabIndex = 33;
            chkColorByIkouFull.Text = "遺構名色優先";
            chkColorByIkouFull.UseVisualStyleBackColor = true;
            // 
            // chkShowWhiteBackground
            // 
            chkShowWhiteBackground.AutoSize = true;
            chkShowWhiteBackground.Checked = true;
            chkShowWhiteBackground.CheckState = CheckState.Checked;
            chkShowWhiteBackground.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkShowWhiteBackground.ForeColor = Color.FromArgb(30, 40, 60);
            chkShowWhiteBackground.Location = new Point(5, 526);
            chkShowWhiteBackground.Name = "chkShowWhiteBackground";
            chkShowWhiteBackground.Size = new Size(93, 25);
            chkShowWhiteBackground.TabIndex = 34;
            chkShowWhiteBackground.Text = "背景白色";
            chkShowWhiteBackground.UseVisualStyleBackColor = true;
            // 
            // btnEnvSettings
            // 
            btnEnvSettings.Dock = DockStyle.Left;
            btnEnvSettings.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnEnvSettings.Location = new Point(0, 0);
            btnEnvSettings.Name = "btnEnvSettings";
            btnEnvSettings.Size = new Size(125, 38);
            btnEnvSettings.TabIndex = 1;
            btnEnvSettings.Text = "遺構図面設定";
            btnEnvSettings.UseVisualStyleBackColor = true;
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
            // picCropCanvas
            // 
            picCropCanvas.Dock = DockStyle.Fill;
            picCropCanvas.Location = new Point(0, 0);
            picCropCanvas.Name = "picCropCanvas";
            picCropCanvas.Size = new Size(695, 517);
            picCropCanvas.TabIndex = 1;
            picCropCanvas.TabStop = false;
            // 
            // panelTopRightHeader
            // 
            panelTopRightHeader.BackColor = Color.FromArgb(233, 236, 243);
            panelTopRightHeader.Controls.Add(chkAutoZoomPaperIkou);
            panelTopRightHeader.Controls.Add(lblPaperInfoBanner);
            panelTopRightHeader.Controls.Add(btnPrintPaper);
            panelTopRightHeader.Controls.Add(btnResetPaperZoom);
            panelTopRightHeader.Controls.Add(btnEnvSettings);
            panelTopRightHeader.Dock = DockStyle.Top;
            panelTopRightHeader.Location = new Point(0, 0);
            panelTopRightHeader.Name = "panelTopRightHeader";
            panelTopRightHeader.Size = new Size(691, 38);
            panelTopRightHeader.TabIndex = 0;
            // 
            // lblPaperInfoBanner
            // 
            lblPaperInfoBanner.Dock = DockStyle.Fill;
            lblPaperInfoBanner.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblPaperInfoBanner.ForeColor = Color.FromArgb(25, 45, 80);
            lblPaperInfoBanner.Location = new Point(325, 0);
            lblPaperInfoBanner.Name = "lblPaperInfoBanner";
            lblPaperInfoBanner.Size = new Size(366, 38);
            lblPaperInfoBanner.TabIndex = 5;
            lblPaperInfoBanner.Text = "A3 (420×297mm) | 1/10";
            lblPaperInfoBanner.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnPrintPaper
            // 
            btnPrintPaper.Dock = DockStyle.Left;
            btnPrintPaper.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnPrintPaper.Location = new Point(220, 0);
            btnPrintPaper.Name = "btnPrintPaper";
            btnPrintPaper.Padding = new Padding(2);
            btnPrintPaper.Size = new Size(105, 38);
            btnPrintPaper.TabIndex = 4;
            btnPrintPaper.Text = "🖨 印刷...";
            btnPrintPaper.UseVisualStyleBackColor = true;
            // 
            // chkAutoZoomPaperIkou
            // 
            chkAutoZoomPaperIkou.AutoSize = true;
            chkAutoZoomPaperIkou.Checked = true;
            chkAutoZoomPaperIkou.CheckState = CheckState.Checked;
            chkAutoZoomPaperIkou.Dock = DockStyle.Left;
            chkAutoZoomPaperIkou.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            chkAutoZoomPaperIkou.ForeColor = Color.FromArgb(30, 40, 60);
            chkAutoZoomPaperIkou.Location = new Point(325, 0);
            chkAutoZoomPaperIkou.Name = "chkAutoZoomPaperIkou";
            chkAutoZoomPaperIkou.Padding = new Padding(2);
            chkAutoZoomPaperIkou.Size = new Size(129, 38);
            chkAutoZoomPaperIkou.TabIndex = 3;
            chkAutoZoomPaperIkou.Text = "選択遺構拡大";
            chkAutoZoomPaperIkou.UseVisualStyleBackColor = true;
            // 
            // btnResetPaperZoom
            // 
            btnResetPaperZoom.Dock = DockStyle.Left;
            btnResetPaperZoom.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnResetPaperZoom.Location = new Point(125, 0);
            btnResetPaperZoom.Name = "btnResetPaperZoom";
            btnResetPaperZoom.Padding = new Padding(2);
            btnResetPaperZoom.Size = new Size(95, 38);
            btnResetPaperZoom.TabIndex = 2;
            btnResetPaperZoom.Text = "全図表示";
            btnResetPaperZoom.UseVisualStyleBackColor = true;
            // 
            // picPaperCanvas
            // 
            picPaperCanvas.Dock = DockStyle.Fill;
            picPaperCanvas.Location = new Point(0, 38);
            picPaperCanvas.Name = "picPaperCanvas";
            picPaperCanvas.Padding = new Padding(2);
            picPaperCanvas.Size = new Size(691, 479);
            picPaperCanvas.TabIndex = 1;
            picPaperCanvas.TabStop = false;
            // 
            // splitContainerTopHorizontal
            // 
            splitContainerTopHorizontal.Dock = DockStyle.Fill;
            splitContainerTopHorizontal.Location = new Point(0, 0);
            splitContainerTopHorizontal.Name = "splitContainerTopHorizontal";
            // 
            // splitContainerTopHorizontal.Panel1
            // 
            splitContainerTopHorizontal.Panel1.Controls.Add(panelTopLeft);
            // 
            // splitContainerTopHorizontal.Panel2
            // 
            splitContainerTopHorizontal.Panel2.Controls.Add(panelTopRight);
            splitContainerTopHorizontal.Size = new Size(1390, 517);
            splitContainerTopHorizontal.SplitterDistance = 695;
            splitContainerTopHorizontal.TabIndex = 1;
            // 
            // panelTopLeft
            // 
            panelTopLeft.Controls.Add(picCropCanvas);
            panelTopLeft.Dock = DockStyle.Fill;
            panelTopLeft.Location = new Point(0, 0);
            panelTopLeft.Name = "panelTopLeft";
            panelTopLeft.Size = new Size(695, 517);
            panelTopLeft.TabIndex = 0;
            // 
            // panelTopRight
            // 
            panelTopRight.Controls.Add(picPaperCanvas);
            panelTopRight.Controls.Add(panelTopRightHeader);
            panelTopRight.Dock = DockStyle.Fill;
            panelTopRight.Location = new Point(0, 0);
            panelTopRight.Name = "panelTopRight";
            panelTopRight.Size = new Size(691, 517);
            panelTopRight.TabIndex = 0;
            // 
            // panelBottomFixedGroup
            // 
            panelBottomFixedGroup.BackColor = Color.FromArgb(245, 246, 248);
            panelBottomFixedGroup.Controls.Add(grpFeatureDetailPreview);
            panelBottomFixedGroup.Controls.Add(panelControls);
            panelBottomFixedGroup.Controls.Add(panelBottomGrids);
            panelBottomFixedGroup.Dock = DockStyle.Bottom;
            panelBottomFixedGroup.Location = new Point(0, 517);
            panelBottomFixedGroup.Name = "panelBottomFixedGroup";
            panelBottomFixedGroup.Size = new Size(1390, 361);
            panelBottomFixedGroup.TabIndex = 3;
            // 
            // grpFeatureDetailPreview
            // 
            grpFeatureDetailPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpFeatureDetailPreview.BackColor = Color.FromArgb(245, 246, 248);
            grpFeatureDetailPreview.Controls.Add(picFeatureDetailCanvas);
            grpFeatureDetailPreview.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpFeatureDetailPreview.ForeColor = Color.FromArgb(25, 55, 105);
            grpFeatureDetailPreview.Location = new Point(968, 0);
            grpFeatureDetailPreview.Name = "grpFeatureDetailPreview";
            grpFeatureDetailPreview.Size = new Size(422, 361);
            grpFeatureDetailPreview.TabIndex = 2;
            grpFeatureDetailPreview.TabStop = false;
            grpFeatureDetailPreview.Text = "選択遺構 詳細プレビュー";
            // 
            // picFeatureDetailCanvas
            // 
            picFeatureDetailCanvas.BackColor = Color.FromArgb(250, 250, 250);
            picFeatureDetailCanvas.Dock = DockStyle.Fill;
            picFeatureDetailCanvas.Location = new Point(3, 25);
            picFeatureDetailCanvas.Name = "picFeatureDetailCanvas";
            picFeatureDetailCanvas.Size = new Size(416, 333);
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
            panelControls.Size = new Size(380, 361);
            panelControls.TabIndex = 1;
            // 
            // grpIkouProps
            // 
            grpIkouProps.BackColor = Color.FromArgb(245, 246, 248);
            grpIkouProps.Controls.Add(grpDanmenProps);
            grpIkouProps.Controls.Add(grpCompassProps);
            grpIkouProps.Controls.Add(btnSetPaperPosition);
            grpIkouProps.Controls.Add(btnRecalcIkouDrawing);
            grpIkouProps.Controls.Add(btnPickCropBounds);
            grpIkouProps.Controls.Add(cmbFeatureSelect);
            grpIkouProps.Controls.Add(lblTargetIkou);
            grpIkouProps.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpIkouProps.ForeColor = Color.FromArgb(25, 55, 105);
            grpIkouProps.Location = new Point(3, 102);
            grpIkouProps.Name = "grpIkouProps";
            grpIkouProps.Size = new Size(374, 255);
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
            grpDanmenProps.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpDanmenProps.ForeColor = Color.FromArgb(25, 55, 105);
            grpDanmenProps.Location = new Point(10, 168);
            grpDanmenProps.Name = "grpDanmenProps";
            grpDanmenProps.Size = new Size(354, 75);
            grpDanmenProps.TabIndex = 6;
            grpDanmenProps.TabStop = false;
            grpDanmenProps.Text = "断面 (複数定義可能)";
            // 
            // btnSetDanmenPosition
            // 
            btnSetDanmenPosition.BackColor = Color.FromArgb(225, 232, 242);
            btnSetDanmenPosition.FlatStyle = FlatStyle.Flat;
            btnSetDanmenPosition.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnSetDanmenPosition.ForeColor = Color.FromArgb(25, 45, 80);
            btnSetDanmenPosition.Location = new Point(190, 23);
            btnSetDanmenPosition.Name = "btnSetDanmenPosition";
            btnSetDanmenPosition.Size = new Size(130, 32);
            btnSetDanmenPosition.TabIndex = 2;
            btnSetDanmenPosition.Text = "断面指定";
            btnSetDanmenPosition.UseVisualStyleBackColor = false;
            // 
            // txtDanmenName
            // 
            txtDanmenName.BackColor = Color.FromArgb(255, 255, 191);
            txtDanmenName.Font = new Font("Yu Gothic UI", 12F);
            txtDanmenName.ForeColor = Color.Black;
            txtDanmenName.Location = new Point(66, 25);
            txtDanmenName.Name = "txtDanmenName";
            txtDanmenName.Size = new Size(115, 29);
            txtDanmenName.TabIndex = 1;
            // 
            // lblDanmenName
            // 
            lblDanmenName.AutoSize = true;
            lblDanmenName.Font = new Font("Yu Gothic UI", 12F);
            lblDanmenName.ForeColor = Color.FromArgb(33, 37, 41);
            lblDanmenName.Location = new Point(8, 28);
            lblDanmenName.Name = "lblDanmenName";
            lblDanmenName.Size = new Size(58, 21);
            lblDanmenName.TabIndex = 0;
            lblDanmenName.Text = "断面名";
            // 
            // grpCompassProps
            // 
            grpCompassProps.BackColor = Color.FromArgb(245, 246, 248);
            grpCompassProps.Controls.Add(btnSetDirectionPosition);
            grpCompassProps.Controls.Add(chkShowDirection);
            grpCompassProps.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpCompassProps.ForeColor = Color.FromArgb(25, 55, 105);
            grpCompassProps.Location = new Point(10, 96);
            grpCompassProps.Name = "grpCompassProps";
            grpCompassProps.Size = new Size(354, 65);
            grpCompassProps.TabIndex = 5;
            grpCompassProps.TabStop = false;
            grpCompassProps.Text = "方位マーク";
            // 
            // btnSetDirectionPosition
            // 
            btnSetDirectionPosition.BackColor = Color.FromArgb(225, 232, 242);
            btnSetDirectionPosition.FlatStyle = FlatStyle.Flat;
            btnSetDirectionPosition.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnSetDirectionPosition.ForeColor = Color.FromArgb(25, 45, 80);
            btnSetDirectionPosition.Location = new Point(90, 22);
            btnSetDirectionPosition.Name = "btnSetDirectionPosition";
            btnSetDirectionPosition.Size = new Size(140, 32);
            btnSetDirectionPosition.TabIndex = 1;
            btnSetDirectionPosition.Text = "表示位置指定";
            btnSetDirectionPosition.UseVisualStyleBackColor = false;
            // 
            // chkShowDirection
            // 
            chkShowDirection.AutoSize = true;
            chkShowDirection.Checked = true;
            chkShowDirection.CheckState = CheckState.Checked;
            chkShowDirection.Font = new Font("Yu Gothic UI", 12F);
            chkShowDirection.ForeColor = Color.FromArgb(33, 37, 41);
            chkShowDirection.Location = new Point(12, 26);
            chkShowDirection.Name = "chkShowDirection";
            chkShowDirection.Size = new Size(61, 25);
            chkShowDirection.TabIndex = 0;
            chkShowDirection.Text = "表示";
            chkShowDirection.UseVisualStyleBackColor = true;
            // 
            // btnSetPaperPosition
            // 
            btnSetPaperPosition.BackColor = Color.FromArgb(225, 232, 242);
            btnSetPaperPosition.FlatStyle = FlatStyle.Flat;
            btnSetPaperPosition.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnSetPaperPosition.ForeColor = Color.FromArgb(25, 45, 80);
            btnSetPaperPosition.Location = new Point(242, 20);
            btnSetPaperPosition.Name = "btnSetPaperPosition";
            btnSetPaperPosition.Size = new Size(124, 32);
            btnSetPaperPosition.TabIndex = 4;
            btnSetPaperPosition.Text = "表示位置指定";
            btnSetPaperPosition.UseVisualStyleBackColor = false;
            // 
            // btnRecalcIkouDrawing
            // 
            btnRecalcIkouDrawing.BackColor = Color.FromArgb(225, 232, 242);
            btnRecalcIkouDrawing.FlatStyle = FlatStyle.Flat;
            btnRecalcIkouDrawing.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnRecalcIkouDrawing.ForeColor = Color.FromArgb(25, 45, 80);
            btnRecalcIkouDrawing.Location = new Point(242, 56);
            btnRecalcIkouDrawing.Name = "btnRecalcIkouDrawing";
            btnRecalcIkouDrawing.Size = new Size(124, 32);
            btnRecalcIkouDrawing.TabIndex = 5;
            btnRecalcIkouDrawing.Text = "遺構図再計算";
            btnRecalcIkouDrawing.UseVisualStyleBackColor = false;
            // 
            // btnPickCropBounds
            // 
            btnPickCropBounds.BackColor = Color.FromArgb(225, 232, 242);
            btnPickCropBounds.FlatStyle = FlatStyle.Flat;
            btnPickCropBounds.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnPickCropBounds.ForeColor = Color.FromArgb(25, 45, 80);
            btnPickCropBounds.Location = new Point(66, 56);
            btnPickCropBounds.Name = "btnPickCropBounds";
            btnPickCropBounds.Size = new Size(170, 32);
            btnPickCropBounds.TabIndex = 3;
            btnPickCropBounds.Text = "遺構枠指示 (3点)";
            btnPickCropBounds.UseVisualStyleBackColor = false;
            // 
            // cmbFeatureSelect
            // 
            cmbFeatureSelect.BackColor = Color.FromArgb(255, 255, 191);
            cmbFeatureSelect.Font = new Font("Yu Gothic UI", 12F);
            cmbFeatureSelect.ForeColor = Color.Black;
            cmbFeatureSelect.Location = new Point(66, 21);
            cmbFeatureSelect.Name = "cmbFeatureSelect";
            cmbFeatureSelect.Size = new Size(170, 29);
            cmbFeatureSelect.TabIndex = 1;
            // 
            // lblTargetIkou
            // 
            lblTargetIkou.AutoSize = true;
            lblTargetIkou.Font = new Font("Yu Gothic UI", 12F);
            lblTargetIkou.ForeColor = Color.FromArgb(33, 37, 41);
            lblTargetIkou.Location = new Point(8, 24);
            lblTargetIkou.Name = "lblTargetIkou";
            lblTargetIkou.Size = new Size(58, 21);
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
            grpDrawingProps.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpDrawingProps.ForeColor = Color.FromArgb(25, 55, 105);
            grpDrawingProps.Location = new Point(3, 3);
            grpDrawingProps.Name = "grpDrawingProps";
            grpDrawingProps.Size = new Size(374, 95);
            grpDrawingProps.TabIndex = 0;
            grpDrawingProps.TabStop = false;
            grpDrawingProps.Text = "図面";
            // 
            // cmbScale
            // 
            cmbScale.BackColor = Color.White;
            cmbScale.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScale.Font = new Font("Yu Gothic UI", 12F);
            cmbScale.ForeColor = Color.FromArgb(33, 37, 41);
            cmbScale.Location = new Point(248, 21);
            cmbScale.Name = "cmbScale";
            cmbScale.Size = new Size(75, 29);
            cmbScale.TabIndex = 5;
            // 
            // lblScale
            // 
            lblScale.AutoSize = true;
            lblScale.Font = new Font("Yu Gothic UI", 12F);
            lblScale.ForeColor = Color.FromArgb(33, 37, 41);
            lblScale.Location = new Point(180, 24);
            lblScale.Name = "lblScale";
            lblScale.Size = new Size(61, 21);
            lblScale.TabIndex = 4;
            lblScale.Text = "縮尺 1/";
            // 
            // cmbPaperSize
            // 
            cmbPaperSize.BackColor = Color.White;
            cmbPaperSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaperSize.Font = new Font("Yu Gothic UI", 12F);
            cmbPaperSize.ForeColor = Color.FromArgb(33, 37, 41);
            cmbPaperSize.Location = new Point(66, 56);
            cmbPaperSize.Name = "cmbPaperSize";
            cmbPaperSize.Size = new Size(165, 29);
            cmbPaperSize.TabIndex = 3;
            // 
            // lblPaperSize
            // 
            lblPaperSize.AutoSize = true;
            lblPaperSize.Font = new Font("Yu Gothic UI", 12F);
            lblPaperSize.ForeColor = Color.FromArgb(33, 37, 41);
            lblPaperSize.Location = new Point(8, 59);
            lblPaperSize.Name = "lblPaperSize";
            lblPaperSize.Size = new Size(42, 21);
            lblPaperSize.TabIndex = 2;
            lblPaperSize.Text = "用紙";
            // 
            // txtDrawingName
            // 
            txtDrawingName.BackColor = Color.FromArgb(255, 255, 191);
            txtDrawingName.Font = new Font("Yu Gothic UI", 12F);
            txtDrawingName.ForeColor = Color.Black;
            txtDrawingName.Location = new Point(66, 21);
            txtDrawingName.Name = "txtDrawingName";
            txtDrawingName.Size = new Size(105, 29);
            txtDrawingName.TabIndex = 1;
            // 
            // lblDrawingName
            // 
            lblDrawingName.AutoSize = true;
            lblDrawingName.Font = new Font("Yu Gothic UI", 12F);
            lblDrawingName.ForeColor = Color.FromArgb(33, 37, 41);
            lblDrawingName.Location = new Point(8, 24);
            lblDrawingName.Name = "lblDrawingName";
            lblDrawingName.Size = new Size(58, 21);
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
            grpDrawings.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpDrawings.ForeColor = Color.FromArgb(25, 55, 105);
            grpDrawings.Location = new Point(3, 3);
            grpDrawings.Name = "grpDrawings";
            grpDrawings.Size = new Size(192, 355);
            grpDrawings.TabIndex = 0;
            grpDrawings.TabStop = false;
            grpDrawings.Text = "図面名称";
            // 
            // dgvDrawings
            // 
            dgvDrawings.AllowUserToAddRows = false;
            dgvDrawings.AllowUserToDeleteRows = false;
            dgvDrawings.Dock = DockStyle.Top;
            dgvDrawings.Location = new Point(3, 25);
            dgvDrawings.Name = "dgvDrawings";
            dgvDrawings.Size = new Size(186, 290);
            dgvDrawings.TabIndex = 0;
            // 
            // btnDeleteDrawing
            // 
            btnDeleteDrawing.BackColor = Color.FromArgb(195, 55, 55);
            btnDeleteDrawing.FlatStyle = FlatStyle.Flat;
            btnDeleteDrawing.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnDeleteDrawing.ForeColor = Color.White;
            btnDeleteDrawing.Location = new Point(6, 318);
            btnDeleteDrawing.Name = "btnDeleteDrawing";
            btnDeleteDrawing.Size = new Size(56, 30);
            btnDeleteDrawing.TabIndex = 1;
            btnDeleteDrawing.Text = "削除";
            btnDeleteDrawing.UseVisualStyleBackColor = false;
            // 
            // btnUpdateDrawingProps
            // 
            btnUpdateDrawingProps.BackColor = Color.FromArgb(38, 145, 75);
            btnUpdateDrawingProps.FlatStyle = FlatStyle.Flat;
            btnUpdateDrawingProps.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnUpdateDrawingProps.ForeColor = Color.White;
            btnUpdateDrawingProps.Location = new Point(66, 318);
            btnUpdateDrawingProps.Name = "btnUpdateDrawingProps";
            btnUpdateDrawingProps.Size = new Size(56, 30);
            btnUpdateDrawingProps.TabIndex = 2;
            btnUpdateDrawingProps.Text = "更新";
            btnUpdateDrawingProps.UseVisualStyleBackColor = false;
            // 
            // btnAddDrawing
            // 
            btnAddDrawing.BackColor = Color.FromArgb(30, 115, 210);
            btnAddDrawing.FlatStyle = FlatStyle.Flat;
            btnAddDrawing.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnAddDrawing.ForeColor = Color.White;
            btnAddDrawing.Location = new Point(126, 318);
            btnAddDrawing.Name = "btnAddDrawing";
            btnAddDrawing.Size = new Size(56, 30);
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
            grpDrawingIkous.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpDrawingIkous.ForeColor = Color.FromArgb(25, 55, 105);
            grpDrawingIkous.Location = new Point(201, 3);
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
            dgvDrawingIkous.Location = new Point(3, 25);
            dgvDrawingIkous.Name = "dgvDrawingIkous";
            dgvDrawingIkous.Size = new Size(181, 290);
            dgvDrawingIkous.TabIndex = 0;
            // 
            // btnDeleteDrawingIkou
            // 
            btnDeleteDrawingIkou.BackColor = Color.FromArgb(195, 55, 55);
            btnDeleteDrawingIkou.FlatStyle = FlatStyle.Flat;
            btnDeleteDrawingIkou.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnDeleteDrawingIkou.ForeColor = Color.White;
            btnDeleteDrawingIkou.Location = new Point(6, 318);
            btnDeleteDrawingIkou.Name = "btnDeleteDrawingIkou";
            btnDeleteDrawingIkou.Size = new Size(56, 30);
            btnDeleteDrawingIkou.TabIndex = 1;
            btnDeleteDrawingIkou.Text = "削除";
            btnDeleteDrawingIkou.UseVisualStyleBackColor = false;
            // 
            // btnUpdateIkouProps
            // 
            btnUpdateIkouProps.BackColor = Color.FromArgb(38, 145, 75);
            btnUpdateIkouProps.FlatStyle = FlatStyle.Flat;
            btnUpdateIkouProps.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnUpdateIkouProps.ForeColor = Color.White;
            btnUpdateIkouProps.Location = new Point(66, 318);
            btnUpdateIkouProps.Name = "btnUpdateIkouProps";
            btnUpdateIkouProps.Size = new Size(56, 30);
            btnUpdateIkouProps.TabIndex = 2;
            btnUpdateIkouProps.Text = "更新";
            btnUpdateIkouProps.UseVisualStyleBackColor = false;
            // 
            // btnAddDrawingIkou
            // 
            btnAddDrawingIkou.BackColor = Color.FromArgb(30, 115, 210);
            btnAddDrawingIkou.FlatStyle = FlatStyle.Flat;
            btnAddDrawingIkou.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnAddDrawingIkou.ForeColor = Color.White;
            btnAddDrawingIkou.Location = new Point(126, 318);
            btnAddDrawingIkou.Name = "btnAddDrawingIkou";
            btnAddDrawingIkou.Size = new Size(56, 30);
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
            grpDanmenList.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpDanmenList.ForeColor = Color.FromArgb(25, 55, 105);
            grpDanmenList.Location = new Point(394, 3);
            grpDanmenList.Name = "grpDanmenList";
            grpDanmenList.Size = new Size(188, 355);
            grpDanmenList.TabIndex = 2;
            grpDanmenList.TabStop = false;
            grpDanmenList.Text = "断面名称";
            // 
            // dgvDanmen
            // 
            dgvDanmen.AllowUserToAddRows = false;
            dgvDanmen.AllowUserToDeleteRows = false;
            dgvDanmen.Dock = DockStyle.Top;
            dgvDanmen.Location = new Point(3, 25);
            dgvDanmen.Name = "dgvDanmen";
            dgvDanmen.Size = new Size(182, 290);
            dgvDanmen.TabIndex = 0;
            // 
            // btnDeleteDanmen
            // 
            btnDeleteDanmen.BackColor = Color.FromArgb(195, 55, 55);
            btnDeleteDanmen.FlatStyle = FlatStyle.Flat;
            btnDeleteDanmen.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnDeleteDanmen.ForeColor = Color.White;
            btnDeleteDanmen.Location = new Point(6, 318);
            btnDeleteDanmen.Name = "btnDeleteDanmen";
            btnDeleteDanmen.Size = new Size(56, 30);
            btnDeleteDanmen.TabIndex = 1;
            btnDeleteDanmen.Text = "削除";
            btnDeleteDanmen.UseVisualStyleBackColor = false;
            // 
            // btnUpdateDanmenName
            // 
            btnUpdateDanmenName.BackColor = Color.FromArgb(38, 145, 75);
            btnUpdateDanmenName.FlatStyle = FlatStyle.Flat;
            btnUpdateDanmenName.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnUpdateDanmenName.ForeColor = Color.White;
            btnUpdateDanmenName.Location = new Point(66, 318);
            btnUpdateDanmenName.Name = "btnUpdateDanmenName";
            btnUpdateDanmenName.Size = new Size(56, 30);
            btnUpdateDanmenName.TabIndex = 2;
            btnUpdateDanmenName.Text = "更新";
            btnUpdateDanmenName.UseVisualStyleBackColor = false;
            // 
            // btnAddDanmen
            // 
            btnAddDanmen.BackColor = Color.FromArgb(30, 115, 210);
            btnAddDanmen.FlatStyle = FlatStyle.Flat;
            btnAddDanmen.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnAddDanmen.ForeColor = Color.White;
            btnAddDanmen.Location = new Point(126, 318);
            btnAddDanmen.Name = "btnAddDanmen";
            btnAddDanmen.Size = new Size(56, 30);
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
            // panelMiddle
            // 
            panelMiddle.Controls.Add(panelRight);
            panelMiddle.Controls.Add(panelHcLeftSidebar);
            panelMiddle.Dock = DockStyle.Fill;
            panelMiddle.Location = new Point(0, 60);
            panelMiddle.Name = "panelMiddle";
            panelMiddle.Size = new Size(1520, 878);
            panelMiddle.TabIndex = 4;
            // 
            // panelRight
            // 
            panelRight.Controls.Add(splitContainerTopHorizontal);
            panelRight.Controls.Add(panelBottomFixedGroup);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(130, 0);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(1390, 878);
            panelRight.TabIndex = 2;
            // 
            // FormDrawingEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 248);
            ClientSize = new Size(1520, 960);
            Controls.Add(panelMiddle);
            Controls.Add(panelHeader);
            Controls.Add(statusStripBar);
            Font = new Font("Yu Gothic UI", 9F);
            MinimumSize = new Size(1280, 780);
            Name = "FormDrawingEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "遺跡調査システム(内業) - 個別遺構図作成エディタ";
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
            panelMiddle.ResumeLayout(false);
            panelRight.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnDrawingFrame;
        private System.Windows.Forms.CheckBox chkShowDrawingFrame;
        private System.Windows.Forms.CheckBox chkShowWhiteBackground;
        private System.Windows.Forms.CheckBox chkShowIkouFull;
        private System.Windows.Forms.CheckBox chkShowIbutuFull;
        private System.Windows.Forms.CheckBox chkShowKikaiFull;
        private System.Windows.Forms.CheckBox chkShowCurveFull;
        private System.Windows.Forms.CheckBox chkShowGridFull;
        private System.Windows.Forms.PictureBox picCropCanvas;

        private System.Windows.Forms.Panel panelTopRightHeader;
        private System.Windows.Forms.Button btnResetPaperZoom;
        private System.Windows.Forms.CheckBox chkAutoZoomPaperIkou;
        private System.Windows.Forms.Button btnPrintPaper;
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
        private System.Windows.Forms.Button btnRecalcIkouDrawing;
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

        private System.Windows.Forms.Button btnIkouNameColorSettings;
        private System.Windows.Forms.CheckBox chkColorByIkouFull;

        private System.Windows.Forms.StatusStrip statusStripBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCoords;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMessage;
        private Panel panelMiddle;
        private Panel panelRight;
    }
}
