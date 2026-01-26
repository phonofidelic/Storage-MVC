using System;
using Ivy;
using Ivy.Shared;
using Ivy.Views;
using Storage.Core.Apps.Publishing;
using Storage.Core.Connections;

namespace Storage.Internal.Apps.Publishing;

[App(icon: Icons.ShoppingBasket, title: "Products Publishing")]
public class ProductsPublishingApp : ViewBase
{
    public override async Task<object?> Build()
    {
        var publishingService = UseService<IPublishingService>();
        var products = publishingService.AllProducts
            .Select(p => new ProductsPublishingListItem
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category.Name,
                Price = p.Price,
                PurchasePrice = p.PurchasePrice,
                OrderDate = p.OrderDate,
                Count = p.InventoryCount,
                Description = p.Description ?? "",
            });

        return new StackLayout([
            Text.H1("Products Publishing"),
            new ProductsPublishingTable(products)
        ]);
    }
}
