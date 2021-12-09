using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventoryEWproject2.Models
{
    public partial class Disk
    {
        public Disk()
        {
            DiskHasArtists = new HashSet<DiskHasArtist>();
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
        }

        public int CdId { get; set; }
        [Required(ErrorMessage = "Please enter a disk/media name.")]
        public string? CdName { get; set; }
        [Required(ErrorMessage = "Please enter a release date.")]
        public DateTime? ReleaseDate { get; set; }
        [Required(ErrorMessage = "Please select a GENRE.")]
        public int? GenreId { get; set; }
        [Required(ErrorMessage = "Please select a DISK TYPE.")]
        public int? DiskTypeId { get; set; }
        [Required(ErrorMessage = "Please select a STATUS.")]
        public int? StatusId { get; set; }

        public virtual DiskType DiskType { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
