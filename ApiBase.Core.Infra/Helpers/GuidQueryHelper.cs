using ApiBase.Core.Domain.View;
using ApiBase.Core.Entities;
using ApiBase.Core.Infra.Query;
using System.Linq.Expressions;

namespace ApiBase.Core.Infra.Helpers
{
    public class GuidQueryHelper
    {
        public ConsultationView Page<T, TView>(IQueryable<T> query, QueryParams queryParams) where T : IdentifierGuid where TView : IdGuidView, new()
        {
            IQueryable<TView> projected = ApplyQuery(query, queryParams).To().New<TView>();
            IQueryable<object> shaped = ApplyFields(projected, queryParams);

            return new ConsultationView
            {
                Total = shaped.Count(),
                Content = ExecutePagination(shaped, queryParams)
            };
        }

        public ConsultationView Page<T>(IQueryable<T> query, QueryParams queryParams)
            where T : class
        {
            return Page(query, queryParams, null);
        }

        public ConsultationView Page<T>(IQueryable<T> query, QueryParams queryParams, Expression<Func<T, bool>> predicate)
            where T : class
        {
            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyQuery(query, queryParams);
            IQueryable<object> shaped = ApplyFields(query, queryParams);

            return new ConsultationView
            {
                Total = shaped.Count(),
                Content = ExecutePagination(shaped, queryParams)
            };
        }

        public IQueryable<T> ApplyQuery<T>(IQueryable<T> query, QueryParams queryParams)
            where T : class
        {
            var sorting = BuildOrderBy<T>(queryParams);
            var queryBuilder = new QueryBuilder<T>();
            var filters = queryParams.GetFilters();

            queryBuilder.Build(query, filters, sorting);
            return queryBuilder.Query;
        }

        public List<T> ExecutePagination<T>(IQueryable<T> query, QueryParams queryParams)
        {
            int page = queryParams.page.GetValueOrDefault(1);
            int limit = queryParams.limit.GetValueOrDefault(25);

            return query.Skip((page - 1) * limit).Take(limit).ToList();
        }

        public IQueryable<T> OrderBy<T>(IQueryable<T> query, QueryParams queryParams)
            where T : class
        {
            var sortList = BuildOrderBy<T>(queryParams);
            return new OrderByQuery().Build(query, sortList);
        }

        public List<SortModel> BuildOrderBy<T>(QueryParams queryParams)
        {
            var sortList = queryParams.GetSort();
            if (sortList != null && sortList.Count > 0)
                return sortList;

            if (typeof(T).GetProperty("Id") != null)
            {
                return new List<SortModel>
                {
                    new SortModel
                    {
                        property = "Id",
                        direction = "asc"
                    }
                };
            }

            return new List<SortModel>();
        }

        public IQueryable<object> ApplyFields<T>(IQueryable<T> query, QueryParams queryParams)
            where T : class
        {
            var fields = queryParams.GetFields();
            if (fields.Count > 0)
                return query.SelectDynamic(fields);

            return query.Cast<object>();
        }
    }
}
