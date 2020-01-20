using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class OrdersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Order",
                table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                "OrderDetail",
                table => new
                {
                    OrderDetailId = table.Column<Guid>(nullable: false),
                    GameKey = table.Column<string>("nvarchar(40)", nullable: true),
                    Price = table.Column<decimal>("Money", nullable: false),
                    Quantity = table.Column<short>("smallint", nullable: false),
                    Discount = table.Column<float>("Real", nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        "FK_OrderDetail_Order_OrderId",
                        x => x.OrderId,
                        "Order",
                        "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_OrderDetail_OrderId",
                "OrderDetail",
                "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "OrderDetail");

            migrationBuilder.DropTable(
                "Order");
        }
    }
}
