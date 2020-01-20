using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class AddedRatingColumnsToGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("2c10ff0a-c4e2-4a3f-8007-8dd1f70e5e35"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("50684a40-5ad3-4422-8ce5-481a8ee94fb6"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("5bb7c881-b24f-4032-8879-0aea65cec9b6"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("5f304498-3b45-44ce-9c9d-80e4a5d2f9c3"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("62582101-3eb5-4642-b152-6171da5a508e"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("73ba3fd4-f979-4dee-9c1b-21a4c63643aa"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("778906ce-28f0-43a5-83c4-284f74f956ad"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("799708b3-0b39-4881-abd2-9956d42e1f3d"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("88444a16-eeb6-4ecf-95a1-b34ee1711811"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("8b640c20-3561-4e89-9719-53144bb64732"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("c0257173-9ab9-4c7f-8ada-d4be4008982a"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("fbed2732-51f1-49d1-a75d-9d50517bc1d5"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("300c7307-8bf2-434f-8f99-f32100d8e166"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("7fd6d1ba-5568-4383-8e62-f3e49021f34e"));

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("3e90816c-3b74-4741-8bfb-911c99db5957"));

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("8fd90271-8665-4444-a4bc-6780a75c4f65"));

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("aa6d1f59-ff28-42a9-baa5-8f474f168d47"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("63375e2d-27a8-436e-b068-57e6882e1b90"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("676c17d3-b1ac-44a3-a184-d682b075570e"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("771d19d5-4a24-4652-b39f-6eab1c7d86ae"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b551938c-d9d9-4dff-b3a5-d5967a004094"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("630e04f5-94e9-4b0a-b544-59cb4a20ce5c"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("c4e18047-c7d7-4640-9200-8d0971748732"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("f596caa2-092b-470a-901c-8fd87fda42a8"));

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Game",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RatingQuantity",
                table: "Game",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.InsertData(
            //    table: "Genre",
            //    columns: new[] { "Id", "IsDeleted", "Name", "ParentGenreId" },
            //    values: new object[,]
            //    {
            //        { new Guid("c5df3f86-dffc-4db3-81d8-b406c91474a4"), false, "Strategy", null },
            //        { new Guid("bd005a69-8ec2-45c7-808f-eb338ee54119"), false, "RPG", null },
            //        { new Guid("d5737819-8245-4619-8c44-08e3523f4121"), false, "Sports", null },
            //        { new Guid("88e9d083-317f-4b98-841d-88de381b9fd4"), false, "Races", null },
            //        { new Guid("7412cdd1-79c6-4e58-a4ab-4127457aaa56"), false, "Action", null },
            //        { new Guid("ff4ab017-8785-4f52-ab6b-d6133902675c"), false, "Adventure", null },
            //        { new Guid("e329857d-1a8b-4c44-b8eb-39a14e25e1e9"), false, "Puzzle & Skill", null }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Language",
            //    columns: new[] { "Id", "Code", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("2cea237c-6c44-4f8e-a16f-160402053b22"), "en", "English" },
            //        { new Guid("d43f3b05-2382-4c12-b839-4d45307c2043"), "ru", "Russian" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Platform",
            //    columns: new[] { "Id", "IsDeleted", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("16c4bc97-f035-4735-8fdc-af1b3633f371"), false, "Desktop" },
            //        { new Guid("fd1f44e2-9c74-4984-a9eb-162fd497c624"), false, "Mobile" },
            //        { new Guid("076aec29-994d-4946-832c-cb008965ad37"), false, "IOS" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Role",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("ee0a46c4-a0ac-4f0f-a0eb-9bc85a649b26"), "User" },
            //        { new Guid("ac8ef5b5-053f-4ca7-9529-512ff922890e"), "Moderator" },
            //        { new Guid("a68933a6-32a6-46b1-aa60-4186fd62ac6c"), "Manager" },
            //        { new Guid("39acdf0a-d4a2-4adf-8729-a63cc4bb74d8"), "Administrator" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Genre",
            //    columns: new[] { "Id", "IsDeleted", "Name", "ParentGenreId" },
            //    values: new object[,]
            //    {
            //        { new Guid("08756527-a005-46f4-a031-692521f11dfa"), false, "RTS", new Guid("c5df3f86-dffc-4db3-81d8-b406c91474a4") },
            //        { new Guid("983393e1-3caa-4a01-9c09-0040b154e584"), false, "TBS", new Guid("c5df3f86-dffc-4db3-81d8-b406c91474a4") },
            //        { new Guid("d4fc932b-0255-4434-b597-5fb9aa1d0750"), false, "Rally", new Guid("88e9d083-317f-4b98-841d-88de381b9fd4") },
            //        { new Guid("01e59d5c-4934-4bd1-acc2-7cb68fe68459"), false, "Arcade", new Guid("88e9d083-317f-4b98-841d-88de381b9fd4") },
            //        { new Guid("44b03411-cfd7-4e6e-83d6-5fd09b3de2ce"), false, "Formula", new Guid("88e9d083-317f-4b98-841d-88de381b9fd4") },
            //        { new Guid("13dbb093-b492-4e0f-b463-c465c107911c"), false, "Off-road", new Guid("88e9d083-317f-4b98-841d-88de381b9fd4") },
            //        { new Guid("5fa0e05f-08ac-42df-8d0a-4265c6fa18bd"), false, "FPS", new Guid("7412cdd1-79c6-4e58-a4ab-4127457aaa56") },
            //        { new Guid("41b037e1-f1ee-471b-8cc4-784b804fb229"), false, "TPS", new Guid("7412cdd1-79c6-4e58-a4ab-4127457aaa56") }
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("01e59d5c-4934-4bd1-acc2-7cb68fe68459"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("08756527-a005-46f4-a031-692521f11dfa"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("13dbb093-b492-4e0f-b463-c465c107911c"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("41b037e1-f1ee-471b-8cc4-784b804fb229"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("44b03411-cfd7-4e6e-83d6-5fd09b3de2ce"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("5fa0e05f-08ac-42df-8d0a-4265c6fa18bd"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("983393e1-3caa-4a01-9c09-0040b154e584"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("bd005a69-8ec2-45c7-808f-eb338ee54119"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("d4fc932b-0255-4434-b597-5fb9aa1d0750"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("d5737819-8245-4619-8c44-08e3523f4121"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("e329857d-1a8b-4c44-b8eb-39a14e25e1e9"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("ff4ab017-8785-4f52-ab6b-d6133902675c"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("2cea237c-6c44-4f8e-a16f-160402053b22"));

            migrationBuilder.DeleteData(
                table: "Language",
                keyColumn: "Id",
                keyValue: new Guid("d43f3b05-2382-4c12-b839-4d45307c2043"));

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("076aec29-994d-4946-832c-cb008965ad37"));

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("16c4bc97-f035-4735-8fdc-af1b3633f371"));

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("fd1f44e2-9c74-4984-a9eb-162fd497c624"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("39acdf0a-d4a2-4adf-8729-a63cc4bb74d8"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a68933a6-32a6-46b1-aa60-4186fd62ac6c"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("ac8ef5b5-053f-4ca7-9529-512ff922890e"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("ee0a46c4-a0ac-4f0f-a0eb-9bc85a649b26"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("7412cdd1-79c6-4e58-a4ab-4127457aaa56"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("88e9d083-317f-4b98-841d-88de381b9fd4"));

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: new Guid("c5df3f86-dffc-4db3-81d8-b406c91474a4"));

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "RatingQuantity",
                table: "Game");

            //migrationBuilder.InsertData(
            //    table: "Genre",
            //    columns: new[] { "Id", "IsDeleted", "Name", "ParentGenreId" },
            //    values: new object[,]
            //    {
            //        { new Guid("c4e18047-c7d7-4640-9200-8d0971748732"), false, "Strategy", null },
            //        { new Guid("2c10ff0a-c4e2-4a3f-8007-8dd1f70e5e35"), false, "RPG", null },
            //        { new Guid("88444a16-eeb6-4ecf-95a1-b34ee1711811"), false, "Sports", null },
            //        { new Guid("f596caa2-092b-470a-901c-8fd87fda42a8"), false, "Races", null },
            //        { new Guid("630e04f5-94e9-4b0a-b544-59cb4a20ce5c"), false, "Action", null },
            //        { new Guid("778906ce-28f0-43a5-83c4-284f74f956ad"), false, "Adventure", null },
            //        { new Guid("fbed2732-51f1-49d1-a75d-9d50517bc1d5"), false, "Puzzle & Skill", null }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Language",
            //    columns: new[] { "Id", "Code", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("7fd6d1ba-5568-4383-8e62-f3e49021f34e"), "en", "English" },
            //        { new Guid("300c7307-8bf2-434f-8f99-f32100d8e166"), "ru", "Russian" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Platform",
            //    columns: new[] { "Id", "IsDeleted", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("8fd90271-8665-4444-a4bc-6780a75c4f65"), false, "Desktop" },
            //        { new Guid("3e90816c-3b74-4741-8bfb-911c99db5957"), false, "Mobile" },
            //        { new Guid("aa6d1f59-ff28-42a9-baa5-8f474f168d47"), false, "IOS" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Role",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { new Guid("676c17d3-b1ac-44a3-a184-d682b075570e"), "User" },
            //        { new Guid("b551938c-d9d9-4dff-b3a5-d5967a004094"), "Moderator" },
            //        { new Guid("63375e2d-27a8-436e-b068-57e6882e1b90"), "Manager" },
            //        { new Guid("771d19d5-4a24-4652-b39f-6eab1c7d86ae"), "Administrator" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Genre",
            //    columns: new[] { "Id", "IsDeleted", "Name", "ParentGenreId" },
            //    values: new object[,]
            //    {
            //        { new Guid("8b640c20-3561-4e89-9719-53144bb64732"), false, "RTS", new Guid("c4e18047-c7d7-4640-9200-8d0971748732") },
            //        { new Guid("c0257173-9ab9-4c7f-8ada-d4be4008982a"), false, "TBS", new Guid("c4e18047-c7d7-4640-9200-8d0971748732") },
            //        { new Guid("50684a40-5ad3-4422-8ce5-481a8ee94fb6"), false, "Rally", new Guid("f596caa2-092b-470a-901c-8fd87fda42a8") },
            //        { new Guid("5bb7c881-b24f-4032-8879-0aea65cec9b6"), false, "Arcade", new Guid("f596caa2-092b-470a-901c-8fd87fda42a8") },
            //        { new Guid("799708b3-0b39-4881-abd2-9956d42e1f3d"), false, "Formula", new Guid("f596caa2-092b-470a-901c-8fd87fda42a8") },
            //        { new Guid("5f304498-3b45-44ce-9c9d-80e4a5d2f9c3"), false, "Off-road", new Guid("f596caa2-092b-470a-901c-8fd87fda42a8") },
            //        { new Guid("73ba3fd4-f979-4dee-9c1b-21a4c63643aa"), false, "FPS", new Guid("630e04f5-94e9-4b0a-b544-59cb4a20ce5c") },
            //        { new Guid("62582101-3eb5-4642-b152-6171da5a508e"), false, "TPS", new Guid("630e04f5-94e9-4b0a-b544-59cb4a20ce5c") }
            //    });
        }
    }
}
