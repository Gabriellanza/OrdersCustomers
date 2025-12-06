using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Infra.Data.Context;
using OrdersCustomers.Infra.Data.Repository;

namespace OrdersCustomers.Infra.Data.IoC;

public static class DependencyInjectorRepository
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
    }
}