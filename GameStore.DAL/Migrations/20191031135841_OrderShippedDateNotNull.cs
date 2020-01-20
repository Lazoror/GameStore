using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class OrderShippedDateNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<DateTime>(
                "ShippedDate",
                "Order",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("0a3d52f0-82d0-44c7-9ddd-990b25057820"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("2768160c-877d-4fed-b82f-c88e4dcff6c3"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("3a9cad66-9bb9-44c8-92df-e95a7f326ebf"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("65dea19a-53bf-4205-9a94-7d5a03c8f522"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("661518c5-b421-45ea-a745-c5d5c4f3434e"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("67fd3678-a02a-45af-a483-9068dd4c939a"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("8bca534e-736c-4259-9563-3a29572de9b6"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("aaad7c5f-944d-48ce-8257-e01c561950ef"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("b9f0dc99-9e7e-4f9e-b562-1f995f53a9ca"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("ca929b54-993b-4232-bd53-72f8575defe3"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("e5a58777-8af5-4f43-a700-fa4fbce2dee7"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("e8860073-9821-41ea-8218-85a7a6652c1f"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("8a05c943-b3df-46a3-900a-8e176882247e"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("df71808e-f968-478d-a0e2-6b8275693329"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("5e2658d2-02d9-4ca7-b539-232888904d0b"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("b4824060-bca4-4b87-83c2-02c99e14c148"));

            migrationBuilder.DeleteData(
                "Platform",
                "Id",
                new Guid("e462e441-19b7-407b-a5b7-c8486563c564"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("725f9d04-a47a-4ffa-a896-fe005e052267"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("9dde4920-33b5-4ce2-afeb-4a333674e805"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("cec10fef-e41f-483f-b09d-51b2af8434a3"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("d4267e6e-0315-49c8-b7cd-1a11a7bdd4d2"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("641a9e9e-a4e9-4672-addd-dda7d4941d86"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("94058fd9-862c-4cc2-a925-09e397628185"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("9a7c122a-eeca-46d1-8a88-b7d6b88221f5"));

            migrationBuilder.AlterColumn<DateTime>(
                "ShippedDate",
                "Order",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
