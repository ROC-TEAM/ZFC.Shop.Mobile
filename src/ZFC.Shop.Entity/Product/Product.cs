using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class Product : BaseEntity
    {
        /// <summary>
        ///ProductTypeId
        /// </summary>
        public int ProductTypeId { get; set; }
        /// <summary>
        ///ParentGroupedProductId
        /// </summary>
        public int ParentGroupedProductId { get; set; }
        /// <summary>
        ///VisibleIndividually
        /// </summary>
        public bool VisibleIndividually { get; set; }
        /// <summary>
        ///Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///ShortDescription
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        ///FullDescription
        /// </summary>
        public string FullDescription { get; set; }
        /// <summary>
        ///AdminComment
        /// </summary>
        public string AdminComment { get; set; }
        /// <summary>
        ///ProductTemplateId
        /// </summary>
        public int ProductTemplateId { get; set; }
        /// <summary>
        ///VendorId
        /// </summary>
        public int VendorId { get; set; }
        /// <summary>
        ///ShowOnHomePage
        /// </summary>
        public bool ShowOnHomePage { get; set; }
        /// <summary>
        ///MetaKeywords
        /// </summary>
        public string MetaKeywords { get; set; }
        /// <summary>
        ///MetaDescription
        /// </summary>
        public string MetaDescription { get; set; }
        /// <summary>
        ///MetaTitle
        /// </summary>
        public string MetaTitle { get; set; }
        /// <summary>
        ///AllowCustomerReviews
        /// </summary>
        public bool AllowCustomerReviews { get; set; }
        /// <summary>
        ///ApprovedRatingSum
        /// </summary>
        public int ApprovedRatingSum { get; set; }
        /// <summary>
        ///NotApprovedRatingSum
        /// </summary>
        public int NotApprovedRatingSum { get; set; }
        /// <summary>
        ///ApprovedTotalReviews
        /// </summary>
        public int ApprovedTotalReviews { get; set; }
        /// <summary>
        ///NotApprovedTotalReviews
        /// </summary>
        public int NotApprovedTotalReviews { get; set; }
        /// <summary>
        ///SubjectToAcl
        /// </summary>
        public bool SubjectToAcl { get; set; }
        /// <summary>
        ///LimitedToStores
        /// </summary>
        public bool LimitedToStores { get; set; }
        /// <summary>
        ///Sku
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        ///ManufacturerPartNumber
        /// </summary>
        public string ManufacturerPartNumber { get; set; }
        /// <summary>
        ///Gtin
        /// </summary>
        public string Gtin { get; set; }
        /// <summary>
        ///IsGiftCard
        /// </summary>
        public bool IsGiftCard { get; set; }
        /// <summary>
        ///GiftCardTypeId
        /// </summary>
        public int GiftCardTypeId { get; set; }
        /// <summary>
        ///OverriddenGiftCardAmount
        /// </summary>
        public decimal OverriddenGiftCardAmount { get; set; }
        /// <summary>
        ///RequireOtherProducts
        /// </summary>
        public bool RequireOtherProducts { get; set; }
        /// <summary>
        ///RequiredProductIds
        /// </summary>
        public string RequiredProductIds { get; set; }
        /// <summary>
        ///AutomaticallyAddRequiredProducts
        /// </summary>
        public bool AutomaticallyAddRequiredProducts { get; set; }
        /// <summary>
        ///IsDownload
        /// </summary>
        public bool IsDownload { get; set; }
        /// <summary>
        ///DownloadId
        /// </summary>
        public int DownloadId { get; set; }
        /// <summary>
        ///UnlimitedDownloads
        /// </summary>
        public bool UnlimitedDownloads { get; set; }
        /// <summary>
        ///MaxNumberOfDownloads
        /// </summary>
        public int MaxNumberOfDownloads { get; set; }
        /// <summary>
        ///DownloadExpirationDays
        /// </summary>
        public int DownloadExpirationDays { get; set; }
        /// <summary>
        ///DownloadActivationTypeId
        /// </summary>
        public int DownloadActivationTypeId { get; set; }
        /// <summary>
        ///HasSampleDownload
        /// </summary>
        public bool HasSampleDownload { get; set; }
        /// <summary>
        ///SampleDownloadId
        /// </summary>
        public int SampleDownloadId { get; set; }
        /// <summary>
        ///HasUserAgreement
        /// </summary>
        public bool HasUserAgreement { get; set; }
        /// <summary>
        ///UserAgreementText
        /// </summary>
        public string UserAgreementText { get; set; }
        /// <summary>
        ///IsRecurring
        /// </summary>
        public bool IsRecurring { get; set; }
        /// <summary>
        ///RecurringCycleLength
        /// </summary>
        public int RecurringCycleLength { get; set; }
        /// <summary>
        ///RecurringCyclePeriodId
        /// </summary>
        public int RecurringCyclePeriodId { get; set; }
        /// <summary>
        ///RecurringTotalCycles
        /// </summary>
        public int RecurringTotalCycles { get; set; }
        /// <summary>
        ///IsRental
        /// </summary>
        public bool IsRental { get; set; }
        /// <summary>
        ///RentalPriceLength
        /// </summary>
        public int RentalPriceLength { get; set; }
        /// <summary>
        ///RentalPricePeriodId
        /// </summary>
        public int RentalPricePeriodId { get; set; }
        /// <summary>
        ///IsShipEnabled
        /// </summary>
        public bool IsShipEnabled { get; set; }
        /// <summary>
        ///IsFreeShipping
        /// </summary>
        public bool IsFreeShipping { get; set; }
        /// <summary>
        ///ShipSeparately
        /// </summary>
        public bool ShipSeparately { get; set; }
        /// <summary>
        ///AdditionalShippingCharge
        /// </summary>
        public decimal AdditionalShippingCharge { get; set; }
        /// <summary>
        ///DeliveryDateId
        /// </summary>
        public int DeliveryDateId { get; set; }
        /// <summary>
        ///IsTaxExempt
        /// </summary>
        public bool IsTaxExempt { get; set; }
        /// <summary>
        ///TaxCategoryId
        /// </summary>
        public int TaxCategoryId { get; set; }
        /// <summary>
        ///IsTelecommunicationsOrBroadcastingOrElectronicServices
        /// </summary>
        public bool IsTelecommunicationsOrBroadcastingOrElectronicServices { get; set; }
        /// <summary>
        ///ManageInventoryMethodId
        /// </summary>
        public int ManageInventoryMethodId { get; set; }
        /// <summary>
        ///ProductAvailabilityRangeId
        /// </summary>
        public int ProductAvailabilityRangeId { get; set; }
        /// <summary>
        ///UseMultipleWarehouses
        /// </summary>
        public bool UseMultipleWarehouses { get; set; }
        /// <summary>
        ///WarehouseId
        /// </summary>
        public int WarehouseId { get; set; }
        /// <summary>
        ///StockQuantity
        /// </summary>
        public int StockQuantity { get; set; }
        /// <summary>
        ///DisplayStockAvailability
        /// </summary>
        public bool DisplayStockAvailability { get; set; }
        /// <summary>
        ///DisplayStockQuantity
        /// </summary>
        public bool DisplayStockQuantity { get; set; }
        /// <summary>
        ///MinStockQuantity
        /// </summary>
        public int MinStockQuantity { get; set; }
        /// <summary>
        ///LowStockActivityId
        /// </summary>
        public int LowStockActivityId { get; set; }
        /// <summary>
        ///NotifyAdminForQuantityBelow
        /// </summary>
        public int NotifyAdminForQuantityBelow { get; set; }
        /// <summary>
        ///BackorderModeId
        /// </summary>
        public int BackorderModeId { get; set; }
        /// <summary>
        ///AllowBackInStockSubscriptions
        /// </summary>
        public bool AllowBackInStockSubscriptions { get; set; }
        /// <summary>
        ///OrderMinimumQuantity
        /// </summary>
        public int OrderMinimumQuantity { get; set; }
        /// <summary>
        ///OrderMaximumQuantity
        /// </summary>
        public int OrderMaximumQuantity { get; set; }
        /// <summary>
        ///AllowedQuantities
        /// </summary>
        public string AllowedQuantities { get; set; }
        /// <summary>
        ///AllowAddingOnlyExistingAttributeCombinations
        /// </summary>
        public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }
        /// <summary>
        ///NotReturnable
        /// </summary>
        public bool NotReturnable { get; set; }
        /// <summary>
        ///DisableBuyButton
        /// </summary>
        public bool DisableBuyButton { get; set; }
        /// <summary>
        ///DisableWishlistButton
        /// </summary>
        public bool DisableWishlistButton { get; set; }
        /// <summary>
        ///AvailableForPreOrder
        /// </summary>
        public bool AvailableForPreOrder { get; set; }
        /// <summary>
        ///PreOrderAvailabilityStartDateTimeUtc
        /// </summary>
        public DateTime PreOrderAvailabilityStartDateTimeUtc { get; set; }
        /// <summary>
        ///CallForPrice
        /// </summary>
        public bool CallForPrice { get; set; }
        /// <summary>
        ///Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        ///OldPrice
        /// </summary>
        public decimal OldPrice { get; set; }
        /// <summary>
        ///ProductCost
        /// </summary>
        public decimal ProductCost { get; set; }
        /// <summary>
        ///CustomerEntersPrice
        /// </summary>
        public bool CustomerEntersPrice { get; set; }
        /// <summary>
        ///MinimumCustomerEnteredPrice
        /// </summary>
        public decimal MinimumCustomerEnteredPrice { get; set; }
        /// <summary>
        ///MaximumCustomerEnteredPrice
        /// </summary>
        public decimal MaximumCustomerEnteredPrice { get; set; }
        /// <summary>
        ///BasepriceEnabled
        /// </summary>
        public bool BasepriceEnabled { get; set; }
        /// <summary>
        ///BasepriceAmount
        /// </summary>
        public decimal BasepriceAmount { get; set; }
        /// <summary>
        ///BasepriceUnitId
        /// </summary>
        public int BasepriceUnitId { get; set; }
        /// <summary>
        ///BasepriceBaseAmount
        /// </summary>
        public decimal BasepriceBaseAmount { get; set; }
        /// <summary>
        ///BasepriceBaseUnitId
        /// </summary>
        public int BasepriceBaseUnitId { get; set; }
        /// <summary>
        ///MarkAsNew
        /// </summary>
        public bool MarkAsNew { get; set; }
        /// <summary>
        ///MarkAsNewStartDateTimeUtc
        /// </summary>
        public DateTime MarkAsNewStartDateTimeUtc { get; set; }
        /// <summary>
        ///MarkAsNewEndDateTimeUtc
        /// </summary>
        public DateTime MarkAsNewEndDateTimeUtc { get; set; }
        /// <summary>
        ///HasTierPrices
        /// </summary>
        public bool HasTierPrices { get; set; }
        /// <summary>
        ///HasDiscountsApplied
        /// </summary>
        public bool HasDiscountsApplied { get; set; }
        /// <summary>
        ///Weight
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        ///Length
        /// </summary>
        public decimal Length { get; set; }
        /// <summary>
        ///Width
        /// </summary>
        public decimal Width { get; set; }
        /// <summary>
        ///Height
        /// </summary>
        public decimal Height { get; set; }
        /// <summary>
        ///AvailableStartDateTimeUtc
        /// </summary>
        public DateTime AvailableStartDateTimeUtc { get; set; }
        /// <summary>
        ///AvailableEndDateTimeUtc
        /// </summary>
        public DateTime AvailableEndDateTimeUtc { get; set; }
        /// <summary>
        ///DisplayOrder
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        ///Published
        /// </summary>
        public bool Published { get; set; }
        /// <summary>
        ///Deleted
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        ///CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
        /// <summary>
        ///UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
