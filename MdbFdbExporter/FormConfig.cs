using System;
using System.IO;
using System.Windows.Forms;

namespace MdbFdbExporter
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();

            // Set default initial paths
            string defaultWorkspace = @"c:\Proj\Antigravity\MDB2Site7";
            if (Directory.Exists(defaultWorkspace))
            {
                txtDbFolder.Text = defaultWorkspace;
                txtOutFolder.Text = Path.Combine(defaultWorkspace, "Exported_CSV");
            }
            else
            {
                txtDbFolder.Text = AppDomain.CurrentDomain.BaseDirectory;
                txtOutFolder.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exported_CSV");
            }

            btnBrowseDb.Click += BtnBrowseDb_Click;
            btnBrowseOut.Click += BtnBrowseOut_Click;
            btnOpenConverter.Click += BtnOpenConverter_Click;
            btnExit.Click += BtnExit_Click;
        }

        private void BtnBrowseDb_Click(object? sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog();
            dlg.Description = "Database Store Root Folder (データベース格納親フォルダ) を選択してください";
            if (Directory.Exists(txtDbFolder.Text))
                dlg.SelectedPath = txtDbFolder.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtDbFolder.Text = dlg.SelectedPath;
            }
        }

        private void BtnBrowseOut_Click(object? sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog();
            dlg.Description = "CSV Output Directory (CSV出力先フォルダ) を選択してください";
            if (Directory.Exists(txtOutFolder.Text))
                dlg.SelectedPath = txtOutFolder.Text;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtOutFolder.Text = dlg.SelectedPath;
            }
        }

        private void BtnOpenConverter_Click(object? sender, EventArgs e)
        {
            string dbFolder = txtDbFolder.Text.Trim();
            string outFolder = txtOutFolder.Text.Trim();

            if (string.IsNullOrEmpty(dbFolder) || !Directory.Exists(dbFolder))
            {
                MessageBox.Show("指定された Database Store Root Folder が存在しません。\n正しいフォルダパスを指定してください。",
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(outFolder))
            {
                MessageBox.Show("CSV Output Directory を指定してください。",
                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(outFolder))
            {
                try
                {
                    Directory.CreateDirectory(outFolder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"CSV出力先フォルダの作成に失敗しました:\n{ex.Message}",
                        "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            bool isSite5 = rdoSite5.Checked;

            // Hide Config form and launch FormMain as modal
            this.Hide();
            using (var mainForm = new FormMain())
            {
                mainForm.ApplyConfiguration(isSite5, dbFolder, outFolder);
                mainForm.ShowDialog(this);
            }
            this.Show();
        }

        private void BtnExit_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
