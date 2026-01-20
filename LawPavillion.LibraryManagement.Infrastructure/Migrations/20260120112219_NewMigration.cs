using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LawPavillion.LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "ISBN", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, "Wield C. Thompson", "9780132350884", new DateTime(2007, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Object Oriented Programming" },
                    { 2, "Eric Evans", "9780321125217", new DateTime(2003, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Domain-Driven Design" },
                    { 3, "Andrew Hunt & David Thomas", "9780201616224", new DateTime(1999, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Pragmatic Programmer" },
                    { 4, "Martin Kleppmann", "9781449373320", new DateTime(2017, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Designing Data-Intensive Applications" },
                    { 5, "Robert T. Kiyosaki", "9781612680194", new DateTime(1997, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rich Dad Poor Dad" },
                    { 6, "Benjamin Graham", "9780060555665", new DateTime(1949, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Intelligent Investor" },
                    { 7, "Morgan Housel", "9780857197689", new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Psychology of Money" },
                    { 8, "C.S. Lewis", "9780060652920", new DateTime(1952, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mere Christianity" },
                    { 9, "Rick Warren", "9780310337508", new DateTime(2002, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Purpose Driven Life" },
                    { 10, "J.I. Packer", "9780830816507", new DateTime(1973, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Knowing God" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
