using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class AdDetail
    {
        public int ProductId { get; set; }
        public int AdId { get; set; }

        public virtual Ad Ad { get; set; }
        public virtual Product Product { get; set; }
    }
}
