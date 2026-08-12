using System;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public partial class FormBottomPanelCtrl : Form
    {
        public event EventHandler? DockToPanelRequested;

        public FormBottomPanelCtrl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ClientSize = new System.Drawing.Size(1480, 420);
        }

        private void btnDockToPanel_Click(object sender, EventArgs e)
        {
            DockToPanelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
