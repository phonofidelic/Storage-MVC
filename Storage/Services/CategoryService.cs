using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models.Entities;

namespace Storage.Services
{
    public class CategoryService : ICategoryService
    {
        public List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories, IEnumerable<int>? selectedIds = null)
        {
            return categories.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = selectedIds?.Contains(c.Id) ?? false
            }).ToList();
        }
    }
}
