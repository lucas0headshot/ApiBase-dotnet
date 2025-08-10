using ApiBase.Core.Domain.Interfaces;
using ApiBase.Core.Infra.Resolvers;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Infra.Bindings
{
    public class ListBindingResolver : IBindingResolver
    {
        private readonly MethodInfo _selectMethod;

        private readonly MethodInfo _toListMethod;

        public ListBindingResolver()
        {
            _selectMethod = typeof(Enumerable)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(Enumerable.Select) && m.GetParameters().Length == 2)!;

            _toListMethod = typeof(Enumerable)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(Enumerable.ToList) && m.GetParameters().Length == 1)!;
        }

        public MemberAssignment Resolve(MemberInitResolver memberInitResolver, int level, Expression parentExpression, PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            Type sourceElementType = sourceProperty.PropertyType.GetGenericArguments().First();
            Type destinationElementType = destinationProperty.PropertyType.GetGenericArguments().First();

            ParameterExpression parameter = Expression.Parameter(sourceElementType, $"p{level}");

            LambdaExpression selectorLambda = Expression.Lambda(memberInitResolver.Resolve(level, parameter, sourceElementType, destinationElementType), parameter);

            MethodInfo selectGeneric = _selectMethod.MakeGenericMethod(sourceElementType, destinationElementType);
            MethodInfo toListGeneric = _toListMethod.MakeGenericMethod(destinationElementType);

            MemberExpression sourceMember = Expression.Property(parentExpression, sourceProperty);
            MethodCallExpression selectCall = Expression.Call(selectGeneric, sourceMember, selectorLambda);
            MethodCallExpression toListCall = Expression.Call(toListGeneric, selectCall);

            return Expression.Bind(destinationProperty, toListCall);
        }
    }
}
