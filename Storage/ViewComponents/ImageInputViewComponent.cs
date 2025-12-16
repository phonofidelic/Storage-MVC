using Microsoft.AspNetCore.Mvc;
using Storage.Models.ViewModels;

namespace Storage.ViewComponents
{
    public class ImageInputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string alt, string src)
        {
            ImageInputViewModel viewModel = new()
            {
                IsOpen = false,
                AltText = alt,
                ImagePath = src
            };

            return View(viewModel);
        }
    }
}
