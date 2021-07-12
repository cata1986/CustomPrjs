using GenerateUmbracoDocTypeModels.CustomAttributes;
using GenerateUmbracoDocTypeModels.Model.UmbracoContentModels;
using GenerateUmbracoDocTypeModels.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace GenerateUmbracoDocTypeModels.Model
{
    public class TestModel
    {
        protected readonly IContentMappingService _mapper;
        public TestModel()
        {
            _mapper = new ContentMappingService(Current.Factory, new TypeLoaderService());
        }

        [Description("test for description value")]        
        public Guid Id { get; set; }

        [DefaultValue("some default name")]
        public string Name { get; set; }
        public int Age { get; set; }

        public DateTime Date { get; set; }
        public Link CTALink { get; set; }
        public CroppedImageBase Image { get; set; }
        
        [IgnoreType(true)]
        public DisplayTextLabel[] SomeDisplayTextLabel { get { return Create(SomeDisplayTextLabelCMS); } }//{ get; set; }
        [CustomType("DisplayTextLabel")]
        public IEnumerable<IPublishedElement> SomeDisplayTextLabelCMS { get; set; }


        private DisplayTextLabel[] Create(IEnumerable<IPublishedElement> displayTextElements)
        {
            var labelParts = new List<DisplayTextLabel>();

            try
            {
                foreach (var element in displayTextElements)
                {
                    DisplayTextLabel labelPart = null;

                    switch (element.ContentType.Alias)
                    {
                        case DisplayTextLabelText.ModelTypeAlias:
                            var displayTextLabelTextContent = _mapper.Map<DisplayTextLabelText>(element);
                            labelPart = Create(displayTextLabelTextContent);
                            break;

                        case DisplayTextLabelLink.ModelTypeAlias:
                            var displayTextLabelLinkContent = _mapper.Map<DisplayTextLabelLink>(element);
                            labelPart = Create(displayTextLabelLinkContent);
                            break;

                    }

                    if (labelPart != null)
                    {
                        labelParts.Add(labelPart);
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.Error<BaseDisplayTextLabelFactory>(ex);
                throw;
            }

            return
                    labelParts.Count > 0
                    ? labelParts.ToArray()
                    : null;
        }

        private DisplayTextLabel Create(DisplayTextLabelLink linkLabel)
        {
            if (!string.IsNullOrWhiteSpace(linkLabel?.Link?.Url))
            {
                var link = linkLabel?.Link;

                var displayTextLabel =
                    new DisplayTextLabel
                    {
                        Text = link.Name,
                        Href = link.Url
                    };

                if (link.Target == "_blank")
                {
                    displayTextLabel.OpenInNewTab = true;
                }

                return displayTextLabel;
            }

            return null;
        }
        private DisplayTextLabel Create(DisplayTextLabelText textLabel)
        {
            return new DisplayTextLabel { Text = textLabel.Text };
        }
    }
}
