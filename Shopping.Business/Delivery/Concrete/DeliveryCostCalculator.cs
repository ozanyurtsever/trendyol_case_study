using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Cart.Concrete;
using Trendyol.Shopping.Business.Delivery.Abstract;

namespace Trendyol.Shopping.Business.Delivery.Concrete
{
    public class DeliveryCostCalculator
    {
        public decimal CostPerDelivery { get; set; }
        public decimal CostPerProduct { get; set; }
        public decimal FixedCost { get; set; }
        private ShoppingCart Cart { get; set; }
        private ICostCalculator CalculatorStrategy { get; set; }
        public DeliveryCostCalculator(decimal costPerDelivery, decimal costPerProduct, decimal fixedCost, ShoppingCart cart, ICostCalculator calculatorStrategy) 
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            CalculatorStrategy = calculatorStrategy;
            Cart = cart;
            FixedCost = fixedCost;
        }
        public decimal CalculateCost()
        {
            if(Cart != null)
            {
                return CalculatorStrategy.CalculateCost(CostPerDelivery, CostPerProduct, Cart, FixedCost);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
