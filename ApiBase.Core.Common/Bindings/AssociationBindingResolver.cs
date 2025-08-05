using ApiBase.Core.Common.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class AssociationBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolver(MemberInitResolver resolvedorMemberInit, int nivel, Expression parentExp, PropertyInfo propSrc, PropertyInfo propDest)
        {
            MemberExpression memberExpression = Expression.Property(parentExp, propSrc);
            MemberInitExpression ifFalse = resolvedorMemberInit.Resolver(nivel, memberExpression, propSrc.PropertyType, propDest.PropertyType);
            ConditionalExpression expression = Expression.Condition(Expression.Equal(memberExpression, Expression.Constant(null)), Expression.Constant(null, propDest.PropertyType), ifFalse);
            return Expression.Bind(propDest, expression);
        }
    }
}
