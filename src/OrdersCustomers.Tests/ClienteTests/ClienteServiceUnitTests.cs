using FluentAssertions;
using Moq;
using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Application.DTOs.Endereco;
using OrdersCustomers.Application.Interfaces.Comum;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Interfaces;

namespace OrdersCustomers.Tests.ClienteTests;
public class ClienteServiceUnitTests
{
    #region Injections
    private Mock<IRepository<Cliente>> RepoMock;
    private Mock<INotificationService> NotificationMock;
    private Mock<IServiceProvider> ServiceProviderMock;

    private ClienteService CriarService()
    {
        RepoMock = new Mock<IRepository<Cliente>>();
        NotificationMock = new Mock<INotificationService>();
        ServiceProviderMock = new Mock<IServiceProvider>();

        ServiceProviderMock.Setup(sp => sp.GetService(typeof(INotificationService)))
            .Returns(NotificationMock.Object);

        return new ClienteService(ServiceProviderMock.Object, RepoMock.Object);
    }
    #endregion


    [Fact]
    public async Task ObterPorId_DeveReturnNull_QuandoVazio()
    {
        var service = CriarService();

        var result = await service.ObterPorId(Guid.Empty);

        result.Should().BeNull();
    }

    [Fact]
    public async Task ObterPorId_DeveRetornarNull_QuandoNaoEncontrado()
    {
        var service = CriarService();

        var result = await service.ObterPorId(Guid.NewGuid());

        result.Should().BeNull();
    }

    [Fact]
    public async Task ValidarCriacaoCliente_DeveRetornarFalso_CasoClienteSejaNulo()
    {
        var service = CriarService();

        var result = await service.ValidarCriacaoCliente(null);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task ValidarCriacaoCliente_DeveRetornarFalso_CasoCpfVazio()
    {
        var service = CriarService();

        var cliente = new ClienteCreateDto
        {
            CpfCnpj = "",
            Nome = "Gabriel Lanza",
            Email = "gabriel-lanza21@outlook.com",
            Telefone = null,
            Celular = null,
            Endereco = new EnderecoCreateDto
            {
                Cep = "19160000",
                Logradouro = "Teste",
                Numero = "22",
                Bairro = "Teste",
                Cidade = "Teste",
                Estado = "SP"
            }
        };

        var result = await service.ValidarCriacaoCliente(cliente);
        result.Should().BeFalse();
    }

    [Fact]
    public Task LimparCpfCnpj_DeveRemoverTracosEPontos()
    {
        const string valor = "123.456.789-00";
        var result = ClienteService.LimparCpfCnpj(valor);
        result.Should().Be("12345678900");

        return Task.CompletedTask;
    }

    [Fact]
    public async Task LimparCpfCnpj_DeveRetornarVazioQuantoNaoEnviado()
    {
        ClienteService.LimparCpfCnpj(null).Should().BeEmpty();
        ClienteService.LimparCpfCnpj("").Should().BeEmpty();
        ClienteService.LimparCpfCnpj("   ").Should().BeEmpty();
    }

}