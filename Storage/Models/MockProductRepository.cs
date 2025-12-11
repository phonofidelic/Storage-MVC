using Storage.Models.ViewModels;
using Bogus;

namespace Storage.Models
{
    public class MockProductRepository : IProductRepository
    {
        private IEnumerable<Product> _mockProducts;
        private IEnumerable<Category> _mockCategories;
        public MockProductRepository(ILogger<MockProductRepository> logger)
        {   
            Randomizer.Seed = new Random(52332);

            int categoryId = 0;

            var mockCategories = new Faker<Category>()
                .StrictMode(true)
                .RuleFor(c => c.Id, f => categoryId++)
                .RuleFor(c => c.Name, f => f.PickRandom(f.Commerce.Categories(5)))
                .RuleFor(c => c.Description, f => f.Commerce.ProductName())
                .RuleFor(c => c.Products, f => []);

            _mockCategories = mockCategories.Generate(5);

            int productId = 0;
            
            var mockProducts = new Faker<Product>()
            .StrictMode(true)
            .RuleFor(p => p.Id, f => productId++)
            .RuleFor(p => p.Name, f => $"{f.Commerce.Product()} {f.Random.WordsArray(51)[productId]}")
            .RuleFor(p => p.Price, f => f.Random.Number(10, 500))
            .RuleFor(p => p.OrderDate, f => f.Date.Recent())
            .RuleFor(p => p.Shelf, f => $"{f.Random.String(1, 'A', 'Z')}-{f.Random.Int(1, 99)}")
            .RuleFor(p => p.CategoryId, f => f.Random.Number(1, 5))
            .RuleFor(p => p.Category, f => f.PickRandom(_mockCategories))
            .RuleFor(p => p.Count, f => f.Random.Number(0, 1000))
            .RuleFor(p => p.Description, f => f.Commerce.ProductName());

            var tempMockProducts = mockProducts.Generate(50).ToHashSet().DistinctBy(p => p.Name).ToList();
            int createdProductsCount = tempMockProducts.Count;

            if (logger.IsEnabled(logLevel: LogLevel.Information)) 
                logger.LogInformation("Generated {Count} unique products.", createdProductsCount);

            _mockProducts = tempMockProducts;
        }
        public IEnumerable<Product> AllProducts => _mockProducts;

        public IEnumerable<Product> FilterProducts(string? categoryName)
        {
            if (categoryName == null) 
                return AllProducts;

            return AllProducts.Where(product => product.Category.Name.Contains(categoryName, StringComparison.OrdinalIgnoreCase));
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
