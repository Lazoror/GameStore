using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class AddedGameStateEntityForViewcountAndComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ViewCount",
                "Game");

            migrationBuilder.AlterColumn<Guid>(
                "GameId",
                "Comment",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                "GameStateId",
                "Comment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                "GameState",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GameKey = table.Column<string>(nullable: true),
                    ViewCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                "IX_Comment_GameStateId",
                "Comment",
                "GameStateId");

            migrationBuilder.AddForeignKey(
                "FK_Comment_GameState_GameStateId",
                "Comment",
                "GameStateId",
                "GameState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comment_Game_GameId",
                "Comment");

            migrationBuilder.DropForeignKey(
                "FK_Comment_GameState_GameStateId",
                "Comment");

            migrationBuilder.DropTable(
                "GameState");

            migrationBuilder.DropIndex(
                "IX_Comment_GameStateId",
                "Comment");

            migrationBuilder.DropColumn(
                "GameStateId",
                "Comment");

            migrationBuilder.AddColumn<int>(
                "ViewCount",
                "Game",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                "GameId",
                "Comment",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
