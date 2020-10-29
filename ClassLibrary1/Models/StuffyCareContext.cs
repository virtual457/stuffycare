using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClassLibrary1.Models
{
    public partial class StuffyCareContext : DbContext
    {
        public StuffyCareContext()
        {
        }

        public StuffyCareContext(DbContextOptions<StuffyCareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Authvendors> Authvendors { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vendoritems> Vendoritems { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StuffyCare;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.Adminid)
                    .HasName("PK__admins__AD040D7E917D96A1");

                entity.ToTable("admins");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__admins__AB6E6164C6C1AF03")
                    .IsUnique();

                entity.HasIndex(e => e.Pno)
                    .HasName("UQ__admins__DD37C148447D7332")
                    .IsUnique();

                entity.Property(e => e.Adminid)
                    .HasColumnName("adminid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
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

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.HasKey(e => e.Aptid)
                    .HasName("PK__appointm__EF39280FA3CC0D81");

                entity.ToTable("appointments");

                entity.Property(e => e.Aptid)
                    .HasColumnName("aptid")
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('Apt'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dt)
                    .HasColumnName("dt")
                    .HasColumnType("date");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pno)
                    .HasColumnName("pno")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Servicetype)
                    .HasColumnName("servicetype")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tm).HasColumnName("tm");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__appointme__useri__04F074D4");
            });

            modelBuilder.Entity<Authvendors>(entity =>
            {
                entity.HasKey(e => e.Authvendorsid)
                    .HasName("PK__authvend__6C0A64B2511F23A0");

                entity.ToTable("authvendors");

                entity.Property(e => e.Authvendorsid)
                    .HasColumnName("authvendorsid")
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('VID'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

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
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.Itemid)
                    .HasName("PK__items__56A22C923AA84DA7");

                entity.ToTable("items");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('I'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Moa).HasColumnName("moa");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Own)
                    .HasColumnName("own")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Saleprice).HasColumnName("saleprice");

                entity.Property(e => e.Sku)
                    .HasColumnName("sku")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("PK__orders__080E3775F05DC9C6");

                entity.ToTable("orders");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('O'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Dt)
                    .HasColumnName("dt")
                    .HasColumnType("date");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Method)
                    .HasColumnName("method")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Itemid)
                    .HasConstraintName("FK__orders__itemid__161B00D6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__orders__userid__1526DC9D");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__users__CBA1B25758DE24F6");

                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("usr_dup_email")
                    .IsUnique();

                entity.HasIndex(e => e.Pno)
                    .HasName("usr_dup_phno")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('U'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

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

            modelBuilder.Entity<Vendoritems>(entity =>
            {
                entity.HasKey(e => e.Itemid)
                    .HasName("PK__vendorit__56A22C92D555B333");

                entity.ToTable("vendoritems");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('VIID'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Moa).HasColumnName("moa");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Own)
                    .HasColumnName("own")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Saleprice).HasColumnName("saleprice");

                entity.Property(e => e.Sku)
                    .HasColumnName("sku")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vendors>(entity =>
            {
                entity.HasKey(e => e.Vendorid)
                    .HasName("PK__vendors__EC64C0BB1B121889");

                entity.ToTable("vendors");

                entity.HasIndex(e => e.Email)
                    .HasName("ven_dup_email")
                    .IsUnique();

                entity.HasIndex(e => e.Pno)
                    .HasName("ven_dup_phno")
                    .IsUnique();

                entity.Property(e => e.Vendorid)
                    .HasColumnName("vendorid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('V'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(200)
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
