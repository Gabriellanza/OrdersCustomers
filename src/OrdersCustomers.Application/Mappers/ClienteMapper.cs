using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Application.DTOs.Endereco;
using OrdersCustomers.Domain.Entities;

namespace OrdersCustomers.Application.Mappers;

public static class ClienteMapper
{
    public static ClienteDto ToApiResponse(this Cliente clienteObj)
    {
        if (clienteObj is null)
            return null;

        return new ClienteDto
        {
            Id = clienteObj.Id,
            CpfCnpj = clienteObj.CpfCnpj,
            Nome = clienteObj.Nome,
            Email = clienteObj.Email,
            Telefone = clienteObj.Telefone,
            Celular = clienteObj.Celular,
            Endereco = clienteObj.Endereco?.ToApiResponse()
        };
    }

    public static IEnumerable<ClienteDto> ToApiResponse(this List<Cliente> clienteList)
    {
        if (clienteList is null) return null;
        if (!clienteList.Any()) return null;

        return clienteList.Select(cliente => new ClienteDto
        {
            Id = cliente.Id,
            CpfCnpj = cliente.CpfCnpj,
            Nome = cliente.Nome,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Celular = cliente.Celular,
            Endereco = cliente.Endereco?.ToApiResponse()
        });

    }

    public static EnderecoDto ToApiResponse(this Endereco endereco)
    {
        if (endereco is null) return null;

        return new EnderecoDto
        {
            Id = endereco.Id,
            Cep = endereco.Cep,
            Logradouro = endereco.Logradouro,
            Numero = endereco.Numero,
            Bairro = endereco.Bairro,
            Cidade = endereco.Cidade,
            Estado = endereco.Estado
        };
    }



}