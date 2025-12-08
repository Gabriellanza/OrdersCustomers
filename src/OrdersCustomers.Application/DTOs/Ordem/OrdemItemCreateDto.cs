namespace OrdersCustomers.Application.DTOs.Ordem;

public class OrdemItemCreateDto
{
    public string NomeProduto { get; set; }

    public int Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }
}