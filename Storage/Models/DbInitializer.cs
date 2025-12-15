using Storage.Data;
using Bogus;

namespace Storage.Models
{
    public static class DbInitializer
    {
        public static IEnumerable<Category> GenerateCategories()
        {
            return Categories.Select(c => c.Value).ToArray();
        }

        public static IEnumerable<Category> GenerateCategoriesWithIds()
        {
            int id = 0;
            return Categories.Select(c => {
                var category = c.Value;
                category.Id = id++;
                return category;
            });
        }

        public static IEnumerable<Product> GenerateProductsWithIds()
        {
            int id = 0;
            return GenerateProducts().Select(p =>
            {
                p.Id = id++;
                return p;
            });
        }

        private static Dictionary<string, Category>? categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    // int categoryId = 0;
                    var genresArray = new Category[]
                    {
                        new() {
                            // Id = categoryId++,
                            Name = "Coffee Beans",
                            Description = "Discover our curated selection of premium coffee beans, crafted from high-quality, sustainably sourced origins. From bright and floral Nordic profiles to rich espresso blends, each roast is designed to deliver exceptional flavor in every cup. Perfect for offices, homes, and coffee lovers everywhere."
                        },
                        new()
                        {
                            // Id = categoryId++,
                            Name = "Coffee Machines",
                            Description = "Explore our range of modern, reliable coffee machines designed for workplaces of all sizes. From compact brewers to high-capacity systems, each machine delivers exceptional quality, intuitive controls, and a seamless coffee experience—cup after cup."
                        },
                        new()
                        {
                            // Id = categoryId++,
                            Name = "Events",
                            Description = "Experience coffee like never before with our interactive Coffee Party events. From corporate tastings to public pop-ups, we bring premium brews, expert guidance, and a fun, social atmosphere that connects people through great coffee."
                        }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (var category in genresArray) { 
                        categories.Add(category.Name, category);
                    }
                }
                return categories;
            }
        }

        public static IEnumerable<Product> GenerateProducts()
        {
            Randomizer.Seed = new Random(52332);
            var faker = new Faker();
            // int productId = 0;

            return [new Product
                    {
                        // Id = productId++,
                        Name = "AromaBrew Pro 800",
                        Price = 12175,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Machines"],
                        CategoryId = Categories["Coffee Machines"].Id,
                        Shelf = $"A-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Premium bean-to-cup machine for larger offices needing café-quality coffee."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "OfficeFlow Mini",
                        Price = 4500,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Machines"],
                        CategoryId = Categories["Coffee Machines"].Id,
                        Shelf = $"A-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Compact, reliable, and fast — perfect for small teams."
                    },
                    new Product
                    {
                        // Id = productId++,
                        Name = "BrewStation XL",
                        Price = 6990,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Machines"],
                        CategoryId = Categories["Coffee Machines"].Id,
                        Shelf = $"A-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "High-capacity filter coffee system for large workplaces and events."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "BaristaTouch One",
                        Price = 17999,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Machines"],
                        CategoryId = Categories["Coffee Machines"].Id,
                        Shelf = $"A-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "A smart touchscreen espresso machine with customizable recipes."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Nordic Roast - Medium",
                        Price = 249,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Beans"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"B-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Balanced and smooth with notes of caramel and red berries."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Midnight Espresso",
                        Price = 279,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Beans"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"B-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Dark, intense espresso with chocolate undertones."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Fika Blend - Light Roast",
                        Price = 259,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Beans"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"B-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "A bright and floral Scandinavian-style light roast."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Mountain Peak Organic",
                        Price = 299,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Beans"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"B-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Organic medium-dark roast with nutty, earthy tones."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "French Roast Bold",
                        Price = 269,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Beans"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"B-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Smoky dark roast for those who love strong, robust flavor."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Decaf Harmony",
                        Price = 259,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Beans"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"B-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Full flavor, zero caffeine."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Ethiopian Sunrise",
                        Price = 289,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Beans"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"B-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Fruity and aromatic with blueberry and floral notes."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Brazilian Classic",
                        Price = 239,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Coffee Beans"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"B-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "Smooth, chocolatey, and reliable — a perfect everyday coffee."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Office Coffee Party Experience",
                        Price = 11999,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Events"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"C-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "A premium coffee-tasting event tailored for companies."
                    },
                    new()
                    {
                        // Id = productId++,
                        Name = "Coffee Party Pop-Up",
                        Price = 399,
                        OrderDate = faker.Date.Recent(),
                        // Category = Categories["Events"],
                        CategoryId = Categories["Coffee Beans"].Id,
                        Shelf = $"C-{faker.Random.Number(10, 99)}",
                        Count = faker.Random.Number(10, 99),
                        Description = "A public tasting event where visitors can sample new beans and buy Coffee Party products."
                    }];
        }
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            StorageContext context = applicationBuilder.ApplicationServices.CreateScope
                ().ServiceProvider.GetRequiredService<StorageContext>();

            if (!context.Category.Any())
            {
                context.Category.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Product.Any())
            {
                context.AddRange(GenerateProducts());
                context.SaveChanges();
            }
        }

        
    }

}
