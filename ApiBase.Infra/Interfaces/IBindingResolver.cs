using ApiBase.Infra.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Infra.Interfaces
{
    public interface IBindingResolver
    {
        MemberAssignment Resolve(MemberInitResolver memberInitResolver, int level, Expression parentExpression, PropertyInfo sourceProperty, PropertyInfo destinationProperty);
    }
}
