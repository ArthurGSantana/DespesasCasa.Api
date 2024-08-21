using System.Linq.Expressions;
using DespesasCasa.Domain.Entity;
using Microsoft.EntityFrameworkCore.Query;

namespace DespesasCasa.Domain.Interface.Repository;

public interface IBaseRepository<K> where K : Base
{
    Task<K> GetAsync(bool tracking, Expression<Func<K, bool>>? predicate = null, Func<IQueryable<K>, IIncludableQueryable<K, object>>? include = null);
    Task<List<K>> GetAllAsync();
    Task<List<K>> GetFilteredAsync(bool tracking, Expression<Func<K, bool>>? predicate = null, Func<IQueryable<K>, IIncludableQueryable<K, object>>? include = null, Func<IQueryable<K>, IOrderedQueryable<K>>? orderBy = null, int? page = null, int? perPage = null, bool ignoreGlobalFilter = false);
    Task<bool> FindAsync(Expression<Func<K, bool>> expression);
    Task<long> CountAsync(Expression<Func<K, bool>> expression);
    Task<K> AddAsync(K entity);
    Task AddRangeAsync(List<K> entities);
    void Update(K entity);
    void UpdateRange(List<K> entities);
    void Remove(K entity);
    void RemoveRange(List<K> entities);
}
