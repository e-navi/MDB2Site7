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
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.panelHeader.Controls.Add(this.lblSubHeader);
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(584, 65);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.lblSubHeader.Location = new System.Drawing.Point(14, 40);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(268, 15);
            this.lblSubHeader.TabIndex = 1;
            this.lblSubHeader.Text = "System Configuration & Database Path Settings";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Yu Gothic UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(294, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "SITE7 Data Migration Exporter";
            // 
            // grpUserType
            // 
            this.grpUserType.Controls.Add(this.rdoSite6);
            this.grpUserType.Controls.Add(this.rdoSite5);
            this.grpUserType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpUserType.Location = new System.Drawing.Point(15, 80);
            this.grpUserType.Name = "grpUserType";
            this.grpUserType.Size = new System.Drawing.Size(554, 70);
            this.grpUserType.TabIndex = 1;
            this.grpUserType.TabStop = false;
            this.grpUserType.Text = "Select User Type / Application";
            // 
            // rdoSite6
            // 
            this.rdoSite6.AutoSize = true;
            this.rdoSite6.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rdoSite6.ForeColor = System.Drawing.Color.White;
            this.rdoSite6.Location = new System.Drawing.Point(260, 30);
            this.rdoSite6.Name = "rdoSite6";
            this.rdoSite6.Size = new System.Drawing.Size(189, 21);
            this.rdoSite6.TabIndex = 1;
            this.rdoSite6.Text = "Site6 User (FDB Conversion)";
            this.rdoSite6.UseVisualStyleBackColor = true;
            // 
            // rdoSite5
            // 
            this.rdoSite5.AutoSize = true;
            this.rdoSite5.Checked = true;
            this.rdoSite5.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rdoSite5.ForeColor = System.Drawing.Color.White;
            this.rdoSite5.Location = new System.Drawing.Point(20, 30);
            this.rdoSite5.Name = "rdoSite5";
            this.rdoSite5.Size = new System.Drawing.Size(193, 21);
            this.rdoSite5.TabIndex = 0;
            this.rdoSite5.TabStop = true;
            this.rdoSite5.Text = "Site5 User (MDB Conversion)";
            this.rdoSite5.UseVisualStyleBackColor = true;
            // 
            // grpFolders
            // 
            this.grpFolders.Controls.Add(this.btnBrowseOut);
            this.grpFolders.Controls.Add(this.txtOutFolder);
            this.grpFolders.Controls.Add(this.lblOutFolder);
            this.grpFolders.Controls.Add(this.btnBrowseDb);
            this.grpFolders.Controls.Add(this.txtDbFolder);
            this.grpFolders.Controls.Add(this.lblDbFolder);
            this.grpFolders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.grpFolders.Location = new System.Drawing.Point(15, 160);
            this.grpFolders.Name = "grpFolders";
            this.grpFolders.Size = new System.Drawing.Size(554, 155);
            this.grpFolders.TabIndex = 2;
            this.grpFolders.TabStop = false;
            this.grpFolders.Text = "Folder Configuration";
            // 
            // btnBrowseOut
            // 
            this.btnBrowseOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnBrowseOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseOut.ForeColor = System.Drawing.Color.White;
            this.btnBrowseOut.Location = new System.Drawing.Point(455, 107);
            this.btnBrowseOut.Name = "btnBrowseOut";
            this.btnBrowseOut.Size = new System.Drawing.Size(85, 25);
            this.btnBrowseOut.TabIndex = 5;
            this.btnBrowseOut.Text = "Browse...";
            this.btnBrowseOut.UseVisualStyleBackColor = false;
            // 
            // txtOutFolder
            // 
            this.txtOutFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.txtOutFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutFolder.ForeColor = System.Drawing.Color.White;
            this.txtOutFolder.Location = new System.Drawing.Point(15, 108);
            this.txtOutFolder.Name = "txtOutFolder";
            this.txtOutFolder.Size = new System.Drawing.Size(430, 23);
            this.txtOutFolder.TabIndex = 4;
            // 
            // lblOutFolder
            // 
            this.lblOutFolder.AutoSize = true;
            this.lblOutFolder.ForeColor = System.Drawing.Color.White;
            this.lblOutFolder.Location = new System.Drawing.Point(15, 88);
            this.lblOutFolder.Name = "lblOutFolder";
            this.lblOutFolder.Size = new System.Drawing.Size(232, 15);
            this.lblOutFolder.TabIndex = 3;
            this.lblOutFolder.Text = "CSV Output Directory (CSV出力先フォルダ):";
            // 
            // btnBrowseDb
            // 
            this.btnBrowseDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnBrowseDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDb.ForeColor = System.Drawing.Color.White;
            this.btnBrowseDb.Location = new System.Drawing.Point(455, 45);
            this.btnBrowseDb.Name = "btnBrowseDb";
            this.btnBrowseDb.Size = new System.Drawing.Size(85, 25);
            this.btnBrowseDb.TabIndex = 2;
            this.btnBrowseDb.Text = "Browse...";
            this.btnBrowseDb.UseVisualStyleBackColor = false;
            // 
            // txtDbFolder
            // 
            this.txtDbFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.txtDbFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDbFolder.ForeColor = System.Drawing.Color.White;
            this.txtDbFolder.Location = new System.Drawing.Point(15, 46);
            this.txtDbFolder.Name = "txtDbFolder";
            this.txtDbFolder.Size = new System.Drawing.Size(430, 23);
            this.txtDbFolder.TabIndex = 1;
            // 
            // lblDbFolder
            // 
            this.lblDbFolder.AutoSize = true;
            this.lblDbFolder.ForeColor = System.Drawing.Color.White;
            this.lblDbFolder.Location = new System.Drawing.Point(15, 26);
            this.lblDbFolder.Name = "lblDbFolder";
            this.lblDbFolder.Size = new System.Drawing.Size(378, 15);
            this.lblDbFolder.TabIndex = 0;
            this.lblDbFolder.Text = "Database Store Root Folder (DB格納親フォルダ - 配下にDBフォルダ群):";
            // 
            // btnOpenConverter
            // 
            this.btnOpenConverter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.btnOpenConverter.FlatAppearance.BorderSize = 0;
            this.btnOpenConverter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenConverter.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOpenConverter.ForeColor = System.Drawing.Color.Black;
            this.btnOpenConverter.Location = new System.Drawing.Point(340, 330);
            this.btnOpenConverter.Name = "btnOpenConverter";
            this.btnOpenConverter.Size = new System.Drawing.Size(140, 35);
            this.btnOpenConverter.TabIndex = 3;
            this.btnOpenConverter.Text = "変換画面を開く ▶";
            this.btnOpenConverter.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(55)))), ((int)(((byte)(76)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(489, 330);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 35);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "終了";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(584, 380);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOpenConverter);
            this.Controls.Add(this.grpFolders);
            this.Controls.Add(this.grpUserType);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SITE7 Data Migration Exporter - System Configuration";
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
