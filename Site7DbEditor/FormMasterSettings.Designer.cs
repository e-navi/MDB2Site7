namespace Site7DbEditor
{
    partial class FormMasterSettings
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblPathInfo = new System.Windows.Forms.Label();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.tabControlMasters = new System.Windows.Forms.TabControl();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.rdoSaveGenba = new System.Windows.Forms.RadioButton();
            this.rdoSaveSystem = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblHeader);
            this.panelTop.Controls.Add(this.lblPathInfo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(12, 8, 12, 4);
            this.panelTop.Size = new System.Drawing.Size(920, 56);
            this.panelTop.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblHeader.Location = new System.Drawing.Point(12, 6);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(235, 20);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "⚙ マスター設定・入力定義ファイル (Def)";
            // 
            // lblPathInfo
            // 
            this.lblPathInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPathInfo.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F);
            this.lblPathInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(190)))), ((int)(((byte)(205)))));
            this.lblPathInfo.Location = new System.Drawing.Point(12, 30);
            this.lblPathInfo.Name = "lblPathInfo";
            this.lblPathInfo.Size = new System.Drawing.Size(896, 20);
            this.lblPathInfo.TabIndex = 1;
            this.lblPathInfo.Text = "読み込み元: ";
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.tabControlMasters);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 56);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.panelCenter.Size = new System.Drawing.Size(920, 428);
            this.panelCenter.TabIndex = 1;
            // 
            // tabControlMasters
            // 
            this.tabControlMasters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMasters.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControlMasters.ItemSize = new System.Drawing.Size(150, 26);
            this.tabControlMasters.Location = new System.Drawing.Point(12, 4);
            this.tabControlMasters.Name = "tabControlMasters";
            this.tabControlMasters.SelectedIndex = 0;
            this.tabControlMasters.Size = new System.Drawing.Size(896, 420);
            this.tabControlMasters.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlMasters.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.rdoSaveGenba);
            this.panelBottom.Controls.Add(this.rdoSaveSystem);
            this.panelBottom.Controls.Add(this.btnSave);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 484);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.panelBottom.Size = new System.Drawing.Size(920, 56);
            this.panelBottom.TabIndex = 2;
            // 
            // rdoSaveGenba
            // 
            this.rdoSaveGenba.AutoSize = true;
            this.rdoSaveGenba.Checked = true;
            this.rdoSaveGenba.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.rdoSaveGenba.ForeColor = System.Drawing.Color.White;
            this.rdoSaveGenba.Location = new System.Drawing.Point(16, 8);
            this.rdoSaveGenba.Name = "rdoSaveGenba";
            this.rdoSaveGenba.Size = new System.Drawing.Size(201, 19);
            this.rdoSaveGenba.TabIndex = 0;
            this.rdoSaveGenba.TabStop = true;
            this.rdoSaveGenba.Text = "現在の現場専用として保存 ([現場]\\Def)";
            this.rdoSaveGenba.UseVisualStyleBackColor = true;
            // 
            // rdoSaveSystem
            // 
            this.rdoSaveSystem.AutoSize = true;
            this.rdoSaveSystem.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F);
            this.rdoSaveSystem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(220)))));
            this.rdoSaveSystem.Location = new System.Drawing.Point(16, 28);
            this.rdoSaveSystem.Name = "rdoSaveSystem";
            this.rdoSaveSystem.Size = new System.Drawing.Size(287, 19);
            this.rdoSaveSystem.TabIndex = 1;
            this.rdoSaveSystem.Text = "システム共通テンプレートとして保存 (C:\\SITE7\\...\\Def)";
            this.rdoSaveSystem.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(176)))), ((int)(((byte)(0)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(680, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "💾 保存して反映";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(80)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(812, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 34);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "閉じる";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // FormMasterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(920, 540);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.MinimumSize = new System.Drawing.Size(820, 460);
            this.Name = "FormMasterSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "マスター設定・入力定義 (Def)";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblPathInfo;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.TabControl tabControlMasters;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.RadioButton rdoSaveGenba;
        private System.Windows.Forms.RadioButton rdoSaveSystem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
