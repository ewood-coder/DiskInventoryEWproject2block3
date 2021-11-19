using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiskInventoryEWproject2.Models
{
    public partial class disk_inventoryEWContext : DbContext
    {
        public disk_inventoryEWContext()
        {
        }

        public disk_inventoryEWContext(DbContextOptions<disk_inventoryEWContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Disk> Disks { get; set; }
        public virtual DbSet<DiskHasArtist> DiskHasArtists { get; set; }
        public virtual DbSet<DiskHasBorrower> DiskHasBorrowers { get; set; }
        public virtual DbSet<DiskType> DiskTypes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ViewIndividualArtist> ViewIndividualArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=disk_inventoryEW;Trusted_Connection=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artist");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("artist_name");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.HasOne(d => d.ArtistType)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artist__artist_t__30F848ED");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.ToTable("artist_type");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("fname")
                    .IsFixedLength(true);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("lname")
                    .IsFixedLength(true);

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_num")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Disk>(entity =>
            {
                entity.HasKey(e => e.CdId)
                    .HasName("PK__disk__D551B5361E772614");

                entity.ToTable("disk");

                entity.Property(e => e.CdId).HasColumnName("cd_id");

                entity.Property(e => e.CdName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("cd_name")
                    .IsFixedLength(true);

                entity.Property(e => e.DiskTypeId).HasColumnName("disk_type_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.DiskType)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.DiskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__disk_type___2A4B4B5E");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__genre_id__2C3393D0");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__status_id__2B3F6F97");
            });

            modelBuilder.Entity<DiskHasArtist>(entity =>
            {
                entity.ToTable("disk_has_artist");

                entity.Property(e => e.DiskHasArtistId).HasColumnName("disk_has_artist_id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.CdId).HasColumnName("cd_id");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.DiskHasArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___artis__34C8D9D1");

                entity.HasOne(d => d.Cd)
                    .WithMany(p => p.DiskHasArtists)
                    .HasForeignKey(d => d.CdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___cd_id__33D4B598");
            });

            modelBuilder.Entity<DiskHasBorrower>(entity =>
            {
                entity.ToTable("disk_has_borrower");

                entity.Property(e => e.DiskHasBorrowerId).HasColumnName("disk_has_borrower_id");

                entity.Property(e => e.BorrowedDate)
                    .HasColumnType("date")
                    .HasColumnName("borrowed_date");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.CdId).HasColumnName("cd_id");

                entity.Property(e => e.ReturnedDate)
                    .HasColumnType("date")
                    .HasColumnName("returned_date");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___borro__398D8EEE");

                entity.HasOne(d => d.Cd)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.CdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___cd_id__3A81B327");
            });

            modelBuilder.Entity<DiskType>(entity =>
            {
                entity.ToTable("disk_type");

                entity.Property(e => e.DiskTypeId).HasColumnName("disk_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ViewIndividualArtist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Individual_Artist");

                entity.Property(e => e.ArtistId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("artist_id");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("artist_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
