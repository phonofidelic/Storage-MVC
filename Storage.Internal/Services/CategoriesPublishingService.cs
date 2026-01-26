using System;
using Storage.Core.Apps.Publishing.Categories;

namespace Storage.Core.Services;

public class CategoriesPublishingService(
    StorageInternalContextFactory contextFactory
) : ICategoriesPublishingService
{
    private readonly StorageInternalContext _context = contextFactory.CreateDbContext();
    public IQueryable<Category> AllCategories => _context.Categories;
}
