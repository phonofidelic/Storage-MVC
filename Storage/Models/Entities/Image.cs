using System.ComponentModel.DataAnnotations;

namespace Storage.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; } = default!;

        [Display(Name = "Alt text")]
        public string Alt { get; set; } = default!;
    }
}