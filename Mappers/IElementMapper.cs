using GenerateUmbracoDocTypeModels.Model;
using Umbraco.Core.Models.PublishedContent;

namespace GenerateUmbracoDocTypeModels.Mappers
{
    public interface IElementMapper<TResult>
        where TResult : BaseElementModel
    {
        // Having the destination as a parameter and the return object allows for mapping an object from scratch
        // as well as passing in an initialized, or partially mapped object to different mappers
        TResult Map(IPublishedElement source, TResult destination = null);
    }
}
