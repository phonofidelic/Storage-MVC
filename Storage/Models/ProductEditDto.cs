using System;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Models;

public record ProductEditDto
(
    int Id,
    string? Name,
    int? Price,
    DateTime? OrderDate,
    int? CategoryId,
    Category? Category,
    string? Shelf,
    int? Count,
    string? Description,
    ImageInputViewModel? Image
);
