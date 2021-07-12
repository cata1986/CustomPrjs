using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateUmbracoDocTypeModels.Model
{
    public interface ITestHasGiftShopItemPickerDetailsElement
    {
        ITestGiftShopItemPickerDetailsElement GiftShopItemPickerDetails { get; set; }
    }
    public class TestHasGiftShopItemPickerDetailsElement : BaseContentModel, ITestHasGiftShopItemPickerDetailsElement
    {
        public ITestGiftShopItemPickerDetailsElement GiftShopItemPickerDetails { get; set; }

        public const string ModelTypeAlias = "testHasGiftShopItemPickerDetailsElement";
        public const string Property_GiftShopItemPickerDetails = "testGiftShopItemPickerDetails";
    }
}
