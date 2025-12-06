using OrdersCustomers.Domain.ValueObjects;

namespace OrdersCustomers.Domain.Entities;

public class Ordem : EntityBase
{
    public long NumeroOrdem { get; private set; }

    public DateTime? DataConclusao { get; private set; }

    public OrdemStatus Status { get; private set; }

    public decimal ValorTotal { get; private set; }

    #region Foreign Keys

    public Guid ClienteId { get; private set; }

    #endregion

    #region Navigation Properties

    public Cliente Cliente { get; private set; }

    public List<ItemOrdem> Itens { get; private set; }

    #endregion

    public Ordem(long numeroOrdem, Guid clienteId, DateTime dataCriacao, OrdemStatus status, decimal valorTotal, DateTime? dataConclusao)
    {
        NumeroOrdem = numeroOrdem;
        DataCriacao = dataCriacao;
        Status = status;
        ValorTotal = valorTotal;
        ClienteId = clienteId;
        DataConclusao = dataConclusao;
    }


    #region Regras de Negocios





    #endregion
}