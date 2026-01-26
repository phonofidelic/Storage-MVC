using System;
using Ivy.Connections;
using Storage.Core.Apps.Publishing.products;

namespace Storage.Core.Connections.StorageInternal;

public class ProductsPublishingService(
    StorageInternalContextFactory contextFactory
    ) : IProductsPublishingService
{
    private readonly StorageInternalContext _context = contextFactory.CreateDbContext();

    public IQueryable<Product> AllProducts => _context.Products;
}
