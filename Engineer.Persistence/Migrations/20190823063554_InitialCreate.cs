using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Engineer.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    Task = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "IsCompleted", "Task" },
                values: new object[,]
                {
                    { new Guid("180d4b4d-439b-4356-bc21-d01e3dd651ca"), new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 28, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Seed Database" },
                    { new Guid("9879d7a2-4138-49ff-bbb7-46b48431fd2e"), new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 38, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Migrate Database" },
                    { new Guid("111d3da4-8c0d-46f4-bb5f-9d47896bdbd2"), new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 48, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Get Data" },
                    { new Guid("780f2cfb-1bcb-4765-9a7c-41d3fda1c14c"), new DateTimeOffset(new DateTime(2019, 8, 23, 5, 40, 58, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Update Data" },
                    { new Guid("005123eb-7299-4845-8f7e-6b2b37f5bdbc"), new DateTimeOffset(new DateTime(2019, 8, 23, 5, 41, 8, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Delete Data" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
