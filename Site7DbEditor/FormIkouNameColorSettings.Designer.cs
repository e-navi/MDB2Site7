namespace Site7DbEditor
{
    partial class FormIkouNameColorSettings
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.grpEdit = new System.Windows.Forms.GroupBox();
            this.lblPattern = new System.Windows.Forms.Label();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.CBoxColor = new System.Windows.Forms.ComboBox();
            this.lblColorSample = new System.Windows.Forms.Label();
            this.lblNotice = new System.Windows.Forms.Label();
            this.btnExportToMaster = new System.Windows.Forms.Button();
            this.btnImportFromMaster = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.grpEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(35)))), ((int)(((byte)(65)))));
            this.lblTitle.Location = new System.Drawing.Point(14, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(356, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "遺構名・プレフィックス別 表示色定義 (遺構名色.txt)";
            // 
            // listBox1
            // 
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 28;
            this.listBox1.Location = new System.Drawing.Point(14, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(250, 310);
            this.listBox1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Location = new System.Drawing.Point(14, 355);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 32);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "➕ 追加";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Location = new System.Drawing.Point(84, 355);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 32);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "➖ 削除";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.BackColor = System.Drawing.Color.White;
            this.btnMoveUp.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMoveUp.Location = new System.Drawing.Point(154, 355);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(52, 32);
            this.btnMoveUp.TabIndex = 4;
            this.btnMoveUp.Text = "▲";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.BackColor = System.Drawing.Color.White;
            this.btnMoveDown.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMoveDown.Location = new System.Drawing.Point(210, 355);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(54, 32);
            this.btnMoveDown.TabIndex = 5;
            this.btnMoveDown.Text = "▼";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            // 
            // grpEdit
            // 
            this.grpEdit.BackColor = System.Drawing.Color.White;
            this.grpEdit.Controls.Add(this.lblNotice);
            this.grpEdit.Controls.Add(this.lblColorSample);
            this.grpEdit.Controls.Add(this.CBoxColor);
            this.grpEdit.Controls.Add(this.lblColor);
            this.grpEdit.Controls.Add(this.txtPattern);
            this.grpEdit.Controls.Add(this.lblPattern);
            this.grpEdit.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.grpEdit.Location = new System.Drawing.Point(276, 38);
            this.grpEdit.Name = "grpEdit";
            this.grpEdit.Size = new System.Drawing.Size(328, 350);
            this.grpEdit.TabIndex = 6;
            this.grpEdit.TabStop = false;
            this.grpEdit.Text = "遺構色設定";
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblPattern.Location = new System.Drawing.Point(14, 28);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(164, 21);
            this.lblPattern.TabIndex = 0;
            this.lblPattern.Text = "遺構名 / プレフィックス:";
            // 
            // txtPattern
            // 
            this.txtPattern.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtPattern.Location = new System.Drawing.Point(14, 54);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(298, 29);
            this.txtPattern.TabIndex = 1;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblColor.Location = new System.Drawing.Point(14, 98);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(61, 21);
            this.lblColor.TabIndex = 2;
            this.lblColor.Text = "表示色:";
            // 
            // CBoxColor
            // 
            this.CBoxColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxColor.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBoxColor.FormattingEnabled = true;
            this.CBoxColor.ItemHeight = 23;
            this.CBoxColor.Location = new System.Drawing.Point(14, 124);
            this.CBoxColor.Name = "CBoxColor";
            this.CBoxColor.Size = new System.Drawing.Size(218, 29);
            this.CBoxColor.TabIndex = 3;
            // 
            // lblColorSample
            // 
            this.lblColorSample.BackColor = System.Drawing.Color.Red;
            this.lblColorSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblColorSample.Location = new System.Drawing.Point(238, 124);
            this.lblColorSample.Name = "lblColorSample";
            this.lblColorSample.Size = new System.Drawing.Size(74, 29);
            this.lblColorSample.TabIndex = 4;
            // 
            // lblNotice
            // 
            this.lblNotice.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F);
            this.lblNotice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblNotice.Location = new System.Drawing.Point(14, 175);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(298, 155);
            this.lblNotice.TabIndex = 5;
            this.lblNotice.Text = "※ 該当しない遺構名は「最終行」の色が自動適用されます。\r\n※ 上端・中・下端の濃淡はレイヤ設定の指定値で自動反映されます。\r\n※ プレフィックス指定（例: 'SB'）で前方一致判定します。";
            // 
            // btnExportToMaster
            // 
            this.btnExportToMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.btnExportToMaster.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.btnExportToMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToMaster.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExportToMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.btnExportToMaster.Location = new System.Drawing.Point(14, 408);
            this.btnExportToMaster.Name = "btnExportToMaster";
            this.btnExportToMaster.Size = new System.Drawing.Size(135, 36);
            this.btnExportToMaster.TabIndex = 7;
            this.btnExportToMaster.Text = "📤 マスターへ反映";
            this.btnExportToMaster.UseVisualStyleBackColor = false;
            // 
            // btnImportFromMaster
            // 
            this.btnImportFromMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.btnImportFromMaster.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.btnImportFromMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromMaster.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnImportFromMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.btnImportFromMaster.Location = new System.Drawing.Point(155, 408);
            this.btnImportFromMaster.Name = "btnImportFromMaster";
            this.btnImportFromMaster.Size = new System.Drawing.Size(145, 36);
            this.btnImportFromMaster.TabIndex = 8;
            this.btnImportFromMaster.Text = "📥 マスターから反映";
            this.btnImportFromMaster.UseVisualStyleBackColor = false;
            // 
            // Save_Button
            // 
            this.Save_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.Save_Button.FlatAppearance.BorderSize = 0;
            this.Save_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_Button.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.Save_Button.ForeColor = System.Drawing.Color.White;
            this.Save_Button.Location = new System.Drawing.Point(364, 406);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(138, 40);
            this.Save_Button.TabIndex = 9;
            this.Save_Button.Text = "💾 設定を保存";
            this.Save_Button.UseVisualStyleBackColor = false;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.Cancel_Button.FlatAppearance.BorderSize = 0;
            this.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_Button.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.Cancel_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.Cancel_Button.Location = new System.Drawing.Point(510, 406);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(94, 40);
            this.Cancel_Button.TabIndex = 10;
            this.Cancel_Button.Text = "閉じる";
            this.Cancel_Button.UseVisualStyleBackColor = false;
            // 
            // FormIkouNameColorSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(618, 460);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.btnImportFromMaster);
            this.Controls.Add(this.btnExportToMaster);
            this.Controls.Add(this.grpEdit);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormIkouNameColorSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "遺構名色設定";
            this.grpEdit.ResumeLayout(false);
            this.grpEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.GroupBox grpEdit;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox CBoxColor;
        private System.Windows.Forms.Label lblColorSample;
        private System.Windows.Forms.Label lblNotice;
        private System.Windows.Forms.Button btnExportToMaster;
        private System.Windows.Forms.Button btnImportFromMaster;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button Cancel_Button;
    }
}
