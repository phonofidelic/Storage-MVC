using Bogus;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Entities;
using Storage.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Persistence
{
    public class SeedData
    {
        private static Faker faker;

        public static async Task InitAsync(StorageContext context)
        {
            if (await context.Products.AnyAsync()) return;

            faker = new();

            IEnumerable<Category> categories = GenerateCategories(5);
            await context.AddRangeAsync(categories);

            IEnumerable<Product> products = GenerateProducts(50, categories);
            await context.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Category> GenerateCategories(int categoryCount)
        {
            List<Category> categories = new();
            for (int i = 0; i < categoryCount; i++)
            {
                categories.Add(new()
                {
                    Name = faker.Commerce.Department(),
                    Description = faker.Commerce.ProductDescription()
                });
            }

            return categories;
        }

        private static IEnumerable<Product> GenerateProducts(int productCount, IEnumerable<Category> categories)
        {
            Random rand = new();
            List<Product> products = new();

            for (int i = 0; i < productCount; i++) {
                decimal randDecimal = faker.Random.Decimal((decimal)0.0, (decimal)0.95);
                decimal price = rand.Next(10, 999) + randDecimal;
                var category = categories.ElementAt(rand.Next(0, categories.Count()));

                products.Add(new()
                {
                    //Name = String.Format("{Adjective} {Product}", faker.Commerce.ProductAdjective(), faker.Commerce.ProductName()),
                    Name = faker.Commerce.ProductName(),
                    Price = price,
                    OrderDate = faker.Date.Recent(30),
                    PurchasePrice = price - (price * faker.Random.Decimal(new(0.1), new(0.75))),
                    InventoryCount = rand.Next(5, 500),
                    Shelf = String.Format("{Section}-{Number}", faker.Random.Char('A','D'), faker.Random.Int(1, 25)),
                    Description = rand.Next(0, 5) > 0 ? faker.Commerce.ProductDescription() : null,
                    //CategoryId = category.Id,
                    Category = category
                });
            }

            return products;
        }
    }
}
