using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Discounts.Abstract;

namespace Trendyol.Shopping.Business.Campaigns.Concrete
{
    public class Campaign : IDiscount
    {
        public IDiscountStrategy DiscountStrategy { get; set; }
        public Category Category { get; set; }
        public uint MinimumItems { get; set; }
        public decimal DiscountQuantity { get; set; }

        public Campaign(Category category, uint minItems, decimal discountQuantity, IDiscountStrategy discountStrategy)
        {
            Category = category;
            MinimumItems = minItems;
            DiscountStrategy = discountStrategy;
            DiscountQuantity = discountQuantity;
        }

        public bool ApplyDiscount(ShoppingCart shoppingCart)
        {
            uint productQuantity = shoppingCart.GetProductQuantityOfCategory(Category);
            if (productQuantity > MinimumItems){
                DiscountStrategy.ApplyDiscount(shoppingCart, DiscountQuantity);
                return true;
            }
            return false;
        }

        #region Operator Overloading
        public static bool operator ==(Campaign campaign, Campaign otherCampaign) => campaign.Category == otherCampaign.Category &&
                   campaign.MinimumItems == otherCampaign.MinimumItems &&
                   campaign.DiscountQuantity == otherCampaign.DiscountQuantity &&
                   campaign.DiscountStrategy.GetType() == otherCampaign.DiscountStrategy.GetType();

        public static bool operator !=(Campaign campaign, Campaign otherCampaign) =>
            !(campaign.Category == otherCampaign.Category &&
                   campaign.MinimumItems == otherCampaign.MinimumItems &&
                   campaign.DiscountQuantity == otherCampaign.DiscountQuantity &&
                   campaign.DiscountStrategy.GetType() == otherCampaign.DiscountStrategy.GetType());
        #endregion

    }
}
