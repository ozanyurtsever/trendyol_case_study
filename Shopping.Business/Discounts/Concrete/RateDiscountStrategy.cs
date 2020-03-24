using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Discounts.Abstract;

namespace Trendyol.Shopping.Business.Discounts.Concrete
{
    public class RateDiscountStrategy : IDiscountStrategy
    {
        public void ApplyDiscount(ShoppingCart shoppingCart, decimal discountQuantity)
        {
            var amountOfDiscount = (shoppingCart.TotalAmount * discountQuantity) / 100;
            var newAmount = shoppingCart.TotalAmount - amountOfDiscount;
            shoppingCart.DiscountedTotalAmount = newAmount > 0 ? newAmount : 0;
            shoppingCart.AppliedCouponDiscount += amountOfDiscount;
        }
    }
}
