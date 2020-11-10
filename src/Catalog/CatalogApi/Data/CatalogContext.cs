﻿using CatalogApi.Controllers.Entities;
using CatalogApi.Data.Interfaces;
using CatalogApi.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(ICatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Products = database.GetCollection<Product>(settings.CollectionName);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
