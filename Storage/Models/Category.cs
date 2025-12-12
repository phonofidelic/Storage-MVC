using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
