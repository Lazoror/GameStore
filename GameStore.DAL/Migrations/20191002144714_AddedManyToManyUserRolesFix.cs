using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class AddedManyToManyUserRolesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_UserRoles_Role_RoleId",
                "UserRoles");

            migrationBuilder.DropForeignKey(
                "FK_UserRoles_User_UserId",
                "UserRoles");

            migrationBuilder.DropPrimaryKey(
                "PK_UserRoles",
                "UserRoles");

            migrationBuilder.RenameTable(
                "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameIndex(
                "IX_UserRoles_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                "PK_UserRole",
                "UserRole",
                new[] { "UserId", "RoleId" });

            migrationBuilder.AddForeignKey(
                "FK_UserRole_Role_RoleId",
                "UserRole",
                "RoleId",
                "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_UserRole_User_UserId",
                "UserRole",
                "UserId",
                "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_UserRole_Role_RoleId",
                "UserRole");

            migrationBuilder.DropForeignKey(
                "FK_UserRole_User_UserId",
                "UserRole");

            migrationBuilder.DropPrimaryKey(
                "PK_UserRole",
                "UserRole");

            migrationBuilder.RenameTable(
                "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameIndex(
                "IX_UserRole_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                "PK_UserRoles",
                "UserRoles",
                new[] { "UserId", "RoleId" });

            migrationBuilder.AddForeignKey(
                "FK_UserRoles_Role_RoleId",
                "UserRoles",
                "RoleId",
                "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_UserRoles_User_UserId",
                "UserRoles",
                "UserId",
                "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
