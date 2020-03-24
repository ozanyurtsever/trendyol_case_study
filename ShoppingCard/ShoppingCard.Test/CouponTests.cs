using Shopping.Business;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Shopping.Test
{
    public class CouponTests
    {
        [Fact]
        public void ApplyDiscount_ShouldDescreasePrice_IfValid()
        {
            Category category = new Category("Electronic");
            Product product = new Product("Television", 1000, category);
            ShoppingCard shoppingCard = new ShoppingCard();

        }
    }
}
