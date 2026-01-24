using System;
using Ivy;
using Ivy.Shared;
using Ivy.Views;

namespace Storage.Internal.Apps.Publishing;

[App(icon: Icons.ShoppingBasket, title: "Products Publishing")]
public class ProductsPublishingApp : ViewBase
{
    public override object? Build()
    {
        return new StackLayout([
            Text.H1("Products Publishing")
        ]);
    }
}
