using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class PlatformTypeLangRenamedToPlatformTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PlatformTypeLang");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("bb705c7b-4c22-4dd7-9ec6-550358d5160f"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("d9bab77b-c3e0-4f8d-965a-80bdc5c10a85"));

            migrationBuilder.CreateTable(
                "PlatformTranslation",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlatformTypeId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    PlatformId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformTranslation", x => x.Id);
                    table.ForeignKey(
                        "FK_PlatformTranslation_Platform_PlatformId",
                        x => x.PlatformId,
                        "Platform",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_PlatformTranslation_PlatformId",
                "PlatformTranslation",
                "PlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PlatformTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("50f6512b-5264-4d78-ad5c-e93a3932f790"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("b847fa49-938a-4974-b257-bd625733a254"));

            migrationBuilder.CreateTable(
                "PlatformTypeLang",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    PlatformId = table.Column<Guid>(nullable: true),
                    PlatformTypeId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformTypeLang", x => x.Id);
                    table.ForeignKey(
                        "FK_PlatformTypeLang_Platform_PlatformId",
                        x => x.PlatformId,
                        "Platform",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_PlatformTypeLang_PlatformId",
                "PlatformTypeLang",
                "PlatformId");
        }
    }
}
