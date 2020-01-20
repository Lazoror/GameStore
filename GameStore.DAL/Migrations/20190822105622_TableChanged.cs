using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class TableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GamePublisher");

            migrationBuilder.AddColumn<Guid>(
                "PublisherId",
                "Game",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                "IX_Game_PublisherId",
                "Game",
                "PublisherId");

            migrationBuilder.AddForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game",
                "PublisherId",
                "Publisher",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game");

            migrationBuilder.DropIndex(
                "IX_Game_PublisherId",
                "Game");

            migrationBuilder.DropColumn(
                "PublisherId",
                "Game");

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
    }
}
