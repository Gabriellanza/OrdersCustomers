using OrdersCustomers.Application.ViewModels;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Application.DTOs.Ordem;

public class OrdemItemResponseDto : ViewModelBase
{
    public string NomeProduto { get; set; }

    public int Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }
}