using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public string Value { get; set; }
        public DateTime Time { get; set; }

        public virtual Comment Parent { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
