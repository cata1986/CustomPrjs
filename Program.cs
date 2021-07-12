using GenerateUmbracoDocTypeModels.CustomAttributes;
using GenerateUmbracoDocTypeModels.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace GenerateUmbracoDocTypeModels
{
    class Program
    {
        static void Main(string[] args)
        {
            var folderPathInsideGeneratedFile = $"Feature/{nameof(TestModel)}/Elements";
            var folderPath = GetFolderPath(folderPathInsideGeneratedFile);

            GenerateUmbracoDocType(typeof(TestModel), nameof(TestModel), folderPath, folderPathInsideGeneratedFile, true);



            var type = typeof(TestGiftShopItemPickerDetailsElement);            

            var interfTypes = type.GetInterfaces();
            var compositions = new List<(Guid docTypeId, string docTypeName)>();
            foreach (var interf in interfTypes)
            {
                if (interf.Name == nameof(IBaseElementModel) || interf.Name == nameof(IBaseContentModel))
                    continue;

                var descriptionAttribute = (DescriptionAttribute[])interf.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttribute.Length != 0)
                {
                    string classFullName = descriptionAttribute[0].Description;
                    if (classFullName == type.FullName)
                    {
                        //var type2 = Type.GetType(classFullName);
                        var folderPathInsideGeneratedFile1 = $"Feature/{type.Name}/Elements";
                        var folderPath1 = GetFolderPath(folderPathInsideGeneratedFile1);

                        var result = GenerateUmbracoDocType(interf, interf.Name.Remove(0, 1), folderPath1, folderPathInsideGeneratedFile1, true);
                        compositions.Add(result);
                    }
                }
            }

            if (compositions.Any())
            {
                var folderPathInsideGeneratedFile1 = $"Feature/{type.Name}/Compositions";
                var folderPath1 = GetFolderPath(folderPathInsideGeneratedFile1);

                GenerateUmbracoCompositionDocType(type, type.Name, folderPath1, folderPathInsideGeneratedFile1, true, compositions);
            }


            Console.WriteLine("done!");
            //Console.ReadKey();
        }

        private static string GetFolderPath(string folderPathInsideGeneratedFile)
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            appPath = @"E:\VistaSource\Develop\Vista.Digital.Curzon\_localwebroot\";
            var rootPrjPath = appPath.Replace(@"_localwebroot\", string.Empty);

            var savePartialPath = @"src\Project\Website\code\uSync\v8\ContentTypes";

            //folderPathInsideGeneratedFile = @"Feature/Test+Model/Elements";
            //var folderPathStoreGeneratedFile = @"feature/test-model/elements";
            var folderPathStoreGeneratedFile = folderPathInsideGeneratedFile.ToLower();

            var folderPath = rootPrjPath + savePartialPath;
            var folders = folderPathStoreGeneratedFile.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var folder in folders)
            {
                folderPath += @"\" + folder;
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        private static void GenerateUmbracoCompositionDocType(Type type, string typeName, string folderPath, string umbracoFolderPath, bool isElement, List<(Guid docTypeId, string docTypeName)> compositions)
        {
            XDocument doc = new XDocument(new XElement("ContentType",
                                                    new XAttribute("Key", Guid.NewGuid()),
                                                    new XAttribute("Alias", typeName),
                                                    new XAttribute("Level", 4),
                                                new XElement("Info",
                                                    new XElement("Name", typeName),
                                                    new XElement("Icon", "icon-document"),
                                                    new XElement("Thumbnail", "folder.png"),
                                                    new XElement("Description"),
                                                    new XElement("AllowAtRoot", "False"),
                                                    new XElement("IsListView", "False"),
                                                    new XElement("Variations", "Nothing"),
                                                    new XElement("IsElement", isElement),
                                                    new XElement("Folder", umbracoFolderPath),
                                                    new XElement("Compositions", 
                                                        from composition in compositions
                                                        select 
                                                            new XElement("Composition",
                                                                new XAttribute("Key", composition.docTypeId), composition.docTypeName)
                                                    ),
                                                    new XElement("DefaultTemplate"),
                                                    new XElement("AllowedTemplates")),
                                                new XElement("Structure"),
                                                new XElement("GenericProperties"),
                                                new XElement("Tabs")
                                                ));

            doc.Save($"{folderPath}/{typeName.ToLower()}.config");
        }

        private static (Guid docTypeId, string docTypeName) GenerateUmbracoDocType(Type type, string typeName, string folderPath, string umbracoFolderPath, bool isElement)
        {
            var docTypeId = Guid.NewGuid();

            var properties = type.GetProperties();

            var descriptionAttributes = properties.Select(p =>
                            Attribute.IsDefined(p, typeof(DescriptionAttribute)) ?
                                p.Name + "_" + (Attribute.GetCustomAttribute(p, typeof(DescriptionAttribute)) as DescriptionAttribute).Description :
                                p.Name
                            ).ToList();

            var defaultValueAttributes = properties.Select(p =>
                            Attribute.IsDefined(p, typeof(DefaultValueAttribute)) ?
                                p.Name + "_" + (Attribute.GetCustomAttribute(p, typeof(DefaultValueAttribute)) as DefaultValueAttribute).Value :
                                p.Name
                            ).ToList();

            var displayTextLabelProperties = properties.Where(p =>
                            Attribute.IsDefined(p, typeof(CustomTypeAttribute)) && (Attribute.GetCustomAttribute(p, typeof(CustomTypeAttribute)) as CustomTypeAttribute).TypeName == nameof(DisplayTextLabel))
                                .Select(p => p.Name)
                                .ToList();

            var ignoreTypes = properties.Where(p =>
                            Attribute.IsDefined(p, typeof(IgnoreTypeAttribute)) && (Attribute.GetCustomAttribute(p, typeof(IgnoreTypeAttribute)) as IgnoreTypeAttribute).Ignore)
                                                .Select(p => p.Name)
                                                .ToList();

            //we will always have the props inside only one content group tab
            var tabName = "Content for " + typeName;

            XDocument doc = new XDocument(new XElement("ContentType",
                                                    new XAttribute("Key", docTypeId),
                                                    new XAttribute("Alias", typeName),
                                                    new XAttribute("Level", 4),
                                                new XElement("Info",
                                                    new XElement("Name", typeName),
                                                    new XElement("Icon", "icon-document"),
                                                    new XElement("Thumbnail", "folder.png"),
                                                    new XElement("Description"),
                                                    new XElement("AllowAtRoot", "False"),
                                                    new XElement("IsListView", "False"),
                                                    new XElement("Variations", "Nothing"),
                                                    new XElement("IsElement", isElement),
                                                    new XElement("Folder", umbracoFolderPath),
                                                    new XElement("Compositions"),
                                                    new XElement("DefaultTemplate"),
                                                    new XElement("AllowedTemplates")),
                                                new XElement("Structure"),
                                                new XElement("GenericProperties",
                                                    from property in properties
                                                    select
                                                        new XElement("GenericProperty",
                                                        new XElement("Key", Guid.NewGuid()),
                                                        new XElement("Name", property.Name),
                                                        new XElement("Alias", property.Name),
                                                        new XElement("Definition", GetUmbracoTypeIdAndName(property.PropertyType.Name).TypeId),
                                                        new XElement("Type", GetUmbracoTypeIdAndName(property.PropertyType.Name).TypeName),
                                                        new XElement("Mandatory", "false"),
                                                        new XElement("Validation"),
                                                        new XElement("Description", new XCData(descriptionAttributes.Any(a => a.StartsWith(property.Name + "_")) ? descriptionAttributes.First(a => a.StartsWith(property.Name + "_")).Remove(0, (property.Name + "_").Length) : string.Empty)),
                                                        new XElement("SortOrder", Array.IndexOf(properties, property) + 1),
                                                        new XElement("Tab", tabName),
                                                        new XElement("Variations", "Nothing"),
                                                        new XElement("MandatoryMessage"),
                                                        new XElement("ValidationRegExpMessage")
                                                        )),
                                                new XElement("Tabs",
                                                    new XElement("Tab",
                                                        new XElement("Caption", tabName),
                                                        new XElement("SortOrder", 0)))
                                                ));

            doc.Save($"{folderPath}/{typeName.ToLower()}.config");

            return (docTypeId, typeName);
        }

        static (string TypeId, string TypeName) GetUmbracoTypeIdAndName(string propertyTypeName)
        {
            switch (propertyTypeName)
            {
                case "Guid":
                case "String":
                    return ("0cc0eba1-9960-42c9-bf9b-60e150b429ae", "Umbraco.TextBox");
                case "Int32":
                    return ("2e6d3631-066e-44b8-aec4-96f09099b2b5", "Umbraco.Integer");
                case "Boolean":
                    return ("92897bc6-a5f3-4ffe-ae27-f2e7e33dda49", "Umbraco.TrueFalse");
                case "Link":
                    return ("c9dad0c6-62ed-49f0-b68f-e16c60fb207f", "Umbraco.MultiUrlPicker");
                case "DateTime":
                    return ("5046194e-4237-453c-a547-15db3a07c4e1", "Umbraco.DateTime");
                case "CroppedImageBase":
                    return ("135d60e0-64d9-49ed-ab08-893c9ba44ae5", "Umbraco.MediaPicker");
                case "DisplayTextLabel[]":
                case "IEnumerable`1":
                    return ("7a8a43d1-402c-4fbe-b7fc-f217aa619436", "Umbraco.NestedContent");
                default:
                    return ("", "undefined");
            }
        }
    }
}
