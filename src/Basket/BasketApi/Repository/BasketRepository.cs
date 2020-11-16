using BasketApi.Controllers.Entities;
using BasketApi.Data.Interfaces;
using BasketApi.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketApi.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> DeleteBasket(string username)
        {
            return await _context.Redis.KeyDeleteAsync(username);
        }

        public async Task<BasketCart> GetBasket(string username)
        {
            var basket = await _context.Redis.StringGetAsync(username);

            if (basket.IsNullOrEmpty) return null;

            return JsonConvert.DeserializeObject<BasketCart>(basket);
        }

        public async Task<BasketCart> UpdateBasket(BasketCart basket)
        {
            var updated = await _context.Redis.StringSetAsync(basket.Username, JsonConvert.SerializeObject(basket));

            if (!updated) return null;

            return await GetBasket(basket.Username);
        }
    }
}
