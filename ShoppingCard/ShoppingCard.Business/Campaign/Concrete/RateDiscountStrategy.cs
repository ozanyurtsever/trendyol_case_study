using Shopping.Business.Campaign.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Business.Campaign.Concrete
{
    public class RateDiscountStrategy : Discount, ICampaign
    {
        public double DiscountRate { get; set; }
        public RateDiscountStrategy(Category category, int minItems, double discountRate) : base(category, minItems)
        {
            DiscountRate = discountRate;
        }

        public void ApplyDiscount(ShoppingCard card)
        {
            throw new NotImplementedException();
        }
    }
}
