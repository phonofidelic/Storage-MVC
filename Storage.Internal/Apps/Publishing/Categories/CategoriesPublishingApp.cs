using System;
using Ivy;
using Ivy.Shared;
using Ivy.Views;
using Storage.Core.Apps.Publishing.Categories;

namespace Storage.Internal.Apps.Publishing;

[App(icon: Icons.Bookmark, title: "Categories Publishing")]
public class CategoriesPublishingApp : ViewBase
{
    public override object? Build()
    {
        var categoriesService = UseService<ICategoriesPublishingService>();
        IQueryable<CategoriesPublishingListItem> categories = categoriesService.AllCategories
            .Select(c => new CategoriesPublishingListItem
            {
                Id = c.Id,
                Name = c.Name,
                ProductCount = c.Products.Count,
                Description = c.Description,
            });

        return new StackLayout([
            Text.H1("Categories Publishing"),
            new CategoriesPublishingTable(categories)
        ]);
    }
}
