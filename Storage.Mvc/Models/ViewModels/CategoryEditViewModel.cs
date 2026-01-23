using System;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models.ViewModels;

public class CategoryEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; } = default!;

    [StringLength(200)]
    public string? Description { get; set; }
}
