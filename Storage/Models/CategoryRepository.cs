
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models.Entities;

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
            return await _storageContext.Category.Include(c => c.Products).ToListAsync();
        }
    }
}
