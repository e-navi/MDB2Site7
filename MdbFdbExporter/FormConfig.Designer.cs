namespace MdbFdbExporter
{
    partial class FormConfig
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
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpUserType = new System.Windows.Forms.GroupBox();
            this.rdoSite6 = new System.Windows.Forms.RadioButton();
            this.rdoSite5 = new System.Windows.Forms.RadioButton();
            this.grpFolders = new System.Windows.Forms.GroupBox();
            this.btnBrowseOut = new System.Windows.Forms.Button();
            this.txtOutFolder = new System.Windows.Forms.TextBox();
            this.lblOutFolder = new System.Windows.Forms.Label();
            this.btnBrowseDb = new System.Windows.Forms.Button();
            this.txtDbFolder = new System.Windows.Forms.TextBox();
            this.lblDbFolder = new System.Windows.Forms.Label();
            this.btnOpenConverter = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.grpUserType.SuspendLayout();
            this.grpFolders.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.panelHeader.Controls.Add(this.lblSubHeader);
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(680, 75);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubHeader.Location = new System.Drawing.Point(18, 44);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(325, 19);
            this.lblSubHeader.TabIndex = 1;
            this.lblSubHeader.Text = "システム動作モードとデータベース参照先フォルダの設定";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(16, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(365, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Site7 データ移行エクスポート設定";
            // 
            // grpUserType
            // 
            this.grpUserType.BackColor = System.Drawing.Color.White;
            this.grpUserType.Controls.Add(this.rdoSite6);
            this.grpUserType.Controls.Add(this.rdoSite5);
            this.grpUserType.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpUserType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpUserType.Location = new System.Drawing.Point(18, 90);
            this.grpUserType.Name = "grpUserType";
            this.grpUserType.Size = new System.Drawing.Size(644, 85);
            this.grpUserType.TabIndex = 1;
            this.grpUserType.TabStop = false;
            this.grpUserType.Text = "移行元システム (データ形式) の選択";
            // 
            // rdoSite6
            // 
            this.rdoSite6.AutoSize = true;
            this.rdoSite6.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdoSite6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.rdoSite6.Location = new System.Drawing.Point(320, 36);
            this.rdoSite6.Name = "rdoSite6";
            this.rdoSite6.Size = new System.Drawing.Size(248, 24);
            this.rdoSite6.TabIndex = 1;
            this.rdoSite6.Text = "Site6 形式 (Firebird FDB データベース)";
            this.rdoSite6.UseVisualStyleBackColor = true;
            // 
            // rdoSite5
            // 
            this.rdoSite5.AutoSize = true;
            this.rdoSite5.Checked = true;
            this.rdoSite5.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdoSite5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.rdoSite5.Location = new System.Drawing.Point(24, 36);
            this.rdoSite5.Name = "rdoSite5";
            this.rdoSite5.Size = new System.Drawing.Size(248, 24);
            this.rdoSite5.TabIndex = 0;
            this.rdoSite5.TabStop = true;
            this.rdoSite5.Text = "Site5 形式 (Access MDB データベース)";
            this.rdoSite5.UseVisualStyleBackColor = true;
            // 
            // grpFolders
            // 
            this.grpFolders.BackColor = System.Drawing.Color.White;
            this.grpFolders.Controls.Add(this.btnBrowseOut);
            this.grpFolders.Controls.Add(this.txtOutFolder);
            this.grpFolders.Controls.Add(this.lblOutFolder);
            this.grpFolders.Controls.Add(this.btnBrowseDb);
            this.grpFolders.Controls.Add(this.txtDbFolder);
            this.grpFolders.Controls.Add(this.lblDbFolder);
            this.grpFolders.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpFolders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpFolders.Location = new System.Drawing.Point(18, 190);
            this.grpFolders.Name = "grpFolders";
            this.grpFolders.Size = new System.Drawing.Size(644, 200);
            this.grpFolders.TabIndex = 2;
            this.grpFolders.TabStop = false;
            this.grpFolders.Text = "フォルダ設定";
            // 
            // btnBrowseOut
            // 
            this.btnBrowseOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBrowseOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseOut.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBrowseOut.ForeColor = System.Drawing.Color.White;
            this.btnBrowseOut.Location = new System.Drawing.Point(528, 138);
            this.btnBrowseOut.Name = "btnBrowseOut";
            this.btnBrowseOut.Size = new System.Drawing.Size(96, 32);
            this.btnBrowseOut.TabIndex = 5;
            this.btnBrowseOut.Text = "参照...";
            this.btnBrowseOut.UseVisualStyleBackColor = false;
            // 
            // txtOutFolder
            // 
            this.txtOutFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtOutFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutFolder.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOutFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtOutFolder.Location = new System.Drawing.Point(20, 140);
            this.txtOutFolder.Name = "txtOutFolder";
            this.txtOutFolder.Size = new System.Drawing.Size(496, 27);
            this.txtOutFolder.TabIndex = 4;
            // 
            // lblOutFolder
            // 
            this.lblOutFolder.AutoSize = true;
            this.lblOutFolder.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOutFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblOutFolder.Location = new System.Drawing.Point(20, 116);
            this.lblOutFolder.Name = "lblOutFolder";
            this.lblOutFolder.Size = new System.Drawing.Size(187, 19);
            this.lblOutFolder.TabIndex = 3;
            this.lblOutFolder.Text = "CSV / SQLite 出力先フォルダ:";
            // 
            // btnBrowseDb
            // 
            this.btnBrowseDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBrowseDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDb.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBrowseDb.ForeColor = System.Drawing.Color.White;
            this.btnBrowseDb.Location = new System.Drawing.Point(528, 62);
            this.btnBrowseDb.Name = "btnBrowseDb";
            this.btnBrowseDb.Size = new System.Drawing.Size(96, 32);
            this.btnBrowseDb.TabIndex = 2;
            this.btnBrowseDb.Text = "参照...";
            this.btnBrowseDb.UseVisualStyleBackColor = false;
            // 
            // txtDbFolder
            // 
            this.txtDbFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtDbFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDbFolder.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDbFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtDbFolder.Location = new System.Drawing.Point(20, 64);
            this.txtDbFolder.Name = "txtDbFolder";
            this.txtDbFolder.Size = new System.Drawing.Size(496, 27);
            this.txtDbFolder.TabIndex = 1;
            // 
            // lblDbFolder
            // 
            this.lblDbFolder.AutoSize = true;
            this.lblDbFolder.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDbFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDbFolder.Location = new System.Drawing.Point(20, 40);
            this.lblDbFolder.Name = "lblDbFolder";
            this.lblDbFolder.Size = new System.Drawing.Size(437, 19);
            this.lblDbFolder.TabIndex = 0;
            this.lblDbFolder.Text = "DB 格納親フォルダ (この配下に現場ごとのMDB/FDBフォルダが存在):";
            // 
            // btnOpenConverter
            // 
            this.btnOpenConverter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnOpenConverter.FlatAppearance.BorderSize = 0;
            this.btnOpenConverter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenConverter.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenConverter.ForeColor = System.Drawing.Color.White;
            this.btnOpenConverter.Location = new System.Drawing.Point(390, 410);
            this.btnOpenConverter.Name = "btnOpenConverter";
            this.btnOpenConverter.Size = new System.Drawing.Size(160, 42);
            this.btnOpenConverter.TabIndex = 3;
            this.btnOpenConverter.Text = "変換画面を開く ▶";
            this.btnOpenConverter.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(562, 410);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 42);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "終了";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // FormConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(680, 470);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOpenConverter);
            this.Controls.Add(this.grpFolders);
            this.Controls.Add(this.grpUserType);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Site7 データ移行エクスポート設定";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.grpUserType.ResumeLayout(false);
            this.grpUserType.PerformLayout();
            this.grpFolders.ResumeLayout(false);
            this.grpFolders.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.GroupBox grpUserType;
        private System.Windows.Forms.RadioButton rdoSite6;
        private System.Windows.Forms.RadioButton rdoSite5;
        private System.Windows.Forms.GroupBox grpFolders;
        private System.Windows.Forms.Button btnBrowseOut;
        private System.Windows.Forms.TextBox txtOutFolder;
        private System.Windows.Forms.Label lblOutFolder;
        private System.Windows.Forms.Button btnBrowseDb;
        private System.Windows.Forms.TextBox txtDbFolder;
        private System.Windows.Forms.Label lblDbFolder;
        private System.Windows.Forms.Button btnOpenConverter;
        private System.Windows.Forms.Button btnExit;
    }
}
