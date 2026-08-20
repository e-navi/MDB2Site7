using System;
using System.Windows.Forms;

namespace Site7Launcher
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormLauncher());
        }
    }
}
