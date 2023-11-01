using System.Text.Json;
using CasperDelivery.Data.Models;

namespace CasperDelivery.Data;

public class SeedContext
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Restaurants.Any())
        {
            var restaurantsData = File.ReadAllText("Data/SeedData/restaurants.json");
            var restaurants = JsonSerializer.Deserialize<List<Restaurants>>(restaurantsData);
            context.Restaurants.AddRange(restaurants);
        }
        
        if (context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
        
        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText("Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Products>>(productsData);
            context.Products.AddRange(products);
        }
        
        if (context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
    }
}