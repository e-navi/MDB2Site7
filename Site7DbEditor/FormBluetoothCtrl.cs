using System;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public partial class FormBluetoothCtrl : Form
    {
        public event EventHandler? DockToPanelRequested;

        public FormBluetoothCtrl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ClientSize = new System.Drawing.Size(264, 396);
        }

        private void btnDockToPanel_Click(object sender, EventArgs e)
        {
            DockToPanelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
