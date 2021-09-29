using System;
using System.Collections.Generic;

#nullable disable

namespace GoninDigital.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
