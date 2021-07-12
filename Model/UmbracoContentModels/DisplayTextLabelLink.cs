using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.Models;

namespace GenerateUmbracoDocTypeModels.Model.UmbracoContentModels
{
    public interface IDisplayTextLabelLink : IBaseElementModel
    {
        Link Link { get; set; }
    }

    public class DisplayTextLabelLink : BaseElementModel, IDisplayTextLabelLink
    {
        public const string ModelTypeAlias = "displayTextLabelLink";
        public const string Property_Link = "link";

        public Link Link { get; set; }
    }
}
