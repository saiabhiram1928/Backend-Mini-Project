using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Contexts
{
    public class VideoStoreContext:DbContext
    {
        public VideoStoreContext(DbContextOptions options) : base(options) { }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Permanent> Permanents { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User dummy data
            using (var hmac = new HMACSHA512())
            {
                modelBuilder.Entity<User>().HasData(

                new User { Uid = 101, FirstName = "test1", LastName = "test", Email = "test1@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt=hmac.Key,Verified = true},
                new User { Uid = 102, FirstName = "test2", LastName = "test", Email = "test2@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")), Salt=hmac.Key,Verified = true}  ,
                new User { Uid = 100, FirstName = "admin", LastName = "admin", Email = "admin@gmail.com", Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin")), Salt=hmac.Key,Verified = true , Role = Role.Admin}
                
                );
            }

            //Video Dummy Data
            modelBuilder.Entity<Video>().HasData(
                 new Video { Id = 1, Tittle = "Action Movie", Genre = Genre.Action, Director = "Director 1", ReleaseDate = DateTime.Now.AddYears(-1), Price = 19.99f , Description = "At Mr. Rad's Warehouse, the best hip-hop crews in Los Angeles compete for money and respect." },
                new Video { Id = 2, Tittle = "Comedy Movie", Genre = Genre.Comedy, Director = "Director 2", ReleaseDate = DateTime.Now.AddYears(-2), Price = 14.99f ,Description= "A basketball player's father must try to convince him to go to a college so he can get a shorter sentence." }
                );
            //Inventory dummy data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { VideoId = 1, Stock = 10, LastUpdate = DateTime.Now.AddDays(-1) },
                new Inventory { VideoId = 2, Stock = 5, LastUpdate = DateTime.Now.AddDays(-2) }
                );
            //Cart Dummy data
            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = 1, CustomerId = 101, TotalPrice = 19.99f },
                new Cart { Id = 2, CustomerId = 102, TotalPrice = 29.98f }
                );

            //CartItems Dummy data
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { Id = 1, CartId = 1, VideoId = 1, Qty = 1, Price = 19.99f },
                new CartItem { Id = 2, CartId = 2, VideoId = 2, Qty = 2, Price = 29.98f }
                );
            //Order dummy data
            modelBuilder.Entity<Order>().HasData(
                    new Order
                    {
                        Id = 1,
                        CustomerId =101,
                        OrderedDate = DateTime.Now.AddDays(-10),
                        ExpectedDeliveryDate = DateTime.Now.AddDays(5),
                        RealDeliveredDate = null,
                        DeliveryAddressId = 1,
                        RentalOrPermanent = "Rental",
                        PaymentDone = true,
                       
                    },
                    new Order
                    {
                        Id = 2,
                        CustomerId =102,
                        OrderedDate = DateTime.Now.AddDays(-20),
                        ExpectedDeliveryDate = DateTime.Now.AddDays(10),
                        RealDeliveredDate = null,
                        DeliveryAddressId = 2,
                        RentalOrPermanent= "Permanent",
                        PaymentDone = false
                    }
               );
            //OrderItems dummy data
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, VideoId = 1, Qty = 1, Price = 19.99f },
                new OrderItem { Id = 2, OrderId = 2, VideoId = 2, Qty = 2, Price = 29.98f }
                );
            //Address dummy data
            modelBuilder.Entity<Address>().HasData(
                new Address { Id = 1, Area = "Downtown", City = "CityA", State = "StateA", Zipcode = 12345, PrimaryAdress = true, CustomerId = 101 },
                new Address { Id = 2, Area = "Uptown", City = "CityB", State = "StateB", Zipcode = 54321, PrimaryAdress = false, CustomerId = 102 }
                );
            //Rental Dummy data
            modelBuilder.Entity<Rental>().HasData(
                 new Rental { Id = 1, OrderId = 1, RentDate = DateTime.Now.AddDays(-9), DueDate = DateTime.Now.AddDays(1), ReturnDate = DateTime.Now, TotalQty = 1, TotalPrice = 19.99f, LateFee = 0 }
           
                );
            //Permanent dummy data
            modelBuilder.Entity<Permanent>().HasData(
               new Permanent { Id = 1, OrderId = 2, TotalQty = 2, TotalPrice = 29.98f }
                );
            
            //Customer dummy data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Uid = 101, MembershipType = MembershipType.Basic },
                new Customer { Uid = 102, MembershipType = MembershipType.Premium },
                new Customer { Uid = 100, MembershipType = MembershipType.Premium }
                );

            // Ensure TransactionId is required if necessary
            modelBuilder.Entity<Payment>(entity =>
            {
                
               entity.Property(e => e.TransactionId)
                      .IsRequired();
                entity.HasIndex(e => e.TransactionId)
                      .IsUnique();

            });

            //Email is unique
            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();
            //Video Tittle is unique
            modelBuilder.Entity<Video>()
               .HasIndex(u => u.Tittle)
               .IsUnique();

            // Video - Genre as Enum
            modelBuilder.Entity<Video>()
                .Property(v => v.Genre)
                .HasConversion<string>();
            
            //Order-PaymentType as Enum
            modelBuilder.Entity<Order>()
                .Property(o => o.PaymentType)
                .HasConversion<string>();
            
            //Order-OrderStatusStatus as Enum
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderStatus)
                .HasConversion<string>();

            // User - Role as Enum
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            // Customer - MembershipType as Enum
            modelBuilder.Entity<Customer>()
                .Property(c => c.MembershipType)
                .HasConversion<string>();


            // Inventory && Video - Foreign Key VideoId 1-1
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Video)
                .WithOne() 
                .HasForeignKey<Inventory>(i => i.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer && User - Foreign Key Uid 1-1
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne() 
                .HasForeignKey<Customer>(c => c.Uid)
                .OnDelete(DeleteBehavior.Restrict);

            // Address && Customer - Foreign Key CustomerId 1-m
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Address)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cart && Customer - Foreign Key CustomerId 1-1
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithOne() 
                .HasForeignKey<Cart>(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // CartItems && Cart - Foreign Key CartId 1-m
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            //Create a compiste Key with cartId and VideoId
               modelBuilder.Entity<CartItem>()
                .HasIndex(ci => new { ci.CartId, ci.VideoId })
                .IsUnique();
            
            // CartItems && Video - Foreign Key VideoId 1-1
            modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Video)
            .WithMany()
            .HasForeignKey(ci => ci.VideoId)
            .OnDelete(DeleteBehavior.Restrict);

            // Order && Customer - Foreign Key CustomerId 1-m
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order && Address - Foreign Key DeliveryAddressId 1-1
            modelBuilder.Entity<Order>()
            .HasOne(o => o.DeliveryAddress)
            .WithMany()
            .HasForeignKey(o => o.DeliveryAddressId)
            .OnDelete(DeleteBehavior.Restrict);
            

            // OrderItems && Order - Foreign Key OrderId 1-m
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
           

            // OrderItems && Video - Foreign Key VideoId 1-m
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Video)
                .WithMany()
                .HasForeignKey(oi => oi.VideoId)
                .OnDelete(DeleteBehavior.Restrict);
           
            // Payment && Order - Foreign Key OrderId 1-m
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Permanent && Order - Foreign Key OrderId 1-1
            modelBuilder.Entity<Permanent>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Permanent)
                .HasForeignKey<Permanent>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Rental && Order - Foreign Key OrderId 1-1
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Order)
                .WithOne(o => o.Rental)
                .HasForeignKey<Rental>(r => r.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            //Refund && Order - Foreignkey 1-1
            modelBuilder.Entity<Order>()
              .HasOne(o => o.Refund)
              .WithOne(r => r.Order)
              .HasForeignKey<Refund>(r => r.OrderId);
        }

    }
}
