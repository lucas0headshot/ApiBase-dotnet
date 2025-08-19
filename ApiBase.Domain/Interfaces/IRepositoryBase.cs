using ApiBase.Domain.Entities;
using ApiBase.Domain.Query;
using System.Linq.Expressions;

namespace ApiBase.Domain.Interfaces
{
    public interface IRepositoryBase<T> : IDisposable where T : EntityGuid
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
