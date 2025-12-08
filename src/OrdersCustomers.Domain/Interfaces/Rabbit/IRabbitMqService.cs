namespace OrdersCustomers.Domain.Interfaces.Rabbit;

public interface IRabbitMqService
{
    Task PublishAsync<T>(string queue, T message);
}