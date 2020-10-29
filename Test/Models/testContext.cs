using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Test.Models
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source =(localdb)\\MSSQLLocalDB;Initial Catalog=test;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.Adminid)
                    .HasName("PK__admins__AD040D7E05F3A7A6");

                entity.ToTable("admins");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__admins__AB6E61647A1EFFE7")
                    .IsUnique();

                entity.HasIndex(e => e.Pno)
                    .HasName("UQ__admins__DD37C1488CA972C5")
                    .IsUnique();

                entity.Property(e => e.Adminid)
                    .HasColumnName("adminid")
                    .HasMaxLength(11)
                    .IsUnicode(false).ValueGeneratedOnAdd()
                    .HasComputedColumnSql("('A'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pno)
                    .HasColumnName("pno")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
