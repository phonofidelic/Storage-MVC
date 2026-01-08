using Storage.Models.ViewModels;
using Bogus;
using Microsoft.IdentityModel.Tokens;
using Storage.Core.Entities;

namespace Storage.Models
{
    public class MockProductRepository : IProductRepository
    {
        private ILogger<MockCategoryRepository> _logger;
        private MockDb _mockDb;
        private Randomizer _random = new();
        public MockProductRepository(MockDb db, ILogger<MockCategoryRepository> logger)
        {   
            _logger = logger;
            _mockDb = db;
            _mockDb.Products = DbInitializer
                .GenerateProductsWithIds()
                .Select(p =>
                {
                    p.Name = "[MOCK] " + p.Name;
                    return p;
                });
        }
        public IEnumerable<Product> AllProducts => _mockDb.Products.ToList();

        public void Create(ProductCreateDto product)
        {
            int lastId = AllProducts.Max(p => p.Id);
            int newProductId = _random.Int(lastId);

            var newList = _mockDb.Products.ToList();
            newList.Add(new()
            {
                Id = newProductId,
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                InventoryCount = product.Count,
                Description = product.Description ?? ""
            });

            _mockDb.Products = newList;
        }

        public Task UpdateAsync(ProductEditDto product)
        {
            throw new NotImplementedException();
        }

        public Product? GetProductById(int? productId)
        {
            return AllProducts.FirstOrDefault(p => p.Id == productId);
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetProductByIdAsync(int? productId)
        {
            return _mockDb.Products.First(p => p.Id == productId);
        }

        public Task CreateAsync(ProductCreateDto product)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetMaxPrice()
        {
            return _mockDb.Products.Max(p => p.Price);
        }

        public async Task<int> GetMinPrice()
        {
            return _mockDb.Products.Min(p => p.Price);
        }

        public async Task<IEnumerable<Product>> FilterProductsAsync(
            int? minPrice, 
            int? maxPrice, 
            IEnumerable<int>? categoryIds, 
            DateTime? minOrderDate, 
            DateTime? maxOrderDate)
        {
            var products = _mockDb.Products;

            if (minPrice > 0)
                products = products.Where(p => p.Price >= minPrice);

            if (maxPrice > 0)
                products = products.Where(p => p.Price <= maxPrice);

            if (categoryIds?.Count() > 0)
                products = products.Where(p => categoryIds.Contains(p.Id));

            if (minOrderDate != null)
                products = products.Where(p => p.OrderDate >= minOrderDate);

            if (maxOrderDate != null)
                products = products.Where(p => p.OrderDate <= maxOrderDate);

            return products;
        }
    }
}
