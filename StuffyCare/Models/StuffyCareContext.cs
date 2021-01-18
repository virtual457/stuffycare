using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StuffyCare.EFModels
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
        public virtual DbSet<BillingDetails> BillingDetails { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pets> Pets { get; set; }
        public virtual DbSet<Reveiws> Reveiws { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vendoritems> Vendoritems { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }

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
                    .HasName("PK__admins__AD040D7E4940DB97");

                entity.ToTable("admins");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__admins__AB6E61642CF59EC2")
                    .IsUnique();

                entity.HasIndex(e => e.Pno)
                    .HasName("UQ__admins__DD37C14870D87BF7")
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
                entity.ToTable("appointments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Aptid)
                    .HasColumnName("aptid")
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('Apt'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Dt)
                    .HasColumnName("dt")
                    .HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Petid)
                    .HasColumnName("petid")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Pno)
                    .HasColumnName("pno")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Servicetype)
                    .HasColumnName("servicetype")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Petid)
                    .HasConstraintName("FK__appointme__petid__1E9C0395");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__appointme__useri__1DA7DF5C");
            });

            modelBuilder.Entity<Authvendors>(entity =>
            {
                entity.HasKey(e => e.Authvendorsid)
                    .HasName("PK__authvend__6C0A64B2182DDC93");

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

            modelBuilder.Entity<BillingDetails>(entity =>
            {
                entity.HasKey(e => e.Reveiwid)
                    .HasName("PK__billing___A18903D30344BAB8");

                entity.ToTable("billing_details");

                entity.Property(e => e.Reveiwid)
                    .HasColumnName("reveiwid")
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('UA'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pincode)
                    .HasColumnName("pincode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BillingDetails)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__billing_d__useri__4A7A85D3");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Itemid)
                    .HasConstraintName("FK__cart__itemid__479E1928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__cart__userid__46A9F4EF");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.Itemid)
                    .HasName("PK__items__56A22C92FCF1F157");

                entity.ToTable("items");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('I'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Breadth).HasColumnName("breadth");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Length).HasColumnName("length");

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

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("PK__orders__080E37759F2F5DCC");

                entity.ToTable("orders");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('O'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Dt)
                    .HasColumnName("dt")
                    .HasColumnType("datetime");

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

                entity.Property(e => e.SrOrderid).HasColumnName("sr_orderid");

                entity.Property(e => e.SrShipmentid).HasColumnName("sr_shipmentid");

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
                    .HasConstraintName("FK__orders__itemid__2FC68F97");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__orders__userid__2ED26B5E");
            });

            modelBuilder.Entity<Pets>(entity =>
            {
                entity.HasKey(e => e.Petid)
                    .HasName("PK__pets__DDFD44A1422D5FF3");

                entity.ToTable("pets");

                entity.Property(e => e.Petid)
                    .HasColumnName("petid")
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('PET'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__pets__userid__19D74E78");
            });

            modelBuilder.Entity<Reveiws>(entity =>
            {
                entity.HasKey(e => e.Reveiwid)
                    .HasName("PK__reveiws__A18903D367E965DE");

                entity.ToTable("reveiws");

                entity.Property(e => e.Reveiwid)
                    .HasColumnName("reveiwid")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('R'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Dt)
                    .HasColumnName("dt")
                    .HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .IsUnicode(false);

                entity.Property(e => e.Stars).HasColumnName("stars");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Reveiws)
                    .HasForeignKey(d => d.Itemid)
                    .HasConstraintName("FK__reveiws__itemid__3F08D327");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reveiws)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__reveiws__userid__3E14AEEE");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__users__CBA1B257BFA73F48");

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
                    .HasName("PK__vendorit__56A22C922DFBFC18");

                entity.ToTable("vendoritems");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('VIID'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Breadth).HasColumnName("breadth");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Moa).HasColumnName("moa");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Own)
                    .HasColumnName("own")
                    .HasMaxLength(11)
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

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.OwnNavigation)
                    .WithMany(p => p.Vendoritems)
                    .HasForeignKey(d => d.Own)
                    .HasConstraintName("FK__vendoritems__own__385BD598");
            });

            modelBuilder.Entity<Vendors>(entity =>
            {
                entity.HasKey(e => e.Vendorid)
                    .HasName("PK__vendors__EC64C0BB20083AFB");

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

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("wishlist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.Itemid)
                    .HasConstraintName("FK__wishlist__itemid__43CD8844");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__wishlist__userid__42D9640B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
