namespace Site7DbEditor
{
    partial class FormKikai
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
            this.timerMeasure = new System.Windows.Forms.Timer(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblMode = new System.Windows.Forms.Label();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.lblInstrH = new System.Windows.Forms.Label();
            this.txtInstrH = new System.Windows.Forms.TextBox();
            this.lblUnit1 = new System.Windows.Forms.Label();
            this.lblMirrorH = new System.Windows.Forms.Label();
            this.txtMirrorH = new System.Windows.Forms.TextBox();
            this.lblUnit2 = new System.Windows.Forms.Label();
            this.pnlCard1 = new System.Windows.Forms.Panel();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.cmbPoint1 = new System.Windows.Forms.ComboBox();
            this.btnMeasure1 = new System.Windows.Forms.Button();
            this.lblBMHeight1 = new System.Windows.Forms.Label();
            this.lblDistance1 = new System.Windows.Forms.Label();
            this.lblAngle1 = new System.Windows.Forms.Label();
            this.lblCalcZ1 = new System.Windows.Forms.Label();
            this.pnlCard2 = new System.Windows.Forms.Panel();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.cmbPoint2 = new System.Windows.Forms.ComboBox();
            this.btnMeasure2 = new System.Windows.Forms.Button();
            this.lblBMHeight2 = new System.Windows.Forms.Label();
            this.lblDistance2 = new System.Windows.Forms.Label();
            this.lblAngle2 = new System.Windows.Forms.Label();
            this.lblCalcZ2 = new System.Windows.Forms.Label();
            this.pnlCard3 = new System.Windows.Forms.Panel();
            this.lblTitle3 = new System.Windows.Forms.Label();
            this.cmbPoint3 = new System.Windows.Forms.ComboBox();
            this.btnMeasure3 = new System.Windows.Forms.Button();
            this.lblBMHeight3 = new System.Windows.Forms.Label();
            this.lblDistance3 = new System.Windows.Forms.Label();
            this.lblAngle3 = new System.Windows.Forms.Label();
            this.lblCalcZ3 = new System.Windows.Forms.Label();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.lblResultStatus = new System.Windows.Forms.Label();
            this.lblResultCoords = new System.Windows.Forms.Label();
            this.lblResultResidual = new System.Windows.Forms.Label();
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.lblNewName = new System.Windows.Forms.Label();
            this.txtNewPointName = new System.Windows.Forms.TextBox();
            this.lblNewLayer = new System.Windows.Forms.Label();
            this.cmbNewPointLayer = new System.Windows.Forms.ComboBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlCard1.SuspendLayout();
            this.pnlCard2.SuspendLayout();
            this.pnlCard3.SuspendLayout();
            this.pnlResult.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerMeasure
            // 
            this.timerMeasure.Tick += new System.EventHandler(this.TimerMeasure_Tick);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.lblMode);
            this.pnlTop.Controls.Add(this.cmbMode);
            this.pnlTop.Controls.Add(this.lblInstrH);
            this.pnlTop.Controls.Add(this.txtInstrH);
            this.pnlTop.Controls.Add(this.lblUnit1);
            this.pnlTop.Controls.Add(this.lblMirrorH);
            this.pnlTop.Controls.Add(this.txtMirrorH);
            this.pnlTop.Controls.Add(this.lblUnit2);
            this.pnlTop.Location = new System.Drawing.Point(14, 10);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(452, 78);
            this.pnlTop.TabIndex = 0;
            // 
            // lblMode
            // 
            this.lblMode.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMode.Location = new System.Drawing.Point(10, 10);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(54, 29);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "方式:";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            "後方交会 ２点指定",
            "後方交会 ３点指定"});
            this.cmbMode.Location = new System.Drawing.Point(68, 8);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(372, 29);
            this.cmbMode.TabIndex = 1;
            this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.CmbMode_SelectedIndexChanged);
            // 
            // lblInstrH
            // 
            this.lblInstrH.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblInstrH.Location = new System.Drawing.Point(10, 44);
            this.lblInstrH.Name = "lblInstrH";
            this.lblInstrH.Size = new System.Drawing.Size(65, 26);
            this.lblInstrH.TabIndex = 2;
            this.lblInstrH.Text = "器械高:";
            this.lblInstrH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInstrH
            // 
            this.txtInstrH.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtInstrH.Location = new System.Drawing.Point(78, 42);
            this.txtInstrH.Name = "txtInstrH";
            this.txtInstrH.Size = new System.Drawing.Size(75, 29);
            this.txtInstrH.TabIndex = 3;
            this.txtInstrH.Text = "1.500";
            this.txtInstrH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblUnit1
            // 
            this.lblUnit1.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblUnit1.Location = new System.Drawing.Point(156, 44);
            this.lblUnit1.Name = "lblUnit1";
            this.lblUnit1.Size = new System.Drawing.Size(24, 26);
            this.lblUnit1.TabIndex = 4;
            this.lblUnit1.Text = "m";
            this.lblUnit1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMirrorH
            // 
            this.lblMirrorH.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblMirrorH.Location = new System.Drawing.Point(216, 44);
            this.lblMirrorH.Name = "lblMirrorH";
            this.lblMirrorH.Size = new System.Drawing.Size(75, 26);
            this.lblMirrorH.TabIndex = 5;
            this.lblMirrorH.Text = "ミラー高:";
            this.lblMirrorH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMirrorH
            // 
            this.txtMirrorH.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtMirrorH.Location = new System.Drawing.Point(294, 42);
            this.txtMirrorH.Name = "txtMirrorH";
            this.txtMirrorH.Size = new System.Drawing.Size(75, 29);
            this.txtMirrorH.TabIndex = 6;
            this.txtMirrorH.Text = "1.200";
            this.txtMirrorH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblUnit2
            // 
            this.lblUnit2.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblUnit2.Location = new System.Drawing.Point(372, 44);
            this.lblUnit2.Name = "lblUnit2";
            this.lblUnit2.Size = new System.Drawing.Size(24, 26);
            this.lblUnit2.TabIndex = 7;
            this.lblUnit2.Text = "m";
            this.lblUnit2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCard1
            // 
            this.pnlCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard1.Controls.Add(this.lblTitle1);
            this.pnlCard1.Controls.Add(this.cmbPoint1);
            this.pnlCard1.Controls.Add(this.btnMeasure1);
            this.pnlCard1.Controls.Add(this.lblBMHeight1);
            this.pnlCard1.Controls.Add(this.lblDistance1);
            this.pnlCard1.Controls.Add(this.lblAngle1);
            this.pnlCard1.Controls.Add(this.lblCalcZ1);
            this.pnlCard1.Location = new System.Drawing.Point(14, 94);
            this.pnlCard1.Name = "pnlCard1";
            this.pnlCard1.Size = new System.Drawing.Size(452, 124);
            this.pnlCard1.TabIndex = 1;
            // 
            // lblTitle1
            // 
            this.lblTitle1.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.lblTitle1.Location = new System.Drawing.Point(10, 6);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(310, 24);
            this.lblTitle1.TabIndex = 0;
            this.lblTitle1.Text = "📍 1点目 (左側・時計回り開始)";
            // 
            // cmbPoint1
            // 
            this.cmbPoint1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoint1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.cmbPoint1.FormattingEnabled = true;
            this.cmbPoint1.Location = new System.Drawing.Point(10, 32);
            this.cmbPoint1.Name = "cmbPoint1";
            this.cmbPoint1.Size = new System.Drawing.Size(306, 29);
            this.cmbPoint1.TabIndex = 1;
            // 
            // btnMeasure1
            // 
            this.btnMeasure1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.btnMeasure1.FlatAppearance.BorderSize = 0;
            this.btnMeasure1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMeasure1.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnMeasure1.ForeColor = System.Drawing.Color.White;
            this.btnMeasure1.Location = new System.Drawing.Point(324, 30);
            this.btnMeasure1.Name = "btnMeasure1";
            this.btnMeasure1.Size = new System.Drawing.Size(116, 33);
            this.btnMeasure1.TabIndex = 2;
            this.btnMeasure1.Text = "🔭 測定";
            this.btnMeasure1.UseVisualStyleBackColor = false;
            // 
            // lblBMHeight1
            // 
            this.lblBMHeight1.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblBMHeight1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblBMHeight1.Location = new System.Drawing.Point(10, 68);
            this.lblBMHeight1.Name = "lblBMHeight1";
            this.lblBMHeight1.Size = new System.Drawing.Size(210, 24);
            this.lblBMHeight1.TabIndex = 3;
            this.lblBMHeight1.Text = "BM: --- m";
            // 
            // lblDistance1
            // 
            this.lblDistance1.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDistance1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lblDistance1.Location = new System.Drawing.Point(225, 68);
            this.lblDistance1.Name = "lblDistance1";
            this.lblDistance1.Size = new System.Drawing.Size(215, 24);
            this.lblDistance1.TabIndex = 4;
            this.lblDistance1.Text = "水平距離: 未測定";
            // 
            // lblAngle1
            // 
            this.lblAngle1.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblAngle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(90)))));
            this.lblAngle1.Location = new System.Drawing.Point(10, 94);
            this.lblAngle1.Name = "lblAngle1";
            this.lblAngle1.Size = new System.Drawing.Size(210, 24);
            this.lblAngle1.TabIndex = 5;
            this.lblAngle1.Text = "角度: ---";
            // 
            // lblCalcZ1
            // 
            this.lblCalcZ1.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCalcZ1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(130)))), ((int)(((byte)(60)))));
            this.lblCalcZ1.Location = new System.Drawing.Point(225, 94);
            this.lblCalcZ1.Name = "lblCalcZ1";
            this.lblCalcZ1.Size = new System.Drawing.Size(215, 24);
            this.lblCalcZ1.TabIndex = 6;
            this.lblCalcZ1.Text = "計算器械高: --- m";
            // 
            // pnlCard2
            // 
            this.pnlCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.pnlCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard2.Controls.Add(this.lblTitle2);
            this.pnlCard2.Controls.Add(this.cmbPoint2);
            this.pnlCard2.Controls.Add(this.btnMeasure2);
            this.pnlCard2.Controls.Add(this.lblBMHeight2);
            this.pnlCard2.Controls.Add(this.lblDistance2);
            this.pnlCard2.Controls.Add(this.lblAngle2);
            this.pnlCard2.Controls.Add(this.lblCalcZ2);
            this.pnlCard2.Location = new System.Drawing.Point(14, 224);
            this.pnlCard2.Name = "pnlCard2";
            this.pnlCard2.Size = new System.Drawing.Size(452, 124);
            this.pnlCard2.TabIndex = 2;
            // 
            // lblTitle2
            // 
            this.lblTitle2.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.lblTitle2.Location = new System.Drawing.Point(10, 6);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(310, 24);
            this.lblTitle2.TabIndex = 0;
            this.lblTitle2.Text = "📍 2点目 (右側)";
            // 
            // cmbPoint2
            // 
            this.cmbPoint2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoint2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.cmbPoint2.FormattingEnabled = true;
            this.cmbPoint2.Location = new System.Drawing.Point(10, 32);
            this.cmbPoint2.Name = "cmbPoint2";
            this.cmbPoint2.Size = new System.Drawing.Size(306, 29);
            this.cmbPoint2.TabIndex = 1;
            // 
            // btnMeasure2
            // 
            this.btnMeasure2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.btnMeasure2.FlatAppearance.BorderSize = 0;
            this.btnMeasure2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMeasure2.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnMeasure2.ForeColor = System.Drawing.Color.White;
            this.btnMeasure2.Location = new System.Drawing.Point(324, 30);
            this.btnMeasure2.Name = "btnMeasure2";
            this.btnMeasure2.Size = new System.Drawing.Size(116, 33);
            this.btnMeasure2.TabIndex = 2;
            this.btnMeasure2.Text = "🔭 測定";
            this.btnMeasure2.UseVisualStyleBackColor = false;
            // 
            // lblBMHeight2
            // 
            this.lblBMHeight2.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblBMHeight2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblBMHeight2.Location = new System.Drawing.Point(10, 68);
            this.lblBMHeight2.Name = "lblBMHeight2";
            this.lblBMHeight2.Size = new System.Drawing.Size(210, 24);
            this.lblBMHeight2.TabIndex = 3;
            this.lblBMHeight2.Text = "BM: --- m";
            // 
            // lblDistance2
            // 
            this.lblDistance2.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDistance2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lblDistance2.Location = new System.Drawing.Point(225, 68);
            this.lblDistance2.Name = "lblDistance2";
            this.lblDistance2.Size = new System.Drawing.Size(215, 24);
            this.lblDistance2.TabIndex = 4;
            this.lblDistance2.Text = "水平距離: 未測定";
            // 
            // lblAngle2
            // 
            this.lblAngle2.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblAngle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(90)))));
            this.lblAngle2.Location = new System.Drawing.Point(10, 94);
            this.lblAngle2.Name = "lblAngle2";
            this.lblAngle2.Size = new System.Drawing.Size(210, 24);
            this.lblAngle2.TabIndex = 5;
            this.lblAngle2.Text = "角度: ---";
            // 
            // lblCalcZ2
            // 
            this.lblCalcZ2.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCalcZ2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(130)))), ((int)(((byte)(60)))));
            this.lblCalcZ2.Location = new System.Drawing.Point(225, 94);
            this.lblCalcZ2.Name = "lblCalcZ2";
            this.lblCalcZ2.Size = new System.Drawing.Size(215, 24);
            this.lblCalcZ2.TabIndex = 6;
            this.lblCalcZ2.Text = "計算器械高: --- m";
            // 
            // pnlCard3
            // 
            this.pnlCard3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(235)))));
            this.pnlCard3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard3.Controls.Add(this.lblTitle3);
            this.pnlCard3.Controls.Add(this.cmbPoint3);
            this.pnlCard3.Controls.Add(this.btnMeasure3);
            this.pnlCard3.Controls.Add(this.lblBMHeight3);
            this.pnlCard3.Controls.Add(this.lblDistance3);
            this.pnlCard3.Controls.Add(this.lblAngle3);
            this.pnlCard3.Controls.Add(this.lblCalcZ3);
            this.pnlCard3.Location = new System.Drawing.Point(14, 354);
            this.pnlCard3.Name = "pnlCard3";
            this.pnlCard3.Size = new System.Drawing.Size(452, 124);
            this.pnlCard3.TabIndex = 3;
            // 
            // lblTitle3
            // 
            this.lblTitle3.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.lblTitle3.Location = new System.Drawing.Point(10, 6);
            this.lblTitle3.Name = "lblTitle3";
            this.lblTitle3.Size = new System.Drawing.Size(310, 24);
            this.lblTitle3.TabIndex = 0;
            this.lblTitle3.Text = "📍 3点目 (精度検定)";
            // 
            // cmbPoint3
            // 
            this.cmbPoint3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoint3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.cmbPoint3.FormattingEnabled = true;
            this.cmbPoint3.Location = new System.Drawing.Point(10, 32);
            this.cmbPoint3.Name = "cmbPoint3";
            this.cmbPoint3.Size = new System.Drawing.Size(306, 29);
            this.cmbPoint3.TabIndex = 1;
            // 
            // btnMeasure3
            // 
            this.btnMeasure3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.btnMeasure3.FlatAppearance.BorderSize = 0;
            this.btnMeasure3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMeasure3.Font = new System.Drawing.Font("Yu Gothic UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnMeasure3.ForeColor = System.Drawing.Color.White;
            this.btnMeasure3.Location = new System.Drawing.Point(324, 30);
            this.btnMeasure3.Name = "btnMeasure3";
            this.btnMeasure3.Size = new System.Drawing.Size(116, 33);
            this.btnMeasure3.TabIndex = 2;
            this.btnMeasure3.Text = "🔭 測定";
            this.btnMeasure3.UseVisualStyleBackColor = false;
            // 
            // lblBMHeight3
            // 
            this.lblBMHeight3.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblBMHeight3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblBMHeight3.Location = new System.Drawing.Point(10, 68);
            this.lblBMHeight3.Name = "lblBMHeight3";
            this.lblBMHeight3.Size = new System.Drawing.Size(210, 24);
            this.lblBMHeight3.TabIndex = 3;
            this.lblBMHeight3.Text = "BM: --- m";
            // 
            // lblDistance3
            // 
            this.lblDistance3.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDistance3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.lblDistance3.Location = new System.Drawing.Point(225, 68);
            this.lblDistance3.Name = "lblDistance3";
            this.lblDistance3.Size = new System.Drawing.Size(215, 24);
            this.lblDistance3.TabIndex = 4;
            this.lblDistance3.Text = "水平距離: 未測定";
            // 
            // lblAngle3
            // 
            this.lblAngle3.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblAngle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(90)))));
            this.lblAngle3.Location = new System.Drawing.Point(10, 94);
            this.lblAngle3.Name = "lblAngle3";
            this.lblAngle3.Size = new System.Drawing.Size(210, 24);
            this.lblAngle3.TabIndex = 5;
            this.lblAngle3.Text = "角度: ---";
            // 
            // lblCalcZ3
            // 
            this.lblCalcZ3.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCalcZ3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(130)))), ((int)(((byte)(60)))));
            this.lblCalcZ3.Location = new System.Drawing.Point(225, 94);
            this.lblCalcZ3.Name = "lblCalcZ3";
            this.lblCalcZ3.Size = new System.Drawing.Size(215, 24);
            this.lblCalcZ3.TabIndex = 6;
            this.lblCalcZ3.Text = "計算器械高: --- m";
            // 
            // pnlResult
            // 
            this.pnlResult.BackColor = System.Drawing.Color.White;
            this.pnlResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResult.Controls.Add(this.lblResultStatus);
            this.pnlResult.Controls.Add(this.lblResultCoords);
            this.pnlResult.Controls.Add(this.lblResultResidual);
            this.pnlResult.Location = new System.Drawing.Point(14, 484);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(452, 138);
            this.pnlResult.TabIndex = 4;
            // 
            // lblResultStatus
            // 
            this.lblResultStatus.Font = new System.Drawing.Font("Yu Gothic UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblResultStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblResultStatus.Location = new System.Drawing.Point(10, 8);
            this.lblResultStatus.Name = "lblResultStatus";
            this.lblResultStatus.Size = new System.Drawing.Size(430, 24);
            this.lblResultStatus.TabIndex = 0;
            this.lblResultStatus.Text = "⚡ 基準点を指定し、「🔭 測定」を実行してください。";
            // 
            // lblResultCoords
            // 
            this.lblResultCoords.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblResultCoords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(30)))));
            this.lblResultCoords.Location = new System.Drawing.Point(10, 34);
            this.lblResultCoords.Name = "lblResultCoords";
            this.lblResultCoords.Size = new System.Drawing.Size(430, 68);
            this.lblResultCoords.TabIndex = 1;
            this.lblResultCoords.Text = "器械点座標:\r\n  X = --- m\r\n  Y = --- m\r\n  Z = --- m";
            // 
            // lblResultResidual
            // 
            this.lblResultResidual.Font = new System.Drawing.Font("Yu Gothic UI", 11F);
            this.lblResultResidual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.lblResultResidual.Location = new System.Drawing.Point(10, 104);
            this.lblResultResidual.Name = "lblResultResidual";
            this.lblResultResidual.Size = new System.Drawing.Size(430, 26);
            this.lblResultResidual.TabIndex = 2;
            this.lblResultResidual.Text = "残差: --- mm";
            // 
            // pnlRegister
            // 
            this.pnlRegister.Controls.Add(this.lblNewName);
            this.pnlRegister.Controls.Add(this.txtNewPointName);
            this.pnlRegister.Controls.Add(this.lblNewLayer);
            this.pnlRegister.Controls.Add(this.cmbNewPointLayer);
            this.pnlRegister.Controls.Add(this.btnRegister);
            this.pnlRegister.Controls.Add(this.btnClose);
            this.pnlRegister.Location = new System.Drawing.Point(14, 628);
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.Size = new System.Drawing.Size(452, 90);
            this.pnlRegister.TabIndex = 5;
            // 
            // lblNewName
            // 
            this.lblNewName.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNewName.Location = new System.Drawing.Point(0, 6);
            this.lblNewName.Name = "lblNewName";
            this.lblNewName.Size = new System.Drawing.Size(65, 29);
            this.lblNewName.TabIndex = 0;
            this.lblNewName.Text = "登録名:";
            this.lblNewName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNewPointName
            // 
            this.txtNewPointName.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtNewPointName.Location = new System.Drawing.Point(68, 4);
            this.txtNewPointName.Name = "txtNewPointName";
            this.txtNewPointName.Size = new System.Drawing.Size(120, 29);
            this.txtNewPointName.TabIndex = 1;
            // 
            // lblNewLayer
            // 
            this.lblNewLayer.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNewLayer.Location = new System.Drawing.Point(200, 6);
            this.lblNewLayer.Name = "lblNewLayer";
            this.lblNewLayer.Size = new System.Drawing.Size(65, 29);
            this.lblNewLayer.TabIndex = 2;
            this.lblNewLayer.Text = "レイヤ:";
            this.lblNewLayer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbNewPointLayer
            // 
            this.cmbNewPointLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewPointLayer.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.cmbNewPointLayer.FormattingEnabled = true;
            this.cmbNewPointLayer.Location = new System.Drawing.Point(268, 4);
            this.cmbNewPointLayer.Name = "cmbNewPointLayer";
            this.cmbNewPointLayer.Size = new System.Drawing.Size(184, 29);
            this.cmbNewPointLayer.TabIndex = 3;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(0, 42);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(310, 44);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "💾 基準点リストへ登録";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.btnClose.Location = new System.Drawing.Point(324, 42);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 44);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // FormKikai
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(480, 730);
            this.Controls.Add(this.pnlRegister);
            this.Controls.Add(this.pnlResult);
            this.Controls.Add(this.pnlCard3);
            this.Controls.Add(this.pnlCard2);
            this.Controls.Add(this.pnlCard1);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKikai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "器械点測定 (後方交会法)";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlCard1.ResumeLayout(false);
            this.pnlCard2.ResumeLayout(false);
            this.pnlCard3.ResumeLayout(false);
            this.pnlResult.ResumeLayout(false);
            this.pnlRegister.ResumeLayout(false);
            this.pnlRegister.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerMeasure;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Label lblInstrH;
        private System.Windows.Forms.TextBox txtInstrH;
        private System.Windows.Forms.Label lblUnit1;
        private System.Windows.Forms.Label lblMirrorH;
        private System.Windows.Forms.TextBox txtMirrorH;
        private System.Windows.Forms.Label lblUnit2;
        private System.Windows.Forms.Panel pnlCard1;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.ComboBox cmbPoint1;
        private System.Windows.Forms.Button btnMeasure1;
        private System.Windows.Forms.Label lblBMHeight1;
        private System.Windows.Forms.Label lblDistance1;
        private System.Windows.Forms.Label lblAngle1;
        private System.Windows.Forms.Label lblCalcZ1;
        private System.Windows.Forms.Panel pnlCard2;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.ComboBox cmbPoint2;
        private System.Windows.Forms.Button btnMeasure2;
        private System.Windows.Forms.Label lblBMHeight2;
        private System.Windows.Forms.Label lblDistance2;
        private System.Windows.Forms.Label lblAngle2;
        private System.Windows.Forms.Label lblCalcZ2;
        private System.Windows.Forms.Panel pnlCard3;
        private System.Windows.Forms.Label lblTitle3;
        private System.Windows.Forms.ComboBox cmbPoint3;
        private System.Windows.Forms.Button btnMeasure3;
        private System.Windows.Forms.Label lblBMHeight3;
        private System.Windows.Forms.Label lblDistance3;
        private System.Windows.Forms.Label lblAngle3;
        private System.Windows.Forms.Label lblCalcZ3;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Label lblResultStatus;
        private System.Windows.Forms.Label lblResultCoords;
        private System.Windows.Forms.Label lblResultResidual;
        private System.Windows.Forms.Panel pnlRegister;
        private System.Windows.Forms.Label lblNewName;
        private System.Windows.Forms.TextBox txtNewPointName;
        private System.Windows.Forms.Label lblNewLayer;
        private System.Windows.Forms.ComboBox cmbNewPointLayer;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnClose;
    }
}
