using ApiBase.Core.Common.Query;
using ApiBase.Core.Domain.Entities;
using ApiBase.Core.Domain.Interfaces;
using ApiBase.Core.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiBase.Core.Repositories.Repositories.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T : EntityGuid
    {
        protected readonly DbContext Db;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(Context context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual void Insert(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            DbSet.Add(entity);
        }

        public virtual void Insert(List<T> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
            }

            DbSet.AddRange(entities);
        }

        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Remove(Guid id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
            {
                Remove(entity);
            }
        }

        public virtual void Remove(List<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public T GetById(Guid id, params string[] includes)
        {
            return Get(includes).FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> Get(params string[] includes)
        {
            if (includes.Any()) return DbSet;

            IQueryable<T> queryable = DbSet.AsQueryable();

            foreach (string navigationPropertyPath in includes)
            {
                queryable = queryable.Include(navigationPropertyPath);
            }

            return queryable;
        }

        public virtual IQueryable<T> Get(QueryParams queryParams)
        {
            List<FilterGroup> filters = queryParams.GetFilters();
            List<string> includes = queryParams.GetIncludes();
            List<SortModel> order = queryParams.GetSort() ?? DefaultOrder();
            return Get(filters, order, includes.ToArray());
        }

        private List<SortModel> DefaultOrder()
        {
            return new List<SortModel>()
            {
                new SortModel
                {
                    property = "Id",
                    direction = "asc"
                }
            };
        }

        public IQueryable<T> Get(List<FilterModel> filters, List<SortModel> order, params string[] includes)
        {
            FilterGroup item = new FilterGroup
            {
                Filters = filters
            };
            List<FilterGroup> list = new List<FilterGroup>();
            list.Add(item);
            return Get(list, order, includes);
        }

        public IQueryable<T> Get(List<FilterGroup> filters, List<SortModel> order, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }

        public T FirstOrDefault()
        {
            return DbSet.FirstOrDefault();
        }
    }
}
