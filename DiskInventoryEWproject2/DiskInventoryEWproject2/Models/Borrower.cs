/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  11-19-2021       Emma        Initial deployment of BORROWER controller & index view.
*  12-3-2021        Emma        Added [Required] to public ints.
*************************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Lname { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string PhoneNum { get; set; }

        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
