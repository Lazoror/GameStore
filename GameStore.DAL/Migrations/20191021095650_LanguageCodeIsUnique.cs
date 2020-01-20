using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class LanguageCodeIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                "Code",
                "Language",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                "IX_Language_Code",
                "Language",
                "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_Language_Code",
                "Language");

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("1f3951c6-72a5-4066-a6f6-db27cd1fa904"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("28a5abe3-3c52-428b-bdcb-56a8e2bf9896"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("50f37a74-9e29-4f71-b3ce-54d9b0000fbe"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("66320ac6-bc77-43d3-a995-d95e705a3667"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("8baaa490-6412-45d6-8a21-a56a10f7edc3"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("916f0a3c-2b28-4b38-b399-1cb30879a4e1"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("94deb12e-27c3-4d8f-b112-bc29a0d0fe0c"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("b049c586-0150-49bb-8a89-287011bef381"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("b4fdaf40-f26c-485c-b9f8-29ca1eadd9ec"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("c0c8577e-743d-4da2-85b4-dc758e1ae6a3"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("db946c24-fe97-4ad9-a678-e498f2cd5748"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("e2baeb1f-f4a5-4d78-bad3-233fcbafe51a"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("34e4f7c2-b0da-4d65-96d7-edb3a41a1d32"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("e8f227e1-f9a7-40f3-841f-bd48d02137ea"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("2fe048ee-4aa3-48b2-8c40-4002ec34d69c"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("79aefe93-4b3d-48fe-9c44-b9770c487cb2"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("9e808ad2-e7e6-448f-9f9b-d086c70def9f"));

            migrationBuilder.AlterColumn<string>(
                "Code",
                "Language",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
