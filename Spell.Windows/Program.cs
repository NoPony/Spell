using Autofac;
using Spell.Forms.Main;
using System.Reflection;

namespace Spell.Windows
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Context());
        }

        /// <summary>
        /// The application context. 
        /// Simplifies the Main() method.
        /// Ensures the application thread isn't tied to a form.
        /// Handles showing the main form using MVP (Model View Presenter).
        /// Can be used to do processing outside the form lifetime.
        /// </summary>
        private class Context : ApplicationContext
        {
            private readonly IContainer _container;
            private readonly ILifetimeScope _scope;

            private readonly MainPresenter _main;

            public Context()
            {   
                _container = RegisterModules();
                _scope = _container.BeginLifetimeScope(); // Disposed in Main_Closed

                _main = _scope.Resolve<MainPresenter>();
                
                _main.Closed.Subscribe(Main_Closed);
                _main.Show();
            }

            private void Main_Closed(EventArgs e)
            {
                _scope.Dispose();

                ExitThread();
            }

            private static IContainer RegisterModules()
            {
                ContainerBuilder builder = new();

                builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

                return builder.Build();
            }
        }
    }
}