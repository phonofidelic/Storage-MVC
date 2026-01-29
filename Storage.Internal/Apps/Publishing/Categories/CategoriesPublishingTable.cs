using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Core.Apps.Publishing.Categories
{
    public class CategoriesPublishingTable(IQueryable<CategoriesPublishingListItem> categories) : ViewBase
    {
        private IQueryable<CategoriesPublishingListItem> _categories { get; init; } = categories;

        public override object? Build()
        {
            return _categories.ToTable()
                .Width(Size.Full())
                .Clear()
                .Add(c => c.Name)
                .Add(c => c.ProductCount)
                .Add(c => c.Description)
                .Order(
                    c => c.Name,
                    c => c.ProductCount,
                    c => c.Description);
        }
    }
}
