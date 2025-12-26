using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models.Entities;

namespace Storage.Services
{
    public interface ICategoryService
    {
        public List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories, IEnumerable<int> selectedIds);
        public List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories, int selectedId);
        public List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories);
    }
}
