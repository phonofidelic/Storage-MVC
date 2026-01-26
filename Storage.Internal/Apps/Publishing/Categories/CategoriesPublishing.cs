using System;
using Ivy;
using Ivy.Shared;
using Ivy.Views;

namespace Storage.Internal.Apps.Publishing;

[App(icon: Icons.Bookmark, title: "Categories Publishing")]
public class CategoriesPublishing : ViewBase
{
    public override object? Build()
    {
        return new StackLayout([
            Text.H1("Categories Publishing")
        ]);
    }
}
