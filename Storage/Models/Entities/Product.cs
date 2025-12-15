using System.ComponentModel.DataAnnotations;

namespace Storage.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be a number between {2} and {1}")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public int CategoryId { get; set; }
        public string Shelf { get; set; } = string.Empty;
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "{0} must be a number between {2} and {1}")]
        public int Count { get; set; }
        
        [StringLength(200)]
        public string? Description { get; set; } = string.Empty;

        public Image? Image { get; set; }
    }
}
