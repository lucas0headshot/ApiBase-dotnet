using ApiBase.Core.Common.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class ConversionBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolver(MemberInitResolver resolvedorMemberInit, int nivel, Expression parentExp, PropertyInfo propSrc, PropertyInfo propDest)
        {
            UnaryExpression expression = Expression.Convert(Expression.Property(parentExp, propSrc), propDest.PropertyType);
            return Expression.Bind(propDest, expression);
        }
    }
}
