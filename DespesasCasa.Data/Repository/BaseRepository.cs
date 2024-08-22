using System.Linq.Expressions;
using DespesasCasa.Data.Context;
using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DespesasCasa.Data.Repository;

public class BaseRepository<K>(PostgresDbContext context) : IBaseRepository<K> where K : Base
{
    public async Task<K> GetAsync(bool tracking, Expression<Func<K, bool>>? predicate = null, Func<IQueryable<K>, IIncludableQueryable<K, object>>? include = null)
    {
        IQueryable<K> query = context.Set<K>();

        if (!tracking)
        {
            query = query.AsNoTracking();
        }
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (include != null)
        {
            query = include(query);
        }

        return (await query.FirstOrDefaultAsync())!;
    }

    public async Task<List<K>> GetAllAsync()
    {
        return await context.Set<K>().ToListAsync();
    }

    public async Task<List<K>> GetFilteredAsync(bool tracking, Expression<Func<K, bool>>? predicate = null, Func<IQueryable<K>, IIncludableQueryable<K, object>>? include = null, Func<IQueryable<K>, IOrderedQueryable<K>>? orderBy = null, int? page = null, int? perPage = null, bool ignoreGlobalFilter = false)
    {
        IQueryable<K> query = context.Set<K>();

        if (!tracking)
        {
            query = query.AsNoTracking();
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (page != null && perPage != null)
        {
            query = query.Skip((page.Value - 1) * perPage.Value).Take(perPage.Value);
        }

        if (ignoreGlobalFilter)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.ToListAsync();
    }

    public async Task<bool> FindAsync(Expression<Func<K, bool>> expression)
    {
        return await context.Set<K>().AnyAsync(expression);
    }

    public async Task<long> CountAsync(Expression<Func<K, bool>> expression)
    {
        return await context.Set<K>().CountAsync(expression);
    }

    public async Task<K> AddAsync(K entity)
    {
        await context.Set<K>().AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(List<K> entities)
    {
        if (entities is null)
        {
            return;
        }

        await context.Set<K>().AddRangeAsync(entities);
    }

    public void Update(K entity)
    {
        context.Set<K>().Attach(entity);
    }

    public void UpdateRange(List<K> entities)
    {
        if (entities is null)
        {
            return;
        }

        context.Set<K>().AttachRange(entities);
    }

    public void Remove(K entity)
    {
        context.Set<K>().Remove(entity);
    }

    public void RemoveRange(List<K> entities)
    {
        if (entities is null)
        {
            return;
        }

        context.Set<K>().RemoveRange(entities);
    }


}
