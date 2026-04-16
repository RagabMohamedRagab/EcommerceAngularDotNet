using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
            
        }

        public CustomerBasket(string id)
        {
           this.id = id; 
        }
        public string id { get; set; }

        public List<BasketItem> basketItems { get; set; } = [];
    }
}
