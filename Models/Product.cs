using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            InvoiceDetails = new HashSet<InvoiceDetail>();
            Purchaseds = new HashSet<Purchased>();
        }

        public int Id { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Unit { get; set; }
        public string Origin { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public byte? DiscountRate { get; set; }
        public bool ApprovalStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int BrandId { get; set; }
        public byte[] Image { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ProductCategory Category { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Purchased> Purchaseds { get; set; }
    }
}
