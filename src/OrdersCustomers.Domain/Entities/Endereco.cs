using OrdersCustomers.Domain.Entities.Comum;
using OrdersCustomers.Domain.Validators.Clientes;

namespace OrdersCustomers.Domain.Entities;

public class Endereco : EntityBase
{
    public string Cep { get; private set; }

    public string Logradouro { get; private set; }

    public string Numero { get; private set; }

    public string Bairro { get; private set; }

    public string Cidade { get; private set; }

    public string Estado { get; private set; }

    public Guid ClienteId { get; set; }

    public Cliente Cliente { get; set; }


    public Endereco(string cep, string logradouro, string numero, string bairro, string cidade, string estado)
    {
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }

    #region Regras de Negocios

    public override bool EhValido() => Validate(this, new CriarEnderecoValidator());

    public static Endereco Novo(string cep, string logradouro, string numero, string bairro, string cidade, string estado, string usuario)
    {
        var endereco = new Endereco(cep, logradouro, numero, bairro, cidade, estado)
        {
            UsuarioCriacao = usuario
        };

        return endereco;
    }

    public Endereco Alterar(string cep, string logradouro, string numero, string bairro, string cidade, string estado, string usuario)
    {
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        UsuarioAlteracao = usuario;
        DataAtualizacao = DateTime.UtcNow;

        return this;
    }

    #endregion

}