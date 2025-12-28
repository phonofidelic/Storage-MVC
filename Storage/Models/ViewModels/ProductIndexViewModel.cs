using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models.Entities;

namespace Storage.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public ProductListItemViewModel Product { get; set; } = default!;
        public int Count { get; set; }
        public IEnumerable<int> SelectedCategoryIds { get; set; } = [];
        public IEnumerable<Category> SelectedCategories { get; set; } = [];
        public IEnumerable<ProductListItemViewModel> Products { get; set; } = [];
        public List<SelectListItem> Categories { get; set; } = [];
        public int DefaultMinPrice { get; set; }
        public int DefaultMaxPrice { get; set; }

        [DataType(DataType.Currency)]
        public int MaxPrice { get; set; }

        [DataType(DataType.Currency)]
        public int MinPrice { get; set; }
    }
}
