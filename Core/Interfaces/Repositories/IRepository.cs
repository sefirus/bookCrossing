using System.Linq.Expressions;
using Core.Pagination;
using Core.Pagination.Parameters;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<PagedList<T>> GetPaged(       
        ParametersBase parameters,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    Task<IList<T>> QueryAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int? take = null, int? skip = null,
        bool asNoTracking = false);

    Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool asNoTracking = false);

    void Delete(T entity);
    void Update(T entity);
    Task InsertAsync(T entity);
    Task SaveChangesAsync();
}