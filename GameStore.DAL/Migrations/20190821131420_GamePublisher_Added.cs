using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class GamePublisher_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_Publishers",
                "Publishers");

            migrationBuilder.RenameTable(
                "Publishers",
                newName: "Publisher");

            migrationBuilder.AddPrimaryKey(
                "PK_Publisher",
                "Publisher",
                "PublisherId");

            migrationBuilder.CreateTable(
                "GamePublisher",
                table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePublisher", x => new { x.GameId, x.PublisherId });
                    table.ForeignKey(
                        "FK_GamePublisher_Game_GameId",
                        x => x.GameId,
                        "Game",
                        "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_GamePublisher_Publisher_PublisherId",
                        x => x.PublisherId,
                        "Publisher",
                        "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_GamePublisher_PublisherId",
                "GamePublisher",
                "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GamePublisher");

            migrationBuilder.DropPrimaryKey(
                "PK_Publisher",
                "Publisher");

            migrationBuilder.RenameTable(
                "Publisher",
                newName: "Publishers");

            migrationBuilder.AddPrimaryKey(
                "PK_Publishers",
                "Publishers",
                "PublisherId");
        }
    }
}
