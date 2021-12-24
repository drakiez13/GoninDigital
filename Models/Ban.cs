using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class Ban
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public DateTime EndDate { get; set; }

        public virtual User IdNavigation { get; set; }
    }
}
