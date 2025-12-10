
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(int? productId);
        IEnumerable<ProductViewModel> GetSummary();
    }
}
