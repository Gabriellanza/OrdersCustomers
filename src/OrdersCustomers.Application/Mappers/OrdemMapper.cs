using OrdersCustomers.Application.DTOs.Ordem;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Application.Mappers;

public static class OrdemMapper
{
    public static IEnumerable<OrdemResponseDto> ToApiResponse(this List<Ordem> ordemList)
    {
        if (ordemList is null) return null;
        if (!ordemList.Any()) return null;

        return ordemList.Select(ordem => ordem.ToApiResponse());
    }

    public static OrdemResponseDto ToApiResponse(this Ordem ordemObj)
    {
        if (ordemObj is null)
            return null;

        return new OrdemResponseDto
        {
            Id = ordemObj.Id,
            NumeroOrdem = ordemObj.NumeroOrdem.ToString(),
            DataConclusao = ordemObj.DataConclusao,
            Status = ordemObj.Status,
            ValorTotal = ordemObj.ValorTotal,
            Items = ordemObj.Itens.ToApiResponse()
        };
    }

    public static List<OrdemItemResponseDto> ToApiResponse(this List<ItemOrdem> itemOrdemList)
    {
        if (itemOrdemList is null) return null;
        if (!itemOrdemList.Any()) return null;

        return itemOrdemList.Select(itemOrdem => new OrdemItemResponseDto
        {
            NomeProduto = itemOrdem.NomeProduto,
            Quantidade = itemOrdem.Quantidade,
            ValorUnitario = itemOrdem.ValorUnitario
        }).ToList();
    }
    
}