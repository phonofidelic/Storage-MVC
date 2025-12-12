using Storage.Models.ViewModels;
using Bogus;

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
