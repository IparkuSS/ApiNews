using Microsoft.EntityFrameworkCore.Migrations;

namespace APINews.Migrations
{
    public partial class AddRolesUpdateToBd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9b94bc5-cb02-4782-a252-42926ba517e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea1481ab-4209-4646-9b05-122824b2f7da");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99e8e505-bfc4-4837-8431-91fe2ba9d532", "2b70235f-48fd-40a9-9d84-d5c2d106675c", "Redactor", "REDACTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b270842e-da76-4e8e-bcab-fcf9dfc80ecf", "85df251e-a432-4c2a-8a24-33602a64398f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99e8e505-bfc4-4837-8431-91fe2ba9d532");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b270842e-da76-4e8e-bcab-fcf9dfc80ecf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea1481ab-4209-4646-9b05-122824b2f7da", "76268428-7042-4cbe-8414-82489568823b", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9b94bc5-cb02-4782-a252-42926ba517e5", "8fc7d0b9-9106-4eb7-b51e-b2dea10427cd", "Administrator", "ADMINISTRATOR" });
        }
    }
}
