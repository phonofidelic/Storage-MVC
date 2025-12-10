using Storage.Models.ViewModels;

namespace Storage.Models
{
    public class MockProductRepository : IProductRepository
    {
        public IEnumerable<Product> AllProducts =>
            new List<Product>
            {
                new() {
                    Id= 1,
                    Name = "Test 1",
                    Price = 100,
                    OrderDate = DateTime.Now,
                    Category = "Category 1",
                    Shelf = "Shelf A",
                    Count = 10,
                    Description = "Description for Test 1"
                },
                new() {
                    Id= 2,
                    Name = "Test 2",
                    Price = 200,
                    OrderDate = DateTime.Now,
                    Category = "Category 2",
                    Shelf = "Shelf B",
                    Count = 20,
                    Description = "Description for Test 2"
                },
                new() {
                    Id= 3,
                    Name = "Test 3",
                    Price = 300,
                    OrderDate = DateTime.Now,
                    Category = "Category 3",
                    Shelf = "Shelf C",
                    Count = 30,
                    Description = "Description for Test 3"
                }
            };

        public IEnumerable<Product> FilterProducts(string? category)
        {
            if (category == null) 
                return AllProducts;

            return AllProducts.Where(product => product.Category.Contains(category,StringComparison.OrdinalIgnoreCase));
        }

        public Product? GetProductById(int? productId)
        {
            return AllProducts.FirstOrDefault(p => p.Id == productId);
        }
        public IEnumerable<ProductViewModel> GetSummary()
        {
            return AllProducts.Select(product => new ProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                InventoryValue = product.Price * product.Count
            });
        }
    }
}
