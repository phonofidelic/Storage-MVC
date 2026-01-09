using System;

namespace Storage.Models;

public record ProductCreateDto
(
    string Name,
    decimal Price,
    decimal PurchasePrice,
    DateTime OrderDate,
    int CategoryId,
    string Shelf,
    int Count,
    string? Description
);