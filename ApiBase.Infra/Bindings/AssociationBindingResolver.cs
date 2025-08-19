using ApiBase.Infra.Interfaces;
using ApiBase.Infra.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Infra.Bindings
{
    public class AssociationBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolve(MemberInitResolver memberInitResolver, int level, Expression parentExpression, PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            MemberExpression memberExpression = Expression.Property(parentExpression, sourceProperty);
            MemberInitExpression ifFalse = memberInitResolver.Resolve(level, memberExpression, sourceProperty.PropertyType, destinationProperty.PropertyType);
            ConditionalExpression expression = Expression.Condition(Expression.Equal(memberExpression, Expression.Constant(null)), Expression.Constant(null, destinationProperty.PropertyType), ifFalse);
            return Expression.Bind(destinationProperty, expression);
        }
    }
}
