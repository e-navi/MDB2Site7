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

        private void InitializeComponent()
        {
            this.comboBoxLayerG = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblLayerName = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblMark = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblLineStyle = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CBoxColor = new System.Windows.Forms.ComboBox();
            this.CBoxMark = new System.Windows.Forms.ComboBox();
            this.CBoxSize = new System.Windows.Forms.ComboBox();
            this.CBoxWidth = new System.Windows.Forms.ComboBox();
            this.CBoxLineStyle = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnExportToMaster = new System.Windows.Forms.Button();
            this.btnImportFromMaster = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxLayerG
            // 
            this.comboBoxLayerG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayerG.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.comboBoxLayerG.FormattingEnabled = true;
            this.comboBoxLayerG.Items.AddRange(new object[] {
            "🏛 遺構 (Layer遺構.txt)",
            "🏺 遺物 (Layer遺物.txt)",
            "📍 基準点 (Layer基準点.txt)",
            "📏 作図 (Layer作図.txt)"});
            this.comboBoxLayerG.Location = new System.Drawing.Point(16, 14);
            this.comboBoxLayerG.Name = "comboBoxLayerG";
            this.comboBoxLayerG.Size = new System.Drawing.Size(260, 29);
            this.comboBoxLayerG.TabIndex = 0;
            this.comboBoxLayerG.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLayerG_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(16, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(260, 445);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // lblLayerName
            // 
            this.lblLayerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.lblLayerName.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLayerName.Location = new System.Drawing.Point(295, 56);
            this.lblLayerName.Name = "lblLayerName";
            this.lblLayerName.Size = new System.Drawing.Size(80, 28);
            this.lblLayerName.TabIndex = 2;
            this.lblLayerName.Text = "レイヤ名";
            this.lblLayerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblColor
            // 
            this.lblColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.lblColor.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblColor.Location = new System.Drawing.Point(295, 100);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(80, 28);
            this.lblColor.TabIndex = 3;
            this.lblColor.Text = "表示色";
            this.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMark
            // 
            this.lblMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.lblMark.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMark.Location = new System.Drawing.Point(295, 144);
            this.lblMark.Name = "lblMark";
            this.lblMark.Size = new System.Drawing.Size(80, 28);
            this.lblMark.TabIndex = 4;
            this.lblMark.Text = "マーク";
            this.lblMark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSize
            // 
            this.lblSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.lblSize.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSize.Location = new System.Drawing.Point(295, 188);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(80, 28);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "サイズ";
            this.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWidth
            // 
            this.lblWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.lblWidth.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWidth.Location = new System.Drawing.Point(295, 232);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(80, 28);
            this.lblWidth.TabIndex = 6;
            this.lblWidth.Text = "線幅";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLineStyle
            // 
            this.lblLineStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.lblLineStyle.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLineStyle.Location = new System.Drawing.Point(295, 276);
            this.lblLineStyle.Name = "lblLineStyle";
            this.lblLineStyle.Size = new System.Drawing.Size(80, 28);
            this.lblLineStyle.TabIndex = 7;
            this.lblLineStyle.Text = "線種";
            this.lblLineStyle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(385, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(240, 29);
            this.textBox1.TabIndex = 8;
            // 
            // CBoxColor
            // 
            this.CBoxColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxColor.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBoxColor.FormattingEnabled = true;
            this.CBoxColor.ItemHeight = 24;
            this.CBoxColor.Items.AddRange(new object[] {
            "黒",
            "赤",
            "緑",
            "青",
            "黄",
            "マゼンタ",
            "シアン",
            "白",
            "牡丹",
            "茶",
            "橙",
            "薄緑",
            "明青",
            "青紫",
            "明灰",
            "暗灰"});
            this.CBoxColor.Location = new System.Drawing.Point(385, 98);
            this.CBoxColor.Name = "CBoxColor";
            this.CBoxColor.Size = new System.Drawing.Size(160, 30);
            this.CBoxColor.TabIndex = 9;
            this.CBoxColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CBoxColor_DrawItem);
            // 
            // CBoxMark
            // 
            this.CBoxMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxMark.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBoxMark.FormattingEnabled = true;
            this.CBoxMark.Items.AddRange(new object[] {
            "〇",
            "□",
            "△",
            "⦿",
            "✕",
            "＋",
            "◇",
            "★"});
            this.CBoxMark.Location = new System.Drawing.Point(385, 142);
            this.CBoxMark.Name = "CBoxMark";
            this.CBoxMark.Size = new System.Drawing.Size(130, 29);
            this.CBoxMark.TabIndex = 10;
            // 
            // CBoxSize
            // 
            this.CBoxSize.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBoxSize.FormattingEnabled = true;
            this.CBoxSize.Items.AddRange(new object[] {
            "0.5",
            "1.0",
            "1.5",
            "2.0",
            "3.0",
            "4.0",
            "5.0",
            "6.0",
            "8.0",
            "10.0"});
            this.CBoxSize.Location = new System.Drawing.Point(385, 186);
            this.CBoxSize.Name = "CBoxSize";
            this.CBoxSize.Size = new System.Drawing.Size(110, 29);
            this.CBoxSize.TabIndex = 11;
            // 
            // CBoxWidth
            // 
            this.CBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxWidth.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBoxWidth.FormattingEnabled = true;
            this.CBoxWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.CBoxWidth.Location = new System.Drawing.Point(385, 230);
            this.CBoxWidth.Name = "CBoxWidth";
            this.CBoxWidth.Size = new System.Drawing.Size(110, 29);
            this.CBoxWidth.TabIndex = 12;
            // 
            // CBoxLineStyle
            // 
            this.CBoxLineStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxLineStyle.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBoxLineStyle.FormattingEnabled = true;
            this.CBoxLineStyle.Items.AddRange(new object[] {
            "折線",
            "曲線"});
            this.CBoxLineStyle.Location = new System.Drawing.Point(385, 274);
            this.CBoxLineStyle.Name = "CBoxLineStyle";
            this.CBoxLineStyle.Size = new System.Drawing.Size(120, 29);
            this.CBoxLineStyle.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(245)))));
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(385, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 36);
            this.button1.TabIndex = 14;
            this.button1.Text = "✔ このレイヤに適用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btnExportToMaster
            // 
            this.btnExportToMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.btnExportToMaster.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.btnExportToMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToMaster.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportToMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.btnExportToMaster.Location = new System.Drawing.Point(295, 410);
            this.btnExportToMaster.Name = "btnExportToMaster";
            this.btnExportToMaster.Size = new System.Drawing.Size(160, 40);
            this.btnExportToMaster.TabIndex = 15;
            this.btnExportToMaster.Text = "📤 マスターへ反映";
            this.btnExportToMaster.UseVisualStyleBackColor = false;
            this.btnExportToMaster.Click += new System.EventHandler(this.BtnExportToMaster_Click);
            // 
            // btnImportFromMaster
            // 
            this.btnImportFromMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.btnImportFromMaster.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(190)))), ((int)(((byte)(210)))));
            this.btnImportFromMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromMaster.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnImportFromMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.btnImportFromMaster.Location = new System.Drawing.Point(465, 410);
            this.btnImportFromMaster.Name = "btnImportFromMaster";
            this.btnImportFromMaster.Size = new System.Drawing.Size(160, 40);
            this.btnImportFromMaster.TabIndex = 16;
            this.btnImportFromMaster.Text = "📥 マスターから反映";
            this.btnImportFromMaster.UseVisualStyleBackColor = false;
            this.btnImportFromMaster.Click += new System.EventHandler(this.BtnImportFromMaster_Click);
            // 
            // Save_Button
            // 
            this.Save_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.Save_Button.FlatAppearance.BorderSize = 0;
            this.Save_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_Button.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.Save_Button.ForeColor = System.Drawing.Color.White;
            this.Save_Button.Location = new System.Drawing.Point(295, 460);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(190, 44);
            this.Save_Button.TabIndex = 17;
            this.Save_Button.Text = "💾 設定を保存";
            this.Save_Button.UseVisualStyleBackColor = false;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.Cancel_Button.FlatAppearance.BorderSize = 0;
            this.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_Button.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.Cancel_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.Cancel_Button.Location = new System.Drawing.Point(495, 460);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(130, 44);
            this.Cancel_Button.TabIndex = 18;
            this.Cancel_Button.Text = "閉じる";
            this.Cancel_Button.UseVisualStyleBackColor = false;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // FormLayerSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(660, 520);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.btnImportFromMaster);
            this.Controls.Add(this.btnExportToMaster);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CBoxLineStyle);
            this.Controls.Add(this.CBoxWidth);
            this.Controls.Add(this.CBoxSize);
            this.Controls.Add(this.CBoxMark);
            this.Controls.Add(this.CBoxColor);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblLineStyle);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblMark);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.lblLayerName);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.comboBoxLayerG);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLayerSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "レイヤ設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLayerG;
        private System.Windows.Forms.ListBox listBox1;
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
