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
                new Product(){Name= "iPhone X", Summary= "", Description= "", ImageFile= "", Price= 100, Category= ""},
                new Product(){Name= "iPhone X", Summary= "", Description= "", ImageFile= "", Price= 100, Category= ""},
                new Product(){Name= "iPhone X", Summary= "", Description= "", ImageFile= "", Price= 100, Category= ""},
                new Product(){Name= "iPhone X", Summary= "", Description= "", ImageFile= "", Price= 100, Category= ""},
            };
        }
    }
}
