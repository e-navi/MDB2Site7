namespace Site7DbEditor
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ApplicationConfiguration.Initialize();

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
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(launcher.SelectedDbPath))
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
    }
}