using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Core.Entities
{
    internal class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public int InventoryCount { get; set; }
        public string? Description { get; set; }
        public  int CategoryId { get; set; }

        // ToDo: Add Category navigational property
        public Category Category { get; set; } = default!;
    }
}
