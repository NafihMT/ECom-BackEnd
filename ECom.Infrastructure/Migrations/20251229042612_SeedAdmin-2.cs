using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 4, 26, 11, 663, DateTimeKind.Utc).AddTicks(9106), "admin@ecom.com", "$2a$11$TjhKRDmnnROdGBGiYYiH..HVWwikImrhs9DRaekhnf/W8sTHbOpuS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "PasswordHash" },
                values: new object[] { new DateTime(2025, 12, 29, 4, 19, 54, 608, DateTimeKind.Utc).AddTicks(8956), "admin@gmail.com", "$2a$11$LNiZcVY97KIUpsdhlTeGhezaMSU/sdDqTuJnow6Hu0KFGWuebt31q" });
        }
    }
}
