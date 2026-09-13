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

        private void InitializeComponent() {
            lblTitle = new Label();
            listBox1 = new ListBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnMoveUp = new Button();
            btnMoveDown = new Button();
            grpEdit = new GroupBox();
            lblNotice = new Label();
            lblColorSample = new Label();
            CBoxColor = new ComboBox();
            lblColor = new Label();
            txtPattern = new TextBox();
            lblPattern = new Label();
            btnExportToMaster = new Button();
            btnImportFromMaster = new Button();
            Save_Button = new Button();
            Cancel_Button = new Button();
            grpEdit.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(20, 35, 65);
            lblTitle.Location = new Point(14, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(348, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "遺構名・プレフィックス別 表示色定義 (遺構名色.txt)";
            // 
            // listBox1
            // 
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            listBox1.FormattingEnabled = true;
            listBox1.IntegralHeight = false;
            listBox1.ItemHeight = 28;
            listBox1.Location = new Point(14, 38);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(250, 310);
            listBox1.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.White;
            btnAdd.Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold);
            btnAdd.Location = new Point(14, 355);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 32);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "➕ 追加";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.White;
            btnDelete.Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold);
            btnDelete.Location = new Point(97, 355);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 32);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "➖ 削除";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnMoveUp
            // 
            btnMoveUp.BackColor = Color.White;
            btnMoveUp.Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold);
            btnMoveUp.Location = new Point(183, 355);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.Size = new Size(39, 32);
            btnMoveUp.TabIndex = 4;
            btnMoveUp.Text = "▲";
            btnMoveUp.UseVisualStyleBackColor = true;
            // 
            // btnMoveDown
            // 
            btnMoveDown.BackColor = Color.White;
            btnMoveDown.Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold);
            btnMoveDown.Location = new Point(222, 355);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.Size = new Size(42, 32);
            btnMoveDown.TabIndex = 5;
            btnMoveDown.Text = "▼";
            btnMoveDown.UseVisualStyleBackColor = true;
            // 
            // grpEdit
            // 
            grpEdit.BackColor = Color.White;
            grpEdit.Controls.Add(lblNotice);
            grpEdit.Controls.Add(lblColorSample);
            grpEdit.Controls.Add(CBoxColor);
            grpEdit.Controls.Add(lblColor);
            grpEdit.Controls.Add(txtPattern);
            grpEdit.Controls.Add(lblPattern);
            grpEdit.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            grpEdit.ForeColor = Color.FromArgb(30, 40, 60);
            grpEdit.Location = new Point(276, 38);
            grpEdit.Name = "grpEdit";
            grpEdit.Size = new Size(328, 350);
            grpEdit.TabIndex = 6;
            grpEdit.TabStop = false;
            grpEdit.Text = "遺構色設定";
            // 
            // lblNotice
            // 
            lblNotice.Font = new Font("Yu Gothic UI", 10.5F);
            lblNotice.ForeColor = Color.FromArgb(80, 90, 110);
            lblNotice.Location = new Point(14, 175);
            lblNotice.Name = "lblNotice";
            lblNotice.Size = new Size(298, 155);
            lblNotice.TabIndex = 5;
            lblNotice.Text = "※ 該当しない遺構名は「最終行」の色が自動適用されます。\r\n※ 上端・中・下端の濃淡はレイヤ設定の指定値で自動反映されます。\r\n※ プレフィックス指定（例: 'SB'）で前方一致判定します。";
            // 
            // lblColorSample
            // 
            lblColorSample.BackColor = Color.Red;
            lblColorSample.BorderStyle = BorderStyle.FixedSingle;
            lblColorSample.Location = new Point(238, 124);
            lblColorSample.Name = "lblColorSample";
            lblColorSample.Size = new Size(74, 29);
            lblColorSample.TabIndex = 4;
            // 
            // CBoxColor
            // 
            CBoxColor.DrawMode = DrawMode.OwnerDrawFixed;
            CBoxColor.DropDownStyle = ComboBoxStyle.DropDownList;
            CBoxColor.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            CBoxColor.FormattingEnabled = true;
            CBoxColor.ItemHeight = 23;
            CBoxColor.Location = new Point(14, 124);
            CBoxColor.Name = "CBoxColor";
            CBoxColor.Size = new Size(218, 29);
            CBoxColor.TabIndex = 3;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Yu Gothic UI", 12F);
            lblColor.Location = new Point(14, 98);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(61, 21);
            lblColor.TabIndex = 2;
            lblColor.Text = "表示色:";
            // 
            // txtPattern
            // 
            txtPattern.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            txtPattern.Location = new Point(14, 54);
            txtPattern.Name = "txtPattern";
            txtPattern.Size = new Size(298, 29);
            txtPattern.TabIndex = 1;
            // 
            // lblPattern
            // 
            lblPattern.AutoSize = true;
            lblPattern.Font = new Font("Yu Gothic UI", 12F);
            lblPattern.Location = new Point(14, 28);
            lblPattern.Name = "lblPattern";
            lblPattern.Size = new Size(152, 21);
            lblPattern.TabIndex = 0;
            lblPattern.Text = "遺構名 / プレフィックス:";
            // 
            // btnExportToMaster
            // 
            btnExportToMaster.BackColor = Color.FromArgb(233, 236, 243);
            btnExportToMaster.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnExportToMaster.FlatStyle = FlatStyle.Flat;
            btnExportToMaster.Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold);
            btnExportToMaster.ForeColor = Color.FromArgb(25, 45, 80);
            btnExportToMaster.Location = new Point(14, 408);
            btnExportToMaster.Name = "btnExportToMaster";
            btnExportToMaster.Size = new Size(135, 36);
            btnExportToMaster.TabIndex = 7;
            btnExportToMaster.Text = "📤 マスターへ反映";
            btnExportToMaster.UseVisualStyleBackColor = false;
            // 
            // btnImportFromMaster
            // 
            btnImportFromMaster.BackColor = Color.FromArgb(233, 236, 243);
            btnImportFromMaster.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnImportFromMaster.FlatStyle = FlatStyle.Flat;
            btnImportFromMaster.Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold);
            btnImportFromMaster.ForeColor = Color.FromArgb(25, 45, 80);
            btnImportFromMaster.Location = new Point(155, 408);
            btnImportFromMaster.Name = "btnImportFromMaster";
            btnImportFromMaster.Size = new Size(145, 36);
            btnImportFromMaster.TabIndex = 8;
            btnImportFromMaster.Text = "📥 マスターから反映";
            btnImportFromMaster.UseVisualStyleBackColor = false;
            // 
            // Save_Button
            // 
            Save_Button.BackColor = Color.FromArgb(40, 167, 69);
            Save_Button.FlatAppearance.BorderSize = 0;
            Save_Button.FlatStyle = FlatStyle.Flat;
            Save_Button.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            Save_Button.ForeColor = Color.White;
            Save_Button.Location = new Point(364, 406);
            Save_Button.Name = "Save_Button";
            Save_Button.Size = new Size(138, 40);
            Save_Button.TabIndex = 9;
            Save_Button.Text = "💾 設定を保存";
            Save_Button.UseVisualStyleBackColor = false;
            // 
            // Cancel_Button
            // 
            Cancel_Button.BackColor = Color.FromArgb(220, 225, 235);
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.FlatStyle = FlatStyle.Flat;
            Cancel_Button.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            Cancel_Button.ForeColor = Color.FromArgb(30, 40, 60);
            Cancel_Button.Location = new Point(510, 406);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(94, 40);
            Cancel_Button.TabIndex = 10;
            Cancel_Button.Text = "閉じる";
            Cancel_Button.UseVisualStyleBackColor = false;
            // 
            // FormIkouNameColorSettings
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(618, 460);
            Controls.Add(Cancel_Button);
            Controls.Add(Save_Button);
            Controls.Add(btnImportFromMaster);
            Controls.Add(btnExportToMaster);
            Controls.Add(grpEdit);
            Controls.Add(btnMoveDown);
            Controls.Add(btnMoveUp);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(listBox1);
            Controls.Add(lblTitle);
            Font = new Font("Yu Gothic UI", 12F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormIkouNameColorSettings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "遺構名色設定";
            grpEdit.ResumeLayout(false);
            grpEdit.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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
