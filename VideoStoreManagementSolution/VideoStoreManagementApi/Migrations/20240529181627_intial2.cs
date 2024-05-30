using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStoreManagementApi.Migrations
{
    public partial class intial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalAmount",
                table: "Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 28, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3319));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "VideoId",
                keyValue: 2,
                column: "LastUpdate",
                value: new DateTime(2024, 5, 27, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3324));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 3, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3358), new DateTime(2024, 5, 19, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3358) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpectedDeliveryDate", "OrderedDate" },
                values: new object[] { new DateTime(2024, 6, 8, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3360), new DateTime(2024, 5, 9, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3360) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "RentDate", "ReturnDate" },
                values: new object[] { new DateTime(2024, 5, 30, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3437), new DateTime(2024, 5, 20, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3436), new DateTime(2024, 5, 29, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3437) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 100,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 39, 87, 28, 148, 77, 85, 247, 38, 234, 84, 237, 80, 168, 176, 219, 176, 132, 137, 37, 182, 164, 169, 161, 155, 87, 238, 173, 40, 174, 132, 248, 181, 190, 254, 15, 29, 121, 5, 87, 44, 41, 74, 75, 245, 125, 61, 75, 2, 120, 23, 46, 188, 215, 172, 49, 12, 165, 196, 62, 18, 131, 198, 204, 241 }, new byte[] { 206, 131, 223, 180, 54, 214, 110, 39, 248, 43, 254, 31, 164, 228, 116, 164, 125, 100, 220, 246, 79, 119, 202, 63, 103, 199, 147, 169, 31, 242, 83, 186, 58, 220, 146, 66, 207, 152, 188, 88, 131, 10, 110, 65, 110, 205, 71, 137, 223, 78, 32, 154, 77, 77, 221, 137, 67, 183, 108, 71, 47, 138, 225, 140, 77, 131, 238, 190, 43, 251, 68, 37, 12, 199, 173, 100, 201, 159, 70, 209, 138, 232, 248, 35, 158, 198, 21, 53, 187, 93, 179, 93, 171, 195, 143, 190, 230, 148, 60, 209, 243, 199, 105, 49, 239, 141, 122, 16, 123, 115, 59, 163, 154, 216, 72, 93, 136, 35, 13, 36, 212, 33, 212, 155, 123, 212, 28, 196 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 101,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 0, 66, 11, 112, 205, 83, 52, 181, 124, 220, 196, 42, 71, 224, 95, 199, 238, 202, 112, 231, 210, 51, 214, 242, 199, 136, 186, 195, 223, 156, 107, 126, 220, 37, 161, 86, 230, 243, 137, 155, 14, 140, 113, 120, 3, 253, 199, 148, 10, 176, 136, 219, 219, 254, 142, 128, 221, 189, 188, 182, 243, 56, 124, 24 }, new byte[] { 206, 131, 223, 180, 54, 214, 110, 39, 248, 43, 254, 31, 164, 228, 116, 164, 125, 100, 220, 246, 79, 119, 202, 63, 103, 199, 147, 169, 31, 242, 83, 186, 58, 220, 146, 66, 207, 152, 188, 88, 131, 10, 110, 65, 110, 205, 71, 137, 223, 78, 32, 154, 77, 77, 221, 137, 67, 183, 108, 71, 47, 138, 225, 140, 77, 131, 238, 190, 43, 251, 68, 37, 12, 199, 173, 100, 201, 159, 70, 209, 138, 232, 248, 35, 158, 198, 21, 53, 187, 93, 179, 93, 171, 195, 143, 190, 230, 148, 60, 209, 243, 199, 105, 49, 239, 141, 122, 16, 123, 115, 59, 163, 154, 216, 72, 93, 136, 35, 13, 36, 212, 33, 212, 155, 123, 212, 28, 196 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: 102,
                columns: new[] { "Password", "Salt" },
                values: new object[] { new byte[] { 0, 66, 11, 112, 205, 83, 52, 181, 124, 220, 196, 42, 71, 224, 95, 199, 238, 202, 112, 231, 210, 51, 214, 242, 199, 136, 186, 195, 223, 156, 107, 126, 220, 37, 161, 86, 230, 243, 137, 155, 14, 140, 113, 120, 3, 253, 199, 148, 10, 176, 136, 219, 219, 254, 142, 128, 221, 189, 188, 182, 243, 56, 124, 24 }, new byte[] { 206, 131, 223, 180, 54, 214, 110, 39, 248, 43, 254, 31, 164, 228, 116, 164, 125, 100, 220, 246, 79, 119, 202, 63, 103, 199, 147, 169, 31, 242, 83, 186, 58, 220, 146, 66, 207, 152, 188, 88, 131, 10, 110, 65, 110, 205, 71, 137, 223, 78, 32, 154, 77, 77, 221, 137, 67, 183, 108, 71, 47, 138, 225, 140, 77, 131, 238, 190, 43, 251, 68, 37, 12, 199, 173, 100, 201, 159, 70, 209, 138, 232, 248, 35, 158, 198, 21, 53, 187, 93, 179, 93, 171, 195, 143, 190, 230, 148, 60, 209, 243, 199, 105, 49, 239, 141, 122, 16, 123, 115, 59, 163, 154, 216, 72, 93, 136, 35, 13, 36, 212, 33, 212, 155, 123, 212, 28, 196 } });

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleaseDate",
                value: new DateTime(2023, 5, 29, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3287));

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleaseDate",
                value: new DateTime(2022, 5, 29, 18, 2, 21, 393, DateTimeKind.Local).AddTicks(3302));
        }
    }
}
