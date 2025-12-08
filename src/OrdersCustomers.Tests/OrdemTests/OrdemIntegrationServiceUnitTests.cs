using Microsoft.EntityFrameworkCore;
using Moq;
using OrdersCustomers.Application.DTOs.Ordem;
using OrdersCustomers.Application.Interfaces.Comum;
using OrdersCustomers.Application.Services;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.Interfaces.Procedures;
using OrdersCustomers.Domain.Interfaces.Rabbit;
using OrdersCustomers.Infra.Data.Context;

namespace OrdersCustomers.Tests.OrdemTests;
public class OrdemIntegrationServiceUnitTests
{
    #region Injections

    private AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private static OrdemService CriarService(AppDbContext context)
    {
        var repo = new OrdersCustomers.Infra.Data.Repository.RepositoryBase<Ordem>(context);
        var finalizarMock = new Mock<IFinalizarOrdemProcedure>();
        finalizarMock.Setup(f => f.Execute(It.IsAny<Guid>())).ReturnsAsync(true);

        var rabbitMock = new Mock<IRabbitMqService>();
        var notificationMock = new Mock<INotificationService>();
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(sp => sp.GetService(typeof(INotificationService)))
            .Returns(notificationMock.Object);

        return new OrdemService(serviceProviderMock.Object, repo, finalizarMock.Object, rabbitMock.Object);
    }

    #endregion


    [Fact]
    public async Task CriarOrdem_DeveSalvarNoBanco()
    {
        await using var context = CriarContexto();
        var service = CriarService(context);

        var dto = new OrdemCreateDto
        {
            ClienteId = Guid.NewGuid(),
            Items = new List<OrdemItemCreateDto>
            {
                new OrdemItemCreateDto { NomeProduto = "Produto1", Quantidade = 2, ValorUnitario = 10 }
            }
        };

        var result = await service.Criar(dto);

        Assert.NotNull(result);

        var ordemNoBanco = await context.Set<Ordem>().FirstOrDefaultAsync(o => o.Id == result.Id);
        Assert.NotNull(ordemNoBanco);
        Assert.Equal(result.ValorTotal, ordemNoBanco.ValorTotal);
    }


    [Fact]
    public async Task Finalizar_DeveAtualizarStatus()
    {
        await using var context = CriarContexto();
        var service = CriarService(context);

        var dto = new OrdemCreateDto
        {
            ClienteId = Guid.NewGuid(),
            Items = new List<OrdemItemCreateDto>
            {
                new OrdemItemCreateDto { NomeProduto = "Produto1", Quantidade = 2, ValorUnitario = 10 }
            }
        };

        var ordemCriada = await service.Criar(dto);

        var result = await service.Finalizar(ordemCriada?.NumeroOrdem);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task CriarVariasOrdens_DeveRetornarTodasNoBanco()
    {
        await using var context = CriarContexto();
        var service = CriarService(context);

        var ordens = new List<OrdemCreateDto>
        {
            new() { ClienteId = Guid.NewGuid(), Items = new List<OrdemItemCreateDto>{ new OrdemItemCreateDto{ NomeProduto="P1", Quantidade=1, ValorUnitario=10 } } },
            new() { ClienteId = Guid.NewGuid(), Items = new List<OrdemItemCreateDto>{ new OrdemItemCreateDto{ NomeProduto="P2", Quantidade=2, ValorUnitario=20 } } }
        };

        foreach (var dto in ordens)
            await service.Criar(dto);

        var todas = await service.ListarTodas();
        Assert.Equal(2, todas.Count());
    }



}