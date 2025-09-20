using ApiBase.Domain.Query;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ApiBase.Infra.Extensions
{
    public class DynamicWhereBuilder
    {
        public IQueryable<T> Build<T>(IQueryable<T> query, List<FilterGroup> filterGroups)
        {
            if (filterGroups == null || !filterGroups.Any())
                return query;

            try
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                Expression finalExpression = null;

                foreach (var group in filterGroups)
                {
                    Expression groupExpression = null;

                    foreach (var filter in group.Filters)
                    {
                        var property = GetProperty(typeof(T), filter.property, out MemberExpression memberExpression, parameter);

                        if (property == null || string.IsNullOrEmpty(filter.value?.ToString()))
                            continue;

                        var condition = BuildCondition(filter, property, memberExpression, query);
                        if (condition == null)
                            continue;

                        if (filter.Not)
                            condition = Expression.Not(condition);

                        groupExpression = groupExpression == null
                            ? condition
                            : (filter.And ? Expression.AndAlso(groupExpression, condition) : Expression.OrElse(groupExpression, condition));
                    }

                    if (groupExpression != null)
                    {
                        finalExpression = finalExpression == null
                            ? groupExpression
                            : (group.And ? Expression.AndAlso(finalExpression, groupExpression) : Expression.OrElse(finalExpression, groupExpression));
                    }
                }

                if (finalExpression != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(finalExpression, parameter);
                    query = query.Where(lambda);
                }

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while building filter: {ex.Message}", ex);
            }
        }

        private PropertyInfo GetProperty(Type type, string path, out MemberExpression memberExpr, ParameterExpression param)
        {
            memberExpr = null;
            PropertyInfo property = null;
            var segments = path.Split('.');

            foreach (var segment in segments)
            {
                property = type.GetProperty(segment, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                    return null;

                memberExpr = memberExpr == null
                    ? Expression.Property(param, property)
                    : Expression.Property(memberExpr, property);

                type = property.PropertyType;
            }

            return property;
        }

        private Expression BuildCondition(FilterModel filter, PropertyInfo property, MemberExpression memberExpr, IQueryable query)
        {
            object value = ConvertValue(filter, property, memberExpr);
            if (value == null)
                return null;

            var right = BuildRightExpression(filter, property, value);
            return FilterExpressionFactory.Create(filter, property, memberExpr, right, value, query);
        }

        private object ConvertValue(FilterModel filter, PropertyInfo property, MemberExpression memberExpr)
        {
            return ValueConverter.Convert(filter, property, memberExpr);
        }

        private Expression BuildRightExpression(FilterModel filter, PropertyInfo property, object value)
        {
            return Expression.Constant(value, property.PropertyType);
        }
    }
}
