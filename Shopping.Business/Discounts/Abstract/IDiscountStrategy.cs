using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Cart.Concrete;

namespace Trendyol.Shopping.Business.Discounts.Abstract
{
    public interface IDiscountStrategy
    {
        void ApplyDiscount(ShoppingCart shoppingCart, decimal discountQuantity);
    }
}
