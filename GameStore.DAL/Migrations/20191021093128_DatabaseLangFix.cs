using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class DatabaseLangFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Comment");

            migrationBuilder.DropTable(
                "GameGenre");

            migrationBuilder.DropTable(
                "GamePlatform");

            migrationBuilder.DropTable(
                "GameTranslation");

            migrationBuilder.DropTable(
                "GenreTranslation");

            migrationBuilder.DropTable(
                "OrderDetail");

            migrationBuilder.DropTable(
                "PlatformTranslation");

            migrationBuilder.DropTable(
                "PublisherTranslation");

            migrationBuilder.DropTable(
                "UserRole");

            migrationBuilder.DropTable(
                "Game");

            migrationBuilder.DropTable(
                "Genre");

            migrationBuilder.DropTable(
                "Order");

            migrationBuilder.DropTable(
                "Platform");

            migrationBuilder.DropTable(
                "Language");

            migrationBuilder.DropTable(
                "Role");

            migrationBuilder.DropTable(
                "User");

            migrationBuilder.DropTable(
                "GameState");

            migrationBuilder.DropTable(
                "Publisher");
        }
    }
}
