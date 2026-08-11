namespace MdbFdbExporter
{
    partial class FormMain
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.grpDbList = new System.Windows.Forms.GroupBox();
            this.lblSelectedDbStatus = new System.Windows.Forms.Label();
            this.lstDbFolders = new System.Windows.Forms.ListBox();
            this.grpSplit = new System.Windows.Forms.GroupBox();
            this.lblRegexHint = new System.Windows.Forms.Label();
            this.txtRegexPattern3 = new System.Windows.Forms.TextBox();
            this.cmbRule3 = new System.Windows.Forms.ComboBox();
            this.lblPriority3 = new System.Windows.Forms.Label();
            this.txtRegexPattern2 = new System.Windows.Forms.TextBox();
            this.cmbRule2 = new System.Windows.Forms.ComboBox();
            this.lblPriority2 = new System.Windows.Forms.Label();
            this.txtRegexPattern1 = new System.Windows.Forms.TextBox();
            this.cmbRule1 = new System.Windows.Forms.ComboBox();
            this.lblPriority1 = new System.Windows.Forms.Label();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.tabPreview = new System.Windows.Forms.TabControl();
            this.tabList = new System.Windows.Forms.TabPage();
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.tabDetail = new System.Windows.Forms.TabPage();
            this.btnToggleGrid = new System.Windows.Forms.Button();
            this.btnToggle2D = new System.Windows.Forms.Button();
            this.picEmbedded2D = new System.Windows.Forms.PictureBox();
            this.btnOpenViewer = new System.Windows.Forms.Button();
            this.lblPoints = new System.Windows.Forms.Label();
            this.dgvPoints = new System.Windows.Forms.DataGridView();
            this.lblIkouLine = new System.Windows.Forms.Label();
            this.lstIkouLine = new System.Windows.Forms.ListBox();
            this.lblIkou = new System.Windows.Forms.Label();
            this.lstIkou = new System.Windows.Forms.ListBox();
            this.grpAllIkouCanvas = new System.Windows.Forms.GroupBox();
            this.picAllIkouCanvas = new System.Windows.Forms.PictureBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnShowLog = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.chkShiftJis = new System.Windows.Forms.CheckBox();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgressPercent = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.grpDbList.SuspendLayout();
            this.grpSplit.SuspendLayout();
            this.grpPreview.SuspendLayout();
            this.tabPreview.SuspendLayout();
            this.tabList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.tabDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmbedded2D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).BeginInit();
            this.grpAllIkouCanvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAllIkouCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(378, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "SITE7 Data Migration Utility (MDB/FDB)";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.panelHeader.Controls.Add(this.btnSettings);
            this.panelHeader.Controls.Add(this.lblSubHeader);
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(834, 65);
            this.panelHeader.TabIndex = 1;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(714, 15);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(105, 35);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "⚙ 設定変更";
            this.btnSettings.UseVisualStyleBackColor = false;
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.lblSubHeader.Location = new System.Drawing.Point(14, 40);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(434, 15);
            this.lblSubHeader.TabIndex = 1;
            this.lblSubHeader.Text = "Extract archaeological artifacts and feature survey points from Access & Firebird to CSV files.";
            // 
            // grpDbList
            // 
            this.grpDbList.Controls.Add(this.lblSelectedDbStatus);
            this.grpDbList.Controls.Add(this.lstDbFolders);
            this.grpDbList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpDbList.Location = new System.Drawing.Point(12, 75);
            this.grpDbList.Name = "grpDbList";
            this.grpDbList.Size = new System.Drawing.Size(300, 130);
            this.grpDbList.TabIndex = 2;
            this.grpDbList.TabStop = false;
            this.grpDbList.Text = "DB List (DB格納フォルダ一覧)";
            // 
            // lblSelectedDbStatus
            // 
            this.lblSelectedDbStatus.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSelectedDbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.lblSelectedDbStatus.Location = new System.Drawing.Point(10, 105);
            this.lblSelectedDbStatus.Name = "lblSelectedDbStatus";
            this.lblSelectedDbStatus.Size = new System.Drawing.Size(280, 20);
            this.lblSelectedDbStatus.TabIndex = 1;
            this.lblSelectedDbStatus.Text = "DBフォルダを選択してください";
            // 
            // lstDbFolders
            // 
            this.lstDbFolders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.lstDbFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDbFolders.ForeColor = System.Drawing.Color.White;
            this.lstDbFolders.FormattingEnabled = true;
            this.lstDbFolders.ItemHeight = 15;
            this.lstDbFolders.Location = new System.Drawing.Point(10, 22);
            this.lstDbFolders.Name = "lstDbFolders";
            this.lstDbFolders.Size = new System.Drawing.Size(280, 77);
            this.lstDbFolders.TabIndex = 0;
            // 
            // grpSplit
            // 
            this.grpSplit.Controls.Add(this.lblRegexHint);
            this.grpSplit.Controls.Add(this.txtRegexPattern3);
            this.grpSplit.Controls.Add(this.cmbRule3);
            this.grpSplit.Controls.Add(this.lblPriority3);
            this.grpSplit.Controls.Add(this.txtRegexPattern2);
            this.grpSplit.Controls.Add(this.cmbRule2);
            this.grpSplit.Controls.Add(this.lblPriority2);
            this.grpSplit.Controls.Add(this.txtRegexPattern1);
            this.grpSplit.Controls.Add(this.cmbRule1);
            this.grpSplit.Controls.Add(this.lblPriority1);
            this.grpSplit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpSplit.Location = new System.Drawing.Point(324, 75);
            this.grpSplit.Name = "grpSplit";
            this.grpSplit.Size = new System.Drawing.Size(498, 130);
            this.grpSplit.TabIndex = 3;
            this.grpSplit.TabStop = false;
            this.grpSplit.Text = "IKOU / IKOULINE Splitting Rules";
            // 
            // lblRegexHint
            // 
            this.lblRegexHint.Font = new System.Drawing.Font("Consolas", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRegexHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.lblRegexHint.Location = new System.Drawing.Point(395, 20);
            this.lblRegexHint.Name = "lblRegexHint";
            this.lblRegexHint.Size = new System.Drawing.Size(95, 95);
            this.lblRegexHint.TabIndex = 10;
            this.lblRegexHint.Text = "Regex Groups:\r\n(?<ikou>...)\r\n(?<ikouline>...)\r\n\r\nRules are run\r\nin order (1 to 3)";
            // 
            // txtRegexPattern3
            // 
            this.txtRegexPattern3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.txtRegexPattern3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegexPattern3.Enabled = false;
            this.txtRegexPattern3.ForeColor = System.Drawing.Color.White;
            this.txtRegexPattern3.Location = new System.Drawing.Point(235, 85);
            this.txtRegexPattern3.Name = "txtRegexPattern3";
            this.txtRegexPattern3.Size = new System.Drawing.Size(150, 23);
            this.txtRegexPattern3.TabIndex = 8;
            // 
            // cmbRule3
            // 
            this.cmbRule3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbRule3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRule3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRule3.ForeColor = System.Drawing.Color.White;
            this.cmbRule3.FormattingEnabled = true;
            this.cmbRule3.Location = new System.Drawing.Point(75, 85);
            this.cmbRule3.Name = "cmbRule3";
            this.cmbRule3.Size = new System.Drawing.Size(150, 23);
            this.cmbRule3.TabIndex = 7;
            // 
            // lblPriority3
            // 
            this.lblPriority3.AutoSize = true;
            this.lblPriority3.ForeColor = System.Drawing.Color.White;
            this.lblPriority3.Location = new System.Drawing.Point(15, 88);
            this.lblPriority3.Name = "lblPriority3";
            this.lblPriority3.Size = new System.Drawing.Size(54, 15);
            this.lblPriority3.TabIndex = 6;
            this.lblPriority3.Text = "Priority 3:";
            // 
            // txtRegexPattern2
            // 
            this.txtRegexPattern2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.txtRegexPattern2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegexPattern2.Enabled = false;
            this.txtRegexPattern2.ForeColor = System.Drawing.Color.White;
            this.txtRegexPattern2.Location = new System.Drawing.Point(235, 55);
            this.txtRegexPattern2.Name = "txtRegexPattern2";
            this.txtRegexPattern2.Size = new System.Drawing.Size(150, 23);
            this.txtRegexPattern2.TabIndex = 5;
            // 
            // cmbRule2
            // 
            this.cmbRule2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbRule2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRule2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRule2.ForeColor = System.Drawing.Color.White;
            this.cmbRule2.FormattingEnabled = true;
            this.cmbRule2.Location = new System.Drawing.Point(75, 55);
            this.cmbRule2.Name = "cmbRule2";
            this.cmbRule2.Size = new System.Drawing.Size(150, 23);
            this.cmbRule2.TabIndex = 4;
            // 
            // lblPriority2
            // 
            this.lblPriority2.AutoSize = true;
            this.lblPriority2.ForeColor = System.Drawing.Color.White;
            this.lblPriority2.Location = new System.Drawing.Point(15, 58);
            this.lblPriority2.Name = "lblPriority2";
            this.lblPriority2.Size = new System.Drawing.Size(54, 15);
            this.lblPriority2.TabIndex = 3;
            this.lblPriority2.Text = "Priority 2:";
            // 
            // txtRegexPattern1
            // 
            this.txtRegexPattern1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.txtRegexPattern1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegexPattern1.Enabled = false;
            this.txtRegexPattern1.ForeColor = System.Drawing.Color.White;
            this.txtRegexPattern1.Location = new System.Drawing.Point(235, 25);
            this.txtRegexPattern1.Name = "txtRegexPattern1";
            this.txtRegexPattern1.Size = new System.Drawing.Size(150, 23);
            this.txtRegexPattern1.TabIndex = 2;
            // 
            // cmbRule1
            // 
            this.cmbRule1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.cmbRule1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRule1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRule1.ForeColor = System.Drawing.Color.White;
            this.cmbRule1.FormattingEnabled = true;
            this.cmbRule1.Location = new System.Drawing.Point(75, 25);
            this.cmbRule1.Name = "cmbRule1";
            this.cmbRule1.Size = new System.Drawing.Size(150, 23);
            this.cmbRule1.TabIndex = 1;
            // 
            // lblPriority1
            // 
            this.lblPriority1.AutoSize = true;
            this.lblPriority1.ForeColor = System.Drawing.Color.White;
            this.lblPriority1.Location = new System.Drawing.Point(15, 28);
            this.lblPriority1.Name = "lblPriority1";
            this.lblPriority1.Size = new System.Drawing.Size(54, 15);
            this.lblPriority1.TabIndex = 0;
            this.lblPriority1.Text = "Priority 1:";
            // 
            // grpPreview
            // 
            this.grpPreview.Controls.Add(this.tabPreview);
            this.grpPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpPreview.Location = new System.Drawing.Point(12, 215);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.Size = new System.Drawing.Size(810, 330);
            this.grpPreview.TabIndex = 4;
            this.grpPreview.TabStop = false;
            this.grpPreview.Text = "Data Analysis & Table Preview";
            // 
            // tabPreview
            // 
            this.tabPreview.Controls.Add(this.tabList);
            this.tabPreview.Controls.Add(this.tabDetail);
            this.tabPreview.Location = new System.Drawing.Point(10, 22);
            this.tabPreview.Name = "tabPreview";
            this.tabPreview.SelectedIndex = 0;
            this.tabPreview.Size = new System.Drawing.Size(790, 300);
            this.tabPreview.TabIndex = 0;
            // 
            // tabList
            // 
            this.tabList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.tabList.Controls.Add(this.dgvPreview);
            this.tabList.Location = new System.Drawing.Point(4, 24);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(782, 272);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "📋 グループ名分割一覧";
            // 
            // dgvPreview
            // 
            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.AllowUserToDeleteRows = false;
            this.dgvPreview.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPreview.Location = new System.Drawing.Point(3, 3);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.RowHeadersVisible = false;
            this.dgvPreview.RowTemplate.Height = 25;
            this.dgvPreview.Size = new System.Drawing.Size(776, 266);
            this.dgvPreview.TabIndex = 0;
            // 
            // tabDetail
            // 
            this.tabDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.tabDetail.Controls.Add(this.btnToggleGrid);
            this.tabDetail.Controls.Add(this.btnToggle2D);
            this.tabDetail.Controls.Add(this.btnOpenViewer);
            this.tabDetail.Controls.Add(this.lblPoints);
            this.tabDetail.Controls.Add(this.picEmbedded2D);
            this.tabDetail.Controls.Add(this.dgvPoints);
            this.tabDetail.Controls.Add(this.lblIkouLine);
            this.tabDetail.Controls.Add(this.lstIkouLine);
            this.tabDetail.Controls.Add(this.lblIkou);
            this.tabDetail.Controls.Add(this.lstIkou);
            this.tabDetail.Location = new System.Drawing.Point(4, 24);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetail.Size = new System.Drawing.Size(782, 272);
            this.tabDetail.TabIndex = 1;
            this.tabDetail.Text = "🔗 階層連動表示 (Ikou ➡ IkouLine ➡ XYZ座標 / 2D描画)";
            // 
            // btnToggleGrid
            // 
            this.btnToggleGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.btnToggleGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleGrid.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToggleGrid.ForeColor = System.Drawing.Color.Black;
            this.btnToggleGrid.Location = new System.Drawing.Point(535, 1);
            this.btnToggleGrid.Name = "btnToggleGrid";
            this.btnToggleGrid.Size = new System.Drawing.Size(70, 19);
            this.btnToggleGrid.TabIndex = 7;
            this.btnToggleGrid.Text = "📊 表形式";
            this.btnToggleGrid.UseVisualStyleBackColor = false;
            // 
            // btnToggle2D
            // 
            this.btnToggle2D.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnToggle2D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggle2D.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToggle2D.ForeColor = System.Drawing.Color.White;
            this.btnToggle2D.Location = new System.Drawing.Point(610, 1);
            this.btnToggle2D.Name = "btnToggle2D";
            this.btnToggle2D.Size = new System.Drawing.Size(70, 19);
            this.btnToggle2D.TabIndex = 8;
            this.btnToggle2D.Text = "🗺 2D図面";
            this.btnToggle2D.UseVisualStyleBackColor = false;
            // 
            // picEmbedded2D
            // 
            this.picEmbedded2D.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.picEmbedded2D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEmbedded2D.Location = new System.Drawing.Point(347, 21);
            this.picEmbedded2D.Name = "picEmbedded2D";
            this.picEmbedded2D.Size = new System.Drawing.Size(430, 244);
            this.picEmbedded2D.TabIndex = 9;
            this.picEmbedded2D.TabStop = false;
            this.picEmbedded2D.Visible = false;
            // 
            // btnOpenViewer
            // 
            this.btnOpenViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnOpenViewer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenViewer.Font = new System.Drawing.Font("Yu Gothic UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenViewer.ForeColor = System.Drawing.Color.White;
            this.btnOpenViewer.Location = new System.Drawing.Point(685, 1);
            this.btnOpenViewer.Name = "btnOpenViewer";
            this.btnOpenViewer.Size = new System.Drawing.Size(92, 19);
            this.btnOpenViewer.TabIndex = 6;
            this.btnOpenViewer.Text = "🔍 別窓拡大";
            this.btnOpenViewer.UseVisualStyleBackColor = false;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPoints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.lblPoints.Location = new System.Drawing.Point(347, 3);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(170, 15);
            this.lblPoints.TabIndex = 4;
            this.lblPoints.Text = "③ 座標データ (XYZ) / 2D描画";
            // 
            // dgvPoints
            // 
            this.dgvPoints.AllowUserToAddRows = false;
            this.dgvPoints.AllowUserToDeleteRows = false;
            this.dgvPoints.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPoints.Location = new System.Drawing.Point(347, 21);
            this.dgvPoints.Name = "dgvPoints";
            this.dgvPoints.ReadOnly = true;
            this.dgvPoints.RowHeadersVisible = false;
            this.dgvPoints.RowTemplate.Height = 22;
            this.dgvPoints.Size = new System.Drawing.Size(430, 244);
            this.dgvPoints.TabIndex = 5;
            // 
            // lblIkouLine
            // 
            this.lblIkouLine.AutoSize = true;
            this.lblIkouLine.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIkouLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.lblIkouLine.Location = new System.Drawing.Point(176, 3);
            this.lblIkouLine.Name = "lblIkouLine";
            this.lblIkouLine.Size = new System.Drawing.Size(111, 15);
            this.lblIkouLine.TabIndex = 2;
            this.lblIkouLine.Text = "② 遺構線名 (IKOULINE)";
            // 
            // lstIkouLine
            // 
            this.lstIkouLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.lstIkouLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstIkouLine.ForeColor = System.Drawing.Color.White;
            this.lstIkouLine.FormattingEnabled = true;
            this.lstIkouLine.ItemHeight = 15;
            this.lstIkouLine.Location = new System.Drawing.Point(176, 21);
            this.lstIkouLine.Name = "lstIkouLine";
            this.lstIkouLine.Size = new System.Drawing.Size(165, 244);
            this.lstIkouLine.TabIndex = 3;
            // 
            // lblIkou
            // 
            this.lblIkou.AutoSize = true;
            this.lblIkou.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIkou.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.lblIkou.Location = new System.Drawing.Point(5, 3);
            this.lblIkou.Name = "lblIkou";
            this.lblIkou.Size = new System.Drawing.Size(89, 15);
            this.lblIkou.TabIndex = 0;
            this.lblIkou.Text = "① 遺構名 (IKOU)";
            // 
            // lstIkou
            // 
            this.lstIkou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.lstIkou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstIkou.ForeColor = System.Drawing.Color.White;
            this.lstIkou.FormattingEnabled = true;
            this.lstIkou.ItemHeight = 15;
            this.lstIkou.Location = new System.Drawing.Point(5, 21);
            this.lstIkou.Name = "lstIkou";
            this.lstIkou.Size = new System.Drawing.Size(165, 244);
            this.lstIkou.TabIndex = 1;
            // 
            // grpAllIkouCanvas
            // 
            this.grpAllIkouCanvas.Controls.Add(this.picAllIkouCanvas);
            this.grpAllIkouCanvas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpAllIkouCanvas.Location = new System.Drawing.Point(12, 550);
            this.grpAllIkouCanvas.Name = "grpAllIkouCanvas";
            this.grpAllIkouCanvas.Size = new System.Drawing.Size(810, 310);
            this.grpAllIkouCanvas.TabIndex = 5;
            this.grpAllIkouCanvas.TabStop = false;
            this.grpAllIkouCanvas.Text = "🗺 全遺構データ描画 (All Features Site Plan - 選択遺構ハイライト表示)";
            // 
            // picAllIkouCanvas
            // 
            this.picAllIkouCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.picAllIkouCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAllIkouCanvas.Location = new System.Drawing.Point(10, 22);
            this.picAllIkouCanvas.Name = "picAllIkouCanvas";
            this.picAllIkouCanvas.Size = new System.Drawing.Size(790, 278);
            this.picAllIkouCanvas.TabIndex = 0;
            this.picAllIkouCanvas.TabStop = false;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyze.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAnalyze.ForeColor = System.Drawing.Color.White;
            this.btnAnalyze.Location = new System.Drawing.Point(12, 870);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(180, 35);
            this.btnAnalyze.TabIndex = 6;
            this.btnAnalyze.Text = "1. Test Connect && Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = false;
            // 
            // btnShowLog
            // 
            this.btnShowLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnShowLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowLog.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnShowLog.ForeColor = System.Drawing.Color.White;
            this.btnShowLog.Location = new System.Drawing.Point(470, 870);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(110, 35);
            this.btnShowLog.TabIndex = 7;
            this.btnShowLog.Text = "📋 ログ表示";
            this.btnShowLog.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(176)))), ((int)(((byte)(0)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(602, 870);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(220, 35);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "2. Site7 SQLite DB 変換出力";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // chkShiftJis
            // 
            this.chkShiftJis.AutoSize = true;
            this.chkShiftJis.Checked = true;
            this.chkShiftJis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShiftJis.ForeColor = System.Drawing.Color.White;
            this.chkShiftJis.Location = new System.Drawing.Point(205, 878);
            this.chkShiftJis.Name = "chkShiftJis";
            this.chkShiftJis.Size = new System.Drawing.Size(182, 19);
            this.chkShiftJis.TabIndex = 8;
            this.chkShiftJis.Text = "Japanese Excel Mode (Shift-JIS)";
            this.chkShiftJis.UseVisualStyleBackColor = true;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(12, 915);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(738, 18);
            this.pbProgress.TabIndex = 10;
            // 
            // lblProgressPercent
            // 
            this.lblProgressPercent.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProgressPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.lblProgressPercent.Location = new System.Drawing.Point(756, 915);
            this.lblProgressPercent.Name = "lblProgressPercent";
            this.lblProgressPercent.Size = new System.Drawing.Size(66, 18);
            this.lblProgressPercent.TabIndex = 11;
            this.lblProgressPercent.Text = "0%";
            this.lblProgressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(834, 945);
            this.Controls.Add(this.lblProgressPercent);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.chkShiftJis);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnShowLog);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.grpAllIkouCanvas);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this.grpSplit);
            this.Controls.Add(this.grpDbList);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SITE7 Data Migration Utility";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.grpDbList.ResumeLayout(false);
            this.grpSplit.ResumeLayout(false);
            this.grpSplit.PerformLayout();
            this.grpPreview.ResumeLayout(false);
            this.tabPreview.ResumeLayout(false);
            this.tabList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.tabDetail.ResumeLayout(false);
            this.tabDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmbedded2D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).EndInit();
            this.grpAllIkouCanvas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAllIkouCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.GroupBox grpDbList;
        private System.Windows.Forms.ListBox lstDbFolders;
        private System.Windows.Forms.Label lblSelectedDbStatus;
        private System.Windows.Forms.GroupBox grpSplit;
        private System.Windows.Forms.Label lblPriority1;
        private System.Windows.Forms.ComboBox cmbRule1;
        private System.Windows.Forms.TextBox txtRegexPattern1;
        private System.Windows.Forms.Label lblPriority2;
        private System.Windows.Forms.ComboBox cmbRule2;
        private System.Windows.Forms.TextBox txtRegexPattern2;
        private System.Windows.Forms.Label lblPriority3;
        private System.Windows.Forms.ComboBox cmbRule3;
        private System.Windows.Forms.TextBox txtRegexPattern3;
        private System.Windows.Forms.Label lblRegexHint;
        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.TabControl tabPreview;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.TabPage tabDetail;
        private System.Windows.Forms.Label lblIkou;
        private System.Windows.Forms.ListBox lstIkou;
        private System.Windows.Forms.Label lblIkouLine;
        private System.Windows.Forms.ListBox lstIkouLine;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.DataGridView dgvPoints;
        private System.Windows.Forms.Button btnToggleGrid;
        private System.Windows.Forms.Button btnToggle2D;
        private System.Windows.Forms.PictureBox picEmbedded2D;
        private System.Windows.Forms.Button btnOpenViewer;
        private System.Windows.Forms.GroupBox grpAllIkouCanvas;
        private System.Windows.Forms.PictureBox picAllIkouCanvas;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnShowLog;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.CheckBox chkShiftJis;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lblProgressPercent;
    }
}
