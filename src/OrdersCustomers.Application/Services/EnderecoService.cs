using OrdersCustomers.Application.Services.Comum;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Application.Interfaces;

namespace OrdersCustomers.Application.Services;

public class EnderecoService : ServiceBase<Endereco>, IEnderecoService
{
    public EnderecoService(IServiceProvider serviceProvider, IRepository<Endereco> repoBase) : base(serviceProvider, repoBase)
    {
    }
    


}