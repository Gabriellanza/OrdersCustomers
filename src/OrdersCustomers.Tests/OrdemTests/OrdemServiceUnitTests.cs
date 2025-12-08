using FluentAssertions;
using Moq;
using OrdersCustomers.Application.DTOs.Cliente;
using OrdersCustomers.Application.DTOs.Endereco;
using OrdersCustomers.Application.DTOs.Ordem;
using OrdersCustomers.Application.Interfaces.Comum;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Entities.Comum;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Domain.Interfaces.Procedures;
using OrdersCustomers.Domain.Interfaces.Rabbit;

namespace OrdersCustomers.Tests.OrdemTests;
public class OrdemServiceUnitTests
{
    #region Injections

    private Mock<IRepository<Ordem>> RepoMock;
    private Mock<IFinalizarOrdemProcedure> FinalizarMock;
    private Mock<IRabbitMqService> RabbitMock;
    private Mock<INotificationService> NotificationMock;
    private Mock<IServiceProvider> ServiceProviderMock;

    private OrdemService CriarService()
    {
        RepoMock = new Mock<IRepository<Ordem>>();
        FinalizarMock = new Mock<IFinalizarOrdemProcedure>();
        RabbitMock = new Mock<IRabbitMqService>();
        NotificationMock = new Mock<INotificationService>();
        ServiceProviderMock = new Mock<IServiceProvider>();

        ServiceProviderMock.Setup(sp => sp.GetService(typeof(INotificationService)))
            .Returns(NotificationMock.Object);

        return new OrdemService(ServiceProviderMock.Object, RepoMock.Object, FinalizarMock.Object, RabbitMock.Object);
    }

    #endregion


    [Fact]
    public async Task Criar_DeveRetornarNull_QuandoOrdemNull()
    {
        var service = CriarService();

        var result = await service.Criar(null);

        result.Should().BeNull();
    }


    [Fact]
    public async Task Criar_DeveRetornarNull_QuandoClienteIdVazio()
    {
        var service = CriarService();
        var dto = new OrdemCreateDto { ClienteId = Guid.Empty, Items = new List<OrdemItemCreateDto> { new OrdemItemCreateDto() } };

        var result = await service.Criar(dto);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Criar_DeveRetornarNull_QuandoNaoTemItens()
    {
        var service = CriarService();
        var dto = new OrdemCreateDto { ClienteId = Guid.NewGuid(), Items = new List<OrdemItemCreateDto>() };

        var result = await service.Criar(dto);

        result.Should().BeNull();
    }

}