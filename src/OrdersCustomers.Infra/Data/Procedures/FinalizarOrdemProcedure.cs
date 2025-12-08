using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Infra.Data.Context;
using OrdersCustomers.Infra.Data.Repository;
using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using OrdersCustomers.Domain.Interfaces.Procedures;

namespace OrdersCustomers.Infra.Data.Procedures;

public class FinalizarOrdemProcedure : RepositoryBase<Ordem>, IFinalizarOrdemProcedure
{
    private readonly AppDbContext _context;

    public FinalizarOrdemProcedure(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> Execute(Guid ordemId)
    {
        try
        {
            var parametros = new DynamicParameters();
            parametros.Add("ord_guid", ordemId);

            await _context.Database.GetDbConnection().ExecuteAsync("FINALIZAR_ORDEM", parametros, commandType: CommandType.StoredProcedure);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao executar a procedure: FINALIZAR_ORDEM: " + ex.Message);
        }
      
    }
}