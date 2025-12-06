using OrdersCustomers.Application.Services.Comum;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Domain.Interfaces.Comum;

namespace OrdersCustomers.Application.Services;

public class ClienteService : ServiceBase<Cliente>, IClienteService
{
    public ClienteService(IServiceProvider serviceProvider, IRepository<Cliente> repoBase) : base(serviceProvider, repoBase)
    {
    }

    public async Task<IEnumerable<Cliente>> Listar()
    {
        var clienteList = await GetList();

        return clienteList;
    }

}