using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.DAL.Migrations
{
    public partial class HotDBFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("04dce021-eeea-433f-957c-1f8746b7a696"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("17d25633-f71c-42a7-bbb8-1148d22859e3"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("330f0c47-acab-4b1f-b65c-759806c1d352"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("34e9526c-c239-476d-95f5-5eac0b830967"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("3e91e90b-42f6-438f-ab98-97926e7d5dbf"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("4ce1bedf-4b5d-41b6-a0a8-b638fd859a3a"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("ac8f0494-0ef3-4510-b188-cf9da07ebc3b"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("affb53cb-b1a1-48a2-9aa7-6369997c0380"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("b61509e5-3dcb-400b-9f07-075c50b65113"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("b61bf8fe-7965-4629-bb6f-22f960779f09"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("c5d62f17-d7b3-4857-85d4-e7a497b1dd7e"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("d0e315e7-3b33-407e-8d2b-d1d23926ce53"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("f8f1c09c-1a58-4d05-8055-5801d1afc4b6"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("8e1ab276-a540-4467-acb2-ff90614ef6c8"));

            migrationBuilder.DeleteData(
                "Language",
                "Id",
                new Guid("f3d469cb-c23b-44e9-b707-9bb2d8f30224"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("50cf1df0-8ced-4160-a68d-2b878d7dff7e"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("5cdc0d9c-7f3b-486c-bcbf-1f2271376470"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("a6927baf-da7f-4c50-8b8e-0561bea00498"));

            migrationBuilder.DeleteData(
                "Role",
                "Id",
                new Guid("f0518960-06f5-419c-a5d1-f44f2d98098a"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("5c12e95e-ab6c-44e6-96d0-2faac90f77e0"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("6b192f2b-7fcc-47e3-9e95-08c44f328474"));

            migrationBuilder.DeleteData(
                "Genre",
                "Id",
                new Guid("fc4650c8-953e-40eb-9d58-99d1f7b78e2e"));
        }
    }
}
