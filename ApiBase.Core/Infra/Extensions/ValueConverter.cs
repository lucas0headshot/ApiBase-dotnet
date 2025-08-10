using ApiBase.Core.Domain.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiBase.Core.Infra.Extensions
{
    public static class ValueConverter
    {
        public static object Convert(FilterModel filter, PropertyInfo property, MemberExpression memberExpr)
        {
            var value = filter.value;
            if (value == null) return null;

            try
            {
                var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                if (value is IList list)
                {
                    if (targetType == typeof(Guid))
                        return list.Cast<object>().Select(x => Guid.Parse(x.ToString())).ToList();
                }

                if (targetType.IsEnum)
                    return Enum.Parse(targetType, value.ToString());

                return System.Convert.ChangeType(value, targetType);
            }
            catch
            {
                return null;
            }
        }
    }
}
