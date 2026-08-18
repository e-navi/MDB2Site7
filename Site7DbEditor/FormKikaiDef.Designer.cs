namespace Site7DbEditor {
    partial class FormKikaiDef {
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.CBSelKikaiP = new System.Windows.Forms.ComboBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CBSelBackP = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonMesure01 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.L_Len1 = new System.Windows.Forms.Label();
            this.L_Len2 = new System.Windows.Forms.Label();
            this.L_Len3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(187, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "０セット";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CBSelKikaiP
            // 
            this.CBSelKikaiP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBSelKikaiP.DropDownHeight = 400;
            this.CBSelKikaiP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSelKikaiP.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBSelKikaiP.FormattingEnabled = true;
            this.CBSelKikaiP.IntegralHeight = false;
            this.CBSelKikaiP.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.CBSelKikaiP.Location = new System.Drawing.Point(123, 17);
            this.CBSelKikaiP.Name = "CBSelKikaiP";
            this.CBSelKikaiP.Size = new System.Drawing.Size(192, 29);
            this.CBSelKikaiP.TabIndex = 170;
            this.CBSelKikaiP.SelectedIndexChanged += new System.EventHandler(this.CBSelKikaiP_SelectedIndexChanged);
            // 
            // Label17
            // 
            this.Label17.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label17.Location = new System.Drawing.Point(37, 21);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(80, 24);
            this.Label17.TabIndex = 171;
            this.Label17.Text = "器械点";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(37, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 173;
            this.label1.Text = "後視点";
            // 
            // CBSelBackP
            // 
            this.CBSelBackP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.CBSelBackP.DropDownHeight = 400;
            this.CBSelBackP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSelBackP.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBSelBackP.FormattingEnabled = true;
            this.CBSelBackP.IntegralHeight = false;
            this.CBSelBackP.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.CBSelBackP.Location = new System.Drawing.Point(123, 56);
            this.CBSelBackP.Name = "CBSelBackP";
            this.CBSelBackP.Size = new System.Drawing.Size(192, 29);
            this.CBSelBackP.TabIndex = 172;
            this.CBSelBackP.SelectedIndexChanged += new System.EventHandler(this.CBSelBackP_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(37, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 24);
            this.label2.TabIndex = 174;
            this.label2.Text = "後視点を視準後";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(187, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 34);
            this.button2.TabIndex = 0;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonMesure01
            // 
            this.buttonMesure01.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonMesure01.Location = new System.Drawing.Point(25, 108);
            this.buttonMesure01.Name = "buttonMesure01";
            this.buttonMesure01.Size = new System.Drawing.Size(133, 32);
            this.buttonMesure01.TabIndex = 175;
            this.buttonMesure01.Tag = "0";
            this.buttonMesure01.Text = "後視点測定";
            this.buttonMesure01.UseVisualStyleBackColor = true;
            this.buttonMesure01.Click += new System.EventHandler(this.buttonMesure01_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(58, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 24);
            this.label4.TabIndex = 176;
            this.label4.Text = "点間距離 :";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(58, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 24);
            this.label6.TabIndex = 176;
            this.label6.Text = "測定距離 :";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(58, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 24);
            this.label5.TabIndex = 176;
            this.label5.Text = "距離誤差 :";
            // 
            // L_Len1
            // 
            this.L_Len1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.L_Len1.ForeColor = System.Drawing.Color.Black;
            this.L_Len1.Location = new System.Drawing.Point(152, 143);
            this.L_Len1.Name = "L_Len1";
            this.L_Len1.Size = new System.Drawing.Size(113, 24);
            this.L_Len1.TabIndex = 176;
            // 
            // L_Len2
            // 
            this.L_Len2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.L_Len2.ForeColor = System.Drawing.Color.Black;
            this.L_Len2.Location = new System.Drawing.Point(152, 167);
            this.L_Len2.Name = "L_Len2";
            this.L_Len2.Size = new System.Drawing.Size(113, 24);
            this.L_Len2.TabIndex = 176;
            // 
            // L_Len3
            // 
            this.L_Len3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.L_Len3.ForeColor = System.Drawing.Color.Black;
            this.L_Len3.Location = new System.Drawing.Point(152, 191);
            this.L_Len3.Name = "L_Len3";
            this.L_Len3.Size = new System.Drawing.Size(113, 24);
            this.L_Len3.TabIndex = 176;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormKikaiDef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(320, 296);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.L_Len3);
            this.Controls.Add(this.L_Len2);
            this.Controls.Add(this.L_Len1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonMesure01);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CBSelBackP);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.CBSelKikaiP);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "FormKikaiDef";
            this.Text = "器械点・後視点指定";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormKikaiDef_FormClosed);
            this.Shown += new System.EventHandler(this.FormKikaiDef_Shown);
            this.VisibleChanged += new System.EventHandler(this.FormKikaiDef_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox CBSelKikaiP;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox CBSelBackP;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonMesure01;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label L_Len1;
        internal System.Windows.Forms.Label L_Len2;
        internal System.Windows.Forms.Label L_Len3;
        private System.Windows.Forms.Timer timer1;
    }
}
