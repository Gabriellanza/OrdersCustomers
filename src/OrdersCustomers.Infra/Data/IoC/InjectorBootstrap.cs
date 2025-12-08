using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrdersCustomers.Infra.Data.IoC;

public static class InjectorBootstrap
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        DependencyInjectorRepository.Register(services, configuration);
        
        DependencyInjectorApplication.Register(services);

        DependencyInjectorWorker.Register(services);
    }
}