namespace Site7DbEditor
{
    partial class FormLeftPanelCtrl
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
            this.btnDockToPanel = new System.Windows.Forms.Button();
            this.panelLeftContent = new System.Windows.Forms.Panel();
            this.panelDockHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDockHeader
            // 
            this.panelDockHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.panelDockHeader.Controls.Add(this.btnDockToPanel);
            this.panelDockHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDockHeader.Location = new System.Drawing.Point(0, 0);
            this.panelDockHeader.Name = "panelDockHeader";
            this.panelDockHeader.Size = new System.Drawing.Size(130, 30);
            this.panelDockHeader.TabIndex = 1;
            // 
            // btnDockToPanel
            // 
            this.btnDockToPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(80)))));
            this.btnDockToPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDockToPanel.Font = new System.Drawing.Font("MS UI Gothic", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDockToPanel.ForeColor = System.Drawing.Color.White;
            this.btnDockToPanel.Location = new System.Drawing.Point(3, 3);
            this.btnDockToPanel.Name = "btnDockToPanel";
            this.btnDockToPanel.Size = new System.Drawing.Size(124, 24);
            this.btnDockToPanel.TabIndex = 0;
            this.btnDockToPanel.Text = "↙ ドック復帰";
            this.btnDockToPanel.UseVisualStyleBackColor = false;
            this.btnDockToPanel.Click += new System.EventHandler(this.btnDockToPanel_Click);
            // 
            // panelLeftContent
            // 
            this.panelLeftContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeftContent.Location = new System.Drawing.Point(0, 30);
            this.panelLeftContent.Name = "panelLeftContent";
            this.panelLeftContent.Size = new System.Drawing.Size(130, 405);
            this.panelLeftContent.TabIndex = 2;
            // 
            // FormLeftPanelCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(130, 480);
            this.Controls.Add(this.panelLeftContent);
            this.Controls.Add(this.panelDockHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormLeftPanelCtrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "レイヤ・表示設定";
            this.TopMost = true;
            this.panelDockHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDockHeader;
        public System.Windows.Forms.Button btnDockToPanel;
        public System.Windows.Forms.Panel panelLeftContent;
    }
}
