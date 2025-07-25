using Domain.Entities;
using Domain.Repository;
using Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityGuid
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) =>
            await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> ListAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
    }
}
