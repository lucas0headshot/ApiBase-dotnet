using ApiBase.Core.Infra.Bindings;
using System.Linq.Expressions;

namespace ApiBase.Core.Infra.Resolvers
{
    public class MemberInitResolver
    {
        private readonly BindingFactory _bindingFactory;

        public MemberInitResolver()
        {
            _bindingFactory = new BindingFactory();
        }

        public MemberInitExpression Resolve(int level, Expression parentExpression, Type sourceType, Type destinationType)
        {
            level++;

            var destinationProperties = destinationType.GetProperties().Where(p => p.CanWrite && p.SetMethod != null && p.SetMethod.IsPublic).ToList();

            var bindings = new List<MemberBinding>();

            foreach (var destinationProperty in destinationProperties)
            {
                var sourceProperty = sourceType.GetProperty(destinationProperty.Name);
                if (sourceProperty == null)
                    continue;

                var assignment = _bindingFactory.GetInstance(sourceProperty, destinationProperty).Resolve(this, level, parentExpression, sourceProperty, destinationProperty);

                bindings.Add(assignment);
            }

            return Expression.MemberInit(Expression.New(destinationType), bindings);
        }
    }
}
