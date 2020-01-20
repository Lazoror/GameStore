using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class PlatformIdChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "PlatformTypeId",
                "PlatformType",
                "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Id",
                "PlatformType",
                "PlatformTypeId");
        }
    }
}
