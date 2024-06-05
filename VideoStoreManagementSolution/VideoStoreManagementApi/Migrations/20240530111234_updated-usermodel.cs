using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStoreManagementApi.Migrations
{
    public partial class updatedusermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 28, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5789));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 27, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5800));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 3, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5878), new DateTime(2024, 5, 19, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5877) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 8, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5882), new DateTime(2024, 5, 9, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5881) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 5, 30, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5965), new DateTime(2024, 5, 20, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5964), new DateTime(2024, 5, 29, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5966) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 100,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 134, 119, 227, 112, 243, 138, 127, 206, 71, 70, 247, 40, 11, 124, 144, 188, 254, 98, 117, 205, 49, 60, 27, 67, 82, 18, 166, 80, 61, 14, 177, 150, 60, 94, 82, 241, 150, 133, 80, 114, 38, 90, 5, 108, 4, 248, 102, 237, 233, 226, 106, 1, 89, 143, 250, 0, 119, 77, 50, 164, 60, 45, 80, 78 }, new byte[] { 232, 109, 251, 181, 20, 70, 62, 189, 63, 174, 198, 20, 176, 68, 236, 95, 5, 97, 231, 85, 220, 87, 101, 123, 255, 171, 200, 178, 156, 249, 162, 128, 211, 13, 228, 30, 173, 80, 198, 36, 49, 3, 136, 87, 77, 116, 196, 61, 109, 126, 144, 160, 82, 73, 21, 253, 77, 38, 16, 157, 139, 120, 98, 177, 91, 229, 115, 178, 207, 64, 182, 139, 6, 228, 207, 21, 79, 225, 31, 4, 166, 72, 5, 204, 160, 0, 67, 77, 82, 20, 121, 42, 33, 57, 127, 151, 218, 222, 200, 101, 93, 5, 225, 143, 167, 141, 187, 247, 95, 22, 85, 168, 198, 211, 63, 68, 227, 21, 239, 197, 9, 10, 212, 96, 114, 167, 224, 242 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 101,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 153, 150, 74, 224, 74, 10, 127, 102, 234, 155, 57, 88, 13, 9, 163, 195, 113, 54, 34, 131, 14, 102, 226, 248, 90, 67, 49, 188, 143, 165, 125, 6, 36, 121, 193, 132, 154, 36, 8, 242, 237, 51, 207, 121, 168, 215, 192, 27, 130, 204, 125, 202, 235, 4, 142, 229, 41, 75, 213, 197, 64, 77, 15, 204 }, new byte[] { 232, 109, 251, 181, 20, 70, 62, 189, 63, 174, 198, 20, 176, 68, 236, 95, 5, 97, 231, 85, 220, 87, 101, 123, 255, 171, 200, 178, 156, 249, 162, 128, 211, 13, 228, 30, 173, 80, 198, 36, 49, 3, 136, 87, 77, 116, 196, 61, 109, 126, 144, 160, 82, 73, 21, 253, 77, 38, 16, 157, 139, 120, 98, 177, 91, 229, 115, 178, 207, 64, 182, 139, 6, 228, 207, 21, 79, 225, 31, 4, 166, 72, 5, 204, 160, 0, 67, 77, 82, 20, 121, 42, 33, 57, 127, 151, 218, 222, 200, 101, 93, 5, 225, 143, 167, 141, 187, 247, 95, 22, 85, 168, 198, 211, 63, 68, 227, 21, 239, 197, 9, 10, 212, 96, 114, 167, 224, 242 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 102,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 153, 150, 74, 224, 74, 10, 127, 102, 234, 155, 57, 88, 13, 9, 163, 195, 113, 54, 34, 131, 14, 102, 226, 248, 90, 67, 49, 188, 143, 165, 125, 6, 36, 121, 193, 132, 154, 36, 8, 242, 237, 51, 207, 121, 168, 215, 192, 27, 130, 204, 125, 202, 235, 4, 142, 229, 41, 75, 213, 197, 64, 77, 15, 204 }, new byte[] { 232, 109, 251, 181, 20, 70, 62, 189, 63, 174, 198, 20, 176, 68, 236, 95, 5, 97, 231, 85, 220, 87, 101, 123, 255, 171, 200, 178, 156, 249, 162, 128, 211, 13, 228, 30, 173, 80, 198, 36, 49, 3, 136, 87, 77, 116, 196, 61, 109, 126, 144, 160, 82, 73, 21, 253, 77, 38, 16, 157, 139, 120, 98, 177, 91, 229, 115, 178, 207, 64, 182, 139, 6, 228, 207, 21, 79, 225, 31, 4, 166, 72, 5, 204, 160, 0, 67, 77, 82, 20, 121, 42, 33, 57, 127, 151, 218, 222, 200, 101, 93, 5, 225, 143, 167, 141, 187, 247, 95, 22, 85, 168, 198, 211, 63, 68, 227, 21, 239, 197, 9, 10, 212, 96, 114, 167, 224, 242 } });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2023, 5, 29, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5734));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 5, 29, 23, 46, 26, 935, DateTimeKind.Local).AddTicks(5759));
        }
    }
}
