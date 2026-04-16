using Ecom.core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.core.Interfaces
{
    public interface ICustomerBasketItemRepository
    {
        Task<CustomerBasket> GetCustomerBasket(string Id);
        Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket basket);

        Task<bool> DeleteCustomerBasket(string Id);
    }
}
