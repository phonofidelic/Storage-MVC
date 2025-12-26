using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Models.Entities;

namespace Storage.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ILogger<CategoryService> logger)
        {
            _logger = logger;
        }
        public List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories, IEnumerable<int> selectedIds)
        {
            return categories.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = selectedIds.Contains(c.Id)
            }).ToList();
        }

        public List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories, int selectedId)
        {
            _logger.LogInformation("*** GetCategorySelects, selectedId: {0}", selectedId);
            return categories.Select(c => { 
                _logger.LogInformation("c.Id: {0}, {1}", c.Id, c.Id == selectedId);
                return new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == selectedId
            };}).ToList();
        }

        public List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories)
        {
            return categories.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();
        }
    }
}
