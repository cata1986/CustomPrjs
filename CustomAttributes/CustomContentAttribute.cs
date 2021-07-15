using System;

namespace GenerateUmbracoDocTypeModels.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomContentAttribute: Attribute
    {
        public CustomContentAttribute(string contentName)
        {
            ContentName = contentName;
        }

        public string ContentName { get; }
    }
}
