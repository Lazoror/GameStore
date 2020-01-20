using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class IsQuoteAddedToComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game");

            migrationBuilder.AlterColumn<Guid>(
                "PublisherId",
                "Game",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsQuote",
                "Comment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                "IX_Genre_ParentGenreId",
                "Genre",
                "ParentGenreId");

            migrationBuilder.CreateIndex(
                "IX_Comment_ParentCommentId",
                "Comment",
                "ParentCommentId");

            migrationBuilder.AddForeignKey(
                "FK_Comment_Comment_ParentCommentId",
                "Comment",
                "ParentCommentId",
                "Comment",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game",
                "PublisherId",
                "Publisher",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Genre_Genre_ParentGenreId",
                "Genre",
                "ParentGenreId",
                "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comment_Comment_ParentCommentId",
                "Comment");

            migrationBuilder.DropForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game");

            migrationBuilder.DropForeignKey(
                "FK_Genre_Genre_ParentGenreId",
                "Genre");

            migrationBuilder.DropIndex(
                "IX_Genre_ParentGenreId",
                "Genre");

            migrationBuilder.DropIndex(
                "IX_Comment_ParentCommentId",
                "Comment");

            migrationBuilder.DropColumn(
                "IsQuote",
                "Comment");

            migrationBuilder.AlterColumn<Guid>(
                "PublisherId",
                "Game",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                "FK_Game_Publisher_PublisherId",
                "Game",
                "PublisherId",
                "Publisher",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
