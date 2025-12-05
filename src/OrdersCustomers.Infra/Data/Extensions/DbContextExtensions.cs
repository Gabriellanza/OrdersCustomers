using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersCustomers.Infra.Data.Context;

namespace OrdersCustomers.Infra.Data.Extensions;

public static class DbContextExtensions
{
    public static void AddDbContextBase<T>(this IServiceCollection services, IConfiguration configuration) where T : AppDbContext
    {
        services.AddDbContext<T>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Connection"), npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("OrdersCustomers.Infra");
                npgsqlOptions.EnableRetryOnFailure();
            });
        });
    }
}