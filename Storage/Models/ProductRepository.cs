using Microsoft.EntityFrameworkCore;
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
                return _storageDbContext.Product.ToList();
            }
        }

        public IEnumerable<Product> FilterProducts(string? category)
        {
            throw new NotImplementedException();
        }

        public Product? GetProductById(int? productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductViewModel> GetSummary()
        {
            throw new NotImplementedException();
        }
    }
}
