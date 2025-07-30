using ApiBase.Core.Common.Attributes;
using System.Collections;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class BindingFactory
    {
        private readonly Dictionary<Type, IBindingResolver> _cache = new();

        private IBindingResolver GetResolverInstance(Type type)
            => _cache.TryGetValue(type, out var resolver)
                ? resolver
                : _cache[type] = (IBindingResolver)Activator.CreateInstance(type);

        public IBindingResolver GetResolver(PropertyInfo sourceProp, PropertyInfo destProp)
        {
            if (sourceProp.PropertyType == destProp.PropertyType)
                return GetResolverInstance(typeof(DefaultBindingResolver));

            if (typeof(IEnumerable).IsAssignableFrom(sourceProp.PropertyType) && sourceProp.PropertyType.IsGenericType)
                return GetResolverInstance(typeof(ListBindingResolver));

            if (destProp.PropertyType.IsClass)
            {
                if (Attribute.IsDefined(sourceProp, typeof(ComplexProjectionAttribute)) || Attribute.IsDefined(destProp, typeof(ComplexProjectionAttribute)))
                    return GetResolverInstance(typeof(ComplexBindingResolver));

                return GetResolverInstance(typeof(AssociationBindingResolver));
            }

            return GetResolverInstance(typeof(ConversionBindingResolver));
        }
    }
}
