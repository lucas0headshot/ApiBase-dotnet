using ApiBase.Domain.Query;
using ApiBase.Infra.Extensions;

namespace ApiBase.Infra.Query
{
    public class QueryBuilder<T>
    {
        public IQueryable<T> Query { get; set; }
        public void Build(IQueryable<T> query, List<FilterGroup> filters, List<SortModel> sorters)
        {
            query = new DynamicWhereBuilder().Build(query, filters);
            query = new OrderByQuery().ApplySorting(query, sorters);
            Query = query;
        }
    }
}
