using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Cart.Abstract;
using Trendyol.Shopping.Business.Cart.Concrete;

namespace Trendyol.Shopping.Business.Delivery.Abstract
{
    public interface ICostCalculator
    {
        decimal CalculateCost(decimal costPerDelivery, decimal costPerProduct, IShoppingCart shoppingCart, decimal fixedCost = 2.99M);
    }
}
