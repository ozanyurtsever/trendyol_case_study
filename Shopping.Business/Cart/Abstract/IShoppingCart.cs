using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Campaigns.Concrete;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Coupons.Concrete;
using Trendyol.Shopping.Business.Discounts.Abstract;

namespace Trendyol.Shopping.Business.Cart.Abstract
{
    public interface IShoppingCart
    {
        void ApplyCampaigns(params Campaign[] campaigns);
        void ApplyCoupon(Coupon coupon);
        void ApplyDiscount(IDiscount discount);
        void AddProduct(Product product, uint quantity);
        decimal GetCouponDiscount();
        decimal GetCampaignDiscount();
        decimal GetTotalAmount();
        decimal GetTotalAmountAfterDiscounts();
        uint GetProductQuantityOfCategory(Category category);
        int GetNumberOfProducts();
        int GetNumberOfDeliveries();
        void Print();
    }
}
