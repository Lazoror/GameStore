using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class Order_Modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
