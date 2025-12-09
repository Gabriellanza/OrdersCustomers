using OrdersCustomers.Application.DTOs.Endereco;
using OrdersCustomers.Application.ViewModels;

namespace OrdersCustomers.Application.DTOs.Cliente;

public class ClienteResponseDto : ViewModelBase
{
    public Guid Id { get; set; }

    public string CpfCnpj { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }

    public string Celular { get; set; }

    public bool? Ativo { get; set; }

    public EnderecoResponseDto  Endereco { get; set; }
}