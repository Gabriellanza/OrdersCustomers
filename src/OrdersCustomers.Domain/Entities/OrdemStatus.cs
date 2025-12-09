using System.ComponentModel;

namespace OrdersCustomers.Domain.Entities;

public enum OrdemStatus
{
    [Description("Criada")]
    Criada = 1,

    [Description("Em Processamento")]
    EmProcessamento = 2,

    [Description("Concluida")]
    Concluida = 3,

    [Description("Cancelada")]
    Cancelada = 4
}