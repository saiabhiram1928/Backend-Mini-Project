using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStoreManagementApi.Migrations
{
    public partial class updatedusermode2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 29, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9437));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 28, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9443));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9598), new DateTime(2024, 5, 20, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9597) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 9, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9600), new DateTime(2024, 5, 10, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9600) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 5, 31, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9640), new DateTime(2024, 5, 21, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9640), new DateTime(2024, 5, 30, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9641) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 100,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 103, 119, 164, 25, 223, 147, 239, 190, 158, 10, 81, 155, 219, 70, 238, 79, 58, 145, 20, 241, 183, 221, 176, 177, 183, 99, 84, 94, 94, 118, 169, 238, 182, 184, 106, 109, 245, 147, 45, 31, 215, 0, 208, 146, 95, 35, 41, 149, 157, 95, 173, 108, 182, 130, 179, 18, 208, 1, 225, 175, 184, 41, 55, 169 }, new byte[] { 17, 237, 83, 59, 189, 27, 126, 116, 14, 5, 21, 149, 132, 21, 84, 95, 55, 52, 39, 238, 152, 133, 57, 49, 238, 217, 211, 21, 96, 231, 24, 51, 208, 202, 149, 112, 96, 145, 231, 210, 74, 127, 25, 36, 115, 37, 29, 196, 72, 142, 9, 237, 58, 114, 232, 93, 78, 234, 237, 34, 132, 209, 208, 248, 61, 148, 24, 239, 57, 139, 59, 223, 232, 18, 231, 228, 227, 81, 137, 80, 31, 175, 114, 216, 7, 253, 46, 100, 230, 24, 0, 175, 106, 78, 158, 175, 89, 88, 97, 36, 218, 177, 135, 156, 203, 46, 175, 218, 34, 139, 250, 138, 60, 38, 49, 3, 248, 111, 36, 241, 190, 90, 254, 100, 92, 34, 22, 44 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 101,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 228, 120, 219, 169, 147, 180, 71, 52, 13, 34, 147, 222, 205, 177, 161, 254, 46, 54, 113, 21, 41, 114, 31, 68, 71, 113, 234, 253, 62, 2, 199, 250, 119, 126, 151, 213, 35, 40, 221, 205, 237, 173, 95, 13, 8, 93, 167, 24, 43, 178, 246, 166, 26, 152, 162, 46, 177, 111, 252, 208, 177, 25, 58, 110 }, new byte[] { 17, 237, 83, 59, 189, 27, 126, 116, 14, 5, 21, 149, 132, 21, 84, 95, 55, 52, 39, 238, 152, 133, 57, 49, 238, 217, 211, 21, 96, 231, 24, 51, 208, 202, 149, 112, 96, 145, 231, 210, 74, 127, 25, 36, 115, 37, 29, 196, 72, 142, 9, 237, 58, 114, 232, 93, 78, 234, 237, 34, 132, 209, 208, 248, 61, 148, 24, 239, 57, 139, 59, 223, 232, 18, 231, 228, 227, 81, 137, 80, 31, 175, 114, 216, 7, 253, 46, 100, 230, 24, 0, 175, 106, 78, 158, 175, 89, 88, 97, 36, 218, 177, 135, 156, 203, 46, 175, 218, 34, 139, 250, 138, 60, 38, 49, 3, 248, 111, 36, 241, 190, 90, 254, 100, 92, 34, 22, 44 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 102,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 228, 120, 219, 169, 147, 180, 71, 52, 13, 34, 147, 222, 205, 177, 161, 254, 46, 54, 113, 21, 41, 114, 31, 68, 71, 113, 234, 253, 62, 2, 199, 250, 119, 126, 151, 213, 35, 40, 221, 205, 237, 173, 95, 13, 8, 93, 167, 24, 43, 178, 246, 166, 26, 152, 162, 46, 177, 111, 252, 208, 177, 25, 58, 110 }, new byte[] { 17, 237, 83, 59, 189, 27, 126, 116, 14, 5, 21, 149, 132, 21, 84, 95, 55, 52, 39, 238, 152, 133, 57, 49, 238, 217, 211, 21, 96, 231, 24, 51, 208, 202, 149, 112, 96, 145, 231, 210, 74, 127, 25, 36, 115, 37, 29, 196, 72, 142, 9, 237, 58, 114, 232, 93, 78, 234, 237, 34, 132, 209, 208, 248, 61, 148, 24, 239, 57, 139, 59, 223, 232, 18, 231, 228, 227, 81, 137, 80, 31, 175, 114, 216, 7, 253, 46, 100, 230, 24, 0, 175, 106, 78, 158, 175, 89, 88, 97, 36, 218, 177, 135, 156, 203, 46, 175, 218, 34, 139, 250, 138, 60, 38, 49, 3, 248, 111, 36, 241, 190, 90, 254, 100, 92, 34, 22, 44 } });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2023, 5, 30, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9402));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 5, 30, 17, 9, 34, 374, DateTimeKind.Local).AddTicks(9419));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 29, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 28, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2788));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 4, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2869), new DateTime(2024, 5, 20, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2867) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 9, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2873), new DateTime(2024, 5, 10, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2872) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 5, 31, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2941), new DateTime(2024, 5, 21, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2940), new DateTime(2024, 5, 30, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2942) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 100,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 92, 139, 115, 178, 102, 81, 114, 132, 169, 175, 146, 9, 209, 53, 18, 103, 90, 8, 209, 118, 3, 244, 111, 175, 209, 24, 230, 185, 61, 185, 216, 43, 244, 109, 115, 131, 242, 187, 168, 19, 193, 139, 161, 43, 9, 72, 203, 46, 169, 66, 94, 202, 117, 198, 125, 238, 246, 10, 241, 60, 151, 116, 171, 224 }, new byte[] { 133, 253, 69, 246, 25, 58, 251, 180, 33, 101, 39, 200, 112, 150, 78, 165, 171, 31, 164, 71, 233, 178, 243, 72, 101, 64, 234, 181, 84, 51, 28, 158, 118, 233, 40, 86, 86, 246, 22, 243, 111, 89, 23, 107, 36, 121, 13, 33, 244, 71, 251, 100, 53, 139, 45, 78, 238, 29, 179, 115, 177, 97, 108, 123, 4, 128, 75, 172, 108, 78, 127, 209, 193, 159, 137, 73, 226, 161, 200, 52, 33, 123, 77, 205, 39, 188, 36, 246, 218, 81, 95, 188, 19, 68, 152, 156, 27, 76, 253, 181, 149, 215, 27, 42, 8, 11, 135, 98, 194, 3, 160, 30, 27, 114, 116, 21, 138, 149, 178, 223, 21, 202, 78, 255, 196, 252, 60, 124 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 101,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 145, 33, 26, 144, 196, 100, 67, 235, 71, 203, 13, 53, 12, 107, 169, 211, 137, 203, 246, 85, 11, 139, 193, 164, 86, 114, 162, 21, 143, 89, 228, 154, 185, 80, 142, 63, 217, 88, 99, 210, 120, 0, 229, 212, 151, 160, 96, 68, 34, 57, 28, 204, 97, 47, 135, 23, 204, 45, 159, 246, 17, 235, 135, 243 }, new byte[] { 133, 253, 69, 246, 25, 58, 251, 180, 33, 101, 39, 200, 112, 150, 78, 165, 171, 31, 164, 71, 233, 178, 243, 72, 101, 64, 234, 181, 84, 51, 28, 158, 118, 233, 40, 86, 86, 246, 22, 243, 111, 89, 23, 107, 36, 121, 13, 33, 244, 71, 251, 100, 53, 139, 45, 78, 238, 29, 179, 115, 177, 97, 108, 123, 4, 128, 75, 172, 108, 78, 127, 209, 193, 159, 137, 73, 226, 161, 200, 52, 33, 123, 77, 205, 39, 188, 36, 246, 218, 81, 95, 188, 19, 68, 152, 156, 27, 76, 253, 181, 149, 215, 27, 42, 8, 11, 135, 98, 194, 3, 160, 30, 27, 114, 116, 21, 138, 149, 178, 223, 21, 202, 78, 255, 196, 252, 60, 124 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 102,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 145, 33, 26, 144, 196, 100, 67, 235, 71, 203, 13, 53, 12, 107, 169, 211, 137, 203, 246, 85, 11, 139, 193, 164, 86, 114, 162, 21, 143, 89, 228, 154, 185, 80, 142, 63, 217, 88, 99, 210, 120, 0, 229, 212, 151, 160, 96, 68, 34, 57, 28, 204, 97, 47, 135, 23, 204, 45, 159, 246, 17, 235, 135, 243 }, new byte[] { 133, 253, 69, 246, 25, 58, 251, 180, 33, 101, 39, 200, 112, 150, 78, 165, 171, 31, 164, 71, 233, 178, 243, 72, 101, 64, 234, 181, 84, 51, 28, 158, 118, 233, 40, 86, 86, 246, 22, 243, 111, 89, 23, 107, 36, 121, 13, 33, 244, 71, 251, 100, 53, 139, 45, 78, 238, 29, 179, 115, 177, 97, 108, 123, 4, 128, 75, 172, 108, 78, 127, 209, 193, 159, 137, 73, 226, 161, 200, 52, 33, 123, 77, 205, 39, 188, 36, 246, 218, 81, 95, 188, 19, 68, 152, 156, 27, 76, 253, 181, 149, 215, 27, 42, 8, 11, 135, 98, 194, 3, 160, 30, 27, 114, 116, 21, 138, 149, 178, 223, 21, 202, 78, 255, 196, 252, 60, 124 } });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2023, 5, 30, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2726));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 5, 30, 16, 42, 33, 468, DateTimeKind.Local).AddTicks(2749));
        }
    }
}
