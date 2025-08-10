using ApiBase.Core.Domain.Query;
using ApiBase.Core.Infra.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBase.Core.Infra.Query
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
