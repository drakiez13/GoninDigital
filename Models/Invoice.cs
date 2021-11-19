using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VendorId { get; set; }
        public decimal Value { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public virtual User Customer { get; set; }
        public virtual InvoiceStatus Status { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
