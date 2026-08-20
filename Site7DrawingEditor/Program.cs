using System;
using System.Text;
using System.Windows.Forms;

namespace Site7DrawingEditor
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ApplicationConfiguration.Initialize();
            string? dbPath = args.Length > 0 ? args[0] : null;
            Application.Run(new FormDrawingEditor(dbPath));
        }
    }
}
