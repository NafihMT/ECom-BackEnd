using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Imageurl_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "ImageUrl");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 18, 6, 31, 2, 16, DateTimeKind.Utc).AddTicks(7215), "$2a$11$T7RbKfnCxbUclgpV5/Dr7ebIAJmmNx.270ZhSAf.8IHqUbDCAL/x6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "Image");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 16, 17, 0, 8, 871, DateTimeKind.Utc).AddTicks(9996), "$2a$11$lvpvtx9v2uUwW8AuUnJql.jQ2Xr7hsy9Vsjq9zsurPNlvw8rydUxi" });
        }
    }
}
