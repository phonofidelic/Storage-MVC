namespace Storage.Core.Apps.Views;

public class ProductCreateDialog(IState<bool> isOpen, RefreshToken refreshToken) : ViewBase
{
    private record ProductCreateRequest
    {
        [Required]
        public string Name { get; init; } = "";

        [Required]
        public decimal? Price { get; init; } = null;

        [Required]
        public decimal? PurchasePrice { get; init; } = null;

        [Required]
        public int? InventoryCount { get; init; } = null;

        [Required]
        public string Shelf { get; init; } = "";

        public string? Description { get; init; } = null;

        [Required]
        public int? CategoryId { get; init; } = null;
    }

    public override object? Build()
    {
        var factory = UseService<StorageInternalContextFactory>();
        var product = UseState(() => new ProductCreateRequest());

        return product
            .ToForm()
            .Builder(e => e.Price, e => e.ToMoneyInput().Currency("USD"))
            .Builder(e => e.PurchasePrice, e => e.ToMoneyInput().Currency("USD"))
            .Builder(e => e.CategoryId, e => e.ToAsyncSelectInput(UseCategorySearch, UseCategoryLookup, placeholder: "Select Category"))
            .HandleSubmit(OnSubmit)
            .ToDialog(isOpen, title: "Create Product", submitTitle: "Create");

        async Task OnSubmit(ProductCreateRequest request)
        {
            var productId = await CreateProductAsync(factory, request);
            refreshToken.Refresh(productId);
        }
    }

    private async Task<int> CreateProductAsync(StorageInternalContextFactory factory, ProductCreateRequest request)
    {
        await using var db = factory.CreateDbContext();

        var product = new Product
        {
            Name = request.Name,
            Price = request.Price!.Value,
            PurchasePrice = request.PurchasePrice!.Value,
            InventoryCount = request.InventoryCount!.Value,
            Shelf = request.Shelf,
            Description = request.Description,
            CategoryId = request.CategoryId!.Value,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();

        return product.Id;
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