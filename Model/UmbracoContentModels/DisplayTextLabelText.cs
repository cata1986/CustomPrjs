using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateUmbracoDocTypeModels.Model.UmbracoContentModels
{
    public interface IDisplayTextLabelText : IBaseElementModel
    {
        string Text { get; set; }
    }

    public class DisplayTextLabelText : BaseElementModel, IDisplayTextLabelText
    {
        public const string ModelTypeAlias = "displayTextLabelText";
        public const string Property_Text = "text";

        public string Text { get; set; }
    }
}
