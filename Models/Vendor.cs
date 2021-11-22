using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Invoices = new HashSet<Invoice>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public byte ApprovalStatus { get; set; }
        public string Cover { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
