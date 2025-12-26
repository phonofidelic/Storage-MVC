using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Storage.Data;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public class ProductRepository : IProductRepository
    {
        private IEnumerable<Product> _products { get; set; } = [];
        private readonly StorageContext _storageDbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(StorageContext storageDbContext, ILogger<ProductRepository> logger)
        {
            _storageDbContext = storageDbContext;
            _logger = logger;
        }
        private async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            _products = await _storageDbContext.Product.ToListAsync();
            return _products;
        }
        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _storageDbContext.Product.Include(p => p.Category).Include(p => p.Image);
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

        public async Task UpdateAsync(ProductEditDto productEditDto)
        {
            Product product = await _storageDbContext.FindAsync<Product>(productEditDto.Id) ?? 
                throw new Exception(string.Format("Could not find product with Id '(Id)'", productEditDto.Id));

            Image? productImage = productEditDto.Image?.Alt != null && productEditDto.Image?.Path != null ? new()
            {
                Alt = productEditDto.Image.Alt ?? product.Image?.Alt!,
                Path = productEditDto.Image.Path ?? product.Image?.Path!
            } : product.Image;

            if (productImage != null)
            {
                _storageDbContext.Add(productImage);
            }

            product.Name = productEditDto.Name ?? product.Name;
            product.Price = productEditDto.Price ?? product.Price;
            product.OrderDate = productEditDto.OrderDate ?? product.OrderDate;
            product.Category = productEditDto.Category ?? product.Category;
            product.CategoryId = productEditDto.CategoryId ?? product.CategoryId;
            product.Shelf = productEditDto.Shelf ?? product.Shelf;
            product.Count = productEditDto.Count ?? product.Count;
            product.Description = productEditDto.Description ?? product.Description;
            product.Image = productImage ?? product.Image;


            // _storageDbContext.Update(product);
            await _storageDbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int? productId)
        {
            return await _storageDbContext.Product
                .Include(p => p.Category)
                .Include(p => p.Image)
                .FirstAsync(p => p.Id == productId);
        }

        public async void Delete(int Id)
        {
            var product = _storageDbContext.Product.Find(Id);
            if (product != null)
            {
                _storageDbContext.Product.Remove(product);
            }

            _storageDbContext.SaveChanges();
        }

        public async Task<IEnumerable<Product>> FilterProductsAsync(IEnumerable<int>? categoryIds)
        {
            var allProducts = await GetAllProductsAsync();

            if (categoryIds == null || categoryIds.IsNullOrEmpty())
                return allProducts;


            return allProducts.Where(p => categoryIds.Contains(p.CategoryId));
        }

        public async Task CreateAsync(ProductCreateDto product)
        {
            await _storageDbContext.AddAsync(new Product()
            {
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Shelf = product.Shelf,
                Count = product.Count,
                Description = product.Description
            });

            await _storageDbContext.SaveChangesAsync();
        }
    }
}
