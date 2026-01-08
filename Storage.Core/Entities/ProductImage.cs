using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Core.Entities
{
    public class ProductImage
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }

        public Image Image { get; set; }
        public Product Product { get; set; }
    }
}
