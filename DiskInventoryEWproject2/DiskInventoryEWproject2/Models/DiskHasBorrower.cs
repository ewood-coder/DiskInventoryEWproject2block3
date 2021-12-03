/*************************************************************************************
*  DATE:            NAME:       DESCRIPTION:
*  12-3-2021        Emma        Initial deployment of DiskHasBorrower model.
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventoryEWproject2.Models
{
    public partial class DiskHasBorrower
    {
        public int DiskHasBorrowerId { get; set; }
        [Required(ErrorMessage = "Please choose a disk/media type.")]
        public int? BorrowerId { get; set; }
        [Required(ErrorMessage = "Please choose a borrower.")]
        public int? CdId { get; set; }
        [Required(ErrorMessage = "Please enter a borrowed date.")]
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Disk Cd { get; set; }
    }
}
