using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Storage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Infrastructure.Data
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

            // https://stackoverflow.com/a/224866
            modelBuilder.Entity<Product>().Property("Price").HasPrecision(19,4);
            modelBuilder.Entity<Product>().Property("PurchasePrice").HasPrecision(19,4);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}
