using OrdersCustomers.Application.DTOs.Endereco;
using OrdersCustomers.Application.ViewModels;

namespace OrdersCustomers.Application.DTOs.Cliente;

public class ClienteDto : ViewModelBase
{
    public Guid Id { get; set; }

    public string CpfCnpj { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }

    public string Celular { get; set; }

    public EnderecoDto Endereco { get; set; }
}