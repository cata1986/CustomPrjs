using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateUmbracoDocTypeModels.Model
{
    public interface ITestGiftShopItemPickerDetailsElement : ITestGiftShopItemPickerConfigElement, ITestGiftShopItemPickerDisplayTextElement, IBaseElementModel
    {

    }

    public class TestGiftShopItemPickerDetailsElement : BaseContentModel, ITestGiftShopItemPickerDetailsElement
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
        public string QuantityCounterLabel { get; set; }
        public string AddToOrderButtonLabel { get; set; }
        public string TotalSavingsLabel { get; set; }
        public string ItemButtonHoverLabel { get; set; }
        public string NoItemsTitle { get; set; }
        public string NoItemsMessage { get; set; }
        public string FailedToLoadDataTitle { get; set; }
        public string FailedToLoadDataMessage { get; set; }
        public string InsufficientModifiersSelectedMessage { get; set; }
        public string FailedToSaveItemSelectionsMessage { get; set; }
        public string FailedToValidateItemSelectionsMessage { get; set; }
        public string InsufficientItemsSelectedMessage { get; set; }
        public string SuccessfullyAddedItemToOrderMessage { get; set; }
        public string ParentSalesItemButtonPriceLabel { get; set; }
        public string ParentSalesItemGenericDiscountLabel { get; set; }
        public string ParentSalesItemDefaultPromptText { get; set; }
        public string ModifierGroupDefaultPromptText { get; set; }
        public string ModifierQuantityLimitMinZeroMaxGreaterThanOneLabel { get; set; }
        public string ModifierQuantityLimitMinOneMaxUnlimitedLabel { get; set; }
        public string ModifierQuantityLimitMinOneMaxOneLabel { get; set; }
        public string ModifierQuantityLimitMinOneMaxGreaterThanOneLabel { get; set; }
        public string ModifierQuantityLimitMinGreaterThanOneMaxUnlimitedLabel { get; set; }
        public string ModifierQuantityLimitMinGreaterThanOneMaxGreaterThanOneLabel { get; set; }
        public string ModifierQuantityLimitMinGreaterThanOneMaxGreaterThanOneAndMinEqualMaxLabel { get; set; }
    }
}
