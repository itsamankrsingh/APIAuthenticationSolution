using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIAuthentication.Models
{
    public partial class NetCoreAuthenticationContext : DbContext
    {
        public NetCoreAuthenticationContext()
        {
        }

        public NetCoreAuthenticationContext(DbContextOptions<NetCoreAuthenticationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUserMst> TblUserMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=NetCoreAuthentication;user=sa;password=sa@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUserMst>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.ToTable("tblUserMst");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
