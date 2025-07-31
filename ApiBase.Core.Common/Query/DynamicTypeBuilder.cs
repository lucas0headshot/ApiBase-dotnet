using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

namespace ApiBase.Core.Common.Query
{
    public static class DynamicTypeBuilder
    {
        private static readonly ModuleBuilder ModuleBuilder;
        private static readonly ConcurrentDictionary<string, Type> TypeCache = new();

        static DynamicTypeBuilder()
        {
            var assemblyName = new AssemblyName("DynamicLinqTypes");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        }

        public static Type GetDynamicType(IDictionary<string, Type> fields)
        {
            string typeKey = string.Join(";", fields.Select(f => $"{f.Key}:{f.Value.Name}"));

            if (TypeCache.TryGetValue(typeKey, out var cachedType))
                return cachedType;

            var typeBuilder = ModuleBuilder.DefineType(
                $"DynamicLinqType_{Guid.NewGuid():N}",
                TypeAttributes.Public | TypeAttributes.Class);

            foreach (var field in fields)
            {
                typeBuilder.DefineField(field.Key, field.Value, FieldAttributes.Public);
            }

            var newType = typeBuilder.CreateTypeInfo()!.AsType();
            TypeCache[typeKey] = newType;

            return newType;
        }
    }
}
