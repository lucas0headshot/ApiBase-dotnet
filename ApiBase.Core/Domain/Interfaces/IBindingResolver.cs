using ApiBase.Core.Infra.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Domain.Interfaces
{
    public interface IBindingResolver
    {
        MemberAssignment Resolve(MemberInitResolver memberInitResolver, int level, Expression parentExpression, PropertyInfo sourceProperty, PropertyInfo destinationProperty);
    }
}
