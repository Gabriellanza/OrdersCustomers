using Microsoft.EntityFrameworkCore;
using OrdersCustomers.Application.Interfaces;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Domain.Interfaces.Procedures;
using OrdersCustomers.Domain.Interfaces.Rabbit;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Infra.Data.Context;
using OrdersCustomers.Infra.Data.Procedures;
using OrdersCustomers.Infra.Data.Repository;
using OrdersCustomers.Infra.Rabbit;
using OrdersCustomers.Worker.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var cs = context.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(cs));

        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IFinalizarOrdemProcedure, FinalizarOrdemProcedure>();
        services.AddScoped<IRabbitMqService, RabbitMqService>();

        services.AddScoped<IOrdemService, OrdemService>();

        services.AddHostedService<ConsumerService>();
    })
    .Build();

await host.RunAsync();