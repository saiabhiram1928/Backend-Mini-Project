﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoStoreManagementApi.Contexts;

#nullable disable

namespace VideoStoreManagementApi.Migrations
{
    [DbContext(typeof(VideoStoreContext))]
    partial class VideoStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VideoStoreManagementApi.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("PrimaryAdress")
                        .HasColumnType("bit");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Zipcode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Area = "Downtown",
                            City = "CityA",
                            CustomerId = 101,
                            PrimaryAdress = true,
                            State = "StateA",
                            Zipcode = 12345
                        },
                        new
                        {
                            Id = 2,
                            Area = "Uptown",
                            City = "CityB",
                            CustomerId = 102,
                            PrimaryAdress = false,
                            State = "StateB",
                            Zipcode = 54321
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Carts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 101,
                            TotalPrice = 19.99f
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 102,
                            TotalPrice = 29.98f
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.HasIndex("CartId", "VideoId")
                        .IsUnique();

                    b.ToTable("CartItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CartId = 1,
                            Price = 19.99f,
                            Qty = 1,
                            VideoId = 1
                        },
                        new
                        {
                            Id = 2,
                            CartId = 2,
                            Price = 29.98f,
                            Qty = 2,
                            VideoId = 2
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Customer", b =>
                {
                    b.Property<int>("Uid")
                        .HasColumnType("int");

                    b.Property<string>("MembershipType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Uid = 101,
                            MembershipType = "Basic"
                        },
                        new
                        {
                            Uid = 102,
                            MembershipType = "Premium"
                        },
                        new
                        {
                            Uid = 100,
                            MembershipType = "Premium"
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Inventory", b =>
                {
                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("VideoId");

                    b.ToTable("Inventories");

                    b.HasData(
                        new
                        {
                            VideoId = 1,
                            LastUpdate = new DateTime(2024, 5, 30, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(713),
                            Stock = 10
                        },
                        new
                        {
                            VideoId = 2,
                            LastUpdate = new DateTime(2024, 5, 29, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(719),
                            Stock = 5
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("DeliveryAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpectedDeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PaymentDone")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RealDeliveredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RentalOrPermanent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalAmount")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryAddressId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 101,
                            DeliveryAddressId = 1,
                            ExpectedDeliveryDate = new DateTime(2024, 6, 5, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(776),
                            OrderStatus = "Delivered",
                            OrderedDate = new DateTime(2024, 5, 21, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(775),
                            PaymentDone = true,
                            PaymentType = "COD",
                            RentalOrPermanent = "Rental",
                            TotalAmount = 0f
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 102,
                            DeliveryAddressId = 2,
                            ExpectedDeliveryDate = new DateTime(2024, 6, 10, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(780),
                            OrderStatus = "Delivered",
                            OrderedDate = new DateTime(2024, 5, 11, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(779),
                            PaymentDone = false,
                            PaymentType = "COD",
                            RentalOrPermanent = "Permanent",
                            TotalAmount = 0f
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("VideoId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            Price = 19.99f,
                            Qty = 1,
                            VideoId = 1
                        },
                        new
                        {
                            Id = 2,
                            OrderId = 2,
                            Price = 29.98f,
                            Qty = 2,
                            VideoId = 2
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PaymentSucess")
                        .HasColumnType("bit");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Permanent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.Property<int>("TotalQty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Permanents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 2,
                            TotalPrice = 29.98f,
                            TotalQty = 2
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Refund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TranasactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Refunds");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("LateFee")
                        .HasColumnType("real");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.Property<int>("TotalQty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Rentals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DueDate = new DateTime(2024, 6, 1, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(904),
                            LateFee = 0f,
                            OrderId = 1,
                            RentDate = new DateTime(2024, 5, 22, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(902),
                            ReturnDate = new DateTime(2024, 5, 31, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(907),
                            TotalPrice = 19.99f,
                            TotalQty = 1
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.User", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Uid"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("Verified")
                        .HasColumnType("bit");

                    b.HasKey("Uid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Uid = 101,
                            Email = "test1@gmail.com",
                            FirstName = "test1",
                            LastName = "test",
                            Password = new byte[] { 103, 34, 118, 72, 75, 37, 15, 240, 69, 235, 197, 41, 237, 21, 211, 0, 80, 202, 67, 89, 96, 153, 25, 140, 196, 72, 128, 99, 181, 239, 100, 15, 232, 137, 234, 178, 52, 179, 104, 154, 113, 116, 33, 76, 17, 101, 205, 56, 102, 186, 191, 56, 57, 91, 18, 19, 10, 48, 38, 211, 150, 49, 184, 103 },
                            Role = "Customer",
                            Salt = new byte[] { 18, 251, 22, 234, 59, 85, 119, 54, 212, 247, 106, 249, 173, 196, 78, 63, 184, 27, 38, 64, 25, 173, 85, 139, 129, 26, 93, 222, 98, 95, 42, 105, 147, 13, 116, 143, 115, 184, 166, 114, 17, 85, 75, 252, 229, 131, 183, 222, 49, 234, 249, 24, 102, 194, 223, 216, 118, 206, 94, 59, 233, 90, 155, 52, 90, 20, 248, 58, 242, 203, 2, 163, 187, 252, 31, 144, 227, 80, 131, 143, 76, 145, 252, 255, 85, 68, 23, 212, 197, 2, 132, 108, 165, 40, 194, 38, 117, 14, 242, 67, 183, 63, 101, 57, 153, 127, 88, 49, 171, 132, 70, 110, 233, 93, 214, 54, 33, 211, 93, 133, 237, 152, 19, 171, 122, 38, 112, 182 },
                            Verified = true
                        },
                        new
                        {
                            Uid = 102,
                            Email = "test2@gmail.com",
                            FirstName = "test2",
                            LastName = "test",
                            Password = new byte[] { 103, 34, 118, 72, 75, 37, 15, 240, 69, 235, 197, 41, 237, 21, 211, 0, 80, 202, 67, 89, 96, 153, 25, 140, 196, 72, 128, 99, 181, 239, 100, 15, 232, 137, 234, 178, 52, 179, 104, 154, 113, 116, 33, 76, 17, 101, 205, 56, 102, 186, 191, 56, 57, 91, 18, 19, 10, 48, 38, 211, 150, 49, 184, 103 },
                            Role = "Customer",
                            Salt = new byte[] { 18, 251, 22, 234, 59, 85, 119, 54, 212, 247, 106, 249, 173, 196, 78, 63, 184, 27, 38, 64, 25, 173, 85, 139, 129, 26, 93, 222, 98, 95, 42, 105, 147, 13, 116, 143, 115, 184, 166, 114, 17, 85, 75, 252, 229, 131, 183, 222, 49, 234, 249, 24, 102, 194, 223, 216, 118, 206, 94, 59, 233, 90, 155, 52, 90, 20, 248, 58, 242, 203, 2, 163, 187, 252, 31, 144, 227, 80, 131, 143, 76, 145, 252, 255, 85, 68, 23, 212, 197, 2, 132, 108, 165, 40, 194, 38, 117, 14, 242, 67, 183, 63, 101, 57, 153, 127, 88, 49, 171, 132, 70, 110, 233, 93, 214, 54, 33, 211, 93, 133, 237, 152, 19, 171, 122, 38, 112, 182 },
                            Verified = true
                        },
                        new
                        {
                            Uid = 100,
                            Email = "admin@gmail.com",
                            FirstName = "admin",
                            LastName = "admin",
                            Password = new byte[] { 56, 153, 100, 118, 110, 21, 181, 58, 191, 191, 225, 209, 26, 250, 184, 99, 170, 54, 211, 192, 140, 101, 34, 51, 67, 122, 129, 246, 134, 195, 148, 71, 255, 57, 43, 190, 237, 92, 100, 222, 184, 106, 231, 76, 202, 197, 15, 88, 130, 244, 39, 135, 39, 114, 234, 53, 109, 23, 169, 34, 45, 40, 86, 17 },
                            Role = "Admin",
                            Salt = new byte[] { 18, 251, 22, 234, 59, 85, 119, 54, 212, 247, 106, 249, 173, 196, 78, 63, 184, 27, 38, 64, 25, 173, 85, 139, 129, 26, 93, 222, 98, 95, 42, 105, 147, 13, 116, 143, 115, 184, 166, 114, 17, 85, 75, 252, 229, 131, 183, 222, 49, 234, 249, 24, 102, 194, 223, 216, 118, 206, 94, 59, 233, 90, 155, 52, 90, 20, 248, 58, 242, 203, 2, 163, 187, 252, 31, 144, 227, 80, 131, 143, 76, 145, 252, 255, 85, 68, 23, 212, 197, 2, 132, 108, 165, 40, 194, 38, 117, 14, 242, 67, 183, 63, 101, 57, 153, 127, 88, 49, 171, 132, 70, 110, 233, 93, 214, 54, 33, 211, 93, 133, 237, 152, 19, 171, 122, 38, 112, 182 },
                            Verified = true
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Tittle")
                        .IsUnique();

                    b.ToTable("Videos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "At Mr. Rad's Warehouse, the best hip-hop crews in Los Angeles compete for money and respect.",
                            Director = "Director 1",
                            Genre = "Action",
                            Price = 19.99f,
                            ReleaseDate = new DateTime(2023, 5, 31, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(665),
                            Tittle = "Action Movie"
                        },
                        new
                        {
                            Id = 2,
                            Description = "A basketball player's father must try to convince him to go to a college so he can get a shorter sentence.",
                            Director = "Director 2",
                            Genre = "Comedy",
                            Price = 14.99f,
                            ReleaseDate = new DateTime(2022, 5, 31, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(688),
                            Tittle = "Comedy Movie"
                        });
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Address", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Customer", "Customer")
                        .WithMany("Address")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Cart", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Customer", "Customer")
                        .WithOne()
                        .HasForeignKey("VideoStoreManagementApi.Models.Cart", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.CartItem", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VideoStoreManagementApi.Models.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Customer", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("VideoStoreManagementApi.Models.Customer", "Uid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Inventory", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Video", "Video")
                        .WithOne()
                        .HasForeignKey("VideoStoreManagementApi.Models.Inventory", "VideoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Video");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Order", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VideoStoreManagementApi.Models.Address", "DeliveryAddress")
                        .WithMany()
                        .HasForeignKey("DeliveryAddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("DeliveryAddress");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.OrderItem", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VideoStoreManagementApi.Models.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Payment", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Permanent", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Order", "Order")
                        .WithOne("Permanent")
                        .HasForeignKey("VideoStoreManagementApi.Models.Permanent", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Refund", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Order", "Order")
                        .WithOne("Refund")
                        .HasForeignKey("VideoStoreManagementApi.Models.Refund", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Rental", b =>
                {
                    b.HasOne("VideoStoreManagementApi.Models.Order", "Order")
                        .WithOne("Rental")
                        .HasForeignKey("VideoStoreManagementApi.Models.Rental", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Customer", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("VideoStoreManagementApi.Models.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payments");

                    b.Navigation("Permanent")
                        .IsRequired();

                    b.Navigation("Refund")
                        .IsRequired();

                    b.Navigation("Rental")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
