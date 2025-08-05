using ApiBase.Core.Common.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public interface IBindingResolver
    {
        MemberAssignment Resolver(MemberInitResolver resolvedorMemberInit, int nivel, Expression parentExp, PropertyInfo propSrc, PropertyInfo propDest);
    }
}
