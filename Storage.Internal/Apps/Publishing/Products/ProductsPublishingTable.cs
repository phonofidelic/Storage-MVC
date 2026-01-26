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
            .Header(p => p.Category, "Category")
            .Add(p => p.PurchasePrice)
            .Header(p => p.PurchasePrice, "Purchase Price")
            .Add(p => p.Price)
            .Header(p => p.Price, "Sales Price")
            .Add(p => p.OrderDate)
            .Header(p => p.OrderDate, "Order Date")
            .Add(p => p.Description);
    }
}
