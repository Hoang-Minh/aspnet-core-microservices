using CatalogApi.Controllers.Entities;
using MongoDB.Driver;

namespace CatalogApi.Data.Interfaces
{
    // Data Access Layer
    public interface ICatalogContext
    {       
        IMongoCollection<Product> Products { get; }
    }
}
