using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStoreManagementApi.Migrations
{
    public partial class updatedOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 29, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9831));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 28, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9879), new DateTime(2024, 5, 20, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9878) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 9, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9881), new DateTime(2024, 5, 10, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9880) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 5, 31, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9922), new DateTime(2024, 5, 21, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9921), new DateTime(2024, 5, 30, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9923) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 100,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 149, 237, 106, 116, 11, 217, 77, 175, 193, 26, 2, 95, 243, 29, 43, 61, 69, 201, 153, 145, 47, 253, 156, 255, 16, 34, 47, 124, 27, 249, 104, 186, 247, 22, 54, 177, 190, 100, 37, 95, 247, 253, 0, 11, 234, 42, 174, 34, 68, 214, 9, 180, 165, 51, 165, 153, 124, 242, 71, 210, 212, 185, 244, 129 }, new byte[] { 207, 70, 239, 202, 195, 152, 239, 190, 240, 191, 82, 94, 207, 63, 65, 144, 214, 104, 69, 9, 151, 141, 198, 75, 72, 6, 63, 112, 189, 119, 173, 4, 122, 138, 86, 201, 21, 242, 31, 68, 206, 11, 222, 145, 11, 70, 134, 195, 86, 94, 230, 82, 252, 240, 158, 132, 143, 44, 62, 60, 30, 92, 211, 183, 111, 224, 229, 156, 204, 110, 182, 175, 191, 201, 47, 3, 109, 22, 145, 75, 230, 109, 40, 165, 189, 213, 77, 50, 23, 99, 157, 24, 169, 117, 91, 120, 27, 248, 112, 161, 120, 150, 120, 69, 228, 248, 165, 2, 41, 61, 62, 248, 207, 54, 146, 255, 65, 184, 211, 205, 254, 13, 250, 202, 196, 176, 76, 60 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 101,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 123, 239, 43, 120, 46, 211, 34, 46, 152, 172, 118, 153, 179, 233, 180, 219, 81, 98, 170, 232, 9, 158, 116, 32, 169, 156, 85, 50, 216, 66, 48, 230, 171, 100, 42, 183, 149, 187, 39, 100, 23, 41, 83, 239, 92, 40, 200, 206, 63, 22, 237, 12, 152, 68, 167, 25, 184, 4, 180, 107, 152, 199, 89, 121 }, new byte[] { 207, 70, 239, 202, 195, 152, 239, 190, 240, 191, 82, 94, 207, 63, 65, 144, 214, 104, 69, 9, 151, 141, 198, 75, 72, 6, 63, 112, 189, 119, 173, 4, 122, 138, 86, 201, 21, 242, 31, 68, 206, 11, 222, 145, 11, 70, 134, 195, 86, 94, 230, 82, 252, 240, 158, 132, 143, 44, 62, 60, 30, 92, 211, 183, 111, 224, 229, 156, 204, 110, 182, 175, 191, 201, 47, 3, 109, 22, 145, 75, 230, 109, 40, 165, 189, 213, 77, 50, 23, 99, 157, 24, 169, 117, 91, 120, 27, 248, 112, 161, 120, 150, 120, 69, 228, 248, 165, 2, 41, 61, 62, 248, 207, 54, 146, 255, 65, 184, 211, 205, 254, 13, 250, 202, 196, 176, 76, 60 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 102,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 123, 239, 43, 120, 46, 211, 34, 46, 152, 172, 118, 153, 179, 233, 180, 219, 81, 98, 170, 232, 9, 158, 116, 32, 169, 156, 85, 50, 216, 66, 48, 230, 171, 100, 42, 183, 149, 187, 39, 100, 23, 41, 83, 239, 92, 40, 200, 206, 63, 22, 237, 12, 152, 68, 167, 25, 184, 4, 180, 107, 152, 199, 89, 121 }, new byte[] { 207, 70, 239, 202, 195, 152, 239, 190, 240, 191, 82, 94, 207, 63, 65, 144, 214, 104, 69, 9, 151, 141, 198, 75, 72, 6, 63, 112, 189, 119, 173, 4, 122, 138, 86, 201, 21, 242, 31, 68, 206, 11, 222, 145, 11, 70, 134, 195, 86, 94, 230, 82, 252, 240, 158, 132, 143, 44, 62, 60, 30, 92, 211, 183, 111, 224, 229, 156, 204, 110, 182, 175, 191, 201, 47, 3, 109, 22, 145, 75, 230, 109, 40, 165, 189, 213, 77, 50, 23, 99, 157, 24, 169, 117, 91, 120, 27, 248, 112, 161, 120, 150, 120, 69, 228, 248, 165, 2, 41, 61, 62, 248, 207, 54, 146, 255, 65, 184, 211, 205, 254, 13, 250, 202, 196, 176, 76, 60 } });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2023, 5, 30, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9799));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 5, 30, 21, 59, 33, 162, DateTimeKind.Local).AddTicks(9815));
        }
    }
}
