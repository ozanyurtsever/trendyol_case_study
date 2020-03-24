using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Campaigns.Concrete;
using Trendyol.Shopping.Business.Campaigns.Enums;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Cart.Enums;
using Trendyol.Shopping.Business.Coupons.Concrete;
using Trendyol.Shopping.Business.Delivery.Concrete;
using Xunit;

namespace Shopping.Tests
{
    public class ShoppingCartTests
    {
        [Theory]
        [InlineData(DiscountType.Amount, 4980)]
        [InlineData(DiscountType.Rate, 4000)]
        public void ApplyCampaign_ShouldNotApplySuccesively_IfSameCampaign(DiscountType discountType, decimal expected)
        {
            Category category = new Category(CategoryType.Food);
            Product product = new Product("Food", 1000, category);
            ShoppingCart shoppingCart = new ShoppingCart();
            Campaign campaign = CampaignFactory.GenerateCampaign(category, 4, 20, discountType);
            Campaign otherCampaign = CampaignFactory.GenerateCampaign(category, 4, 20, discountType);

            shoppingCart.AddProduct(product, 5);
            shoppingCart.ApplyCampaigns(campaign, otherCampaign);

            Assert.Equal(expected, shoppingCart.DiscountedTotalAmount);
        }

        [Theory]
        [InlineData(DiscountType.Amount, 4980)]
        [InlineData(DiscountType.Rate, 4000)]
        public void ApplyCampaign_ShouldDecreaseAmount_IfMinimumItemConditionSatisfied(DiscountType discountType, decimal expected)
        {
            Category category = new Category(CategoryType.Food);
            Product product = new Product("Apple", 1000, category);
            ShoppingCart shoppingCart = new ShoppingCart();
            Campaign campaign = CampaignFactory.GenerateCampaign(category, 4, 20, discountType);

            shoppingCart.AddProduct(product, 5);
            shoppingCart.ApplyCampaigns(campaign);

            Assert.Equal(expected, shoppingCart.DiscountedTotalAmount);
        }


        [Theory]
        [InlineData(DiscountType.Amount, 2000)]
        [InlineData(DiscountType.Rate, 2000)]
        public void ApplyCampaign_ShouldNotDecreaseAmount_IfMinimumItemIsNotSatisfied(DiscountType discountType, decimal expected)
        {
            Category category = new Category(CategoryType.Electronic);
            Product product = new Product("Television", 1000, category);
            ShoppingCart shoppingCart = new ShoppingCart();
            Campaign campaign = CampaignFactory.GenerateCampaign(category, 4, 20, discountType);

            shoppingCart.AddProduct(product, 2);
            shoppingCart.ApplyCampaigns();

            Assert.Equal(expected, shoppingCart.DiscountedTotalAmount);
        }

        [Theory]
        [InlineData(DiscountType.Amount, 5000)]
        [InlineData(DiscountType.Rate, 5000)]
        public void ApplyCampaign_ShouldNotDecreaseAmount_IfNoCampaignIsGiven(DiscountType discountType, decimal expected)
        {
            Category category = new Category(CategoryType.Electronic);
            Product product = new Product("Television", 1000, category);
            ShoppingCart shoppingCart = new ShoppingCart();
            Campaign campaign = CampaignFactory.GenerateCampaign(category, 4, 20, discountType);

            shoppingCart.AddProduct(product, 5);
            shoppingCart.ApplyCampaigns();

            Assert.Equal(expected, shoppingCart.DiscountedTotalAmount);
        }

        [Theory]
        [InlineData(DiscountType.Amount, 4980)]
        [InlineData(DiscountType.Rate, 4000)]
        public void ApplyCoupon_ShouldApplyDiscount_IfMinimumAmountSatisfied(DiscountType discountType, decimal expected)
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Category foodCategory = new Category(CategoryType.Food);
            Product television = new Product("Television", 1000, electronicCategory);
            Product apple = new Product("Apple", 5, foodCategory);
            ShoppingCart shoppingCart = new ShoppingCart();
            Coupon coupon = CouponFactory.GenerateCoupon(4000, 20, discountType);

            shoppingCart.AddProduct(television, 4);
            shoppingCart.AddProduct(apple, 200);
            shoppingCart.ApplyCoupon(coupon);

            Assert.Equal(expected, shoppingCart.DiscountedTotalAmount);
        }

        [Theory]
        [InlineData(DiscountType.Amount, 3995)]
        [InlineData(DiscountType.Rate, 3995)]
        public void ApplyCoupon_ShouldNotApplyDiscount_IfMinimumAmountNotSatisfied(DiscountType discountType, decimal expected)
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Category foodCategory = new Category(CategoryType.Food);
            Product television = new Product("Television", 1000, electronicCategory);
            Product apple = new Product("Apple", 5, foodCategory);
            ShoppingCart shoppingCart = new ShoppingCart();
            Coupon coupon = CouponFactory.GenerateCoupon(4000, 20, discountType);

            shoppingCart.AddProduct(television, 3);
            shoppingCart.AddProduct(apple, 199);
            shoppingCart.ApplyCoupon(coupon);

