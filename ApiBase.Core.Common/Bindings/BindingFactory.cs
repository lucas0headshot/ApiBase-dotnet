using ApiBase.Core.Common.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ApiBase.Core.Common.Bindings
{
    public class BindingFactory
    {
        private readonly Dictionary<Type, IBindingResolver> _cache = new();

        public BindingFactory()
        {
            _cache = new Dictionary<Type, IBindingResolver>();
        }

        private IBindingResolver GetResolvedor(Type type)
        {
            if (_cache.TryGetValue(type, out IBindingResolver value))
            {
                return value;
            }

            return _cache[type] = (IBindingResolver)Activator.CreateInstance(type);
        }

        public IBindingResolver GetInstance(PropertyInfo propSrc, PropertyInfo propDest)
        {
            if (propSrc.PropertyType == propDest.PropertyType)
            {
                return GetResolvedor(typeof(DefaultBindingResolver));
            }

            if (propSrc.PropertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(propSrc.PropertyType))
            {
                return GetResolvedor(typeof(ListBindingResolver));
            }

            if (propDest.PropertyType.IsClass)
            {
                if (Attribute.IsDefined(propSrc, typeof(ComplexBindingResolver)) || Attribute.IsDefined(propDest, typeof(ComplexBindingResolver)))
                {
                    return GetResolvedor(typeof(ComplexBindingResolver));
                }

                return GetResolvedor(typeof(AssociationBindingResolver));
            }

            return GetResolvedor(typeof(ConversionBindingResolver));
        }
    }
}
