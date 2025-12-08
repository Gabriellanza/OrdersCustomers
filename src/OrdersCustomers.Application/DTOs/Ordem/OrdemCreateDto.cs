namespace OrdersCustomers.Application.DTOs.Ordem;

public class OrdemCreateDto
{
    public Guid ClienteId { get; set; }

    public List<OrdemItemCreateDto> Items { get; set; } = new List<OrdemItemCreateDto>();
}