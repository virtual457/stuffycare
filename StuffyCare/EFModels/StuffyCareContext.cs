using System;
using System.Configuration;
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

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Authvendors> Authvendors { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Otp> Otp { get; set; }
        public virtual DbSet<Pets> Pets { get; set; }
        public virtual DbSet<Reveiws> Reveiws { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vendoritems> Vendoritems { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }
        public virtual DbSet<Vendorservices> Vendorservices { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["stuffycaredb"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Addressid)
                    .HasName("PK__address__26A01585861054E8");

                entity.ToTable("address");

                entity.Property(e => e.Addressid)
                    .HasColumnName("addressid")
                    .HasMaxLength(12)
                    .IsUnicode(false).ValueGeneratedOnAdd()
                    .HasComputedColumnSql("('UA'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Addresslineone)
                    .HasColumnName("addresslineone")
                    .IsUnicode(false);

                entity.Property(e => e.Addresslinetwo)
                    .HasColumnName("addresslinetwo")
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

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Isshippingaddress).HasColumnName("isshippingaddress");

                entity.Property(e => e.Landmark)
                    .HasColumnName("landmark")
                    .IsUnicode(false);

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
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__address__userid__39525BD6");
            });

            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.Adminid)
                    .HasName("PK__admins__AD040D7E0BE13EA3");

                entity.ToTable("admins");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__admins__AB6E61648FB73933")
                    .IsUnique();

                entity.HasIndex(e => e.Pno)
                    .HasName("UQ__admins__DD37C1482C0A1253")
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
                    .IsUnicode(false).ValueGeneratedOnAdd()
                    .HasComputedColumnSql("('Apt'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ishomeservice).HasColumnName("ishomeservice");

                entity.Property(e => e.Ispaid).HasColumnName("ispaid");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Petid)
                    .HasColumnName("petid")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("phonenumber")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Servicedatetime)
                    .HasColumnName("servicedatetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Servicefees).HasColumnName("servicefees");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Vendorid)
                    .HasColumnName("vendorid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Petid)
                    .HasConstraintName("FK__appointme__petid__11446A7C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__appointme__useri__10504643");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Vendorid)
                    .HasConstraintName("FK__appointme__vendo__12388EB5");
            });

            modelBuilder.Entity<Authvendors>(entity =>
            {
                entity.HasKey(e => e.Authvendorsid)
                    .HasName("PK__authvend__6C0A64B20EE58D6D");

                entity.ToTable("authvendors");

                entity.Property(e => e.Authvendorsid)
                    .HasColumnName("authvendorsid")
                    .HasMaxLength(14)
                    .IsUnicode(false).ValueGeneratedOnAdd()
                    .HasComputedColumnSql("('AVID'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

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
                    .HasConstraintName("FK__cart__itemid__3675EF2B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__cart__userid__3581CAF2");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.Itemid)
                    .HasName("PK__items__56A22C9294CB7C0A");

                entity.ToTable("items");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(11)
                    .IsUnicode(false).ValueGeneratedOnAdd()
                    .HasComputedColumnSql("('I'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Addedby)
                    .HasColumnName("addedby")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Authorizedby)
                    .HasColumnName("authorizedby")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Authorizedstatus)
                    .HasColumnName("authorizedstatus")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Breadth).HasColumnName("breadth");

                entity.Property(e => e.Cancelationpolicy)
                    .HasColumnName("cancelationpolicy")
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Commissionfor)
                    .HasColumnName("commissionfor")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Commissionmode)
                    .HasColumnName("commissionmode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Crosssells)
                    .HasColumnName("crosssells")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Deletedstatus)
                    .HasColumnName("deletedstatus")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Exchangepolicy)
                    .HasColumnName("exchangepolicy")
                    .IsUnicode(false);

                entity.Property(e => e.Foranimal)
                    .HasColumnName("foranimal")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Mililitres)
                    .HasColumnName("mililitres")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Moa).HasColumnName("moa");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Packsizeingrams)
                    .HasColumnName("packsizeingrams")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .IsUnicode(false);

                entity.Property(e => e.Policylabel)
                    .HasColumnName("policylabel")
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Processingtime)
                    .HasColumnName("processingtime")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Refundpolicy)
                    .HasColumnName("refundpolicy")
                    .IsUnicode(false);

                entity.Property(e => e.Saleprice).HasColumnName("saleprice");

                entity.Property(e => e.Shippingclass)
                    .HasColumnName("shippingclass")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Shippingpolicy)
                    .HasColumnName("shippingpolicy")
                    .IsUnicode(false);

                entity.Property(e => e.Sku)
                    .HasColumnName("sku")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Storename)
                    .HasColumnName("storename")
                    .IsUnicode(false);

                entity.Property(e => e.Subcategory)
                    .HasColumnName("subcategory")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Subdescription)
                    .HasColumnName("subdescription")
                    .IsUnicode(false);

                entity.Property(e => e.Unitcount).HasColumnName("unitcount");

                entity.Property(e => e.Upsells)
                    .HasColumnName("upsells")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.AuthorizedbyNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Authorizedby)
                    .HasConstraintName("FK__items__authorize__1DAA4161");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("PK__orders__080E377560C30D47");

                entity.ToTable("orders");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasMaxLength(11)
                    .IsUnicode(false).ValueGeneratedOnAdd()
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
                    .HasConstraintName("FK__orders__itemid__226EF67E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__orders__userid__217AD245");
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.HasKey(e => e.Phoneno)
                    .HasName("PK__otp__960E13C608E4DCB2");

                entity.ToTable("otp");

                entity.Property(e => e.Phoneno)
                    .HasColumnName("phoneno")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Otpstring)
                    .HasColumnName("otpstring")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pets>(entity =>
            {
                entity.HasKey(e => e.Petid)
                    .HasName("PK__pets__DDFD44A1DED08194");

                entity.ToTable("pets");

                entity.Property(e => e.Petid)
                    .HasColumnName("petid")
                    .HasMaxLength(13)
                    .IsUnicode(false).ValueGeneratedOnAdd()
                    .HasComputedColumnSql("('PET'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Allergies)
                    .HasColumnName("allergies")
                    .IsUnicode(false);

                entity.Property(e => e.Breed)
                    .HasColumnName("breed")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Moreinfo)
                    .HasColumnName("moreinfo")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__pets__userid__0C7FB55F");
            });

            modelBuilder.Entity<Reveiws>(entity =>
            {
                entity.HasKey(e => e.Reveiwid)
                    .HasName("PK__reveiws__A18903D31F1E8BD0");

                entity.ToTable("reveiws");

                entity.Property(e => e.Reveiwid)
                    .HasColumnName("reveiwid")
                    .HasMaxLength(11)
                    .IsUnicode(false).ValueGeneratedOnAdd()
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
                    .HasConstraintName("FK__reveiws__itemid__2DE0A92A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reveiws)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__reveiws__userid__2CEC84F1");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__users__CBA1B2578B779869");

                entity.ToTable("users");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(11)
                    .IsUnicode(false).ValueGeneratedOnAdd()
                    .HasComputedColumnSql("('U'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

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

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .IsUnicode(false);

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LoyaltyPoints).HasColumnName("loyaltyPoints");

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
                    .HasName("PK__vendorit__56A22C92583FDC61");

                entity.ToTable("vendoritems");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasMaxLength(14)
                    .IsUnicode(false).ValueGeneratedOnAdd()
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

            modelBuilder.Entity<Vendors>(entity =>
            {
                entity.HasKey(e => e.Vendorid)
                    .HasName("PK__vendors__EC64C0BBF0B8A8C1");

                entity.ToTable("vendors");

                entity.HasIndex(e => e.Email)
                    .HasName("ven_duplicate_email")
                    .IsUnique();

                entity.HasIndex(e => e.Pno)
                    .HasName("ven_duplicate_phno")
                    .IsUnique();

                entity.Property(e => e.Vendorid)
                    .HasColumnName("vendorid")
                    .HasMaxLength(11)
                    .IsUnicode(false).ValueGeneratedOnAdd()
                    .HasComputedColumnSql("('V'+right('0000000000'+CONVERT([varchar](10),[id]),(10)))");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Frifrom)
                    .HasColumnName("frifrom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Frito)
                    .HasColumnName("frito")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Homeservice).HasColumnName("homeservice");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Isauthorized).HasColumnName("isauthorized");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Issellingitem).HasColumnName("issellingitem");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .IsUnicode(false);

                entity.Property(e => e.Monfrom)
                    .HasColumnName("monfrom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Monto)
                    .HasColumnName("monto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .IsUnicode(false);

                entity.Property(e => e.Photoidproof)
                    .HasColumnName("photoidproof")
                    .IsUnicode(false);

                entity.Property(e => e.Pno)
                    .HasColumnName("pno")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Satfrom)
                    .HasColumnName("satfrom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Satto)
                    .HasColumnName("satto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .HasColumnName("storeName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sunfrom)
                    .HasColumnName("sunfrom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sunto)
                    .HasColumnName("sunto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Thurfrom)
                    .HasColumnName("thurfrom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Thurto)
                    .HasColumnName("thurto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tuefrom)
                    .HasColumnName("tuefrom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tueto)
                    .HasColumnName("tueto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Wedfrom)
                    .HasColumnName("wedfrom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Wedto)
                    .HasColumnName("wedto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Yearsofexperience).HasColumnName("yearsofexperience");
            });

            modelBuilder.Entity<Vendorservices>(entity =>
            {
                entity.ToTable("vendorservices");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Vendorid)
                    .HasColumnName("vendorid")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Vendorservices)
                    .HasForeignKey(d => d.Vendorid)
                    .HasConstraintName("FK__vendorser__vendo__07BB0042");
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
                    .HasConstraintName("FK__wishlist__itemid__32A55E47");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__wishlist__userid__31B13A0E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
