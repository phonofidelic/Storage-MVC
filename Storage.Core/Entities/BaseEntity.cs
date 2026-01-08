using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Core.Entities
{
    internal abstract class BaseEntity
    {
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
