using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Core.Entities
{
    public class Image : BaseEntity
    {
        public int Id { get; set; }
        public string Src { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
    }
}
