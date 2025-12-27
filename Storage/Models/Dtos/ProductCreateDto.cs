using System;

namespace Storage.Models;

public record ProductCreateDto
(
    string Name,
    int Price,
    DateTime OrderDate,
    int CategoryId,
    string Shelf,
    int Count,
    string? Description
);