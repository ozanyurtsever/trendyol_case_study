using Trendyol.Shopping.Business.Campaigns.Enums;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Cart.Enums;
using Trendyol.Shopping.Business.Coupons.Concrete;
using Xunit;

namespace Shopping.Tests
{
    public class CouponTests
    {
        [Theory]
        [InlineData(DiscountType.Amount, 4980)]
        [InlineData(DiscountType.Rate, 4000)]
        public void ApplyDiscount_ShouldApplyDiscount_IfMinimumAmountSatisfied(DiscountType discountType, decimal expected)
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Category foodCategory = new Category(CategoryType.Food);
            Product television = new Product("Television", 1000, electronicCategory);
            Product apple = new Product("Apple", 5, foodCategory);
            ShoppingCart shoppingCart = new ShoppingCart();
            Coupon coupon = CouponFactory.GenerateCoupon(4000, 20, discountType);

            shoppingCart.AddProduct(television, 4);
            shoppingCart.AddProduct(apple, 200);
            coupon.ApplyDiscount(shoppingCart);

            Assert.Equal(expected, shoppingCart.DiscountedTotalAmount);
        }

        [Fact]
        public void ApplyDiscount_ShouldNotDescreaseTotalAmountValue_IfCartIsEmpty()
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            Coupon coupon = CouponFactory.GenerateCoupon(-1, 20, DiscountType.Amount);

            coupon.ApplyDiscount(shoppingCart);

            Assert.Equal(0, shoppingCart.DiscountedTotalAmount);
        }

    }
}
