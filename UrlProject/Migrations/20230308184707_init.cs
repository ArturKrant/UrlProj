using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UrlProject.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlUsageLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlUsageLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplexUrls",
                columns: table => new
                {
                    FullUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShortUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfUses = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexUrls", x => new { x.FullUrl, x.ShortUrl });
                    table.ForeignKey(
                        name: "FK_ComplexUrls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ComplexUrls",
                columns: new[] { "FullUrl", "ShortUrl", "NumberOfUses", "UserId" },
                values: new object[] { "https://www.google.com/", "https://localhost:7212/s/add1sd", 0, null });

            migrationBuilder.InsertData(
                table: "UrlUsageLogs",
                columns: new[] { "Id", "IpAdress", "ShortUrl", "Time" },
                values: new object[,]
                {
                    { 1, "192.158.1.38", "https://localhost:7212/s/add1sd", new DateTime(2023, 3, 8, 20, 47, 7, 166, DateTimeKind.Local).AddTicks(5457) },
                    { 2, "192.158.1.38", "https://localhost:7212/s/adw13q", new DateTime(2023, 3, 8, 20, 47, 7, 166, DateTimeKind.Local).AddTicks(5503) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, "Artur@gmail.com", "Artur", "Krant", "123456" },
                    { 2, "Moshe@gmail.com", "Moshe", "Levi", "857342" },
                    { 3, "Bob@gmail.com", "Bob", "Dosomething", "098765" },
                    { 4, "Lebron@gmail.com", "Lebron", "James", "847231" },
                    { 5, "admin@admin.com", "Admin", "Admin", "admin" }
                });

            migrationBuilder.InsertData(
                table: "ComplexUrls",
                columns: new[] { "FullUrl", "ShortUrl", "NumberOfUses", "UserId" },
                values: new object[,]
                {
                    { "https://translate.google.com/?hl=iw&sl=en&tl=iw&op=translate", "https://localhost:7212/s/adw13q", 0, 1 },
                    { "https://www.youtube.com/", "https://localhost:7212/s/41gHlq", 0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplexUrls_UserId",
                table: "ComplexUrls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplexUrls");

            migrationBuilder.DropTable(
                name: "UrlUsageLogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
