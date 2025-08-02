using ApiBase.Core.Common.Resolvers;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class ListBindingResolver : IBindingResolver
    {
        private readonly MethodInfo _selectMethod;
        private readonly MethodInfo _toListMethod;

        public ListBindingResolver()
        {
            _selectMethod = typeof(Enumerable).GetMethods()
                .First(m => m.Name == "Select" && m.GetParameters().Length == 2);

            _toListMethod = typeof(Enumerable).GetMethods()
                .First(m => m.Name == "ToList" && m.GetParameters().Length == 1);
        }

        public MemberAssignment Resolve(MemberInitResolver resolver, int depth, Expression source, PropertyInfo srcProp, PropertyInfo destProp)
        {
            var sourceCollection = Expression.Property(source, srcProp);
            var srcItemType = srcProp.PropertyType.GetGenericArguments().First();
            var destItemType = destProp.PropertyType.GetGenericArguments().First();

            var param = Expression.Parameter(srcItemType, $"x{depth}");
            var body = resolver.Resolve(param, srcItemType, destItemType, depth);
            var selector = Expression.Lambda(body, param);

            var selectCall = Expression.Call(_selectMethod.MakeGenericMethod(srcItemType, destItemType), sourceCollection, selector);
            var toListCall = Expression.Call(_toListMethod.MakeGenericMethod(destItemType), selectCall);

            return Expression.Bind(destProp, toListCall);
        }
    }
}
