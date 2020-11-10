using CatalogApi.Controllers.Entities;
using MongoDB.Driver;

namespace CatalogApi.Data.Interfaces
{
    public interface ICatalogContext
    {       
        IMongoCollection<Product> Products { get; }
    }
}
