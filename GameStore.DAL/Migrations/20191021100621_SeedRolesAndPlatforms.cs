using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class SeedRolesAndPlatforms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                "Name",
                "Role",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                "IX_Role_Name",
                "Role",
                "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_Role_Name",
                "Role");

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("1bde550f-2a67-4ce4-89e7-4b26a39ce15b"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("22ce4723-5532-4361-9563-94dbaec986df"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("27c420d9-5498-4dd5-8383-7ad85ec969d4"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("2ba1898d-9807-4509-8bac-c10f1763db75"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("43ec0a0c-d260-4283-9d9d-c99e6665a160"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("47c65fd0-0195-4467-9ba9-ba24238d0a5b"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("4bd1b457-02fe-4aa7-bca9-6c6bb450ec07"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("57b1a1fc-6ac1-4350-9a2f-d728509819f7"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("ac310abf-b580-4cb6-a823-5c413208f1b6"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("c3ffa9b6-5a78-4210-be74-fe111d8addb2"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("e28b9c16-033b-4ba0-bbf4-f41c3e796822"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("e98bbacf-b65d-4fbb-bc0f-081b02aa5f60"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("0a4909e4-a7fd-4fe9-a027-f0bb4542de6c"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("a7d10b70-3c5a-44bd-bb44-1a01ac0e5dca"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("89a6045f-5454-4ea6-8dec-178cf0ba6921"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("bd17cf37-c80e-4f59-a972-7a7e93865186"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("ef5460cc-fd2b-4561-89ea-5c2f925bd1f6"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("09694660-a54e-4075-b340-231c11bfbab5"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("0f8b7be1-532e-4f7a-93fd-30c38998e416"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("afef83be-aa97-4e59-bcc2-3185a522e8e1"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("e23fab55-c2f7-47cd-8d95-834426442f09"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("20bd0c9c-bf3e-430b-a51b-6d0990cbeae1"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("98e34ed6-4e0d-4672-b6dc-a520523d217d"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("fa5e667c-1aa9-437a-b930-2071a6f032b3"));

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Role",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
