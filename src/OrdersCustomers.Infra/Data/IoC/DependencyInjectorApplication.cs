using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersCustomers.Application.Interfaces;
using OrdersCustomers.Application.Interfaces.Comum;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Application.Services.Comum;
using OrdersCustomers.Domain.Interfaces.Procedures;
using OrdersCustomers.Infra.Data.Procedures;

namespace OrdersCustomers.Infra.Data.IoC;

public static class DependencyInjectorApplication
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IEnderecoService, EnderecoService>();
        services.AddScoped<IOrdemService, OrdemService>();

        services.AddScoped<IFinalizarOrdemProcedure, FinalizarOrdemProcedure>();
    }
}