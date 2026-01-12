
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Storage.Core.Entities;
using Storage.Models.ViewModels;

namespace Storage.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        int AllProductsCount { get; }
        Task<Product?> GetProductByIdAsync(int? productId);
        Task<IEnumerable<Product>> FilterProductsAsync(decimal? minPrice, decimal? maxPrice, IEnumerable<int>? categoryIds, DateTime? minOrderDate, DateTime? maxOrderDate);
        Task CreateAsync(ProductCreateDto product);
        Task UpdateAsync(ProductEditDto product);
        void Delete(int Id);
        Task<decimal> GetMaxPrice();
        Task<decimal> GetMinPrice();
    }
}
