using Microsoft.AspNetCore.Mvc.Rendering;

namespace Storage.Models.ViewModels
{
    public class AllProductsViewModel
    {
        public ProductEditViewModel Product { get; set; } = default!;
        public int Count { get; set; }
        public IEnumerable<int>? SelectedCategoryIds { get; set; } = [];
        public IEnumerable<Product> Products { get; set; } = [];
        public List<SelectListItem> Categories { get; set; } = [];
    }
}
