namespace Site7DbEditor
{
    partial class FormBackgroundSettings
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpBgImage = new System.Windows.Forms.GroupBox();
            this.lblImgFile = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.lblK1 = new System.Windows.Forms.Label();
            this.cmbKikai1 = new System.Windows.Forms.ComboBox();
            this.lblX1 = new System.Windows.Forms.Label();
            this.txtKikai1X = new System.Windows.Forms.TextBox();
            this.lblY1 = new System.Windows.Forms.Label();
            this.txtKikai1Y = new System.Windows.Forms.TextBox();
            this.btnSetPoint1 = new System.Windows.Forms.Button();
            this.lblPoint1Pix = new System.Windows.Forms.Label();
            this.lblK2 = new System.Windows.Forms.Label();
            this.cmbKikai2 = new System.Windows.Forms.ComboBox();
            this.lblX2 = new System.Windows.Forms.Label();
            this.txtKikai2X = new System.Windows.Forms.TextBox();
            this.lblY2 = new System.Windows.Forms.Label();
            this.txtKikai2Y = new System.Windows.Forms.TextBox();
            this.btnSetPoint2 = new System.Windows.Forms.Button();
            this.lblPoint2Pix = new System.Windows.Forms.Label();
            this.btnSwap = new System.Windows.Forms.Button();
            this.lblOpacity = new System.Windows.Forms.Label();
            this.trkOpacity = new System.Windows.Forms.TrackBar();
            this.lblOpacityVal = new System.Windows.Forms.Label();
            this.grpPointCloud = new System.Windows.Forms.GroupBox();
            this.lblPcFile = new System.Windows.Forms.Label();
            this.txtPointCloudPath = new System.Windows.Forms.TextBox();
            this.btnBrowsePointCloud = new System.Windows.Forms.Button();
            this.btnClearPointCloud = new System.Windows.Forms.Button();
            this.chkSwapPointCloudXY = new System.Windows.Forms.CheckBox();
            this.lblPointCloudStatus = new System.Windows.Forms.Label();
            this.btnOpen3D = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.lblStatusGuide = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.grpBgImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).BeginInit();
            this.grpPointCloud.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.AutoScroll = true;
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pnlLeft.Controls.Add(this.grpBgImage);
            this.pnlLeft.Controls.Add(this.grpPointCloud);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlLeft.Size = new System.Drawing.Size(430, 668);
            this.pnlLeft.TabIndex = 0;
            // 
            // grpBgImage
            // 
            this.grpBgImage.BackColor = System.Drawing.Color.White;
            this.grpBgImage.Controls.Add(this.lblImgFile);
            this.grpBgImage.Controls.Add(this.txtImagePath);
            this.grpBgImage.Controls.Add(this.btnBrowseImage);
            this.grpBgImage.Controls.Add(this.lblK1);
            this.grpBgImage.Controls.Add(this.cmbKikai1);
            this.grpBgImage.Controls.Add(this.lblX1);
            this.grpBgImage.Controls.Add(this.txtKikai1X);
            this.grpBgImage.Controls.Add(this.lblY1);
            this.grpBgImage.Controls.Add(this.txtKikai1Y);
            this.grpBgImage.Controls.Add(this.btnSetPoint1);
            this.grpBgImage.Controls.Add(this.lblPoint1Pix);
            this.grpBgImage.Controls.Add(this.lblK2);
            this.grpBgImage.Controls.Add(this.cmbKikai2);
            this.grpBgImage.Controls.Add(this.lblX2);
            this.grpBgImage.Controls.Add(this.txtKikai2X);
            this.grpBgImage.Controls.Add(this.lblY2);
            this.grpBgImage.Controls.Add(this.txtKikai2Y);
            this.grpBgImage.Controls.Add(this.btnSetPoint2);
            this.grpBgImage.Controls.Add(this.lblPoint2Pix);
            this.grpBgImage.Controls.Add(this.btnSwap);
            this.grpBgImage.Controls.Add(this.lblOpacity);
            this.grpBgImage.Controls.Add(this.trkOpacity);
            this.grpBgImage.Controls.Add(this.lblOpacityVal);
            this.grpBgImage.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpBgImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpBgImage.Location = new System.Drawing.Point(10, 8);
            this.grpBgImage.Name = "grpBgImage";
            this.grpBgImage.Size = new System.Drawing.Size(410, 396);
            this.grpBgImage.TabIndex = 0;
            this.grpBgImage.TabStop = false;
            this.grpBgImage.Text = "🗺 背景画像設定";
            // 
            // lblImgFile
            // 
            this.lblImgFile.AutoSize = true;
            this.lblImgFile.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblImgFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblImgFile.Location = new System.Drawing.Point(12, 28);
            this.lblImgFile.Name = "lblImgFile";
            this.lblImgFile.Size = new System.Drawing.Size(89, 21);
            this.lblImgFile.TabIndex = 0;
            this.lblImgFile.Text = "画像ファイル:";
            // 
            // txtImagePath
            // 
            this.txtImagePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.txtImagePath.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.txtImagePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtImagePath.Location = new System.Drawing.Point(12, 54);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.ReadOnly = true;
            this.txtImagePath.Size = new System.Drawing.Size(296, 29);
            this.txtImagePath.TabIndex = 1;
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(248)))));
            this.btnBrowseImage.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBrowseImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(100)))));
            this.btnBrowseImage.Location = new System.Drawing.Point(314, 53);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(84, 31);
            this.btnBrowseImage.TabIndex = 2;
            this.btnBrowseImage.Text = "参照...";
            this.btnBrowseImage.UseVisualStyleBackColor = false;
            this.btnBrowseImage.Click += new System.EventHandler(this.BtnBrowseImage_Click);
            // 
            // lblK1
            // 
            this.lblK1.AutoSize = true;
            this.lblK1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblK1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblK1.Location = new System.Drawing.Point(12, 94);
            this.lblK1.Name = "lblK1";
            this.lblK1.Size = new System.Drawing.Size(73, 21);
            this.lblK1.TabIndex = 3;
            this.lblK1.Text = "基準点 1:";
            // 
            // cmbKikai1
            // 
            this.cmbKikai1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKikai1.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbKikai1.FormattingEnabled = true;
            this.cmbKikai1.Location = new System.Drawing.Point(90, 90);
            this.cmbKikai1.Name = "cmbKikai1";
            this.cmbKikai1.Size = new System.Drawing.Size(308, 29);
            this.cmbKikai1.TabIndex = 4;
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblX1.Location = new System.Drawing.Point(12, 129);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(22, 21);
            this.lblX1.TabIndex = 5;
            this.lblX1.Text = "X:";
            // 
            // txtKikai1X
            // 
            this.txtKikai1X.BackColor = System.Drawing.Color.White;
            this.txtKikai1X.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.txtKikai1X.ForeColor = System.Drawing.Color.Black;
            this.txtKikai1X.Location = new System.Drawing.Point(36, 125);
            this.txtKikai1X.Name = "txtKikai1X";
            this.txtKikai1X.Size = new System.Drawing.Size(130, 29);
            this.txtKikai1X.TabIndex = 6;
            // 
            // lblY1
            // 
            this.lblY1.AutoSize = true;
            this.lblY1.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblY1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblY1.Location = new System.Drawing.Point(176, 129);
            this.lblY1.Name = "lblY1";
            this.lblY1.Size = new System.Drawing.Size(22, 21);
            this.lblY1.TabIndex = 7;
            this.lblY1.Text = "Y:";
            // 
            // txtKikai1Y
            // 
            this.txtKikai1Y.BackColor = System.Drawing.Color.White;
            this.txtKikai1Y.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.txtKikai1Y.ForeColor = System.Drawing.Color.Black;
            this.txtKikai1Y.Location = new System.Drawing.Point(200, 125);
            this.txtKikai1Y.Name = "txtKikai1Y";
            this.txtKikai1Y.Size = new System.Drawing.Size(130, 29);
            this.txtKikai1Y.TabIndex = 8;
            // 
            // btnSetPoint1
            // 
            this.btnSetPoint1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnSetPoint1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSetPoint1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSetPoint1.Location = new System.Drawing.Point(12, 160);
            this.btnSetPoint1.Name = "btnSetPoint1";
            this.btnSetPoint1.Size = new System.Drawing.Size(140, 32);
            this.btnSetPoint1.TabIndex = 9;
            this.btnSetPoint1.Text = "🎯 点1を指示";
            this.btnSetPoint1.UseVisualStyleBackColor = false;
            // 
            // lblPoint1Pix
            // 
            this.lblPoint1Pix.AutoSize = true;
            this.lblPoint1Pix.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPoint1Pix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblPoint1Pix.Location = new System.Drawing.Point(160, 166);
            this.lblPoint1Pix.Name = "lblPoint1Pix";
            this.lblPoint1Pix.Size = new System.Drawing.Size(58, 21);
            this.lblPoint1Pix.TabIndex = 10;
            this.lblPoint1Pix.Text = "未指示";
            // 
            // lblK2
            // 
            this.lblK2.AutoSize = true;
            this.lblK2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblK2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(80)))), ((int)(((byte)(190)))));
            this.lblK2.Location = new System.Drawing.Point(12, 204);
            this.lblK2.Name = "lblK2";
            this.lblK2.Size = new System.Drawing.Size(73, 21);
            this.lblK2.TabIndex = 11;
            this.lblK2.Text = "基準点 2:";
            // 
            // cmbKikai2
            // 
            this.cmbKikai2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKikai2.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.cmbKikai2.FormattingEnabled = true;
            this.cmbKikai2.Location = new System.Drawing.Point(90, 200);
            this.cmbKikai2.Name = "cmbKikai2";
            this.cmbKikai2.Size = new System.Drawing.Size(308, 29);
            this.cmbKikai2.TabIndex = 12;
            // 
            // lblX2
            // 
            this.lblX2.AutoSize = true;
            this.lblX2.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblX2.Location = new System.Drawing.Point(12, 239);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(22, 21);
            this.lblX2.TabIndex = 13;
            this.lblX2.Text = "X:";
            // 
            // txtKikai2X
            // 
            this.txtKikai2X.BackColor = System.Drawing.Color.White;
            this.txtKikai2X.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.txtKikai2X.ForeColor = System.Drawing.Color.Black;
            this.txtKikai2X.Location = new System.Drawing.Point(36, 235);
            this.txtKikai2X.Name = "txtKikai2X";
            this.txtKikai2X.Size = new System.Drawing.Size(130, 29);
            this.txtKikai2X.TabIndex = 14;
            // 
            // lblY2
            // 
            this.lblY2.AutoSize = true;
            this.lblY2.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblY2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblY2.Location = new System.Drawing.Point(176, 239);
            this.lblY2.Name = "lblY2";
            this.lblY2.Size = new System.Drawing.Size(22, 21);
            this.lblY2.TabIndex = 15;
            this.lblY2.Text = "Y:";
            // 
            // txtKikai2Y
            // 
            this.txtKikai2Y.BackColor = System.Drawing.Color.White;
            this.txtKikai2Y.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.txtKikai2Y.ForeColor = System.Drawing.Color.Black;
            this.txtKikai2Y.Location = new System.Drawing.Point(200, 235);
            this.txtKikai2Y.Name = "txtKikai2Y";
            this.txtKikai2Y.Size = new System.Drawing.Size(130, 29);
            this.txtKikai2Y.TabIndex = 16;
            // 
            // btnSetPoint2
            // 
            this.btnSetPoint2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.btnSetPoint2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSetPoint2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnSetPoint2.Location = new System.Drawing.Point(12, 270);
            this.btnSetPoint2.Name = "btnSetPoint2";
            this.btnSetPoint2.Size = new System.Drawing.Size(140, 32);
            this.btnSetPoint2.TabIndex = 17;
            this.btnSetPoint2.Text = "🎯 点2を指示";
            this.btnSetPoint2.UseVisualStyleBackColor = false;
            // 
            // lblPoint2Pix
            // 
            this.lblPoint2Pix.AutoSize = true;
            this.lblPoint2Pix.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPoint2Pix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.lblPoint2Pix.Location = new System.Drawing.Point(160, 276);
            this.lblPoint2Pix.Name = "lblPoint2Pix";
            this.lblPoint2Pix.Size = new System.Drawing.Size(58, 21);
            this.lblPoint2Pix.TabIndex = 18;
            this.lblPoint2Pix.Text = "未指示";
            // 
            // btnSwap
            // 
            this.btnSwap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.btnSwap.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.btnSwap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnSwap.Location = new System.Drawing.Point(12, 310);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(386, 34);
            this.btnSwap.TabIndex = 19;
            this.btnSwap.Text = "🔄 2点を入れ替えて180°反転";
            this.btnSwap.UseVisualStyleBackColor = false;
            // 
            // lblOpacity
            // 
            this.lblOpacity.AutoSize = true;
            this.lblOpacity.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblOpacity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblOpacity.Location = new System.Drawing.Point(12, 356);
            this.lblOpacity.Name = "lblOpacity";
            this.lblOpacity.Size = new System.Drawing.Size(61, 21);
            this.lblOpacity.TabIndex = 20;
            this.lblOpacity.Text = "不透明度:";
            // 
            // trkOpacity
            // 
            this.trkOpacity.Location = new System.Drawing.Point(82, 350);
            this.trkOpacity.Maximum = 100;
            this.trkOpacity.Minimum = 10;
            this.trkOpacity.Name = "trkOpacity";
            this.trkOpacity.Size = new System.Drawing.Size(260, 45);
            this.trkOpacity.TabIndex = 21;
            this.trkOpacity.TickFrequency = 10;
            this.trkOpacity.Value = 80;
            // 
            // lblOpacityVal
            // 
            this.lblOpacityVal.AutoSize = true;
            this.lblOpacityVal.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOpacityVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblOpacityVal.Location = new System.Drawing.Point(348, 356);
            this.lblOpacityVal.Name = "lblOpacityVal";
            this.lblOpacityVal.Size = new System.Drawing.Size(42, 21);
            this.lblOpacityVal.TabIndex = 22;
            this.lblOpacityVal.Text = "80%";
            // 
            // grpPointCloud
            // 
            this.grpPointCloud.BackColor = System.Drawing.Color.White;
            this.grpPointCloud.Controls.Add(this.lblPcFile);
            this.grpPointCloud.Controls.Add(this.txtPointCloudPath);
            this.grpPointCloud.Controls.Add(this.btnBrowsePointCloud);
            this.grpPointCloud.Controls.Add(this.btnClearPointCloud);
            this.grpPointCloud.Controls.Add(this.chkSwapPointCloudXY);
            this.grpPointCloud.Controls.Add(this.lblPointCloudStatus);
            this.grpPointCloud.Controls.Add(this.btnOpen3D);
            this.grpPointCloud.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpPointCloud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(80)))));
            this.grpPointCloud.Location = new System.Drawing.Point(10, 412);
            this.grpPointCloud.Name = "grpPointCloud";
            this.grpPointCloud.Size = new System.Drawing.Size(410, 240);
            this.grpPointCloud.TabIndex = 1;
            this.grpPointCloud.TabStop = false;
            this.grpPointCloud.Text = "🌐 点群データ設定";
            // 
            // lblPcFile
            // 
            this.lblPcFile.AutoSize = true;
            this.lblPcFile.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.lblPcFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPcFile.Location = new System.Drawing.Point(12, 28);
            this.lblPcFile.Name = "lblPcFile";
            this.lblPcFile.Size = new System.Drawing.Size(209, 21);
            this.lblPcFile.TabIndex = 0;
            this.lblPcFile.Text = "点群ファイル (XYZ / LAS / CSV):";
            // 
            // txtPointCloudPath
            // 
            this.txtPointCloudPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.txtPointCloudPath.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.txtPointCloudPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtPointCloudPath.Location = new System.Drawing.Point(12, 54);
            this.txtPointCloudPath.Name = "txtPointCloudPath";
            this.txtPointCloudPath.ReadOnly = true;
            this.txtPointCloudPath.Size = new System.Drawing.Size(236, 29);
            this.txtPointCloudPath.TabIndex = 1;
            // 
            // btnBrowsePointCloud
            // 
            this.btnBrowsePointCloud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(248)))));
            this.btnBrowsePointCloud.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBrowsePointCloud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(50)))), ((int)(((byte)(100)))));
            this.btnBrowsePointCloud.Location = new System.Drawing.Point(254, 53);
            this.btnBrowsePointCloud.Name = "btnBrowsePointCloud";
            this.btnBrowsePointCloud.Size = new System.Drawing.Size(76, 31);
            this.btnBrowsePointCloud.TabIndex = 2;
            this.btnBrowsePointCloud.Text = "参照...";
            this.btnBrowsePointCloud.UseVisualStyleBackColor = false;
            this.btnBrowsePointCloud.Click += new System.EventHandler(this.BtnBrowsePointCloud_Click);
            // 
            // btnClearPointCloud
            // 
            this.btnClearPointCloud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnClearPointCloud.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.btnClearPointCloud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnClearPointCloud.Location = new System.Drawing.Point(336, 53);
            this.btnClearPointCloud.Name = "btnClearPointCloud";
            this.btnClearPointCloud.Size = new System.Drawing.Size(62, 31);
            this.btnClearPointCloud.TabIndex = 3;
            this.btnClearPointCloud.Text = "解除";
            this.btnClearPointCloud.UseVisualStyleBackColor = false;
            // 
            // chkSwapPointCloudXY
            // 
            this.chkSwapPointCloudXY.AutoSize = true;
            this.chkSwapPointCloudXY.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.chkSwapPointCloudXY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.chkSwapPointCloudXY.Location = new System.Drawing.Point(12, 94);
            this.chkSwapPointCloudXY.Name = "chkSwapPointCloudXY";
            this.chkSwapPointCloudXY.Size = new System.Drawing.Size(288, 25);
            this.chkSwapPointCloudXY.TabIndex = 4;
            this.chkSwapPointCloudXY.Text = "🔄 点群のX・Y座標を入れ替える (E/N反転)";
            this.chkSwapPointCloudXY.UseVisualStyleBackColor = true;
            // 
            // lblPointCloudStatus
            // 
            this.lblPointCloudStatus.AutoSize = true;
            this.lblPointCloudStatus.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPointCloudStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblPointCloudStatus.Location = new System.Drawing.Point(12, 130);
            this.lblPointCloudStatus.Name = "lblPointCloudStatus";
            this.lblPointCloudStatus.Size = new System.Drawing.Size(164, 21);
            this.lblPointCloudStatus.TabIndex = 5;
            this.lblPointCloudStatus.Text = "点群未読込 (Z表示なし)";
            // 
            // btnOpen3D
            // 
            this.btnOpen3D.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(114)))), ((int)(((byte)(186)))));
            this.btnOpen3D.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpen3D.ForeColor = System.Drawing.Color.White;
            this.btnOpen3D.Location = new System.Drawing.Point(12, 168);
            this.btnOpen3D.Name = "btnOpen3D";
            this.btnOpen3D.Size = new System.Drawing.Size(386, 38);
            this.btnOpen3D.TabIndex = 6;
            this.btnOpen3D.Text = "🎮 3次元で確認 (3Dプレビュー)";
            this.btnOpen3D.UseVisualStyleBackColor = false;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.pnlBottom.Controls.Add(this.btnReset);
            this.pnlBottom.Controls.Add(this.btnOk);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 668);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1184, 53);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnReset.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(12, 10);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(160, 34);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "🗑 解除 / リセット";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnOk.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(904, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(134, 34);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "✔ 設定を適用";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(1050, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(122, 34);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.pnlCenter.Controls.Add(this.picPreview);
            this.pnlCenter.Controls.Add(this.lblStatusGuide);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(430, 0);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.pnlCenter.Size = new System.Drawing.Size(754, 668);
            this.pnlCenter.TabIndex = 2;
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(6, 34);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(742, 634);
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            // 
            // lblStatusGuide
            // 
            this.lblStatusGuide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.lblStatusGuide.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatusGuide.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatusGuide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.lblStatusGuide.Location = new System.Drawing.Point(6, 6);
            this.lblStatusGuide.Name = "lblStatusGuide";
            this.lblStatusGuide.Size = new System.Drawing.Size(742, 28);
            this.lblStatusGuide.TabIndex = 1;
            this.lblStatusGuide.Text = "【操作ガイド】ホイール: 拡大/縮小 | 右ドラッグ: 平行移動 | 左クリック: 点の指示";
            this.lblStatusGuide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormBackgroundSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1184, 721);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1080, 680);
            this.Name = "FormBackgroundSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "背景画像・点群設定";
            this.pnlLeft.ResumeLayout(false);
            this.grpBgImage.ResumeLayout(false);
            this.grpBgImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkOpacity)).EndInit();
            this.grpPointCloud.ResumeLayout(false);
            this.grpPointCloud.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpBgImage;
        private System.Windows.Forms.Label lblImgFile;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Label lblK1;
        private System.Windows.Forms.ComboBox cmbKikai1;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.TextBox txtKikai1X;
        private System.Windows.Forms.Label lblY1;
        private System.Windows.Forms.TextBox txtKikai1Y;
        private System.Windows.Forms.Button btnSetPoint1;
        private System.Windows.Forms.Label lblPoint1Pix;
        private System.Windows.Forms.Label lblK2;
        private System.Windows.Forms.ComboBox cmbKikai2;
        private System.Windows.Forms.Label lblX2;
        private System.Windows.Forms.TextBox txtKikai2X;
        private System.Windows.Forms.Label lblY2;
        private System.Windows.Forms.TextBox txtKikai2Y;
        private System.Windows.Forms.Button btnSetPoint2;
        private System.Windows.Forms.Label lblPoint2Pix;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Label lblOpacity;
        private System.Windows.Forms.TrackBar trkOpacity;
        private System.Windows.Forms.Label lblOpacityVal;
        private System.Windows.Forms.GroupBox grpPointCloud;
        private System.Windows.Forms.Label lblPcFile;
        private System.Windows.Forms.TextBox txtPointCloudPath;
        private System.Windows.Forms.Button btnBrowsePointCloud;
        private System.Windows.Forms.Button btnClearPointCloud;
        private System.Windows.Forms.CheckBox chkSwapPointCloudXY;
        private System.Windows.Forms.Label lblPointCloudStatus;
        private System.Windows.Forms.Button btnOpen3D;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label lblStatusGuide;
    }
}
