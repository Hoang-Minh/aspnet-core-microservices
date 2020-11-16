using BasketApi.Controllers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketApi.Repository.Interfaces
{
    // Business layer
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasket(string username);
        Task<BasketCart> UpdateBasket(BasketCart basket);
        Task<bool> DeleteBasket(string username);
    }
}
