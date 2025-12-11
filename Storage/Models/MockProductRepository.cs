using Storage.Models.ViewModels;
using Bogus;

namespace Storage.Models
{
    public class MockProductRepository : IProductRepository
    {
        private IEnumerable<Product> _mockProducts;
        public MockProductRepository()
        {   
            Randomizer.Seed = new Random(55432);
            int productId = 0;
            
            var mockProducts = new Faker<Product>()
            .StrictMode(true)
            .RuleFor(p => p.Id, f => productId++)
            .RuleFor(p => p.Name, f => f.Commerce.Product())
            .RuleFor(p => p.Price, f => f.Random.Number(10, 500))
            .RuleFor(p => p.OrderDate, f => f.Date.Recent())
            .RuleFor(p => p.Shelf, f => $"{f.Random.String(1, 'A', 'Z')}-{f.Random.Int(1, 99)}")
            .RuleFor(p => p.Category, f => f.PickRandom(f.Commerce.Categories(5)))
            .RuleFor(p => p.Count, f => f.Random.Number(0, 1000))
            .RuleFor(p => p.Description, f => f.Commerce.ProductName());

            _mockProducts = mockProducts.Generate(50).ToHashSet().DistinctBy(p => p.Name);
        }
        public IEnumerable<Product> AllProducts => _mockProducts;

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
