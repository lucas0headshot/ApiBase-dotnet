using ApiBase.Core.Domain.Interfaces;
using ApiBase.Core.Infra.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Infra.Bindings
{
    public class DefaultBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolve(MemberInitResolver memberInitResolver, int level, Expression parentExpression, PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            return Expression.Bind(destinationProperty, Expression.Property(parentExpression, sourceProperty));
        }
    }
}
