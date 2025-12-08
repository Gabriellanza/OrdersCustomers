using OrdersCustomers.Application.DTOs.Ordem;
using OrdersCustomers.Application.Interfaces.Comum;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Application.Interfaces;

public interface IOrdemService : IService<Ordem>
{
    Task<Ordem> ObterPorNumeroOrdem(string numeroOrdem);

    Task<IEnumerable<OrdemResponseDto>> ListarTodas();

    Task<OrdemResponseDto> Criar(OrdemCreateDto ordemCreateDto);

    Task<OrdemResponseDto> Finalizar(string numeroOrdem);

}