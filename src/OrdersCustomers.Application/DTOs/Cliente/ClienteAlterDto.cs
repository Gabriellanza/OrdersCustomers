using OrdersCustomers.Application.DTOs.Endereco;

namespace OrdersCustomers.Application.DTOs.Cliente;

public class ClienteAlterDto
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }

    public string Celular { get; set; }

    public EnderecoAlterDto Endereco { get; set; }
}