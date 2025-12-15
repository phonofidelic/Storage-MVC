using Storage.Models;
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
    }
}
