using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GameStore.DAL.Migrations
{
    public partial class AddedManyToManyUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Role_User_UserId",
                "Role");

            migrationBuilder.DropIndex(
                "IX_Role_UserId",
                "Role");

            migrationBuilder.DropColumn(
                "UserId",
                "Role");

            migrationBuilder.AlterColumn<string>(
                "Email",
                "User",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                "UserRoles",
                table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        "FK_UserRoles_Role_RoleId",
                        x => x.RoleId,
                        "Role",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserRoles_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_User_Email",
                "User",
                "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_UserRoles_RoleId",
                "UserRoles",
                "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UserRoles");

            migrationBuilder.DropIndex(
                "IX_User_Email",
                "User");

            migrationBuilder.AlterColumn<string>(
                "Email",
                "User",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                "UserId",
                "Role",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Role_UserId",
                "Role",
                "UserId");

            migrationBuilder.AddForeignKey(
                "FK_Role_User_UserId",
                "Role",
                "UserId",
                "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
