using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class OrderEntityModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsCompleted",
                "Order");

            migrationBuilder.AlterColumn<DateTime>(
                "OrderDate",
                "Order",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                "OrderStatus",
                "Order",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "OrderStatus",
                "Order");

            migrationBuilder.AlterColumn<DateTime>(
                "OrderDate",
                "Order",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<bool>(
                "IsCompleted",
                "Order",
                nullable: false,
                defaultValue: false);
        }
    }
}
