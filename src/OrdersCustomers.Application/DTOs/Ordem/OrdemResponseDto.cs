using OrdersCustomers.Application.ViewModels;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Application.DTOs.Ordem;

public class OrdemResponseDto : ViewModelBase
{
    public Guid Id { get; set; }

    public string NumeroOrdem { get; set; }

    public DateTime? DataConclusao { get; set; }

    public OrdemStatus Status { get; set; }

    public string DescricaoStatus { get; set; }

    public decimal ValorTotal { get; set; }

    public List<OrdemItemResponseDto> Items { get; set; }
}