namespace OrdersCustomers.Domain.Entities.Rabbit;

public class OrdemItemCreateMessage
{
    public string NomeProduto { get; set; }

    public int Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }
}