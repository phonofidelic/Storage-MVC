using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IProductRepository
    {
        IEnumerable<ProductIndexViewModel> AllProducts { get; }
        ProductIndexViewModel? GetProductById(int? productId);
    }
}
