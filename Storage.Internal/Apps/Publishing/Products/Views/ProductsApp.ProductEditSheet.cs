namespace Storage.Core.Apps.Views;

public class ProductEditSheet(IState<bool> isOpen, int productId) : ViewBase
{
    public override object? Build()
    {
        var factory = UseService<StorageInternalContextFactory>();
        var queryService = UseService<IQueryService>();

        var productQuery = UseQuery(
            key: (typeof(Product), productId),
            fetcher: async ct =>
            {
                await using var db = factory.CreateDbContext();
                return await db.Products.FirstAsync(e => e.Id == productId, ct);
            },
            tags: [(typeof(Product), productId)]
        );

        if (productQuery.Loading || productQuery.Value == null)
            return Skeleton.Form().ToSheet(isOpen, "Edit Product");

        return productQuery.Value
            .ToForm()
            .Builder(e => e.Name, e => e.ToTextInput())
            .Builder(e => e.Price, e => e.ToMoneyInput().Currency("USD"))
            .Builder(e => e.PurchasePrice, e => e.ToMoneyInput().Currency("USD"))
            .Builder(e => e.InventoryCount, e => e.ToNumberInput())
            .Builder(e => e.Shelf, e => e.ToTextInput())
            .Builder(e => e.Description, e => e.ToTextAreaInput())
            .Builder(e => e.CategoryId, e => e.ToAsyncSelectInput(UseCategorySearch, UseCategoryLookup, placeholder: "Select Category"))
            .Remove(e => e.Id, e => e.CreatedAt, e => e.UpdatedAt)
            .HandleSubmit(OnSubmit)
            .ToSheet(isOpen, "Edit Product");

        async Task OnSubmit(Product? request)
        {
            if (request == null) return;
            await using var db = factory.CreateDbContext();
            request.UpdatedAt = DateTime.UtcNow;
            db.Products.Update(request);
            await db.SaveChangesAsync();
            queryService.RevalidateByTag((typeof(Product), productId));
        }
    }

    private static QueryResult<Option<int?>[]> UseCategorySearch(IViewContext context, string query)
    {
        var factory = context.UseService<StorageInternalContextFactory>();
        return context.UseQuery(
            key: (nameof(UseCategorySearch), query),
            fetcher: async ct =>
            {
                await using var db = factory.CreateDbContext();
                return (await db.Categories
                        .Where(e => e.Name.Contains(query))
                        .Select(e => new { e.Id, e.Name })
                        .Take(50)
                        .ToArrayAsync(ct))
                    .Select(e => new Option<int?>(e.Name, e.Id))
                    .ToArray();
            });
    }

    private static QueryResult<Option<int?>?> UseCategoryLookup(IViewContext context, int? id)
    {
        var factory = context.UseService<StorageInternalContextFactory>();
        return context.UseQuery(
            key: (nameof(UseCategoryLookup), id),
            fetcher: async ct =>
            {
                if (id == null) return null;
                await using var db = factory.CreateDbContext();
                var category = await db.Categories.FirstOrDefaultAsync(e => e.Id == id, ct);
                if (category == null) return null;
                return new Option<int?>(category.Name, category.Id);
            });
    }
}