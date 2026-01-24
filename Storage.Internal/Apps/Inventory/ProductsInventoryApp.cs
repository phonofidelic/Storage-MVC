using System;
using Ivy;
using Ivy.Views;

namespace Storage.Internal.Apps.Inventory;

[App(icon: Ivy.Shared.Icons.Warehouse, title: "Products Inventory")]
public class ProductsInventoryApp : ViewBase
{
    public override object? Build()
    {
        return new StackLayout([
            Text.H1("Products Inventory")
        ]);
    }
}
