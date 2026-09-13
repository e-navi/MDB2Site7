namespace Site7DbEditor
{
    partial class FormKikaiDef
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grpPointSel = new System.Windows.Forms.GroupBox();
            this.lblKikai = new System.Windows.Forms.Label();
            this.CBSelKikaiP = new System.Windows.Forms.ComboBox();
            this.lblBack = new System.Windows.Forms.Label();
            this.CBSelBackP = new System.Windows.Forms.ComboBox();
            this.grpMeasure = new System.Windows.Forms.GroupBox();
            this.buttonMesure01 = new System.Windows.Forms.Button();
            this.lblPlanLabel = new System.Windows.Forms.Label();
            this.L_Len1 = new System.Windows.Forms.Label();
            this.lblMeasureLabel = new System.Windows.Forms.Label();
            this.L_Len2 = new System.Windows.Forms.Label();
            this.lblErrorLabel = new System.Windows.Forms.Label();
            this.L_Len3 = new System.Windows.Forms.Label();
            this.lblGuidance = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.grpPointSel.SuspendLayout();
            this.grpMeasure.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // grpPointSel
            // 
            this.grpPointSel.BackColor = System.Drawing.Color.White;
            this.grpPointSel.Controls.Add(this.lblKikai);
            this.grpPointSel.Controls.Add(this.CBSelKikaiP);
            this.grpPointSel.Controls.Add(this.lblBack);
            this.grpPointSel.Controls.Add(this.CBSelBackP);
            this.grpPointSel.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpPointSel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.grpPointSel.Location = new System.Drawing.Point(14, 12);
            this.grpPointSel.Name = "grpPointSel";
            this.grpPointSel.Size = new System.Drawing.Size(326, 118);
            this.grpPointSel.TabIndex = 0;
            this.grpPointSel.TabStop = false;
            this.grpPointSel.Text = " 📍 基準点選択 ";
            // 
            // lblKikai
            // 
            this.lblKikai.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblKikai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.lblKikai.Location = new System.Drawing.Point(8, 30);
            this.lblKikai.Name = "lblKikai";
            this.lblKikai.Size = new System.Drawing.Size(84, 29);
            this.lblKikai.TabIndex = 0;
            this.lblKikai.Text = "📍 器械点:";
            this.lblKikai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBSelKikaiP
            // 
            this.CBSelKikaiP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSelKikaiP.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBSelKikaiP.FormattingEnabled = true;
            this.CBSelKikaiP.Location = new System.Drawing.Point(96, 28);
            this.CBSelKikaiP.Name = "CBSelKikaiP";
            this.CBSelKikaiP.Size = new System.Drawing.Size(218, 29);
            this.CBSelKikaiP.TabIndex = 1;
            this.CBSelKikaiP.SelectedIndexChanged += new System.EventHandler(this.CBSelKikaiP_SelectedIndexChanged);
            // 
            // lblBack
            // 
            this.lblBack.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.lblBack.Location = new System.Drawing.Point(8, 70);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(84, 29);
            this.lblBack.TabIndex = 2;
            this.lblBack.Text = "👁 後視点:";
            this.lblBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBSelBackP
            // 
            this.CBSelBackP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBSelBackP.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.CBSelBackP.FormattingEnabled = true;
            this.CBSelBackP.Location = new System.Drawing.Point(96, 68);
            this.CBSelBackP.Name = "CBSelBackP";
            this.CBSelBackP.Size = new System.Drawing.Size(218, 29);
            this.CBSelBackP.TabIndex = 3;
            this.CBSelBackP.SelectedIndexChanged += new System.EventHandler(this.CBSelBackP_SelectedIndexChanged);
            // 
            // grpMeasure
            // 
            this.grpMeasure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.grpMeasure.Controls.Add(this.buttonMesure01);
            this.grpMeasure.Controls.Add(this.lblPlanLabel);
            this.grpMeasure.Controls.Add(this.L_Len1);
            this.grpMeasure.Controls.Add(this.lblMeasureLabel);
            this.grpMeasure.Controls.Add(this.L_Len2);
            this.grpMeasure.Controls.Add(this.lblErrorLabel);
            this.grpMeasure.Controls.Add(this.L_Len3);
            this.grpMeasure.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpMeasure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(190)))));
            this.grpMeasure.Location = new System.Drawing.Point(14, 138);
            this.grpMeasure.Name = "grpMeasure";
            this.grpMeasure.Size = new System.Drawing.Size(326, 172);
            this.grpMeasure.TabIndex = 1;
            this.grpMeasure.TabStop = false;
            this.grpMeasure.Text = " 🔭 後視点測定 & 距離精度確認 ";
            // 
            // buttonMesure01
            // 
            this.buttonMesure01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.buttonMesure01.FlatAppearance.BorderSize = 0;
            this.buttonMesure01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMesure01.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.buttonMesure01.ForeColor = System.Drawing.Color.White;
            this.buttonMesure01.Location = new System.Drawing.Point(190, 22);
            this.buttonMesure01.Name = "buttonMesure01";
            this.buttonMesure01.Size = new System.Drawing.Size(124, 36);
            this.buttonMesure01.TabIndex = 0;
            this.buttonMesure01.Text = "🔭 測定開始";
            this.buttonMesure01.UseVisualStyleBackColor = false;
            this.buttonMesure01.Click += new System.EventHandler(this.buttonMesure01_Click);
            // 
            // lblPlanLabel
            // 
            this.lblPlanLabel.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblPlanLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.lblPlanLabel.Location = new System.Drawing.Point(6, 68);
            this.lblPlanLabel.Name = "lblPlanLabel";
            this.lblPlanLabel.Size = new System.Drawing.Size(104, 26);
            this.lblPlanLabel.TabIndex = 1;
            this.lblPlanLabel.Text = "点間計画距離:";
            this.lblPlanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_Len1
            // 
            this.L_Len1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.L_Len1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.L_Len1.Location = new System.Drawing.Point(114, 68);
            this.L_Len1.Name = "L_Len1";
            this.L_Len1.Size = new System.Drawing.Size(200, 26);
            this.L_Len1.TabIndex = 2;
            this.L_Len1.Text = "--- m";
            this.L_Len1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMeasureLabel
            // 
            this.lblMeasureLabel.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblMeasureLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.lblMeasureLabel.Location = new System.Drawing.Point(6, 100);
            this.lblMeasureLabel.Name = "lblMeasureLabel";
            this.lblMeasureLabel.Size = new System.Drawing.Size(104, 26);
            this.lblMeasureLabel.TabIndex = 3;
            this.lblMeasureLabel.Text = "TS測定距離:";
            this.lblMeasureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_Len2
            // 
            this.L_Len2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.L_Len2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.L_Len2.Location = new System.Drawing.Point(114, 100);
            this.L_Len2.Name = "L_Len2";
            this.L_Len2.Size = new System.Drawing.Size(200, 26);
            this.L_Len2.TabIndex = 4;
            this.L_Len2.Text = "--- m";
            this.L_Len2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblErrorLabel
            // 
            this.lblErrorLabel.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblErrorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.lblErrorLabel.Location = new System.Drawing.Point(6, 132);
            this.lblErrorLabel.Name = "lblErrorLabel";
            this.lblErrorLabel.Size = new System.Drawing.Size(104, 26);
            this.lblErrorLabel.TabIndex = 5;
            this.lblErrorLabel.Text = "距離誤差:";
            this.lblErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_Len3
            // 
            this.L_Len3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.L_Len3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(60)))));
            this.L_Len3.Location = new System.Drawing.Point(114, 132);
            this.L_Len3.Name = "L_Len3";
            this.L_Len3.Size = new System.Drawing.Size(200, 26);
            this.L_Len3.TabIndex = 6;
            this.L_Len3.Text = "--- m";
            this.L_Len3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGuidance
            // 
            this.lblGuidance.Font = new System.Drawing.Font("Yu Gothic UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblGuidance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(0)))));
            this.lblGuidance.Location = new System.Drawing.Point(14, 320);
            this.lblGuidance.Name = "lblGuidance";
            this.lblGuidance.Size = new System.Drawing.Size(326, 28);
            this.lblGuidance.TabIndex = 2;
            this.lblGuidance.Text = "⚡ 後視点を視準後、「０セット」を実行してください。";
            this.lblGuidance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(14, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 44);
            this.button1.TabIndex = 3;
            this.button1.Text = "🎯 ０セット (確定)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.button2.Location = new System.Drawing.Point(228, 354);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 44);
            this.button2.TabIndex = 4;
            this.button2.Text = "閉じる";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormKikaiDef
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(354, 412);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblGuidance);
            this.Controls.Add(this.grpMeasure);
            this.Controls.Add(this.grpPointSel);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKikaiDef";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "器械点・後視点設定 (既知2点)";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormKikaiDef_FormClosed);
            this.Shown += new System.EventHandler(this.FormKikaiDef_Shown);
            this.VisibleChanged += new System.EventHandler(this.FormKikaiDef_VisibleChanged);
            this.grpPointSel.ResumeLayout(false);
            this.grpMeasure.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPointSel;
        private System.Windows.Forms.Label lblKikai;
        public System.Windows.Forms.ComboBox CBSelKikaiP;
        private System.Windows.Forms.Label lblBack;
        public System.Windows.Forms.ComboBox CBSelBackP;
        private System.Windows.Forms.GroupBox grpMeasure;
        private System.Windows.Forms.Button buttonMesure01;
        private System.Windows.Forms.Label lblPlanLabel;
        internal System.Windows.Forms.Label L_Len1;
        private System.Windows.Forms.Label lblMeasureLabel;
        internal System.Windows.Forms.Label L_Len2;
        private System.Windows.Forms.Label lblErrorLabel;
        internal System.Windows.Forms.Label L_Len3;
        private System.Windows.Forms.Label lblGuidance;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
    }
}
