using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventoryEWproject2.Models
{
    public partial class Artist
    {
        public Artist()
        {
            DiskHasArtists = new HashSet<DiskHasArtist>();
        }

        [Required(ErrorMessage = "Please enter an artist name.")]
        public string? ArtistName { get; set; }
        public int ArtistId { get; set; }
        [Required(ErrorMessage = "Please select an artist type.")]
        public int? ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
    }
}
