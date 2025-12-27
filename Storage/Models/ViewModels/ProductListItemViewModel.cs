using System;
using System.ComponentModel.DataAnnotations;
using Storage.Models.Entities;

namespace Storage.Models.ViewModels;

public class ProductListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    [DataType(DataType.Currency)]
    public int Price { get; set; }

    [Display(Name = "Order Date")]
    [DataType(DataType.Date)]
    public DateTime OrderDate { get; set; } = default!;

    public Category Category { get; set; } = default!;

    public string Shelf { get; set; } = default!;

    public int Count { get; set; }

    public string? Description { get; set; }
}
