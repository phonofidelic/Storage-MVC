using System;

namespace Storage.Core.Apps.Publishing.products;

public interface IProductsPublishingService
{
    IQueryable<Product> AllProducts { get; }
}