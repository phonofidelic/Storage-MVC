using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Storage.Models.ViewModels
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be a number between {1} and {2}")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be a number between {1} and {2}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        // public Category Category { get; set; } = default!;

        public int CategoryId { get; set; } = default!;

        public string Shelf { get; set; } = default!;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be a number between {2} and {1}")]
        public int Count { get; set; }

        [StringLength(200)]
        public string? Description { get; set; } = string.Empty;

        public ImageInputViewModel? Image { get; set; }

        public IEnumerable<SelectListItem> CategorySelectItems { get; set; } = [];
    }
}
