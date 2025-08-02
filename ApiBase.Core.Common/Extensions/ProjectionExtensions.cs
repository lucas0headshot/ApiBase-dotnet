using ApiBase.Core.Common.Projection;
using System.Collections.Generic;
using System.Linq;

namespace ApiBase.Core.Common.Extensions
{
    public static class ProjectionExtensions
    {
        public static Projection<TSource> Project<TSource>(this IEnumerable<TSource> source) where TSource : class
            => new Projection<TSource>(source.AsQueryable());

        public static ObjectProjection<TSource> Project<TSource>(this TSource source) where TSource : class
        => source == null ? null : new ObjectProjection<TSource>(new List<TSource> { source }.AsQueryable());
    }
}
