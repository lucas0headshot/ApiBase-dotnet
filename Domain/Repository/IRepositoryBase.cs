using Domain.Entities;

namespace Domain.Repository
{
    public interface IRepositoryBase<T> where T : EntityGuid
    {
        IQueryable<T> Query();
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> ListAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
