using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrdersCustomers.Infra.Data.IoC;

public static class InjectorBootstrap
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        DependencyInjectorApplication.Register(services, configuration);

        DependencyInjectorRepository.Register(services, configuration);
    }
}