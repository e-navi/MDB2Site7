namespace Site7DbEditor
{
    partial class FormKikaiDef
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();

            // FormKikaiDef
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(336, 410);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "器械点・後視点設定 (既知2点)";
            this.TopMost = true;
            this.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Regular);

            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormKikaiDef_FormClosed);
            this.Shown += new System.EventHandler(this.FormKikaiDef_Shown);
            this.VisibleChanged += new System.EventHandler(this.FormKikaiDef_VisibleChanged);

            this.ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.ComboBox CBSelKikaiP = null!;
        public System.Windows.Forms.ComboBox CBSelBackP = null!;
        private System.Windows.Forms.Button buttonMesure01 = null!;
        private System.Windows.Forms.Button button1 = null!;
        private System.Windows.Forms.Button button2 = null!;
        internal System.Windows.Forms.Label L_Len1 = null!;
        internal System.Windows.Forms.Label L_Len2 = null!;
        internal System.Windows.Forms.Label L_Len3 = null!;
        private System.Windows.Forms.Timer timer1 = null!;
    }
}
