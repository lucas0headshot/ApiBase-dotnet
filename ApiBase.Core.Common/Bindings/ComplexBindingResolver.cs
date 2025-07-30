using ApiBase.Core.Common.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class ComplexBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolve(MemberInitResolver resolver, int depth, Expression source, PropertyInfo srcProp, PropertyInfo destProp)
        {
            var innerSource = Expression.Property(source, srcProp);
            var innerInit = resolver.Resolve(innerSource, srcProp.PropertyType, destProp.PropertyType, depth);
            return Expression.Bind(destProp, innerInit);
        }
    }
}
