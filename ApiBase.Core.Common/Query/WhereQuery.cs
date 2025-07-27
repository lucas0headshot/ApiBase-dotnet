using System.Linq.Expressions;
using System.Reflection;

namespace ApiBase.Core.Common.Query
{
    public class WhereQuery
    {
        public IQueryable<T> BuildWhere<T>(IQueryable<T> query, List<FilterGroup> filterGroups)
        {
            if (filterGroups == null)
            {
                return query;
            }

            try
            {
                Type typeFromHandle = typeof(T);
                ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "x");
                Expression expression = null;

                foreach (FilterGroup filterGroup in filterGroups)
                {
                    Expression expression2 = null;

                    foreach (FilterModel filter in filterGroup.Filters)
                    {
                        MemberExpression memberExpression = null;
                        PropertyInfo property = typeFromHandle.GetProperty(filter.property);

                        if (property == null)
                        {
                            string[] array = filter.property.Split('.');
                            Type type = typeFromHandle;
                            string[] array2 = array;

                            foreach (string name in array2)
                            {
                                property = type.GetProperty(name);

                                if (!(property == null))
                                {
                                    type = property.PropertyType;
                                    PropertyInfo member = property;
                                    memberExpression = ((memberExpression != null) ? Expression.MakeMemberAccess(memberExpression, member) : Expression.MakeMemberAccess(parameterExpression, member));

                                    if (property.PropertyType.IsGenericType)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            memberExpression = Expression.MakeMemberAccess(parameterExpression, property);
                        }


                        if (property == null)
                        {
                            throw new Exception("Property '" + filter.property + "' not found.");
                        }


                        if (filter.value.ToString() == "")
                        {
                            continue;
                        }

                        Expression expression3 = BuildCondition(filter, property, parameterExpression, memberExpression, query);

                        if (expression3 != null)
                        {
                            if (filter.Not)
                            {
                                expression3 = Expression.Not(expression3);
                            }

                            expression2 = ((expression2 != null) ? ((!filter.And) ? Expression.Or(expression2, expression3) : Expression.AndAlso(expression2, expression3)) : expression3);
                        }
                    }
                    if (expression2 != null)
                    {
                        expression = ((expression != null) ? ((!filterGroup.And) ? Expression.Or(expression, expression2) : Expression.AndAlso(expression, expression2)) : expression2);
                    }
                }

                if (expression != null)
                {
                    Expression<Func<T, bool>> expression4 = null;
                    expression4 = Expression.Lambda<Func<T, bool>>(expression, new ParameterExpression[1] { parameterExpression });
                    query = query.Where(expression4);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error when filtering '" + ex.Message + "' ");
            }

            return query;
        }

        private Expression BuildCondition(FilterModel filtro, PropertyInfo property, ParameterExpression param, MemberExpression member, IQueryable query)
        {
            Expression result = null;
            object obj = null;

            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                Type type = property.PropertyType.GetGenericArguments()[0];
                property = type.GetProperty(filtro.PropertyName);

                if (property == null)
                {
                    return null;
                }


            }
            else
            {

            }
        }

    }
}
