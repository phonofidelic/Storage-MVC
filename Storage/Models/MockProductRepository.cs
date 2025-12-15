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

        public void Update(int Id, Product product)
        {
            _logger.LogInformation("Product update info: {Update}", product);
        }

        public IEnumerable<Product> FilterProducts(IEnumerable<int>? categoryIds)
        {
            if (categoryIds == null || categoryIds.IsNullOrEmpty())
                return AllProducts;


            return AllProducts.Where(p => categoryIds.Contains(p.CategoryId));
        }

        public Product? GetProductById(int? productId)
        {
            return AllProducts.FirstOrDefault(p => p.Id == productId);
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
