using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Infra.Data.Context;
using OrdersCustomers.Infra.Data.Extensions;
using OrdersCustomers.Infra.Data.Repository;

namespace OrdersCustomers.Infra.Data.IoC;

public static class DependencyInjectorRepository
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextBase<AppDbContext>(configuration);

        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
    }
}