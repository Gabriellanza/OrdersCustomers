using Microsoft.EntityFrameworkCore;
using OrdersCustomers.Application.DTOs.Ordem;
using OrdersCustomers.Application.Interfaces;
using OrdersCustomers.Application.Mappers;
using OrdersCustomers.Application.Services.Comum;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Entities.Comum;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Domain.Interfaces.Procedures;
using OrdersCustomers.Domain.Interfaces.Rabbit;

namespace OrdersCustomers.Application.Services;

public class OrdemService : ServiceBase<Ordem>, IOrdemService
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly IFinalizarOrdemProcedure _finalizarOrdemProcedure;

    public OrdemService(IServiceProvider serviceProvider, IRepository<Ordem> repoBase, IFinalizarOrdemProcedure finalizarOrdemProcedure, IRabbitMqService rabbitMqService) : base(serviceProvider, repoBase)
    {
        _finalizarOrdemProcedure = finalizarOrdemProcedure;
        _rabbitMqService = rabbitMqService;
    }

    public async Task<Ordem> ObterPorNumeroOrdem(string numeroOrdem)
    {
        if (!long.TryParse(numeroOrdem, out var numero))
        {
            NewNotification("Ordem", "Número da Ordem inválido");
            return null;
        }

        var ordem = await GetSingle(x => x.NumeroOrdem == numero && x.Ativo, include: query => query
            .Include(x => x.Itens));

        if (ordem is null)
        {
            NewNotification("Ordem", "Ordem não encontrada");
            return null;
        }

        return ordem;
    }

    public async Task<IEnumerable<OrdemResponseDto>> ListarTodas()
    {
        var ordemList = await GetList(x => x.Ativo, include: query => query
            .Include(x => x.Itens));

        return ordemList.ToList().ToApiResponse();
    }


    public async Task<OrdemResponseDto> Criar(OrdemCreateDto ordemCreateDto)
    {
        if (ValidarCriacaoOrdem(ordemCreateDto) == false)
            return null;

        var ordem = Ordem.Nova(ordemCreateDto.ClienteId, GetAuthUserId());

        foreach (var item in ordemCreateDto.Items ?? Enumerable.Empty<OrdemItemCreateDto>())
        {
            ordem.Itens.Add(ItemOrdem.Novo(item.NomeProduto, item.Quantidade, item.ValorUnitario, GetAuthUserId()));
        }

        ordem.AtualizarTotal(ordem.Itens?.Sum(x => x?.ValorTotal));

        ordem = Add(ordem);

        if (await Commit() == false)
            return null;

        NewNotification("Ordem", "Ordem criada com sucesso", NotificationType.Information);

        await _rabbitMqService.PublishAsync("nova_ordem", ordem.ToCreateMessage());

        return ordem.ToApiResponse();
    }

    public async Task<OrdemResponseDto> Finalizar(string numeroOrdem)
    {
        var ordem = await ObterPorNumeroOrdem(numeroOrdem);
        
        if (ValidarFinalizacaoOrdem(ordem) == false)
            return null;

        await _finalizarOrdemProcedure.Execute(ordem.Id);
        
        NewNotification("Ordem", "Ordem finalizada com sucesso", NotificationType.Information);

        return ordem.ToApiResponse();
    }



    #region Validacoes

    public bool ValidarCriacaoOrdem(OrdemCreateDto ordemCreateDto)
    {
        if (ordemCreateDto is null)
        {
            NewNotification("Ordem", "Ordem não enviada para criação");
            return false;
        }

        if (ordemCreateDto.ClienteId == Guid.Empty)
        {
            NewNotification("Cliente", "Cliente não encontrado");
            return false;
        }

        if (ordemCreateDto.Items is null || !ordemCreateDto.Items.Any())
        {
            NewNotification("OrdemItem", "Ordem não possui itens para criação");
            return false;
        }

        return true;
    }

    public bool ValidarFinalizacaoOrdem(Ordem ordem)
    {
        if (ordem is null)
            return false;

        if (ordem.Status == OrdemStatus.Cancelada)
        {
            NewNotification("Ordem", "Não é possível finalizar pois a ordem está cancelada");
            return false;
        }

        if (ordem.Status == OrdemStatus.Concluida)
        {
            NewNotification("Ordem", "A Ordem já está finalizada");
            return false;
        }

        if (ordem.Status == OrdemStatus.EmProcessamento)
        {
            NewNotification("Ordem", "Ordem em processamento, necessário aguardar acabar");
            return false;
        }

        return true;
    }

    #endregion

}