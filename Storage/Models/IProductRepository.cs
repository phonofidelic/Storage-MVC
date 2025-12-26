
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Task<Product?> GetProductByIdAsync(int? productId);
        Task<IEnumerable<Product>> FilterProductsAsync(IEnumerable<int>? categoryIds);
        Task CreateAsync(ProductCreateDto product);
        Task UpdateAsync(ProductEditDto product);
        void Delete(int Id);
    }

}
