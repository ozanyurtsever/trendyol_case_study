using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Discounts.Abstract;

namespace Trendyol.Shopping.Business.Coupons.Concrete
{
    public class Coupon : IDiscount
    {
        public IDiscountStrategy DiscountStrategy { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal DiscountQuantity { get; set; }

        public Coupon(decimal minAmount, decimal discountQuantity, IDiscountStrategy discountStrategy)
        {
            MinimumAmount = minAmount;
            DiscountStrategy = discountStrategy;
            DiscountQuantity = discountQuantity;
        }

        public bool ApplyDiscount(ShoppingCart shoppingCart)
        {
            if (shoppingCart.TotalAmount > MinimumAmount)
            {
                DiscountStrategy.ApplyDiscount(shoppingCart, DiscountQuantity);
                return true;
            }
            return false;
        }
    }
}
