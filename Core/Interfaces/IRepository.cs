using Core.DTOs;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> ListAsync(QueryParams queryParams);
        Task AddAsync(T entity);
        void Update(T enitity);
        void Remove(T entity);
    }
}
