using Microsoft.EntityFrameworkCore.Migrations;

namespace APINews.Migrations
{
    public partial class ModelsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "77d5a098-549c-4a0c-89f3-d7f74a83ab07", "83943a31-92fd-4d49-a823-a8f0010cdd77", "Redactor", "REDACTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6beefda7-13ec-4f5c-bb65-bf2b9a0ba31c", "4637e353-a34e-4144-8d33-a699660e8e93", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6beefda7-13ec-4f5c-bb65-bf2b9a0ba31c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77d5a098-549c-4a0c-89f3-d7f74a83ab07");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99e8e505-bfc4-4837-8431-91fe2ba9d532", "2b70235f-48fd-40a9-9d84-d5c2d106675c", "Redactor", "REDACTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b270842e-da76-4e8e-bcab-fcf9dfc80ecf", "85df251e-a432-4c2a-8a24-33602a64398f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
