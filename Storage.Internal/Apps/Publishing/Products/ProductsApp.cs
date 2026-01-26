using Storage.Core.Apps.Views;

namespace Storage.Core.Apps;

[App(icon: Icons.PackagePlus, path: ["Publishing"])]
public class ProductsApp : ViewBase
{
    public override object? Build()
    {
        return this.UseBlades(() => new ProductListBlade(), "Search");
    }
}
