using System;
using System.Text;
using System.Windows.Forms;

namespace MdbFdbExporter
{
    public partial class FormLog : Form
    {
        private static FormLog? _instance;
        private static readonly StringBuilder LogBuffer = new StringBuilder();

        public FormLog()
        {
            InitializeComponent();
            this.FormClosing += FormLog_FormClosing;
            this.btnClear.Click += btnClear_Click;
            this.btnClose.Click += btnClose_Click;

            // Load existing buffer
            txtLogContent.Text = LogBuffer.ToString();
            txtLogContent.SelectionStart = txtLogContent.TextLength;
            txtLogContent.ScrollToCaret();
        }

        public static void AppendLog(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string formatted = $"[{timestamp}] {message}{Environment.NewLine}";
            LogBuffer.Append(formatted);

            if (_instance != null && !_instance.IsDisposed && _instance.txtLogContent != null)
            {
                if (_instance.txtLogContent.InvokeRequired)
                {
                    _instance.txtLogContent.Invoke(new Action(() =>
                    {
                        _instance.txtLogContent.AppendText(formatted);
                    }));
                }
                else
                {
                    _instance.txtLogContent.AppendText(formatted);
                }
            }
        }

        public static void ShowLogWindow(IWin32Window owner)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new FormLog();
            }
            _instance.Show(owner);
            _instance.BringToFront();
        }

        private void FormLog_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            LogBuffer.Clear();
            txtLogContent.Clear();
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
