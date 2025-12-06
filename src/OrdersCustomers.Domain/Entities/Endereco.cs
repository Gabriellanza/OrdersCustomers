using OrdersCustomers.Domain.Entities.Comum;

namespace OrdersCustomers.Domain.Entities;

public class Endereco : EntityBase
{
    public string Cep { get; private set; }

    public string Logradouro { get; private set; }

    public string Numero { get; private set; }

    public string Bairro { get; private set; }

    public string Cidade { get; private set; }

    public string Estado { get; private set; }

    public Guid ClienteId { get; private set; }

    public Endereco(string cep, string logradouro, string numero, string bairro, string cidade, string estado)
    {
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }
}