using System.ComponentModel.DataAnnotations;

namespace Storage.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }

        [Display(Name="Order Date")]
        public DateTime OrderDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;
        public int Count { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
