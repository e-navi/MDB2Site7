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

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show($"致命的なエラーが発生しました:\n{ex.Message}\n\n{ex.StackTrace}", "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show($"アプリケーションエラー:\n{e.Exception.Message}\n\n{e.Exception.StackTrace}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            try
            {
                Application.Run(new FormLauncher());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"起動時エラー:\n{ex.Message}\n\n{ex.StackTrace}", "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
