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
        public string Name { get; set; }
        public Guid Key { get; set; }
        public string ContentTypeAlias { get; set; } // Do we need this?
    }
}
