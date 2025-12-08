using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OrdersCustomers.Domain.Interfaces.Rabbit;
using RabbitMQ.Client;

namespace OrdersCustomers.Infra.Rabbit;

public class RabbitMqService : IRabbitMqService
{
    private readonly ConnectionFactory _factory;

    public RabbitMqService(IConfiguration configuration)
    {
        _factory = new ConnectionFactory()
        {
            HostName = configuration["RabbitMQ:Host"] ?? "rabbitmq",
            Port = Convert.ToInt32(configuration["RabbitMQ:Port"] ?? "5672"),
            UserName = configuration["RabbitMQ:User"] ?? "guest",
            Password = configuration["RabbitMQ:Pass"] ?? "guest"
        };
    }

    public async Task PublishAsync<T>(string queue, T message)
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync(exchange: "", routingKey: queue, body: body);
    }

}