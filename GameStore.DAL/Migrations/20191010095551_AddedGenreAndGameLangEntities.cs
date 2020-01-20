using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class AddedGenreAndGameLangEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("721e5202-4997-4b7c-ac25-520a522c956f"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("de621d94-4839-44ae-a481-0f72844301c7"));

            migrationBuilder.CreateTable(
                "GameLang",
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
                    table.PrimaryKey("PK_GameLang", x => x.Id);
                    table.ForeignKey(
                        "FK_GameLang_Game_GameId",
                        x => x.GameId,
                        "Game",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "GenreLang",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreLang", x => x.Id);
                    table.ForeignKey(
                        "FK_GenreLang_Genre_GenreId",
                        x => x.GenreId,
                        "Genre",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_GameLang_GameId",
                "GameLang",
                "GameId");

            migrationBuilder.CreateIndex(
                "IX_GenreLang_GenreId",
                "GenreLang",
                "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GameLang");

            migrationBuilder.DropTable(
                "GenreLang");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("2cb78d39-26f3-4516-9061-95e88b2928b6"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("52c7a79c-53dd-4c4d-a608-78e7fa52a1a8"));
        }
    }
}
