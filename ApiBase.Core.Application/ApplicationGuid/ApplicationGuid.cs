using ApiBase.Core.Application.Base;
using ApiBase.Core.Common.Extensions;
using ApiBase.Core.Common.Query;
using ApiBase.Core.Domain.Entities;
using ApiBase.Core.Domain.Interfaces;
using ApiBase.Core.Domain.View;
using ApiBase.Core.Infra.Helpers;
using System.Linq.Expressions;

namespace ApiBase.Core.Application.ApplicationGuid
{
    public abstract class ApplicationGuid<TEntity, TRepository, TView> : ApplicationBase<TEntity, TRepository>, IApplicationGuid<TView> where TEntity : IdentifierGuid, new() where TRepository : IRepositoryBase<TEntity> where TView : IdGuidView, new()
    {
        public ApplicationGuid(IUnitOfWork unitOfWork, TRepository repository) : base(unitOfWork, repository) { }

        public GetView Get(QueryParams queryParams)
        {
            IQueryable<TEntity> query = base.Repository.Get().Where(DefaultFilter());
            GetView consultationView = new GuidQueryHelper().Page<TEntity, TView>(query, queryParams);
            //IList<object> content
            return new GetView();
        }

        public object Get(Guid id)
        {
            IQueryable<TEntity> query = base.Repository.Get().Where(DefaultFilter());
            GetView consultationView = new GuidQueryHelper().Page<TEntity, TView>(query, QueryParams.FilterById(id));

            if (consultationView.Content != null && consultationView.Content.Any())
            {
                //
            }

            return new { };
        }

        public object Get(Guid id, List<string> fields)
        {
            IQueryable<TEntity> query = base.Repository.Where((TEntity e) => e.Id == id).Where(DefaultFilter());
            dynamic value = (fields.Any() ? query.Qry().New<TView>().FirstOrDefault() : query.Qry().New(TypeBuilderHelper.CreateType(typeof(TView), fields)).FirstOrDefault());

            if (value == null) return null;

            return new { }; //
        }

        public virtual Expression<Func<TEntity, bool>> DefaultFilter()
        {
            return (TEntity e) => true;
        }
    }
}
