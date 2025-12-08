namespace OrdersCustomers.Domain.Entities.Rabbit;

public class OrdemCreateMessage
{
    public Guid Id { get; set; }

    public string NumeroOrdem { get; set; }

    public Guid ClienteId { get; set; }

    public decimal ValorTotal { get; set; }

    public List<OrdemItemCreateMessage> Itens { get; set; } = new();

}