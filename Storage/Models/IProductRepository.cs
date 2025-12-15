
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(int? productId);
        IEnumerable<Product> FilterProducts(IEnumerable<int>? categoryIds);
        IEnumerable<ProductViewModel> GetSummary();
        void Create(ProductCreateDto product);
        void Update(int Id, Product product);
    }

}
