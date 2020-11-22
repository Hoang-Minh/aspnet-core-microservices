using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            var retryForAvailability = retry.Value;

            try
            {
                orderContext.Database.Migrate();

                if(!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrder());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if(retryForAvailability < 3)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(orderContext, loggerFactory, retryForAvailability);
                }                
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrder()
        {
            return new List<Order>
            {
                new Order() {Username = "test", FirstName = "test", LastName = "test", EmailAddress = "test@test.com", AddressLine="blablabla", Country = "USA"}
            };
        }
    }
}
