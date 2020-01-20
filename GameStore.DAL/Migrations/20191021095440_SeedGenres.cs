using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class SeedGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("73ca2e42-7004-4408-8d63-2fbd07c667d3"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("f27d0895-ced1-4839-80a0-2431849c8946"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("0e7904af-98c5-4d99-8f46-7def293f3139"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("1201df43-8e8d-47b5-8044-52536449e9f1"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("195cd41a-ba44-447a-a650-9557afecd707"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("1d1cfc25-9df3-4397-9ffe-5adf74446bb4"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("2afc49ef-f084-45bb-8770-9e18fc6fc4ef"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("747bb2ac-e5e9-42e5-ac80-b04d474c8f79"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("98b37907-d6db-406d-bf6e-4783f64da9a9"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("9c60bc78-c242-4f86-8d0c-7239b58e8dd0"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("a40b825d-351e-f2fc-715c-0e1405050505"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("a40b825d-351e-f2fc-715c-0e1505050505"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("a40b825d-351e-f2fc-715c-0e1605050505"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("a40b825d-351e-f2fc-715c-0e1705050505"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("a40b825d-351e-f2fc-715c-0e1805050505"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("a40b825d-351e-f2fc-715c-0e1905050505"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("a40b825d-351e-f2fc-715c-0e1a05050505"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("a40b825d-351e-f2fc-715c-0e1b05050505"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("bd255c44-c46d-4ef3-bde8-079050e4dd79"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("ead19086-69d4-440b-bab1-ac64e0bb43dc"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("f6f910cc-ed06-4e90-a72e-2f362ed77514"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("f7f2093d-46fb-4a87-9645-3cccf031db3b"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("340435df-1b6d-42da-b73e-8eea55c4a8b2"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("55cb44d9-b9fa-49f4-90d9-5c3a20013ac2"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("671fbbf8-4f4e-46fb-bc5b-0a20beeaf223"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("c0304b57-20be-4dc5-b406-156458d3c92b"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("e103084d-937f-4637-bd86-3a0423fd06b7"));
        }
    }
}
