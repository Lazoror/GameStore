using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class GenreLangRenamedToGenreTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GenreLang");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("5f0222ec-0e3a-4fb2-8da4-cc12a4414a1a"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("b7bfc9bb-507e-456e-baf9-1b31d3538c7d"));

            migrationBuilder.CreateTable(
                "GenreTranslation",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreTranslation", x => x.Id);
                    table.ForeignKey(
                        "FK_GenreTranslation_Genre_GenreId",
                        x => x.GenreId,
                        "Genre",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_GenreTranslation_GenreId",
                "GenreTranslation",
                "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "GenreTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("87e9188c-9aa5-48cc-abe4-a0471668b97c"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("9f63ee38-17bd-441b-8e07-b1de5cf64b65"));

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
                "IX_GenreLang_GenreId",
                "GenreLang",
                "GenreId");
        }
    }
}
