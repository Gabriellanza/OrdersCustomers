using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Application.DTOs.Endereco;
using OrdersCustomers.Application.Interfaces.Comum;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Infra.Data.Context;
using OrdersCustomers.Infra.Data.Repository;
using System.Data.Entity;

namespace OrdersCustomers.Tests.ClienteTests;
public class ClienteIntegrationServiceUnitTests
{
    #region Injections


    private AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private ClienteService CriarService(AppDbContext context)
    {
        var repo = new RepositoryBase<Cliente>(context);

        var notificationMock = new Mock<INotificationService>();
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(sp => sp.GetService(typeof(INotificationService)))
            .Returns(notificationMock.Object);

        return new ClienteService(serviceProviderMock.Object, repo);
    }


    #endregion



    [Fact]
    public async Task CriarCliente_DeveSalvarNoBanco()
    {
        await using var context = CriarContexto();
        var service = CriarService(context);

        var dto = new ClienteCreateDto
        {
            CpfCnpj = "42000933831",
            Nome = "Gabriel Lanza",
            Email = "gabriel@example.com",
            Endereco = new EnderecoCreateDto
            {
                Cep = "19160000",
                Logradouro = "Rua Teste",
                Numero = "22",
                Bairro = "Centro",
                Cidade = "Presidente Prudente",
                Estado = "SP"
            }
        };

        var result = await service.Criar(dto);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ListarTodos_DeveRetornarClientesDoBanco()
    {
        await using var context = CriarContexto();

        var cliente = Cliente.Novo("42000933831", "Gabriel Lanza", "gabriel@example.com", null, null, "");
        cliente.Endereco = Endereco.Novo("19160000", "Rua Teste", "22", "Centro", "Presidente Prudente", "SP", "");
        context.Set<Cliente>().Add(cliente);
        await context.SaveChangesAsync();

        var service = CriarService(context);
        var result = (await service.ListarTodos())?.ToList();

        result.Should().HaveCount(1);
        result.Should().Contain(c => c.Nome == "Gabriel Lanza");
    }


    [Fact]
    public async Task ObterPorId_DeveRetornarClienteExistente()
    {
        await using var context = CriarContexto();
        var cliente = Cliente.Novo("42000933831", "Gabriel Lanza", "gabriel@example.com", null, null, "");
        context.Set<Cliente>().Add(cliente);
        await context.SaveChangesAsync();

        var service = CriarService(context);
        var result = await service.ObterPorId(cliente.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(cliente.Id);
    }


    [Fact]
    public async Task ValidarCriacaoCliente_DeveRetornarFalso_SeCpfJaExistir()
    {
        var context = CriarContexto();
        var service = CriarService(context);

        var clienteExistente = Cliente.Novo("42000933831", "Gabriel Lanza", "gabriel@example.com", null, null, "");
        clienteExistente.Endereco = Endereco.Novo("19160000", "Rua Teste", "22", "Centro", "Presidente Prudente", "SP", "");
        context.Set<Cliente>().Add(clienteExistente);
        await context.SaveChangesAsync();

        var dto = new ClienteCreateDto
        {
            CpfCnpj = "42000933831",
            Nome = "Novo Cliente",
            Email = "novo@teste.com",
            Endereco = new EnderecoCreateDto
            {
                Cep = "12345678",
                Logradouro = "Rua B",
                Numero = "20",
                Bairro = "Bairro B",
                Cidade = "Cidade B",
                Estado = "SP"
            }
        };

        var result = await service.ValidarCriacaoCliente(dto);

        result.Should().BeFalse();
    }



}