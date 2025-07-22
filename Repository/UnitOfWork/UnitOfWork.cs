using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(DbContext context) => _context = context;

        public IRepository<T> Repository<T>() where T : EntityGuid
        {
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<T>(_context);

            return (IRepository<T>)_repositories[type];
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
        public void Dispose() => _context.Dispose();
    }
}
