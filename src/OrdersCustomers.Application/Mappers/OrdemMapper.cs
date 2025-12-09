using OrdersCustomers.Application.DTOs.Ordem;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Entities.Rabbit;
using System.ComponentModel;
using System.Reflection;

namespace OrdersCustomers.Application.Mappers;

public static class OrdemMapper
{
    public static IEnumerable<OrdemResponseDto> ToApiResponse(this List<Ordem> ordemList)
    {
        if (ordemList is null) return Enumerable.Empty<OrdemResponseDto>();
        if (!ordemList.Any()) return Enumerable.Empty<OrdemResponseDto>();

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
            DataConclusao = ordemObj.DataCriacao,
            Status = ordemObj.Status,
            DescricaoStatus = GetEnumDescription(ordemObj.Status),
            ValorTotal = ordemObj.ValorTotal,
            Items = ordemObj.Itens.ToApiResponse()
        };
    }

    public static List<OrdemItemResponseDto> ToApiResponse(this List<ItemOrdem> itemOrdemList)
    {
        if (itemOrdemList is null) return new List<OrdemItemResponseDto>();
        if (!itemOrdemList.Any()) return new List<OrdemItemResponseDto>();

        return itemOrdemList.Select(itemOrdem => new OrdemItemResponseDto
        {
            NomeProduto = itemOrdem.NomeProduto,
            Quantidade = itemOrdem.Quantidade,
            ValorUnitario = itemOrdem.ValorUnitario
        }).ToList();
    }

    public static OrdemCreateMessage ToCreateMessage(this Ordem ordem)
    {
        if (ordem is null) return null;

        return new OrdemCreateMessage
        {
            Id = ordem.Id,
            ClienteId = ordem.ClienteId,
            NumeroOrdem = ordem.NumeroOrdem.ToString(),
            ValorTotal = ordem.ValorTotal,
            Itens = ordem.Itens.ToCreateMessage(),
        };
    }

    public static List<OrdemItemCreateMessage> ToCreateMessage(this List<ItemOrdem> itemOrdemList)
    {
        if (itemOrdemList is null) return new List<OrdemItemCreateMessage>();
        if (!itemOrdemList.Any()) return new List<OrdemItemCreateMessage>();

        return itemOrdemList.Select(itemOrdem => new OrdemItemCreateMessage
        {
            NomeProduto = itemOrdem.NomeProduto,
            Quantidade = itemOrdem.Quantidade,
            ValorUnitario = itemOrdem.ValorUnitario
        }).ToList();
    }

    public static string GetEnumDescription<T>(T enumValue) where T : Enum
    {
        var fi = enumValue.GetType().GetField(enumValue.ToString());
        if (fi != null)
        {
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : enumValue.ToString();
        }

        return enumValue.ToString();
    }
}