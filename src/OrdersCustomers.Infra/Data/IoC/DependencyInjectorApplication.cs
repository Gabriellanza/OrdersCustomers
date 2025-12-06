using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Application.Services.Comum;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Domain.Interfaces.Comum;

namespace OrdersCustomers.Infra.Data.IoC;

public class DependencyInjectorApplication
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IClienteService, ClienteService>();
    }
}