namespace Site7 {
    partial class FormDefLayer {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.LAYERPAGE1L = new System.Windows.Forms.Label();
            this.PEN1L = new System.Windows.Forms.Label();
            this.CBoxColor = new System.Windows.Forms.ComboBox();
            this.SIZE1L = new System.Windows.Forms.Label();
            this.CBoxSize = new System.Windows.Forms.ComboBox();
            this.MARK1L = new System.Windows.Forms.Label();
            this.MARK1 = new System.Windows.Forms.TextBox();
            this.PENSTYLEV = new System.Windows.Forms.TextBox();
            this.MARK2L = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CBoxWidth = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxLayerG = new System.Windows.Forms.ComboBox();
            this.CBoxMark = new System.Windows.Forms.ComboBox();
            this.CBoxLineStyle = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "L01",
            "L02",
            "L03",
            "L04",
            "L05",
            "L06",
            "L07",
            "L08",
            "L09",
            "L10",
            "L11",
            "L12",
            "L13",
            "L14",
            "L15",
            "L16"});
            this.listBox1.Location = new System.Drawing.Point(21, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(143, 276);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // LAYERPAGE1L
            // 
            this.LAYERPAGE1L.AutoSize = true;
            this.LAYERPAGE1L.BackColor = System.Drawing.Color.MistyRose;
            this.LAYERPAGE1L.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LAYERPAGE1L.Location = new System.Drawing.Point(189, 44);
            this.LAYERPAGE1L.Name = "LAYERPAGE1L";
            this.LAYERPAGE1L.Size = new System.Drawing.Size(57, 14);
            this.LAYERPAGE1L.TabIndex = 172;
            this.LAYERPAGE1L.Text = "レイヤ名";
            // 
            // PEN1L
            // 
            this.PEN1L.AutoSize = true;
            this.PEN1L.BackColor = System.Drawing.Color.MistyRose;
            this.PEN1L.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PEN1L.Location = new System.Drawing.Point(189, 84);
            this.PEN1L.Name = "PEN1L";
            this.PEN1L.Size = new System.Drawing.Size(52, 14);
            this.PEN1L.TabIndex = 171;
            this.PEN1L.Text = "表示色";
            // 
            // CBoxColor
            // 
            this.CBoxColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.CBoxColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBoxColor.DropDownHeight = 350;
            this.CBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxColor.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBoxColor.FormattingEnabled = true;
            this.CBoxColor.IntegralHeight = false;
            this.CBoxColor.Items.AddRange(new object[] {
            "黒",
            "赤",
            "黄",
            "緑",
            "水色",
            "青",
            "紫",
            "黒",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.CBoxColor.Location = new System.Drawing.Point(261, 74);
            this.CBoxColor.Name = "CBoxColor";
            this.CBoxColor.Size = new System.Drawing.Size(119, 30);
            this.CBoxColor.TabIndex = 174;
            this.CBoxColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CBoxColor_DrawItem);
            this.CBoxColor.SelectedIndexChanged += new System.EventHandler(this.CBoxColor_SelectedIndexChanged);
            // 
            // SIZE1L
            // 
            this.SIZE1L.AutoSize = true;
            this.SIZE1L.BackColor = System.Drawing.Color.MistyRose;
            this.SIZE1L.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SIZE1L.Location = new System.Drawing.Point(189, 166);
            this.SIZE1L.Name = "SIZE1L";
            this.SIZE1L.Size = new System.Drawing.Size(42, 14);
            this.SIZE1L.TabIndex = 169;
            this.SIZE1L.Text = "サイズ";
            // 
            // CBoxSize
            // 
            this.CBoxSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBoxSize.DropDownHeight = 400;
            this.CBoxSize.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBoxSize.FormattingEnabled = true;
            this.CBoxSize.IntegralHeight = false;
            this.CBoxSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "10",
            "20"});
            this.CBoxSize.Location = new System.Drawing.Point(261, 156);
            this.CBoxSize.Name = "CBoxSize";
            this.CBoxSize.Size = new System.Drawing.Size(75, 29);
            this.CBoxSize.TabIndex = 175;
            // 
            // MARK1L
            // 
            this.MARK1L.AutoSize = true;
            this.MARK1L.BackColor = System.Drawing.Color.MistyRose;
            this.MARK1L.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MARK1L.Location = new System.Drawing.Point(189, 129);
            this.MARK1L.Name = "MARK1L";
            this.MARK1L.Size = new System.Drawing.Size(42, 14);
            this.MARK1L.TabIndex = 170;
            this.MARK1L.Text = "マーク";
            // 
            // MARK1
            // 
            this.MARK1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.MARK1.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MARK1.Location = new System.Drawing.Point(376, 166);
            this.MARK1.Name = "MARK1";
            this.MARK1.Size = new System.Drawing.Size(40, 29);
            this.MARK1.TabIndex = 151;
            this.MARK1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MARK1.Visible = false;
            // 
            // PENSTYLEV
            // 
            this.PENSTYLEV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.PENSTYLEV.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PENSTYLEV.Location = new System.Drawing.Point(376, 211);
            this.PENSTYLEV.Name = "PENSTYLEV";
            this.PENSTYLEV.Size = new System.Drawing.Size(59, 29);
            this.PENSTYLEV.TabIndex = 163;
            this.PENSTYLEV.Visible = false;
            // 
            // MARK2L
            // 
            this.MARK2L.AutoSize = true;
            this.MARK2L.BackColor = System.Drawing.Color.LightCyan;
            this.MARK2L.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MARK2L.Location = new System.Drawing.Point(189, 247);
            this.MARK2L.Name = "MARK2L";
            this.MARK2L.Size = new System.Drawing.Size(37, 14);
            this.MARK2L.TabIndex = 160;
            this.MARK2L.Text = "線種";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.BackColor = System.Drawing.Color.LightCyan;
            this.Label22.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label22.Location = new System.Drawing.Point(434, 271);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(22, 14);
            this.Label22.TabIndex = 164;
            this.Label22.Text = "線";
            this.Label22.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightCyan;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(189, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 160;
            this.label2.Text = "線幅";
            // 
            // CBoxWidth
            // 
            this.CBoxWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBoxWidth.DropDownHeight = 400;
            this.CBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxWidth.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBoxWidth.FormattingEnabled = true;
            this.CBoxWidth.IntegralHeight = false;
            this.CBoxWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.CBoxWidth.Location = new System.Drawing.Point(261, 201);
            this.CBoxWidth.Name = "CBoxWidth";
            this.CBoxWidth.Size = new System.Drawing.Size(59, 29);
            this.CBoxWidth.TabIndex = 175;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(261, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(184, 29);
            this.textBox1.TabIndex = 151;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancel_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Cancel_Button.Location = new System.Drawing.Point(358, 325);
            this.Cancel_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(100, 28);
            this.Cancel_Button.TabIndex = 176;
            this.Cancel_Button.Text = "閉じる";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(192, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 177;
            this.button1.Text = "設定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxLayerG
            // 
            this.comboBoxLayerG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayerG.FormattingEnabled = true;
            this.comboBoxLayerG.Items.AddRange(new object[] {
            "遺物レイヤGRP",
            "基準点レイヤGRP",
            "作図レイヤGRP",
            "遺構レイヤGRP"});
            this.comboBoxLayerG.Location = new System.Drawing.Point(21, 5);
            this.comboBoxLayerG.Name = "comboBoxLayerG";
            this.comboBoxLayerG.Size = new System.Drawing.Size(138, 20);
            this.comboBoxLayerG.TabIndex = 178;
            this.comboBoxLayerG.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayerG_SelectedIndexChanged);
            // 
            // CBoxMark
            // 
            this.CBoxMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBoxMark.DropDownHeight = 400;
            this.CBoxMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxMark.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBoxMark.FormattingEnabled = true;
            this.CBoxMark.IntegralHeight = false;
            this.CBoxMark.Items.AddRange(new object[] {
            "〇",
            "□",
            "△",
            "⦿"});
            this.CBoxMark.Location = new System.Drawing.Point(261, 119);
            this.CBoxMark.Name = "CBoxMark";
            this.CBoxMark.Size = new System.Drawing.Size(59, 29);
            this.CBoxMark.TabIndex = 175;
            // 
            // CBoxLineStyle
            // 
            this.CBoxLineStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBoxLineStyle.DropDownHeight = 400;
            this.CBoxLineStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxLineStyle.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBoxLineStyle.FormattingEnabled = true;
            this.CBoxLineStyle.IntegralHeight = false;
            this.CBoxLineStyle.Items.AddRange(new object[] {
            "折線",
            "曲線"});
            this.CBoxLineStyle.Location = new System.Drawing.Point(261, 237);
            this.CBoxLineStyle.Name = "CBoxLineStyle";
            this.CBoxLineStyle.Size = new System.Drawing.Size(84, 29);
            this.CBoxLineStyle.TabIndex = 175;
            // 
            // FormDefLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(462, 357);
            this.Controls.Add(this.comboBoxLayerG);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.LAYERPAGE1L);
            this.Controls.Add(this.PEN1L);
            this.Controls.Add(this.CBoxWidth);
            this.Controls.Add(this.CBoxLineStyle);
            this.Controls.Add(this.CBoxMark);
            this.Controls.Add(this.CBoxSize);
            this.Controls.Add(this.PENSTYLEV);
            this.Controls.Add(this.Label22);
            this.Controls.Add(this.CBoxColor);
            this.Controls.Add(this.SIZE1L);
            this.Controls.Add(this.MARK1L);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MARK1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MARK2L);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "FormDefLayer";
            this.Text = "レイヤ設定";
            this.Load += new System.EventHandler(this.FormDefLayer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        internal System.Windows.Forms.Label LAYERPAGE1L;
        internal System.Windows.Forms.Label PEN1L;
        public System.Windows.Forms.ComboBox CBoxColor;
        internal System.Windows.Forms.Label SIZE1L;
        public System.Windows.Forms.ComboBox CBoxSize;
        internal System.Windows.Forms.Label MARK1L;
        public System.Windows.Forms.TextBox MARK1;
        internal System.Windows.Forms.TextBox PENSTYLEV;
        internal System.Windows.Forms.Label MARK2L;
        internal System.Windows.Forms.Label Label22;
        internal System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox CBoxWidth;
        public System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxLayerG;
        public System.Windows.Forms.ComboBox CBoxMark;
        public System.Windows.Forms.ComboBox CBoxLineStyle;
    }
}