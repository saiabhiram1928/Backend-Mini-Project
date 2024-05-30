using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStoreManagementApi.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false),
                    MembershipType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Customers_Users_Uid",
                        column: x => x.Uid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_Inventories_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zipcode = table.Column<int>(type: "int", nullable: false),
                    PrimaryAdress = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RealDeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryAddressId = table.Column<int>(type: "int", nullable: false),
                    RentalOrPermanent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDone = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentSucess = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permanents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TotalQty = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permanents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permanents_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalQty = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    LateFee = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Uid", "Email", "FirstName", "LastName", "Password", "Role", "Salt", "Verified" },
                values: new object[,]
                {
                    { 100, "admin@gmail.com", "admin", "admin", new byte[] { 39, 87, 28, 148, 77, 85, 247, 38, 234, 84, 237, 80, 168, 176, 219, 176, 132, 137, 37, 182, 164, 169, 161, 155, 87, 238, 173, 40, 174, 132, 248, 181, 190, 254, 15, 29, 121, 5, 87, 44, 41, 74, 75, 245, 125, 61, 75, 2, 120, 23, 46, 188, 215, 172, 49, 12, 165, 196, 62, 18, 131, 198, 204, 241 }, "Admin", new byte[] { 206, 131, 223, 180, 54, 214, 110, 39, 248, 43, 254, 31, 164, 228, 116, 164, 125, 100, 220, 246, 79, 119, 202, 63, 103, 199, 147, 169, 31, 242, 83, 186, 58, 220, 146, 66, 207, 152, 188, 88, 131, 10, 110, 65, 110, 205, 71, 137, 223, 78, 32, 154, 77, 77, 221, 137, 67, 183, 108, 71, 47, 138, 225, 140, 77, 131, 238, 190, 43, 251, 68, 37, 12, 199, 173, 100, 201, 159, 70, 209, 138, 232, 248, 35, 158, 198, 21, 53, 187, 93, 179, 93, 171, 195, 143, 190, 230, 148, 60, 209, 243, 199, 105, 49, 239, 141, 122, 16, 123, 115, 59, 163, 154, 216, 72, 93, 136, 35, 13, 36, 212, 33, 212, 155, 123, 212, 28, 196 }, true },
                    { 101, "test1@gmail.com", "test1", "test", new byte[] { 0, 66, 11, 112, 205, 83, 52, 181, 124, 220, 196, 42, 71, 224, 95, 199, 238, 202, 112, 231, 210, 51, 214, 242, 199, 136, 186, 195, 223, 156, 107, 126, 220, 37, 161, 86, 230, 243, 137, 155, 14, 140, 113, 120, 3, 253, 199, 148, 10, 176, 136, 219, 219, 254, 142, 128, 221, 189, 188, 182, 243, 56, 124, 24 }, "Customer", new byte[] { 206, 131, 223, 180, 54, 214, 110, 39, 248, 43, 254, 31, 164, 228, 116, 164, 125, 100, 220, 246, 79, 119, 202, 63, 103, 199, 147, 169, 31, 242, 83, 186, 58, 220, 146, 66, 207, 152, 188, 88, 131, 10, 110, 65, 110, 205, 71, 137, 223, 78, 32, 154, 77, 77, 221, 137, 67, 183, 108, 71, 47, 138, 225, 140, 77, 131, 238, 190, 43, 251, 68, 37, 12, 199, 173, 100, 201, 159, 70, 209, 138, 232, 248, 35, 158, 198, 21, 53, 187, 93, 179, 93, 171, 195, 143, 190, 230, 148, 60, 209, 243, 199, 105, 49, 239, 141, 122, 16, 123, 115, 59, 163, 154, 216, 72, 93, 136, 35, 13, 36, 212, 33, 212, 155, 123, 212, 28, 196 }, true },
                    { 102, "test2@gmail.com", "test2", "test", new byte[] { 0, 66, 11, 112, 205, 83, 52, 181, 124, 220, 196, 42, 71, 224, 95, 199, 238, 202, 112, 231, 210, 51, 214, 242, 199, 136, 186, 195, 223, 156, 107, 126, 220, 37, 161, 86, 230, 243, 137, 155, 14, 140, 113, 120, 3, 253, 199, 148, 10, 176, 136, 219, 219, 254, 142, 128, 221, 189, 188, 182, 243, 56, 124, 24 }, "Customer", new byte[] { 206, 131, 223, 180, 54, 214, 110, 39, 248, 43, 254, 31, 164, 228, 116, 164, 125, 100, 220, 246, 79, 119, 202, 63, 103, 199, 147, 169, 31, 242, 83, 186, 58, 220, 146, 66, 207, 152, 188, 88, 131, 10, 110, 65, 110, 205, 71, 137, 223, 78, 32, 154, 77, 77, 221, 137, 67, 183, 108, 71, 47, 138, 225, 140, 77, 131, 238, 190, 43, 251, 68, 37, 12, 199, 173, 100, 201, 159, 70, 209, 138, 232, 248, 35, 158, 198, 21, 53, 187, 93, 179, 93, 171, 195, 143, 190, 230, 148, 60, 209, 243, 199, 105, 49, 239, 141, 122, 16, 123, 115, 59, 163, 154, 216, 72, 93, 136, 35, 13, 36, 212, 33, 212, 155, 123, 212, 28, 196 }, true }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "Description", "Director", "Genre", "Price", "ReleaseDate", "Tittle" },
                values: new object[,]
                {
                    { 1, "At Mr. Rad's Warehouse, the best hip-hop crews in Los Angeles compete for money and respect.", "Director 1", "Action", 19.99f, new DateTime(2023, 5, 29, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3287), "Action Movie" },
                    { 2, "A basketball player's father must try to convince him to go to a college so he can get a shorter sentence.", "Director 2", "Comedy", 14.99f, new DateTime(2022, 5, 29, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3302), "Comedy Movie" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Uid", "MembershipType" },
                values: new object[,]
                {
                    { 100, "Premium" },
                    { 101, "Basic" },
                    { 102, "Premium" }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "VideoId", "LastUpdate", "Stock" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 28, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3319), 10 },
                    { 2, new DateTime(2024, 5, 27, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3324), 5 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "Area", "City", "CustomerId", "PrimaryAdress", "State", "Zipcode" },
                values: new object[,]
                {
                    { 1, "Downtown", "CityA", 101, true, "StateA", 12345 },
                    { 2, "Uptown", "CityB", 102, false, "StateB", 54321 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CustomerId", "TotalPrice" },
                values: new object[,]
                {
                    { 1, 101, 19.99f },
                    { 2, 102, 29.98f }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "Price", "Qty", "VideoId" },
                values: new object[,]
                {
                    { 1, 1, 19.99f, 1, 1 },
                    { 2, 2, 29.98f, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DeliveryAddressId", "DeliveryStatus", "ExpectedDeliveryDate", "OrderedDate", "PaymentDone", "PaymentType", "RealDeliveredDate", "RentalOrPermanent" },
                values: new object[,]
                {
                    { 1, 101, 1, "Delivered", new DateTime(2024, 6, 3, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3358), new DateTime(2024, 5, 19, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3358), true, "COD", null, "Rental" },
                    { 2, 102, 2, "Delivered", new DateTime(2024, 6, 8, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3360), new DateTime(2024, 5, 9, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3360), false, "COD", null, "Permanent" }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "Price", "Qty", "VideoId" },
                values: new object[,]
                {
                    { 1, 1, 19.99f, 1, 1 },
                    { 2, 2, 29.98f, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Permanents",
                columns: new[] { "Id", "OrderId", "TotalPrice", "TotalQty" },
                values: new object[] { 1, 2, 29.98f, 2 });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "DueDate", "LateFee", "OrderId", "RentDate", "ReturnDate", "TotalPrice", "TotalQty" },
                values: new object[] { 1, new DateTime(2024, 5, 30, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3437), 0f, 1, new DateTime(2024, 5, 20, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3436), new DateTime(2024, 5, 29, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3437), 19.99f, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId_VideoId",
                table: "CartItems",
                columns: new[] { "CartId", "VideoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_VideoId",
                table: "CartItems",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_VideoId",
                table: "OrderItems",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permanents_OrderId",
                table: "Permanents",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_OrderId",
                table: "Rentals",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Permanents");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
