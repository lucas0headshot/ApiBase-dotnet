using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : EntityGuid;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
