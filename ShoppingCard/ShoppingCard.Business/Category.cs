using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Business
{
    public class Category
    {
        public string Title { get; set; }
        public Category(string title)
        {
            Title = title;
        }
    }
}
