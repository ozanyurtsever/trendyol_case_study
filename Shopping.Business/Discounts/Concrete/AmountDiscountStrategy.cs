using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Discounts.Abstract;

namespace Trendyol.Shopping.Business.Discounts.Concrete
{
    public class AmountDiscountStrategy : IDiscountStrategy
    {
        public void ApplyDiscount(ShoppingCart shoppingCart, decimal discountQuantity)
        {
            var newAmount = shoppingCart.TotalAmount - discountQuantity;
            shoppingCart.DiscountedTotalAmount = newAmount > 0 ? newAmount : 0;
            shoppingCart.AppliedCampaignDiscount += discountQuantity;
        }
    }
}
