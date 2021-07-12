using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateUmbracoDocTypeModels.Model
{
    [Description("GenerateUmbracoDocTypeModels.Model.TestGiftShopItemPickerDetailsElement")]
    public interface ITestGiftShopItemPickerConfigElement : IBaseElementModel
    {
        string QuantityInputType { get; set; }
        bool QuantityLimitIsUnlimited { get; set; }
        int QuantityLimitMinimum { get; set; }
        int QuantityLimitMaximum { get; set; }
        string Mode { get; set; }
        bool Readonly { get; set; }
        bool ItemButtonDisabled { get; set; }
        bool ItemButtonShowDescription { get; set; }
        bool ItemButtonShowPrice { get; set; }
        bool ItemButtonShowPromotionLabel { get; set; }
        bool ParentSalesItemOptionsShowImages { get; set; }
        string ParentSalesItemOptionsQuantityInputType { get; set; }
        bool NormalItemModifiersShowExpandedByDefault { get; set; }
        bool NormalItemModifiersShowImages { get; set; }
        int MinimumRequiredItems { get; set; }
    }

    public class TestGiftShopItemPickerConfigElement : BaseElementModel, ITestGiftShopItemPickerConfigElement
    {
        public string QuantityInputType { get; set; }
        public bool QuantityLimitIsUnlimited { get; set; }
        public int QuantityLimitMinimum { get; set; }
        public int QuantityLimitMaximum { get; set; }
        public string Mode { get; set; }
        public bool Readonly { get; set; }
        public bool ItemButtonDisabled { get; set; }
        public bool ItemButtonShowDescription { get; set; }
        public bool ItemButtonShowPrice { get; set; }
        public bool ItemButtonShowPromotionLabel { get; set; }
        public bool ParentSalesItemOptionsShowImages { get; set; }
        public string ParentSalesItemOptionsQuantityInputType { get; set; }
        public bool NormalItemModifiersShowExpandedByDefault { get; set; }
        public bool NormalItemModifiersShowImages { get; set; }
        public int MinimumRequiredItems { get; set; }
    }
}
