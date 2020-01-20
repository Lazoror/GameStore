using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class AddedPlatformTypeLangEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("02abd65f-c833-446f-942a-29fc498ebc2f"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("cf9fb8b5-88c1-4eee-84e4-1a210a1eb32c"));

            migrationBuilder.CreateTable(
                "PlatformTypeLang",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlatformTypeId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformTypeLang", x => x.Id);
                    table.ForeignKey(
                        "FK_PlatformTypeLang_PlatformType_PlatformTypeId",
                        x => x.PlatformTypeId,
                        "PlatformType",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_PublisherLang_PublisherId",
                "PublisherLang",
                "PublisherId");

            migrationBuilder.CreateIndex(
                "IX_PlatformTypeLang_PlatformTypeId",
                "PlatformTypeLang",
                "PlatformTypeId");

            migrationBuilder.AddForeignKey(
                "FK_PublisherLang_Publisher_PublisherId",
                "PublisherLang",
                "PublisherId",
                "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_PublisherLang_Publisher_PublisherId",
                "PublisherLang");

            migrationBuilder.DropTable(
                "PlatformTypeLang");

            migrationBuilder.DropIndex(
                "IX_PublisherLang_PublisherId",
                "PublisherLang");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("721e5202-4997-4b7c-ac25-520a522c956f"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("de621d94-4839-44ae-a481-0f72844301c7"));
        }
    }
}
