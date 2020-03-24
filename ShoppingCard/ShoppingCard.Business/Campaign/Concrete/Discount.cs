using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Business.Campaign.Concrete
{
    public abstract class Discount
    {
        public Category Category { get; set; }
        public int MinimumItems { get; set; }

        public Discount(Category category, int minItems)
        {
            Category = category;
            MinimumItems = minItems;
        }
    }
}
