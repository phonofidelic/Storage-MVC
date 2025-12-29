using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Storage.Models.Entities;

namespace Storage.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public ProductListItemViewModel Product { get; set; } = default!;
        public int Count { get; set; }
        public IEnumerable<int> SelectedCategoryIds { get; set; } = [];

        [Display(Name = "categories")]
        public IEnumerable<Category> SelectedCategories { get; set; } = [];
        public IEnumerable<ProductListItemViewModel> Products { get; set; } = [];
        public List<SelectListItem> Categories { get; set; } = [];
        public int DefaultMinPrice { get; set; }
        public int DefaultMaxPrice { get; set; }

        [Display(Name = "max price")]
        [DataType(DataType.Currency)]
        public int MaxPrice { get; set; }

        [Display(Name = "min price")]
        [DataType(DataType.Currency)]
        public int MinPrice { get; set; }

        [Display(Name = "min. order date")]
        [DataType(DataType.Date)]
        public DateTime? MinOrderDate { get; set; }

        [Display(Name = "max. order date")]
        [DataType(DataType.Date)]
        public DateTime? MaxOrderDate { get; set; }

        public ProductSortBy SortBy { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}
