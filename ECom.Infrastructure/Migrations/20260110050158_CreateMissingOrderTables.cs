using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateMissingOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "Orders",
    columns: table => new
    {
        Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        UserId = table.Column<int>(nullable: false),
        Status = table.Column<int>(nullable: false),
        TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        CreatedAt = table.Column<DateTime>(nullable: false),
        UpdatedAt = table.Column<DateTime>(nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Orders", x => x.Id);
        table.ForeignKey(
            name: "FK_Orders_Users_UserId",
            column: x => x.UserId,
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    });

            migrationBuilder.CreateTable(
    name: "OrderItems",
    columns: table => new
    {
        Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        OrderId = table.Column<int>(nullable: false),
        ProductId = table.Column<int>(nullable: false),
        Quantity = table.Column<int>(nullable: false),
        Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        CreatedAt = table.Column<DateTime>(nullable: false),
        UpdatedAt = table.Column<DateTime>(nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_OrderItems", x => x.Id);
        table.ForeignKey(
            name: "FK_OrderItems_Orders_OrderId",
            column: x => x.OrderId,
            principalTable: "Orders",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        table.ForeignKey(
            name: "FK_OrderItems_Products_ProductId",
            column: x => x.ProductId,
            principalTable: "Products",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 5, 1, 57, 199, DateTimeKind.Utc).AddTicks(6227), "$2a$11$ajAvgeZW0p9.exzork3x0.5e/pAuOM9vgwnbZQHBKL2ulfGn8TKMy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 10, 4, 58, 23, 804, DateTimeKind.Utc).AddTicks(9043), "$2a$11$BXjkrJGvPdua05Lib5H6g.12uJIsQwDU6udcoWDu0tGmPEvd2qhEW" });
        }
    }
}
