using Storage.Models.Entities;

namespace Storage.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<Category?> GetCategoryByIdAsync(int categoryId);

        Task<IEnumerable<Category>> GetCategoriesByIdAsync(IEnumerable<int> ids);
    }

}