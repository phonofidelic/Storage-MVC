
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(int? productId);
        IEnumerable<Product> FilterProducts(IEnumerable<int>? categoryIds);
        void Create(ProductCreateDto product);
        void Update(int Id, Product product);
        void Delete(int Id);
    }

}
