using StackExchange.Redis;

namespace BasketApi.Data.Interfaces
{
    // Data Access layer
    public interface IBasketContext
    {
        IDatabase Redis { get; }
    }
}
