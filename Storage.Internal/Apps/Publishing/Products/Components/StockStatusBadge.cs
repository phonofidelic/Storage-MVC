using System;
using System.Diagnostics;
using Storage.Core.Apps.Publishing.Products.ViewModels;

namespace Storage.Core.Apps.Publishing.Products.Components;

public class StockStatusBadge(StockInfo stockInfo) : ViewBase
{
    public override object? Build()
    {
        return stockInfo.Status switch
        {
            StockStatus.OutOfStock => new Badge("out of stock").Variant(BadgeVariant.Destructive),
            StockStatus.Low => new Badge(stockInfo.Count.ToString("0 in stock")).Variant(BadgeVariant.Warning),
            _ => new Badge(stockInfo.Count.ToString("0 in stock")).Variant(BadgeVariant.Primary),
        };
    }
}

public class StockStatusContentBuilder : IContentBuilder
{
    public bool CanHandle(object? content)
    {
        return content is StockInfo;
    }

    public object? Format(object? content)
    {
        if (content is StockInfo info)
        {
            return new StockStatusBadge(info);
        }
        // Transform your custom type into a visual representation
        return  content;
    }

    private StockStatus GetStockStatus(int inventoryCount)
    {
        return inventoryCount switch
        {
            0 => StockStatus.OutOfStock,
            <=10 => StockStatus.Low,
            _ => StockStatus.Default
        };
    }
}

