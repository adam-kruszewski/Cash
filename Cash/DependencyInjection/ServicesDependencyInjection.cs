using Cash.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace Cash.DependencyInjection
{
    class ServicesDependencyInjection : IDependecyInjectionModule
    {
        public void Init(IServiceCollection injector)
        {
            injector.AddTransient<IProductRepository, ProductRepository>();
            injector.AddTransient<IPrintCodesService, PrintCodeService>();
        }
    }
}
