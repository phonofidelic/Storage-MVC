using System;
using Ivy.Connections;

namespace Storage.Core.Connections.StorageInternal;

public interface IPublishingService
{
    IQueryable<Product> AllProducts { get; }
}
public class PublishingService : IPublishingService
{
    private readonly StorageInternalContext _context;
    public PublishingService(
        StorageInternalContextFactory contextFactory
    )
    {
        _context = contextFactory.CreateDbContext();
    }
    public IQueryable<Product> AllProducts => _context.Products.Include(p => p.Category);
}
