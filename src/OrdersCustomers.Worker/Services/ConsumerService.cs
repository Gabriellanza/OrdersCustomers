using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersCustomers.Application.Interfaces;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Domain.Entities.Rabbit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrdersCustomers.Worker.Services;

public class ConsumerService : BackgroundService
{
    private readonly ConnectionFactory _factory;
    private readonly IServiceScopeFactory _scopeFactory;

    public ConsumerService(IConfiguration configuration, IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _factory = new ConnectionFactory()
        {
            HostName = configuration["RabbitMQ:Host"] ?? "rabbitmq",
            Port = Convert.ToInt32(configuration["RabbitMQ:Port"] ?? "5672"),
            UserName = configuration["RabbitMQ:User"] ?? "guest",
            Password = configuration["RabbitMQ:Pass"] ?? "guest"
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Console.WriteLine("Tentando conectar ao RabbitMQ...");

                await using var connection = await _factory.CreateConnectionAsync(stoppingToken);
                await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

                await channel.QueueDeclareAsync(
                    queue: "nova_ordem",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: stoppingToken
                );

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (sender, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = JsonSerializer.Deserialize<OrdemCreateMessage>(body);

                    if (message is not null)
                    {
                        Console.WriteLine($"Ordem recebida - ID: {message.Id}");
                        Console.WriteLine($"Ordem recebida - Numero Ordem: {message.NumeroOrdem}");

                        using var scope = _scopeFactory.CreateScope();
                        var ordemService = scope.ServiceProvider.GetRequiredService<IOrdemService>();

                        await ordemService.Finalizar(message.NumeroOrdem);
                    }

                    await Task.Yield();
                };

                await channel.BasicConsumeAsync(
                    queue: "nova_ordem",
                    autoAck: true,
                    consumer: consumer,
                    cancellationToken: stoppingToken
                );

                Console.WriteLine("Conectado ao RabbitMQ. Aguardando mensagens...");

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha ao conectar no RabbitMQ: {ex.Message}");
                Console.WriteLine("Tentando novamente em 5 segundos...");

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

}