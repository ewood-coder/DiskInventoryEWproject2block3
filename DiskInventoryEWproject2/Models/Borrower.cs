using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventoryEWproject2.Models
{
    public partial class Borrower
    {
        public Borrower()
        {
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
        }

        public int BorrowerId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string PhoneNum { get; set; }

        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
