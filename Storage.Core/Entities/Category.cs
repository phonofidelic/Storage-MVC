using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Core.Entities
{
    public class Category : BaseEntity
    {
        public  int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
