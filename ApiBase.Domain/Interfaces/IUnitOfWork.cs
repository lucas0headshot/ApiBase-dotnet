using ApiBase.Domain.Entities;

namespace ApiBase.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RollbackChanges();
        IList<object> BuildCustomFieldsList<T>(List<object> pagedResults) where T : EntityGuid, new();
        object BuildCustomFieldsList<T>(object result) where T : EntityGuid, new();
    }
}
