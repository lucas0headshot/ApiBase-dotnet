using ApiBase.Core.src.Domain.Entities;
using ApiBase.Core.src.Domain.Interfaces;
using ApiBase.Core.src.Infra.Query;
using System.Linq.Expressions;

namespace ApiBase.Core.src.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T : IdentifierGuid
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Recover(params string[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Recover(QueryParams queryParams)
        {
            throw new NotImplementedException();
        }

        public T RecoverPorId(Guid id, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
