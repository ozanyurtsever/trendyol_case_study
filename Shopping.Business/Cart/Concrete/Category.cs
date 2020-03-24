using System;
using System.Collections.Generic;
using System.Text;
using Trendyol.Shopping.Business.Cart.Enums;

namespace Trendyol.Shopping.Business.Cart.Concrete
{
    public class Category
    {
        public CategoryType Title { get; set; }
        public Category(CategoryType title)
        {
            Title = title;
        }
    }
}
