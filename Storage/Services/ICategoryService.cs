using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models;

namespace Storage.Services
{
    public interface ICategoryService
    {
        public List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories, IEnumerable<int>? selectedIds = null);
    }
}
