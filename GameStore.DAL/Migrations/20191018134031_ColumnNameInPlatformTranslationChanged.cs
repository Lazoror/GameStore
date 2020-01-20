using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class ColumnNameInPlatformTranslationChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_PlatformTranslation_Platform_PlatformId",
                "PlatformTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("2cbd46ca-22c8-4335-93cb-cd74726841cb"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("54b44948-3638-45c0-840b-4abd991c2606"));

            migrationBuilder.DropColumn(
                "PlatformTypeId",
                "PlatformTranslation");

            migrationBuilder.AlterColumn<Guid>(
                "PlatformId",
                "PlatformTranslation",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_PlatformTranslation_Platform_PlatformId",
                "PlatformTranslation",
                "PlatformId",
                "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_PlatformTranslation_Platform_PlatformId",
                "PlatformTranslation");

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("94f11f4a-ff83-4610-9611-ef838600b3da"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("e0aa65a1-524c-42fb-b79e-373cd5c74086"));

            migrationBuilder.AlterColumn<Guid>(
                "PlatformId",
                "PlatformTranslation",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                "PlatformTypeId",
                "PlatformTranslation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                "FK_PlatformTranslation_Platform_PlatformId",
                "PlatformTranslation",
                "PlatformId",
                "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
