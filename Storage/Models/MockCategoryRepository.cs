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
}
