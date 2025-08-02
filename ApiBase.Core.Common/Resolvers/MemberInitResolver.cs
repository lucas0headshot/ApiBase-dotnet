using ApiBase.Core.Common.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiBase.Core.Common.Resolvers
{
    public class MemberInitResolver
    {
        private readonly BindingFactory _factory = new();

        public MemberInitExpression Resolve(Expression source, Type sourceType, Type targetType, int depth)
        {
            var bindings = new List<MemberBinding>();
            var targetProps = targetType.GetProperties().Where(p => p.CanWrite);

            foreach (var destProp in targetProps)
            {
                var srcProp = sourceType.GetProperty(destProp.Name);
                if (srcProp == null) continue;

                var resolver = _factory.GetResolver(srcProp, destProp);
                bindings.Add(resolver.Resolve(this, depth + 1, source, srcProp, destProp));
            }

            return Expression.MemberInit(Expression.New(targetType), bindings);
        }
    }
}