            Assert.Equal(expected, shoppingCart.DiscountedTotalAmount);
        }

        [Fact]
        public void AddProduct_ShoulAddProduct_IfValid()
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Product television = new Product("Television", 2000, electronicCategory);
            ShoppingCart shoppingCart = new ShoppingCart();

            shoppingCart.AddProduct(television, 100);
            int numberOfProducts = shoppingCart.GetNumberOfProducts();

            Assert.Equal(1, numberOfProducts);
        }

        [Fact]
        public void AddProduct_ShouldNotAdd_IfProductIsNull()
        {
            ShoppingCart shoppingCart = new ShoppingCart();

            shoppingCart.AddProduct(null, 100);
            int numberOfProducts = shoppingCart.GetNumberOfProducts();

            Assert.Equal(0, numberOfProducts);
        }

        [Fact]
        public void CalculateDeliveryCost_ShouldWorkCorrectly_IfStandardAlgorithmApplied()
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Product television = new Product("Television", 2000, electronicCategory);
            ShoppingCart shoppingCart = new ShoppingCart();
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(10, 5, 10, shoppingCart, new StandardCalculatorStrategy());

            shoppingCart.AddProduct(television, 10);
            int numberOfProducts = shoppingCart.GetNumberOfProducts();
            decimal deliveryCost = deliveryCostCalculator.CalculateCost();

            Assert.Equal(25M, deliveryCost);
        }

        [Fact]
        public void CalculateDeliveryCost_ShouldThrowArgumentNullException_IfParameterNull()
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Product television = new Product("Television", 2000, electronicCategory);
            ShoppingCart shoppingCart = new ShoppingCart();
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(10, 5, 10, null, new StandardCalculatorStrategy());

            shoppingCart.AddProduct(television, 10);
            int numberOfProducts = shoppingCart.GetNumberOfProducts();

            Assert.Throws<ArgumentNullException>(() => deliveryCostCalculator.CalculateCost());
        }


        [Fact]
        public void GetTotalAmount_ShouldReturnCorrectAmount_IfTotalAmountIsZero()
        {
            ShoppingCart shoppingCart = new ShoppingCart();


            var totalAmount = shoppingCart.GetTotalAmount();

            Assert.Equal(0, totalAmount);
        }

        [Fact]
        public void GetTotalAmount_ShouldReturnCorrectAmount_IfTotalAmountIsTenThousand()
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Category clothing =  new Category(CategoryType.Clothing);
            Category food =  new Category(CategoryType.Food);
            Product television = new Product("Television", 800, electronicCategory);
            Product telephone = new Product("Telephone", 500, electronicCategory);
            Product weddingDress = new Product("Wedding Dress", 400, clothing);
            Product hamburger = new Product("Hamburger", 10, food);
            ShoppingCart shoppingCart = new ShoppingCart();

            shoppingCart.AddProduct(television, 2); 
            shoppingCart.AddProduct(telephone, 5); 
            shoppingCart.AddProduct(weddingDress, 10); 
            shoppingCart.AddProduct(hamburger, 190); 
            var totalAmount = shoppingCart.GetTotalAmount();

            Assert.Equal(10000, totalAmount);
        }

        [Fact]
        public void GetNumberOfDeliveries_ShouldReturnZero_IfThereIsNoCategory()
        {
            ShoppingCart shoppingCart = new ShoppingCart();

            var numberOfDeliveries = shoppingCart.GetNumberOfDeliveries();

            Assert.Equal(0, numberOfDeliveries);
        }

        [Fact]
        public void GetNumberOfDeliveries_ShouldReturnCorrectValue_IfThereIsTwoCategory()
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Category clothing = new Category(CategoryType.Clothing);
            Product television = new Product("Television", 800, electronicCategory);
            Product telephone = new Product("Telephone", 500, electronicCategory);
            Product weddingDress = new Product("Wedding Dress", 400, clothing);
            ShoppingCart shoppingCart = new ShoppingCart();

            shoppingCart.AddProduct(television, 2);
            shoppingCart.AddProduct(telephone, 5);
            shoppingCart.AddProduct(weddingDress, 10);
            var numberOfDeliveries = shoppingCart.GetNumberOfDeliveries();

            Assert.Equal(2, numberOfDeliveries);
        }

        [Fact]
        public void GetProductQuantityOfCategory_ShouldReturnCorrect_IfThereIsSeventeenItemsInSameCategory()
        {
            Category electronicCategory = new Category(CategoryType.Electronic);
            Product television = new Product("Television", 800, electronicCategory);
            Product telephone = new Product("Telephone", 500, electronicCategory);
            Product smartBand = new Product("SmartBand", 300, electronicCategory);
            ShoppingCart shoppingCart = new ShoppingCart();

            shoppingCart.AddProduct(television, 2);
            shoppingCart.AddProduct(telephone, 5);
            shoppingCart.AddProduct(smartBand, 10);
            var productQuantityOfCategory = shoppingCart.GetProductQuantityOfCategory(electronicCategory);

            Assert.Equal(17u, productQuantityOfCategory);
        }
    }
}
