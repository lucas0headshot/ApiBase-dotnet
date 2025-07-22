using Core.DTOs;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyQueryParameters<T>(this IQueryable<T> query, QueryParams? parameters)
        {
            if (parameters == null) return query;

            if (parameters.Filter is { Count: > 0 })
            {
                foreach (var filter in parameters.Filter)
                {
                    if (filter.Value == null || string.IsNullOrWhiteSpace(filter.Property)) continue;

                    var op = filter.Operator.ToLowerInvariant();
                    var prop = filter.Property;

                    switch (op)
                    {
                        case "equal":
                            query = query.Where($"{prop} == @0", filter.Value);
                            break;
                        case "notequal":
                            query = query.Where($"{prop} != @0", filter.Value);
                            break;
                        case "contains":
                            query = query.Where($"{prop}.Contains(@0)", filter.Value);
                            break;
                        case "in":
                            query = query.Where($"@0.Contains({prop})", filter.Value);
                            break;
                        case "gt":
                            query = query.Where($"{prop} > @0", filter.Value);
                            break;
                        case "lt":
                            query = query.Where($"{prop} < @0", filter.Value);
                            break;
                    }
                }
            }

            if (parameters.Sort is { Count: > 0 })
            {
                var orderClauses = parameters.Sort
                    .Where(s => !string.IsNullOrWhiteSpace(s.Property))
                    .Select(s => $"{s.Property} {s.Sort?.ToUpperInvariant() ?? "ASC"}");

                if (orderClauses.Any())
                    query = query.OrderBy(string.Join(",", orderClauses));
            }


            query = query.Skip(parameters.Start).Take(parameters.Limit);

            return query;
        }
    }
}
