using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class ProductImage
    {
        public ProductImage()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
