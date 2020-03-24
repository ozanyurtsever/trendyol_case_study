using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Cart.Concrete;

namespace Trendyol.Shopping.Business.Discounts.Abstract
{
    public interface IDiscount
    {
        bool ApplyDiscount(ShoppingCart shoppingCart);
    }
}
