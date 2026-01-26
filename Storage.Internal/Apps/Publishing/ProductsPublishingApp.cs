using System;
using Ivy;
using Ivy.Shared;
using Ivy.Views;
using Storage.Core.Connections;

namespace Storage.Internal.Apps.Publishing;

[App(icon: Icons.ShoppingBasket, title: "Products Publishing")]
public class ProductsPublishingApp : ViewBase
{
    public override async Task<object?> Build()
    {
        var publishingService = UseService<IPublishingService>();
        var products = publishingService.AllProducts;

        return new StackLayout([
            Text.H1("Products Publishing"),
            products.ToTable()
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
                .Add(p => p.Shelf)
                .Header(p => p.Shelf, "Stock Location")
        ]);
    }
}
