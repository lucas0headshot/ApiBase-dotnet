using ApiBase.Core.Common.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class DefaultBindingResolver : IBindingResolver
    {
        public MemberAssignment Resolver(MemberInitResolver resolvedorMemberInit, int nivel, Expression parentExp, PropertyInfo propSrc, PropertyInfo propDest)
        {
            return Expression.Bind(propDest, Expression.Property(parentExp, propSrc));
        }
    }
}
