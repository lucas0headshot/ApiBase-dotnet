using ApiBase.Core.Common.Resolvers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class ListBindingResolver : IBindingResolver
    {
        private readonly MethodInfo _selectMethod;

        private readonly MethodInfo _toListMethod;

        public ListBindingResolver()
        {
            _selectMethod = typeof(Enumerable).GetMethods().FirstOrDefault((MethodInfo p) => p.Name == "Select");
            _toListMethod = typeof(Enumerable).GetMethods().FirstOrDefault((MethodInfo p) => p.Name == "ToList");
        }

        public MemberAssignment Resolver(MemberInitResolver resolvedorMemberInit, int nivel, Expression parentExp, PropertyInfo propSrc, PropertyInfo propDest)
        {
            Type type = propSrc.PropertyType.GetGenericArguments().First();
            Type type2 = propDest.PropertyType.GetGenericArguments().First();
            ParameterExpression parameterExpression = Expression.Parameter(type, $"p{nivel}");
            LambdaExpression lambdaExpression = Expression.Lambda(resolvedorMemberInit.Resolver(nivel, parameterExpression, type, type2), parameterExpression);
            MethodInfo method = _selectMethod.MakeGenericMethod(type, type2);
            MemberExpression memberExpression = Expression.Property(parentExp, propSrc);
            MethodCallExpression arg = Expression.Call(method, new Expression[2] { memberExpression, lambdaExpression });
            MethodCallExpression expression = Expression.Call(_toListMethod.MakeGenericMethod(type2), arg);
            return Expression.Bind(propDest, expression);
        }
    }
}
