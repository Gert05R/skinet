using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {   
        //static allows us to use a method inside a class without actually needing to create
        //a new isntance of the class before using the mehod  
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory) 
        {
            
            //get our data in here
            //because we run the method rom inside our prgram, no global exception handling is available
            try {
                if (!context.ProductBrands.Any())
                {
                    //lees de file in een variable
                    var brandsData = 
                    File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    //Deserialize de text in een lijst
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    //voeg alle elementen toe in de database via context
                    foreach (var item in brands) {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    //lees de file in een variable
                    var typesData = 
                    File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    //Deserialize de text in een lijst
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    //voeg alle elementen toe in de database via context
                    foreach (var item in types) {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    //lees de file in een variable
                    var productsData = 
                    File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    //Deserialize de text in een lijst
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    //voeg alle elementen toe in de database via context
                    foreach (var item in products) {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex) {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}