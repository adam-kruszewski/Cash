using Microsoft.Extensions.DependencyInjection;

namespace Cash.DependencyInjection
{
    interface IDependecyInjectionModule
    {
        void Init(IServiceCollection injector);
    }
}