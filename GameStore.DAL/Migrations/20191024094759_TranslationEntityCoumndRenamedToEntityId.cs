using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class TranslationEntityCoumndRenamedToEntityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_GameTranslation_Game_EntityTranslationId",
                "GameTranslation");

            migrationBuilder.DropForeignKey(
                "FK_GenreTranslation_Genre_EntityTranslationId",
                "GenreTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PlatformTranslation_Platform_EntityTranslationId",
                "PlatformTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PublisherTranslation_Publisher_EntityTranslationId",
                "PublisherTranslation");

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

            migrationBuilder.RenameColumn(
                "EntityTranslationId",
                "PublisherTranslation",
                "EntityId");

            migrationBuilder.RenameIndex(
                "IX_PublisherTranslation_EntityTranslationId",
                table: "PublisherTranslation",
                newName: "IX_PublisherTranslation_EntityId");

            migrationBuilder.RenameColumn(
                "EntityTranslationId",
                "PlatformTranslation",
                "EntityId");

            migrationBuilder.RenameIndex(
                "IX_PlatformTranslation_EntityTranslationId",
                table: "PlatformTranslation",
                newName: "IX_PlatformTranslation_EntityId");

            migrationBuilder.RenameColumn(
                "EntityTranslationId",
                "GenreTranslation",
                "EntityId");

            migrationBuilder.RenameIndex(
                "IX_GenreTranslation_EntityTranslationId",
                table: "GenreTranslation",
                newName: "IX_GenreTranslation_EntityId");

            migrationBuilder.RenameColumn(
                "EntityTranslationId",
                "GameTranslation",
                "EntityId");

            migrationBuilder.RenameIndex(
                "IX_GameTranslation_EntityTranslationId",
                table: "GameTranslation",
                newName: "IX_GameTranslation_EntityId");

            migrationBuilder.AddForeignKey(
                "FK_GameTranslation_Game_EntityId",
                "GameTranslation",
                "EntityId",
                "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_GenreTranslation_Genre_EntityId",
                "GenreTranslation",
                "EntityId",
                "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_PlatformTranslation_Platform_EntityId",
                "PlatformTranslation",
                "EntityId",
                "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_PublisherTranslation_Publisher_EntityId",
                "PublisherTranslation",
                "EntityId",
                "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_GameTranslation_Game_EntityId",
                "GameTranslation");

            migrationBuilder.DropForeignKey(
                "FK_GenreTranslation_Genre_EntityId",
                "GenreTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PlatformTranslation_Platform_EntityId",
                "PlatformTranslation");

            migrationBuilder.DropForeignKey(
                "FK_PublisherTranslation_Publisher_EntityId",
                "PublisherTranslation");

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("2c10ff0a-c4e2-4a3f-8007-8dd1f70e5e35"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("50684a40-5ad3-4422-8ce5-481a8ee94fb6"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("5bb7c881-b24f-4032-8879-0aea65cec9b6"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("5f304498-3b45-44ce-9c9d-80e4a5d2f9c3"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("62582101-3eb5-4642-b152-6171da5a508e"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("73ba3fd4-f979-4dee-9c1b-21a4c63643aa"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("778906ce-28f0-43a5-83c4-284f74f956ad"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("799708b3-0b39-4881-abd2-9956d42e1f3d"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("88444a16-eeb6-4ecf-95a1-b34ee1711811"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("8b640c20-3561-4e89-9719-53144bb64732"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("c0257173-9ab9-4c7f-8ada-d4be4008982a"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("fbed2732-51f1-49d1-a75d-9d50517bc1d5"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("300c7307-8bf2-434f-8f99-f32100d8e166"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("7fd6d1ba-5568-4383-8e62-f3e49021f34e"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("3e90816c-3b74-4741-8bfb-911c99db5957"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("8fd90271-8665-4444-a4bc-6780a75c4f65"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("aa6d1f59-ff28-42a9-baa5-8f474f168d47"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("63375e2d-27a8-436e-b068-57e6882e1b90"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("676c17d3-b1ac-44a3-a184-d682b075570e"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("771d19d5-4a24-4652-b39f-6eab1c7d86ae"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("b551938c-d9d9-4dff-b3a5-d5967a004094"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("630e04f5-94e9-4b0a-b544-59cb4a20ce5c"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("c4e18047-c7d7-4640-9200-8d0971748732"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("f596caa2-092b-470a-901c-8fd87fda42a8"));

            migrationBuilder.RenameColumn(
                "EntityId",
                "PublisherTranslation",
                "EntityTranslationId");

            migrationBuilder.RenameIndex(
                "IX_PublisherTranslation_EntityId",
                table: "PublisherTranslation",
                newName: "IX_PublisherTranslation_EntityTranslationId");

            migrationBuilder.RenameColumn(
                "EntityId",
                "PlatformTranslation",
                "EntityTranslationId");

            migrationBuilder.RenameIndex(
                "IX_PlatformTranslation_EntityId",
                table: "PlatformTranslation",
                newName: "IX_PlatformTranslation_EntityTranslationId");

            migrationBuilder.RenameColumn(
                "EntityId",
                "GenreTranslation",
                "EntityTranslationId");

            migrationBuilder.RenameIndex(
                "IX_GenreTranslation_EntityId",
                table: "GenreTranslation",
                newName: "IX_GenreTranslation_EntityTranslationId");

            migrationBuilder.RenameColumn(
                "EntityId",
                "GameTranslation",
                "EntityTranslationId");

            migrationBuilder.RenameIndex(
                "IX_GameTranslation_EntityId",
                table: "GameTranslation",
                newName: "IX_GameTranslation_EntityTranslationId");

            migrationBuilder.AddForeignKey(
                "FK_GameTranslation_Game_EntityTranslationId",
                "GameTranslation",
                "EntityTranslationId",
                "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_GenreTranslation_Genre_EntityTranslationId",
                "GenreTranslation",
                "EntityTranslationId",
                "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_PlatformTranslation_Platform_EntityTranslationId",
                "PlatformTranslation",
                "EntityTranslationId",
                "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_PublisherTranslation_Publisher_EntityTranslationId",
                "PublisherTranslation",
                "EntityTranslationId",
                "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
