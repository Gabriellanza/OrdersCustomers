using OrdersCustomers.Domain.Entities.Comum;
using OrdersCustomers.Domain.Validators;

namespace OrdersCustomers.Domain.Entities;

public class Cliente : EntityBase
{
    public string CpfCnpj { get; private set; }

    public string Nome { get; private set; }

    public string Email { get; private set; }

    public string Telefone { get; private set; }

    public string Celular { get; private set; }

    public Endereco Endereco { get; set; }


    private Cliente() { }

    private Cliente(string cpfCnpj, string nome, string email, string telefone, string celular)
    {
        CpfCnpj = cpfCnpj;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Celular = celular;
    }

    #region Regras de Negocios

    public override bool EhValido() => Validate(this, new CriarClienteValidator());

    public override bool EhValidoAlterar() => Validate(this, new AlterarClienteValidator());

    public static Cliente Novo(string cpfCnpj, string nome, string email, string telefone, string celular, string usuario)
    {
        var cliente = new Cliente(cpfCnpj, nome, email, telefone, celular)
        {
            UsuarioCriacao = usuario
        };

        return cliente;
    }

    public Cliente Alterar(string nome, string email, string telefone, string celular, string usuario)
    {
        UsuarioAlteracao = usuario;
        DataAtualizacao = DateTime.UtcNow;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Celular = celular;

        return this;
    }

    public Cliente Inativar(string usuario)
    {
        UsuarioAlteracao = usuario;
        DataAtualizacao = DateTime.UtcNow;
        Ativo = false;

        return this;
    }


    #endregion
}