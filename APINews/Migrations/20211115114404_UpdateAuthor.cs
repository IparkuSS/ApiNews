using Microsoft.EntityFrameworkCore.Migrations;

namespace APINews.Migrations
{
    public partial class UpdateAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b155f7a-86d4-40ae-aa88-ab1877a72f75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb151225-f9aa-4083-8341-d1e822b51759");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b3767967-abac-4887-9a6b-3d37103f8305", "b2f75d70-e564-4185-97b9-9e3801a9b474", "Redactor", "REDACTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83ff3f82-52c5-4558-9ee8-4099980009b5", "c53c21fe-da71-4f17-a60b-fe41386ab2eb", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83ff3f82-52c5-4558-9ee8-4099980009b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3767967-abac-4887-9a6b-3d37103f8305");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb151225-f9aa-4083-8341-d1e822b51759", "d9a35e48-a843-4142-aeb7-ae6f2c09bb2d", "Redactor", "REDACTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b155f7a-86d4-40ae-aa88-ab1877a72f75", "8e705539-def9-4c23-99ff-41f9bd0bd08a", "Administrator", "ADMINISTRATOR" });
        }
    }
}
