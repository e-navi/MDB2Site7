namespace Site7DbEditor
{
    partial class FormBatchUpdate
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTable = new System.Windows.Forms.Label();
            this.cmbBatchTable = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cmbBatchFilterCol = new System.Windows.Forms.ComboBox();
            this.cmbBatchFilterOp = new System.Windows.Forms.ComboBox();
            this.txtBatchFilterVal = new System.Windows.Forms.TextBox();
            this.lblBatchPreviewCount = new System.Windows.Forms.Label();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.cmbBatchUpdateCol = new System.Windows.Forms.ComboBox();
            this.txtBatchUpdateVal = new System.Windows.Forms.TextBox();
            this.btnBatchExecute = new System.Windows.Forms.Button();
            this.dgvBatchPreview = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Yu Gothic UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.lblHeader.Location = new System.Drawing.Point(14, 12);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(193, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "⚡ 属性データ一括更新";
            // 
            // pnlTop
            // 
            this.pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.pnlTop.Controls.Add(this.lblTable);
            this.pnlTop.Controls.Add(this.cmbBatchTable);
            this.pnlTop.Controls.Add(this.lblFilter);
            this.pnlTop.Controls.Add(this.cmbBatchFilterCol);
            this.pnlTop.Controls.Add(this.cmbBatchFilterOp);
            this.pnlTop.Controls.Add(this.txtBatchFilterVal);
            this.pnlTop.Controls.Add(this.lblBatchPreviewCount);
            this.pnlTop.Controls.Add(this.lblUpdate);
            this.pnlTop.Controls.Add(this.cmbBatchUpdateCol);
            this.pnlTop.Controls.Add(this.txtBatchUpdateVal);
            this.pnlTop.Controls.Add(this.btnBatchExecute);
            this.pnlTop.Location = new System.Drawing.Point(14, 46);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(932, 136);
            this.pnlTop.TabIndex = 1;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.lblTable.Location = new System.Drawing.Point(14, 16);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(100, 21);
            this.lblTable.TabIndex = 0;
            this.lblTable.Text = "対象テーブル:";
            // 
            // cmbBatchTable
            // 
            this.cmbBatchTable.BackColor = System.Drawing.Color.White;
            this.cmbBatchTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchTable.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbBatchTable.ForeColor = System.Drawing.Color.Black;
            this.cmbBatchTable.FormattingEnabled = true;
            this.cmbBatchTable.Location = new System.Drawing.Point(120, 12);
            this.cmbBatchTable.Name = "cmbBatchTable";
            this.cmbBatchTable.Size = new System.Drawing.Size(200, 29);
            this.cmbBatchTable.TabIndex = 1;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.lblFilter.Location = new System.Drawing.Point(14, 56);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(78, 21);
            this.lblFilter.TabIndex = 2;
            this.lblFilter.Text = "条件設定:";
            // 
            // cmbBatchFilterCol
            // 
            this.cmbBatchFilterCol.BackColor = System.Drawing.Color.White;
            this.cmbBatchFilterCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchFilterCol.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbBatchFilterCol.ForeColor = System.Drawing.Color.Black;
            this.cmbBatchFilterCol.FormattingEnabled = true;
            this.cmbBatchFilterCol.Location = new System.Drawing.Point(120, 52);
            this.cmbBatchFilterCol.Name = "cmbBatchFilterCol";
            this.cmbBatchFilterCol.Size = new System.Drawing.Size(150, 29);
            this.cmbBatchFilterCol.TabIndex = 3;
            // 
            // cmbBatchFilterOp
            // 
            this.cmbBatchFilterOp.BackColor = System.Drawing.Color.White;
            this.cmbBatchFilterOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchFilterOp.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbBatchFilterOp.ForeColor = System.Drawing.Color.Black;
            this.cmbBatchFilterOp.FormattingEnabled = true;
            this.cmbBatchFilterOp.Location = new System.Drawing.Point(278, 52);
            this.cmbBatchFilterOp.Name = "cmbBatchFilterOp";
            this.cmbBatchFilterOp.Size = new System.Drawing.Size(190, 29);
            this.cmbBatchFilterOp.TabIndex = 4;
            // 
            // txtBatchFilterVal
            // 
            this.txtBatchFilterVal.BackColor = System.Drawing.Color.White;
            this.txtBatchFilterVal.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.txtBatchFilterVal.ForeColor = System.Drawing.Color.Black;
            this.txtBatchFilterVal.Location = new System.Drawing.Point(476, 52);
            this.txtBatchFilterVal.Name = "txtBatchFilterVal";
            this.txtBatchFilterVal.Size = new System.Drawing.Size(180, 29);
            this.txtBatchFilterVal.TabIndex = 5;
            // 
            // lblBatchPreviewCount
            // 
            this.lblBatchPreviewCount.AutoSize = true;
            this.lblBatchPreviewCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBatchPreviewCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblBatchPreviewCount.Location = new System.Drawing.Point(670, 56);
            this.lblBatchPreviewCount.Name = "lblBatchPreviewCount";
            this.lblBatchPreviewCount.Size = new System.Drawing.Size(117, 21);
            this.lblBatchPreviewCount.TabIndex = 6;
            this.lblBatchPreviewCount.Text = "対象件数: 0 件";
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.lblUpdate.Location = new System.Drawing.Point(14, 96);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(78, 21);
            this.lblUpdate.TabIndex = 7;
            this.lblUpdate.Text = "更新内容:";
            // 
            // cmbBatchUpdateCol
            // 
            this.cmbBatchUpdateCol.BackColor = System.Drawing.Color.White;
            this.cmbBatchUpdateCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatchUpdateCol.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbBatchUpdateCol.ForeColor = System.Drawing.Color.Black;
            this.cmbBatchUpdateCol.FormattingEnabled = true;
            this.cmbBatchUpdateCol.Location = new System.Drawing.Point(120, 92);
            this.cmbBatchUpdateCol.Name = "cmbBatchUpdateCol";
            this.cmbBatchUpdateCol.Size = new System.Drawing.Size(150, 29);
            this.cmbBatchUpdateCol.TabIndex = 8;
            // 
            // txtBatchUpdateVal
            // 
            this.txtBatchUpdateVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.txtBatchUpdateVal.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.txtBatchUpdateVal.ForeColor = System.Drawing.Color.Black;
            this.txtBatchUpdateVal.Location = new System.Drawing.Point(278, 92);
            this.txtBatchUpdateVal.Name = "txtBatchUpdateVal";
            this.txtBatchUpdateVal.Size = new System.Drawing.Size(240, 29);
            this.txtBatchUpdateVal.TabIndex = 9;
            // 
            // btnBatchExecute
            // 
            this.btnBatchExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBatchExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnBatchExecute.FlatAppearance.BorderSize = 0;
            this.btnBatchExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatchExecute.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBatchExecute.ForeColor = System.Drawing.Color.White;
            this.btnBatchExecute.Location = new System.Drawing.Point(728, 88);
            this.btnBatchExecute.Name = "btnBatchExecute";
            this.btnBatchExecute.Size = new System.Drawing.Size(190, 36);
            this.btnBatchExecute.TabIndex = 10;
            this.btnBatchExecute.Text = "⚡ 一括更新実行";
            this.btnBatchExecute.UseVisualStyleBackColor = false;
            // 
            // dgvBatchPreview
            // 
            this.dgvBatchPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBatchPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBatchPreview.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBatchPreview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBatchPreview.ColumnHeadersHeight = 32;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(229)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(30)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBatchPreview.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBatchPreview.EnableHeadersVisualStyles = false;
            this.dgvBatchPreview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(220)))), ((int)(((byte)(228)))));
            this.dgvBatchPreview.Location = new System.Drawing.Point(14, 192);
            this.dgvBatchPreview.Name = "dgvBatchPreview";
            this.dgvBatchPreview.ReadOnly = true;
            this.dgvBatchPreview.RowHeadersVisible = false;
            this.dgvBatchPreview.RowTemplate.Height = 28;
            this.dgvBatchPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBatchPreview.Size = new System.Drawing.Size(932, 344);
            this.dgvBatchPreview.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.btnClose.Location = new System.Drawing.Point(826, 550);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 38);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // FormBatchUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(960, 600);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvBatchPreview);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(900, 520);
            this.Name = "FormBatchUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "一括更新";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ComboBox cmbBatchTable;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cmbBatchFilterCol;
        private System.Windows.Forms.ComboBox cmbBatchFilterOp;
        private System.Windows.Forms.TextBox txtBatchFilterVal;
        private System.Windows.Forms.Label lblBatchPreviewCount;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.ComboBox cmbBatchUpdateCol;
        private System.Windows.Forms.TextBox txtBatchUpdateVal;
        private System.Windows.Forms.Button btnBatchExecute;
        private System.Windows.Forms.DataGridView dgvBatchPreview;
        private System.Windows.Forms.Button btnClose;
    }
}
