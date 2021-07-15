using GenerateUmbracoDocTypeModels.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateUmbracoDocTypeModels.Model
{
    public interface IBaseElementModel
    {
        string Name { get; set; }
        Guid Key { get; set; }
    }
    public class BaseElementModel : IBaseElementModel
    {
        [IgnoreType(true)]
        public string Name { get; set; }
        [IgnoreType(true)]
        public Guid Key { get; set; }
        [IgnoreType(true)]
        public string ContentTypeAlias { get; set; } // Do we need this?
    }
}
