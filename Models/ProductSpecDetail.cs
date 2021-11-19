using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class ProductSpecDetail
    {
        public int ProductId { get; set; }
        public int SpecId { get; set; }
        public string Value { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductSpec Spec { get; set; }
    }
}
