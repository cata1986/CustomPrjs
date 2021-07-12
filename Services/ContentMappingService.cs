using GenerateUmbracoDocTypeModels.Helpers;
using GenerateUmbracoDocTypeModels.Mappers;
using GenerateUmbracoDocTypeModels.Model;
using System;
using System.Linq;
using System.Reflection;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Zone.UmbracoMapper.V8;

namespace GenerateUmbracoDocTypeModels.Services
{
    public interface IContentMappingService
    {
        TResult Map<TResult>(IPublishedElement source, TResult target = null)
            where TResult : BaseElementModel;

        TResult Map<TResult>(IPublishedContent source, TResult target = null)
            where TResult : BaseContentModel;

        object MapElement(IPublishedElement source, Type targetType, object target = null);
        object MapContent(IPublishedContent source, Type targetType, object target = null);

        object MapElement(IPublishedElement source, string typeName, string typeAssemblyPrefix, object target = null);
        object MapContent(IPublishedContent source, string typeName, string typeAssemblyPrefix, object target = null);

    }

    /// <summary>
    /// Service to map umbraco content to a strongly typed object. This allows the mapping tool to be 
    /// abstracte out, so that different mapping libraries can be used as and when they are available
    /// Current use is
    /// a) A custom mapper is defined, use that
    /// b) No customer mapper defined, use UmbracoMapper: https://github.com/AndyButland/UmbracoMapper
    /// Ideal is to use UmbMapper: https://github.com/JimBobSquarePants/UmbMapper - however this isn't 
    /// ready for Umbraco 8 yet.
    /// </summary>
    public class ContentMappingService : IContentMappingService
    {
        private readonly IFactory _factory;
        private readonly IUmbracoMapper _mapper;
        private readonly ITypeLoaderService _typeLoaderService;

        public ContentMappingService(IFactory factory, ITypeLoaderService typeLoaderService)
        {
            _factory = factory;
            _typeLoaderService = typeLoaderService;
            _mapper = new UmbracoMapper();
        }

        public TResult Map<TResult>(IPublishedElement source, TResult target = null)
            where TResult : BaseElementModel
        {
            target = InitializationHelper.EnsureTarget(target);

            var mapperType = typeof(IElementMapper<>).MakeGenericType(typeof(TResult));
            var mapper = _factory.TryGetInstance(mapperType);

            // If a custom apper has been registered in IoC Container, use it
            if (mapper != null)
            {
                return (TResult)((dynamic)mapper).Map(source, target);
            }

            // Otherwise, use UmbracoMapper
            return MapWithUmbracoMapper<TResult>(source, target);
        }

        public TResult Map<TResult>(IPublishedContent source, TResult target = null)
            where TResult : BaseContentModel
        {
            target = InitializationHelper.EnsureTarget(target);

            var mapperType = typeof(IContentMapper<>).MakeGenericType(typeof(TResult));
            var mapper = _factory.TryGetInstance(mapperType);

            // If a custom apper has been registered in IoC Container, use it
            if (mapper != null)
            {
                return (TResult)((dynamic)mapper).Map(source, target);
            }

            // Otherwise, use UmbracoMapper
            return MapWithUmbracoMapper<TResult>(source, target);
        }

        public object MapElement(IPublishedElement source, Type targetType, object target = null)
        {
            var mapMethod = GetMapMethod(targetType);
            if (targetType == null)
            {
                return null;
            }

            var genericMethod = mapMethod.MakeGenericMethod(targetType);

            return genericMethod.Invoke(this, new object[2] { source, target });
        }

        public object MapContent(IPublishedContent source, Type targetType, object target = null)
        {
            var mapMethod = GetMapMethod(targetType);
            if (targetType == null)
            {
                return null;
            }

            var genericMethod = mapMethod.MakeGenericMethod(targetType);

            return genericMethod.Invoke(this, new object[2] { source, target });
        }

        public object MapElement(IPublishedElement source, string typeName, string typeAssemblyPrefix, object target = null)
        {
            var type = _typeLoaderService.GetType(typeName, typeAssemblyPrefix);

            if (type == null)
            {
                return null;
            }

            return MapElement(source, type, target);
        }

        public object MapContent(IPublishedContent source, string typeName, string typeAssemblyPrefix, object target = null)
        {
            var type = _typeLoaderService.GetType(typeName, typeAssemblyPrefix);

            if (type == null)
            {
                return null;
            }

            return MapContent(source, type, target);
        }

        private TResult MapWithUmbracoMapper<TResult>(IPublishedElement source, TResult target)
            where TResult : BaseElementModel
        {
            _mapper.Map<TResult>(source, target);

            return target;
        }

        private TResult MapWithUmbracoMapper<TResult>(IPublishedContent source, TResult target)
            where TResult : BaseContentModel
        {
            _mapper.Map<TResult>(source, target);

            return target;
        }

        private MethodInfo GetMapMethod(Type targetType)
        {
            var mapMethods =
                 this
                    .GetType()
                    .GetMethods()
                    .Where(
                        m =>
                            m.Name.Equals(nameof(ContentMappingService.Map))
                            && m.IsGenericMethod
                            && (typeof(BaseElementModel).IsAssignableFrom(m.ReturnType))
                    );

            if (typeof(BaseContentModel).IsAssignableFrom(targetType))
            {
                // Return the method with BaseContentModel generic type
                return mapMethods.FirstOrDefault(m => (typeof(BaseContentModel).IsAssignableFrom(m.ReturnType)));
            }
            if (typeof(BaseElementModel).IsAssignableFrom(targetType))
            {
                // Return the method with BaseElementModel generic type
                // NOT BaseContentModel, as BaseContentModel : BaseElementModel
                return
                    mapMethods
                        .FirstOrDefault(m =>
                            !typeof(BaseContentModel).IsAssignableFrom(m.ReturnType)
                            && typeof(BaseElementModel).IsAssignableFrom(m.ReturnType)
                        );
            }

            return null;
        }
    }
}
