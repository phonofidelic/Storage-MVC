using Storage.Core.Apps.Publishing.Products.ViewModels;
using ProductImage = Storage.Core.Connections.StorageInternal.Image;

namespace Storage.Core.Apps.Publishing.Products.Views;

public class ProductDetailsViewModel
{
    public int Id { get; init; }

    public required string Name { get; init; }

    [DataType(DataType.Currency)]
    public required decimal Price { get; init; }

    [DataType(DataType.Currency)]
    public required decimal PurchasePrice { get; init; }

    public required int InventoryCount { get; init; }

    public StockInfo StockInfo { get => new(InventoryCount); }

    public required string Shelf { get; init; }

    public required string CategoryName { get; init; }

    public required string Description { get; init; }

    public IEnumerable<ProductImage> Images { get; set; } = [];
}
