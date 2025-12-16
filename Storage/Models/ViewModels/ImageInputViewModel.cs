namespace Storage.Models.ViewModels
{
    public class ImageInputViewModel
    {
        public bool IsOpen { get; set; } = false;
        public string? ImagePath { get; set; } = null;
        public string? AltText { get; set; } = null;
    }
}
