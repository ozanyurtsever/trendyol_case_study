using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Trendyol.Shopping.Business.Campaigns.Concrete;
using Trendyol.Shopping.Business.Cart.Abstract;
using Trendyol.Shopping.Business.Coupons.Concrete;
using Trendyol.Shopping.Business.Discounts.Abstract;

namespace Trendyol.Shopping.Business.Cart.Concrete
{
    public class ShoppingCart : IShoppingCart
    {
        public decimal TotalAmount { get; set; } = 0;
        public decimal DiscountedTotalAmount { get; set; } = 0;
        public decimal AppliedCampaignDiscount { get; set; }
        public decimal AppliedCouponDiscount { get; set; }
        public IDictionary<Product, uint> Products { get; set; }

        public ShoppingCart()
        {
            Products = new Dictionary<Product, uint>();
        }
        public void ApplyCampaigns(params Campaign[] campaigns)
        {
            Contract.Requires(campaigns != null && campaigns.Any());
            for(int i = 0; i < campaigns.Length; ++i)
            {
                bool isCampaignPreviouslyApplied = false;
                for(int j = 0; j < i; ++j)
                {
                    if(campaigns[i] == campaigns[j])
                    {
                        isCampaignPreviouslyApplied = true;
                        break;
                    }
                }
                if (!isCampaignPreviouslyApplied)
                {
                    campaigns[i].ApplyDiscount(this);
                }
            }
        }

        public void ApplyCoupon(Coupon coupon)
        {
            coupon.ApplyDiscount(this);
        }

        public void ApplyDiscount(IDiscount discount)
        {
            discount.ApplyDiscount(this);
        }

        public void AddProduct(Product product, uint quantity)
        {
            if (product != null &&  !Products.ContainsKey(product))
            {
                Products[product] = quantity;
                UpdateTotalAmount(product.Price, quantity);
            }
        }

        public decimal GetCouponDiscount()
        {
            return AppliedCouponDiscount;
        }

        public decimal GetCampaignDiscount()
        {
            return AppliedCampaignDiscount;
        }

        public decimal GetTotalAmount()
        {
            return TotalAmount;
        }

        public decimal GetTotalAmountAfterDiscounts()
        {
            return DiscountedTotalAmount;
        }

        public uint GetProductQuantityOfCategory(Category category)
        {
            uint quantity = 0;
            foreach (var product in Products.Keys)
            {
                if(product.Category == category)
                {
                    quantity += Products[product];
                }
            }

            return quantity;
        }

        public int GetNumberOfProducts()
        {
            return Products.Count;
        }

        public int GetNumberOfDeliveries()
        {
            HashSet<Category> categoriesInCart = new HashSet<Category>();
            foreach (var product in Products.Keys)
            {
                categoriesInCart.Add(product.Category);
            }

            return categoriesInCart.Count;
        }

        public void Print()
        {
            var table = new ConsoleTable("CategoryName", "ProductName", "Quantity", "Unit Price", "Total Price", "Total Discount");

            foreach (var product in Products.Keys)
            {
                var quantity = Products[product];
                var totalDiscount = AppliedCampaignDiscount + AppliedCouponDiscount;
                var totalPrice = product.Price * quantity;
                var categoryName = product.Category.Title.ToString();
                table.AddRow(categoryName, product.Title, quantity, product.Price, totalPrice, totalDiscount);
            }

            table.Write(Format.Alternative);
        }

        private void UpdateTotalAmount(decimal productPrice, uint quantity)
        {
            TotalAmount += productPrice * quantity;
            DiscountedTotalAmount+= productPrice * quantity;
        }
    }
}
