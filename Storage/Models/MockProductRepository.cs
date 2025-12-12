using Storage.Models.ViewModels;
using Bogus;
using Microsoft.IdentityModel.Tokens;

namespace Storage.Models
{
    public class MockProductRepository : IProductRepository
    {
        private IEnumerable<Product> _mockProducts;
        public MockProductRepository()
        {   
            _mockProducts = DbInitializer.GenerateProducts();;
        }
        public IEnumerable<Product> AllProducts => _mockProducts.ToList();

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
