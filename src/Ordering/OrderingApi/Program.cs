using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace OrderingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateAndSeedDatabase(host);
            MigrationDatabase<OrderContext>(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateAndSeedDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var orderConext = services.GetRequiredService<OrderContext>();
                var task = OrderContextSeed.SeedAsync(orderConext, loggerFactory);
                Task.WaitAll(task);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message);
            }
        }

        public static void MigrationDatabase<T>(IHost host, int? retry = 0) where T : DbContext
        {
            var retryForAvailability = retry.Value;
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var db = services.GetRequiredService<T>();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Error {ex.Message}, retry {retryForAvailability}");

                if (retryForAvailability < 5)
                {
                    retryForAvailability++;
                    Task.Delay(2000).Wait();
                    MigrationDatabase<T>(host, retryForAvailability);
                }
                throw;
            }            
        }
    }
}
