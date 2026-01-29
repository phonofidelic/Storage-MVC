using System;

namespace Storage.Core.Apps.Publishing.Products;

public interface IProductsPublishingService
{
    IQueryable<Product> AllProducts { get; }
}