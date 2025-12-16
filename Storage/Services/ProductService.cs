using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Services
{
    public class ProductService : IProductService
    {
        public ProductSummary GetProductSummary(Product product)
        {
            return new ProductSummary()
            {
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                InventoryValue = product.Price * product.Count
            };
        }

        public int GetTotalInventoryValue(IEnumerable<ProductSummary> productSummaries)
        {
            return productSummaries.Sum(p => p.InventoryValue);
        }

        public ProductDetailsViewModel MapProductDetails(Product product, IEnumerable<Category> allCategories)
        {

            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Category = allCategories.First(c => c.Id == product.CategoryId),
                Shelf = product.Shelf,
                Count = product.Count,
                Description = product.Description
            };
        }
    }
}
