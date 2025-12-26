using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Services
{
    public interface IProductService
    {
        public ProductSummary GetProductSummary(Product product);
        public int GetTotalInventoryValue(IEnumerable<ProductSummary> inventoryValues);
        public ProductDetailsViewModel MapProductDetails(Product product);

        public ProductEditViewModel MapProductEditViewModel(Product product, IEnumerable<SelectListItem> categorySelectItems);
        public ImageInputViewModel? MapImageInputViewModel(Image? image);
    }
}
