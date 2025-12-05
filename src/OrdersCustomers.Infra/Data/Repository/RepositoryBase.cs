using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using OrdersCustomers.Domain.ValueObjects;
using System.Linq.Expressions;
using OrdersCustomers.Domain.Interfaces;
using OrdersCustomers.Infra.Data.Context;

namespace OrdersCustomers.Infra.Data.Repository;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(AppDbContext context)
    {
        _dbContext = context;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public bool Commit()
    {
        _dbContext.SaveChanges();
        return true;
    }

    public virtual void Add(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Added;
    }

    public virtual void Update(TEntity entity)
    {
        if (_dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.Equals(entity)) != null)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
        }
        else
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }

    public virtual void Update(TEntity entityExists, TEntity entity)
    {
        _dbContext.Entry(entityExists).CurrentValues.SetValues(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Deleted;
    }

    public virtual TEntity? GetSingle(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Expression<Func<TEntity, TEntity>>? selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true)
    {
        return GetList(predicate, include, orderBy, selector, disableTracking, take ?? 1, skip, useSplitQuery).FirstOrDefault();
    }

    public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Expression<Func<TEntity, TEntity>>? selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true)
    {
        return GetListQuery(predicate, include, orderBy, selector, disableTracking, take, skip, useSplitQuery).ToList();
    }

    public virtual IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Expression<Func<TEntity, TEntity>>? selector = null, bool disableTracking = true, int? take = null, int? skip = null, bool useSplitQuery = true)
    {
        var query = _dbSet.AsQueryable();
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (useSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        if (include is not null)
        {
            query = include(query);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (selector is not null)
        {
            query = query.Select(selector);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        if (skip is not null)
        {
            query = query.Skip(skip.Value);
        }

        if (take is not null)
        {
            query = query.Take(take.Value);
        }

        return query;
    }

    public virtual IEnumerable<TEntity> ObterLista(Dictionary<string, object> filtros)
    {
        return GetList();
    }

    #region Dispose

    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _dbContext.Dispose();
        }

        _disposed = true;
    }

    #endregion
}