using OrdersCustomers.Domain.Entities.Comum;

namespace OrdersCustomers.Domain.Entities;

public class ItemOrdem : EntityBase
{
    public string NomeProduto { get; private set; }

    public int Quantidade { get; private set; }

    public decimal ValorUnitario { get; private set; }

    public decimal ValorTotal => Quantidade * ValorUnitario;

    public Guid OrdemId { get; set; }

    public ItemOrdem() { }

    public ItemOrdem(string nomeProduto, int quantidade, decimal valorUnitario, Guid ordemId)
    {
        NomeProduto = nomeProduto;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        OrdemId = ordemId;
    }

    #region Regras de Negocios

    public static ItemOrdem Novo(string nomeProduto, int qtde, decimal valorUnit, string usuario)
    {
        return new ItemOrdem
        {
            UsuarioCriacao = usuario,
            NomeProduto = nomeProduto,
            Quantidade = qtde,
            ValorUnitario = valorUnit
        };
    }


    #endregion

}