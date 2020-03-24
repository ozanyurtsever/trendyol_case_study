using System;
using Trendyol.Shopping.Business.Campaigns.Enums;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Discounts.Concrete;

namespace Trendyol.Shopping.Business.Campaigns.Concrete
{
    public static class CampaignFactory
    {
        public static Campaign GenerateCampaign(Category category, uint minItems, decimal discountQuantity,  DiscountType discountType)
        {
            if(discountType == DiscountType.Amount)
            {
                AmountDiscountStrategy amountDiscountStrategy = new AmountDiscountStrategy();
                return new Campaign(category, minItems, discountQuantity, amountDiscountStrategy);
            }
            else if(discountType == DiscountType.Rate)
            {
                RateDiscountStrategy rateDiscountStrategy = new RateDiscountStrategy();
                return new Campaign(category, minItems, discountQuantity, rateDiscountStrategy);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
