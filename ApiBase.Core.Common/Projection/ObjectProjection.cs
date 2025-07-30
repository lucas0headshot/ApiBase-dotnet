namespace ApiBase.Core.Common.Projection
{
    public class ObjectProjection<TSource> : Projection<TSource> where TSource : class
    {
        public ObjectProjection(IQueryable<TSource> source) : base(source) { }

        public IQueryable<TDestination> To<TDestination>()
        {
            var selector = new ProjectionBuilder().Build<TSource, TDestination>();
            return QueryableSource.Select(selector);
        }
    }
}
