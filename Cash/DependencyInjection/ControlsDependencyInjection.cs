using Cash.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Cash.DependencyInjection
{
    class ControlsDependencyInjection : IDependecyInjectionModule
    {
        public void Init(IServiceCollection injector)
        {
            injector.AddTransient<CurrentProduct>();
            injector.AddTransient<ProductList>();
            injector.AddTransient<ShoppingActions>();
        }
    }
}
