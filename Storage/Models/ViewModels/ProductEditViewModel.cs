using System.ComponentModel.DataAnnotations;

namespace Storage.Models.ViewModels
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public string Shelf { get; set; } = string.Empty;
        public int Count { get; set; }
        public string Description { get; set; } = string.Empty;
        public IEnumerable<Category> Categories { get; set; } = [];
    }
}
