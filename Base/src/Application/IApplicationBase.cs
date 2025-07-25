using CoreBackend.src.DTOs;
using CoreBackend.src.Entities;

namespace CoreBackend.src.Application
{
    public interface IApplicationBase<T> where T : EntityGuid
    {
        Task<T?> GetAsync(Guid id);
        Task<RetConView<T>> GetAllAsync(QueryParams queryParams);
    }
}
