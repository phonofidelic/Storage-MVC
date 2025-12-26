using Storage.Models.ViewModels;
using Bogus;
using Microsoft.IdentityModel.Tokens;
using Storage.Models.Entities;

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
                Count = product.Count,
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


        public Task<IEnumerable<Product>> FilterProductsAsync(IEnumerable<int>? categoryIds)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductByIdAsync(int? productId)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ProductCreateDto product)
        {
            throw new NotImplementedException();
        }
    }
}
