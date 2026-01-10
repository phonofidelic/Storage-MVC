using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;

namespace Storage.Models.ViewModels;

public class ProductCreateViewModel
{
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be a positive number between {1} and {2}")]
        public decimal Price { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be a positive number between {1} and {2}")]
        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        
        public string Shelf { get; set; } = default!;

        public int Count { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> CategorySelectItems { get; set; } = [];
}