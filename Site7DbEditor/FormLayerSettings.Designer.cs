namespace Site7DbEditor
{
    partial class FormLayerSettings
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
            comboBoxLayerG = new ComboBox();
            listBox1 = new ListBox();
            grpEdit = new GroupBox();
            lblLayerName = new Label();
            textBox1 = new TextBox();
            lblColor = new Label();
            CBoxColor = new ComboBox();
            lblMark = new Label();
            CBoxMark = new ComboBox();
            lblSize = new Label();
            CBoxSize = new ComboBox();
            lblWidth = new Label();
            CBoxWidth = new ComboBox();
            lblLineStyle = new Label();
            CBoxLineStyle = new ComboBox();
            button1 = new Button();
            btnExportToMaster = new Button();
            btnImportFromMaster = new Button();
            Save_Button = new Button();
            Cancel_Button = new Button();
            grpEdit.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxLayerG
            // 
            comboBoxLayerG.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLayerG.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            comboBoxLayerG.FormattingEnabled = true;
            comboBoxLayerG.Items.AddRange(new object[] { "🏛 遺構 (Layer遺構.txt)", "🏺 遺物 (Layer遺物.txt)", "📍 基準点 (Layer基準点.txt)", "📏 作図 (Layer作図.txt)" });
            comboBoxLayerG.Location = new Point(14, 12);
            comboBoxLayerG.Name = "comboBoxLayerG";
            comboBoxLayerG.Size = new Size(264, 29);
            comboBoxLayerG.TabIndex = 0;
            comboBoxLayerG.SelectedIndexChanged += ComboBoxLayerG_SelectedIndexChanged;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Yu Gothic UI", 13F);
            listBox1.FormattingEnabled = true;
            listBox1.IntegralHeight = true;
            listBox1.ItemHeight = 23;
            listBox1.Location = new Point(14, 48);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(264, 372);
            listBox1.TabIndex = 1;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            // 
            // grpEdit
            // 
            grpEdit.Controls.Add(lblLayerName);
            grpEdit.Controls.Add(textBox1);
            grpEdit.Controls.Add(lblColor);
            grpEdit.Controls.Add(CBoxColor);
            grpEdit.Controls.Add(lblMark);
            grpEdit.Controls.Add(CBoxMark);
            grpEdit.Controls.Add(lblSize);
            grpEdit.Controls.Add(CBoxSize);
            grpEdit.Controls.Add(lblWidth);
            grpEdit.Controls.Add(CBoxWidth);
            grpEdit.Controls.Add(lblLineStyle);
            grpEdit.Controls.Add(CBoxLineStyle);
            grpEdit.Controls.Add(button1);
            grpEdit.Font = new Font("Yu Gothic UI", 11F, FontStyle.Bold);
            grpEdit.ForeColor = Color.FromArgb(40, 60, 90);
            grpEdit.Location = new Point(292, 6);
            grpEdit.Name = "grpEdit";
            grpEdit.Size = new Size(362, 276);
            grpEdit.TabIndex = 2;
            grpEdit.TabStop = false;
            grpEdit.Text = " 選択レイヤの設定 ";
            // 
            // lblLayerName
            // 
            lblLayerName.BackColor = Color.FromArgb(232, 237, 245);
            lblLayerName.BorderStyle = BorderStyle.FixedSingle;
            lblLayerName.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblLayerName.ForeColor = Color.FromArgb(30, 40, 60);
            lblLayerName.Location = new Point(14, 26);
            lblLayerName.Name = "lblLayerName";
            lblLayerName.Size = new Size(80, 29);
            lblLayerName.TabIndex = 0;
            lblLayerName.Text = "レイヤ名";
            lblLayerName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            textBox1.Location = new Point(100, 26);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(246, 29);
            textBox1.TabIndex = 1;
            // 
            // lblColor
            // 
            lblColor.BackColor = Color.FromArgb(232, 237, 245);
            lblColor.BorderStyle = BorderStyle.FixedSingle;
            lblColor.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblColor.ForeColor = Color.FromArgb(30, 40, 60);
            lblColor.Location = new Point(14, 64);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(80, 30);
            lblColor.TabIndex = 2;
            lblColor.Text = "表示色";
            lblColor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CBoxColor
            // 
            CBoxColor.DrawMode = DrawMode.OwnerDrawFixed;
            CBoxColor.DropDownStyle = ComboBoxStyle.DropDownList;
            CBoxColor.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            CBoxColor.FormattingEnabled = true;
            CBoxColor.ItemHeight = 24;
            CBoxColor.Items.AddRange(new object[] { "黒", "赤", "緑", "青", "黄", "マゼンタ", "シアン", "白", "牡丹", "茶", "橙", "薄緑", "明青", "青紫", "明灰", "暗灰" });
            CBoxColor.Location = new Point(100, 64);
            CBoxColor.Name = "CBoxColor";
            CBoxColor.Size = new Size(160, 30);
            CBoxColor.TabIndex = 3;
            CBoxColor.DrawItem += CBoxColor_DrawItem;
            // 
            // lblMark
            // 
            lblMark.BackColor = Color.FromArgb(232, 237, 245);
            lblMark.BorderStyle = BorderStyle.FixedSingle;
            lblMark.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblMark.ForeColor = Color.FromArgb(30, 40, 60);
            lblMark.Location = new Point(14, 104);
            lblMark.Name = "lblMark";
            lblMark.Size = new Size(80, 29);
            lblMark.TabIndex = 4;
            lblMark.Text = "マーク";
            lblMark.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CBoxMark
            // 
            CBoxMark.DropDownStyle = ComboBoxStyle.DropDownList;
            CBoxMark.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            CBoxMark.FormattingEnabled = true;
            CBoxMark.Items.AddRange(new object[] { "〇", "□", "△", "⦿", "✕", "＋", "◇", "★" });
            CBoxMark.Location = new Point(100, 104);
            CBoxMark.Name = "CBoxMark";
            CBoxMark.Size = new Size(140, 29);
            CBoxMark.TabIndex = 5;
            // 
            // lblSize
            // 
            lblSize.BackColor = Color.FromArgb(232, 237, 245);
            lblSize.BorderStyle = BorderStyle.FixedSingle;
            lblSize.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblSize.ForeColor = Color.FromArgb(30, 40, 60);
            lblSize.Location = new Point(14, 144);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(80, 29);
            lblSize.TabIndex = 6;
            lblSize.Text = "サイズ";
            lblSize.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CBoxSize
            // 
            CBoxSize.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            CBoxSize.FormattingEnabled = true;
            CBoxSize.Items.AddRange(new object[] { "0.5", "1.0", "1.5", "2.0", "3.0", "4.0", "5.0", "6.0", "8.0", "10.0" });
            CBoxSize.Location = new Point(100, 144);
            CBoxSize.Name = "CBoxSize";
            CBoxSize.Size = new Size(120, 29);
            CBoxSize.TabIndex = 7;
            // 
            // lblWidth
            // 
            lblWidth.BackColor = Color.FromArgb(232, 237, 245);
            lblWidth.BorderStyle = BorderStyle.FixedSingle;
            lblWidth.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblWidth.ForeColor = Color.FromArgb(30, 40, 60);
            lblWidth.Location = new Point(14, 144);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(80, 29);
            lblWidth.TabIndex = 8;
            lblWidth.Text = "線幅";
            lblWidth.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CBoxWidth
            // 
            CBoxWidth.DropDownStyle = ComboBoxStyle.DropDownList;
            CBoxWidth.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            CBoxWidth.FormattingEnabled = true;
            CBoxWidth.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            CBoxWidth.Location = new Point(100, 144);
            CBoxWidth.Name = "CBoxWidth";
            CBoxWidth.Size = new Size(120, 29);
            CBoxWidth.TabIndex = 9;
            // 
            // lblLineStyle
            // 
            lblLineStyle.BackColor = Color.FromArgb(232, 237, 245);
            lblLineStyle.BorderStyle = BorderStyle.FixedSingle;
            lblLineStyle.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblLineStyle.ForeColor = Color.FromArgb(30, 40, 60);
            lblLineStyle.Location = new Point(14, 184);
            lblLineStyle.Name = "lblLineStyle";
            lblLineStyle.Size = new Size(80, 29);
            lblLineStyle.TabIndex = 10;
            lblLineStyle.Text = "線種";
            lblLineStyle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CBoxLineStyle
            // 
            CBoxLineStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            CBoxLineStyle.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            CBoxLineStyle.FormattingEnabled = true;
            CBoxLineStyle.Items.AddRange(new object[] { "折線", "曲線" });
            CBoxLineStyle.Location = new Point(100, 184);
            CBoxLineStyle.Name = "CBoxLineStyle";
            CBoxLineStyle.Size = new Size(120, 29);
            CBoxLineStyle.TabIndex = 11;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(232, 238, 248);
            button1.FlatAppearance.BorderColor = Color.FromArgb(170, 190, 220);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            button1.ForeColor = Color.FromArgb(20, 50, 95);
            button1.Location = new Point(100, 226);
            button1.Name = "button1";
            button1.Size = new Size(246, 36);
            button1.TabIndex = 12;
            button1.Text = "✔ このレイヤに適用";
            button1.UseVisualStyleBackColor = false;
            button1.Click += Button1_Click;
            // 
            // btnExportToMaster
            // 
            btnExportToMaster.BackColor = Color.FromArgb(233, 236, 243);
            btnExportToMaster.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnExportToMaster.FlatStyle = FlatStyle.Flat;
            btnExportToMaster.Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold);
            btnExportToMaster.ForeColor = Color.FromArgb(25, 45, 80);
            btnExportToMaster.Location = new Point(292, 294);
            btnExportToMaster.Name = "btnExportToMaster";
            btnExportToMaster.Size = new Size(174, 36);
            btnExportToMaster.TabIndex = 3;
            btnExportToMaster.Text = "📤 マスターへ反映";
            btnExportToMaster.UseVisualStyleBackColor = false;
            btnExportToMaster.Click += BtnExportToMaster_Click;
            // 
            // btnImportFromMaster
            // 
            btnImportFromMaster.BackColor = Color.FromArgb(233, 236, 243);
            btnImportFromMaster.FlatAppearance.BorderColor = Color.FromArgb(180, 190, 210);
            btnImportFromMaster.FlatStyle = FlatStyle.Flat;
            btnImportFromMaster.Font = new Font("Yu Gothic UI", 11.5F, FontStyle.Bold);
            btnImportFromMaster.ForeColor = Color.FromArgb(25, 45, 80);
            btnImportFromMaster.Location = new Point(480, 294);
            btnImportFromMaster.Name = "btnImportFromMaster";
            btnImportFromMaster.Size = new Size(174, 36);
            btnImportFromMaster.TabIndex = 4;
            btnImportFromMaster.Text = "📥 マスターから反映";
            btnImportFromMaster.UseVisualStyleBackColor = false;
            btnImportFromMaster.Click += BtnImportFromMaster_Click;
            // 
            // Save_Button
            // 
            Save_Button.BackColor = Color.FromArgb(40, 167, 69);
            Save_Button.FlatAppearance.BorderSize = 0;
            Save_Button.FlatStyle = FlatStyle.Flat;
            Save_Button.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            Save_Button.ForeColor = Color.White;
            Save_Button.Location = new Point(292, 338);
            Save_Button.Name = "Save_Button";
            Save_Button.Size = new Size(224, 42);
            Save_Button.TabIndex = 5;
            Save_Button.Text = "💾 設定を保存";
            Save_Button.UseVisualStyleBackColor = false;
            Save_Button.Click += Save_Button_Click;
            // 
            // Cancel_Button
            // 
            Cancel_Button.BackColor = Color.FromArgb(220, 225, 235);
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.FlatStyle = FlatStyle.Flat;
            Cancel_Button.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            Cancel_Button.ForeColor = Color.FromArgb(30, 40, 60);
            Cancel_Button.Location = new Point(526, 338);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(128, 42);
            Cancel_Button.TabIndex = 6;
            Cancel_Button.Text = "閉じる";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // FormLayerSettings
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(668, 396);
            Controls.Add(Cancel_Button);
            Controls.Add(Save_Button);
            Controls.Add(btnImportFromMaster);
            Controls.Add(btnExportToMaster);
            Controls.Add(grpEdit);
            Controls.Add(listBox1);
            Controls.Add(comboBoxLayerG);
            Font = new Font("Yu Gothic UI", 12F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLayerSettings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "レイヤ設定";
            grpEdit.ResumeLayout(false);
            grpEdit.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLayerG;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox grpEdit;
        private System.Windows.Forms.Label lblLayerName;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblMark;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblLineStyle;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox CBoxColor;
        private System.Windows.Forms.ComboBox CBoxMark;
        private System.Windows.Forms.ComboBox CBoxSize;
        private System.Windows.Forms.ComboBox CBoxWidth;
        private System.Windows.Forms.ComboBox CBoxLineStyle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnExportToMaster;
        private System.Windows.Forms.Button btnImportFromMaster;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button Cancel_Button;
    }
}
