using Storage.Core.Apps.Publishing;
using Storage.Models;
using Storage.Models.ViewModels;

namespace Storage.Core.Apps.Views;

public class ProductEditSheet(IState<bool> isOpen, int productId) : ViewBase
{
    public override object? Build()
    {
        var factory = UseService<StorageInternalContextFactory>();
        var queryService = UseService<IQueryService>();
        var publishingRepository = UseService<IPublishingRepository>();

        var productQuery = UseQuery(
            key: (typeof(ProductEditViewModel), productId),
            fetcher: async ct =>
            {
                await using var db = factory.CreateDbContext();
                var queryResult = await db.Products.FirstAsync(e => e.Id == productId, ct);
                
                return new ProductEditViewModel
                {
                    Id = queryResult.Id,
                    Name = queryResult.Name,
                    Price = queryResult.Price,
                    PurchasePrice = queryResult.PurchasePrice,
                    InventoryCount = queryResult.InventoryCount,
                    OrderDate = queryResult.OrderDate,
                    Shelf = queryResult.Shelf,
                    CategoryId = queryResult.CategoryId,
                    Description = queryResult.Description ?? ""
                };
            },
            tags: [(typeof(Product), productId)]
        );

        if (productQuery.Loading || productQuery.Value == null)
            return Skeleton.Form().ToSheet(isOpen, "Edit Product");

        return productQuery.Value
            .ToForm()
            .Builder(e => e.Name, e => e.ToTextInput())
            .Builder(e => e.Price, e => e.ToMoneyInput().Currency(RegionInfo.CurrentRegion.ISOCurrencySymbol))
            .Builder(e => e.PurchasePrice, e => e.ToMoneyInput().Currency(RegionInfo.CurrentRegion.ISOCurrencySymbol))
            .Builder(e => e.InventoryCount, e => e.ToNumberInput())
            .Builder(e => e.Shelf, e => e.ToTextInput())
            .Builder(e => e.Description, e => e.ToTextAreaInput())
            .Builder(e => e.CategoryId, e => e.ToAsyncSelectInput(UseCategorySearch, UseCategoryLookup, placeholder: "Select Category"))
            .Remove(e => e.Id)
            .HandleSubmit(OnSubmit)
            .ToSheet(isOpen, "Edit Product");

        async Task OnSubmit(ProductEditViewModel? request)
        {
            if (request == null) return;

            ProductEditDto productEditDto = new 
            (
                Id: request.Id,
                Name: request.Name,
                Price: request.Price,
                PurchasePrice: request.PurchasePrice,
                OrderDate: request.OrderDate,
                CategoryId: request.CategoryId,
                Shelf: request.Shelf,
                InventoryCount: request.InventoryCount,
                Description: request.Description
            );

            await publishingRepository.EditProductAsync(productEditDto);
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