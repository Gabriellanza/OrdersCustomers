namespace OrdersCustomers.Application.DTOs.Endereco;

public class EnderecoAlterDto
{
    public Guid Id { get; set; }

    public string Cep { get; set; }

    public string Logradouro { get; set; }

    public string Numero { get; set; }

    public string Bairro { get; set; }

    public string Cidade { get; set; }

    public string Estado { get; set; }
}