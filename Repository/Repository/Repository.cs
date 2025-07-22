using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityGuid
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FirstAsync(x => x.Id == id);
    
        public async Task<IEnumerable<T>> ListAsync(QueryParams? queryParams = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (queryParams != null)
                query = query.ApplyQueryParameters(queryParams);
            
            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T  entity) => _dbSet.Update(entity);
        public void Remove(T entity) => _dbSet.Remove(entity);
    }
}
