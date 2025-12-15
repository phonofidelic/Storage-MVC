using Storage.Models;
using Storage.Models.ViewModels;

namespace Storage.Services
{
    public interface IProductService
    {
        public ProductSummary GetProductSummary(Product product);
    }
}
