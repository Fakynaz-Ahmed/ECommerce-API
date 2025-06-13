using DomainLayer.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {
      public Task<CustomerBasket?> GetBasketAsync(string key);
      public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket Basket , TimeSpan? TimeLive = null);
      public Task<bool> DeleteBasketAsync(string id);

    }
}
