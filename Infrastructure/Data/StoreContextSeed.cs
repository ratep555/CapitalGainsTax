using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Categories.Any())
                {
                    var categoriesData = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Countries.Any())
                {
                    var countriesData = File.ReadAllText("../Infrastructure/Data/SeedData/countries.json");
                    var countries = JsonSerializer.Deserialize<List<Country>>(countriesData);

                    foreach (var item in countries)
                    {
                        context.Countries.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Stocks.Any())
                {
                    var stocksData = File.ReadAllText("../Infrastructure/Data/SeedData/stocks.json");
                    var stocks = JsonSerializer.Deserialize<List<Stock>>(stocksData);

                    foreach (var item in stocks)
                    {
                        context.Stocks.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
