using Microsoft.EntityFrameworkCore.Migrations;

namespace APINews.Migrations
{
    public partial class UserNewAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                column: "ConcurrencyStamp",
                value: "9e76c02e-048a-4a62-a4f5-826ebf5c044c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f269b9e-308f-4467-87e3-2a39a48d25fd",
                column: "ConcurrencyStamp",
                value: "b4c5a978-75c8-43c2-a31a-7fa40f285bc4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "719b61ee-41f3-41ee-9e10-48f8f3be9e6d", "AQAAAAEAACcQAAAAEEzcg5EB3fHsvn91yB9OptLkWuPE/vWHS82J2mjrUeGlzzn0bYdxyu37UDHGj65thg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                column: "ConcurrencyStamp",
                value: "2a4cb289-9b48-407b-82b7-92e53c655400");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f269b9e-308f-4467-87e3-2a39a48d25fd",
                column: "ConcurrencyStamp",
                value: "42276d05-14d0-4df5-8809-172c8137a6cc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3339a7ca-3afc-4eef-b5e7-82c0e92fd4e0", "AQAAAAEAACcQAAAAEJstG1VmzbUW4sGmt5ayVzj97qm6h1Jh1C9fMOYlZRGtRnGchOruJNS6h4A44tSGVA==" });
        }
    }
}
