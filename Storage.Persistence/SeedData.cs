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
            List<string> shelfSections = ["A", "B", "C", "D", "E", "F", "G", "H"];
            List<Product> products = new();

            for (int i = 0; i < productCount; i++) {
                decimal randDecimal = faker.Random.Decimal((decimal)0.0, (decimal)0.9);
                decimal price = rand.Next(10, 999) + randDecimal;
                decimal purchasePrice = price - (price * faker.Random.Decimal((decimal)0.1, (decimal)0.75));

                string shelfSection = shelfSections.ElementAt(rand.Next(0, shelfSections.Count - 1));
                string shelf = String.Format("{0}{1}{2}-{3}{4}", shelfSection, rand.Next(0,9), rand.Next(1,9), rand.Next(0, 9), rand.Next(1, 9));

                var category = categories.ElementAt(rand.Next(0, categories.Count() -1));
                string name = String.Format($"{faker.Commerce.ProductAdjective()} {faker.Commerce.ProductName()}");

                products.Add(new()
                {
                    Name = name,
                    Price = price,
                    OrderDate = faker.Date.Recent(30),
                    PurchasePrice = purchasePrice,
                    InventoryCount = rand.Next(5, 500),
                    Shelf = shelf,
                    Description = rand.Next(0, 5) > 0 ? faker.Commerce.ProductDescription() : null,
                    Category = category
                });
            }

            return products;
        }
    }
}
