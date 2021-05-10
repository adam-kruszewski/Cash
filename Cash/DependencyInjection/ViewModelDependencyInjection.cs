using Cash.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Cash.DependencyInjection
{
    class ViewModelDependencyInjection : IDependecyInjectionModule
    {
        public void Init(IServiceCollection injector)
        {
            injector.AddTransient<MainWindowViewModel>();
            injector.AddTransient<CurrentProductViewModel>();
            injector.AddTransient<ProductListViewModel>();
            injector.AddTransient<ShoppingActionViewModel>();
            injector.AddTransient<PrintCodesViewModel>();
        }
    }
}