using Microsoft.EntityFrameworkCore.Migrations;

namespace News.API.Migrations
{
    public partial class SubsectionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "9818a855-2497-41f5-89c4-e81f44566fc1", "fb172132-faf6-4be1-853a-b5385d52c76f", "Redactor", "REDACTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c15e97b9-c173-4fbf-b53b-fa8660601d28", "0b162882-4031-4a81-8b4b-6f4856e4f37b", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9818a855-2497-41f5-89c4-e81f44566fc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c15e97b9-c173-4fbf-b53b-fa8660601d28");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b3767967-abac-4887-9a6b-3d37103f8305", "b2f75d70-e564-4185-97b9-9e3801a9b474", "Redactor", "REDACTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83ff3f82-52c5-4558-9ee8-4099980009b5", "c53c21fe-da71-4f17-a60b-fe41386ab2eb", "Administrator", "ADMINISTRATOR" });
        }
    }
}
