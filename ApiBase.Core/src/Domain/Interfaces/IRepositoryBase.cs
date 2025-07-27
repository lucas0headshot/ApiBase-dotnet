using ApiBase.Core.src.Domain.Entities;
using ApiBase.Core.src.Infra.Query;
using System.Linq.Expressions;

namespace ApiBase.Core.src.Domain.Interfaces
{
    public interface IRepositoryBase<T> : IDisposable where T : IdentifierGuid
    {
        void Insert(T entity);
        void Insert(List<T> entities);
        void Remove(T entity);
        void Remove(Guid id);
        void Remove(List<T> entities);

        T GetById(Guid id, params string[] includes);
        IQueryable<T> Get(params string[] includes);
        IQueryable<T> Get(List<FilterModel> filters, List<SortModel> order, params string[] includes);
        IQueryable<T> Get(List<FilterGroup> filters, List<SortModel> order, params string[] includes);

        T FirstOrDefault();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
