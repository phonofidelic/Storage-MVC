using Storage.Core.Apps.Publishing.Products.Components;
using Storage.Core.Apps.Publishing.Products.Views;

namespace Storage.Core.Apps.Views;

public class ProductDetailsBlade(int productId) : ViewBase
{
    public override object? Build()
    {
        var factory = UseService<StorageInternalContextFactory>();
        var blades = UseContext<IBladeService>();
        var queryService = UseService<IQueryService>();

        var productQuery = UseQuery(
            key: (nameof(ProductDetailsBlade), productId),
            fetcher: async ct =>
            {
                await using var db = factory.CreateDbContext();
                return await db.Products
                    .Include(e => e.Category)
                    .Include(e => e.Images)
                    .SingleOrDefaultAsync(e => e.Id == productId, ct);
            },
            tags: [(typeof(Product), productId)]
        );

        if (productQuery.Loading) return Skeleton.Card();

        if (productQuery.Value == null)
        {
            return new Callout($"Product '{productId}' not found. It may have been deleted.")
                .Variant(CalloutVariant.Warning);
        }

        var productValue = productQuery.Value;

        var deleteBtn = new Button("Delete", onClick: async _ =>
            {
                blades.Pop(refresh: true);
                await DeleteAsync(factory);
                queryService.RevalidateByTag(typeof(Product[]));
            })
            .Variant(ButtonVariant.Destructive)
            .Icon(Icons.Trash)
            .WithConfirm("Are you sure you want to delete this product?", "Delete Product");

        var editBtn = new Button("Edit")
            .Variant(ButtonVariant.Outline)
            .Icon(Icons.Pencil)
            .Width(Size.Grow())
            .ToTrigger((isOpen) => new ProductEditSheet(isOpen, productId));

        var detailsCard = new Card(
            content: new ProductDetailsViewModel
                {
                    Id = productValue.Id,
                    Name = productValue.Name,
                    Price = productValue.Price,
                    PurchasePrice = productValue.PurchasePrice,
                    InventoryCount = productValue.InventoryCount,
                    Shelf = productValue.Shelf,
                    CategoryName = productValue.Category.Name,
                    Description = productValue.Description ?? string.Empty,
                    Images = productValue.Images
                }
                .ToDetails()
                .MultiLine(e => e.Description)
                .RemoveEmpty()
                .Builder(e => e.Id, e => e.CopyToClipboard()),
            footer: Layout.Horizontal().Gap(2).Align(Align.Right)
                    | deleteBtn
                    | editBtn
        ).Title("Product Details").Width(Size.Units(100));

        return new Fragment()
               | new BladeHeader(Text.Literal(productValue.Name))
               | (Layout.Vertical() | detailsCard);
    }

    private async Task DeleteAsync(StorageInternalContextFactory dbFactory)
    {
        await using var db = dbFactory.CreateDbContext();
        var product = await db.Products.FirstOrDefaultAsync(e => e.Id == productId);
        if (product != null)
        {
            db.Products.Remove(product);
            await db.SaveChangesAsync();
        }
    }
}