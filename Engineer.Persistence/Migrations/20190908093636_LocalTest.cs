using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Engineer.Persistence.Migrations
{
    public partial class LocalTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("180d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 0", "", "Item 0", 100m },
                    { new Guid("181d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 5, 57, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 1", "", "Item 1", 110m },
                    { new Guid("182d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 6, 13, 48, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 2", "", "Item 2", 120m },
                    { new Guid("183d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 6, 30, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 3", "", "Item 3", 130m },
                    { new Guid("184d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 6, 47, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 4", "", "Item 4", 140m },
                    { new Guid("185d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 7, 3, 48, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 5", "", "Item 5", 150m },
                    { new Guid("186d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 7, 20, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 6", "", "Item 6", 160m },
                    { new Guid("187d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 7, 37, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 7", "", "Item 7", 170m },
                    { new Guid("188d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 7, 53, 48, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 8", "", "Item 8", 180m },
                    { new Guid("189d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 8, 10, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Funny little item 9", "", "Item 9", 190m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("180d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("181d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("182d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("183d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("184d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("185d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("186d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("187d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("188d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("189d4b4d-439b-4356-bc21-d01e3dd651ca"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
