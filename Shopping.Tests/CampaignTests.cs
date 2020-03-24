using Trendyol.Shopping.Business.Campaigns.Concrete;
using Trendyol.Shopping.Business.Campaigns.Enums;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Cart.Enums;
using Xunit;

namespace Shopping.Tests
{
    public class CampaignTests
    {
        [Theory]
        [InlineData(DiscountType.Amount, 4980)]
        [InlineData(DiscountType.Rate, 4000)]
        public void ApplyDiscount_ShouldApplyDiscount_IfSameCategory(DiscountType discountType, decimal expected)
        {
            Category category = new Category(CategoryType.Electronic);
            Product product = new Product("Television", 1000, category);
            ShoppingCart shoppingCart = new ShoppingCart();
            Campaign campaign = CampaignFactory.GenerateCampaign(category, 4, 20, discountType);

            shoppingCart.AddProduct(product, 5);
            campaign.ApplyDiscount(shoppingCart);

            Assert.Equal(expected, shoppingCart.DiscountedTotalAmount);
        }

        [Theory]
        [InlineData(DiscountType.Amount)]
        [InlineData(DiscountType.Rate)]
        public void ApplyDiscount_ShouldNotApplyDiscount_IfNotSameCategory(DiscountType discountType)
        {
            Category category = new Category(CategoryType.Electronic);
            Category otherCategory = new Category(CategoryType.Food);
            Product product = new Product("Television", 1000, category);
            ShoppingCart shoppingCart = new ShoppingCart();
            Campaign campaign = CampaignFactory.GenerateCampaign(otherCategory, 4, 20, discountType);

            shoppingCart.AddProduct(product, 5);
            bool result = campaign.ApplyDiscount(shoppingCart);

            Assert.False(result);
        }
    }
}
