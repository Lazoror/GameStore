using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class AddedGameStateToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                "GameStateId",
                "Game",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Game_GameStateId",
                "Game",
                "GameStateId");

            migrationBuilder.AddForeignKey(
                "FK_Game_GameState_GameStateId",
                "Game",
                "GameStateId",
                "GameState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Game_GameState_GameStateId",
                "Game");

            migrationBuilder.DropIndex(
                "IX_Game_GameStateId",
                "Game");

            migrationBuilder.DropColumn(
                "GameStateId",
                "Game");
        }
    }
}
