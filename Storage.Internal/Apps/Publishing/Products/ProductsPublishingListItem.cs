using System;

namespace Storage.Core.Apps.Publishing;

public class ProductsPublishingListItem
{
    public int Id { get; init; }

    public required string Name { get; init; }

    [DataType(DataType.Currency)]
    public decimal Price { get; init; }

    // [DataType(DataType.Currency)]
    [Display(Name = "Purchase Price")]
    [DisplayFormat(DataFormatString="{0:C0}")]
    public decimal PurchasePrice { get; init; }

    [Display(Name = "Order Date")]
    [DataType(DataType.Date)]
    public required DateTime OrderDate { get; init; }

    public required string Category { get; init; }

    public int Count { get; init; }

    public string Description { get; init; } = string.Empty;
}
