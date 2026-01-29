using Storage.Core.Apps.Publishing.Products.Components;
using Storage.Core.Apps.Publishing.Products.ViewModels;

namespace Storage.Core.Apps.Views;

public class ProductListBlade : ViewBase
{
    private record ProductListRecord(
        int Id, string Name, string? CategoryName, int InventoryCount, decimal Price);

    public override object? Build()
    {
        var blades = UseContext<IBladeService>();
        var refreshToken = UseRefreshToken();
        var filter = UseState("");
        var productsQuery = UseProductListRecords(Context, filter.Value);

        UseEffect(() =>
        {
            if (refreshToken.ReturnValue is int productId)
            {
                blades.Pop(this, true);
                productsQuery.Mutator.Revalidate();
                blades.Push(this, new ProductDetailsBlade(productId));
            }
        }, [refreshToken]);

        var onItemClicked = new Action<Event<ListItem>>(e =>
        {
            var product = (ProductListItemViewModel)e.Sender.Tag!;
            blades.Push(this, new ProductDetailsBlade(product.Id), product.Name);
        });

        object CreateItem(ProductListItemViewModel listRecord) => new FuncView(context =>
        {
            var itemQuery = UseProductListRecord(context, listRecord);
            if (itemQuery.Loading || itemQuery.Value == null)
            {
                return new ListItem();
            }
            var product = itemQuery.Value;

            return new ListItem(
                title: product.Name,
                subtitle: string.Format("{0:C2} - {1}", product.Price, product.InventoryCount > 0 ? product.InventoryCount + " in stock" : "out of stock"),
                tag: product,
                onClick: onItemClicked,
                // Setting `items` doesn't seem to have ant effect?
                items:
                [
                    Layout.Horizontal().Gap(2)
                        | Text.Block(product.Price.ToString("{0:C1}"))
                        | new StockStatusBadge(product.InventoryCount)
                ]
            );
        });

        var createBtn = Icons.Plus.ToButton(_ =>
        {
            blades.Pop(this);
        }).Ghost().Tooltip("Create Product").ToTrigger((isOpen) => new ProductCreateDialog(isOpen, refreshToken));

        var items = (productsQuery.Value ?? []).Select(CreateItem);

        var header = Layout.Horizontal().Gap(1)
                     | filter.ToSearchInput().Placeholder("Search").Width(Size.Grow())
                     | createBtn;

        return new Fragment()
               | new BladeHeader(header)
               | (productsQuery.Value == null ? Text.Muted("Loading...") : new List(items));
    }

    private static QueryResult<ProductListItemViewModel[]> UseProductListRecords(IViewContext context, string filter)
    {
        var factory = context.UseService<StorageInternalContextFactory>();
        return context.UseQuery(
            key: (nameof(UseProductListRecords), filter),
            fetcher: async ct =>
            {
                await using var db = factory.CreateDbContext();

                var linq = db.Products.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    filter = filter.Trim();
                    linq = linq.Where(e => e.Name.Contains(filter));
                }

                return await linq
                    .OrderByDescending(e => e.CreatedAt)
                    .Take(50)
                    .Select(e => new ProductListItemViewModel
                    {
                        Id = e.Id, 
                        Name = e.Name, 
                        CategoryName = e.Category.Name,
                        InventoryCount = e.InventoryCount,
                        Price = e.Price
                    })
                    .ToArrayAsync(ct);
            },
            tags: [typeof(Product[])],
            options: new QueryOptions()
            {
                KeepPrevious = true
            }
        );
    }

    private static QueryResult<ProductListItemViewModel?> UseProductListRecord(IViewContext context, ProductListItemViewModel record)
    {
        var factory = context.UseService<StorageInternalContextFactory>();
        return context.UseQuery(
            key: (nameof(UseProductListRecord), record.Id),
            fetcher: async ct =>
            {
                await using var db = factory.CreateDbContext();
                return await db.Products
                    .Where(e => e.Id == record.Id)
                    .Select(e => new ProductListItemViewModel {
                        Id = e.Id, 
                        Name = e.Name, 
                        CategoryName = e.Category.Name, 
                        InventoryCount = e.InventoryCount,
                        Price = e.Price
                    })
                    .FirstOrDefaultAsync(ct);
            },
            options: new QueryOptions { RevalidateOnMount = false },
            initialValue: record,
            tags: [(typeof(Product), record.Id)]
        );
    }
}