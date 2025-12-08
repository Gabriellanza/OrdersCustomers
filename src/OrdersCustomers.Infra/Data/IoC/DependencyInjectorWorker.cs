using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersCustomers.Application.Interfaces;
using OrdersCustomers.Application.Interfaces.Comum;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Application.Services.Comum;
using OrdersCustomers.Domain.Interfaces.Procedures;
using OrdersCustomers.Infra.Data.Procedures;
using IRabbitMqService = OrdersCustomers.Domain.Interfaces.Rabbit.IRabbitMqService;
using RabbitMqService = OrdersCustomers.Infra.Rabbit.RabbitMqService;

namespace OrdersCustomers.Infra.Data.IoC;

public class DependencyInjectorWorker
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
    }
}