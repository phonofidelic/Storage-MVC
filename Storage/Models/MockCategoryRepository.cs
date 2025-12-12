using System;

namespace Storage.Models;

public class MockCategoryRepository : ICategoryRepository
{
    private IEnumerable<Category> _mockCategoties;

    public MockCategoryRepository()
    {
        _mockCategoties = DbInitializer.GenerateCategories();
    }
    public IEnumerable<Category> AllCategories => _mockCategoties;
}
