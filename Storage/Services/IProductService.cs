using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Services
{
    public interface IProductService
    {
        public ProductSummary GetProductSummary(Product product);
        public int GetTotalInventoryValue(IEnumerable<ProductSummary> inventoryValues);
        public ProductDetailsViewModel MapProductDetails(Product product, IEnumerable<Category> allCategories);
    }
}
