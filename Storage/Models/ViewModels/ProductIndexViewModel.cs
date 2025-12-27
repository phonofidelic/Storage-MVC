using Microsoft.AspNetCore.Mvc.Rendering;

namespace Storage.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public ProductListItemViewModel Product { get; set; } = default!;
        public int Count { get; set; }
        public IEnumerable<int>? SelectedCategoryIds { get; set; } = [];
        public IEnumerable<ProductListItemViewModel> Products { get; set; } = [];
        public List<SelectListItem> Categories { get; set; } = [];
    }
}
