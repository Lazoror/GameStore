using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class DatabaseLangFixEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("49bada3c-a5fe-4cd0-acad-ca2ca5cd89c8"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("96876f1d-535c-4d72-97cb-54c405af1852"));
        }
    }
}
