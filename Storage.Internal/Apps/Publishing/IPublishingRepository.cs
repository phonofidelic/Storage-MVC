using System;
using Storage.Models;

namespace Storage.Core.Apps.Publishing;

public interface IPublishingRepository
{
    public Task EditProductAsync(ProductEditDto productEditDto);
}
