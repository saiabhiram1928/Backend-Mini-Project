using Microsoft.EntityFrameworkCore;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Contexts
{
    public class VideoStoreContext:DbContext
    {
        public VideoStoreContext(DbContextOptions options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Adresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Video - Genre as Enum
            modelBuilder.Entity<Video>()
                .Property(v => v.Genre)
                .HasConversion<int>();

            // User - Role as Enum
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<int>();

            // Customer - MembershipType as Enum
            modelBuilder.Entity<Customer>()
                .Property(c => c.MembershipType)
                .HasConversion<int>();


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
                .OnDelete(DeleteBehavior.Cascade);

            // Address && Customer - Foreign Key CustomerId 1-m
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Address)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cart && Customer - Foreign Key CustomerId 1-1
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithOne() 
                .HasForeignKey<Cart>(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItems && Cart - Foreign Key CartId 1-m
            modelBuilder.Entity<CartItems>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItems && Video - Foreign Key VideoId 1-1
            modelBuilder.Entity<CartItems>()
               .HasOne(ci => ci.Video)
               .WithOne()
               .HasForeignKey<CartItems>(ci => ci.VideoId)
               .OnDelete(DeleteBehavior.Restrict);

            // Order && Customer - Foreign Key CustomerId 1-m
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order && Address - Foreign Key DeliveryAddressId 1-1
            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryAddress)
                .WithOne()
                .HasForeignKey<Order>(o => o.DeliveryAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItems && Order - Foreign Key OrderId 1-m
            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItems && Video - Foreign Key VideoId 1-m
            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.Video)
                .WithMany()
                .HasForeignKey(oi => oi.VideoId)
                .OnDelete(DeleteBehavior.Restrict);
            // Payment && Order - Foreign Key OrderId 1-m
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Permanent && Order - Foreign Key OrderId 1-1
            modelBuilder.Entity<Permanent>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Permanent)
                .HasForeignKey<Permanent>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rental && Order - Foreign Key OrderId 1-1
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Order)
                .WithOne(o => o.Rental)
                .HasForeignKey<Rental>(r => r.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
