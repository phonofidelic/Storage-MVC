using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Storage.Data;
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly StorageContext _storageDbContext;

        public ProductRepository(StorageContext storageDbContext)
        {
            _storageDbContext = storageDbContext;
        }
        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _storageDbContext.Product.Include(p => p.Category);
            }
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
            return AllProducts.Select(p => new ProductViewModel()
            {
                Name = p.Name,
                Price = p.Price,
                Count = p.Count,
                InventoryValue = p.Price * p.Count
            });
        }
    }
}
