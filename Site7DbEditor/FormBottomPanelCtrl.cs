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
        }

        private void btnDockToPanel_Click(object sender, EventArgs e)
        {
            DockToPanelRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
