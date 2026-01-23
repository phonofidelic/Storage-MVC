using System.ComponentModel.DataAnnotations;

namespace Storage.Models.ViewModels
{
    public class ImageInputViewModel
    {
        [Display(Name = "Image path")]
        public string? Path { get; set; } = "";

        [Display(Name = "Alternative text")]
        public string? Alt { get; set; } = "Product image description";
    }
}
