using ApiBase.Core.Common.Resolvers;
using System;
using System.Linq.Expressions;

namespace ApiBase.Core.Common.Projection
{
    public class ProjectionBuilder
    {
        public Expression<Func<TSource, TDestination>> Build<TSource, TDestination>()
        {
            var parameter = Expression.Parameter(typeof(TSource), "src");
            var body = new MemberInitResolver().Resolve(parameter, typeof(TSource), typeof(TDestination), 0);
            return Expression.Lambda<Func<TSource, TDestination>>(body, parameter);
        }

        public Expression<Func<TSource, object>> Build<TSource>(Type targetType)
        {
            var parameter = Expression.Parameter(typeof(TSource), "src");
            var body = new MemberInitResolver().Resolve(parameter, typeof(TSource), targetType, 0);
            return Expression.Lambda<Func<TSource, object>>(body, parameter);
        }
    }
}
