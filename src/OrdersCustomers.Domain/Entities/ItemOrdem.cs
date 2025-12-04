using OrdersCustomers.Domain.ValueObjects;

namespace OrdersCustomers.Domain.Entities;

public class ItemOrdem : EntityBase
{
    public string NomeProduto { get; private set; }

    public int Quantidade { get; private set; }

    public decimal ValorUnitario { get; private set; }

    public decimal ValorTotal => Quantidade * ValorUnitario;

    public Guid OrdemId { get; set; }

    public ItemOrdem(string nomeProduto, int quantidade, decimal valorUnitario, Guid ordemId)
    {
        NomeProduto = nomeProduto;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        OrdemId = ordemId;
    }

    #region Regras de Negocios



    #endregion

}