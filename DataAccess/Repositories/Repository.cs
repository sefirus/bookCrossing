using System.Linq.Expressions;
using Core.Interfaces.Repositories;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class
    {

        private readonly BookCrossingContext _context;
        protected readonly DbSet<T> DbSet;
        
        public Repository(BookCrossingContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async Task<IList<T>> QueryAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int? take = null, int skip = 0,
            bool asNoTracking = false)
        {
            var query = DbSet.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();
            
            if (include is not null)
                query = include(query);
            
            if (filter is not null)
                query = query.Where(filter);
            
            if (orderBy is not null)
                query = orderBy(query);
            
            query = query.Skip(skip);

            if (take is not null)
                query = query.Take(take.Value);
            
            return await query.ToListAsync();
        }
        
        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool asNoTracking = false)
        {
            var query = await QueryAsync(
                filter: filter,
                include: include,
                asNoTracking: asNoTracking
                );
            
            return query.FirstOrDefault();
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }
            
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;
        }
        
        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }