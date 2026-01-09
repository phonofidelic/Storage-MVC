using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;

namespace Storage.Models.ViewModels;

public class ProductCreateViewModel
{
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        [Range(0, 999999999999, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; } = 0;

        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        
        public string Shelf { get; set; } = string.Empty;

        public int Count { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> CategorySelectItems { get; set; } = [];
}