using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Cart.Abstract;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Delivery.Abstract;

namespace Trendyol.Shopping.Business.Delivery.Concrete
{
    public class StandardCalculatorStrategy : ICostCalculator
    {
        public decimal CalculateCost(decimal costPerDelivery, decimal costPerProduct, IShoppingCart shoppingCart, decimal fixedCost = 2.99M)
        {
            return (costPerDelivery * shoppingCart.GetNumberOfDeliveries()) +
                       (costPerProduct * shoppingCart.GetNumberOfProducts()) +
                           fixedCost;
        }
    }
}
