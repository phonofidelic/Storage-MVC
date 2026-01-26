namespace Storage.Core.Apps.Views;

public class ProductListBlade : ViewBase
{
    private record ProductListRecord(int Id, string Name, string? CategoryName);

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
            var product = (ProductListRecord)e.Sender.Tag!;
            blades.Push(this, new ProductDetailsBlade(product.Id), product.Name);
        });

        object CreateItem(ProductListRecord listRecord) => new FuncView(context =>
        {
            var itemQuery = UseProductListRecord(context, listRecord);
            if (itemQuery.Loading || itemQuery.Value == null)
            {
                return new ListItem();
            }
            var record = itemQuery.Value;
            return new ListItem(title: record.Name, subtitle: record.CategoryName, onClick: onItemClicked, tag: record);
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

    private static QueryResult<ProductListRecord[]> UseProductListRecords(IViewContext context, string filter)
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
                    .Select(e => new ProductListRecord(e.Id, e.Name, e.Category != null ? e.Category.Name : null))
                    .ToArrayAsync(ct);
            },
            tags: [typeof(Product[])],
            options: new QueryOptions()
            {
                KeepPrevious = true
            }
        );
    }

    private static QueryResult<ProductListRecord?> UseProductListRecord(IViewContext context, ProductListRecord record)
    {
        var factory = context.UseService<StorageInternalContextFactory>();
        return context.UseQuery(
            key: (nameof(UseProductListRecord), record.Id),
            fetcher: async ct =>
            {
                await using var db = factory.CreateDbContext();
                return await db.Products
                    .Where(e => e.Id == record.Id)
                    .Select(e => new ProductListRecord(e.Id, e.Name, e.Category != null ? e.Category.Name : null))
                    .FirstOrDefaultAsync(ct);
            },
            options: new QueryOptions { RevalidateOnMount = false },
            initialValue: record,
            tags: [(typeof(Product), record.Id)]
        );
    }
}