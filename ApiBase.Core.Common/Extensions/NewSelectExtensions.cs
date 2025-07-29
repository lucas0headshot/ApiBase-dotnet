namespace ApiBase.Core.Common.Extensions
{
    public class NewSelectExtensions
    {
        public static NewSelect<TSource> ToNewSelect<TSource>(this IEnumerable<TSource> source) where TSource : class
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new NewSelect<TSource>(source.AsQueryable());
        }

        public static NewSelect<TSource, TSource> ToNewSelect<TSource>(this TSource source) where TSource : class
        {
            if (source == null) return null;
            return new NewSelect<TSource, TSource>(new List<TSource> { source }.AsQueryable());
        }
    }
}
