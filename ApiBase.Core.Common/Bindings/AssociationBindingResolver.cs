using ApiBase.Core.Common.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class AssociationBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolve(MemberInitResolver resolver, int depth, Expression source, PropertyInfo srcProp, PropertyInfo destProp)
        {
            var innerSource = Expression.Property(source, srcProp);
            var ifNull = Expression.Constant(null, destProp.PropertyType);
            var resolved = resolver.Resolve(innerSource, srcProp.PropertyType, destProp.PropertyType, depth);
            var condition = Expression.Condition(
                Expression.Equal(innerSource, Expression.Constant(null)),
                ifNull,
                resolved
            );

            return Expression.Bind(destProp, condition);
        }
    }
}
