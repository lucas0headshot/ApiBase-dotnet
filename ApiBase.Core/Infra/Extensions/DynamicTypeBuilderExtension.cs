using System.Collections;
using System.Reflection;

namespace ApiBase.Core.Infra.Extensions
{
    public static class DynamicTypeBuilderExtension
    {
        private class PropertyNode
        {
            public string Name { get; set; } = string.Empty;
            public List<PropertyNode> Children { get; set; } = new();
            public override string ToString() => Name;
        }

        public static Type FromPropertyDictionary(Dictionary<string, Type> properties)
        {
            return CustomTypeBuilder.CreateType(properties);
        }

        public static Type FromPropertyPaths(Type baseType, IEnumerable<string> propertyPaths)
        {
            var propertyGraph = BuildPropertyGraph(propertyPaths);
            return CreateDynamicType(baseType, propertyGraph);
        }

        private static List<PropertyNode> BuildPropertyGraph(IEnumerable<string> paths)
        {
            var splitPaths = paths
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Split('.'))
                .ToList();

            return BuildNodeTree(splitPaths);
        }

        private static List<PropertyNode> BuildNodeTree(List<string[]> segments)
        {
            var grouped = segments.Where(s => s.Length > 0).GroupBy(s => s[0]);
            var result = new List<PropertyNode>();

            foreach (var group in grouped)
            {
                var node = new PropertyNode { Name = group.Key };

                var children = group.Where(s => s.Length > 1).Select(s => s.Skip(1).ToArray()).ToList();

                if (children.Any())
                {
                    node.Children = BuildNodeTree(children);
                }

                result.Add(node);
            }

            return result;
        }

        private static Type CreateDynamicType(Type baseType, List<PropertyNode> properties)
        {
            var propTypes = new Dictionary<string, Type>();

            foreach (var prop in properties)
            {
                var propInfo = baseType.GetProperty(prop.Name);

                if (propInfo == null)
                    continue;

                propTypes[prop.Name] = ResolvePropertyType(propInfo, prop.Children);
            }

            return CustomTypeBuilder.CreateType(propTypes);
        }

        private static Type ResolvePropertyType(PropertyInfo propInfo, List<PropertyNode> children)
        {
            var propType = propInfo.PropertyType;

            if (IsEnumerableButNotString(propType))
            {
                return CreateGenericListType(propType, children);
            }

            if (propType.IsClass && propType != typeof(string))
            {
                return children.Any() ? CreateDynamicType(propType, children) : propType;
            }

            return propType;
        }

        private static Type CreateGenericListType(Type listType, List<PropertyNode> children)
        {
            var elementTypes = listType.GetGenericArguments().Select(type =>
                type.IsPrimitive || type == typeof(string)
                ? type : children.Any() ? CreateDynamicType(type, children)
                : CreateTypeFromAllProperties(type)).ToArray();

            return typeof(List<>).MakeGenericType(elementTypes);
        }

        private static Type CreateTypeFromAllProperties(Type type)
        {
            var props = type.GetProperties().ToDictionary(p => p.Name, p => p.PropertyType);

            return CustomTypeBuilder.CreateType(props);
        }

        private static bool IsEnumerableButNotString(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
        }
    }
}
