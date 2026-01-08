using Microsoft.EntityFrameworkCore;
using Storage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Persistence.Data
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions options) : base(options)
        {}
        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Image> Images { get; set; } = default!;

        public DbSet<ProductImage> ProductImages { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductImage>().HasKey(p => new {p.ImageId, p.ProductId});

            modelBuilder.Entity<Product>().Property("Price").HasPrecision(2);
            modelBuilder.Entity<Product>().Property("PurchasePrice").HasPrecision(2);
        }
    }
}
