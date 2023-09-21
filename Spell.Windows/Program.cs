using Spell.Forms.Main;

namespace Spell.Windows
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new MainView());
            Application.Run(new AppContext());
        }

        public class AppContext : ApplicationContext
        {
            private readonly MainPresenter _main;

            public AppContext()
            {
                _main = new MainPresenter();

                _main.Exit().Subscribe(next => MainForm_Exit(this, next));
                _main.Open();
            }

            private void MainForm_Exit(object? sender, EventArgs e)
            {
                ExitThread();
            }
        }
    }
}