using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class GameLangRenamedToGameTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GameLang");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("87e9188c-9aa5-48cc-abe4-a0471668b97c"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("9f63ee38-17bd-441b-8e07-b1de5cf64b65"));

            migrationBuilder.CreateTable(
                "GameTranslation",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTranslation", x => x.Id);
                    table.ForeignKey(
                        "FK_GameTranslation_Game_GameId",
                        x => x.GameId,
                        "Game",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_GameTranslation_GameId",
                "GameTranslation",
                "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GameTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("2cbd46ca-22c8-4335-93cb-cd74726841cb"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("54b44948-3638-45c0-840b-4abd991c2606"));

            migrationBuilder.CreateTable(
                "GameLang",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GameId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLang", x => x.Id);
                    table.ForeignKey(
                        "FK_GameLang_Game_GameId",
                        x => x.GameId,
                        "Game",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_GameLang_GameId",
                "GameLang",
                "GameId");
        }
    }
}
