using Domain.Entities;

namespace Application.ApplicationBase
{
    public interface IApplicationBase<T> where T : EntityGuid
    {
        Task<T?> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
