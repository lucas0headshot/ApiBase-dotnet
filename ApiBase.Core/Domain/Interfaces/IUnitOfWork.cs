using ApiBase.Core.Domain.Entities;

namespace ApiBase.Core.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RollbackChanges();
        IList<object> BuildCustomFieldsList<T>(List<object> pagedResults) where T : EntityGuid, new();
        object BuildCustomFieldsList<T>(object result) where T : EntityGuid, new();
    }
}
