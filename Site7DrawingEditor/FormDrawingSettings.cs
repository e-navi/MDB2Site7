using System;
using System.Drawing;
using System.Windows.Forms;
using Site7DrawingEditor.Services;

namespace Site7DrawingEditor
{
    public class FormDrawingSettings : Form
    {
        private NumericUpDown numMarginLeft = null!;
        private NumericUpDown numMarginOther = null!;

        private CheckBox chkShowNorthArrow = null!;
        private ComboBox cmbNorthType = null!;
        private NumericUpDown numNorthSize = null!;

        private CheckBox chkShowScaleBar = null!;
        private ComboBox cmbScaleBarType = null!;
        private ComboBox cmbScaleBarPos = null!;

        private CheckBox chkShowTitleBlock = null!;
        private CheckBox chkShowDrawingName = null!;
        private CheckBox chkShowScale = null!;
        private CheckBox chkShowPaperSize = null!;
        private CheckBox chkShowAuthor = null!;
        private TextBox txtAuthor = null!;
        private CheckBox chkShowDate = null!;
        private TextBox txtDate = null!;

        private Button btnSave = null!;
        private Button btnCancel = null!;

        public FormDrawingSettings()
        {
            InitializeComponent();
            LoadFromSettings();
        }

        private void InitializeComponent()
        {
            this.Text = "遺構図面設定 (枠余白・付加・表題欄)";
            this.ClientSize = new Size(295, 495);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular);

            int curY = 8;

            // 1. 枠余白・間隔 (mm)
            var grpMargin = new GroupBox
            {
                Text = "枠余白 (mm)",
                Location = new Point(8, curY),
                Size = new Size(278, 76),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 45, 80)
            };

            var lblMarginLeft = new Label { Text = "外枠余白 [左]:", Location = new Point(10, 21), AutoSize = true, ForeColor = Color.Black };
            numMarginLeft = new NumericUpDown { Location = new Point(175, 18), Size = new Size(60, 23), Maximum = 100, Minimum = 0, TextAlign = HorizontalAlignment.Right };
            var lblMm1 = new Label { Text = "mm", Location = new Point(238, 21), AutoSize = true, ForeColor = Color.Black };

            var lblMarginOther = new Label { Text = "外枠余白 [左以外]:", Location = new Point(10, 46), AutoSize = true, ForeColor = Color.Black };
            numMarginOther = new NumericUpDown { Location = new Point(175, 43), Size = new Size(60, 23), Maximum = 100, Minimum = 0, TextAlign = HorizontalAlignment.Right };
            var lblMm2 = new Label { Text = "mm", Location = new Point(238, 46), AutoSize = true, ForeColor = Color.Black };

            grpMargin.Controls.AddRange(new Control[] { lblMarginLeft, numMarginLeft, lblMm1, lblMarginOther, numMarginOther, lblMm2 });
            curY += 82;

