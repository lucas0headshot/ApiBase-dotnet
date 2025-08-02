using ApiBase.Core.Domain.Entities;
using System;
using System.Collections.Generic;

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
