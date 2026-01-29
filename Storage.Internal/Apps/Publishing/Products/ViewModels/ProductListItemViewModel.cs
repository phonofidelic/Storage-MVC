using System;

namespace Storage.Core.Apps.Publishing.Products.ViewModels;

public class ProductListItemViewModel
{
    public int Id { get; set; }

    public required string Name { get; set; }
    
    public required string CategoryName { get; set; }

    public int InventoryCount { get; set; }

    public decimal Price { get; set; }
}

