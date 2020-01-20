using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class GenreIdChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "GenreId",
                "Genre",
                "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Id",
                "Genre",
                "GenreId");
        }
    }
}
