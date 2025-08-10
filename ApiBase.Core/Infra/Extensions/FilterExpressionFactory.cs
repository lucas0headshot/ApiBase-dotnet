using ApiBase.Core.Domain.Enums;
using ApiBase.Core.Domain.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Infra.Extensions
{
    public static class FilterExpressionFactory
    {
        public static Expression Create(FilterModel filter, PropertyInfo property, Expression left, Expression right, object value, IQueryable query)
        {
            switch (filter.GetOperator())
            {
                case FilterOperator.Equal:
                    return Expression.Equal(left, right);

                case FilterOperator.GreaterThan:
                    return Expression.GreaterThan(left, right);

                case FilterOperator.LessThan:
                    return Expression.LessThan(left, right);

                case FilterOperator.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(left, right);

                case FilterOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(left, right);

                case FilterOperator.Contains:
                    return Expression.Call(
                        Expression.Call(left, typeof(string).GetMethod("ToLower", Type.EmptyTypes)),
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                        Expression.Call(right, typeof(string).GetMethod("ToLower", Type.EmptyTypes))
                    );

                case FilterOperator.StartsWith:
                    return Expression.Call(left, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), right);

                case FilterOperator.EndsWith:
                    return Expression.Call(left, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), right);

                case FilterOperator.In:
                    return Expression.Call(Expression.Constant(value), value.GetType().GetMethod("Contains", new[] { left.Type }), left);

                case FilterOperator.InOrNull:
                    var contains = Expression.Call(Expression.Constant(value), value.GetType().GetMethod("Contains", new[] { left.Type }), left);
                    var isNull = Expression.Equal(left, Expression.Constant(null, left.Type));
                    return Expression.OrElse(contains, isNull);

                default:
                    throw new NotImplementedException($"Operator '{filter.Operator}' not implemented.");
            }
        }
    }
}
