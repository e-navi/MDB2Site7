namespace Site7DrawingEditor
{
    partial class FormIkou3D
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
            this.picCanvas3D = new System.Windows.Forms.PictureBox();
            this.panelRightControls = new System.Windows.Forms.Panel();
            
            // Grid Settings
            this.grpGridSettings = new System.Windows.Forms.GroupBox();
            this.lblGridRes = new System.Windows.Forms.Label();
            this.cmbGridResolution = new System.Windows.Forms.ComboBox();
            this.chkIkouHeight = new System.Windows.Forms.CheckBox();
            this.btnGridCalc = new System.Windows.Forms.Button();

            // View Modes & Rotation
            this.panelViewToggle = new System.Windows.Forms.Panel();
            this.btnView2D = new System.Windows.Forms.Button();
            this.btnView3D = new System.Windows.Forms.Button();
            this.grpRotationControls = new System.Windows.Forms.GroupBox();
            this.lblRotateV = new System.Windows.Forms.Label();
            this.tbRotateV = new System.Windows.Forms.TrackBar();
            this.lblRotateH = new System.Windows.Forms.Label();
            this.tbRotateH = new System.Windows.Forms.TrackBar();

            // Section Specification Controls
            this.grpDanmenControls = new System.Windows.Forms.GroupBox();
            this.btnSectionPick = new System.Windows.Forms.Button();
            this.chkShowElevation = new System.Windows.Forms.CheckBox();

            // Summary Info
            this.grpIkouSummary = new System.Windows.Forms.GroupBox();
            this.lblIkouName = new System.Windows.Forms.Label();
            this.txtIkouName = new System.Windows.Forms.TextBox();
            this.lblND = new System.Windows.Forms.Label();
            this.txtND = new System.Windows.Forms.TextBox();
            this.lblN = new System.Windows.Forms.Label();
            this.txtN = new System.Windows.Forms.TextBox();
            this.lblMinX = new System.Windows.Forms.Label();
            this.txtMinX = new System.Windows.Forms.TextBox();
            this.lblMinY = new System.Windows.Forms.Label();
            this.txtMinY = new System.Windows.Forms.TextBox();
            this.lblMaxX = new System.Windows.Forms.Label();
            this.txtMaxX = new System.Windows.Forms.TextBox();
            this.lblMaxY = new System.Windows.Forms.Label();
            this.txtMaxY = new System.Windows.Forms.TextBox();

            // Bottom Buttons
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDanmenSet = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.picCanvas3D)).BeginInit();
            this.panelRightControls.SuspendLayout();
            this.grpGridSettings.SuspendLayout();
            this.panelViewToggle.SuspendLayout();
            this.grpRotationControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRotateV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRotateH)).BeginInit();
            this.grpDanmenControls.SuspendLayout();
            this.grpIkouSummary.SuspendLayout();
            this.SuspendLayout();

            // picCanvas3D
            this.picCanvas3D.BackColor = System.Drawing.Color.White;
            this.picCanvas3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCanvas3D.Location = new System.Drawing.Point(0, 0);
            this.picCanvas3D.Name = "picCanvas3D";
            this.picCanvas3D.Size = new System.Drawing.Size(680, 640);
            this.picCanvas3D.TabIndex = 0;
            this.picCanvas3D.TabStop = false;

            // panelRightControls
            this.panelRightControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.panelRightControls.Controls.Add(this.grpGridSettings);
            this.panelRightControls.Controls.Add(this.panelViewToggle);
            this.panelRightControls.Controls.Add(this.grpRotationControls);
            this.panelRightControls.Controls.Add(this.grpDanmenControls);
            this.panelRightControls.Controls.Add(this.grpIkouSummary);
            this.panelRightControls.Controls.Add(this.btnCancel);
            this.panelRightControls.Controls.Add(this.btnDanmenSet);
            this.panelRightControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightControls.Location = new System.Drawing.Point(680, 0);
            this.panelRightControls.Name = "panelRightControls";
            this.panelRightControls.Padding = new System.Windows.Forms.Padding(10);
            this.panelRightControls.Size = new System.Drawing.Size(240, 640);
            this.panelRightControls.TabIndex = 1;

            // grpGridSettings
            this.grpGridSettings.Controls.Add(this.lblGridRes);
            this.grpGridSettings.Controls.Add(this.cmbGridResolution);
            this.grpGridSettings.Controls.Add(this.chkIkouHeight);
            this.grpGridSettings.Controls.Add(this.btnGridCalc);
            this.grpGridSettings.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpGridSettings.Location = new System.Drawing.Point(10, 10);
            this.grpGridSettings.Name = "grpGridSettings";
            this.grpGridSettings.Size = new System.Drawing.Size(220, 115);
            this.grpGridSettings.TabIndex = 0;
            this.grpGridSettings.TabStop = false;
            this.grpGridSettings.Text = "グリッド設定";

            // lblGridRes
            this.lblGridRes.AutoSize = true;
            this.lblGridRes.Location = new System.Drawing.Point(8, 20);
            this.lblGridRes.Name = "lblGridRes";
            this.lblGridRes.Size = new System.Drawing.Size(31, 15);
            this.lblGridRes.Text = "間隔";

            // cmbGridResolution
            this.cmbGridResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGridResolution.FormattingEnabled = true;
            this.cmbGridResolution.Items.AddRange(new object[] {
            "大(縦横 25分割)",
            "中(縦横 50分割)",
            "小(縦横 100分割)"});
            this.cmbGridResolution.Location = new System.Drawing.Point(8, 38);
            this.cmbGridResolution.Name = "cmbGridResolution";
            this.cmbGridResolution.Size = new System.Drawing.Size(204, 23);
            this.cmbGridResolution.TabIndex = 1;

            // chkIkouHeight
            this.chkIkouHeight.AutoSize = true;
            this.chkIkouHeight.Checked = true;
            this.chkIkouHeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIkouHeight.Location = new System.Drawing.Point(8, 68);
            this.chkIkouHeight.Name = "chkIkouHeight";
            this.chkIkouHeight.Size = new System.Drawing.Size(86, 19);
            this.chkIkouHeight.TabIndex = 2;
            this.chkIkouHeight.Text = "遺構線高さ";

            // btnGridCalc
            this.btnGridCalc.Location = new System.Drawing.Point(100, 65);
            this.btnGridCalc.Name = "btnGridCalc";
            this.btnGridCalc.Size = new System.Drawing.Size(112, 26);
            this.btnGridCalc.TabIndex = 3;
            this.btnGridCalc.Text = "グリッド計算";

            // panelViewToggle
            this.panelViewToggle.Controls.Add(this.btnView2D);
            this.panelViewToggle.Controls.Add(this.btnView3D);
            this.panelViewToggle.Location = new System.Drawing.Point(10, 130);
            this.panelViewToggle.Name = "panelViewToggle";
            this.panelViewToggle.Size = new System.Drawing.Size(220, 32);
            this.panelViewToggle.TabIndex = 1;

            // btnView2D
            this.btnView2D.Location = new System.Drawing.Point(0, 2);
            this.btnView2D.Name = "btnView2D";
            this.btnView2D.Size = new System.Drawing.Size(106, 28);
            this.btnView2D.TabIndex = 0;
            this.btnView2D.Text = "2D表示";

            // btnView3D
            this.btnView3D.Location = new System.Drawing.Point(112, 2);
            this.btnView3D.Name = "btnView3D";
            this.btnView3D.Size = new System.Drawing.Size(106, 28);
            this.btnView3D.TabIndex = 1;
            this.btnView3D.Text = "3D表示";

            // grpRotationControls
            this.grpRotationControls.Controls.Add(this.lblRotateV);
            this.grpRotationControls.Controls.Add(this.tbRotateV);
            this.grpRotationControls.Controls.Add(this.lblRotateH);
            this.grpRotationControls.Controls.Add(this.tbRotateH);
            this.grpRotationControls.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpRotationControls.Location = new System.Drawing.Point(10, 165);
            this.grpRotationControls.Name = "grpRotationControls";
            this.grpRotationControls.Size = new System.Drawing.Size(220, 110);
            this.grpRotationControls.TabIndex = 2;
            this.grpRotationControls.TabStop = false;
            this.grpRotationControls.Visible = false;

            // lblRotateV
            this.lblRotateV.AutoSize = true;
            this.lblRotateV.Location = new System.Drawing.Point(6, 16);
            this.lblRotateV.Name = "lblRotateV";
            this.lblRotateV.Size = new System.Drawing.Size(147, 15);
            this.lblRotateV.Text = "垂直回転 (右Btn 上下)";

            // tbRotateV
            this.tbRotateV.Location = new System.Drawing.Point(6, 32);
            this.tbRotateV.Maximum = 80;
            this.tbRotateV.Minimum = -80;
            this.tbRotateV.Name = "tbRotateV";
            this.tbRotateV.Size = new System.Drawing.Size(208, 25);
            this.tbRotateV.TabIndex = 1;
            this.tbRotateV.Value = 35;

            // lblRotateH
            this.lblRotateH.AutoSize = true;
            this.lblRotateH.Location = new System.Drawing.Point(6, 58);
            this.lblRotateH.Name = "lblRotateH";
            this.lblRotateH.Size = new System.Drawing.Size(147, 15);
            this.lblRotateH.Text = "水平回転 (右Btn 左右)";

            // tbRotateH
            this.tbRotateH.Location = new System.Drawing.Point(6, 74);
            this.tbRotateH.Maximum = 180;
            this.tbRotateH.Minimum = -180;
            this.tbRotateH.Name = "tbRotateH";
            this.tbRotateH.Size = new System.Drawing.Size(208, 25);
            this.tbRotateH.TabIndex = 3;
            this.tbRotateH.Value = -45;

            // grpDanmenControls
            this.grpDanmenControls.Controls.Add(this.btnSectionPick);
            this.grpDanmenControls.Controls.Add(this.chkShowElevation);
            this.grpDanmenControls.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpDanmenControls.Location = new System.Drawing.Point(10, 165);
            this.grpDanmenControls.Name = "grpDanmenControls";
            this.grpDanmenControls.Size = new System.Drawing.Size(220, 80);
            this.grpDanmenControls.TabIndex = 3;
            this.grpDanmenControls.TabStop = false;

            // btnSectionPick
            this.btnSectionPick.Location = new System.Drawing.Point(15, 18);
            this.btnSectionPick.Name = "btnSectionPick";
            this.btnSectionPick.Size = new System.Drawing.Size(190, 30);
            this.btnSectionPick.TabIndex = 0;
            this.btnSectionPick.Text = "断面指示";

            // chkShowElevation
            this.chkShowElevation.AutoSize = true;
            this.chkShowElevation.Location = new System.Drawing.Point(15, 52);
            this.chkShowElevation.Name = "chkShowElevation";
            this.chkShowElevation.Size = new System.Drawing.Size(62, 19);
            this.chkShowElevation.TabIndex = 1;
            this.chkShowElevation.Text = "標高値";

            // grpIkouSummary
            this.grpIkouSummary.Controls.Add(this.lblIkouName);
            this.grpIkouSummary.Controls.Add(this.txtIkouName);
            this.grpIkouSummary.Controls.Add(this.lblND);
            this.grpIkouSummary.Controls.Add(this.txtND);
            this.grpIkouSummary.Controls.Add(this.lblN);
            this.grpIkouSummary.Controls.Add(this.txtN);
            this.grpIkouSummary.Controls.Add(this.lblMinX);
            this.grpIkouSummary.Controls.Add(this.txtMinX);
            this.grpIkouSummary.Controls.Add(this.lblMinY);
            this.grpIkouSummary.Controls.Add(this.txtMinY);
            this.grpIkouSummary.Controls.Add(this.lblMaxX);
            this.grpIkouSummary.Controls.Add(this.txtMaxX);
            this.grpIkouSummary.Controls.Add(this.lblMaxY);
            this.grpIkouSummary.Controls.Add(this.txtMaxY);
            this.grpIkouSummary.Font = new System.Drawing.Font("Yu Gothic UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpIkouSummary.Location = new System.Drawing.Point(10, 280);
            this.grpIkouSummary.Name = "grpIkouSummary";
            this.grpIkouSummary.Size = new System.Drawing.Size(220, 260);
            this.grpIkouSummary.TabIndex = 4;
            this.grpIkouSummary.TabStop = false;
            this.grpIkouSummary.Text = "遺構概要";

            // lblIkouName
            this.lblIkouName.AutoSize = true;
            this.lblIkouName.Location = new System.Drawing.Point(10, 22);
            this.lblIkouName.Name = "lblIkouName";
            this.lblIkouName.Size = new System.Drawing.Size(43, 15);
            this.lblIkouName.TabIndex = 0;
            this.lblIkouName.Text = "遺構名";

            // txtIkouName
            this.txtIkouName.ReadOnly = true;
            this.txtIkouName.Location = new System.Drawing.Point(70, 19);
            this.txtIkouName.Name = "txtIkouName";
            this.txtIkouName.Size = new System.Drawing.Size(140, 23);
            this.txtIkouName.TabIndex = 1;

            // lblND
            this.lblND.AutoSize = true;
            this.lblND.Location = new System.Drawing.Point(10, 52);
            this.lblND.Name = "lblND";
            this.lblND.Size = new System.Drawing.Size(24, 15);
            this.lblND.TabIndex = 2;
            this.lblND.Text = "ND";

            // txtND
            this.txtND.ReadOnly = true;
            this.txtND.Location = new System.Drawing.Point(70, 49);
            this.txtND.Name = "txtND";
            this.txtND.Size = new System.Drawing.Size(140, 23);
            this.txtND.TabIndex = 3;

            // lblN
            this.lblN.AutoSize = true;
            this.lblN.Location = new System.Drawing.Point(10, 82);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(16, 15);
            this.lblN.TabIndex = 4;
            this.lblN.Text = "N";

            // txtN
            this.txtN.ReadOnly = true;
            this.txtN.Location = new System.Drawing.Point(70, 79);
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(140, 23);
            this.txtN.TabIndex = 5;

            // lblMinX
            this.lblMinX.AutoSize = true;
            this.lblMinX.Location = new System.Drawing.Point(10, 112);
            this.lblMinX.Name = "lblMinX";
            this.lblMinX.Size = new System.Drawing.Size(35, 15);
            this.lblMinX.TabIndex = 6;
            this.lblMinX.Text = "minX";

            // txtMinX
            this.txtMinX.ReadOnly = true;
            this.txtMinX.Location = new System.Drawing.Point(70, 109);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Size = new System.Drawing.Size(140, 23);
            this.txtMinX.TabIndex = 7;

            // lblMinY
            this.lblMinY.AutoSize = true;
            this.lblMinY.Location = new System.Drawing.Point(10, 142);
            this.lblMinY.Name = "lblMinY";
            this.lblMinY.Size = new System.Drawing.Size(35, 15);
            this.lblMinY.TabIndex = 8;
            this.lblMinY.Text = "minY";

            // txtMinY
            this.txtMinY.ReadOnly = true;
            this.txtMinY.Location = new System.Drawing.Point(70, 139);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Size = new System.Drawing.Size(140, 23);
            this.txtMinY.TabIndex = 9;

            // lblMaxX
            this.lblMaxX.AutoSize = true;
            this.lblMaxX.Location = new System.Drawing.Point(10, 172);
            this.lblMaxX.Name = "lblMaxX";
            this.lblMaxX.Size = new System.Drawing.Size(37, 15);
            this.lblMaxX.TabIndex = 10;
            this.lblMaxX.Text = "maxX";

            // txtMaxX
            this.txtMaxX.ReadOnly = true;
            this.txtMaxX.Location = new System.Drawing.Point(70, 169);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(140, 23);
            this.txtMaxX.TabIndex = 11;

            // lblMaxY
            this.lblMaxY.AutoSize = true;
            this.lblMaxY.Location = new System.Drawing.Point(10, 202);
            this.lblMaxY.Name = "lblMaxY";
            this.lblMaxY.Size = new System.Drawing.Size(37, 15);
            this.lblMaxY.TabIndex = 12;
            this.lblMaxY.Text = "maxY";

            // txtMaxY
            this.txtMaxY.ReadOnly = true;
            this.txtMaxY.Location = new System.Drawing.Point(70, 199);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(140, 23);
            this.txtMaxY.TabIndex = 13;

            // btnCancel
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(10, 595);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;

            // btnDanmenSet
            this.btnDanmenSet.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDanmenSet.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDanmenSet.Location = new System.Drawing.Point(120, 595);
            this.btnDanmenSet.Name = "btnDanmenSet";
            this.btnDanmenSet.Size = new System.Drawing.Size(110, 32);
            this.btnDanmenSet.TabIndex = 6;
            this.btnDanmenSet.Text = "戻る";
            this.btnDanmenSet.UseVisualStyleBackColor = true;

            // FormIkou3D
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 640);
            this.Controls.Add(this.picCanvas3D);
            this.Controls.Add(this.panelRightControls);
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "FormIkou3D";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "遺構 3D";

            ((System.ComponentModel.ISupportInitialize)(this.picCanvas3D)).BeginInit();
            this.panelRightControls.ResumeLayout(false);
            this.grpGridSettings.ResumeLayout(false);
            this.grpGridSettings.PerformLayout();
            this.panelViewToggle.ResumeLayout(false);
            this.grpRotationControls.ResumeLayout(false);
            this.grpRotationControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRotateV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRotateH)).BeginInit();
            this.grpDanmenControls.ResumeLayout(false);
            this.grpDanmenControls.PerformLayout();
            this.grpIkouSummary.ResumeLayout(false);
            this.grpIkouSummary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas3D;
        private System.Windows.Forms.Panel panelRightControls;
        private System.Windows.Forms.GroupBox grpGridSettings;
        private System.Windows.Forms.Label lblGridRes;
        private System.Windows.Forms.ComboBox cmbGridResolution;
        private System.Windows.Forms.CheckBox chkIkouHeight;
        private System.Windows.Forms.Button btnGridCalc;
        private System.Windows.Forms.Panel panelViewToggle;
        private System.Windows.Forms.Button btnView2D;
        private System.Windows.Forms.Button btnView3D;
        private System.Windows.Forms.GroupBox grpRotationControls;
        private System.Windows.Forms.Label lblRotateV;
        private System.Windows.Forms.TrackBar tbRotateV;
        private System.Windows.Forms.Label lblRotateH;
        private System.Windows.Forms.TrackBar tbRotateH;
        private System.Windows.Forms.GroupBox grpDanmenControls;
        private System.Windows.Forms.Button btnSectionPick;
        private System.Windows.Forms.CheckBox chkShowElevation;
        private System.Windows.Forms.GroupBox grpIkouSummary;
        private System.Windows.Forms.Label lblIkouName;
        private System.Windows.Forms.TextBox txtIkouName;
        private System.Windows.Forms.Label lblND;
        private System.Windows.Forms.TextBox txtND;
        private System.Windows.Forms.Label lblN;
        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.Label lblMinX;
        private System.Windows.Forms.TextBox txtMinX;
        private System.Windows.Forms.Label lblMinY;
        private System.Windows.Forms.TextBox txtMinY;
        private System.Windows.Forms.Label lblMaxX;
        private System.Windows.Forms.TextBox txtMaxX;
        private System.Windows.Forms.Label lblMaxY;
        private System.Windows.Forms.TextBox txtMaxY;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDanmenSet;
    }
}
