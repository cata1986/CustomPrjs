using GenerateUmbracoDocTypeModels.Model;
using Umbraco.Core.Models.PublishedContent;

namespace GenerateUmbracoDocTypeModels.Mappers
{
    public interface IContentMapper<TResult>
        where TResult : BaseContentModel
    {
        // Having the destination as a parameter and the return object allows for mapping an object from scratch
        // as well as passing in an initialized, or partially mapped object to different mappers
        TResult Map(IPublishedContent source, TResult destination = null);
    }
}
