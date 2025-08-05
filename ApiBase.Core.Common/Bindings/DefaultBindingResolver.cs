using ApiBase.Core.Common.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class DefaultBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolve(MemberInitResolver memberInitResolver, int level, Expression parentExpression, PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            return Expression.Bind(destinationProperty, Expression.Property(parentExpression, sourceProperty));
        }
    }
}
