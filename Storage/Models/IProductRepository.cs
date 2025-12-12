
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(int? productId);
        IEnumerable<Product> FilterProducts(IEnumerable<int>? categoryIds);
        IEnumerable<ProductViewModel> GetSummary();
    }
}
