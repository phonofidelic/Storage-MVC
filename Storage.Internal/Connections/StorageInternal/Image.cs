using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Storage.Core.Connections.StorageInternal;

public partial class Image
{
    [Key]
    public int Id { get; set; }

    public string Src { get; set; } = null!;

    public string AltText { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("ImageId")]
    [InverseProperty("Images")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
