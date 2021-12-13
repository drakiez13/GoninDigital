using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoninDigital.Utils
{
    static class Constants
    {
        public enum UserType
        {
            ADMIN = 1,
            VENDOR = 2,
            CUSTOMER = 3,
        }
        public enum ApprovalStatus
        {
            APPROVED = 1,
            REJECTED = 2, //EQUAL TO REMOVE
            WAITING = 0,
        }
        public enum GenderType
        {
            NOTKNOWN = 0,
            MAKE = 1,
            FEMALE = 2,
            NOTAPPLICABLE = 9
        }

        public enum ProductStatus
        {
            CREATED = 1,
            UPDATED = 2,
            ACCEPTED =3,
            REMOVED = 4,
        }

        public enum InvoiceStatus
        {
            CREATED = 1,
            ACCEPTED = 2,
            REFUSED = 3,
            DELIVERED = 4,
            CANCELED = 5
        }
        //public enum ApprovalStatus
        //{
        //    REQUEST = 0,
        //    ACTIVE = 1,
        //    CLOSED = 2
        //}
    }
}
