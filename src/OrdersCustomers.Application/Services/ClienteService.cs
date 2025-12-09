using Microsoft.EntityFrameworkCore;
using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Application.Interfaces;
using OrdersCustomers.Application.Mappers;
using OrdersCustomers.Application.Services.Comum;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Entities.Comum;
using OrdersCustomers.Domain.Interfaces;

namespace OrdersCustomers.Application.Services;

public class ClienteService : ServiceBase<Cliente>, IClienteService
{

    public ClienteService(IServiceProvider serviceProvider, IRepository<Cliente> repoBase) : base(serviceProvider, repoBase)
    {
    }

    public async Task<Cliente> ObterPorId(Guid id)
    {
        if (id == Guid.Empty)
        {
            NewNotification("Cliente", "Necessário id do cliente para a Busca");
            return null;
        }

        var cliente = await GetSingle(x => x.Id == id, include: query => query
                .Include(x => x.Endereco));

        if (cliente?.Ativo == false)
        {
            NewNotification("Cliente", "O Cliente está inativo");
            return null;
        }

        if (cliente is null)
        {
            NewNotification("Cliente", "Cliente não encontrado");
            return null;
        }

        return cliente;
    }

    public async Task<IEnumerable<ClienteResponseDto>> ListarTodos()
    {
        var clienteList = await GetList(include: query => query
            .Include(x => x.Endereco));

        return clienteList.ToList().ToApiResponse();
    }

    public async Task<ClienteResponseDto> Inativar(Guid id)
    {
        var cliente = await ObterPorId(id);

        cliente.Inativar(GetAuthUserId());

        cliente = Update(cliente);

        if (!await Commit())
            return null;

        NewNotification("Cliente", "Cliente foi inativado!", NotificationType.Information);

        return cliente.ToApiResponse();
    }

    public async Task<ClienteResponseDto> Criar(ClienteCreateDto clienteDto)
    {
        if (!await ValidarCriacaoCliente(clienteDto))
            return null;

        var cliente = Cliente.Novo(clienteDto.CpfCnpj, clienteDto.Nome, clienteDto.Email, clienteDto.Telefone,
                                    clienteDto.Celular, GetAuthUserId());

        cliente.Endereco = Endereco.Novo(clienteDto.Endereco?.Cep, clienteDto.Endereco?.Logradouro, clienteDto.Endereco?.Numero,
            clienteDto.Endereco?.Bairro, clienteDto.Endereco?.Cidade, clienteDto.Endereco?.Estado, GetAuthUserId());

        cliente = Add(cliente);

        if (!await Commit())
            return null;

        return cliente.ToApiResponse();
    }


    public async Task<ClienteResponseDto> Alterar(ClienteAlterDto clienteDto)
    {
        var cliente = await ObterPorId(clienteDto.Id);

        if (!await ValidarAlteracaoCliente(cliente, clienteDto.Email))
            return null;

        cliente.Alterar(clienteDto.Nome, clienteDto.Email, clienteDto.Telefone, clienteDto.Celular, GetAuthUserId());

        cliente.Endereco = cliente.Endereco.Alterar(clienteDto.Endereco?.Cep, clienteDto.Endereco?.Logradouro,
            clienteDto.Endereco?.Numero, clienteDto.Endereco?.Bairro,
            clienteDto.Endereco?.Cidade, clienteDto.Endereco?.Estado, GetAuthUserId());

        cliente = Update(cliente);

        if (!await Commit())
            return null;

        NewNotification("Cliente", "Cliente alterado com sucesso !", NotificationType.Information);

        return cliente.ToApiResponse();
    }

    #region Validacoes

    public async Task<bool> ValidarAlteracaoCliente(Cliente cliente, string emailDto)
    {
        if (cliente is null)
            return false;

        if (string.IsNullOrEmpty(emailDto))
            return true;

        emailDto = emailDto.Trim();

        if (cliente.Email == emailDto)
            return true;

        var clienteEmail = await GetSingle(x => x.Id != cliente.Id &&
                                                x.Email == emailDto);

        if (clienteEmail is not null)
        {
            NewNotification("Cliente", "Já existe outro cliente com esse e-mail");
            return false;
        }

        return true;
    }

    public async Task<bool> ValidarCriacaoCliente(ClienteCreateDto clienteDto)
    {
        if (string.IsNullOrWhiteSpace(clienteDto?.CpfCnpj))
        {
            NewNotification("Cliente", "CPF/CNPJ é obrigatório");
            return false;
        }

        if (clienteDto.Endereco is null)
        {
            NewNotification("Endereco", "Endereço não informado");
            return false;
        }

        var cpfCnpj = LimparCpfCnpj(clienteDto.CpfCnpj);
        var email = clienteDto.Email?.Trim();

        var cliente = await GetSingle(x => (!string.IsNullOrEmpty(cpfCnpj) && x.CpfCnpj == cpfCnpj) ||
                                           (!string.IsNullOrEmpty(email) && x.Email == email));

        if (cliente is null)
            return true;

        if (cliente.CpfCnpj == cpfCnpj)
            NewNotification("Cliente", "CPF/CNPJ já cadastrado");

        if (cliente.Email == email)
            NewNotification("Cliente", "E-mail já cadastrado");

        return false;
    }

    public static string LimparCpfCnpj(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            return string.Empty;

        return new string(valor.Trim().Where(char.IsDigit).ToArray());
    }

    #endregion

}