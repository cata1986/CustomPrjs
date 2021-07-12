using System;

namespace GenerateUmbracoDocTypeModels.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CustomTypeAttribute: Attribute
    {
        public CustomTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; }
    }
}
