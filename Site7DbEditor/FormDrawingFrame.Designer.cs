namespace Site7DbEditor
{
    partial class FormDrawingFrame
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
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.grpPaper = new System.Windows.Forms.GroupBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.rdoPortrait = new System.Windows.Forms.RadioButton();
            this.grpScale = new System.Windows.Forms.GroupBox();
            this.lblScalePrefix = new System.Windows.Forms.Label();
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.grpTransform = new System.Windows.Forms.GroupBox();
            this.lblCenterX = new System.Windows.Forms.Label();
            this.numCenterX = new System.Windows.Forms.NumericUpDown();
            this.lblCenterY = new System.Windows.Forms.Label();
            this.numCenterY = new System.Windows.Forms.NumericUpDown();
            this.lblRotation = new System.Windows.Forms.Label();
            this.numRotation = new System.Windows.Forms.NumericUpDown();
            this.btnMoveCenter = new System.Windows.Forms.Button();
            this.btnSetRotation = new System.Windows.Forms.Button();
            this.btnFitAll = new System.Windows.Forms.Button();
            this.btnResetRotation = new System.Windows.Forms.Button();
            this.grpMargin = new System.Windows.Forms.GroupBox();
            this.lblMarginL = new System.Windows.Forms.Label();
            this.numMarginL = new System.Windows.Forms.NumericUpDown();
            this.lblMarginR = new System.Windows.Forms.Label();
            this.numMarginR = new System.Windows.Forms.NumericUpDown();
            this.lblMarginT = new System.Windows.Forms.Label();
            this.numMarginT = new System.Windows.Forms.NumericUpDown();
            this.lblMarginB = new System.Windows.Forms.Label();
            this.numMarginB = new System.Windows.Forms.NumericUpDown();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpPaper.SuspendLayout();
            this.grpScale.SuspendLayout();
            this.grpTransform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRotation)).BeginInit();
            this.grpMargin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginB)).BeginInit();
            this.SuspendLayout();
            // 
            // chkVisible
            // 
            this.chkVisible.AutoSize = true;
            this.chkVisible.Checked = true;
            this.chkVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisible.Font = new System.Drawing.Font("Yu Gothic UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkVisible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.chkVisible.Location = new System.Drawing.Point(12, 10);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(124, 21);
            this.chkVisible.TabIndex = 0;
            this.chkVisible.Text = "全図図枠を表示";
            this.chkVisible.UseVisualStyleBackColor = true;
            // 
            // grpPaper
            // 
            this.grpPaper.Controls.Add(this.rdoPortrait);
            this.grpPaper.Controls.Add(this.rdoLandscape);
            this.grpPaper.Controls.Add(this.cmbPaperSize);
            this.grpPaper.Controls.Add(this.lblPaperSize);
            this.grpPaper.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpPaper.ForeColor = System.Drawing.Color.White;
            this.grpPaper.Location = new System.Drawing.Point(12, 36);
            this.grpPaper.Name = "grpPaper";
            this.grpPaper.Size = new System.Drawing.Size(260, 56);
            this.grpPaper.TabIndex = 1;
            this.grpPaper.TabStop = false;
            this.grpPaper.Text = "用紙設定";
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(8, 24);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(34, 15);
            this.lblPaperSize.TabIndex = 0;
            this.lblPaperSize.Text = "サイズ:";
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Items.AddRange(new object[] {
            "A4",
            "A3",
            "A2",
            "A1",
            "A0"});
            this.cmbPaperSize.Location = new System.Drawing.Point(48, 20);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(70, 21);
            this.cmbPaperSize.TabIndex = 1;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Checked = true;
            this.rdoLandscape.Location = new System.Drawing.Point(135, 22);
            this.rdoLandscape.Name = "rdoLandscape";
            this.rdoLandscape.Size = new System.Drawing.Size(37, 19);
            this.rdoLandscape.TabIndex = 2;
            this.rdoLandscape.TabStop = true;
            this.rdoLandscape.Text = "横";
            this.rdoLandscape.UseVisualStyleBackColor = true;
            // 
            // rdoPortrait
            // 
            this.rdoPortrait.AutoSize = true;
            this.rdoPortrait.Location = new System.Drawing.Point(185, 22);
            this.rdoPortrait.Name = "rdoPortrait";
            this.rdoPortrait.Size = new System.Drawing.Size(37, 19);
            this.rdoPortrait.TabIndex = 3;
            this.rdoPortrait.Text = "縦";
            this.rdoPortrait.UseVisualStyleBackColor = true;
            // 
            // grpScale
            // 
            this.grpScale.Controls.Add(this.cmbScale);
            this.grpScale.Controls.Add(this.lblScalePrefix);
            this.grpScale.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpScale.ForeColor = System.Drawing.Color.White;
            this.grpScale.Location = new System.Drawing.Point(12, 98);
            this.grpScale.Name = "grpScale";
            this.grpScale.Size = new System.Drawing.Size(260, 52);
            this.grpScale.TabIndex = 2;
            this.grpScale.TabStop = false;
            this.grpScale.Text = "図面縮尺";
            // 
            // lblScalePrefix
            // 
            this.lblScalePrefix.AutoSize = true;
            this.lblScalePrefix.Location = new System.Drawing.Point(8, 22);
            this.lblScalePrefix.Name = "lblScalePrefix";
            this.lblScalePrefix.Size = new System.Drawing.Size(33, 15);
            this.lblScalePrefix.TabIndex = 0;
            this.lblScalePrefix.Text = "縮尺: 1 /";
            // 
            // cmbScale
            // 
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.Items.AddRange(new object[] {
            "50",
            "100",
            "200",
            "250",
            "300",
            "500",
            "1000",
            "2000",
            "5000"});
            this.cmbScale.Location = new System.Drawing.Point(68, 19);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(110, 21);
            this.cmbScale.TabIndex = 1;
            // 
            // grpTransform
            // 
            this.grpTransform.Controls.Add(this.btnResetRotation);
            this.grpTransform.Controls.Add(this.btnFitAll);
            this.grpTransform.Controls.Add(this.btnMoveCenter);
            this.grpTransform.Controls.Add(this.btnSetRotation);
            this.grpTransform.Controls.Add(this.numRotation);
            this.grpTransform.Controls.Add(this.lblRotation);
            this.grpTransform.Controls.Add(this.numCenterY);
            this.grpTransform.Controls.Add(this.lblCenterY);
            this.grpTransform.Controls.Add(this.numCenterX);
            this.grpTransform.Controls.Add(this.lblCenterX);
            this.grpTransform.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpTransform.ForeColor = System.Drawing.Color.White;
            this.grpTransform.Location = new System.Drawing.Point(12, 156);
            this.grpTransform.Name = "grpTransform";
            this.grpTransform.Size = new System.Drawing.Size(260, 175);
            this.grpTransform.TabIndex = 3;
            this.grpTransform.TabStop = false;
            this.grpTransform.Text = "中心座標 & 回転";
            // 
            // lblCenterX
            // 
            this.lblCenterX.AutoSize = true;
            this.lblCenterX.Location = new System.Drawing.Point(8, 22);
            this.lblCenterX.Name = "lblCenterX";
            this.lblCenterX.Size = new System.Drawing.Size(43, 15);
            this.lblCenterX.TabIndex = 0;
            this.lblCenterX.Text = "北 X(m):";
            // 
            // numCenterX
            // 
            this.numCenterX.DecimalPlaces = 3;
            this.numCenterX.Location = new System.Drawing.Point(62, 19);
            this.numCenterX.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numCenterX.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numCenterX.Name = "numCenterX";
            this.numCenterX.Size = new System.Drawing.Size(185, 23);
            this.numCenterX.TabIndex = 1;
            this.numCenterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCenterY
            // 
            this.lblCenterY.AutoSize = true;
            this.lblCenterY.Location = new System.Drawing.Point(8, 48);
            this.lblCenterY.Name = "lblCenterY";
            this.lblCenterY.Size = new System.Drawing.Size(42, 15);
            this.lblCenterY.TabIndex = 2;
            this.lblCenterY.Text = "東 Y(m):";
            // 
            // numCenterY
            // 
            this.numCenterY.DecimalPlaces = 3;
            this.numCenterY.Location = new System.Drawing.Point(62, 45);
            this.numCenterY.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numCenterY.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.numCenterY.Name = "numCenterY";
            this.numCenterY.Size = new System.Drawing.Size(185, 23);
            this.numCenterY.TabIndex = 3;
            this.numCenterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRotation
            // 
            this.lblRotation.AutoSize = true;
            this.lblRotation.Location = new System.Drawing.Point(8, 74);
            this.lblRotation.Name = "lblRotation";
            this.lblRotation.Size = new System.Drawing.Size(46, 15);
            this.lblRotation.TabIndex = 4;
            this.lblRotation.Text = "回転(°):";
            // 
            // numRotation
            // 
            this.numRotation.DecimalPlaces = 2;
            this.numRotation.Location = new System.Drawing.Point(62, 71);
            this.numRotation.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numRotation.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numRotation.Name = "numRotation";
            this.numRotation.Size = new System.Drawing.Size(120, 23);
            this.numRotation.TabIndex = 5;
            this.numRotation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnResetRotation
            // 
            this.btnResetRotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(65)))), ((int)(((byte)(80)))));
            this.btnResetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetRotation.Font = new System.Drawing.Font("Yu Gothic UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnResetRotation.Location = new System.Drawing.Point(188, 71);
            this.btnResetRotation.Name = "btnResetRotation";
            this.btnResetRotation.Size = new System.Drawing.Size(59, 23);
            this.btnResetRotation.TabIndex = 6;
            this.btnResetRotation.Text = "0°リセット";
            this.btnResetRotation.UseVisualStyleBackColor = false;
            // 
            // btnMoveCenter
            // 
            this.btnMoveCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnMoveCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveCenter.Location = new System.Drawing.Point(11, 102);
            this.btnMoveCenter.Name = "btnMoveCenter";
            this.btnMoveCenter.Size = new System.Drawing.Size(112, 28);
            this.btnMoveCenter.TabIndex = 7;
            this.btnMoveCenter.Text = "📍 中心移動";
            this.btnMoveCenter.UseVisualStyleBackColor = false;
            // 
            // btnSetRotation
            // 
            this.btnSetRotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.btnSetRotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetRotation.Location = new System.Drawing.Point(135, 102);
            this.btnSetRotation.Name = "btnSetRotation";
            this.btnSetRotation.Size = new System.Drawing.Size(112, 28);
            this.btnSetRotation.TabIndex = 8;
            this.btnSetRotation.Text = "🔄 回転指定";
            this.btnSetRotation.UseVisualStyleBackColor = false;
            // 
            // btnFitAll
            // 
            this.btnFitAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(140)))), ((int)(((byte)(90)))));
            this.btnFitAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFitAll.Location = new System.Drawing.Point(11, 136);
            this.btnFitAll.Name = "btnFitAll";
            this.btnFitAll.Size = new System.Drawing.Size(236, 28);
            this.btnFitAll.TabIndex = 9;
            this.btnFitAll.Text = "🔍 現場全体にフィット";
            this.btnFitAll.UseVisualStyleBackColor = false;
            // 
            // grpMargin
            // 
            this.grpMargin.Controls.Add(this.numMarginB);
            this.grpMargin.Controls.Add(this.lblMarginB);
            this.grpMargin.Controls.Add(this.numMarginT);
            this.grpMargin.Controls.Add(this.lblMarginT);
            this.grpMargin.Controls.Add(this.numMarginR);
            this.grpMargin.Controls.Add(this.lblMarginR);
            this.grpMargin.Controls.Add(this.numMarginL);
            this.grpMargin.Controls.Add(this.lblMarginL);
            this.grpMargin.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMargin.ForeColor = System.Drawing.Color.White;
            this.grpMargin.Location = new System.Drawing.Point(12, 338);
            this.grpMargin.Name = "grpMargin";
            this.grpMargin.Size = new System.Drawing.Size(260, 80);
            this.grpMargin.TabIndex = 4;
            this.grpMargin.TabStop = false;
            this.grpMargin.Text = "内枠余白 (mm)";
            // 
            // lblMarginL
            // 
            this.lblMarginL.AutoSize = true;
            this.lblMarginL.Location = new System.Drawing.Point(8, 22);
            this.lblMarginL.Name = "lblMarginL";
            this.lblMarginL.Size = new System.Drawing.Size(22, 15);
            this.lblMarginL.TabIndex = 0;
            this.lblMarginL.Text = "左:";
            // 
            // numMarginL
            // 
            this.numMarginL.Location = new System.Drawing.Point(32, 20);
            this.numMarginL.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginL.Name = "numMarginL";
            this.numMarginL.Size = new System.Drawing.Size(50, 23);
            this.numMarginL.TabIndex = 1;
            this.numMarginL.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblMarginR
            // 
            this.lblMarginR.AutoSize = true;
            this.lblMarginR.Location = new System.Drawing.Point(92, 22);
            this.lblMarginR.Name = "lblMarginR";
            this.lblMarginR.Size = new System.Drawing.Size(22, 15);
            this.lblMarginR.TabIndex = 2;
            this.lblMarginR.Text = "右:";
            // 
            // numMarginR
            // 
            this.numMarginR.Location = new System.Drawing.Point(116, 20);
            this.numMarginR.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginR.Name = "numMarginR";
            this.numMarginR.Size = new System.Drawing.Size(50, 23);
            this.numMarginR.TabIndex = 3;
            this.numMarginR.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblMarginT
            // 
            this.lblMarginT.AutoSize = true;
            this.lblMarginT.Location = new System.Drawing.Point(8, 50);
            this.lblMarginT.Name = "lblMarginT";
            this.lblMarginT.Size = new System.Drawing.Size(22, 15);
            this.lblMarginT.TabIndex = 4;
            this.lblMarginT.Text = "上:";
            // 
            // numMarginT
            // 
            this.numMarginT.Location = new System.Drawing.Point(32, 48);
            this.numMarginT.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginT.Name = "numMarginT";
            this.numMarginT.Size = new System.Drawing.Size(50, 23);
            this.numMarginT.TabIndex = 5;
            this.numMarginT.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblMarginB
            // 
            this.lblMarginB.AutoSize = true;
            this.lblMarginB.Location = new System.Drawing.Point(92, 50);
            this.lblMarginB.Name = "lblMarginB";
            this.lblMarginB.Size = new System.Drawing.Size(22, 15);
            this.lblMarginB.TabIndex = 6;
            this.lblMarginB.Text = "下:";
            // 
            // numMarginB
            // 
            this.numMarginB.Location = new System.Drawing.Point(116, 48);
            this.numMarginB.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMarginB.Name = "numMarginB";
            this.numMarginB.Size = new System.Drawing.Size(50, 23);
            this.numMarginB.TabIndex = 7;
            this.numMarginB.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(65)))), ((int)(((byte)(80)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(167, 424);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 28);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // FormDrawingFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(284, 460);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpMargin);
            this.Controls.Add(this.grpTransform);
            this.Controls.Add(this.grpScale);
            this.Controls.Add(this.grpPaper);
            this.Controls.Add(this.chkVisible);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormDrawingFrame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "全図・図枠設定";
            this.TopMost = true;
            this.grpPaper.ResumeLayout(false);
            this.grpPaper.PerformLayout();
            this.grpScale.ResumeLayout(false);
            this.grpScale.PerformLayout();
            this.grpTransform.ResumeLayout(false);
            this.grpTransform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRotation)).EndInit();
            this.grpMargin.ResumeLayout(false);
            this.grpMargin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMarginB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.GroupBox grpPaper;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.RadioButton rdoLandscape;
        private System.Windows.Forms.RadioButton rdoPortrait;
        private System.Windows.Forms.GroupBox grpScale;
        private System.Windows.Forms.Label lblScalePrefix;
        private System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.GroupBox grpTransform;
        private System.Windows.Forms.Label lblCenterX;
        private System.Windows.Forms.NumericUpDown numCenterX;
        private System.Windows.Forms.Label lblCenterY;
        private System.Windows.Forms.NumericUpDown numCenterY;
        private System.Windows.Forms.Label lblRotation;
        private System.Windows.Forms.NumericUpDown numRotation;
        private System.Windows.Forms.Button btnResetRotation;
        public System.Windows.Forms.Button btnMoveCenter;
        public System.Windows.Forms.Button btnSetRotation;
        private System.Windows.Forms.Button btnFitAll;
        private System.Windows.Forms.GroupBox grpMargin;
        private System.Windows.Forms.Label lblMarginL;
        private System.Windows.Forms.NumericUpDown numMarginL;
        private System.Windows.Forms.Label lblMarginR;
        private System.Windows.Forms.NumericUpDown numMarginR;
        private System.Windows.Forms.Label lblMarginT;
        private System.Windows.Forms.NumericUpDown numMarginT;
        private System.Windows.Forms.Label lblMarginB;
        private System.Windows.Forms.NumericUpDown numMarginB;
        private System.Windows.Forms.Button btnClose;
    }
}
