using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Storage.Data;
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly StorageContext _storageDbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(StorageContext storageDbContext, ILogger<ProductRepository> logger)
        {
            _storageDbContext = storageDbContext;
            _logger = logger;
        }
        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _storageDbContext.Product.ToList();
            }
        }

        public async void Create(ProductCreateDto product)
        {
            _storageDbContext.Add(new Product()
            {
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Shelf = product.Shelf,
                Count = product.Count,
                Description = product.Description
            });
            
        
            _storageDbContext.SaveChanges();
        }

        public async void Update(int Id, Product product)
        {
            _storageDbContext.Update(product);
            _storageDbContext.SaveChanges();
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
    }
}
