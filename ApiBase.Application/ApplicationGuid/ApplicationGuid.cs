using ApiBase.Application.Base;
using ApiBase.Domain.Entities;
using ApiBase.Domain.Interfaces;
using ApiBase.Domain.Query;
using ApiBase.Domain.View;
using ApiBase.Infra.Extensions;
using ApiBase.Infra.Helpers;
using ApiBase.Infra.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Application.ApplicationGuid
{
    public abstract class ApplicationGuid<TEntity, TRepository, TView> : ApplicationBase<TEntity, TRepository>, IApplicationGuid<TView> where TEntity : EntityGuid, new() where TRepository : IRepositoryBase<TEntity> where TView : IdGuidView, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApplicationGuid(IUnitOfWork unitOfWork,
                               TRepository repository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual GetView Get(QueryParams queryParams)
        {
            IQueryable<TEntity> query = base.Repository.Get().Where(DefaultFilter());

            GetView result = new GuidQueryHelper().Page<TEntity, TView>(query, queryParams);

            result.Content = _unitOfWork.BuildCustomFieldsList<TEntity>(result.Content.Cast<object>().ToList());

            return result;
        }

        public virtual object Get(Guid id)
        {
            IQueryable<TEntity> query = base.Repository.Get().Where(DefaultFilter());
            GetView consultationView = new GuidQueryHelper().Page<TEntity, TView>(query, QueryParams.FilterById(id));

            if (consultationView.Content != null && consultationView.Content.Any())
            {
                return _unitOfWork.BuildCustomFieldsList<TEntity>(consultationView.Content.ToList()).FirstOrDefault();
            }

            return new { };
        }

        public object Get(Guid id, List<string> fields)
        {
            IQueryable<TEntity> query = base.Repository.Where(e => e.Id == id).Where(DefaultFilter());

            object? result;

            if (fields == null || fields.Count == 0)
            {
                result = query.Project().To<TView>().FirstOrDefault();
            }
            else
            {
                var propertyMap = typeof(TView)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => fields.Contains(p.Name))
                    .ToDictionary(p => p.Name, p => p.PropertyType);

                var dynamicType = CustomTypeBuilder.CreateType(propertyMap);
                result = query.Project().To(dynamicType).FirstOrDefault();
            }

            return result ?? null!;
        }

        public virtual Expression<Func<TEntity, bool>> DefaultFilter()
        {
            return (TEntity e) => true;
        }
    }
}
