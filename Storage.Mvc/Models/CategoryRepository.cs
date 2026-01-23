
using Microsoft.EntityFrameworkCore;
using Storage.Core.Entities;
using Storage.Infrastructure.Data;

namespace Storage.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StorageContext _storageContext;

        public CategoryRepository(StorageContext storageContext) { 
            _storageContext = storageContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _storageContext.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByIdAsync(IEnumerable<int> ids)
        {
            return await _storageContext.Categories.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await _storageContext.FindAsync<Category>(categoryId);
        }
    }
}
