using Storage.Models.Entities;

namespace Storage.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }

}