namespace Site7DbEditor
{
    partial class FormBottomPanelCtrl
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
            this.panelDockHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDockToPanel = new System.Windows.Forms.Button();
            this.panelBottomContent = new System.Windows.Forms.Panel();
            this.panelDockHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDockHeader
            // 
            this.panelDockHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.panelDockHeader.Controls.Add(this.lblTitle);
            this.panelDockHeader.Controls.Add(this.btnDockToPanel);
            this.panelDockHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDockHeader.Location = new System.Drawing.Point(0, 0);
            this.panelDockHeader.Name = "panelDockHeader";
            this.panelDockHeader.Size = new System.Drawing.Size(1480, 30);
            this.panelDockHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(10, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(155, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 データ・編集テーブル (分離ウィンドウ)";
            // 
            // btnDockToPanel
            // 
            this.btnDockToPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDockToPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(80)))));
            this.btnDockToPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDockToPanel.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnDockToPanel.ForeColor = System.Drawing.Color.White;
            this.btnDockToPanel.Location = new System.Drawing.Point(1350, 3);
            this.btnDockToPanel.Name = "btnDockToPanel";
            this.btnDockToPanel.Size = new System.Drawing.Size(124, 24);
            this.btnDockToPanel.TabIndex = 1;
            this.btnDockToPanel.Text = "↙ ドック復帰";
            this.btnDockToPanel.UseVisualStyleBackColor = false;
            this.btnDockToPanel.Click += new System.EventHandler(this.btnDockToPanel_Click);
            // 
            // panelBottomContent
            // 
            this.panelBottomContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomContent.Location = new System.Drawing.Point(0, 30);
            this.panelBottomContent.Name = "panelBottomContent";
            this.panelBottomContent.Size = new System.Drawing.Size(1480, 390);
            this.panelBottomContent.TabIndex = 1;
            // 
            // FormBottomPanelCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1480, 362);
            this.Controls.Add(this.panelBottomContent);
            this.Controls.Add(this.panelDockHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormBottomPanelCtrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "データ・編集テーブル (分離ウィンドウ)";
            this.panelDockHeader.ResumeLayout(false);
            this.panelDockHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.Panel panelDockHeader;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Button btnDockToPanel;
        public System.Windows.Forms.Panel panelBottomContent;
    }
}
