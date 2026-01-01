using Storage.Models.Entities;
using System;

namespace Storage.Models;

public class MockCategoryRepository : ICategoryRepository
{
    private MockDb _mockDb;

    public MockCategoryRepository(MockDb db)
    {
        _mockDb = db;
        _mockDb.Categories = DbInitializer.GenerateCategoriesWithIds();
    }
    public IEnumerable<Category> AllCategories => _mockDb.Categories;

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return _mockDb.Categories.ToList();
    }

    public async Task<IEnumerable<Category>> GetCategoriesByIdAsync(IEnumerable<int> ids)
    {
        return _mockDb.Categories.Where(c => ids.Contains(c.Id));
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        return _mockDb.Categories.First(c => c.Id == categoryId);
    }
}
