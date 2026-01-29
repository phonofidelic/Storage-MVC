using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Core.Apps.Publishing.Categories
{
    public class CategoriesPublishingListItem
    {
        public int Id { get; set; } = default!;

        [Display(Name = "Products")]
        public int ProductCount { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}
