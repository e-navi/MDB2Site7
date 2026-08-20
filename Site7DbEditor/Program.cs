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
            string? dbPath = args.Length > 0 ? args[0] : null;
            Application.Run(new FormEditor(dbPath));
        }    
    }
}