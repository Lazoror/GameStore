using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class ChangedPlatformTranslatinColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("49bada3c-a5fe-4cd0-acad-ca2ca5cd89c8"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("96876f1d-535c-4d72-97cb-54c405af1852"));

            migrationBuilder.RenameColumn(
                "Type",
                "PlatformTranslation",
                "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("73ca2e42-7004-4408-8d63-2fbd07c667d3"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("f27d0895-ced1-4839-80a0-2431849c8946"));

            migrationBuilder.RenameColumn(
                "Name",
                "PlatformTranslation",
                "Type");

            //migrationBuilder.InsertData(
            //    table: "Language",
            //    columns: new[] { "Id", "Code", "Name" },
            //    values: new object[] { new Guid("49bada3c-a5fe-4cd0-acad-ca2ca5cd89c8"), "en", "English" });

            //migrationBuilder.InsertData(
            //    table: "Language",
            //    columns: new[] { "Id", "Code", "Name" },
            //    values: new object[] { new Guid("96876f1d-535c-4d72-97cb-54c405af1852"), "ru", "Russian" });
        }
    }
}
