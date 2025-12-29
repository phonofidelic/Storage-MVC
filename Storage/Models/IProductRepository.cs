
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Task<Product?> GetProductByIdAsync(int? productId);
        Task<IEnumerable<Product>> FilterProductsAsync(int? minPrice, int? MaxPrice, IEnumerable<int>? categoryIds, DateTime? minOrderDate, DateTime? maxOrderDate);
        Task CreateAsync(ProductCreateDto product);
        Task UpdateAsync(ProductEditDto product);
        void Delete(int Id);
        Task<int> GetMaxPrice();
        Task<int> GetMinPrice();
    }
}
