using Storage.Models.ViewModels;
using Bogus;
using Microsoft.IdentityModel.Tokens;

namespace Storage.Models
{
    public class MockProductRepository : IProductRepository
    {
        private MockDb _mockDb;
        private Randomizer _random = new();
        public MockProductRepository(MockDb db)
        {   
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
