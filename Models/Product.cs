using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class Product
    {
        public Product()
        {
            AdDetails = new HashSet<AdDetail>();
            Carts = new HashSet<Cart>();
            Favorites = new HashSet<Favorite>();
            InvoiceDetails = new HashSet<InvoiceDetail>();
            ProductSpecDetails = new HashSet<ProductSpecDetail>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Origin { get; set; }
        public long Price { get; set; }
        public long OriginPrice { get; set; }
        public int? StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int BrandId { get; set; }
        public string Image { get; set; }
        public byte Rating { get; set; }
        public int NRating { get; set; }
        public int Available { get; set; }
        public byte? New { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ProductCategory Category { get; set; }
        public virtual ProductStatus Status { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<AdDetail> AdDetails { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<ProductSpecDetail> ProductSpecDetails { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
