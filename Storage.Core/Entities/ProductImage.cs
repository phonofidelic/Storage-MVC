using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Core.Entities
{
    public class ProductImage
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }

        public required Image Image { get; set; }
        public required Product Product { get; set; }
    }
}
