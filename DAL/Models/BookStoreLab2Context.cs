using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class BookStoreLab2Context : DbContext
    {
        public BookStoreLab2Context()
        {
        }

        public BookStoreLab2Context(DbContextOptions<BookStoreLab2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Press> Presses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(local);Database=BookStoreLab2;Uid=sa;Password=thuytan123;TrustServerCertificate=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.City);

                entity.ToTable("Address");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.LocationName, "IX_Books_LocationName");

                entity.HasIndex(e => e.PressId, "IX_Books_PressId");

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.LocationNameNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.LocationName);

                entity.HasOne(d => d.Press)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PressId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
