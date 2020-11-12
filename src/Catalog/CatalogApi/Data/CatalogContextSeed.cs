using CatalogApi.Controllers.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CatalogApi.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection) 
        {
            var existProduct = productCollection.Find(p => true).Any();

            if(existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfigureProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfigureProducts() 
        {
            return new List<Product>()
            {
                new Product(){Name= "iPhone X", Summary= "Best iphone ever", Description= "iPhone X", ImageFile= "product-1.jpg", Price= 500, Category= "iPhone"},
                new Product(){Name= "iPhone 8", Summary= "Smart iphone", Description= "iPhone 8", ImageFile= "product-2.jpg", Price= 400, Category= "iPhone"},
                new Product(){Name= "iPhone 7", Summary= "No one can beat it", Description= "iPhone 7", ImageFile= "product-3.jpg", Price= 300, Category= "iPhone"},
                new Product(){Name= "iPhone 6", Summary= "What can you say about it", Description= "iPhone 6", ImageFile= "product-4.jpg", Price= 200, Category= "iPhone"},
            };
        }
    }
}
