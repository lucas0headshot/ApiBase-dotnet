using Domain.DTOs;
using Domain.Entities;

namespace Application.ApplicationBase
{
    public interface IApplicationBase<T> where T : EntityGuid
    {
        Task<T?> GetAsync(Guid id);
        Task<RetConView<T>> GetAllAsync(QueryParams queryParams);
    }
}
