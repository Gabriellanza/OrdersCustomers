using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace OrdersCustomers.Application.Interfaces.Comum;

public interface IService : IDisposable
{

}

public interface IService<TEntity> : IService
{
    Task<TEntity> Save(TEntity obj);

    Task<TEntity> Delete(TEntity obj, bool forced);

    Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true);

    Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true);
}