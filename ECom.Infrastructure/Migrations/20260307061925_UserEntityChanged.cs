using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserEntityChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 7, 6, 19, 23, 309, DateTimeKind.Utc).AddTicks(1077), "$2a$11$DAy1Rk641lmXEsQBBdipdeB135ZLGMPRmgDhkot8sqLBHvqB8YJYa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 6, 5, 20, 2, 810, DateTimeKind.Utc).AddTicks(5314), "$2a$11$8bGr49AlrE/zfPjoR3BZ9eUSBjOK95/RStpuZUd5NjGUZ23I9auvi" });
        }
    }
}
