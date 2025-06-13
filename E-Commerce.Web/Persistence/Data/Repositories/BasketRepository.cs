using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories
{
    public class BasketRepository(IConnectionMultiplexer Connection) : IBasketRepository
    {
        private readonly IDatabase _database=Connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket Basket, TimeSpan? TimeLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);
            var IsCreatedOrUpdated =await _database.StringSetAsync(Basket.Id, JsonBasket, TimeLive?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(Basket.Id);
            else return null;
        }

        public async Task<bool> DeleteBasketAsync(string id) =>await _database.KeyDeleteAsync(id);
        

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
           var Basket=await _database.StringGetAsync(key);
            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
