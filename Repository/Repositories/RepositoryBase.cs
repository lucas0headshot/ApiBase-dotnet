using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityGuid
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) =>
            await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> ListAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Delete(T entity) =>
            _dbSet.Remove(entity);

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
    }
}
