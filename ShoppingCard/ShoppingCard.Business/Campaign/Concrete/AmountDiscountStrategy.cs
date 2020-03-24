using Shopping.Business.Campaign.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Business.Campaign.Concrete
{
    public class AmountDiscountStrategy : Discount, ICampaign
    {
        public decimal DiscountAmount { get; set; }
        public AmountDiscountStrategy(Category category, int minItems, decimal discountAmount) : base(category, minItems)
        {
            DiscountAmount = discountAmount;
        }

        public void ApplyDiscount(ShoppingCard card)
        {
            throw new NotImplementedException();
        }
    }
}
