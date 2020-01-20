using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class PublisherLangEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("6cfd292d-9c4c-4baa-9f92-91772bb93093"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("c797f2a5-e802-4c40-9657-1dc4d9f9c94d"));

            migrationBuilder.CreateTable(
                "PublisherLang",
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
                    table.PrimaryKey("PK_PublisherLang", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PublisherLang");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("02abd65f-c833-446f-942a-29fc498ebc2f"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("cf9fb8b5-88c1-4eee-84e4-1a210a1eb32c"));
        }
    }
}
