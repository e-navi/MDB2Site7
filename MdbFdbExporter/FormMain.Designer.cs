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
            this.lblPreset = new System.Windows.Forms.Label();
            this.cmbPreset = new System.Windows.Forms.ComboBox();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.btnDeletePreset = new System.Windows.Forms.Button();
            this.lblIgnoreWords = new System.Windows.Forms.Label();
            this.txtIgnoreWords = new System.Windows.Forms.TextBox();
            this.lblRegexHint = new System.Windows.Forms.Label();
            this.lblPriority4 = new System.Windows.Forms.Label();
            this.cmbRule4 = new System.Windows.Forms.ComboBox();
            this.txtRegexPattern4 = new System.Windows.Forms.TextBox();
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
            this.btnRawCsvExport = new System.Windows.Forms.Button();
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
            this.lblHeader.Location = new System.Drawing.Point(16, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(460, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Site7 データ移行ユーティリティ (MDB / FDB 変換)";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.panelHeader.Controls.Add(this.btnSettings);
            this.panelHeader.Controls.Add(this.lblSubHeader);
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 72);
            this.panelHeader.TabIndex = 1;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(860, 16);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(124, 38);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "⚙ 設定変更";
            this.btnSettings.UseVisualStyleBackColor = false;
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubHeader.Location = new System.Drawing.Point(18, 42);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(560, 19);
            this.lblSubHeader.TabIndex = 1;
            this.lblSubHeader.Text = "Access (MDB) / Firebird (FDB) から Site7 SQLite データベースへの遺物・遺構データ移行を行います。";
            // 
            // grpDbList
            // 
            this.grpDbList.BackColor = System.Drawing.Color.White;
            this.grpDbList.Controls.Add(this.lblSelectedDbStatus);
            this.grpDbList.Controls.Add(this.lstDbFolders);
            this.grpDbList.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpDbList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpDbList.Location = new System.Drawing.Point(16, 84);
            this.grpDbList.Name = "grpDbList";
            this.grpDbList.Size = new System.Drawing.Size(350, 205);
            this.grpDbList.TabIndex = 2;
            this.grpDbList.TabStop = false;
            this.grpDbList.Text = "DB 格納フォルダ一覧";
            // 
            // lblSelectedDbStatus
            // 
            this.lblSelectedDbStatus.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSelectedDbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSelectedDbStatus.Location = new System.Drawing.Point(10, 172);
            this.lblSelectedDbStatus.Name = "lblSelectedDbStatus";
            this.lblSelectedDbStatus.Size = new System.Drawing.Size(330, 24);
            this.lblSelectedDbStatus.TabIndex = 1;
            this.lblSelectedDbStatus.Text = "DBフォルダを選択してください";
            // 
            // lstDbFolders
            // 
            this.lstDbFolders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lstDbFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDbFolders.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstDbFolders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lstDbFolders.FormattingEnabled = true;
            this.lstDbFolders.ItemHeight = 20;
            this.lstDbFolders.Location = new System.Drawing.Point(10, 26);
            this.lstDbFolders.Name = "lstDbFolders";
            this.lstDbFolders.Size = new System.Drawing.Size(330, 142);
            this.lstDbFolders.TabIndex = 0;
            // 
            // grpSplit
            // 
            this.grpSplit.BackColor = System.Drawing.Color.White;
            this.grpSplit.Controls.Add(this.btnDeletePreset);
            this.grpSplit.Controls.Add(this.txtIgnoreWords);
            this.grpSplit.Controls.Add(this.lblIgnoreWords);
            this.grpSplit.Controls.Add(this.btnDeletePreset);
            this.grpSplit.Controls.Add(this.btnSavePreset);
            this.grpSplit.Controls.Add(this.cmbPreset);
            this.grpSplit.Controls.Add(this.lblPreset);
            this.grpSplit.Controls.Add(this.lblRegexHint);
            this.grpSplit.Controls.Add(this.txtRegexPattern4);
            this.grpSplit.Controls.Add(this.cmbRule4);
            this.grpSplit.Controls.Add(this.lblPriority4);
            this.grpSplit.Controls.Add(this.txtRegexPattern3);
            this.grpSplit.Controls.Add(this.cmbRule3);
            this.grpSplit.Controls.Add(this.lblPriority3);
            this.grpSplit.Controls.Add(this.txtRegexPattern2);
            this.grpSplit.Controls.Add(this.cmbRule2);
            this.grpSplit.Controls.Add(this.lblPriority2);
            this.grpSplit.Controls.Add(this.txtRegexPattern1);
            this.grpSplit.Controls.Add(this.cmbRule1);
            this.grpSplit.Controls.Add(this.lblPriority1);
            this.grpSplit.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpSplit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpSplit.Location = new System.Drawing.Point(376, 84);
            this.grpSplit.Name = "grpSplit";
            this.grpSplit.Size = new System.Drawing.Size(608, 205);
            this.grpSplit.TabIndex = 3;
            this.grpSplit.TabStop = false;
            this.grpSplit.Text = "遺構名 / 遺構線名 分割ルール (IKOU / IKOULINE)";
            // 
            // lblPreset
            // 
            this.lblPreset.AutoSize = true;
            this.lblPreset.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPreset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblPreset.Location = new System.Drawing.Point(12, 24);
            this.lblPreset.Name = "lblPreset";
            this.lblPreset.Size = new System.Drawing.Size(64, 19);
            this.lblPreset.TabIndex = 11;
            this.lblPreset.Text = "プリセット:";
            // 
            // cmbPreset
            // 
            this.cmbPreset.BackColor = System.Drawing.Color.White;
            this.cmbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPreset.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbPreset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cmbPreset.FormattingEnabled = true;
            this.cmbPreset.Location = new System.Drawing.Point(90, 21);
            this.cmbPreset.Name = "cmbPreset";
            this.cmbPreset.Size = new System.Drawing.Size(244, 25);
            this.cmbPreset.TabIndex = 12;
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSavePreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePreset.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSavePreset.ForeColor = System.Drawing.Color.White;
            this.btnSavePreset.Location = new System.Drawing.Point(340, 21);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(68, 25);
            this.btnSavePreset.TabIndex = 13;
            this.btnSavePreset.Text = "💾 登録";
            this.btnSavePreset.UseVisualStyleBackColor = false;
            // 
            // btnDeletePreset
            // 
            this.btnDeletePreset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDeletePreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePreset.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDeletePreset.ForeColor = System.Drawing.Color.White;
            this.btnDeletePreset.Location = new System.Drawing.Point(412, 21);
            this.btnDeletePreset.Name = "btnDeletePreset";
            this.btnDeletePreset.Size = new System.Drawing.Size(68, 25);
            this.btnDeletePreset.TabIndex = 14;
            this.btnDeletePreset.Text = "🗑 削除";
            this.btnDeletePreset.UseVisualStyleBackColor = false;
            // 
            // lblPriority1
            // 
            this.lblPriority1.AutoSize = true;
            this.lblPriority1.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPriority1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPriority1.Location = new System.Drawing.Point(12, 53);
            this.lblPriority1.Name = "lblPriority1";
            this.lblPriority1.Size = new System.Drawing.Size(63, 17);
            this.lblPriority1.TabIndex = 0;
            this.lblPriority1.Text = "優先度 1:";
            // 
            // cmbRule1
            // 
            this.cmbRule1.BackColor = System.Drawing.Color.White;
            this.cmbRule1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRule1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRule1.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbRule1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cmbRule1.FormattingEnabled = true;
            this.cmbRule1.Location = new System.Drawing.Point(90, 50);
            this.cmbRule1.Name = "cmbRule1";
            this.cmbRule1.Size = new System.Drawing.Size(244, 25);
            this.cmbRule1.TabIndex = 1;
            // 
            // txtRegexPattern1
            // 
            this.txtRegexPattern1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtRegexPattern1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegexPattern1.Enabled = false;
            this.txtRegexPattern1.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRegexPattern1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtRegexPattern1.Location = new System.Drawing.Point(340, 50);
            this.txtRegexPattern1.Name = "txtRegexPattern1";
            this.txtRegexPattern1.Size = new System.Drawing.Size(140, 24);
            this.txtRegexPattern1.TabIndex = 2;
            // 
            // lblPriority2
            // 
            this.lblPriority2.AutoSize = true;
            this.lblPriority2.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPriority2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPriority2.Location = new System.Drawing.Point(12, 82);
            this.lblPriority2.Name = "lblPriority2";
            this.lblPriority2.Size = new System.Drawing.Size(63, 17);
            this.lblPriority2.TabIndex = 0;
            this.lblPriority2.Text = "優先度 2:";
            // 
            // cmbRule2
            // 
            this.cmbRule2.BackColor = System.Drawing.Color.White;
            this.cmbRule2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRule2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRule2.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbRule2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cmbRule2.FormattingEnabled = true;
            this.cmbRule2.Location = new System.Drawing.Point(90, 79);
            this.cmbRule2.Name = "cmbRule2";
            this.cmbRule2.Size = new System.Drawing.Size(244, 25);
            this.cmbRule2.TabIndex = 1;
            // 
            // txtRegexPattern2
            // 
            this.txtRegexPattern2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtRegexPattern2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegexPattern2.Enabled = false;
            this.txtRegexPattern2.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRegexPattern2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtRegexPattern2.Location = new System.Drawing.Point(340, 79);
            this.txtRegexPattern2.Name = "txtRegexPattern2";
            this.txtRegexPattern2.Size = new System.Drawing.Size(140, 24);
            this.txtRegexPattern2.TabIndex = 2;
            // 
            // lblPriority3
            // 
            this.lblPriority3.AutoSize = true;
            this.lblPriority3.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPriority3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPriority3.Location = new System.Drawing.Point(12, 111);
            this.lblPriority3.Name = "lblPriority3";
            this.lblPriority3.Size = new System.Drawing.Size(63, 17);
            this.lblPriority3.TabIndex = 3;
            this.lblPriority3.Text = "優先度 3:";
            // 
            // cmbRule3
            // 
            this.cmbRule3.BackColor = System.Drawing.Color.White;
            this.cmbRule3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRule3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRule3.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbRule3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cmbRule3.FormattingEnabled = true;
            this.cmbRule3.Location = new System.Drawing.Point(90, 108);
            this.cmbRule3.Name = "cmbRule3";
            this.cmbRule3.Size = new System.Drawing.Size(244, 25);
            this.cmbRule3.TabIndex = 4;
            // 
            // txtRegexPattern3
            // 
            this.txtRegexPattern3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtRegexPattern3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegexPattern3.Enabled = false;
            this.txtRegexPattern3.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRegexPattern3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtRegexPattern3.Location = new System.Drawing.Point(340, 108);
            this.txtRegexPattern3.Name = "txtRegexPattern3";
            this.txtRegexPattern3.Size = new System.Drawing.Size(140, 24);
            this.txtRegexPattern3.TabIndex = 5;
            // 
            // lblPriority4
            // 
            this.lblPriority4.AutoSize = true;
            this.lblPriority4.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPriority4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPriority4.Location = new System.Drawing.Point(12, 140);
            this.lblPriority4.Name = "lblPriority4";
            this.lblPriority4.Size = new System.Drawing.Size(63, 17);
            this.lblPriority4.TabIndex = 6;
            this.lblPriority4.Text = "優先度 4:";
            // 
            // cmbRule4
            // 
            this.cmbRule4.BackColor = System.Drawing.Color.White;
            this.cmbRule4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRule4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRule4.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbRule4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cmbRule4.FormattingEnabled = true;
            this.cmbRule4.Location = new System.Drawing.Point(90, 137);
            this.cmbRule4.Name = "cmbRule4";
            this.cmbRule4.Size = new System.Drawing.Size(244, 25);
            this.cmbRule4.TabIndex = 7;
            // 
            // txtRegexPattern4
            // 
            this.txtRegexPattern4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtRegexPattern4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegexPattern4.Enabled = false;
            this.txtRegexPattern4.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRegexPattern4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtRegexPattern4.Location = new System.Drawing.Point(340, 137);
            this.txtRegexPattern4.Name = "txtRegexPattern4";
            this.txtRegexPattern4.Size = new System.Drawing.Size(140, 24);
            this.txtRegexPattern4.TabIndex = 8;
            // 
            // lblIgnoreWords
            // 
            this.lblIgnoreWords.AutoSize = true;
            this.lblIgnoreWords.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIgnoreWords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblIgnoreWords.Location = new System.Drawing.Point(12, 169);
            this.lblIgnoreWords.Name = "lblIgnoreWords";
            this.lblIgnoreWords.Size = new System.Drawing.Size(63, 17);
            this.lblIgnoreWords.TabIndex = 9;
            this.lblIgnoreWords.Text = "除外略称:";
            // 
            // txtIgnoreWords
            // 
            this.txtIgnoreWords.BackColor = System.Drawing.Color.White;
            this.txtIgnoreWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIgnoreWords.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtIgnoreWords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtIgnoreWords.Location = new System.Drawing.Point(90, 166);
            this.txtIgnoreWords.Name = "txtIgnoreWords";
            this.txtIgnoreWords.Size = new System.Drawing.Size(390, 24);
            this.txtIgnoreWords.TabIndex = 10;
            this.txtIgnoreWords.Text = "SK, SD, SI, SB, SX, SE, SP, SC, SH, SA, SR, SN, SM";
            // 
            // lblRegexHint
            // 
            this.lblRegexHint.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRegexHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRegexHint.Location = new System.Drawing.Point(490, 20);
            this.lblRegexHint.Name = "lblRegexHint";
            this.lblRegexHint.Size = new System.Drawing.Size(110, 175);
            this.lblRegexHint.TabIndex = 10;
            this.lblRegexHint.Text = "【グループ例】\r\n・略称保護:\r\n  SK,SD,SI...\r\n・区切り文字:\r\n  -,_,NS,U...\r\n・正規表現:\r\n  (?<ikou>...)\r\n  (?<ikouline>...)\r\n※優先度順に\r\n判定されます";

            // 
            // grpPreview
            // 
            this.grpPreview.BackColor = System.Drawing.Color.White;
            this.grpPreview.Controls.Add(this.tabPreview);
            this.grpPreview.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpPreview.Location = new System.Drawing.Point(16, 298);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.Size = new System.Drawing.Size(968, 330);
            this.grpPreview.TabIndex = 4;
            this.grpPreview.TabStop = false;
            this.grpPreview.Text = "データ解析 & プレビュー (Preview)";
            // 
            // tabPreview
            // 
            this.tabPreview.Controls.Add(this.tabList);
            this.tabPreview.Controls.Add(this.tabDetail);
            this.tabPreview.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabPreview.Location = new System.Drawing.Point(10, 26);
            this.tabPreview.Name = "tabPreview";
            this.tabPreview.SelectedIndex = 0;
            this.tabPreview.Size = new System.Drawing.Size(948, 314);
            this.tabPreview.TabIndex = 0;
            // 
            // tabList
            // 
            this.tabList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabList.Controls.Add(this.dgvPreview);
            this.tabList.Location = new System.Drawing.Point(4, 29);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(940, 281);
            this.tabList.TabIndex = 0;
            this.tabList.Text = "📋 グループ名分割一覧";
            // 
            // dgvPreview
            // 
            this.dgvPreview.AllowUserToAddRows = false;
            this.dgvPreview.AllowUserToDeleteRows = false;
            this.dgvPreview.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPreview.Location = new System.Drawing.Point(3, 3);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.RowHeadersVisible = false;
            this.dgvPreview.RowTemplate.Height = 30;
            this.dgvPreview.Size = new System.Drawing.Size(934, 275);
            this.dgvPreview.TabIndex = 0;
            // 
            // tabDetail
            // 
            this.tabDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
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
            this.tabDetail.Location = new System.Drawing.Point(4, 29);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetail.Size = new System.Drawing.Size(940, 281);
            this.tabDetail.TabIndex = 1;
            this.tabDetail.Text = "🔗 階層連動表示 (遺構 ➡ 遺構線 ➡ 座標データ / 2D描画)";
            // 
            // btnToggleGrid
            // 
            this.btnToggleGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnToggleGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleGrid.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToggleGrid.ForeColor = System.Drawing.Color.White;
            this.btnToggleGrid.Location = new System.Drawing.Point(676, 4);
            this.btnToggleGrid.Name = "btnToggleGrid";
            this.btnToggleGrid.Size = new System.Drawing.Size(80, 27);
            this.btnToggleGrid.TabIndex = 7;
            this.btnToggleGrid.Text = "📊 表形式";
            this.btnToggleGrid.UseVisualStyleBackColor = false;
            // 
            // btnToggle2D
            // 
            this.btnToggle2D.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnToggle2D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggle2D.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToggle2D.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnToggle2D.Location = new System.Drawing.Point(760, 4);
            this.btnToggle2D.Name = "btnToggle2D";
            this.btnToggle2D.Size = new System.Drawing.Size(80, 27);
            this.btnToggle2D.TabIndex = 8;
            this.btnToggle2D.Text = "🗺 2D図面";
            this.btnToggle2D.UseVisualStyleBackColor = false;
            // 
            // picEmbedded2D
            // 
            this.picEmbedded2D.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.picEmbedded2D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEmbedded2D.Location = new System.Drawing.Point(440, 34);
            this.picEmbedded2D.Name = "picEmbedded2D";
            this.picEmbedded2D.Size = new System.Drawing.Size(492, 240);
            this.picEmbedded2D.TabIndex = 9;
            this.picEmbedded2D.TabStop = false;
            this.picEmbedded2D.Visible = false;
            // 
            // btnOpenViewer
            // 
            this.btnOpenViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnOpenViewer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenViewer.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenViewer.ForeColor = System.Drawing.Color.White;
            this.btnOpenViewer.Location = new System.Drawing.Point(844, 4);
            this.btnOpenViewer.Name = "btnOpenViewer";
            this.btnOpenViewer.Size = new System.Drawing.Size(88, 27);
            this.btnOpenViewer.TabIndex = 6;
            this.btnOpenViewer.Text = "🔍 拡大表示";
            this.btnOpenViewer.UseVisualStyleBackColor = false;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPoints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPoints.Location = new System.Drawing.Point(440, 8);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(189, 19);
            this.lblPoints.TabIndex = 4;
            this.lblPoints.Text = "③ 座標データ (XYZ) / 2D描画";
            // 
            // dgvPoints
            // 
            this.dgvPoints.AllowUserToAddRows = false;
            this.dgvPoints.AllowUserToDeleteRows = false;
            this.dgvPoints.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvPoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPoints.Location = new System.Drawing.Point(440, 34);
            this.dgvPoints.Name = "dgvPoints";
            this.dgvPoints.ReadOnly = true;
            this.dgvPoints.RowHeadersVisible = false;
            this.dgvPoints.RowTemplate.Height = 26;
            this.dgvPoints.Size = new System.Drawing.Size(492, 240);
            this.dgvPoints.TabIndex = 5;
            // 
            // lblIkouLine
            // 
            this.lblIkouLine.AutoSize = true;
            this.lblIkouLine.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIkouLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblIkouLine.Location = new System.Drawing.Point(220, 8);
            this.lblIkouLine.Name = "lblIkouLine";
            this.lblIkouLine.Size = new System.Drawing.Size(149, 19);
            this.lblIkouLine.TabIndex = 2;
            this.lblIkouLine.Text = "② 遺構線名 (IKOULINE)";
            // 
            // lstIkouLine
            // 
            this.lstIkouLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lstIkouLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstIkouLine.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstIkouLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lstIkouLine.FormattingEnabled = true;
            this.lstIkouLine.ItemHeight = 20;
            this.lstIkouLine.Location = new System.Drawing.Point(220, 34);
            this.lstIkouLine.Name = "lstIkouLine";
            this.lstIkouLine.Size = new System.Drawing.Size(210, 242);
            this.lstIkouLine.TabIndex = 3;
            // 
            // lblIkou
            // 
            this.lblIkou.AutoSize = true;
            this.lblIkou.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIkou.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblIkou.Location = new System.Drawing.Point(6, 8);
            this.lblIkou.Name = "lblIkou";
            this.lblIkou.Size = new System.Drawing.Size(117, 19);
            this.lblIkou.TabIndex = 0;
            this.lblIkou.Text = "① 遺構名 (IKOU)";
            // 
            // lstIkou
            // 
            this.lstIkou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lstIkou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstIkou.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstIkou.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lstIkou.FormattingEnabled = true;
            this.lstIkou.ItemHeight = 20;
            this.lstIkou.Location = new System.Drawing.Point(6, 34);
            this.lstIkou.Name = "lstIkou";
            this.lstIkou.Size = new System.Drawing.Size(206, 242);
            this.lstIkou.TabIndex = 1;
            // 
            // grpAllIkouCanvas
            // 
            this.grpAllIkouCanvas.BackColor = System.Drawing.Color.White;
            this.grpAllIkouCanvas.Controls.Add(this.picAllIkouCanvas);
            this.grpAllIkouCanvas.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpAllIkouCanvas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpAllIkouCanvas.Location = new System.Drawing.Point(16, 604);
            this.grpAllIkouCanvas.Name = "grpAllIkouCanvas";
            this.grpAllIkouCanvas.Size = new System.Drawing.Size(968, 230);
            this.grpAllIkouCanvas.TabIndex = 5;
            this.grpAllIkouCanvas.TabStop = false;
            this.grpAllIkouCanvas.Text = "🗺 全遺構データ描画 (All Features Site Plan - 選択遺構ハイライト表示)";
            // 
            // picAllIkouCanvas
            // 
            this.picAllIkouCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.picAllIkouCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAllIkouCanvas.Location = new System.Drawing.Point(10, 26);
            this.picAllIkouCanvas.Name = "picAllIkouCanvas";
            this.picAllIkouCanvas.Size = new System.Drawing.Size(948, 192);
            this.picAllIkouCanvas.TabIndex = 0;
            this.picAllIkouCanvas.TabStop = false;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyze.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAnalyze.ForeColor = System.Drawing.Color.White;
            this.btnAnalyze.Location = new System.Drawing.Point(16, 846);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(220, 40);
            this.btnAnalyze.TabIndex = 6;
            this.btnAnalyze.Text = "🔍 1. 接続確認 && 解析実行";
            this.btnAnalyze.UseVisualStyleBackColor = false;
            // 
            // btnRawCsvExport
            // 
            this.btnRawCsvExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnRawCsvExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRawCsvExport.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRawCsvExport.ForeColor = System.Drawing.Color.White;
            this.btnRawCsvExport.Location = new System.Drawing.Point(440, 846);
            this.btnRawCsvExport.Name = "btnRawCsvExport";
            this.btnRawCsvExport.Size = new System.Drawing.Size(126, 40);
            this.btnRawCsvExport.TabIndex = 7;
            this.btnRawCsvExport.Text = "🧪 生CSV出力";
            this.btnRawCsvExport.UseVisualStyleBackColor = false;
            // 
            // btnShowLog
            // 
            this.btnShowLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnShowLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowLog.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnShowLog.ForeColor = System.Drawing.Color.White;
            this.btnShowLog.Location = new System.Drawing.Point(574, 846);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(126, 40);
            this.btnShowLog.TabIndex = 8;
            this.btnShowLog.Text = "📋 ログ表示";
            this.btnShowLog.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(710, 846);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(274, 40);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "🚀 2. Site7 SQLite DB 変換出力";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // chkShiftJis
            // 
            this.chkShiftJis.AutoSize = true;
            this.chkShiftJis.Checked = true;
            this.chkShiftJis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShiftJis.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkShiftJis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.chkShiftJis.Location = new System.Drawing.Point(240, 854);
            this.chkShiftJis.Name = "chkShiftJis";
            this.chkShiftJis.Size = new System.Drawing.Size(195, 23);
            this.chkShiftJis.TabIndex = 6;
            this.chkShiftJis.Text = "Shift-JIS 出力 (Excel互換)";
            this.chkShiftJis.UseVisualStyleBackColor = true;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(16, 896);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(880, 22);
            this.pbProgress.TabIndex = 10;
            // 
            // lblProgressPercent
            // 
            this.lblProgressPercent.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProgressPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblProgressPercent.Location = new System.Drawing.Point(904, 896);
            this.lblProgressPercent.Name = "lblProgressPercent";
            this.lblProgressPercent.Size = new System.Drawing.Size(80, 22);
            this.lblProgressPercent.TabIndex = 11;
            this.lblProgressPercent.Text = "0%";
            this.lblProgressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1000, 930);
            this.Controls.Add(this.lblProgressPercent);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.chkShiftJis);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnShowLog);
            this.Controls.Add(this.btnRawCsvExport);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.grpAllIkouCanvas);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this.grpSplit);
            this.Controls.Add(this.grpDbList);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Site7 データ移行ユーティリティ (MDB / FDB)";
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
        private System.Windows.Forms.Label lblPreset;
        private System.Windows.Forms.ComboBox cmbPreset;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Button btnDeletePreset;
        private System.Windows.Forms.Label lblPriority1;
        private System.Windows.Forms.ComboBox cmbRule1;
        private System.Windows.Forms.TextBox txtRegexPattern1;
        private System.Windows.Forms.Label lblPriority2;
        private System.Windows.Forms.ComboBox cmbRule2;
        private System.Windows.Forms.TextBox txtRegexPattern2;
        private System.Windows.Forms.Label lblPriority3;
        private System.Windows.Forms.ComboBox cmbRule3;
        private System.Windows.Forms.TextBox txtRegexPattern3;
        private System.Windows.Forms.Label lblPriority4;
        private System.Windows.Forms.ComboBox cmbRule4;
        private System.Windows.Forms.TextBox txtRegexPattern4;
        private System.Windows.Forms.Label lblIgnoreWords;
        private System.Windows.Forms.TextBox txtIgnoreWords;
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
        private System.Windows.Forms.Button btnRawCsvExport;
        private System.Windows.Forms.Button btnShowLog;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.CheckBox chkShiftJis;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lblProgressPercent;
    }
}
