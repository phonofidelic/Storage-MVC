using System;

namespace Storage.Models;

public class MockCategoryRepository : ICategoryRepository
{
    private IEnumerable<Category> _mockCategories;

    public MockCategoryRepository()
    {
        _mockCategories = DbInitializer.GenerateCategoriesWithIds();
    }
    public IEnumerable<Category> AllCategories => _mockCategories;
}
