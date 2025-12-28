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
        public int DefaultMinPrice { get; set; }
        public int DefaultMaxPrice { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
    }
}