            // 2. 方位記号 (北矢印)
            var grpNorth = new GroupBox
            {
                Text = "方位記号（北矢印）",
                Location = new Point(8, curY),
                Size = new Size(278, 76),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 45, 80)
            };

            chkShowNorthArrow = new CheckBox { Text = "方位記号を表示", Location = new Point(10, 18), AutoSize = true, Checked = true, ForeColor = Color.Black };
            var lblNorthType = new Label { Text = "種類:", Location = new Point(10, 45), AutoSize = true, ForeColor = Color.Black };
            cmbNorthType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(48, 42), Size = new Size(100, 23) };
            cmbNorthType.Items.AddRange(new object[] { "モダン", "標準矢印", "シンプル", "円形コンパス" });

            var lblNorthSize = new Label { Text = "寸:", Location = new Point(155, 45), AutoSize = true, ForeColor = Color.Black };
            numNorthSize = new NumericUpDown { Location = new Point(178, 42), Size = new Size(57, 23), Maximum = 100, Minimum = 5, TextAlign = HorizontalAlignment.Right };
            var lblMm3 = new Label { Text = "mm", Location = new Point(238, 45), AutoSize = true, ForeColor = Color.Black };

            grpNorth.Controls.AddRange(new Control[] { chkShowNorthArrow, lblNorthType, cmbNorthType, lblNorthSize, numNorthSize, lblMm3 });
            curY += 82;

            // 3. 縮尺スケールバー
            var grpScale = new GroupBox
            {
                Text = "縮尺スケールバー",
                Location = new Point(8, curY),
                Size = new Size(278, 76),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 45, 80)
            };

            chkShowScaleBar = new CheckBox { Text = "スケールバーを表示", Location = new Point(10, 18), AutoSize = true, Checked = true, ForeColor = Color.Black };
            var lblScaleType = new Label { Text = "種類:", Location = new Point(10, 45), AutoSize = true, ForeColor = Color.Black };
            cmbScaleBarType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(48, 42), Size = new Size(110, 23) };
            cmbScaleBarType.Items.AddRange(new object[] { "精密線 (下縮尺)", "ブロック", "シンプル線", "二重枠", "目盛付き" });

            var lblScalePos = new Label { Text = "位置:", Location = new Point(165, 45), AutoSize = true, ForeColor = Color.Black };
            cmbScaleBarPos = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(198, 42), Size = new Size(68, 23) };
            cmbScaleBarPos.Items.AddRange(new object[] { "中下", "右下", "左下", "右上", "左上" });

            grpScale.Controls.AddRange(new Control[] { chkShowScaleBar, lblScaleType, cmbScaleBarType, lblScalePos, cmbScaleBarPos });
            curY += 82;

            // 4. 右下 表題欄・図面名・縮尺設定
            var grpTitleBlock = new GroupBox
            {
                Text = "右下 表題欄・図面情報",
                Location = new Point(8, curY),
                Size = new Size(278, 195),
                Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 45, 80)
            };

            chkShowTitleBlock = new CheckBox { Text = "表題欄枠を表示", Location = new Point(10, 18), AutoSize = true, Checked = true, ForeColor = Color.Black };
            chkShowDrawingName = new CheckBox { Text = "図面名を表示", Location = new Point(10, 42), AutoSize = true, Checked = true, ForeColor = Color.Black };
            chkShowScale = new CheckBox { Text = "縮尺を表示", Location = new Point(10, 66), AutoSize = true, Checked = true, ForeColor = Color.Black };
            chkShowPaperSize = new CheckBox { Text = "用紙サイズを表示", Location = new Point(120, 66), AutoSize = true, Checked = true, ForeColor = Color.Black };

            chkShowAuthor = new CheckBox { Text = "調査主体 / 作成者:", Location = new Point(10, 92), AutoSize = true, ForeColor = Color.Black };
            txtAuthor = new TextBox { Location = new Point(10, 114), Size = new Size(256, 23) };

            chkShowDate = new CheckBox { Text = "作成年月日 / 日付:", Location = new Point(10, 142), AutoSize = true, ForeColor = Color.Black };
            txtDate = new TextBox { Location = new Point(10, 164), Size = new Size(256, 23) };

            grpTitleBlock.Controls.AddRange(new Control[] {
                chkShowTitleBlock, chkShowDrawingName, chkShowScale, chkShowPaperSize,
                chkShowAuthor, txtAuthor, chkShowDate, txtDate
            });
            curY += 203;

            // 5. Buttons
            btnSave = new Button
            {
                Text = "💾 設定を保存",
                Location = new Point(25, curY),
                Size = new Size(130, 32),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "閉じる",
                Location = new Point(165, curY),
                Size = new Size(100, 32),
                Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(225, 230, 240),
                ForeColor = Color.FromArgb(30, 40, 60),
                FlatStyle = FlatStyle.Standard,
                UseVisualStyleBackColor = true
            };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.AddRange(new Control[] { grpMargin, grpNorth, grpScale, grpTitleBlock, btnSave, btnCancel });
        }

        private void LoadFromSettings()
        {
            var s = DrawingSheetSettings.Instance;

            numMarginLeft.Value = Math.Clamp((decimal)s.MarginLeftMm, numMarginLeft.Minimum, numMarginLeft.Maximum);
            numMarginOther.Value = Math.Clamp((decimal)s.MarginOtherMm, numMarginOther.Minimum, numMarginOther.Maximum);

            chkShowNorthArrow.Checked = s.ShowNorthArrow;
            cmbNorthType.SelectedItem = s.NorthArrowType;
            if (cmbNorthType.SelectedIndex < 0) cmbNorthType.SelectedIndex = 0;
            numNorthSize.Value = Math.Clamp((decimal)s.NorthArrowSizeMm, numNorthSize.Minimum, numNorthSize.Maximum);

            chkShowScaleBar.Checked = s.ShowScaleBar;
            cmbScaleBarType.SelectedItem = s.ScaleBarType;
            if (cmbScaleBarType.SelectedIndex < 0) cmbScaleBarType.SelectedIndex = 0;
            cmbScaleBarPos.SelectedItem = s.ScaleBarPos;
            if (cmbScaleBarPos.SelectedIndex < 0) cmbScaleBarPos.SelectedIndex = 0;

            chkShowTitleBlock.Checked = s.ShowTitleBlock;
            chkShowDrawingName.Checked = s.ShowDrawingName;
            chkShowScale.Checked = s.ShowScale;
            chkShowPaperSize.Checked = s.ShowPaperSize;
            chkShowAuthor.Checked = s.ShowAuthor;
            txtAuthor.Text = s.AuthorText;
            chkShowDate.Checked = s.ShowDate;
            txtDate.Text = s.DateText;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            var s = DrawingSheetSettings.Instance;

            s.MarginLeftMm = (double)numMarginLeft.Value;
            s.MarginOtherMm = (double)numMarginOther.Value;

            s.ShowNorthArrow = chkShowNorthArrow.Checked;
            s.NorthArrowType = cmbNorthType.SelectedItem?.ToString() ?? "モダン";
            s.NorthArrowSizeMm = (double)numNorthSize.Value;

            s.ShowScaleBar = chkShowScaleBar.Checked;
            s.ScaleBarType = cmbScaleBarType.SelectedItem?.ToString() ?? "精密線 (下縮尺)";
            s.ScaleBarPos = cmbScaleBarPos.SelectedItem?.ToString() ?? "中下";

            s.ShowTitleBlock = chkShowTitleBlock.Checked;
            s.ShowDrawingName = chkShowDrawingName.Checked;
            s.ShowScale = chkShowScale.Checked;
            s.ShowPaperSize = chkShowPaperSize.Checked;
            s.ShowAuthor = chkShowAuthor.Checked;
            s.AuthorText = txtAuthor.Text.Trim();
            s.ShowDate = chkShowDate.Checked;
            s.DateText = txtDate.Text.Trim();

            s.SaveToIni();

            MessageBox.Show(this, "遺構図面設定（枠余白・方位記号・スケールバー・表題欄）を保存しました。", "設定保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
