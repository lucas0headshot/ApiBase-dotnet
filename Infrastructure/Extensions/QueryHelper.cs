using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class QueryHelper
    {
        public static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, Dictionary<string, string> filters)
        {
            if (filters == null || !filters.Any()) return query;
            
            foreach(var filter in filters)
            {
                var property = typeof(T).GetProperty(filter.Key);

                if (property == null) continue;

                query = query.Where(e => EF.Property<string>(e, filter.Key).Contains(filter.Value));
            }

            return query;
        }

        public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, string? orderBy, string? direction)
        {
            if (string.IsNullOrEmpty(orderBy)) return query;

            return direction?.ToLower() == "desc"
                ? query.OrderByDescending(e => EF.Property<object>(e, orderBy))
                : query.OrderBy(e => EF.Property<object>(e, orderBy));
        }
    }
}
