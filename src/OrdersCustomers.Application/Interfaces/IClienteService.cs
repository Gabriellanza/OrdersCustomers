using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> ListarTodos();

    Task<Cliente> ObterPorId(Guid id);

    Task<ClienteDto> Criar(ClienteCreateDto clienteDto);

    Task<ClienteDto> Alterar(ClienteAlterDto clienteDto);

    Task<ClienteDto> Inativar(Guid id);
}