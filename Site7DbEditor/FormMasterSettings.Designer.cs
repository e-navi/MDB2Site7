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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblPathInfo = new System.Windows.Forms.Label();
            this.tabControlMasters = new System.Windows.Forms.TabControl();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.rdoSaveGenba = new System.Windows.Forms.RadioButton();
            this.rdoSaveSystem = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblHeader.Location = new System.Drawing.Point(12, 10);
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
            this.lblPathInfo.Location = new System.Drawing.Point(12, 34);
            this.lblPathInfo.Name = "lblPathInfo";
            this.lblPathInfo.Size = new System.Drawing.Size(640, 20);
            this.lblPathInfo.TabIndex = 1;
            this.lblPathInfo.Text = "読み込み元: ";
            // 
            // tabControlMasters
            // 
            this.tabControlMasters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMasters.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControlMasters.Location = new System.Drawing.Point(12, 58);
            this.tabControlMasters.Name = "tabControlMasters";
            this.tabControlMasters.SelectedIndex = 0;
            this.tabControlMasters.Size = new System.Drawing.Size(640, 370);
            this.tabControlMasters.TabIndex = 2;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.rdoSaveGenba);
            this.panelBottom.Controls.Add(this.rdoSaveSystem);
            this.panelBottom.Controls.Add(this.btnSave);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 436);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(664, 55);
            this.panelBottom.TabIndex = 3;
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
            this.btnSave.Location = new System.Drawing.Point(664, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 32);
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
            this.btnCancel.Location = new System.Drawing.Point(784, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "閉じる";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // FormMasterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(880, 530);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.tabControlMasters);
            this.Controls.Add(this.lblPathInfo);
            this.Controls.Add(this.lblHeader);
            this.MinimumSize = new System.Drawing.Size(800, 450);
            this.Name = "FormMasterSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "マスター設定・入力定義 (Def)";
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblPathInfo;
        private System.Windows.Forms.TabControl tabControlMasters;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.RadioButton rdoSaveGenba;
        private System.Windows.Forms.RadioButton rdoSaveSystem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
