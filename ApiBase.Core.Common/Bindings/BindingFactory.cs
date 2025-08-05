using ApiBase.Core.Common.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class BindingFactory
    {
        private readonly Dictionary<Type, IBindingResolver> _resolverCache;

        public BindingFactory()
        {
            _resolverCache = new Dictionary<Type, IBindingResolver>();
        }

        private IBindingResolver GetResolver(Type resolverType)
        {
            if (_resolverCache.TryGetValue(resolverType, out var resolver))
            {
                return resolver;
            }

            var instance = (IBindingResolver)Activator.CreateInstance(resolverType)!;
            _resolverCache[resolverType] = instance;
            return instance;
        }

        public IBindingResolver GetInstance(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            var sourceType = sourceProperty.PropertyType;
            var destinationType = destinationProperty.PropertyType;

            if (sourceType == destinationType)
            {
                return GetResolver(typeof(DefaultBindingResolver));
            }

            if (sourceType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(sourceType))
            {
                return GetResolver(typeof(ListBindingResolver));
            }

            if (destinationType.IsClass)
            {
                if (Attribute.IsDefined(sourceProperty, typeof(ComplexBindingResolver)) ||
                    Attribute.IsDefined(destinationProperty, typeof(ComplexBindingResolver)))
                {
                    return GetResolver(typeof(ComplexBindingResolver));
                }

                return GetResolver(typeof(AssociationBindingResolver));
            }

            return GetResolver(typeof(ConversionBindingResolver));
        }
    }
}
