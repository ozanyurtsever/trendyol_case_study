using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Campaigns.Concrete;
using Trendyol.Shopping.Business.Campaigns.Enums;
using Trendyol.Shopping.Business.Cart.Abstract;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Cart.Enums;
using Trendyol.Shopping.Business.Coupons.Concrete;
using Trendyol.Shopping.Business.Delivery.Abstract;
using Trendyol.Shopping.Business.Discounts.Abstract;

namespace Shopping.UI
{
    public class App
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly ICostCalculator _costCalculator;
        public App(IShoppingCart shoppingCart, ICostCalculator costCalculator)
        {
            _shoppingCart = shoppingCart;
            _costCalculator = costCalculator;
        }

        public void Run()
        {
            Category electronic = new Category(CategoryType.Electronic);
            Category book = new Category(CategoryType.Books);

            Product television = new Product("Television", 1000, electronic);
            Product essentialCSharp = new Product("EssentialCSharp", 400, book);

            _shoppingCart.AddProduct(television, 5);
            _shoppingCart.AddProduct(essentialCSharp, 1);


            Coupon coupon = CouponFactory.GenerateCoupon(1000, 20, DiscountType.Rate);
            Campaign campaign = CampaignFactory.GenerateCampaign(electronic, 2, 10, DiscountType.Amount);


            _shoppingCart.ApplyCampaigns(campaign);
            _shoppingCart.Print();

            var deliveryCost = _costCalculator.CalculateCost(50, 2, _shoppingCart);
            var totalCartAmount = _shoppingCart.GetTotalAmount();
            WriteClosingText(deliveryCost, totalCartAmount);
        }

        private static void WriteClosingText(decimal deliveryCost, decimal totalCartAmount)
        {
            Console.WriteLine($"Total Cart Amount: {totalCartAmount}\n" +
                                          $"Delivery Cost:     {deliveryCost}");
            Console.ReadLine();
        }
    }
}
