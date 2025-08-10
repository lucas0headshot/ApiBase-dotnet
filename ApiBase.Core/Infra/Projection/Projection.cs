using System.Linq.Expressions;

namespace ApiBase.Core.Infra.Projection
{
    public class Projection<TSource> where TSource : class
    {
        protected readonly IQueryable<TSource> QueryableSource;

        public Projection(IQueryable<TSource> source)
        {
            QueryableSource = source;
        }

        public IQueryable<TDestination> To<TDestination>() where TDestination : new()
        {
            var selector = new ProjectionBuilder().Build<TSource, TDestination>();
            return QueryableSource.Select(selector);
        }

        public IQueryable<object> To(Type targetType)
        {
            var selector = new ProjectionBuilder().Build<TSource>(targetType);
            return QueryableSource.Select(selector);
        }

        public Expression<Func<TSource, TDestination>> Expression<TDestination>() where TDestination : new()
            => new ProjectionBuilder().Build<TSource, TDestination>();

        public Func<TSource, TDestination> Compile<TDestination>() where TDestination : new()
            => Expression<TDestination>().Compile();
    }
}
