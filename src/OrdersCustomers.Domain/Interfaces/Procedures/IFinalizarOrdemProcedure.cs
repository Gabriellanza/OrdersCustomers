namespace OrdersCustomers.Domain.Interfaces.Procedures;

public interface IFinalizarOrdemProcedure
{
    Task<bool> Execute(Guid ordemId);
}