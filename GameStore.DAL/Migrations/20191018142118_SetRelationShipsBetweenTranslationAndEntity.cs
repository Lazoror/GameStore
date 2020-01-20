using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class SetRelationShipsBetweenTranslationAndEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_GameTranslation_Game_GameId",
                "GameTranslation");

            migrationBuilder.DropForeignKey(
                "FK_GenreTranslation_Genre_GenreId",
                "GenreTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PlatformTranslation_Platform_PlatformId",
                "PlatformTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PublisherTranslation_Publisher_PublisherId",
                "PublisherTranslation");

            migrationBuilder.DropIndex(
                "IX_PublisherTranslation_PublisherId",
                "PublisherTranslation");

            migrationBuilder.DropIndex(
                "IX_PlatformTranslation_PlatformId",
                "PlatformTranslation");

            migrationBuilder.DropIndex(
                "IX_GenreTranslation_GenreId",
                "GenreTranslation");

            migrationBuilder.DropIndex(
                "IX_GameTranslation_GameId",
                "GameTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("246dc2b7-f1d7-4408-aaad-0c2b7821e157"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("449aa031-22a5-4f07-be80-d744b8fe0d13"));

            migrationBuilder.DropColumn(
                "PublisherId",
                "PublisherTranslation");

            migrationBuilder.DropColumn(
                "PlatformId",
                "PlatformTranslation");

            migrationBuilder.DropColumn(
                "GenreId",
                "GenreTranslation");

            migrationBuilder.DropColumn(
                "GameId",
                "GameTranslation");

            migrationBuilder.CreateIndex(
                "IX_PublisherTranslation_EntityTranslationId",
                "PublisherTranslation",
                "EntityTranslationId");

            migrationBuilder.CreateIndex(
                "IX_PlatformTranslation_EntityTranslationId",
                "PlatformTranslation",
                "EntityTranslationId");

            migrationBuilder.CreateIndex(
                "IX_GenreTranslation_EntityTranslationId",
                "GenreTranslation",
                "EntityTranslationId");

            migrationBuilder.CreateIndex(
                "IX_GameTranslation_EntityTranslationId",
                "GameTranslation",
                "EntityTranslationId");

            migrationBuilder.AddForeignKey(
                "FK_GameTranslation_Game_EntityTranslationId",
                "GameTranslation",
                "EntityTranslationId",
                "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_GenreTranslation_Genre_EntityTranslationId",
                "GenreTranslation",
                "EntityTranslationId",
                "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_PlatformTranslation_Platform_EntityTranslationId",
                "PlatformTranslation",
                "EntityTranslationId",
                "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_PublisherTranslation_Publisher_EntityTranslationId",
                "PublisherTranslation",
                "EntityTranslationId",
                "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_GameTranslation_Game_EntityTranslationId",
                "GameTranslation");

            migrationBuilder.DropForeignKey(
                "FK_GenreTranslation_Genre_EntityTranslationId",
                "GenreTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PlatformTranslation_Platform_EntityTranslationId",
                "PlatformTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PublisherTranslation_Publisher_EntityTranslationId",
                "PublisherTranslation");

            migrationBuilder.DropIndex(
                "IX_PublisherTranslation_EntityTranslationId",
                "PublisherTranslation");

            migrationBuilder.DropIndex(
                "IX_PlatformTranslation_EntityTranslationId",
                "PlatformTranslation");

            migrationBuilder.DropIndex(
                "IX_GenreTranslation_EntityTranslationId",
                "GenreTranslation");

            migrationBuilder.DropIndex(
                "IX_GameTranslation_EntityTranslationId",
                "GameTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("cec56795-5721-469c-bf54-c1744eaa415f"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("f5149e0d-8e70-4df1-8768-00bd592d0926"));

            migrationBuilder.AddColumn<Guid>(
                "PublisherId",
                "PublisherTranslation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "PlatformId",
                "PlatformTranslation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "GenreId",
                "GenreTranslation",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                "GameId",
                "GameTranslation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                "IX_PublisherTranslation_PublisherId",
                "PublisherTranslation",
                "PublisherId");

            migrationBuilder.CreateIndex(
                "IX_PlatformTranslation_PlatformId",
                "PlatformTranslation",
                "PlatformId");

            migrationBuilder.CreateIndex(
                "IX_GenreTranslation_GenreId",
                "GenreTranslation",
                "GenreId");

            migrationBuilder.CreateIndex(
                "IX_GameTranslation_GameId",
                "GameTranslation",
                "GameId");

            migrationBuilder.AddForeignKey(
                "FK_GameTranslation_Game_GameId",
                "GameTranslation",
                "GameId",
                "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_GenreTranslation_Genre_GenreId",
                "GenreTranslation",
                "GenreId",
                "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_PlatformTranslation_Platform_PlatformId",
                "PlatformTranslation",
                "PlatformId",
                "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_PublisherTranslation_Publisher_PublisherId",
                "PublisherTranslation",
                "PublisherId",
                "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
