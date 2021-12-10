using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class Rating
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Value { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
