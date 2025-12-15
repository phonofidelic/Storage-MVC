using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models.Entities;

namespace Storage.Models.ViewModels
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; } = default!;
        public IEnumerable<SelectListItem> Categories { get; set; } = [];
    }
}
