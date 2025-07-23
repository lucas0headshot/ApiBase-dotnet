using Base.DTOs;

namespace Base.Application
{
    public interface IApplicationBase
    {
        Task<T?> GetAsync(Guid id);
        Task<RetConView<T>> GetAllAsync(QueryParams queryParams);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
