using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WoofyTailsDALLayer.EFModels
{
    public partial class WoofyTailsDBContext : DbContext
    {
        public WoofyTailsDBContext()
        {
        }

        public WoofyTailsDBContext(DbContextOptions<WoofyTailsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Vendorservice> Vendorservices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WoofyTailsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Aptid)
                    .HasName("PK__appointm__EF39280F7D6C49C6");

                entity.ToTable("appointments");

                entity.Property(e => e.Aptid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("aptid");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Ishomeservice).HasColumnName("ishomeservice");

                entity.Property(e => e.Ispaid).HasColumnName("ispaid");

                entity.Property(e => e.Message)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.Petid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("petid");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("phonenumber");

                entity.Property(e => e.Servicedatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("servicedatetime");

                entity.Property(e => e.Servicefees).HasColumnName("servicefees");

                entity.Property(e => e.Userid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.Vendorid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("vendorid");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Petid)
                    .HasConstraintName("FK__appointme__petid__178D7CA5");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__appointme__useri__1699586C");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Vendorid)
                    .HasConstraintName("FK__appointme__vendo__1881A0DE");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("items");

                entity.Property(e => e.Itemid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("itemid");

                entity.Property(e => e.Addedby)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("addedby");

                entity.Property(e => e.Authorizedby)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("authorizedby");

                entity.Property(e => e.Authorizedstatus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("authorizedstatus");

                entity.Property(e => e.Breadth).HasColumnName("breadth");

                entity.Property(e => e.Cancelationpolicy)
                    .IsUnicode(false)
                    .HasColumnName("cancelationpolicy");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Commissionfor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("commissionfor");

                entity.Property(e => e.Commissionmode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("commissionmode");

                entity.Property(e => e.Crosssells)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("crosssells");

                entity.Property(e => e.Deletedstatus).HasColumnName("deletedstatus");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Exchangepolicy)
                    .IsUnicode(false)
                    .HasColumnName("exchangepolicy");

                entity.Property(e => e.Foranimal)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("foranimal");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Mililitres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mililitres");

                entity.Property(e => e.Moa).HasColumnName("moa");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Packsizeingrams)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("packsizeingrams");

                entity.Property(e => e.Photo)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Policylabel)
                    .IsUnicode(false)
                    .HasColumnName("policylabel");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Processingtime)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("processingtime");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Refundpolicy)
                    .IsUnicode(false)
                    .HasColumnName("refundpolicy");

                entity.Property(e => e.Saleprice).HasColumnName("saleprice");

                entity.Property(e => e.Shippingclass)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("shippingclass");

                entity.Property(e => e.Shippingpolicy)
                    .IsUnicode(false)
                    .HasColumnName("shippingpolicy");

                entity.Property(e => e.Sku)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sku");

                entity.Property(e => e.Storename)
                    .IsUnicode(false)
                    .HasColumnName("storename");

                entity.Property(e => e.Subcategory)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("subcategory");

                entity.Property(e => e.Subdescription)
                    .IsUnicode(false)
                    .HasColumnName("subdescription");

                entity.Property(e => e.Unitcount).HasColumnName("unitcount");

                entity.Property(e => e.Upsells)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("upsells");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.AddedbyNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Addedby)
                    .HasConstraintName("FK__items__addedby__1B5E0D89");

                entity.HasOne(d => d.AuthorizedbyNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Authorizedby)
                    .HasConstraintName("FK__items__authorize__1C5231C2");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("pets");

                entity.Property(e => e.Petid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("petid");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Allergies)
                    .IsUnicode(false)
                    .HasColumnName("allergies");

                entity.Property(e => e.Breed)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("breed");

                entity.Property(e => e.Gender)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Moreinfo)
                    .IsUnicode(false)
                    .HasColumnName("moreinfo");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Size)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("size");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Userid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__pets__userid__13BCEBC1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("userId");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emailId");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__users__roleId__0B27A5C0");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendors");

                entity.Property(e => e.Vendorid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("vendorid");

                entity.Property(e => e.Authorizedby)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("authorizedby");

                entity.Property(e => e.Authorizedstatus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("authorizedstatus");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Frifrom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("frifrom");

                entity.Property(e => e.Frito)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("frito");

                entity.Property(e => e.Homeservice).HasColumnName("homeservice");

                entity.Property(e => e.Isauthorized).HasColumnName("isauthorized");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Issellingitem).HasColumnName("issellingitem");

                entity.Property(e => e.Location)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Monfrom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("monfrom");

                entity.Property(e => e.Monto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("monto");

                entity.Property(e => e.Photo)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Photoidproof)
                    .IsUnicode(false)
                    .HasColumnName("photoidproof");

                entity.Property(e => e.Satfrom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("satfrom");

                entity.Property(e => e.Satto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("satto");

                entity.Property(e => e.StoreName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("storeName");

                entity.Property(e => e.Sunfrom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sunfrom");

                entity.Property(e => e.Sunto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sunto");

                entity.Property(e => e.Thurfrom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("thurfrom");

                entity.Property(e => e.Thurto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("thurto");

                entity.Property(e => e.Tuefrom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tuefrom");

                entity.Property(e => e.Tueto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tueto");

                entity.Property(e => e.Wedfrom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wedfrom");

                entity.Property(e => e.Wedto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wedto");

                entity.Property(e => e.Yearsofexperience).HasColumnName("yearsofexperience");
            });

            modelBuilder.Entity<Vendorservice>(entity =>
            {
                entity.ToTable("vendorservices");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nameofservice)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nameofservice");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Vendorid)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("vendorid");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Vendorservices)
                    .HasForeignKey(d => d.Vendorid)
                    .HasConstraintName("FK__vendorser__vendo__10E07F16");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
