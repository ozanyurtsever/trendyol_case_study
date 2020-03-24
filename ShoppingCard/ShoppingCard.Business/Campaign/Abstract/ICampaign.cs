using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Business.Campaign.Abstract
{
    public interface ICampaign
    {
        void ApplyDiscount(ShoppingCard card);
    }
}
