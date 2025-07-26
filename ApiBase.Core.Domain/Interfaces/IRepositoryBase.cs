using ApiBase.Core.Entities;
using System.Linq.Expressions;

namespace ApiBase.Core.Domain.Interfaces
{
    public interface IRepositoryBase<T> : IDisposable where T  : IdentifierGuid
    {
        void Insert(T entity);
        void Insert(List<T> entities);
        void Remove(T entity);
        void Remove(Guid id);
        void Remove(List<T> entities);
        T RecoverPorId(Guid id, params string[] includes);
        IQueryable<T> Recover(params string[] includes);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        T FirstOrDefault();
        IQueryable<T> Recover(QueryParams queryParams);
    }
}
