using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.PropertyEditors.ValueConverters;

namespace GenerateUmbracoDocTypeModels.Model
{
    public class CroppedImageBase
    {
        public const string Property_UmbracoFile = "umbracoFile";

        public ImageCropperValue ImageCropDetails { get; set; }

        public bool HasCropDetails =>
            this.ImageCropDetails != null;

        protected string GetFullCropUrl(string alias)
        {
            if (!HasCropDetails)
            {
                return string.Empty;
            }

            var cropUrl = GetCropUrl(alias);

            var src = this.ImageCropDetails?.Src;

            return $"{src}{cropUrl}";
        }

        protected string GetCropUrl(string alias)
        {
            if (!HasCropDetails)
            {
                return string.Empty;
            }

            var url = this.ImageCropDetails?.GetCropUrl(alias);

            return url;
        }
        protected int? GetCropWidth(string alias)
        {
            if (!HasCropDetails)
            {
                return null;
            }

            var crop = ImageCropDetails?.GetCrop(alias);

            if (crop != null)
            {
                return crop.Width;
            }
            return null;
        }
    }
}
