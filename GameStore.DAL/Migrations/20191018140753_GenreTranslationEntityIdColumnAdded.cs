using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class GenreTranslationEntityIdColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_GenreTranslation_Genre_GenreId",
                "GenreTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("94f11f4a-ff83-4610-9611-ef838600b3da"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("e0aa65a1-524c-42fb-b79e-373cd5c74086"));

            migrationBuilder.AddColumn<Guid>(
                "EntityTranslationId",
                "PublisherTranslation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "EntityTranslationId",
                "PlatformTranslation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                "GenreId",
                "GenreTranslation",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                "EntityTranslationId",
                "GenreTranslation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "EntityTranslationId",
                "GameTranslation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                "FK_GenreTranslation_Genre_GenreId",
                "GenreTranslation",
                "GenreId",
                "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_GenreTranslation_Genre_GenreId",
                "GenreTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("246dc2b7-f1d7-4408-aaad-0c2b7821e157"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("449aa031-22a5-4f07-be80-d744b8fe0d13"));

            migrationBuilder.DropColumn(
                "EntityTranslationId",
                "PublisherTranslation");

            migrationBuilder.DropColumn(
                "EntityTranslationId",
                "PlatformTranslation");

            migrationBuilder.DropColumn(
                "EntityTranslationId",
                "GenreTranslation");

            migrationBuilder.DropColumn(
                "EntityTranslationId",
                "GameTranslation");

            migrationBuilder.AlterColumn<Guid>(
                "GenreId",
                "GenreTranslation",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_GenreTranslation_Genre_GenreId",
                "GenreTranslation",
                "GenreId",
                "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
