using OrdersCustomers.Domain.ValueObjects;

namespace OrdersCustomers.Domain.Entities;

public class Cliente : EntityBase
{
    public string CpfCnpj { get; private set; }

    public string Nome { get; private set; }

    public string Email { get; private set; }

    public string Telefone { get; private set; }

    public string Celular { get; private set; }

    public IEnumerable<Endereco> EnderecoList { get; private set; }

    public Cliente(string cpfCnpj, string nome, string email, string telefone, string celular)
    {
        CpfCnpj = cpfCnpj;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Celular = celular;
    }
}