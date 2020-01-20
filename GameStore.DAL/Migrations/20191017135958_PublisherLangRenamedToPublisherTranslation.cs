using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class PublisherLangRenamedToPublisherTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PublisherLang");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("50f6512b-5264-4d78-ad5c-e93a3932f790"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("b847fa49-938a-4974-b257-bd625733a254"));

            migrationBuilder.CreateTable(
                "PublisherTranslation",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HomePage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherTranslation", x => x.Id);
                    table.ForeignKey(
                        "FK_PublisherTranslation_Publisher_PublisherId",
                        x => x.PublisherId,
                        "Publisher",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_PublisherTranslation_PublisherId",
                "PublisherTranslation",
                "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PublisherTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("5f0222ec-0e3a-4fb2-8da4-cc12a4414a1a"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("b7bfc9bb-507e-456e-baf9-1b31d3538c7d"));

            migrationBuilder.CreateTable(
                "PublisherLang",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HomePage = table.Column<string>(nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherLang", x => x.Id);
                    table.ForeignKey(
                        "FK_PublisherLang_Publisher_PublisherId",
                        x => x.PublisherId,
                        "Publisher",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_PublisherLang_PublisherId",
                "PublisherLang",
                "PublisherId");
        }
    }
}
