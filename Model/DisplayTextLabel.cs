using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateUmbracoDocTypeModels.Model
{
    public class DisplayTextLabel
    {
        public string Text { get; set; }
        //public string ClassName { get; set; }
        public string Href { get; set; }
        public bool? OpenInNewTab { get; set; } = null;
        public string InternalAction { get; set; }
    }
}
