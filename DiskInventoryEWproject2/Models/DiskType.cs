using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventoryEWproject2.Models
{
    public partial class DiskType
    {
        public DiskType()
        {
            Disks = new HashSet<Disk>();
        }

        public int DiskTypeId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Disk> Disks { get; set; }
    }
}
