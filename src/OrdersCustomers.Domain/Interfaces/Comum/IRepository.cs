using Microsoft.EntityFrameworkCore.Query;
using OrdersCustomers.Domain.Entities.Comum;
using System.Linq.Expressions;

namespace OrdersCustomers.Domain.Interfaces.Comum;

public interface IRepository<TEntity> : IDisposable where TEntity : EntityBase
{

    bool Commit();

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Update(TEntity entityExists, TEntity entity);

    void Delete(TEntity entity);

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="take">A function to take elements by qtd</param>
    /// <param name="skip">A function to skip elements</param>
    /// <param name="useSplitQuery">A function to enable/disable splitQuery</param>
    /// <returns>An <see><cref>{TEntity}</cref></see> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    TEntity GetSingle(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true);

    /// <summary>
    /// Gets list entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="take">A function to take elements by qtd</param>
    /// <param name="skip">A function to skip elements</param>
    /// <param name="useSplitQuery">A function to enable/disable splitQuery</param>
    /// <returns>An <see cref="IEnumerable{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true);

    /// <summary>
    /// Gets list entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="selector">The selector for projection.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <param name="take">A function to take elements by qtd</param>
    /// <param name="skip">A function to skip elements</param>
    /// <param name="useSplitQuery">A function to enable/disable splitQuery</param>
    /// <returns>An <see cref="IQueryable{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true);

}