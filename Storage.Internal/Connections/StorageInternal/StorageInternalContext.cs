using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Storage.Core.Connections.StorageInternal;

public partial class StorageInternalContext : DbContext
{
    public StorageInternalContext(DbContextOptions<StorageInternalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasMany(d => d.Products).WithMany(p => p.Images)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductImage",
                    r => r.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    l => l.HasOne<Image>().WithMany().HasForeignKey("ImageId"),
                    j =>
                    {
                        j.HasKey("ImageId", "ProductId");
                        j.ToTable("ProductImages");
                        j.HasIndex(new[] { "ProductId" }, "IX_ProductImages_ProductId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
