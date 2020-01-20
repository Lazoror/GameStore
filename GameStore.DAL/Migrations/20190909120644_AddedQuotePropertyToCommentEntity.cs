using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class AddedQuotePropertyToCommentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsQuote",
                "Comment");

            migrationBuilder.AddColumn<string>(
                "Quote",
                "Comment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Quote",
                "Comment");

            migrationBuilder.AddColumn<bool>(
                "IsQuote",
                "Comment",
                nullable: false,
                defaultValue: false);
        }
    }
}
