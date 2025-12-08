using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteResponseDto>> ListarTodos();

    Task<Cliente> ObterPorId(Guid id);

    Task<ClienteResponseDto> Criar(ClienteCreateDto clienteDto);

    Task<ClienteResponseDto> Alterar(ClienteAlterDto clienteDto);

    Task<ClienteResponseDto> Inativar(Guid id);
}