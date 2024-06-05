using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStoreManagementApi.Migrations
{
    public partial class AdddedRefundModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryStatus",
                table: "Orders",
                newName: "OrderStatus");

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranasactionId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refunds_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 30, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(713));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 29, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(719));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 5, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(776), new DateTime(2024, 5, 21, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(775) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 10, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(780), new DateTime(2024, 5, 11, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(779) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 6, 1, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(904), new DateTime(2024, 5, 22, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(902), new DateTime(2024, 5, 31, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(907) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 100,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 56, 153, 100, 118, 110, 21, 181, 58, 191, 191, 225, 209, 26, 250, 184, 99, 170, 54, 211, 192, 140, 101, 34, 51, 67, 122, 129, 246, 134, 195, 148, 71, 255, 57, 43, 190, 237, 92, 100, 222, 184, 106, 231, 76, 202, 197, 15, 88, 130, 244, 39, 135, 39, 114, 234, 53, 109, 23, 169, 34, 45, 40, 86, 17 }, new byte[] { 18, 251, 22, 234, 59, 85, 119, 54, 212, 247, 106, 249, 173, 196, 78, 63, 184, 27, 38, 64, 25, 173, 85, 139, 129, 26, 93, 222, 98, 95, 42, 105, 147, 13, 116, 143, 115, 184, 166, 114, 17, 85, 75, 252, 229, 131, 183, 222, 49, 234, 249, 24, 102, 194, 223, 216, 118, 206, 94, 59, 233, 90, 155, 52, 90, 20, 248, 58, 242, 203, 2, 163, 187, 252, 31, 144, 227, 80, 131, 143, 76, 145, 252, 255, 85, 68, 23, 212, 197, 2, 132, 108, 165, 40, 194, 38, 117, 14, 242, 67, 183, 63, 101, 57, 153, 127, 88, 49, 171, 132, 70, 110, 233, 93, 214, 54, 33, 211, 93, 133, 237, 152, 19, 171, 122, 38, 112, 182 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 101,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 103, 34, 118, 72, 75, 37, 15, 240, 69, 235, 197, 41, 237, 21, 211, 0, 80, 202, 67, 89, 96, 153, 25, 140, 196, 72, 128, 99, 181, 239, 100, 15, 232, 137, 234, 178, 52, 179, 104, 154, 113, 116, 33, 76, 17, 101, 205, 56, 102, 186, 191, 56, 57, 91, 18, 19, 10, 48, 38, 211, 150, 49, 184, 103 }, new byte[] { 18, 251, 22, 234, 59, 85, 119, 54, 212, 247, 106, 249, 173, 196, 78, 63, 184, 27, 38, 64, 25, 173, 85, 139, 129, 26, 93, 222, 98, 95, 42, 105, 147, 13, 116, 143, 115, 184, 166, 114, 17, 85, 75, 252, 229, 131, 183, 222, 49, 234, 249, 24, 102, 194, 223, 216, 118, 206, 94, 59, 233, 90, 155, 52, 90, 20, 248, 58, 242, 203, 2, 163, 187, 252, 31, 144, 227, 80, 131, 143, 76, 145, 252, 255, 85, 68, 23, 212, 197, 2, 132, 108, 165, 40, 194, 38, 117, 14, 242, 67, 183, 63, 101, 57, 153, 127, 88, 49, 171, 132, 70, 110, 233, 93, 214, 54, 33, 211, 93, 133, 237, 152, 19, 171, 122, 38, 112, 182 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 102,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 103, 34, 118, 72, 75, 37, 15, 240, 69, 235, 197, 41, 237, 21, 211, 0, 80, 202, 67, 89, 96, 153, 25, 140, 196, 72, 128, 99, 181, 239, 100, 15, 232, 137, 234, 178, 52, 179, 104, 154, 113, 116, 33, 76, 17, 101, 205, 56, 102, 186, 191, 56, 57, 91, 18, 19, 10, 48, 38, 211, 150, 49, 184, 103 }, new byte[] { 18, 251, 22, 234, 59, 85, 119, 54, 212, 247, 106, 249, 173, 196, 78, 63, 184, 27, 38, 64, 25, 173, 85, 139, 129, 26, 93, 222, 98, 95, 42, 105, 147, 13, 116, 143, 115, 184, 166, 114, 17, 85, 75, 252, 229, 131, 183, 222, 49, 234, 249, 24, 102, 194, 223, 216, 118, 206, 94, 59, 233, 90, 155, 52, 90, 20, 248, 58, 242, 203, 2, 163, 187, 252, 31, 144, 227, 80, 131, 143, 76, 145, 252, 255, 85, 68, 23, 212, 197, 2, 132, 108, 165, 40, 194, 38, 117, 14, 242, 67, 183, 63, 101, 57, 153, 127, 88, 49, 171, 132, 70, 110, 233, 93, 214, 54, 33, 211, 93, 133, 237, 152, 19, 171, 122, 38, 112, 182 } });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2023, 5, 31, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 5, 31, 10, 52, 15, 540, DateTimeKind.Local).AddTicks(688));

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_OrderId",
                table: "Refunds",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "Orders",
                newName: "DeliveryStatus");

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 29, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(383));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 28, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(391));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(448), new DateTime(2024, 5, 20, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(447) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 9, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(450), new DateTime(2024, 5, 10, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 5, 31, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(503), new DateTime(2024, 5, 21, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(503), new DateTime(2024, 5, 30, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(506) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 100,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 236, 239, 44, 50, 210, 135, 26, 28, 106, 197, 42, 225, 241, 157, 88, 21, 15, 31, 22, 110, 14, 253, 141, 108, 182, 84, 170, 78, 67, 7, 134, 152, 36, 94, 84, 45, 231, 131, 167, 227, 13, 202, 246, 80, 151, 216, 175, 168, 2, 222, 190, 161, 254, 153, 212, 118, 234, 82, 64, 151, 127, 121, 138, 149 }, new byte[] { 122, 27, 152, 16, 170, 222, 203, 83, 10, 96, 220, 111, 53, 107, 58, 87, 39, 107, 113, 217, 212, 190, 108, 128, 187, 221, 49, 44, 38, 168, 89, 107, 178, 49, 88, 67, 218, 136, 38, 168, 160, 27, 123, 218, 166, 197, 141, 38, 195, 190, 0, 209, 254, 66, 103, 25, 102, 17, 102, 22, 100, 124, 38, 25, 3, 62, 93, 85, 248, 192, 11, 96, 59, 214, 12, 18, 71, 53, 172, 124, 67, 13, 251, 237, 183, 219, 194, 23, 81, 108, 109, 133, 176, 71, 218, 244, 95, 139, 77, 236, 29, 5, 200, 197, 56, 134, 191, 125, 191, 138, 0, 34, 206, 127, 10, 17, 175, 65, 74, 232, 240, 96, 99, 255, 172, 213, 53, 37 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 101,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 213, 149, 131, 188, 125, 209, 142, 22, 34, 73, 227, 81, 3, 243, 199, 2, 142, 108, 83, 154, 41, 44, 102, 89, 163, 7, 182, 135, 162, 51, 232, 124, 232, 217, 217, 70, 60, 70, 53, 55, 109, 24, 22, 98, 19, 148, 142, 5, 56, 76, 172, 142, 108, 103, 164, 200, 85, 134, 97, 197, 23, 36, 254, 27 }, new byte[] { 122, 27, 152, 16, 170, 222, 203, 83, 10, 96, 220, 111, 53, 107, 58, 87, 39, 107, 113, 217, 212, 190, 108, 128, 187, 221, 49, 44, 38, 168, 89, 107, 178, 49, 88, 67, 218, 136, 38, 168, 160, 27, 123, 218, 166, 197, 141, 38, 195, 190, 0, 209, 254, 66, 103, 25, 102, 17, 102, 22, 100, 124, 38, 25, 3, 62, 93, 85, 248, 192, 11, 96, 59, 214, 12, 18, 71, 53, 172, 124, 67, 13, 251, 237, 183, 219, 194, 23, 81, 108, 109, 133, 176, 71, 218, 244, 95, 139, 77, 236, 29, 5, 200, 197, 56, 134, 191, 125, 191, 138, 0, 34, 206, 127, 10, 17, 175, 65, 74, 232, 240, 96, 99, 255, 172, 213, 53, 37 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 102,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 213, 149, 131, 188, 125, 209, 142, 22, 34, 73, 227, 81, 3, 243, 199, 2, 142, 108, 83, 154, 41, 44, 102, 89, 163, 7, 182, 135, 162, 51, 232, 124, 232, 217, 217, 70, 60, 70, 53, 55, 109, 24, 22, 98, 19, 148, 142, 5, 56, 76, 172, 142, 108, 103, 164, 200, 85, 134, 97, 197, 23, 36, 254, 27 }, new byte[] { 122, 27, 152, 16, 170, 222, 203, 83, 10, 96, 220, 111, 53, 107, 58, 87, 39, 107, 113, 217, 212, 190, 108, 128, 187, 221, 49, 44, 38, 168, 89, 107, 178, 49, 88, 67, 218, 136, 38, 168, 160, 27, 123, 218, 166, 197, 141, 38, 195, 190, 0, 209, 254, 66, 103, 25, 102, 17, 102, 22, 100, 124, 38, 25, 3, 62, 93, 85, 248, 192, 11, 96, 59, 214, 12, 18, 71, 53, 172, 124, 67, 13, 251, 237, 183, 219, 194, 23, 81, 108, 109, 133, 176, 71, 218, 244, 95, 139, 77, 236, 29, 5, 200, 197, 56, 134, 191, 125, 191, 138, 0, 34, 206, 127, 10, 17, 175, 65, 74, 232, 240, 96, 99, 255, 172, 213, 53, 37 } });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2023, 5, 30, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(338));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 5, 30, 23, 37, 5, 15, DateTimeKind.Local).AddTicks(360));
        }
    }
}
