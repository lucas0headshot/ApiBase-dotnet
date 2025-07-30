using ApiBase.Core.Common.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class ConversionBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolve(MemberInitResolver resolver, int depth, Expression source, PropertyInfo srcProp, PropertyInfo destProp)
        {
            var converted = Expression.Convert(Expression.Property(source, srcProp), destProp.PropertyType);
            return Expression.Bind(destProp, converted);
        }
    }
}
