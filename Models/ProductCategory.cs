using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductSpecs = new HashSet<ProductSpec>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductSpec> ProductSpecs { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
