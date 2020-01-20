using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class PlatformTranslationColumnTypeRenamedToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_GameTranslation_Language_LanguageId",
                "GameTranslation");

            migrationBuilder.DropForeignKey(
                "FK_GenreTranslation_Language_LanguageId",
                "GenreTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PlatformTranslation_Language_LanguageId",
                "PlatformTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PublisherTranslation_Language_LanguageId",
                "PublisherTranslation");

            migrationBuilder.DropIndex(
                "IX_PublisherTranslation_LanguageId",
                "PublisherTranslation");

            migrationBuilder.DropIndex(
                "IX_PlatformTranslation_LanguageId",
                "PlatformTranslation");

            migrationBuilder.DropIndex(
                "IX_GenreTranslation_LanguageId",
                "GenreTranslation");

            migrationBuilder.DropIndex(
                "IX_GameTranslation_LanguageId",
                "GameTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("06831c22-60de-45c2-996f-36d493f921d8"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("191694ec-5530-492d-b010-3761d3f08fd9"));

            migrationBuilder.RenameColumn(
                "Name",
                "PlatformTranslation",
                "Type");
        }
    }
}
