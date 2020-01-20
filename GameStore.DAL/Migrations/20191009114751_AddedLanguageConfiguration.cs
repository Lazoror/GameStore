using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class AddedLanguageConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_Languages",
                "Languages");

            migrationBuilder.RenameTable(
                "Languages",
                newName: "Language");

            migrationBuilder.AddPrimaryKey(
                "PK_Language",
                "Language",
                "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_Language",
                "Language");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("6cfd292d-9c4c-4baa-9f92-91772bb93093"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("c797f2a5-e802-4c40-9657-1dc4d9f9c94d"));

            migrationBuilder.RenameTable(
                "Language",
                newName: "Languages");

            migrationBuilder.AddPrimaryKey(
                "PK_Languages",
                "Languages",
                "Id");
        }
    }
}
