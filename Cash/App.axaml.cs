using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Cash.DependencyInjection;
using Cash.ViewModels;
using Cash.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Cash
{
    public class App : Application
    {
        private static IServiceCollection Injector { get; set; }

        public static ServiceProvider ServiceProvider { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            Injector = new ServiceCollection();

            var injectorModules =
                this.GetType()
                    .Assembly
                        .GetTypes()
                            .Where(o => typeof(IDependecyInjectionModule).IsAssignableFrom(o))
                            .Where(o => o != typeof(IDependecyInjectionModule));

            foreach (var module in injectorModules)
            {
                var instance = module.GetConstructors().Single().Invoke(new object[0]) as IDependecyInjectionModule;

                instance.Init(Injector);
            }

            ServiceProvider = Injector.BuildServiceProvider();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var viewModel = ServiceProvider.GetService<MainWindowViewModel>();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = viewModel
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        protected override void LogBindingError(AvaloniaProperty property, System.Exception e)
        {
            base.LogBindingError(property, e);
        }
    }
}
