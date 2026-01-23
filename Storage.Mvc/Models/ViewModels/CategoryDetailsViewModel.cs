using System;

namespace Storage.Models.ViewModels;

public class CategoryDetailsViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }

    public ProductListItemViewModel Product { get; set; } = default!;
    public IEnumerable<ProductListItemViewModel> Products { get; set; } = [];
}
