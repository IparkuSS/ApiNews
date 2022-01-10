using Microsoft.EntityFrameworkCore.Migrations;

namespace News.API.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea1481ab-4209-4646-9b05-122824b2f7da", "76268428-7042-4cbe-8414-82489568823b", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9b94bc5-cb02-4782-a252-42926ba517e5", "8fc7d0b9-9106-4eb7-b51e-b2dea10427cd", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9b94bc5-cb02-4782-a252-42926ba517e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea1481ab-4209-4646-9b05-122824b2f7da");
        }
    }
}
