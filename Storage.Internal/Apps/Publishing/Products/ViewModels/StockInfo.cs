namespace Storage.Core.Apps.Publishing.Products.ViewModels;

public class StockInfo
{
    public StockStatus Status { get; init; }
    public int Count { get; init; }

    public StockInfo(int inventoryCount)
    {
        Status = GetStockStatus(inventoryCount);
        Count = inventoryCount;
    }

    private static StockStatus GetStockStatus(int inventoryCount)
    {
        return inventoryCount switch
        {
            0 => StockStatus.OutOfStock,
            <=10 => StockStatus.Low,
            _ => StockStatus.Default
        };
    }
}
public enum StockStatus
{
    Default,
    Low,
    OutOfStock
}
