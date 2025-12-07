using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Application.DTOs.Endereco;
using OrdersCustomers.Application.Mappers;
using OrdersCustomers.Application.Services.Comum;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using OrdersCustomers.Application.Interfaces;

namespace OrdersCustomers.Application.Services;

public class EnderecoService : ServiceBase<Endereco>, IEnderecoService
{
    public EnderecoService(IServiceProvider serviceProvider, IRepository<Endereco> repoBase) : base(serviceProvider, repoBase)
    {
    }
    


}