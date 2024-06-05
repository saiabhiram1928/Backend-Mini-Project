using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStoreManagementApi.Migrations
{
    public partial class updatedvideoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tittle",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Tittle",
                table: "Videos",
                column: "Tittle",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Videos_Tittle",
                table: "Videos");

            migrationBuilder.AlterColumn<string>(
                name: "Tittle",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
