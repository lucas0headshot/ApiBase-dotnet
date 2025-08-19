using ApiBase.Infra.Interfaces;
using ApiBase.Infra.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Infra.Bindings
{
    public class ConversionBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolve(MemberInitResolver memberInitResolver, int level, Expression parentExpression, PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            UnaryExpression expression = Expression.Convert(Expression.Property(parentExpression, sourceProperty), destinationProperty.PropertyType);
            return Expression.Bind(destinationProperty, expression);
        }
    }
}
