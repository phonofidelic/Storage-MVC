using System;
using Storage.Models.Entities;

namespace Storage.Models.ViewModels;

public class CategoryIndexViewModel
{
    public CategoryListItemViewModel Category { get; set; } = default!;
    public IEnumerable<CategoryListItemViewModel> Categories { get; set; } = [];
}
