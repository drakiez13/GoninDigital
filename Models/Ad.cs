using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class Ad
    {
        public Ad()
        {
            AdDetails = new HashSet<AdDetail>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }

        public virtual ICollection<AdDetail> AdDetails { get; set; }
    }
}
