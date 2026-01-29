using System;

namespace Storage.Core.Apps.Publishing.Categories;

public interface ICategoriesPublishingService
{
    IQueryable<Category> AllCategories { get; }
}
