namespace MdbFdbExporter
{
    partial class FormIkouViewer
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSummary = new System.Windows.Forms.Label();
            this.cmbIkouSelect = new System.Windows.Forms.ComboBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerTop = new System.Windows.Forms.SplitContainer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.lblCanvasHeader = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvIkouPoints = new System.Windows.Forms.DataGridView();
            this.lblGridHeader = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.picCanvasAll = new System.Windows.Forms.PictureBox();
            this.lblBottomHeader = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTop)).BeginInit();
            this.splitContainerTop.Panel1.SuspendLayout();
            this.splitContainerTop.Panel2.SuspendLayout();
            this.splitContainerTop.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIkouPoints)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvasAll)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlHeader.Controls.Add(this.lblSummary);
            this.pnlHeader.Controls.Add(this.cmbIkouSelect);
            this.pnlHeader.Controls.Add(this.lblSelect);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 56);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSummary
            // 
            this.lblSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSummary.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(189)))), ((int)(((byte)(248)))));
            this.lblSummary.Location = new System.Drawing.Point(420, 14);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(565, 28);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "全 0 点 | 0 線 | Z: -";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbIkouSelect
            // 
            this.cmbIkouSelect.BackColor = System.Drawing.Color.White;
            this.cmbIkouSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIkouSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIkouSelect.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cmbIkouSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cmbIkouSelect.FormattingEnabled = true;
            this.cmbIkouSelect.Location = new System.Drawing.Point(190, 14);
            this.cmbIkouSelect.Name = "cmbIkouSelect";
            this.cmbIkouSelect.Size = new System.Drawing.Size(210, 28);
            this.cmbIkouSelect.TabIndex = 1;
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSelect.ForeColor = System.Drawing.Color.White;
            this.lblSelect.Location = new System.Drawing.Point(14, 17);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(167, 20);
            this.lblSelect.TabIndex = 0;
            this.lblSelect.Text = "表示する遺構名 (IKOU):";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 56);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerTop);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.pnlBottom);
            this.splitContainerMain.Size = new System.Drawing.Size(1000, 684);
            this.splitContainerMain.SplitterDistance = 350;
            this.splitContainerMain.TabIndex = 1;
            // 
            // splitContainerTop
            // 
            this.splitContainerTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTop.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTop.Name = "splitContainerTop";
            // 
            // splitContainerTop.Panel1
            // 
            this.splitContainerTop.Panel1.Controls.Add(this.pnlLeft);
            // 
            // splitContainerTop.Panel2
            // 
            this.splitContainerTop.Panel2.Controls.Add(this.pnlRight);
            this.splitContainerTop.Size = new System.Drawing.Size(1000, 350);
            this.splitContainerTop.SplitterDistance = 480;
            this.splitContainerTop.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.picCanvas);
            this.pnlLeft.Controls.Add(this.lblCanvasHeader);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10, 6, 6, 6);
            this.pnlLeft.Size = new System.Drawing.Size(480, 350);
            this.pnlLeft.TabIndex = 0;
            // 
            // picCanvas
            // 
            this.picCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.picCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCanvas.Location = new System.Drawing.Point(10, 32);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(464, 312);
            this.picCanvas.TabIndex = 1;
            this.picCanvas.TabStop = false;
            // 
            // lblCanvasHeader
            // 
            this.lblCanvasHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCanvasHeader.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCanvasHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblCanvasHeader.Location = new System.Drawing.Point(10, 6);
            this.lblCanvasHeader.Name = "lblCanvasHeader";
            this.lblCanvasHeader.Size = new System.Drawing.Size(464, 26);
            this.lblCanvasHeader.TabIndex = 0;
            this.lblCanvasHeader.Text = "🗺 2D平面配置描画 (X/Y Plan View)";
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.dgvIkouPoints);
            this.pnlRight.Controls.Add(this.lblGridHeader);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(6, 6, 10, 6);
            this.pnlRight.Size = new System.Drawing.Size(516, 350);
            this.pnlRight.TabIndex = 0;
            // 
            // dgvIkouPoints
            // 
            this.dgvIkouPoints.AllowUserToAddRows = false;
            this.dgvIkouPoints.AllowUserToDeleteRows = false;
            this.dgvIkouPoints.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvIkouPoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIkouPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIkouPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIkouPoints.Location = new System.Drawing.Point(6, 32);
            this.dgvIkouPoints.Name = "dgvIkouPoints";
            this.dgvIkouPoints.ReadOnly = true;
            this.dgvIkouPoints.RowHeadersVisible = false;
            this.dgvIkouPoints.RowTemplate.Height = 26;
            this.dgvIkouPoints.Size = new System.Drawing.Size(500, 312);
            this.dgvIkouPoints.TabIndex = 1;
            // 
            // lblGridHeader
            // 
            this.lblGridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridHeader.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGridHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblGridHeader.Location = new System.Drawing.Point(6, 6);
            this.lblGridHeader.Name = "lblGridHeader";
            this.lblGridHeader.Size = new System.Drawing.Size(500, 26);
            this.lblGridHeader.TabIndex = 0;
            this.lblGridHeader.Text = "📊 測量点座標データ一覧";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.picCanvasAll);
            this.pnlBottom.Controls.Add(this.lblBottomHeader);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(10, 6, 10, 10);
            this.pnlBottom.Size = new System.Drawing.Size(1000, 330);
            this.pnlBottom.TabIndex = 0;
            // 
            // picCanvasAll
            // 
            this.picCanvasAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.picCanvasAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCanvasAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCanvasAll.Location = new System.Drawing.Point(10, 32);
            this.picCanvasAll.Name = "picCanvasAll";
            this.picCanvasAll.Size = new System.Drawing.Size(980, 288);
            this.picCanvasAll.TabIndex = 1;
            this.picCanvasAll.TabStop = false;
            // 
            // lblBottomHeader
            // 
            this.lblBottomHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBottomHeader.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBottomHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblBottomHeader.Location = new System.Drawing.Point(10, 6);
            this.lblBottomHeader.Name = "lblBottomHeader";
            this.lblBottomHeader.Size = new System.Drawing.Size(980, 26);
            this.lblBottomHeader.TabIndex = 0;
            this.lblBottomHeader.Text = "🗺 全遺構データ描画 (All Features Overview)";
            // 
            // FormIkouViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1000, 740);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormIkouViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "遺構詳細ビューワ (Site7 プレビュー)";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerTop.Panel1.ResumeLayout(false);
            this.splitContainerTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTop)).EndInit();
            this.splitContainerTop.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIkouPoints)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvasAll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.ComboBox cmbIkouSelect;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerTop;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblCanvasHeader;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblGridHeader;
        private System.Windows.Forms.DataGridView dgvIkouPoints;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblBottomHeader;
        private System.Windows.Forms.PictureBox picCanvasAll;
    }
}
