using System;

namespace GenerateUmbracoDocTypeModels.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreTypeAttribute: Attribute
    {
        public IgnoreTypeAttribute(bool ignore)
        {
            Ignore = ignore;
        }

        public bool Ignore { get; }
    }
}
