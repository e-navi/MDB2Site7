using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Site7DbEditor
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_RESTORE = 9;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            const string mutexName = @"Global\Site7_Archaeological_System_Mutex_2026";
            using var mutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                // 既に起動しているプロセスを最前面にアクティブ化
                ActivateExistingProcessWindow();
                MessageBox.Show("Site7 は既に起動しています。", "二重起動防止", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

            // コマンドライン引数で直接DBが渡された場合は直接エディタを起動
            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            {
                Application.Run(new FormEditor(args[0]));
                return;
            }

            // 現場選択ランチャー ⇄ エディタ の遷移ループ
            while (true)
            {
                using var launcher = new FormLauncher();
                var result = launcher.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrEmpty(launcher.SelectedDbPath))
                {
                    var editor = new FormEditor(launcher.SelectedDbPath, launcher.IsGaigyoMode);
                    Application.Run(editor);
                    // エディタ終了後は再びループ先頭に戻り、ランチャーを表示
                }
                else
                {
                    // ランチャーで「終了」または×ボタン押下時はループを抜けて終了
                    break;
                }
            }
        }

        private static void ActivateExistingProcessWindow()
        {
            try
            {
                var current = Process.GetCurrentProcess();
                var processes = Process.GetProcessesByName(current.ProcessName);
                foreach (var p in processes)
                {
                    if (p.Id != current.Id && p.MainWindowHandle != IntPtr.Zero)
                    {
                        ShowWindow(p.MainWindowHandle, SW_RESTORE);
                        SetForegroundWindow(p.MainWindowHandle);
                        break;
                    }
                }
            }
            catch { }
        }
    }
}