using System;
using System.Collections.Generic;
using System.Text;

namespace Trendyol.Shopping.Business.Cart.Concrete
{
    public class Product
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public Product(string title, decimal price, Category category)
        {
            Title = title;
            Price = price;
            Category = category;
        }
    }
}
