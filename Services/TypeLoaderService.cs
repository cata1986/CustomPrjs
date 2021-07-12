using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenerateUmbracoDocTypeModels.Services
{
    public interface ITypeLoaderService
    {
        Type GetType(string typeName, string assemblyNamespacePrefix);

    }
    public class TypeLoaderService : ITypeLoaderService
    {
        ConcurrentDictionary<string, Type> _types;
        public TypeLoaderService()
        {
            _types = new ConcurrentDictionary<string, Type>();
        }
        public Type GetType(string typeName, string assemblyNamespacePrefix)
        {
            string key = GetTypeDictionaryKey(typeName, assemblyNamespacePrefix);

            return
                _types.GetOrAdd(
                    key,
                    (itemKey) => GetTypeFromAppDomain(typeName, assemblyNamespacePrefix)
                );
        }

        private Type GetTypeFromAppDomain(string typeName, string assemblyNamespacePrefix)
        {
            var allAssemblies = GetAssembliesForNamespace(assemblyNamespacePrefix);
            if (allAssemblies == null)
            {
                return null;
            }
            var allTypes = allAssemblies.SelectMany(a => a.GetTypes().Where(t => t.IsClass));
            if (allTypes == null)
            {
                return null;
            }

            var match = allTypes.FirstOrDefault(t => t.Name.EndsWith(typeName));
            return match;
        }

        private IEnumerable<Assembly> GetAssembliesForNamespace(string assemblyNamespacePrefix)
        {
            return AppDomain
                .CurrentDomain
                    .GetAssemblies()
                    .Where(a => a.FullName.StartsWith(assemblyNamespacePrefix))
                    .ToArray();
        }

        private string GetTypeDictionaryKey(string typeName, string typeNamespacePart)
        {
            return $"{typeName}.{typeNamespacePart}";
        }
    }
}
