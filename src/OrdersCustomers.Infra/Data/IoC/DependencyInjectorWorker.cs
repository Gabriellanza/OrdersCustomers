using Microsoft.Extensions.DependencyInjection;
using IRabbitMqService = OrdersCustomers.Domain.Interfaces.Rabbit.IRabbitMqService;
using RabbitMqService = OrdersCustomers.Infra.Rabbit.RabbitMqService;

namespace OrdersCustomers.Infra.Data.IoC;

public static class DependencyInjectorWorker
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
    }
}