using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Campaigns.Enums;
using Trendyol.Shopping.Business.Discounts.Concrete;

namespace Trendyol.Shopping.Business.Coupons.Concrete
{
    public class CouponFactory
    {
        public static Coupon GenerateCoupon(decimal minAmount, decimal discountQuantity, DiscountType discountType)
        {
            if (discountType == DiscountType.Amount)
            {
                AmountDiscountStrategy amountDiscountStrategy = new AmountDiscountStrategy();
                return new Coupon(minAmount, discountQuantity, amountDiscountStrategy);
            }
            else if (discountType == DiscountType.Rate)
            {
                RateDiscountStrategy rateDiscountStrategy = new RateDiscountStrategy();
                return new Coupon(minAmount, discountQuantity, rateDiscountStrategy);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
