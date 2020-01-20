using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class DeletedPublisherIdPropertyFromGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game");

            migrationBuilder.AlterColumn<Guid>(
                "PublisherId",
                "Game",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game",
                "PublisherId",
                "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game");

            migrationBuilder.AlterColumn<Guid>(
                "PublisherId",
                "Game",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game",
                "PublisherId",
                "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
