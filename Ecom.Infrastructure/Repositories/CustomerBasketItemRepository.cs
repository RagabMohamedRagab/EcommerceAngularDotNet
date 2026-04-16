using Ecom.core.Entities;
using Ecom.core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecom.Infrastructure.Repositories
{
    public class CustomerBasketItemRepository(IDatabase database) : ICustomerBasketItemRepository
    {

        public async Task<bool> DeleteCustomerBasket(string Id)
        {
            return await database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket> GetCustomerBasket(string Id)
        {
            var result = await database.StringGetAsync(Id);
            if (!result.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<CustomerBasket>(result.ToString());
                 
            }
            return null;
        }

        public async Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket basket)
        {
            var created = await database.StringSetAsync(basket.id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!created)
            {
                return null;
            }
            return await GetCustomerBasket(basket.id);
        }
    }
}
