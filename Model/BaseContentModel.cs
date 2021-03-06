using GenerateUmbracoDocTypeModels.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateUmbracoDocTypeModels.Model
{
    public interface IBaseContentModel
    {
        int Id { get; set; }
    }
    public class BaseContentModel : BaseElementModel, IBaseElementModel
    {
        [IgnoreType(true)]
        public int Id { get; set; }
    }
}
