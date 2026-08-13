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
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == 1 /* HTCLIENT */)
            {
                Point screenPt = new Point(m.LParam.ToInt32());
                Point clientPt = this.PointToClient(screenPt);

                int border = 8;
                bool isLeft = clientPt.X <= border;
                bool isRight = clientPt.X >= this.ClientSize.Width - border;
                bool isTop = clientPt.Y <= border;
                bool isBottom = clientPt.Y >= this.ClientSize.Height - border;

                if (isTop && isLeft) m.Result = (IntPtr)HTTOPLEFT;
                else if (isTop && isRight) m.Result = (IntPtr)HTTOPRIGHT;
                else if (isBottom && isLeft) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (isBottom && isRight) m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (isLeft) m.Result = (IntPtr)HTLEFT;
                else if (isRight) m.Result = (IntPtr)HTRIGHT;
                else if (isTop) m.Result = (IntPtr)HTTOP;
                else if (isBottom) m.Result = (IntPtr)HTBOTTOM;
            }
        }

        private void btnDockToPanel_Click(object sender, EventArgs e)
        {
            DockToPanelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
