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
        private void InitializeComponent() {
            pnlLeft = new Panel();
            grpBgImage = new GroupBox();
            lblImgFile = new Label();
            txtImagePath = new TextBox();
            btnBrowseImage = new Button();
            lblK1 = new Label();
            cmbKikai1 = new ComboBox();
            lblX1 = new Label();
            txtKikai1X = new TextBox();
            lblY1 = new Label();
            txtKikai1Y = new TextBox();
            btnSetPoint1 = new Button();
            lblPoint1Pix = new Label();
            lblK2 = new Label();
            cmbKikai2 = new ComboBox();
            lblX2 = new Label();
            txtKikai2X = new TextBox();
            lblY2 = new Label();
            txtKikai2Y = new TextBox();
            btnSetPoint2 = new Button();
            lblPoint2Pix = new Label();
            btnSwap = new Button();
            lblOpacity = new Label();
            trkOpacity = new TrackBar();
            lblOpacityVal = new Label();
            grpPointCloud = new GroupBox();
            lblPcFile = new Label();
            txtPointCloudPath = new TextBox();
            btnBrowsePointCloud = new Button();
            btnClearPointCloud = new Button();
            chkSwapPointCloudXY = new CheckBox();
            lblPointCloudStatus = new Label();
            btnOpen3D = new Button();
            pnlBottom = new Panel();
            btnReset = new Button();
            btnOk = new Button();
            btnCancel = new Button();
            pnlCenter = new Panel();
            picPreview = new PictureBox();
            lblStatusGuide = new Label();
            pnlLeft.SuspendLayout();
            grpBgImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkOpacity).BeginInit();
            grpPointCloud.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.AutoScroll = true;
            pnlLeft.BackColor = Color.FromArgb(242, 244, 248);
            pnlLeft.Controls.Add(grpBgImage);
            pnlLeft.Controls.Add(grpPointCloud);
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(10, 8, 10, 8);
            pnlLeft.Size = new Size(430, 668);
            pnlLeft.TabIndex = 0;
            // 
            // grpBgImage
            // 
            grpBgImage.BackColor = Color.White;
            grpBgImage.Controls.Add(lblImgFile);
            grpBgImage.Controls.Add(txtImagePath);
            grpBgImage.Controls.Add(btnBrowseImage);
            grpBgImage.Controls.Add(lblK1);
            grpBgImage.Controls.Add(cmbKikai1);
            grpBgImage.Controls.Add(lblX1);
            grpBgImage.Controls.Add(txtKikai1X);
            grpBgImage.Controls.Add(lblY1);
            grpBgImage.Controls.Add(txtKikai1Y);
            grpBgImage.Controls.Add(btnSetPoint1);
            grpBgImage.Controls.Add(lblPoint1Pix);
            grpBgImage.Controls.Add(lblK2);
            grpBgImage.Controls.Add(cmbKikai2);
            grpBgImage.Controls.Add(lblX2);
            grpBgImage.Controls.Add(txtKikai2X);
            grpBgImage.Controls.Add(lblY2);
            grpBgImage.Controls.Add(txtKikai2Y);
            grpBgImage.Controls.Add(btnSetPoint2);
            grpBgImage.Controls.Add(lblPoint2Pix);
            grpBgImage.Controls.Add(btnSwap);
            grpBgImage.Controls.Add(lblOpacity);
            grpBgImage.Controls.Add(lblOpacityVal);
            grpBgImage.Controls.Add(trkOpacity);
            grpBgImage.Font = new Font("Yu Gothic UI", 13F, FontStyle.Bold);
            grpBgImage.ForeColor = Color.FromArgb(25, 45, 80);
            grpBgImage.Location = new Point(10, 8);
            grpBgImage.Name = "grpBgImage";
            grpBgImage.Size = new Size(410, 400);
            grpBgImage.TabIndex = 0;
            grpBgImage.TabStop = false;
            grpBgImage.Text = "🗺 背景画像設定";
            // 
            // lblImgFile
            // 
            lblImgFile.AutoSize = true;
            lblImgFile.Font = new Font("Yu Gothic UI", 12F);
            lblImgFile.ForeColor = Color.FromArgb(60, 60, 60);
            lblImgFile.Location = new Point(12, 28);
            lblImgFile.Name = "lblImgFile";
            lblImgFile.Size = new Size(90, 21);
            lblImgFile.TabIndex = 0;
            lblImgFile.Text = "画像ファイル:";
            // 
            // txtImagePath
            // 
            txtImagePath.BackColor = Color.FromArgb(248, 249, 251);
            txtImagePath.Font = new Font("Yu Gothic UI", 12F);
            txtImagePath.ForeColor = Color.FromArgb(20, 20, 20);
            txtImagePath.Location = new Point(12, 54);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.ReadOnly = true;
            txtImagePath.Size = new Size(313, 29);
            txtImagePath.TabIndex = 1;
            // 
            // btnBrowseImage
            // 
            btnBrowseImage.BackColor = Color.FromArgb(230, 238, 248);
            btnBrowseImage.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnBrowseImage.ForeColor = Color.FromArgb(20, 50, 100);
            btnBrowseImage.Location = new Point(336, 53);
            btnBrowseImage.Name = "btnBrowseImage";
            btnBrowseImage.Size = new Size(62, 31);
            btnBrowseImage.TabIndex = 2;
            btnBrowseImage.Text = "参照...";
            btnBrowseImage.UseVisualStyleBackColor = false;
            btnBrowseImage.Click += BtnBrowseImage_Click;
            // 
            // lblK1
            // 
            lblK1.AutoSize = true;
            lblK1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblK1.ForeColor = Color.FromArgb(190, 30, 30);
            lblK1.Location = new Point(12, 94);
            lblK1.Name = "lblK1";
            lblK1.Size = new Size(72, 21);
            lblK1.TabIndex = 3;
            lblK1.Text = "基準点 1:";
            // 
            // cmbKikai1
            // 
            cmbKikai1.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKikai1.Font = new Font("Yu Gothic UI", 12F);
            cmbKikai1.FormattingEnabled = true;
            cmbKikai1.Location = new Point(90, 90);
            cmbKikai1.Name = "cmbKikai1";
            cmbKikai1.Size = new Size(308, 29);
            cmbKikai1.TabIndex = 4;
            // 
            // lblX1
            // 
            lblX1.AutoSize = true;
            lblX1.Font = new Font("Yu Gothic UI", 12F);
            lblX1.ForeColor = Color.FromArgb(60, 60, 60);
            lblX1.Location = new Point(12, 129);
            lblX1.Name = "lblX1";
            lblX1.Size = new Size(22, 21);
            lblX1.TabIndex = 5;
            lblX1.Text = "X:";
            // 
            // txtKikai1X
            // 
            txtKikai1X.BackColor = Color.White;
            txtKikai1X.Font = new Font("Yu Gothic UI", 12F);
            txtKikai1X.ForeColor = Color.Black;
            txtKikai1X.Location = new Point(36, 125);
            txtKikai1X.Name = "txtKikai1X";
            txtKikai1X.Size = new Size(130, 29);
            txtKikai1X.TabIndex = 6;
            // 
            // lblY1
            // 
            lblY1.AutoSize = true;
            lblY1.Font = new Font("Yu Gothic UI", 12F);
            lblY1.ForeColor = Color.FromArgb(60, 60, 60);
            lblY1.Location = new Point(176, 129);
            lblY1.Name = "lblY1";
            lblY1.Size = new Size(21, 21);
            lblY1.TabIndex = 7;
            lblY1.Text = "Y:";
            // 
            // txtKikai1Y
            // 
            txtKikai1Y.BackColor = Color.White;
            txtKikai1Y.Font = new Font("Yu Gothic UI", 12F);
            txtKikai1Y.ForeColor = Color.Black;
            txtKikai1Y.Location = new Point(200, 125);
            txtKikai1Y.Name = "txtKikai1Y";
            txtKikai1Y.Size = new Size(130, 29);
            txtKikai1Y.TabIndex = 8;
            // 
            // btnSetPoint1
            // 
            btnSetPoint1.BackColor = Color.FromArgb(254, 226, 226);
            btnSetPoint1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnSetPoint1.ForeColor = Color.FromArgb(185, 28, 28);
            btnSetPoint1.Location = new Point(12, 160);
            btnSetPoint1.Name = "btnSetPoint1";
            btnSetPoint1.Size = new Size(140, 32);
            btnSetPoint1.TabIndex = 9;
            btnSetPoint1.Text = "🎯 点1を指示";
            btnSetPoint1.UseVisualStyleBackColor = false;
            // 
            // lblPoint1Pix
            // 
            lblPoint1Pix.AutoSize = true;
            lblPoint1Pix.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblPoint1Pix.ForeColor = Color.FromArgb(185, 28, 28);
            lblPoint1Pix.Location = new Point(160, 166);
            lblPoint1Pix.Name = "lblPoint1Pix";
            lblPoint1Pix.Size = new Size(58, 21);
            lblPoint1Pix.TabIndex = 10;
            lblPoint1Pix.Text = "未指示";
            // 
            // lblK2
            // 
            lblK2.AutoSize = true;
            lblK2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblK2.ForeColor = Color.FromArgb(25, 80, 190);
            lblK2.Location = new Point(12, 204);
            lblK2.Name = "lblK2";
            lblK2.Size = new Size(75, 21);
            lblK2.TabIndex = 11;
            lblK2.Text = "基準点 2:";
            // 
            // cmbKikai2
            // 
            cmbKikai2.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKikai2.Font = new Font("Yu Gothic UI", 12F);
            cmbKikai2.FormattingEnabled = true;
            cmbKikai2.Location = new Point(90, 200);
            cmbKikai2.Name = "cmbKikai2";
            cmbKikai2.Size = new Size(308, 29);
            cmbKikai2.TabIndex = 12;
            // 
            // lblX2
            // 
            lblX2.AutoSize = true;
            lblX2.Font = new Font("Yu Gothic UI", 12F);
            lblX2.ForeColor = Color.FromArgb(60, 60, 60);
            lblX2.Location = new Point(12, 239);
            lblX2.Name = "lblX2";
            lblX2.Size = new Size(22, 21);
            lblX2.TabIndex = 13;
            lblX2.Text = "X:";
            // 
            // txtKikai2X
            // 
            txtKikai2X.BackColor = Color.White;
            txtKikai2X.Font = new Font("Yu Gothic UI", 12F);
            txtKikai2X.ForeColor = Color.Black;
            txtKikai2X.Location = new Point(36, 235);
            txtKikai2X.Name = "txtKikai2X";
            txtKikai2X.Size = new Size(130, 29);
            txtKikai2X.TabIndex = 14;
            // 
            // lblY2
            // 
            lblY2.AutoSize = true;
            lblY2.Font = new Font("Yu Gothic UI", 12F);
            lblY2.ForeColor = Color.FromArgb(60, 60, 60);
            lblY2.Location = new Point(176, 239);
            lblY2.Name = "lblY2";
            lblY2.Size = new Size(21, 21);
            lblY2.TabIndex = 15;
            lblY2.Text = "Y:";
            // 
            // txtKikai2Y
            // 
            txtKikai2Y.BackColor = Color.White;
            txtKikai2Y.Font = new Font("Yu Gothic UI", 12F);
            txtKikai2Y.ForeColor = Color.Black;
            txtKikai2Y.Location = new Point(200, 235);
            txtKikai2Y.Name = "txtKikai2Y";
            txtKikai2Y.Size = new Size(130, 29);
            txtKikai2Y.TabIndex = 16;
            // 
            // btnSetPoint2
            // 
            btnSetPoint2.BackColor = Color.FromArgb(219, 234, 254);
            btnSetPoint2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnSetPoint2.ForeColor = Color.FromArgb(29, 78, 216);
            btnSetPoint2.Location = new Point(12, 270);
            btnSetPoint2.Name = "btnSetPoint2";
            btnSetPoint2.Size = new Size(140, 32);
            btnSetPoint2.TabIndex = 17;
            btnSetPoint2.Text = "🎯 点2を指示";
            btnSetPoint2.UseVisualStyleBackColor = false;
            // 
            // lblPoint2Pix
            // 
            lblPoint2Pix.AutoSize = true;
            lblPoint2Pix.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblPoint2Pix.ForeColor = Color.FromArgb(29, 78, 216);
            lblPoint2Pix.Location = new Point(160, 276);
            lblPoint2Pix.Name = "lblPoint2Pix";
            lblPoint2Pix.Size = new Size(58, 21);
            lblPoint2Pix.TabIndex = 18;
            lblPoint2Pix.Text = "未指示";
            // 
            // btnSwap
            // 
            btnSwap.BackColor = Color.FromArgb(240, 243, 248);
            btnSwap.Font = new Font("Yu Gothic UI", 12F);
            btnSwap.ForeColor = Color.FromArgb(40, 40, 40);
            btnSwap.Location = new Point(12, 310);
            btnSwap.Name = "btnSwap";
            btnSwap.Size = new Size(386, 34);
            btnSwap.TabIndex = 19;
            btnSwap.Text = "🔄 2点を入れ替えて180°反転";
            btnSwap.UseVisualStyleBackColor = false;
            // 
            // lblOpacity
            // 
            lblOpacity.AutoSize = true;
            lblOpacity.Font = new Font("Yu Gothic UI", 12F);
            lblOpacity.ForeColor = Color.FromArgb(60, 60, 60);
            lblOpacity.Location = new Point(12, 356);
            lblOpacity.Name = "lblOpacity";
            lblOpacity.Size = new Size(77, 21);
            lblOpacity.TabIndex = 20;
            lblOpacity.Text = "不透明度:";
            // 
            // trkOpacity
            // 
            trkOpacity.Location = new Point(82, 350);
            trkOpacity.Maximum = 100;
            trkOpacity.Minimum = 10;
            trkOpacity.Name = "trkOpacity";
            trkOpacity.Size = new Size(260, 45);
            trkOpacity.TabIndex = 21;
            trkOpacity.TickFrequency = 10;
            trkOpacity.TickStyle = TickStyle.TopLeft;
            trkOpacity.Value = 80;
            // 
            // lblOpacityVal
            // 
            lblOpacityVal.AutoSize = true;
            lblOpacityVal.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblOpacityVal.ForeColor = Color.FromArgb(20, 20, 20);
            lblOpacityVal.Location = new Point(348, 356);
            lblOpacityVal.Name = "lblOpacityVal";
            lblOpacityVal.Size = new Size(41, 21);
            lblOpacityVal.TabIndex = 22;
            lblOpacityVal.Text = "80%";
            // 
            // grpPointCloud
            // 
            grpPointCloud.BackColor = Color.White;
            grpPointCloud.Controls.Add(lblPcFile);
            grpPointCloud.Controls.Add(txtPointCloudPath);
            grpPointCloud.Controls.Add(btnBrowsePointCloud);
            grpPointCloud.Controls.Add(btnClearPointCloud);
            grpPointCloud.Controls.Add(chkSwapPointCloudXY);
            grpPointCloud.Controls.Add(lblPointCloudStatus);
            grpPointCloud.Controls.Add(btnOpen3D);
            grpPointCloud.Font = new Font("Yu Gothic UI", 13F, FontStyle.Bold);
            grpPointCloud.ForeColor = Color.FromArgb(25, 45, 80);
            grpPointCloud.Location = new Point(10, 427);
            grpPointCloud.Name = "grpPointCloud";
            grpPointCloud.Size = new Size(410, 219);
            grpPointCloud.TabIndex = 1;
            grpPointCloud.TabStop = false;
            grpPointCloud.Text = "🌐 点群データ設定";
            // 
            // lblPcFile
            // 
            lblPcFile.AutoSize = true;
            lblPcFile.Font = new Font("Yu Gothic UI", 12F);
            lblPcFile.ForeColor = Color.FromArgb(60, 60, 60);
            lblPcFile.Location = new Point(12, 28);
            lblPcFile.Name = "lblPcFile";
            lblPcFile.Size = new Size(215, 21);
            lblPcFile.TabIndex = 0;
            lblPcFile.Text = "点群ファイル (XYZ / LAS / CSV):";
            // 
            // txtPointCloudPath
            // 
            txtPointCloudPath.BackColor = Color.FromArgb(248, 249, 251);
            txtPointCloudPath.Font = new Font("Yu Gothic UI", 12F);
            txtPointCloudPath.ForeColor = Color.FromArgb(20, 20, 20);
            txtPointCloudPath.Location = new Point(12, 54);
            txtPointCloudPath.Name = "txtPointCloudPath";
            txtPointCloudPath.ReadOnly = true;
            txtPointCloudPath.Size = new Size(266, 29);
            txtPointCloudPath.TabIndex = 1;
            // 
            // btnBrowsePointCloud
            // 
            btnBrowsePointCloud.BackColor = Color.FromArgb(230, 238, 248);
            btnBrowsePointCloud.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnBrowsePointCloud.ForeColor = Color.FromArgb(20, 50, 100);
            btnBrowsePointCloud.Location = new Point(284, 53);
            btnBrowsePointCloud.Name = "btnBrowsePointCloud";
            btnBrowsePointCloud.Size = new Size(62, 31);
            btnBrowsePointCloud.TabIndex = 2;
            btnBrowsePointCloud.Text = "参照...";
            btnBrowsePointCloud.UseVisualStyleBackColor = false;
            btnBrowsePointCloud.Click += BtnBrowsePointCloud_Click;
            // 
            // btnClearPointCloud
            // 
            btnClearPointCloud.BackColor = Color.FromArgb(254, 226, 226);
            btnClearPointCloud.Font = new Font("Yu Gothic UI", 12F);
            btnClearPointCloud.ForeColor = Color.FromArgb(185, 28, 28);
            btnClearPointCloud.Location = new Point(348, 53);
            btnClearPointCloud.Name = "btnClearPointCloud";
            btnClearPointCloud.Size = new Size(50, 31);
            btnClearPointCloud.TabIndex = 3;
            btnClearPointCloud.Text = "解除";
            btnClearPointCloud.UseVisualStyleBackColor = false;
            // 
            // chkSwapPointCloudXY
            // 
            chkSwapPointCloudXY.AutoSize = true;
            chkSwapPointCloudXY.Font = new Font("Yu Gothic UI", 12F);
            chkSwapPointCloudXY.ForeColor = Color.FromArgb(40, 40, 40);
            chkSwapPointCloudXY.Location = new Point(12, 94);
            chkSwapPointCloudXY.Name = "chkSwapPointCloudXY";
            chkSwapPointCloudXY.Size = new Size(313, 25);
            chkSwapPointCloudXY.TabIndex = 4;
            chkSwapPointCloudXY.Text = "🔄 点群のX・Y座標を入れ替える (E/N反転)";
            chkSwapPointCloudXY.UseVisualStyleBackColor = true;
            // 
            // lblPointCloudStatus
            // 
            lblPointCloudStatus.AutoSize = true;
            lblPointCloudStatus.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblPointCloudStatus.ForeColor = Color.FromArgb(100, 100, 100);
            lblPointCloudStatus.Location = new Point(12, 130);
            lblPointCloudStatus.Name = "lblPointCloudStatus";
            lblPointCloudStatus.Size = new Size(170, 21);
            lblPointCloudStatus.TabIndex = 5;
            lblPointCloudStatus.Text = "点群未読込 (Z表示なし)";
            // 
            // btnOpen3D
            // 
            btnOpen3D.BackColor = Color.FromArgb(43, 114, 186);
            btnOpen3D.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnOpen3D.ForeColor = Color.White;
            btnOpen3D.Location = new Point(12, 168);
            btnOpen3D.Name = "btnOpen3D";
            btnOpen3D.Size = new Size(386, 38);
            btnOpen3D.TabIndex = 6;
            btnOpen3D.Text = "🎮 3次元で確認 (3Dプレビュー)";
            btnOpen3D.UseVisualStyleBackColor = false;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.FromArgb(232, 235, 240);
            pnlBottom.Controls.Add(btnReset);
            pnlBottom.Controls.Add(btnOk);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 668);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1184, 53);
            pnlBottom.TabIndex = 1;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(239, 68, 68);
            btnReset.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(12, 10);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(160, 34);
            btnReset.TabIndex = 0;
            btnReset.Text = "🗑 解除 / リセット";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += BtnReset_Click;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.BackColor = Color.FromArgb(34, 197, 94);
            btnOk.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(904, 10);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(134, 34);
            btnOk.TabIndex = 1;
            btnOk.Text = "✔ 設定を適用";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += BtnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.FromArgb(220, 224, 230);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Font = new Font("Yu Gothic UI", 12F);
            btnCancel.ForeColor = Color.Black;
            btnCancel.Location = new Point(1050, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(122, 34);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // pnlCenter
            // 
            pnlCenter.BackColor = Color.FromArgb(232, 235, 240);
            pnlCenter.Controls.Add(picPreview);
            pnlCenter.Controls.Add(lblStatusGuide);
            pnlCenter.Dock = DockStyle.Fill;
            pnlCenter.Location = new Point(430, 0);
            pnlCenter.Name = "pnlCenter";
            pnlCenter.Padding = new Padding(6, 6, 6, 0);
            pnlCenter.Size = new Size(754, 668);
            pnlCenter.TabIndex = 2;
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(24, 26, 32);
            picPreview.Dock = DockStyle.Fill;
            picPreview.Location = new Point(6, 34);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(742, 634);
            picPreview.TabIndex = 0;
            picPreview.TabStop = false;
            // 
            // lblStatusGuide
            // 
            lblStatusGuide.BackColor = Color.FromArgb(232, 235, 240);
            lblStatusGuide.Dock = DockStyle.Top;
            lblStatusGuide.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            lblStatusGuide.ForeColor = Color.FromArgb(40, 50, 70);
            lblStatusGuide.Location = new Point(6, 6);
            lblStatusGuide.Name = "lblStatusGuide";
            lblStatusGuide.Size = new Size(742, 28);
            lblStatusGuide.TabIndex = 1;
            lblStatusGuide.Text = "【操作ガイド】ホイール: 拡大/縮小 | 右ドラッグ: 平行移動 | 左クリック: 点の指示";
            lblStatusGuide.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormBackgroundSettings
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(242, 244, 248);
            CancelButton = btnCancel;
            ClientSize = new Size(1184, 721);
            Controls.Add(pnlCenter);
            Controls.Add(pnlLeft);
            Controls.Add(pnlBottom);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            ForeColor = Color.FromArgb(30, 30, 30);
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(1080, 680);
            Name = "FormBackgroundSettings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "背景画像・点群設定";
            pnlLeft.ResumeLayout(false);
            grpBgImage.ResumeLayout(false);
            grpBgImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkOpacity).EndInit();
            grpPointCloud.ResumeLayout(false);
            grpPointCloud.PerformLayout();
            pnlBottom.ResumeLayout(false);
            pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ResumeLayout(false);

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
