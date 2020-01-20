using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class Publisher_Entity_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Game",
                table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>("Money", nullable: false),
                    UnitsInStock = table.Column<short>("smallint", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Discontinued = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                "Genre",
                table => new
                {
                    GenreId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ParentGenreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                "PlatformType",
                table => new
                {
                    PlatformTypeId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformType", x => x.PlatformTypeId);
                });

            migrationBuilder.CreateTable(
                "Publishers",
                table => new
                {
                    PublisherId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>("nvarchar(40)", nullable: true),
                    Description = table.Column<string>("Ntext", nullable: true),
                    HomePage = table.Column<string>("Ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                "Comment",
                table => new
                {
                    CommentId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ParentCommentId = table.Column<Guid>(nullable: true),
                    GameId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        "FK_Comment_Game_GameId",
                        x => x.GameId,
                        "Game",
                        "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "GameGenre",
                table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GameId, x.GenreId });
                    table.ForeignKey(
                        "FK_GameGenre_Game_GameId",
                        x => x.GameId,
                        "Game",
                        "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_GameGenre_Genre_GenreId",
                        x => x.GenreId,
                        "Genre",
                        "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "GamePlatform",
                table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    PlatformTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.GameId, x.PlatformTypeId });
                    table.ForeignKey(
                        "FK_GamePlatform_Game_GameId",
                        x => x.GameId,
                        "Game",
                        "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_GamePlatform_PlatformType_PlatformTypeId",
                        x => x.PlatformTypeId,
                        "PlatformType",
                        "PlatformTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Comment_GameId",
                "Comment",
                "GameId");

            migrationBuilder.CreateIndex(
                "IX_Game_Key",
                "Game",
                "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_GameGenre_GenreId",
                "GameGenre",
                "GenreId");

            migrationBuilder.CreateIndex(
                "IX_GamePlatform_PlatformTypeId",
                "GamePlatform",
                "PlatformTypeId");

            migrationBuilder.CreateIndex(
                "IX_Genre_Name",
                "Genre",
                "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_PlatformType_Type",
                "PlatformType",
                "Type",
                unique: true,
                filter: "[Type] IS NOT NULL");
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
                "Publishers");

            migrationBuilder.DropTable(
                "Genre");

            migrationBuilder.DropTable(
                "Game");

            migrationBuilder.DropTable(
                "PlatformType");
        }
    }
}
