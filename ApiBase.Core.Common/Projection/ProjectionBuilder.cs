using ApiBase.Core.Common.Resolvers;
using System;
using System.Linq.Expressions;

namespace ApiBase.Core.Common.Projection
{
    public class ProjectionBuilder
    {
        public Expression<Func<TSource, TDestination>> Build<TSource, TDestination>()
        {
            Type typeFromHandle = typeof(TSource);
            ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "p");
            return Expression.Lambda<Func<TSource, TDestination>>(new MemberInitResolver().Resolve(0, parameterExpression, typeFromHandle, typeof(TDestination)), new ParameterExpression[1] { parameterExpression });
        }

        public Expression<Func<TSource, object>> Build<TSource>(Type targetType)
        {
            Type typeFromHandle = typeof(TSource);
            ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "p");
            return Expression.Lambda<Func<TSource, object>>(new MemberInitResolver().Resolve(0, parameterExpression, typeFromHandle, targetType), new ParameterExpression[1] { parameterExpression });
        }
    }
}
