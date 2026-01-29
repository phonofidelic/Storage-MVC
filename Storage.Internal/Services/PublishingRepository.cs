using System;
using Storage.Core.Apps.Publishing;
using Storage.Models;

namespace Storage.Core.Services;

public class PublishingRepository(StorageInternalContextFactory contextFactory) : IPublishingRepository
{
    public async Task EditProductAsync(ProductEditDto productEditDto)
    {
        await using var dbContext = contextFactory.CreateDbContext();

        Product product = await dbContext.Products.FindAsync(productEditDto.Id) 
            ?? throw new KeyNotFoundException(message: string.Format("Could not find Product with Id {0}", productEditDto.Id));
        
        product.Name = productEditDto.Name ?? product.Name;
        product.Price = productEditDto.Price ?? product.Price;
        product.PurchasePrice = productEditDto.PurchasePrice ?? product.PurchasePrice;
        product.OrderDate = productEditDto.OrderDate ?? product.OrderDate;
        product.CategoryId = productEditDto.CategoryId ?? product.CategoryId;
        product.Shelf = productEditDto.Shelf ?? product.Shelf;
        product.InventoryCount = productEditDto.InventoryCount ?? product.InventoryCount;
        product.Description = productEditDto.Description ?? product.Description;

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
    }
}
