using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Core.Entities;
using Storage.Models;
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
                Price = (int)product.Price,
                Count = product.InventoryCount,
                InventoryValue = (int)product.Price * product.InventoryCount
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
                Alt = image.AltText,
                Path = image.Src
            } : null;
        }

        public ProductDetailsViewModel MapProductDetails(Product product)
        {
            

            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = (int)product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Category = product.Category,
                Shelf = product.Shelf,
                Count = product.InventoryCount,
                Description = product.Description,
                //Image = MapImageInputViewModel(product.Image)
            };
        }

        public ProductEditViewModel MapProductEditViewModel(Product product, IEnumerable<SelectListItem> categorySelectItems)
        {
            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = (int)product.Price,
                OrderDate = product.OrderDate,
                // Category = product.Category,
                CategoryId = product.CategoryId,
                Shelf = product.Shelf,
                Count = product.InventoryCount,
                Description = product.Description,
                //Image = MapImageInputViewModel(product.Image),
                CategorySelectItems = categorySelectItems,
            };
        }

        public ProductListItemViewModel MapProductListItem(Product product)
        {
            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = (int)product.Price,
                OrderDate = product.OrderDate,
                Category = product.Category,
                Shelf = product.Shelf,
                Count = product.InventoryCount,
                Description = product.Description
            };
        }
    }
}
