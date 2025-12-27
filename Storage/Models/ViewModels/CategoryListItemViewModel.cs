using System;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models.ViewModels;

public class CategoryListItemViewModel
{
    public int Id { get; set; } = default!;

    [Display(Name = "Products")]
    public int ProductCount { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}
