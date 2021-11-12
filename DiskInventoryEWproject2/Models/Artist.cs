using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventoryEWproject2.Models
{
    public partial class Artist
    {
        public Artist()
        {
            DiskHasArtists = new HashSet<DiskHasArtist>();
        }

        public int ArtistId { get; set; }
        public int ArtistTypeId { get; set; }
        public string ArtistName { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
    }
}
