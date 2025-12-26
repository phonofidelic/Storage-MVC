using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Services
{
    public class ProductService : IProductService
    {
        public ProductSummary GetProductSummary(Product product)
        {
            return new ProductSummary()
            {
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                InventoryValue = product.Price * product.Count
            };
        }

        public int GetTotalInventoryValue(IEnumerable<ProductSummary> productSummaries)
        {
            return productSummaries.Sum(p => p.InventoryValue);
        }

        public ImageInputViewModel? MapImageInputViewModel(Image? image)
        {
            return image != null ? new ()
            {
                Alt = image.Alt,
                Path = image.Path
            } : null;
        }

        public ProductDetailsViewModel MapProductDetails(Product product)
        {
            

            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Category = product.Category,
                Shelf = product.Shelf,
                Count = product.Count,
                Description = product.Description,
                Image = MapImageInputViewModel(product.Image)
            };
        }

        public ProductEditViewModel MapProductEditViewModel(Product product, IEnumerable<SelectListItem> categorySelectItems)
        {
            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                // Category = product.Category,
                CategoryId = product.CategoryId,
                Shelf = product.Shelf,
                Count = product.Count,
                Description = product.Description,
                Image = MapImageInputViewModel(product.Image),
                CategorySelectItems = categorySelectItems,
            };
        }
    }
}
