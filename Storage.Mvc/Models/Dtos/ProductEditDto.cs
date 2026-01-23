using System;
using Storage.Core.Entities;
using Storage.Models.ViewModels;

namespace Storage.Models;

public record ProductEditDto
(
    int Id,
    string? Name,
    decimal? Price,
    decimal? PurchasePrice,
    DateTime? OrderDate,
    int? CategoryId,
    // Category? Category,
    string? Shelf,
    int? InventoryCount,
    string? Description
    // ImageInputViewModel? Image
);
