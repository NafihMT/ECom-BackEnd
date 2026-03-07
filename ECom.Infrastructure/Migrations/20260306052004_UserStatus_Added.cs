using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserStatus_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsBlocked", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 6, 5, 20, 2, 810, DateTimeKind.Utc).AddTicks(5314), false, "$2a$11$8bGr49AlrE/zfPjoR3BZ9eUSBjOK95/RStpuZUd5NjGUZ23I9auvi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 4, 5, 58, 32, 578, DateTimeKind.Utc).AddTicks(6055), "$2a$11$GuJ6Df/tqo.XlqPmdOHdwu/.wt3bOQAtplbq1yc4GoMJj8C7ctRdC" });
        }
    }
}
