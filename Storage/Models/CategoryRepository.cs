
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models.Entities;

namespace Storage.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StorageContext _storateContext;

        public CategoryRepository(StorageContext storageContext) { 
            _storateContext = storageContext;
        }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _storateContext.Category.Include(c => c.Products);
            }
        }
    }
}
