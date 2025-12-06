using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Domain.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> Listar();
}