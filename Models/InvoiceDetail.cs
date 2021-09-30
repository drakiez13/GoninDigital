using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class InvoiceDetail
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveriedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}
