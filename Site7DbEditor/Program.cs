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
            ApplicationConfiguration.Initialize();

            // コマンドライン引数で直接DBが渡された場合は直接エディタを起動
            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            {
                Application.Run(new FormEditor(args[0]));
                return;
            }

            // 起動時に現場選択ランチャーを表示
            using var launcher = new FormLauncher();
            if (launcher.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(launcher.SelectedDbPath))
            {
                var editor = new FormEditor(launcher.SelectedDbPath, launcher.IsGaigyoMode);
                Application.Run(editor);
            }
        }    
    }
}