namespace Site7DbEditor
{
    partial class FormDefEnv
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
            this.SuspendLayout();

            // FormDefEnv
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(370, 520);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TS・GPS環境設定";
            this.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9.0F, System.Drawing.FontStyle.Regular);

            this.Load += new System.EventHandler(this.FormDefEnv_Load);

            this.ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.ComboBox CBSetPrism = null!;
        public System.Windows.Forms.TextBox TBPrismVal = null!;
        public System.Windows.Forms.ComboBox CBSokkyoMode = null!;
        public System.Windows.Forms.ComboBox CBTilt = null!;
        public System.Windows.Forms.ComboBox CBLightPat = null!;
        public System.Windows.Forms.ComboBox CBLightVal = null!;
        public System.Windows.Forms.TextBox TBSearchH = null!;
        public System.Windows.Forms.TextBox TBSearchV = null!;
        public System.Windows.Forms.ComboBox CBUseRC = null!;
        public System.Windows.Forms.ComboBox CBGuidLightPat = null!;
        public System.Windows.Forms.ComboBox CBGuidLightVal = null!;
        public System.Windows.Forms.ComboBox CBGPSHeight = null!;
        public System.Windows.Forms.TextBox TBGPSCount = null!;
        public System.Windows.Forms.ComboBox CBi93IMU = null!;
        public System.Windows.Forms.Button Do_Button = null!;
        public System.Windows.Forms.Button Cancel_Button = null!;
    }
}
