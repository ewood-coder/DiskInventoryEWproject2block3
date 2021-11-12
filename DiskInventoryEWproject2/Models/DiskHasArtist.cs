using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventoryEWproject2.Models
{
    public partial class DiskHasArtist
    {
        public int DiskHasArtistId { get; set; }
        public int CdId { get; set; }
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Disk Cd { get; set; }
    }
}
