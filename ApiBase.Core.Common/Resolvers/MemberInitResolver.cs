using ApiBase.Core.Common.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Resolvers
{
    public class MemberInitResolver
    {
        private readonly BindingFactory _fabricaResolvedorBinding;

        public MemberInitResolver()
        {
            _fabricaResolvedorBinding = new BindingFactory();
        }

        public MemberInitExpression Resolver(int nivel, Expression parentExp, Type typeSrc, Type typeDest)
        {
            nivel++;
            List<PropertyInfo> list = (from p in typeDest.GetProperties()
                                       where p.CanWrite && p.GetSetMethod() != null && p.GetSetMethod().IsPublic
                                       select p).ToList();
            List<MemberBinding> list2 = new List<MemberBinding>();
            foreach (PropertyInfo item2 in list)
            {
                PropertyInfo property = typeSrc.GetProperty(item2.Name);
                if (!(property == null))
                {
                    MemberAssignment item = _fabricaResolvedorBinding.GetInstance(property, item2).Resolver(this, nivel, parentExp, property, item2);
                    list2.Add(item);
                }
            }

            return Expression.MemberInit(Expression.New(typeDest), list2);
        }
    }
}
