using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public partial class FormBluetoothCtrl : Form
    {
        public event EventHandler? DockToPanelRequested;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        public FormBluetoothCtrl()
        {
            InitializeComponent();
            WireHeaderDragEvents();
        }

        private void WireHeaderDragEvents()
        {
            this.panelDockHeader.MouseDown += Header_MouseDown;
            this.lblTitle.MouseDown += Header_MouseDown;
        }

        private void Header_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ClientSize = new System.Drawing.Size(264, 482);
        }

        private void btnDockToPanel_Click(object sender, EventArgs e)
        {
            DockToPanelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
