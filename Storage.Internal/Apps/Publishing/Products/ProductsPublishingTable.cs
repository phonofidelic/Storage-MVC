using System;

namespace Storage.Core.Apps.Publishing;

public class ProductsPublishingTable(IQueryable<ProductsPublishingListItem> products) : ViewBase
{
    private IQueryable<ProductsPublishingListItem> _products { get; init; } = products;
    public override object? Build()
    {
        return _products.ToTable()
            .Width(Size.Full())
            .Clear()
            .Add(p => p.Name)
            .Add(p => p.Category)
            .Add(p => p.PurchasePrice)
            .Add(p => p.Price)
            .Add(p => p.OrderDate)
            .Add(p => p.Count)
            .Header(p => p.Count, "Inv. Count")
            .Add(p => p.Description)
            .Order(
                p => p.Name,
                p => p.Category,
                p => p.Price,
                p => p.PurchasePrice,
                p => p.OrderDate,
                p => p.Count,
                p => p.Description
                );
    }
}
