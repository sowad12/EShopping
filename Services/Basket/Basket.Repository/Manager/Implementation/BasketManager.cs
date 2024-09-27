using Basket.Library.Model.Entities;
using Basket.Repository.Manager.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;


namespace Basket.Repository.Manager.Implementation
{
    public class BasketManager : IBasketManager
    {
        private readonly IDistributedCache _redisCache;
        public BasketManager(IDistributedCache redisCache)
        {
            _redisCache=redisCache;
        }
        public async Task DeleteBasket(string userName)
        {
           await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket=await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
           
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await _redisCache.SetStringAsync(shoppingCart.Name, JsonConvert.SerializeObject(shoppingCart));
            return await GetBasket(shoppingCart.Name);
        }
    }
}
