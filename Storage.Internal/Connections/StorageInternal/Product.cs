using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Storage.Core.Connections.StorageInternal;

[Index("CategoryId", Name = "IX_Products_CategoryId")]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Price { get; set; }

    public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal PurchasePrice { get; set; }

    public int InventoryCount { get; set; }

    public string Shelf { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Products")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
