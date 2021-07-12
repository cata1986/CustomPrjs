using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateUmbracoDocTypeModels.Model
{
    [Description("GenerateUmbracoDocTypeModels.Model.TestGiftShopItemPickerDetailsElement")]
    public interface ITestGiftShopItemPickerDisplayTextElement : IBaseElementModel
    {
        string QuantityCounterLabel { get; set; }
        string AddToOrderButtonLabel { get; set; }
        string TotalSavingsLabel { get; set; }
        string ItemButtonHoverLabel { get; set; }
        string NoItemsTitle { get; set; }
        string NoItemsMessage { get; set; }
        string FailedToLoadDataTitle { get; set; }
        string FailedToLoadDataMessage { get; set; }
        string InsufficientModifiersSelectedMessage { get; set; }
        string FailedToSaveItemSelectionsMessage { get; set; }
        string FailedToValidateItemSelectionsMessage { get; set; }
        string InsufficientItemsSelectedMessage { get; set; }
        string SuccessfullyAddedItemToOrderMessage { get; set; }
        string ParentSalesItemButtonPriceLabel { get; set; }
        string ParentSalesItemGenericDiscountLabel { get; set; }
        string ParentSalesItemDefaultPromptText { get; set; }
        string ModifierGroupDefaultPromptText { get; set; }
        string ModifierQuantityLimitMinZeroMaxGreaterThanOneLabel { get; set; }
        string ModifierQuantityLimitMinOneMaxUnlimitedLabel { get; set; }
        string ModifierQuantityLimitMinOneMaxOneLabel { get; set; }
        string ModifierQuantityLimitMinOneMaxGreaterThanOneLabel { get; set; }
        string ModifierQuantityLimitMinGreaterThanOneMaxUnlimitedLabel { get; set; }
        string ModifierQuantityLimitMinGreaterThanOneMaxGreaterThanOneLabel { get; set; }
        string ModifierQuantityLimitMinGreaterThanOneMaxGreaterThanOneAndMinEqualMaxLabel { get; set; }
    }

    public class TestGiftShopItemPickerDisplayTextElement : BaseElementModel, ITestGiftShopItemPickerDisplayTextElement
    {
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
