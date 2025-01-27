using MyApi.Models;
using System.Linq;

namespace MyApi.Data {
    public static class DataSeeder {
        public static void Seed(AppDbContext context) {
            if (!context.Products.Any()) {
                context.Products.AddRange(
                    new Product { Name = "Laptop", Price = 1500.99m, Quantity = 5 },
                    new Product { Name = "Smartphone", Price = 799.49m, Quantity = 10 }
                );
                context.SaveChanges();
            }
        }
    }
}
