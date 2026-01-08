
using Microsoft.EntityFrameworkCore;
using Storage.Core.Entities;
using Storage.Persistence.Data;

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
            throw new NotImplementedException();
            //return await _storageContext.Category.Include(c => c.Products).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByIdAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
            //return await _storageContext.Category.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await _storageContext.FindAsync<Category>(categoryId);
        }
    }
}
